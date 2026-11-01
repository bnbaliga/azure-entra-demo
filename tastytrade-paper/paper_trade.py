#!/usr/bin/env python3
"""
Console app for placing trades in a tastytrade SANDBOX (paper) account.

This script only ever talks to the tastytrade certification/sandbox API
(https://api.cert.tastyworks.com). It refuses to run against the live API.

Usage examples:
    python paper_trade.py accounts
    python paper_trade.py balance
    python paper_trade.py positions
    python paper_trade.py orders
    python paper_trade.py buy AAPL 10                 # market order, Buy to Open
    python paper_trade.py buy AAPL 10 --limit 150.25  # limit order
    python paper_trade.py sell AAPL 10 --limit 160    # Sell to Close
    python paper_trade.py sell AAPL 5 --open          # Sell to Open (short)
    python paper_trade.py cancel 123456
    python paper_trade.py                             # interactive menu
"""

from __future__ import annotations

import argparse
import asyncio
import os
import sys
from decimal import Decimal, InvalidOperation

from tastytrade import CERT_URL, Account, Session
from tastytrade.instruments import Equity
from tastytrade.order import (
    LimitOrder,
    MarketOrder,
    OrderAction,
    OrderTimeInForce,
)

try:
    from dotenv import load_dotenv

    load_dotenv()
except ImportError:  # python-dotenv is optional
    pass


# --------------------------------------------------------------------------- #
# Session / safety
# --------------------------------------------------------------------------- #
def make_sandbox_session() -> Session:
    """Create a session that is guaranteed to point at the sandbox API."""
    secret = os.environ.get("TT_SANDBOX_SECRET")
    refresh = os.environ.get("TT_SANDBOX_REFRESH")
    if not secret or not refresh:
        sys.exit(
            "Missing TT_SANDBOX_SECRET / TT_SANDBOX_REFRESH. "
            "Copy .env.example to .env and fill in your SANDBOX credentials."
        )

    # is_test=True -> https://api.cert.tastyworks.com (paper/sandbox)
    session = Session(provider_secret=secret, refresh_token=refresh, is_test=True)

    # Belt and braces: never allow this tool to hit production.
    base_url = str(session._client.base_url).rstrip("/")
    if not session.is_test or base_url != CERT_URL:
        sys.exit(f"Refusing to run: session is not the sandbox API ({base_url}).")
    return session


async def pick_account(session: Session, account_number: str | None) -> Account:
    accounts = await Account.get(session)
    if not accounts:
        sys.exit("No open accounts found in the sandbox environment.")
    wanted = account_number or os.environ.get("TT_SANDBOX_ACCOUNT") or None
    if wanted:
        for acct in accounts:
            if acct.account_number == wanted:
                return acct
        sys.exit(f"Account {wanted} not found. Run the 'accounts' command to list them.")
    return accounts[0]


# --------------------------------------------------------------------------- #
# Commands
# --------------------------------------------------------------------------- #
async def cmd_accounts(session: Session, _args: argparse.Namespace) -> None:
    for acct in await Account.get(session):
        print(
            f"{acct.account_number:<12} {acct.nickname or '':<20} "
            f"{acct.account_type_name:<20} {acct.margin_or_cash}"
        )


async def cmd_balance(session: Session, args: argparse.Namespace) -> None:
    acct = await pick_account(session, args.account)
    bal = await acct.get_balances(session)
    print(f"Account:               {acct.account_number} (SANDBOX)")
    print(f"Cash balance:          {bal.cash_balance:,.2f}")
    print(f"Net liquidating value: {bal.net_liquidating_value:,.2f}")
    print(f"Equity buying power:   {bal.equity_buying_power:,.2f}")
    print(f"Derivative BP:         {bal.derivative_buying_power:,.2f}")


