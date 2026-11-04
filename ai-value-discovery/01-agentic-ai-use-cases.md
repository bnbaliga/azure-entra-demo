# Step 1 — Top Agentic AI Use Cases for Fintech

> **Audience:** Fintech CXOs, BU heads, transformation office
> **Purpose:** A shortlist of the highest-value, most feasible AI agent use cases that frames the Discovery Questionnaire (Step 2), the AI Value Discovery Workshop (Step 3) and the Outcome-Based Pilot (Step 4).
> **Scope:** Fintechs such as digital banks/neobanks, payments and PSPs, digital lenders/BNPL, wealthtech, and embedded finance providers.

---

## How to read this document

Each use case is described with:

| Attribute | Meaning |
|---|---|
| **Agent pattern** | *Assist* (copilot drafts, human decides) · *Augment* (agent acts, human approves exceptions) · *Autonomous* (agent acts within guardrails, humans audit samples) |
| **Value levers** | Where the money is: cost-to-serve, loss avoidance, revenue, risk/compliance, CX |
| **Indicative impact** | Typical ranges seen in industry programs. **These are hypotheses to validate in discovery, not commitments.** |
| **Complexity** | L / M / H based on integration, data readiness and regulatory sensitivity |
| **Accelerator** | ✅ = pre-configured accelerator exists (see [05 – Accelerators](05-ai-implementation-accelerators.md)) · ◐ = partial reuse · — = custom build |

Agentic AI differs from earlier "chatbot" or "RPA + OCR" automation because the agent **plans multi-step work, calls tools and systems, reasons over unstructured evidence, and escalates with a full explanation.** That makes it a good fit for the judgement-heavy, exception-heavy work that dominates fintech operations.

---

## A. Core Fintech Processes — Top 10

### Summary heat map

| # | Use case | Primary value lever | Indicative impact | Pattern | Complexity | Accelerator |
|---|---|---|---|---|---|---|
| C1 | KYC / KYB Onboarding & Periodic Review Agent | Cost-to-serve, conversion | 40–60% less analyst effort per file; 30–50% faster time-to-yes | Augment | M | ✅ |
| C2 | AML Alert Triage & SAR/STR Narrative Agent | Cost, compliance quality | 30–50% less L1 investigation time; 50–70% faster SAR drafting | Augment | H | ✅ |
| C3 | Fraud Case Investigation & Disputes/Chargeback Agent | Loss avoidance, cost | 30–45% lower handling time; 10–20% more chargebacks won | Augment | M | ✅ |
| C4 | Credit Underwriting & Loan Origination Document Agent | Revenue, cost, speed | 50–70% less manual doc review; 2–5× faster decisions on thin files | Assist → Augment | H | ◐ |
| C5 | Collections & Recovery Engagement Agent | Revenue (recovery), cost | 10–25% higher right-party contact/promise-to-pay; 30–40% less agent effort | Augment | M | ◐ |
| C6 | Customer Service & Account Servicing Agent (Tier-0/1) | Cost-to-serve, CX | 30–50% containment; 20–35% lower AHT on assisted contacts | Autonomous (bounded) | M | ✅ |
| C7 | Regulatory Change & Compliance Obligations Agent | Risk, cost | 50–70% less effort mapping regulations to policies and controls | Assist | M | ◐ |
| C8 | Payment Exceptions, Repairs & Reconciliation Agent | Cost, SLA, working capital | 40–60% fewer manual repairs; 30–50% faster break resolution | Augment | M | ✅ |
| C9 | Merchant / Partner Risk Onboarding & Monitoring Agent | Loss avoidance, speed | 40–60% faster merchant onboarding; earlier detection of high-risk merchants | Augment | M | ◐ |
| C10 | Personalised Financial Insights & Advisor Copilot | Revenue, engagement | 5–15% uplift in cross-sell/engagement; 30–40% advisor prep-time saved | Assist | M | ◐ |

