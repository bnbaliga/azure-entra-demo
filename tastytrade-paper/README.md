# tastytrade paper-trading console app

A small Python console app that places trades in your **tastytrade sandbox (paper)**
account using the [`tastytrade`](https://github.com/tastyware/tastytrade) SDK.

It is hard-wired to the sandbox API (`https://api.cert.tastyworks.com`) and exits if the
session points anywhere else, so it can't send orders to a live account.

## 1. Get sandbox credentials

1. Sign in at <https://developer.tastytrade.com> and open the **Sandbox** section.
   This environment is separate from your real account. It has its own login and
   fake-money accounts.
2. Create a sandbox account if you don't have one yet.
3. Create an **OAuth application** for the sandbox and note its **client secret**.
4. Generate a **refresh token** for your sandbox user (a "personal grant").

Use the sandbox's secret and refresh token, not the live ones.

## 2. Install

Requires Python 3.11+.

```bash
cd tastytrade-paper
python -m venv .venv
source .venv/bin/activate        # Windows: .venv\Scripts\activate
pip install -r requirements.txt
cp .env.example .env             # then edit .env
```

`.env` is gitignored. Fill in `TT_SANDBOX_SECRET` and `TT_SANDBOX_REFRESH`.
These variable names are deliberately different from the SDK's defaults
(`TT_SECRET`/`TT_REFRESH`), so live credentials in your shell are never picked up.

## 3. Use

```bash
python paper_trade.py accounts                     # list sandbox accounts
python paper_trade.py balance
python paper_trade.py positions
python paper_trade.py orders                       # today's live orders

python paper_trade.py buy AAPL 10                  # market, Buy to Open
python paper_trade.py buy AAPL 10 --limit 150.25   # limit order
python paper_trade.py sell AAPL 10 --limit 160     # Sell to Close
python paper_trade.py sell AAPL 5 --open           # Sell to Open (short)
python paper_trade.py buy AAPL 5 --close           # Buy to Close (cover)
python paper_trade.py buy AAPL 10 --dry-run        # preview only
python paper_trade.py cancel 123456

python paper_trade.py                              # interactive menu
```

Each trade first runs a **dry run**, which shows the buying-power impact, fees and any
warnings. You then confirm with `y` before anything is sent. Add `-y` to skip the prompt.

Use `--account <number>` (or `TT_SANDBOX_ACCOUNT` in `.env`) to choose a specific
account. Otherwise the first open sandbox account is used.

## Notes

- The sandbox doesn't fill orders the way a real market does, so expect fills and quotes
  to look unrealistic.
- Only equities are supported. Options and futures can be added the same way with
  `Option.get(...)` / `Future.get(...)` and `build_leg(...)`.