async def cmd_positions(session: Session, args: argparse.Namespace) -> None:
    acct = await pick_account(session, args.account)
    positions = await acct.get_positions(session)
    if not positions:
        print("No open positions.")
        return
    print(f"{'Symbol':<22}{'Type':<16}{'Qty':>10}  {'Dir':<6}{'Avg open':>12}{'Close':>12}")
    for p in positions:
        print(
            f"{p.symbol:<22}{p.instrument_type.value:<16}{p.quantity:>10}  "
            f"{p.quantity_direction:<6}{p.average_open_price:>12.2f}{p.close_price:>12.2f}"
        )


async def cmd_orders(session: Session, args: argparse.Namespace) -> None:
    acct = await pick_account(session, args.account)
    orders = await acct.get_live_orders(session)
    if not orders:
        print("No live orders today.")
        return
    for o in orders:
        legs = ", ".join(f"{leg.action.value} {leg.quantity} {leg.symbol}" for leg in o.legs)
        price = "" if o.price is None else f" @ {abs(o.price)}"
        print(f"#{o.id:<10} {o.status.value:<12} {o.order_type.value:<8} {legs}{price}")


async def cmd_cancel(session: Session, args: argparse.Namespace) -> None:
    acct = await pick_account(session, args.account)
    await acct.delete_order(session, args.order_id)
    print(f"Cancel requested for order #{args.order_id}.")


async def cmd_trade(session: Session, args: argparse.Namespace) -> None:
    acct = await pick_account(session, args.account)
    symbol = args.symbol.upper()

    try:
        quantity = Decimal(args.quantity)
        limit = Decimal(args.limit) if args.limit is not None else None
    except InvalidOperation:
        sys.exit("Quantity and limit price must be numbers.")
    if quantity <= 0:
        sys.exit("Quantity must be positive.")
    if limit is not None and limit <= 0:
        sys.exit("Limit price must be positive.")

    # Map buy/sell + open/close to tastytrade order actions.
    if args.side == "buy":
        action = OrderAction.BUY_TO_CLOSE if args.close else OrderAction.BUY_TO_OPEN
    else:
        action = OrderAction.SELL_TO_OPEN if args.open else OrderAction.SELL_TO_CLOSE

    equity = await Equity.get(session, symbol)
    leg = equity.build_leg(quantity, action)
    tif = OrderTimeInForce(args.tif)

    if limit is None:
        order = MarketOrder(time_in_force=tif, legs=[leg])
    else:
        # tastytrade sign convention: negative = debit (paying), positive = credit.
        price = -limit if args.side == "buy" else limit
        order = LimitOrder(time_in_force=tif, legs=[leg], price=price)

    desc = f"{action.value} {quantity} {symbol} " + (
        "@ MARKET" if limit is None else f"@ LIMIT {limit}"
    )
    print(f"\n[SANDBOX] Account {acct.account_number}: {desc} ({tif.value})")

    # 1) Dry run: validates the order and shows buying-power impact, places nothing.
    preview = await acct.place_order(session, order, dry_run=True)
    bpe = preview.buying_power_effect
    print(f"  Buying power change: {bpe.change_in_buying_power:,.2f}")
    print(f"  New buying power:    {bpe.new_buying_power:,.2f}")
    if preview.fee_calculation:
        print(f"  Est. fees:           {preview.fee_calculation.total_fees:,.2f}")
    for w in preview.warnings or []:
        print(f"  WARNING: {w.message}")
    if preview.errors:
        for e in preview.errors:
            print(f"  ERROR: {e.message}")
        sys.exit("Order rejected in preview; nothing was sent.")

    if args.dry_run:
        print("Dry run only; order not sent.")
        return

    if not args.yes:
        answer = input("Send this order to the sandbox? [y/N] ").strip().lower()
        if answer not in ("y", "yes"):
            print("Cancelled; nothing was sent.")
            return

    # 2) Real placement (still sandbox).
    result = await acct.place_order(session, order, dry_run=False)
    placed = result.order
    print(f"Order #{placed.id} submitted, status: {placed.status.value}")