### C1. KYC / KYB Onboarding & Periodic Review Agent
- **Pain today:** Manual document checks, high abandonment, KYB on complex ownership structures, periodic refresh backlogs, inconsistent decisions between analysts.
- **What the agent does:** Extracts and validates IDs, proof of address and company registry documents; resolves UBO structures from registry APIs; runs sanctions/PEP/adverse-media screening; **reasons over false-positive hits** and writes a risk-rated case summary with evidence; requests missing documents from the customer; routes only true exceptions to an analyst.
- **Systems/data:** Onboarding platform, IDV vendor, registries (e.g. Companies House, OpenCorporates), screening vendor, CRM, case management.
- **KPIs:** Minutes per file, straight-through-processing rate, time-to-yes, onboarding drop-off, false-positive clearance rate, QA error rate.

### C2. AML Transaction-Monitoring Alert Triage & SAR/STR Narrative Agent
- **Pain today:** 90–95%+ of rule-based alerts are false positives; L1 analysts spend most of their time gathering data across systems; SAR narratives are slow and of uneven quality.
- **What the agent does:** For every alert, collects KYC profile, transaction history, counterparties and prior alerts; applies typology reasoning; recommends close/escalate with a cited rationale; drafts the SAR/STR narrative in regulator format for the investigator to approve.
- **Guardrail:** The agent never files; a human always disposes. Full audit trail for regulator/model-risk review.
- **KPIs:** Alerts handled per analyst per day, average handling time, backlog age, QA pass rate, SAR quality score, regulatory findings.

### C3. Fraud Case Investigation & Disputes / Chargeback Agent
- **Pain today:** Dispute volumes rising (card-not-present, APP scams); scheme deadlines missed; evidence packs assembled by hand; first-party fraud is hard to spot.
- **What the agent does:** Classifies disputes by reason code; gathers transaction, device, 3DS, delivery and communication evidence; decides between refund, representment and rejection within policy; builds scheme-compliant representment packs; flags first-party fraud patterns.
- **KPIs:** Cost per dispute, win rate, scheme SLA adherence, write-offs, customer complaints.

### C4. Credit Underwriting & Loan Origination Document Agent
- **Pain today:** SME and thin-file lending depends on bank statements, financials, tax returns and invoices, which are reviewed by hand; decisions are slow; credit memos vary between underwriters.
- **What the agent does:** Ingests documents and open-banking data; performs cash-flow analysis and categorisation; spots inconsistencies and possible document tampering; computes policy ratios; drafts the credit memo and conditions. **The decision stays with the scorecard or the human under the credit policy.**
- **Regulatory note:** Fair-lending and adverse-action explainability (e.g. ECOA/Reg B, EU CCD2, EU AI Act "high-risk" classification for creditworthiness) applies, so the design is *Assist* first.
- **KPIs:** Time-to-decision, underwriter capacity, pull-through rate, early-delinquency rate (must stay flat or improve).

### C5. Collections & Recovery Engagement Agent
- **Pain today:** One-size-fits-all dunning; low right-party contact; expensive call centres; conduct risk (vulnerable customers, contact-frequency rules such as FDCPA/Reg F and Consumer Duty).
- **What the agent does:** Segments delinquent accounts; chooses the next best action, channel and timing; holds compliant digital conversations to set up payment plans and promises-to-pay; detects vulnerability or hardship and hands off to a human with context.
- **KPIs:** Roll rates, cure rate, promise-to-pay kept rate, cost-to-collect, complaints.

### C6. Customer Service & Account Servicing Agent (Tier-0/1)
- **Pain today:** High contact volumes for "where is my payment", card freeze/replace, limit changes, statement queries and failed transfers; long wait times; agents toggle between 6–10 screens.
- **What the agent does:** Authenticates the customer, understands intent, **executes servicing actions through APIs** (freeze card, trace payment, raise dispute) and answers from policy knowledge with citations. For human agents it provides a live copilot: summaries, next-best action and after-call work.
- **KPIs:** Containment rate, AHT, FCR, CSAT/NPS, cost per contact, escalations.

### C7. Regulatory Change & Compliance Obligations Agent
- **Pain today:** Multi-jurisdiction licences (EMI, PI, lending, MSB, broker) bring a constant stream of regulatory updates; mapping obligations to policies, controls and owners is manual and error-prone.
- **What the agent does:** Monitors regulator feeds; summarises changes; maps them to an obligations register, policies and controls; drafts gap assessments and impact memos; tracks actions to closure.
- **KPIs:** Time from publication to impact assessment, coverage of the obligations register, audit findings.

