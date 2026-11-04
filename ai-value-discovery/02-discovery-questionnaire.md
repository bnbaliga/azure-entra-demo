# Step 2 — AI Value Discovery Questionnaire

> **To:** Business Unit / Department Heads and their nominated process owners
> **From:** AI Value Discovery Team
> **Please return:** 5 working days before the AI Value Discovery Workshop
> **Time to complete:** ~45–60 minutes per BU. Estimates are fine; we will refine numbers together in the workshop.

---

## Why we are asking

Over the 3-day AI Value Discovery Workshop we will meet each business group for a focused 60-minute session. Your answers here let us **use that hour for depth rather than data collection**, and help us jointly find the 5–6 opportunities where AI agents can make a measurable difference within one quarter.

There are no right or wrong answers. Rough numbers ("about 12 FTEs", "roughly 4,000 a month") are more useful than blanks.

### How to complete
1. Every BU completes **Section A (General)**, **Section X (Knowledge & Technology Landscape)** and **Section D (Data, Risk & Readiness)**. IT/Engineering also completes **X-Tech**.
2. Complete the **Section B (Core Process)** or **Section C (Horizontal Function)** modules that apply to you. The table below shows which.
3. Mark anything sensitive "discuss live" and we will cover it in the session.

| Business group | Modules to complete |
|---|---|
| Onboarding / KYC / Customer Due Diligence | A, B1, D |
| Financial Crime / AML / Sanctions | A, B2, D |
| Fraud & Disputes / Chargebacks | A, B3, D |
| Lending / Credit / Underwriting | A, B4, B5, D |
| Customer Service / Contact Centre | A, B6, D |
| Compliance / Regulatory Affairs | A, B7, D |
| Payment Operations / Treasury Ops | A, B8, D |
| Merchant Acquiring / Partnerships | A, B9, D |
| Wealth / Product / Growth | A, B10, D |
| Finance (AP, R2R, FP&A) | A, C1–C3, D |
| HR / People | A, C4, C5, D |
| IT / Engineering | A, C6, C7, D |
| Legal / Procurement / Vendor Mgmt | A, C8, D |
| Risk / Internal Audit / Controls | A, C9, D |
| Sales / Marketing / RevOps | A, C10, D |

> **Interviewer note (remove before sending):** Each question has an internal **🔎 signal**: the type of answer that suggests a strong pilot candidate. Use these to score the BU on the rubric in Section E. Strip the 🔎 lines from the customer-facing version, or send the clean copy generated from this master.

---

## Section A — General (all BUs)

| # | Question | Response |
|---|---|---|
| A1 | What are your BU's top 3 objectives or KPIs for the next 12 months? | |
| A2 | Which **2–3 processes** take most of your team's time? For each, give approximate headcount (FTE incl. outsourced/BPO) and monthly volume. | |
| A3 | Where do work items **wait, pile up or get reworked**? What is the current backlog or ageing? | |
| A4 | Which activities involve **reading, comparing or summarising documents, emails, chats or screens** to make a decision? | |
| A5 | How many systems or screens does a typical team member use to finish one case or transaction? | |
| A6 | What share of work follows clear rules, and what share needs judgement or exception handling? (e.g. 70/30) | |
| A7 | What happens when volume rises 2× (growth, a new market, seasonality)? Do you hire, outsource, or let SLAs slip? | |
| A8 | Have you had audit findings, regulatory feedback, customer complaints or SLA penalties in the last 18 months linked to these processes? | |
| A9 | What automation already exists (RPA, OCR, rules engines, chatbots, GenAI tools)? What worked and what did not? | |
| A10 | If one process could be **50% faster or cheaper by next quarter**, which would you choose, and why? | |
| A11 | Who would be the business sponsor and process owner for a pilot in your area? Could you release a SME for ~4 hrs/week for 10–12 weeks? | |

🔎 **Signals:** ≥5 FTE on one process · high volume, repetitive but judgement-based work · ≥3 systems per case · a backlog or SLA breaches · a recent audit or regulatory finding · named sponsor and SME available · "we hire to cope with growth".

---

