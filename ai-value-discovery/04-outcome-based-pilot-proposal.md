# Step 4 — Outcome-Based Agentic AI Pilot: Fixed-Price Proposal

> **Client:** [Fintech Client Name] · **Prepared by:** [Partner Name] · **Version:** 1.0 (draft, before workshop validation) · **Validity:** 60 days
> **Basis:** Findings from the AI Value Discovery Workshop ([03](03-ai-value-discovery-workshop.md)), the use-case catalogue ([01](01-agentic-ai-use-cases.md)) and the accelerators ([05](05-ai-implementation-accelerators.md)).
> *All figures are illustrative until the workshop baselines are confirmed.*

---

## 1. Executive Summary

[Client] has said that **acquisitions have left too many applications and siloed knowledge**, and that **maintaining several tech stacks** is a growing burden. The same issues push up cost-to-serve in core processes such as servicing, financial crime and payment operations.

We propose a **14-week, fixed-price, outcome-based pilot** of **6 agentic AI use cases**. We **commit that at least 3 of the 6 (50%) will demonstrate business value** against agreed, ranged success criteria, centred on **30–40% cost-savings potential on the in-scope process, projected over 8–12 months.** **30% of our fee is at risk** against that commitment.

AI value comes from innovation and process re-engineering, so **not every use case will succeed**, and we say so openly. Our commitment covers the portfolio, not each individual use case. We treat our at-risk fee and our accelerator investment as **co-invested R&D**: if things go wrong, we carry a meaningful share of the cost.

We build on our **pre-configured AI Implementation Accelerators**, which cut pilot effort by about **48%** (illustrative: 565 vs 1,080 person-days). They are **production-grade by design**, so successful pilots are **extended into production, not thrown away and rebuilt.**

| | |
|---|---|
| **Scope** | 6 use cases: 2 anchors for the stated challenges + 4 domain use cases |
| **Duration** | 14 weeks (2 mobilise · 8 build/iterate · 4 measured run, overlapping · 2 value assessment) |
| **Commitment** | ≥ 3 of 6 use cases meet "Value Demonstrated" criteria |
| **Fixed price** | **USD 565,000** (70% milestone-based / 30% at risk on the outcome) |
| **Excluded** | Cloud and LLM consumption (est. USD 25–45K, customer subscription or pass-through), third-party licences |
| **Partner co-investment** | ~USD 515K of effort avoided through accelerators + USD 169.5K fee at risk |

---

## 2. Pilot Scope — 6 Use Cases

*Indicative portfolio. The final 4 domain use cases are confirmed at the workshop read-out using the scoring rubric. Swapping like-for-like use cases from the catalogue does not change the price, provided the accelerator fit and complexity are equivalent (see Assumption A12).*

| # | Use case | Customer challenge addressed | In-scope slice for the pilot | Confidence | Accelerator |
|---|---|---|---|---|---|
| **UC1** | **Enterprise Knowledge & Expertise Agent** (X1) | Knowledge silos across acquired apps | 4–6 knowledge sources (e.g. 2× SharePoint tenants, Confluence, ServiceNow KB, one legacy intranet); 2–3 pilot teams (~150–300 users) from at least 2 acquired entities | High | ✅ |
| **UC2** | **Multi-Stack Engineering Maintenance & Modernisation Agent** (X2) | Tech-stack maintenance | 15–25 repositories across 3 stacks (e.g. .NET, Java, Node/PHP): code knowledge graph, auto-documentation, CVE/dependency-upgrade PRs, test generation; **app-overlap rationalisation analysis** for one capability domain (e.g. payment gateways) | Medium (stretch) | ◐ |
| **UC3** | Customer Service Agent-Assist & Tier-1 Servicing (C6) | Agents searching across apps from different acquisitions | Top 5–8 contact intents, one channel (chat), agent copilot + bounded self-service; reuses UC1's knowledge layer | High | ✅ |
| **UC4** | AML Alert Triage & SAR Narrative Agent (C2) | Investigators gathering data across systems | One TM scenario family (e.g. rapid movement of funds); L1 triage recommendation + SAR draft; shadow mode, human disposes | Medium (stretch: MRM) | ✅ |
| **UC5** | Payment Exceptions & Reconciliation Agent (C8) | Ops effort on breaks across acquired ledgers and PSPs | 1 payment rail + 2–3 settlement reconciliations (e.g. PSP/acquirer vs ledger) | High | ✅ |
| **UC6** | AP Invoice-to-Pay Agent (H1) | Finance efficiency | Non-PO and PO invoices for one entity/ERP; extraction, matching, coding, exception emails | High | ✅ |

**Portfolio design:** 4 high-confidence and 2 stretch use cases. This keeps the ≥3 commitment realistic while leaving room for innovation (UC2 modernisation, UC4 under model-risk scrutiny).