### C8. Payment Exceptions, Repairs & Reconciliation Agent
- **Pain today:** Failed or held payments (format, sanctions, missing data, ISO 20022 migration issues), nostro/settlement breaks, scheme file mismatches; ops teams work through spreadsheets.
- **What the agent does:** Classifies the exception and finds its root cause; repairs it within rules (enrich BIC/IBAN, correct remittance data); proposes break matches using fuzzy and contextual reasoning; drafts investigation messages to correspondents and partners; escalates anything ageing.
- **KPIs:** STP rate, manual repair rate, break ageing, SLA breaches, cost per exception, funds tied up.

### C9. Merchant / Partner Risk Onboarding & Monitoring Agent
- **Pain today:** Slow merchant underwriting; risky MCCs; transaction laundering; poor visibility of embedded-finance partners' compliance.
- **What the agent does:** Reviews merchant websites, business models and documents; checks prohibited-goods lists; compares stated activity with observed transactions; monitors adverse signals continuously; recommends reserves or limits for analyst approval.
- **KPIs:** Onboarding cycle time, merchant losses, chargeback ratio, scheme fines.

### C10. Personalised Financial Insights & Advisor / RM Copilot
- **Pain today:** Generic engagement; RMs and advisors spend hours preparing for meetings; low product penetration.
- **What the agent does:** Builds a customer 360 summary; finds life events and financial-health insights; prepares meeting briefs and suitability-checked recommendations; drafts compliant follow-ups. For customers, it offers in-app "money coach" insights.
- **Regulatory note:** Suitability rules (MiFID II, Reg BI) and fair-value / Consumer Duty mean recommendations need a human in the loop or approved guardrails.
- **KPIs:** Products per customer, conversion, advisor capacity, engagement (MAU), complaints.

---

## B. Horizontal Business Functions — Top 10

| # | Function | Use case | Primary value lever | Indicative impact | Pattern | Complexity | Accelerator |
|---|---|---|---|---|---|---|---|
| H1 | Finance – P2P | Accounts Payable Invoice-to-Pay Agent | Cost, controls | 60–80% touchless invoices; 40–60% lower cost per invoice | Augment | L–M | ✅ |
| H2 | Finance – R2R | Financial Close & Reconciliation Agent | Speed, cost | Close shortened by 2–4 days; 40–60% less reconciliation effort | Augment | M | ✅ |
| H3 | Finance – FP&A | Variance Analysis & Management Reporting Agent | Productivity, decision speed | 50–70% less time on commentary and flux analysis | Assist | L–M | ◐ |
| H4 | HR – Talent | Talent Acquisition Screening & Scheduling Agent | Speed, cost | 30–50% faster time-to-shortlist; 70% less scheduling effort | Augment | L–M | ◐ |
| H5 | HR – Shared Services | Employee Helpdesk, Policy & Onboarding Agent | Cost-to-serve, EX | 40–60% ticket deflection | Autonomous (bounded) | L | ✅ |
| H6 | IT | IT Service Desk & AIOps Incident Triage Agent | Cost, MTTR | 30–50% L1 deflection; 20–40% lower MTTR | Augment | M | ✅ |
| H7 | Engineering | SDLC Productivity & Legacy Modernisation Agent | Throughput, tech debt | 20–40% faster delivery on targeted work; 30–50% faster test creation | Assist | M | ◐ |
| H8 | Legal & Procurement | Contract Review & Third-Party Risk (TPRM) Agent | Cost, risk | 50–70% faster first-pass review; TPRM questionnaire effort cut by 50% | Assist | M | ◐ |
| H9 | Risk, Audit & Controls | Controls Testing & Audit Evidence Agent | Cost, assurance | 40–60% less effort on controls testing and evidence collection | Augment | M | ◐ |
| H10 | Sales & Marketing | RevOps, RFP/Security-Questionnaire Response & Campaign Agent | Revenue, cycle time | 50–70% faster RFP/DDQ responses; better lead conversion | Assist | L–M | ✅ |

**H1. AP Invoice-to-Pay:** Ingests invoices from email, portal or EDI; extracts and validates data; performs 2/3-way match against PO and GRN; codes GL and cost centre; resolves exceptions with suppliers by email; detects duplicates and fraudulent bank-detail changes.