## Section X — Knowledge & Technology Landscape (all BUs) → *(Use cases X1, X2)*

*You told us that acquisitions have left many applications and knowledge silos, and that maintaining several tech stacks is a challenge. These questions help us size that problem in your area.*

### X-Knowledge (all BUs) → *(X1 Enterprise Knowledge & Expertise Agent)*
| # | Question | Response |
|---|---|---|
| XK1 | List the tools your team uses to **store or find knowledge** (SharePoint, Confluence, Google Drive, Notion, wikis, ticket history, Teams/Slack, shared drives, legacy intranets). Which came from acquired companies? | |
| XK2 | Roughly how many **hours a week** does a typical team member spend looking for information, or asking colleagues for it? | |
| XK3 | When someone needs an answer they cannot find, what happens? (ask on chat, raise a ticket, escalate to an SME, guess) | |
| XK4 | Are there **conflicting or duplicate versions** of procedures, policies or product rules from different acquired entities? Give an example. | |
| XK5 | Which **5–10 people** are the "go-to" experts everyone depends on? What happens when they are on leave or leave the company? | |
| XK6 | How long does it take a new joiner, or someone moving from an acquired entity, to become fully productive? | |
| XK7 | Do teams in different entities or locations solve the same problem separately? How would you find out? | |
| XK8 | Are users in **separate identity tenants or directories** (e.g. a different Entra ID/Okta per acquired company)? Is consolidation planned? | |

🔎 *≥3 hours/week searching · ≥4 knowledge repositories · conflicting procedures across entities · key-person dependency · ramp-up >3 months · separate identity tenants (makes permission-aware retrieval a differentiator).*

### X-Tech (IT / Engineering / Enterprise Architecture) → *(X2 Multi-Stack Maintenance & Modernisation Agent)*
| # | Question | Response |
|---|---|---|
| XT1 | How many **business applications** are in the portfolio? How many came from acquisitions? Is there an up-to-date application inventory / CMDB? | |
| XT2 | Which **tech stacks** are in production (languages, frameworks, databases, clouds, vendor packages)? Rough application count per stack. | |
| XT3 | Where do you have **functional overlap** (e.g. 2+ payment gateways, KYC engines, CRMs, ledgers, notification services)? | |
| XT4 | What share of engineering capacity goes to **maintenance / keep-the-lights-on** versus new features? | |
| XT5 | How many open **critical/high CVEs** and end-of-life frameworks or runtimes? Mean time to patch? | |
| XT6 | For which stacks or apps do you rely on **one or two people** (or a vendor) who understand the code? | |
| XT7 | State of documentation, architecture diagrams and test coverage for acquired applications? | |
| XT8 | Is there a **target reference stack** or consolidation roadmap? What has blocked it so far (funding, risk, knowledge, business case)? | |
| XT9 | Annual run cost (licences, hosting, support contracts, contractor spend) of the acquired applications? | |
| XT10 | Source control and CI/CD tools in use (GitHub, GitLab, Azure DevOps, Bitbucket)? Policy on AI coding assistants? | |

🔎 *≥4 stacks · >40% of capacity on maintenance · overlapping apps · CVE/EOL backlog · key-person risk · no up-to-date inventory · consolidation roadmap stalled for lack of a business case (X2 rationalisation analysis provides that case).*

---

## Section B — Core Fintech Process Modules

### B1. KYC / KYB Onboarding & Periodic Review → *(Use case C1)*
1. How many retail / business onboardings per month? What share need manual review?
2. Average analyst minutes per retail file and per business (KYB) file? How many analysts (incl. BPO)?
3. Onboarding drop-off rate, and average time-to-approve for each segment?
4. Screening false-positive rate (sanctions/PEP/adverse media), and time spent clearing each hit?
5. How large is your periodic review / remediation backlog?
6. Which vendors do you use (IDV, screening, registries)? Are they accessible by API?
7. How consistent are decisions between analysts? Do QA findings show recurring errors?

🔎 *>30% manual review · >20 min per file · KYB done largely by hand · refresh backlog · drop-off >15% · APIs available.*