**Shared components built once and reused:** identity and permission-aware retrieval (UC1 → UC3, UC4), connector/adapter layer (all), reviewer workbench (UC3–UC6), evaluation harness, observability, audit ledger.

---

## 3. Success Criteria (Ranged)

### 3.1 Portfolio-level commitment
> **At least 3 of the 6 use cases reach the "Value Demonstrated" rating** below, measured during the 4-week measured-run period and projected over 8–12 months.

### 3.2 Rating scale (per use case)

| Rating | Cost-savings potential on the in-scope process* | Quality guardrail | Adoption guardrail |
|---|---|---|---|
| **Value Exceeded** | > 40% | Quality ≥ baseline | ≥ 70% of pilot users active weekly |
| **Value Demonstrated** ✅ | **30–40%** | Quality ≥ baseline (no statistically significant degradation) | ≥ 50% of pilot users active weekly |
| **Partial Value** | 15–29% | Quality ≥ baseline | ≥ 30% |
| **Not Demonstrated** | < 15%, or a guardrail failed | — | — |

\* **Cost-savings potential** = (baseline effort per unit − pilot effort per unit) ÷ baseline effort per unit, applied to annualised volume × fully loaded cost, and projected over 8–12 months with the agreed ramp-up curve. For revenue- or risk-led use cases, an equivalent agreed metric replaces it (see 3.3). Savings are *potential*: realising them depends on how the client redeploys capacity.

### 3.3 Use-case-specific metrics (indicative; finalised in Week 2 baseline sign-off)

| UC | Primary value metric | "Value Demonstrated" range | Quality guardrail |
|---|---|---|---|
| UC1 | Time spent finding information and repeat questions/escalations (hours per user per week) | 30–40% reduction | Answer groundedness ≥ 90%; citation accuracy ≥ 95%; zero permission leaks |
| UC2 | Engineering effort per maintenance task (CVE fix, upgrade, doc, test) + onboarding time to an unfamiliar codebase | 30–40% reduction | PR acceptance ≥ 60%; no increase in defect escape rate; rationalisation analysis accepted by EA |
| UC3 | AHT on in-scope intents + containment | 30–40% cost-per-contact reduction | CSAT ≥ baseline; compliance QA ≥ baseline |
| UC4 | L1 investigation time per alert + SAR drafting time | 30–40% reduction | Recommendation agreement with investigators ≥ 85%; zero missed true positives in the sample |
| UC5 | Manual effort per exception/break; break ageing | 30–40% reduction | Match precision ≥ 98%; no incorrect auto-repairs |
| UC6 | Cost/time per invoice; touchless rate | 30–40% reduction | Field accuracy ≥ 97%; zero duplicate payments |

### 3.4 Measurement method
- **Baseline (Weeks 1–2):** taken from system logs, WFM/ITSM/TM data or a 2-week time-and-motion sample, then **jointly signed off.**
- **Measured run (minimum 4 weeks per UC):** shadow mode first, then live with human-in-the-loop, on a statistically meaningful sample agreed per UC.
- **Independent check:** the client's Finance or Transformation Office validates the value calculation. Disputes go to the Steering Committee (see §9).

---

## 4. Approach & Timeline (14 weeks)

```
Week:        1   2   3   4   5   6   7   8   9  10  11  12  13  14
Mobilise    ███████
 Baseline    ███████
Platform      ░░░████                         (accelerator deploy into client tenant)
Wave A (UC1, UC3, UC6)    ████████████  ▶ measured run ████████████
Wave B (UC2, UC4, UC5)            ████████████████  ▶ measured run █████████
Value assessment & business case                              ███████
Steering Committee   ◆       ◆       ◆       ◆       ◆       ◆   ◆(final)
```

| Phase | Weeks | Key activities | Exit / milestone |
|---|---|---|---|
| **0. Mobilise** | 1–2 | Accelerator landing zone in the client cloud tenant; security/privacy/MRM approvals; data access; baseline sign-off; final UC confirmation | **M1:** Environment live; baselines signed |
| **1. Wave A build** | 3–7 | UC1, UC3, UC6: configure accelerator packs, connectors, prompts/agents, evaluation sets; UAT | **M2:** Wave A in measured run (Week 7) |
| **2. Wave B build** | 5–10 | UC2, UC4, UC5: build stretch use cases; MRM review for UC4 | **M3:** Wave B in measured run (Week 10) |
| **3. Measured run** | 7–13 | Shadow → live (HITL); weekly value dashboard; tuning sprints | Value data captured |
| **4. Value assessment** | 13–14 | Value report per UC; 8–12-month projection; production architecture and estimate; scale roadmap; **RFP-ready pack** | **M4:** Final read-out & outcome determination |

