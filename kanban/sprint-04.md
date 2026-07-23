# Sprint 4 — Guichê + Painel

> **Sprint Goal:** Implementar gerenciamento de guichês de atendimento e painel de exibição pública.
>
> **Período:** 03/08/2026 a 09/08/2026

---

## A Fazer

### 11. Guichê (Workstation) com Ativação/Desativação

- [ ] **11.1-a [TEST]** Escrever testes para modelo `Workstation` (propriedade `IsActive`)
- [ ] **11.1-b [IMPL]** Estender `Workstation` com `IsActive` (bool)
- [ ] **11.2-a [TEST]** Escrever testes para `IWorkstationRepository` e `WorkstationRepository`
- [ ] **11.2-b [IMPL]** Criar `IWorkstationRepository` e `WorkstationRepository`
- [ ] **11.3-a [TEST]** Escrever testes para `WorkstationService` (Activate, Deactivate, ListActive, ListAll)
- [ ] **11.3-b [IMPL]** Criar `WorkstationService` com `Activate()`, `Deactivate()`, `ListActive()`, `ListAll()`
- [ ] **11.4-a [TEST]** Escrever testes para `GET /api/workstations` (lista todos)
- [ ] **11.4-b [IMPL]** Implementar `GET /api/workstations`
- [ ] **11.5-a [TEST]** Escrever testes para `GET /api/workstations/active` (lista apenas ativos)
- [ ] **11.5-b [IMPL]** Implementar `GET /api/workstations/active`
- [ ] **11.6-a [TEST]** Escrever testes para `POST /api/workstations` (criação válida → 201, dados inválidos → 400)
- [ ] **11.6-b [IMPL]** Implementar `POST /api/workstations`
- [ ] **11.7-a [TEST]** Escrever testes para `PUT /api/workstations/{id}/activate` (ativa guichê)
- [ ] **11.7-b [IMPL]** Implementar `PUT /api/workstations/{id}/activate`
- [ ] **11.8-a [TEST]** Escrever testes para `PUT /api/workstations/{id}/deactivate` (desativa guichê)
- [ ] **11.8-b [IMPL]** Implementar `PUT /api/workstations/{id}/deactivate`
- [ ] **11.9-a [TEST]** Escrever testes para `DELETE /api/workstations/{id}` (remove apenas se sem histórico, com histórico → 400)
- [ ] **11.9-b [IMPL]** Implementar `DELETE /api/workstations/{id}`

### 12. Vincular Chamada com Guichê Ativo

- [ ] **12.1-a [TEST]** Escrever testes para validação de guichê ativo na chamada (guichê ativo → OK, inativo → 400, inexistente → 400)
- [ ] **12.1-b [IMPL]** Validar que `POST /api/attendance/call` só aceita `ticketWindow` de guichê ativo

### 13. Painel de Exibição

- [ ] **13.1-a [TEST]** Escrever testes para `GET /api/panel` (retorna guichês ativos, últimas N chamadas, status das filas)
- [ ] **13.1-b [IMPL]** Criar `PanelService` para agregar dados
- [ ] **13.1-c [IMPL]** Implementar `GET /api/panel` — retorna guichês ativos, últimas 10 chamadas, status das filas

---

## Em Andamento

- Nenhum cartão em andamento.

---

## Concluído

- Nenhum cartão concluído nesta sprint.

---

## 🚧 Impedimentos

- Nenhum impedimento registrado.

---

## 📝 Observações

- Depende da Sprint 3 (APIs de negócio) estar concluída.
- `WorkstationsController` e `PanelController` serão criados.
- Regra: guichês inativos não aparecem no painel.
- Sprint segue TDD: [TEST] → [IMPL] para cada operação.