**H2. Financial Close & Reconciliation:** Prepares balance-sheet reconciliations; matches sub-ledger, bank and PSP settlements; proposes journals with support; explains unreconciled items; tracks the close checklist. Particularly valuable where many PSP/acquirer settlement flows exist.

**H3. FP&A Variance & Reporting:** Pulls actuals against budget and forecast; drafts flux and variance commentary from drivers; answers "why" questions in natural language over the finance data mart; prepares board-pack drafts.

**H4. Talent Acquisition:** Writes job descriptions; screens CVs against skills (with bias controls and audit); runs scheduling; drafts interview kits; answers candidate FAQs.

**H5. Employee Helpdesk & Onboarding:** Answers policy, payroll, leave and benefits questions with citations; carries out transactions in the HRIS (leave requests, letters); orchestrates new-joiner onboarding tasks across HR, IT and Facilities.

**H6. IT Service Desk & AIOps:** Resolves password, access and common L1 issues through automated runbooks; correlates alerts; drafts incident timelines and RCA; suggests remediations to SREs. This matters for operational resilience regimes (DORA, FFIEC).

**H7. SDLC Productivity & Modernisation:** Generates code and tests; reviews PRs; produces documentation; analyses legacy code (COBOL/Java monolith to services); generates regression suites for payment and ledger logic.

**H8. Contract Review & TPRM:** Reviews contracts against the playbook and flags deviations; extracts obligations; answers vendor due-diligence questionnaires; continuously monitors critical third parties (DORA ICT third-party register).

**H9. Controls Testing & Audit Evidence:** Executes control test scripts against system evidence; collects and labels audit evidence (SOC 2, PCI DSS, ISO 27001); drafts findings; follows up on remediation.

**H10. RevOps / RFP / Campaigns:** Drafts RFP, DDQ and security-questionnaire answers from an approved answer library; enriches and qualifies leads; generates compliant marketing copy with financial-promotion rule checks.

---

## B+. Customer-Stated Challenges — Anchor Use Cases

The customer has raised two pain points in early conversations:

1. **Knowledge silos.** Acquisitions have left *too many apps*, and knowledge is hard to share between team members and across teams.
2. **Different tech stacks.** Maintaining several stacks inherited from acquired companies is a growing burden.

Neither belongs to a single BU. Both affect **every** core process above: an AML analyst, a disputes agent or a payment-ops engineer loses time because answers and code sit in systems inherited from different acquisitions. We therefore propose two cross-cutting **anchor use cases**. They are low-risk (internal, assistive), reach many users, map well to accelerators, and **produce the shared knowledge and integration layer that the domain agents (C1–C10) reuse.**

| # | Use case | Addresses | Indicative impact | Pattern | Complexity | Accelerator |
|---|---|---|---|---|---|---|
| X1 | **Enterprise Knowledge & Expertise Agent** ("one front door" across acquired apps) | Knowledge silos | 30–50% less time searching for information; 20–40% fewer repeat questions and escalations; 25–40% faster new-joiner ramp-up | Assist → Augment | M | ✅ |
| X2 | **Multi-Stack Engineering Maintenance & Modernisation Agent** | Tech-stack sprawl, maintenance cost | 25–40% less maintenance effort (L2/L3 fixes, upgrades, patching); 30–50% faster onboarding of engineers to unfamiliar codebases; evidence-based app rationalisation roadmap | Assist → Augment | M–H | ◐ |

### X1. Enterprise Knowledge & Expertise Agent
- **Pain today:** Each acquired business brought its own wiki, SharePoint, Confluence, ticketing tool, CRM and shared drives. Policies and procedures are duplicated or contradict each other. Staff ask colleagues on chat; tribal knowledge leaves with people; teams solve the same problem twice.
- **What the agent does:**
  - **Federated retrieval** across the acquired apps through connectors (SharePoint/OneDrive, Confluence, Jira/ServiceNow, Google Drive, Slack/Teams, CRM, legacy intranets), without a big-bang content migration.
  - **Permission-aware answers.** It respects each source's ACLs and Entra ID/Okta groups, so users only see what they are entitled to see.
  - **Cited answers**, with links back to the source system, freshness date and owner.
  - **Conflict & duplicate detection.** It flags where two acquired entities hold contradicting procedures and routes the conflict to the content owner, which supports policy harmonisation after the acquisition.
  - **Expertise finder** ("who knows about X") built from authorship, ticket resolution and code ownership signals.
  - **Knowledge capture.** It turns resolved tickets, chat threads and incident post-mortems into draft knowledge articles for SME approval.