**Delivery method:** 2-week agile sprints, a demo every sprint, and a weekly value dashboard (usage, quality, effort, cost per unit).

---

## 5. Commercials — Fixed Price

### 5.1 Effort and how the accelerators reduce it (illustrative)

| Work package | Traditional build (person-days) | With accelerators (person-days) | Reduction |
|---|---:|---:|---:|
| Platform foundation (landing zone, identity, gateway, observability, audit) | 120 | 40 | 67% |
| UC1 Enterprise Knowledge Agent ✅ | 130 | 55 | 58% |
| UC2 Multi-Stack Maintenance Agent ◐ | 160 | 100 | 38% |
| UC3 Customer Service Agent ✅ | 130 | 60 | 54% |
| UC4 AML Triage Agent ✅ | 150 | 75 | 50% |
| UC5 Payment Exceptions & Recon ✅ | 130 | 65 | 50% |
| UC6 AP Invoice Agent ✅ | 110 | 50 | 55% |
| Value measurement & business case | 60 | 50 | 17% |
| Programme management, change & adoption | 90 | 70 | 22% |
| **Total** | **1,080** | **565** | **~48%** |

Blended rate (onshore/offshore mix) at an illustrative **USD 1,000 per person-day**: **traditional ≈ USD 1.08M; accelerated = USD 565K.**

### 5.2 Fee structure — USD 565,000 fixed

**Base fee: 70% = USD 395,500**, paid on milestones:

| Milestone | % of total | Amount |
|---|---:|---:|
| M1 — Environment live, baselines signed (Week 2) | 15% | 84,750 |
| M2 — Wave A in measured run (Week 7) | 25% | 141,250 |
| M3 — Wave B in measured run (Week 10) | 20% | 113,000 |
| M4 — Final value report delivered (Week 14) | 10% | 56,500 |

**At-risk outcome fee: 30% = USD 169,500**, payable at M4 based on results:

| Outcome (UCs rated "Value Demonstrated" or better) | At-risk fee payable |
|---|---|
| **5–6 of 6** | 100% (USD 169,500), plus an optional success bonus of USD 56,500 (10%), *or* waived if the production SOW is signed within 90 days |
| **3–4 of 6** (commitment met) | 100% (USD 169,500) |
| **2 of 6** | 40% (USD 67,800) |
| **0–1 of 6** | 0%. The partner also provides up to **4 weeks of remediation effort at no cost** for the most promising use case, if the client wishes |

**Production credit:** if the client signs a production/scale SOW with the partner within 90 days of M4, **25% of the base fee (USD 98,875) is credited** against that SOW.

### 5.3 Partner co-investment ("R&D investment")
| Item | Value |
|---|---|
| Accelerator effort avoided (1,080 − 565 PD) | ~USD 515,000 |
| Accelerator IP licence for the pilot term | Included (no charge) |
| Maximum at-risk fee | USD 169,500 |
| Optional remediation (worst case) | Up to ~USD 60,000 of effort |

### 5.4 Excluded from the fixed price
- Cloud infrastructure and LLM/API consumption (estimated **USD 25–45K** for the pilot), billed to the client's subscription or as pass-through at cost.
- Third-party software licences (e.g. IDV, screening, OCR vendors) beyond what the client already holds.
- Client staff time (see §7).
- Production hardening, rollout and hypercare after the pilot (priced separately; see §8).

---

## 6. Assumptions

| # | Assumption |
|---|---|
| A1 | The pilot runs in the **client's cloud tenant** (Azure preferred; AWS/GCP supported) with an approved LLM service (e.g. Azure OpenAI / Anthropic Claude via Bedrock or Vertex / other approved models). |
| A2 | Environment access, network connectivity, service accounts and data access are provided **by end of Week 2**. Each week of delay moves the timeline by one week; the fixed price holds for up to 3 weeks of client-caused delay, after which change control applies. |
| A3 | InfoSec, Privacy (DPIA), Model Risk and Compliance reviews can be completed within Weeks 1–4, with a named approver for each. |
| A4 | **3–6 months of representative historical data** (anonymised or pseudonymised where needed) is available for each UC for evaluation and baselining. |
| A5 | Required systems expose APIs or read-only data access. Where they do not, RPA/screen integration is limited to at most 2 integration points across the pilot. |
| A6 | Integrations are limited to the "in-scope slice" in §2 (up to 6 knowledge sources for UC1; up to 25 repositories for UC2; one rail and up to 3 reconciliations for UC5; one ERP entity for UC6). |
| A7 | All customer-impacting and regulatory decisions stay **human-in-the-loop** during the pilot. No autonomous filing (UC4), payment release (UC5/UC6) or code merge (UC2). |
| A8 | The client nominates a **business sponsor and an SME (~4 hrs/week) per UC**, plus a product owner and a pilot programme manager. |
| A9 | Pilot user groups (e.g. 150–300 knowledge users; 10–25 agents/analysts per domain UC) are available for the measured run. |
| A10 | Baselines and the value calculation method are **jointly signed off by Week 2** and stay fixed for the outcome determination. |
| A11 | Pilot-grade environments are non-production but built on production-grade architecture (see doc 05). Production deployment, penetration testing and production DR are out of scope. |
| A12 | Replacing a domain UC with another catalogue UC of equivalent complexity and accelerator fit before Week 3 costs nothing. Other changes go through change control. |
| A13 | A use case may be stopped early by joint decision ("fail fast"). Its effort can be redirected to strengthen other UCs. A stopped UC counts as "Not Demonstrated" unless replaced. |
| A14 | Outcome is rated on the measured-run data. Volume or process changes outside the partner's control (e.g. reorganisation, system freeze) are adjusted for by agreement. |
| A15 | Working language English; work delivered with a blended onshore/offshore team; on-site presence for the workshop, key ceremonies and the read-out. |