### B2. AML Transaction Monitoring & SAR/STR → *(C2)*
1. Monthly alert volume? False-positive rate? Number of L1/L2 investigators?
2. Average time per alert, split between data gathering and actual analysis?
3. Number of SARs/STRs filed per month, and hours spent drafting each narrative?
4. Current alert backlog and ageing; any regulatory or lookback remediation in progress?
5. Which TM system and case manager? Can investigators' past dispositions be used as training/eval data?
6. What is the model-risk / 2nd-line governance process for tools that recommend dispositions?

🔎 *>90% false positives · >50% of time spent gathering data · backlog or regulatory pressure · historic dispositions available · supportive 2nd line.*

### B3. Fraud Investigation & Disputes / Chargebacks → *(C3)*
1. Monthly disputes and fraud claims (card, APP/push-payment, ACH/SEPA)? Number of agents?
2. Cost per dispute and average handling time? Chargeback win rate?
3. How often are scheme deadlines missed? How are evidence packs assembled?
4. Share of disputes that are suspected first-party / friendly fraud?
5. Fraud losses as bps of volume, and the trend?

🔎 *Rising volumes · manual evidence collection · win rate <40% · missed deadlines · reimbursement-rule pressure (e.g. UK PSR APP).*

### B4. Credit Underwriting & Origination → *(C4)*
1. Applications per month by product (consumer, SME, BNPL, mortgage)? Share referred to manual underwriting?
2. Which documents are reviewed manually (bank statements, financials, tax returns, payslips)? Minutes per application?
3. Time from application to decision; pull-through and abandonment?
4. How are credit memos written, and how consistent are they?
5. Experience of document fraud or tampering?
6. Is open-banking or cash-flow data used today?

🔎 *High manual referral rate · SME/thin-file growth ambitions · document-heavy · slow decisions losing customers · open-banking data available.*

### B5. Collections & Recovery → *(C5)*
1. Accounts in collections by bucket (1–30, 31–60, 61–90, 90+)? Collections headcount and agency spend?
2. Right-party-contact and promise-to-pay rates? Which channels are used?
3. How do you identify vulnerable or hardship customers?
4. Recent conduct, complaint or contact-frequency issues?

🔎 *Growing delinquency · voice-heavy collections · low digital self-cure · conduct-risk concerns.*

### B6. Customer Service & Account Servicing → *(C6)*
1. Monthly contacts by channel (chat, voice, email, social)? Number of agents (in-house/BPO)? Cost per contact?
2. Top 10 contact reasons and their share of volume?
3. Current bot containment rate, and why customers escalate?
4. AHT, FCR, CSAT/NPS? How many systems does an agent use per contact?
5. Which servicing actions have APIs (card freeze, payment trace, address change, dispute raise)?
6. Quality of the knowledge base: is it current and owned by someone?

🔎 *Top-10 intents >60% of volume · containment <30% · ≥4 agent systems · servicing APIs exist · BPO cost pressure.*

### B7. Regulatory Change & Compliance Obligations → *(C7)*
1. How many jurisdictions and licences? How many regulatory updates are tracked each month?
2. How are obligations mapped to policies and controls today (GRC tool, spreadsheets)?
3. Effort per impact assessment? Any missed or late implementations?
4. Upcoming major regulations (e.g. DORA, EU AI Act, PSD3/PSR, Consumer Duty, CFPB 1033)?

🔎 *Multi-jurisdiction · spreadsheet-based register · upcoming regulatory deadlines · small compliance team.*

### B8. Payment Exceptions & Reconciliation → *(C8)*
1. Daily payment volume by rail? STP rate? Daily count of exceptions or repairs?
2. Number of reconciliations (nostro, PSP, scheme, wallet) and daily breaks? FTE in ops?
3. Average break age? Amounts in suspense? SLA or scheme penalties?
4. Main exception root causes (data quality, sanctions holds, format, ISO 20022)?
5. Matching tools in use, and the share of matches done by hand?

🔎 *STP <95% · breaks managed in spreadsheets · suspense balances · growth in rails/partners · ISO 20022 migration pain.*

