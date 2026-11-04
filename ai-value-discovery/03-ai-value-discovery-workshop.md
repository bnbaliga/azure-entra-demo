# Step 3 — 3-Day AI Value Discovery Workshop

> **Objective:** Agree on 5–6 high-value AI agent use cases, with baselines and success criteria, ready to be scoped into an **Outcome-Based Pilot**. This includes the customer's two stated challenges: **knowledge silos across acquired apps** and **maintenance across several tech stacks**.
> **Format:** On-site preferred (hybrid possible) · Day 1 = orientation + leadership alignment · Days 2–3 = BU discovery sessions
> **Inputs:** [Use-case catalogue](01-agentic-ai-use-cases.md) · [Discovery Questionnaire](02-discovery-questionnaire.md) responses from each BU

---

## 0. Pre-Workshop Preparation (T-3 weeks → T-0)

| When | Activity | Owner |
|---|---|---|
| T-3 wks | Kick-off call with the executive sponsor; confirm BU list, attendees, logistics, NDA/data-sharing terms | Engagement Lead + Customer PMO |
| T-3 wks | Send the Discovery Questionnaire (customer-facing version, without 🔎 signals) to each BU head | Engagement Lead |
| T-2 wks | Request sample artefacts: process maps, org charts, KPI dashboards, application inventory/CMDB, sample anonymised cases | Business Analyst |
| T-1 wk | Collect questionnaire responses; pre-score candidates on the rubric; prepare a **one-page BU brief** per session with hypotheses and follow-up questions | Value Analyst + AI Architect |
| T-1 wk | Tailor Day-1 demos to the customer's top hypotheses (e.g. knowledge agent on sample public docs; multi-stack code agent on an open-source repo) | Solution Engineer |
| T-2 days | Confirm room, AV, sandbox/demo access, and a Wi-Fi/guest network that allows demos | Customer PMO |

**Our team on site:** Engagement Lead (facilitator), AI Solution Architect, Domain SME (Payments/Risk/Lending), Business Analyst / Value Analyst (scribe + baselining), Solution Engineer (demos). Optional: Change & Adoption Lead.

**Customer participants:** Executive sponsor (CEO/COO/CDO/CTO), BU heads, process owners and SMEs, IT/Enterprise Architecture, InfoSec, Data, Risk/Compliance (2nd line), HR/Change.

---

## Day 1 — Business AI Orientation (6 hrs) + Senior Leadership Alignment (2 hrs)

### Part A: Business AI Orientation — 6 hours
*Audience: BU heads, process owners, IT, Risk, Data (25–40 people)*

| Time | Session | Content | Outcome |
|---|---|---|---|
| 09:00–09:30 | **Welcome & objectives** | Sponsor opening; workshop goals; how the 3 days lead to a pilot | Shared purpose |
| 09:30–10:30 | **AI in 2026: from GenAI to Agentic AI** | LLMs, RAG, tool use, multi-agent orchestration, MCP; Assist / Augment / Autonomous patterns; what agents can and cannot do yet; myths and realities | Common vocabulary |
| 10:30–10:45 | *Break* | | |
| 10:45–11:45 | **Agentic AI in Fintech: what peers are doing** | Industry case studies across KYC, AML, disputes, servicing, payment ops, lending, finance; typical value ranges and lessons learnt | "Art of the possible" |
| 11:45–12:30 | **Live demos (accelerators)** | (1) Enterprise Knowledge Agent answering across several "acquired" sources with citations and permission trimming. (2) Multi-stack code agent documenting a legacy service and raising a dependency-upgrade PR. (3) AML alert triage or AP invoice agent with a human-review workbench | Belief that this is real and near-term |
| 12:30–13:15 | *Lunch* | | |
| 13:15–14:00 | **Responsible & Regulated AI** | Model risk (SR 11-7 / SS1/23), EU AI Act, DORA, privacy, PCI; human-in-the-loop; explainability and audit; how we get 2nd line to "yes" | Risk and Compliance as partners, not blockers |
| 14:00–14:45 | **Data, Platform & Architecture Foundations** | Reference architecture; integrating with a heterogeneous estate from acquisitions; permission-aware retrieval; build vs buy; pilot-to-production path; cost (FinOps) | IT/EA alignment |
| 14:45–15:00 | *Break* | | |
| 15:00–15:45 | **Value Framework & Business Case** | How we measure: baseline → pilot → projected 8–12-month value; cost, revenue, risk and CX levers; ranged success criteria; scoring rubric | Shared definition of "value" |
| 15:45–16:30 | **Hands-on ideation ("Agent Canvas")** | BU tables map their top pain points to agent patterns using the canvas (trigger, inputs, steps, tools, decision, human checkpoint, KPI). Each table pitches 2 ideas in 3 minutes | Initial idea backlog; engagement |
| 16:30–17:00 | **Wrap-up & Days 2–3 briefing** | Explain the discovery-session format; confirm SME attendance; questionnaire gaps | Ready for discovery |