# --------------------------------------------------------------------------- #
# Interactive menu (when run with no arguments)
# --------------------------------------------------------------------------- #
async def interactive(session: Session) -> None:
    ns = argparse.Namespace(account=None)
    menu = {
        "1": ("List accounts", cmd_accounts),
        "2": ("Show balance", cmd_balance),
        "3": ("Show positions", cmd_positions),
        "4": ("Show live orders", cmd_orders),
        "5": ("Place a trade", None),
        "6": ("Cancel an order", None),
        "q": ("Quit", None),
    }
    while True:
        print("\n=== tastytrade PAPER (sandbox) ===")
        for key, (label, _) in menu.items():
            print(f"  {key}) {label}")
        choice = input("> ").strip().lower()
        try:
            if choice == "q":
                return
            if choice in ("1", "2", "3", "4"):
                await menu[choice][1](session, ns)
            elif choice == "5":
                side = input("buy or sell? ").strip().lower()
                if side not in ("buy", "sell"):
                    print("Please type buy or sell.")
                    continue
                trade_args = argparse.Namespace(
                    account=None,
                    side=side,
                    symbol=input("Symbol: ").strip(),
                    quantity=input("Quantity: ").strip(),
                    limit=input("Limit price (blank = market): ").strip() or None,
                    tif="Day",
                    open=False,
                    close=False,
                    dry_run=False,
                    yes=False,
                )
                await cmd_trade(session, trade_args)
            elif choice == "6":
                order_id = int(input("Order id: ").strip())
                await cmd_cancel(session, argparse.Namespace(account=None, order_id=order_id))
            else:
                print("Unknown choice.")
        except SystemExit as exc:  # keep the menu alive after validation errors
            if exc.code:
                print(exc.code)
        except Exception as exc:  # noqa: BLE001 - show API errors and continue
            print(f"Error: {exc}")


# --------------------------------------------------------------------------- #
# CLI
# --------------------------------------------------------------------------- #
def build_parser() -> argparse.ArgumentParser:
    parser = argparse.ArgumentParser(
        description="Trade a tastytrade SANDBOX (paper) account from the console."
    )
    parser.add_argument("--account", help="Sandbox account number (default: first / $TT_SANDBOX_ACCOUNT)")
    sub = parser.add_subparsers(dest="command")

    sub.add_parser("accounts", help="List sandbox accounts").set_defaults(func=cmd_accounts)
    sub.add_parser("balance", help="Show balances").set_defaults(func=cmd_balance)
    sub.add_parser("positions", help="Show open positions").set_defaults(func=cmd_positions)
    sub.add_parser("orders", help="Show today's live orders").set_defaults(func=cmd_orders)

    cancel = sub.add_parser("cancel", help="Cancel an order")
    cancel.add_argument("order_id", type=int)
    cancel.set_defaults(func=cmd_cancel)

    for side in ("buy", "sell"):
        p = sub.add_parser(side, help=f"{side.capitalize()} an equity")
        p.add_argument("symbol")
        p.add_argument("quantity")
        p.add_argument("--limit", help="Limit price; omit for a market order")
        p.add_argument("--tif", default="Day", choices=["Day", "GTC", "Ext"], help="Time in force")
        if side == "buy":
            p.add_argument("--close", action="store_true", help="Buy to Close (cover a short)")
        else:
            p.add_argument("--open", action="store_true", help="Sell to Open (go short)")
        p.add_argument("--dry-run", action="store_true", help="Preview only; do not send")
        p.add_argument("-y", "--yes", action="store_true", help="Skip the confirmation prompt")
        p.set_defaults(func=cmd_trade, side=side, open=False, close=False)
    return parser


async def main() -> None:
    args = build_parser().parse_args()
    async with make_sandbox_session() as session:
        if args.command is None:
            await interactive(session)
        else:
            await args.func(session, args)


if __name__ == "__main__":
    asyncio.run(main())