- **Measures:** Time-to-answer, search success rate, repeat-question volume, ticket deflection, content-conflict count resolved, onboarding time-to-productivity, user adoption (WAU).
- **Why it is a strong pilot:** It is visible to everyone, internal-only (lower regulatory risk), quick to stand up with accelerators, and its retrieval and connector layer is **reused by C2, C3, C6, C7, H5 and H6.**

### X2. Multi-Stack Engineering Maintenance & Modernisation Agent
- **Pain today:** Several stacks (for example .NET, Java/Spring, Node, PHP, Python, possibly mainframe or vendor packages) and duplicate capabilities (multiple payment gateways, KYC flows, CRMs) came in with acquisitions. Few engineers know each stack, documentation is thin, patching and upgrades lag, and keeping the lights on uses up the change budget.
- **What the agent does:**
  - **Code & architecture knowledge graph** across all repositories and stacks: services, APIs, data stores, dependencies, owners. Engineers can ask "where is fee calculation implemented in each platform?"
  - **Auto-documentation** of undocumented legacy services (README, sequence diagrams, API specs, runbooks).
  - **Maintenance automation:** dependency and CVE upgrades, framework version bumps, and generated regression tests, delivered as PRs for human review. Covers each stack, not just one.
  - **Incident-to-fix assistance:** links production incidents (H6) to the likely code location and proposes a fix with tests.
  - **App portfolio rationalisation analysis:** inventories overlapping applications from the acquisitions, scores them (functional overlap, tech health, cost, risk, usage) and recommends a consolidate / retire / modernise path. The resulting **target-state roadmap** becomes a natural follow-on implementation programme.
  - **Modernisation assist:** converts code between stacks or toward a target reference stack, with equivalence tests.
- **Measures:** Maintenance hours per application, mean time to patch critical CVEs, test coverage, MTTR, engineer onboarding time, number of applications rationalised, run-cost per application.
- **Why it is a strong pilot:** It directly addresses the customer's stated maintenance pain, the value is measurable (engineering hours, CVE backlog), and it builds the **case for a larger consolidation and modernisation programme.**

> **Positioning:** X1 and X2 tell the customer we have listened. They also act as *platform enablers*: the knowledge layer and the code/integration graph built in the pilot lower the cost of every later agent. We recommend both are in the pilot portfolio, alongside 3–4 domain use cases chosen in the workshop.

---

## C. Common Non-Functional Requirements (NFRs) — applies to all use cases

Fintech is regulated, handles PII and payment data, and is subject to model-risk scrutiny. **The NFRs are what get a pilot through to production.** They are built into the accelerators from day one rather than added after the pilot.

### C.1 Security & Identity
| ID | Requirement |
|---|---|
| SEC-01 | Enterprise SSO and RBAC/ABAC for users **and agents** (e.g. Microsoft Entra ID, Okta). Agents run under their own workload identities with least privilege. |
| SEC-02 | No standing secrets in code. Use a secrets vault (Key Vault / Secrets Manager) and managed identities. |
| SEC-03 | Private networking (private endpoints/VNet), no public LLM endpoints for confidential data, egress control. |
| SEC-04 | Encryption in transit (TLS 1.2+) and at rest (customer-managed keys where required). |
| SEC-05 | Defences against prompt injection, jailbreak and tool misuse: input/output filtering, tool allow-lists, sandboxed execution, and limits on what agents may do (amount thresholds, read-only by default). |
| SEC-06 | Secure SDLC: SAST/DAST, dependency/SBOM scanning, threat modelling (STRIDE + OWASP Top 10 for LLM/Agentic apps), and pen testing before production. |