> **Agent Canvas (one per idea):** Problem & volume · Who does it today (FTE) · Trigger · Inputs/data · Steps the agent performs · Systems/tools it calls · Decision & human checkpoint · Risk/regulatory considerations · KPI & baseline · Value hypothesis.

### Part B: Expectation Setting with Senior Leaders — 2 hours
*Audience: Executive sponsor + C-suite / ExCo (6–10 people). Closed session.*

| Time | Session | Key questions / content |
|---|---|---|
| 17:15–17:35 | **Strategic intent** | Why AI now? Is the main goal cost-to-serve, growth without adding headcount, risk and compliance, CX, or post-acquisition integration? What does "AI-first" mean here? |
| 17:35–18:00 | **Validating the stated challenges** | (1) **Knowledge silos:** how many entities and apps, and what does it cost in productivity, errors and customer impact? Is harmonisation a board priority? (2) **Tech-stack sprawl:** maintenance share of IT budget; appetite for consolidation; target reference stack; examples where these issues hurt a customer, an audit or a launch |
| 18:00–18:25 | **Ambition, risk appetite & guardrails** | Where would leadership accept autonomous agents, and where must humans always decide? Regulator relationship. Brand and reputational red lines. Workforce stance (redeploy vs reduce) and communication |
| 18:25–18:45 | **Success measures & investment logic** | What results in 12 weeks would justify scaling? Value threshold (e.g. 30–40% cost-savings potential). Budget envelope; how an outcome-based / risk-shared pilot fits procurement; is an RFP expected for scale-up? |
| 18:45–19:05 | **Governance & sponsorship** | Name the pilot steering committee, BU sponsors and decision rights; cadence; approval path (InfoSec, Privacy, MRM, Procurement) and lead times |
| 19:05–19:15 | **Close** | Confirm the top 3 leadership priorities to steer Days 2–3 |

**Day-1 deliverable (issued that evening):** a one-page **Leadership Intent Statement** covering objectives, priorities, guardrails, success definition and governance. All Day 2–3 sessions are tested against it.

---

## Days 2 & 3 — BU Discovery Sessions

### Standard 60-minute session structure
Every session uses that BU's **questionnaire responses** as the baseline, so the hour is spent validating and going deeper, not collecting data.

| Minutes | Segment | What happens |
|---|---|---|
| 0–5 | Context | Recap of Leadership Intent; purpose of the session |
| 5–20 | **AS-IS walkthrough** | Process owner walks through the 1–2 highest-effort processes (from A2). We sketch a swim-lane on the whiteboard: steps, systems, hand-offs, waits |
| 20–35 | **Pain points & root causes** | Probe the 🔎 signals: volumes, FTE, systems per case, backlog, errors, audit findings. Run the **Section X knowledge and tech-stack probes**: where silos and duplicated apps slow this BU |
| 35–50 | **Opportunity shaping** | Map pain points to catalogue use cases (C1–C10, H1–H10, X1–X2); complete or refine Agent Canvases; agree the human checkpoint |
| 50–60 | **Data, risk & next steps** | Confirm data availability (D2), APIs (D3), approvals (D6), baseline method (D8), and a named SME. Agree follow-ups |

**Deep-dive buffer (60 min per day):** a flexible block for sessions that need more time, follow-ups with SMEs, a whiteboard architecture session with IT, or screen-share walkthroughs of the real systems ("show me how you do it today"). Allocated live by the facilitator.

### Day 2 — Core Fintech Processes

| Time | Session (60 min) | BU / Attendees | Use cases in focus |
|---|---|---|---|
| 08:45–09:00 | Team huddle | Our team + Customer PMO | |
| 09:00–10:00 | **Session 1** | Customer Service / Contact Centre | C6, X1 |
| 10:15–11:15 | **Session 2** | Onboarding / KYC / Merchant & Partner Risk | C1, C9 |
| 11:30–12:30 | **Session 3** | Financial Crime (AML/Sanctions) + Fraud & Disputes | C2, C3 |
| 12:30–13:15 | *Lunch* | | |
| 13:15–14:15 | **Session 4** | Payment Operations / Treasury Ops / Reconciliation | C8, H2 |
| 14:30–15:30 | **Session 5** | Lending / Credit / Collections | C4, C5 |
| 15:30–16:30 | **Deep-dive buffer** | As needed; recommended default: system walkthrough with the highest-scoring BU | — |
| 16:30–17:15 | Internal synthesis | Score candidates on the rubric; update heat map | — |
| 17:15–17:30 | Daily sponsor check-in | Emerging themes; any course corrections | — |