---

## 7. Client Responsibilities
- Executive sponsor and Steering Committee participation (fortnightly, 60 min).
- Timely approvals (A3), environment and data access (A2, A4), and SME availability (A8).
- Change management for pilot users, supported by the partner's adoption toolkit.
- Independent validation of value measurements by Finance or the Transformation Office.

---

## 8. From Pilot to Production — No Throw-Away

Because the pilot is built on production-grade accelerators (see [05](05-ai-implementation-accelerators.md)):
- **Reused as-is:** IaC, CI/CD, identity, AI gateway, agent runtime, connector layer, evaluation harness, observability and audit ledger, plus the use-case code, prompts and test sets.
- **Hardened in production:** scale-out and HA/DR configuration, pen test and remediation, production MRM validation, SOC/SIEM integration, runbooks and hypercare, wider integrations and user rollout.
- **Indicative production effort** for successful UCs: **30–50% of a from-scratch build.** A firm estimate is part of the M4 deliverables.
- **IP:** The client owns use-case-specific code, configuration, prompts, data and outputs. The partner keeps the accelerator IP and grants the client a **perpetual, royalty-free licence** to use it for the pilot use cases in production, whichever partner implements the next phase.

---

## 9. Governance

| Forum | Cadence | Members | Purpose |
|---|---|---|---|
| Steering Committee | Fortnightly | Exec sponsor, BU sponsors, CIO/CTO, CRO/2nd-line rep, partner engagement director | Decisions, risks, scope, outcome determination |
| Pilot Working Group | Weekly | Product owner, UC SMEs, partner delivery lead, architect | Progress, value dashboard, blockers |
| Design Authority | As needed | EA, InfoSec, Data, MRM, partner architect | Architecture, security and model-risk decisions |

**Outcome dispute resolution:** joint review of the measurement data, then escalation to the executive sponsor and partner executive, then an independent assessor agreed by both parties.

---

## 10. Why a Successful Pilot Matters for the Implementation-Partner Decision

The client may run an RFP to choose its scale-up partner. A successful pilot gives the client **lower-risk evidence** for that decision, and gives the partner a strong, legitimate advantage:

1. **Proven outcomes, not slideware.** Measured value on the client's own data and processes, validated by the client's Finance team.
2. **Working assets in the client's tenant.** Production-grade code, IaC and evaluation suites that a scale-up can extend, avoiding 30–50% of the cost of a fresh start.
3. **RFP-ready deliverables at M4.** Production architecture, TCO, a detailed estimate and a value-realisation plan, which the client can use as the basis for the RFP scope.
4. **De-risked approvals.** InfoSec, Privacy and MRM patterns already approved, which shortens time-to-production.
5. **Domain and context knowledge.** Familiarity with the acquired-app landscape (UC1/UC2 maps) and the people who run it.
6. **Commercial continuity.** Production credit (§5.2) and the perpetual accelerator licence (§8).

Where the client's procurement policy requires competitive tendering, these outcomes and artefacts serve as **objective evaluation evidence**, and in some cases as grounds for a single-source justification under the client's procurement rules.

---

## 11. Next Steps
1. Confirm the final pilot portfolio at the workshop read-out.
2. Agree the baseline method and commercial terms (this document → SOW).
3. Start InfoSec/Privacy/MRM pre-reviews and provision the environment (Week 0).
4. Hold the kick-off.

---

**Previous:** [03 – Workshop](03-ai-value-discovery-workshop.md) · **Next:** [05 – AI Implementation Accelerators](05-ai-implementation-accelerators.md)