### C.2 Data Privacy & Residency
| ID | Requirement |
|---|---|
| PRV-01 | PII/PCI minimisation and redaction before LLM calls; tokenisation of PAN (PCI DSS v4.0 scope reduction). |
| PRV-02 | Data residency and sovereignty controls by jurisdiction (GDPR, UK GDPR, India DPDP, GLBA, local banking secrecy). |
| PRV-03 | Contractual guarantee of zero data retention and no training on customer data by model providers. |
| PRV-04 | Consent and purpose-limitation tracking; support for data-subject rights (access, deletion). |
| PRV-05 | **Permission-aware retrieval:** knowledge agents enforce source-system ACLs at query time (document- and row-level trimming), including across acquired entities' separate identity tenants. No "super-index" that bypasses entitlements. |

### C.3 Responsible AI, Model Risk & Compliance
| ID | Requirement |
|---|---|
| RAI-01 | Model inventory and model-risk management aligned to SR 11-7 / OCC 2011-12, PRA SS1/23, and the EU AI Act risk classification. |
| RAI-02 | **Human-in-the-loop** for all customer-impacting or regulatory decisions (credit, SAR filing, account closure), with configurable autonomy levels per use case. |
| RAI-03 | Explainability: every agent recommendation carries cited evidence, reasoning summary and confidence, so it can be read by auditors and regulators. |
| RAI-04 | Fairness and bias testing for decisions affecting customers or employees (fair lending, hiring). |
| RAI-05 | Immutable audit trail of prompts, retrieved context, tool calls, outputs and human decisions, retained per record-keeping rules (typically 5–7 years). |
| RAI-06 | Content safety and conduct rules: no unauthorised financial advice; financial-promotion checks; vulnerable-customer detection. |

### C.4 Quality, Evaluation & Observability (LLMOps / AgentOps)
| ID | Requirement |
|---|---|
| QLT-01 | Offline evaluation harness with golden datasets per use case (accuracy, groundedness, hallucination rate, task-completion rate), run in CI on every change to prompts or models. |
| QLT-02 | Online monitoring: drift, quality sampling, human override rate, and user feedback loops. |
| QLT-03 | End-to-end tracing of agent steps (OpenTelemetry) with dashboards for latency, cost, errors and tool failures. |
| QLT-04 | Versioning of prompts, agents, tools and models, with canary or shadow-mode releases and rollback. |

### C.5 Performance, Scalability & Resilience
| ID | Requirement |
|---|---|
| PRF-01 | Latency targets by channel: conversational p95 < 3–5 s to first token; back-office batch agents measured on throughput and SLA. |
| PRF-02 | Horizontal scalability to peak volumes (month-end, festive or Black-Friday payment spikes); queue-based back-pressure. |
| PRF-03 | Availability 99.9% (back-office) and 99.95% (customer-facing). Multi-region / model-provider failover, with graceful degradation to human queues. |
| PRF-04 | Operational resilience and DR (DORA, FFIEC): RTO/RPO defined; ICT third-party concentration risk assessed for LLM providers. |

### C.6 Cost, Portability & Integration
| ID | Requirement |
|---|---|
| CST-01 | FinOps: per-use-case token and infrastructure cost tracking, budgets and alerts, model routing (small models for simple steps), caching. |
| CST-02 | Model-agnostic AI gateway: swap LLM providers or versions without code changes, to avoid lock-in. |
| INT-01 | API-first integration (REST/events), with standard connector patterns for core banking, card processors, CRM, ERP, HRIS and ITSM; MCP/tool registry for agent tools. |
| INT-02 | Idempotent, transactional tool calls with compensation and rollback for any action that changes state. |
| INT-03 | **Stack-agnostic integration:** agents work with heterogeneous systems from the acquisitions through an adapter/connector layer (REST, events, DB read-replicas, RPA as a last resort), so a later app consolidation means swapping adapters, not rewriting agents. |
| MNT-01 | **Maintainability:** one reference stack and coding standard for all agent services (rather than adding another stack to the estate), IaC, automated tests, and documentation generated as part of CI. |

### C.7 Usability & Change
| ID | Requirement |
|---|---|
| USE-01 | Reviewer workbench ("agent inbox") embedded where users already work, so they can approve, edit or reject with one click and their feedback is captured. |
| USE-02 | Accessibility (WCAG 2.1 AA) and multilingual support where needed. |
| USE-03 | Training, SOP updates and role redesign planned as part of every use case, not as an afterthought. |

---

**Next:** [02 – Discovery Questionnaire](02-discovery-questionnaire.md)