### B9. Merchant / Partner Risk → *(C9)*
1. Merchant/partner onboardings per month? Time-to-onboard? Underwriting FTE?
2. How is ongoing monitoring done (website changes, MCC mismatch, transaction laundering)?
3. Merchant-related losses, scheme fines, high chargeback-ratio programmes?

🔎 *Manual website or document reviews · onboarding is a growth bottleneck · scheme monitoring programmes.*

### B10. Personalised Insights & Advisor / RM Copilot → *(C10)*
1. Number of advisors/RMs and clients per advisor? Hours of prep per client meeting?
2. Products per customer and cross-sell conversion?
3. What customer data is available for personalisation (transactions, goals, life events)?
4. How are suitability and compliance checks evidenced today?

🔎 *High prep effort · low products per customer · rich data that is not used · advisor capacity limits growth.*

---

## Section C — Horizontal Function Modules

### C1. Finance – Accounts Payable → *(H1)*
1. Monthly invoice volume? Share PO vs non-PO? Formats (PDF, email, portal, EDI)?
2. AP headcount? Cost per invoice? Touchless rate? Exception rate and top causes?
3. Duplicate payments or vendor bank-detail fraud attempts in the last 2 years?
4. ERP (e.g. NetSuite, SAP, Oracle, D365) and any AP automation tool?

🔎 *>2,000 invoices/month · touchless <50% · high exception rate · fraud attempts · emailed PDFs.*

### C2. Finance – Record-to-Report / Close → *(H2)*
1. Days to close? Number of balance-sheet reconciliations? Hours on reconciliations each month?
2. Number of PSP, acquirer, bank and wallet settlement flows reconciled?
3. Share of manual journals, and audit adjustments?

🔎 *Close >7 days · many settlement flows · spreadsheet reconciliations · audit adjustments.*

### C3. Finance – FP&A & Reporting → *(H3)*
1. Hours each cycle on variance commentary, board and management packs?
2. How often do leaders ask ad-hoc "why" questions that need analyst time?
3. Is there a governed finance data mart or semantic layer?

🔎 *Heavy commentary effort · frequent ad-hoc requests · data mart exists.*

### C4. HR – Talent Acquisition → *(H4)*
1. Annual hires, applications per role, recruiter headcount? Time-to-hire?
2. Hours per role on screening and scheduling?
3. ATS in use (Workday, Greenhouse, Lever…)? Bias and fairness review process?

🔎 *High application volume · scheduling bottlenecks · ATS with APIs.*

### C5. HR – Employee Helpdesk & Onboarding → *(H5)*
1. Monthly HR tickets or queries? Top categories? HR shared-services FTE?
2. How many systems and tasks are involved in onboarding a new joiner? Day-1 readiness rate?
3. Policies in one place? Languages and geographies?

🔎 *Repetitive policy questions · multi-geo workforce · onboarding delays.*

### C6. IT – Service Desk & Operations → *(H6)*
1. Monthly tickets? Share L1? Top categories (access, password, device)? Service-desk FTE/outsourcing cost?
2. Incident volume, MTTR, alert noise? Runbooks documented?
3. ITSM and observability tools (ServiceNow, Jira SM, Datadog, Splunk, Azure Monitor)?

🔎 *L1 >50% · access requests dominate · alert fatigue · documented runbooks.*

### C7. Engineering – SDLC & Modernisation → *(H7)*
1. Engineer headcount? Release frequency? Test automation coverage?
2. Legacy platforms needing modernisation (languages, size)?
3. Current AI coding tool adoption and policy?

🔎 *Low test coverage · legacy core components · delivery backlog · policy allows AI tools.*

### C8. Legal & Procurement – Contracts & TPRM → *(H8)*
1. Contracts reviewed per month (NDA, MSA, partner, vendor)? In-house vs external counsel spend?
2. Critical third parties? TPRM questionnaires and assessments per year? DORA register status?
3. Do you have contract playbooks or clause libraries?

🔎 *High external counsel spend · playbooks exist · TPRM backlog · DORA/outsourcing obligations.*

### C9. Risk, Internal Audit & Controls → *(H9)*
1. Number of key controls tested each year? Hours per test? Certifications held (SOC 2, PCI DSS, ISO 27001)?
2. How is evidence collected and stored?
3. Recurring audit findings?