### Day 3 — Horizontal Functions, Technology & Read-out

| Time | Session (60 min) | BU / Attendees | Use cases in focus |
|---|---|---|---|
| 08:45–09:00 | Team huddle | | |
| 09:00–10:00 | **Session 6** | **IT / Engineering / Enterprise Architecture / InfoSec** (dedicated to the *tech-stack sprawl* challenge: application inventory, overlap, maintenance load, CVE/EOL, target stack) | X2, H6, H7 |
| 10:15–11:15 | **Session 7** | **Cross-team Knowledge Silo session** (knowledge-management owners, Ops leads from several acquired entities, HR/L&D, Intranet/Collaboration owner): repositories, conflicting procedures, expertise dependency, identity tenants | X1, H5 |
| 11:30–12:30 | **Session 8** | Finance (AP, R2R, FP&A) + Procurement | H1, H2, H3, H8 |
| 12:30–13:15 | *Lunch* | | |
| 13:15–14:15 | **Session 9** | Compliance / Risk / Internal Audit / Legal | C7, H8, H9 |
| 14:30–15:30 | **Session 10** | HR / People + Sales, Marketing & RevOps (+ Wealth/Product if in scope) | H4, H5, H10, C10 |
| 15:30–16:30 | **Deep-dive buffer** | As needed; recommended default: architecture and data feasibility check on the top 8 candidates with IT/EA and InfoSec | — |
| 16:30–17:00 | Internal synthesis | Final scoring; draft pilot portfolio | — |
| 17:00–17:45 | **Executive read-out** | Sponsor + BU heads: top-10 opportunity heat map; **recommended pilot portfolio of 5–6 (anchored on X1 + X2)**; value hypotheses; next steps to the proposal | Agreement in principle on pilot scope |

> If there are more than 10 BUs, either combine adjacent functions (as above) or run some sessions as 45-minute virtual follow-ups in the week after the workshop.

---

## Workshop Outputs (delivered within 5–7 working days)

| # | Deliverable | Description |
|---|---|---|
| 1 | **Leadership Intent Statement** | Objectives, guardrails, success definition, governance (Day 1) |
| 2 | **AS-IS Process & Pain-Point Pack** | Swim-lanes, volumes, FTE, systems and pain points per BU, including a cross-BU **Knowledge & Application Landscape** map (repositories, overlapping apps, stacks) |
| 3 | **AI Opportunity Heat Map** | All candidates scored on the rubric (value × feasibility), with a ranked top 10 |
| 4 | **Use-Case Briefs (top 8)** | Agent Canvas, TO-BE flow, data and integration needs, risk classification, KPI baseline method, value hypothesis (low/high range) |
| 5 | **Recommended Pilot Portfolio (5–6)** | X1 + X2 anchors plus 3–4 domain use cases, balanced between high-confidence (accelerator-backed) and stretch; dependencies; approvals plan |
| 6 | **Outcome-Based Pilot Proposal** | Fixed price, success criteria, risk share, plan. See [04 – Proposal](04-outcome-based-pilot-proposal.md) |
| 7 | **Wave-2 Roadmap** | Remaining opportunities sequenced toward production, including the app-rationalisation opportunity from X2 |

---

## Facilitation Tips

- **Follow the volume, the FTEs and the wait time.** Value sits where the three overlap.
- **Ask "show me".** Ten minutes of screen-share reveals more than an hour of discussion, especially for knowledge silos ("find the latest refund policy for Entity B").
- **Bring Risk/Compliance into the room early.** Make them co-designers of the human checkpoints.
- **Record baselines on the spot** (volume, AHT, FTE, error rate) and agree how they will be measured during the pilot.
- **Be candid** that not every idea will work. This is innovation, and it sets up the risk-shared commercial model.

---

**Previous:** [02 – Questionnaire](02-discovery-questionnaire.md) · **Next:** [04 – Outcome-Based Pilot Proposal](04-outcome-based-pilot-proposal.md)