🔎 *Many certifications · manual screenshot evidence · repeated findings.*

### C10. Sales, Marketing & RevOps → *(H10)*
1. RFPs, DDQs and security questionnaires answered per quarter? Hours per response? Win rate?
2. Lead volume and qualification process? CRM in use?
3. How are financial-promotion and marketing compliance reviews done?

🔎 *Many enterprise/partner RFPs · answer library exists · slow compliance sign-off.*

---

## Section D — Data, Technology, Risk & Readiness (all BUs)

| # | Question | Response |
|---|---|---|
| D1 | Where does the data for the processes above live (systems, data lake/warehouse, document stores, email)? | |
| D2 | Can you share **3–6 months of anonymised historical cases** (inputs + human decisions) to evaluate a pilot? | |
| D3 | Which systems have **APIs**, and which are screen-only? | |
| D4 | Preferred or approved cloud and AI platforms (Azure OpenAI, AWS Bedrock, GCP Vertex, on-prem)? Any LLMs already approved by InfoSec? | |
| D5 | Data classification of the inputs (Public / Internal / Confidential / PII / PCI)? Any residency constraints? | |
| D6 | What approvals does an AI pilot need (InfoSec, Privacy/DPIA, Model Risk, Compliance, Outsourcing/Vendor Risk)? Typical lead time? | |
| D7 | Which decisions must **always** stay with a human? | |
| D8 | How do you baseline process cost and performance today (time-and-motion, WFM tool, BPO invoices, system logs)? | |
| D9 | What would make you call an AI pilot a success in 12 weeks? What would make you stop it? | |
| D10 | Change appetite: how do frontline teams and works councils / unions (if applicable) view automation? | |

🔎 *Historical data available · APIs in place · an approved cloud/LLM · known approval path · a measurable baseline · clear success definition.*

---

## Section E — Internal Scoring Rubric (interviewer use only)

Score each candidate use case 1–5 on each dimension after the questionnaire and the BU meeting.

| Dimension | Weight | 1 (weak) | 3 (moderate) | 5 (strong) |
|---|---|---|---|---|
| **Business value** (cost, revenue, loss, risk) | 30% | < $250K/yr potential | $250K–$1M/yr | > $1M/yr or strategic/regulatory driver |
| **Volume & repeatability** | 15% | < 500 items/month or highly variable | 500–5,000/month | > 5,000/month, standard patterns |
| **Data readiness** | 15% | No history, paper-based | Partial history, mixed formats | 3–6 months of labelled history available |
| **Technical feasibility & integration** | 15% | Screen-only legacy, no APIs | Some APIs, RPA fallback | API-first, approved cloud/LLM |
| **Risk & regulatory complexity** (inverse) | 10% | Autonomous customer-impacting decision | Human-approved recommendations | Internal, low-risk, assistive |
| **Sponsorship & change readiness** | 10% | No sponsor, resistant teams | Sponsor, but limited SME time | Committed sponsor + SME + clear KPI |
| **Accelerator fit** (cost/time to pilot) | 5% | Fully custom | Partial reuse | Pre-configured accelerator (✅) |

**Pilot shortlisting rule of thumb**

- **Weighted score ≥ 3.8** → *Pilot candidate*
- **3.0–3.7** → *Wave 2 / needs data or approval work*
- **< 3.0** → *Backlog*

The final pilot portfolio of 5–6 should **mix risk levels deliberately**: about 3–4 "high-confidence" use cases that use accelerators and about 2 "stretch / innovation" use cases. **X1 and X2 are pre-nominated as anchors** because they address the customer's stated challenges. Section X answers are used to size and baseline them, and the other 3–4 slots are filled from the BU modules. This gives the best chance of meeting the ≥50% success commitment in the [Outcome-Based Pilot proposal](04-outcome-based-pilot-proposal.md).

---

**Previous:** [01 – Use Cases](01-agentic-ai-use-cases.md) · **Next:** [03 – Workshop Plan](03-ai-value-discovery-workshop.md)
