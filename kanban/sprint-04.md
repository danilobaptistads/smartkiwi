# Sprint 4 — Guichê + Painel

> **Sprint Goal:** Implementar gerenciamento de guichês de atendimento e painel de exibição pública.
>
> **Período:** 03/08/2026 a 09/08/2026

---

## A Fazer

- [ ] **11.1** `Workstation` estendida com `IsActive` (bool)
- [ ] **11.2** `IWorkstationRepository` e `WorkstationRepository` criados
- [ ] **11.3** `WorkstationService` com `Activate()`, `Deactivate()`, `ListActive()`, `ListAll()`
- [ ] **11.4** `GET /api/workstations` — lista todos
- [ ] **11.5** `GET /api/workstations/active` — lista apenas ativos
- [ ] **11.6** `POST /api/workstations` — cria guichê
- [ ] **11.7** `PUT /api/workstations/{id}/activate` — ativa
- [ ] **11.8** `PUT /api/workstations/{id}/deactivate` — desativa
- [ ] **11.9** `DELETE /api/workstations/{id}` — remove apenas se sem histórico de uso
- [ ] **12.1** `POST /api/attendance/call` valida que `ticketWindow` corresponde a um guichê ativo
- [ ] **12.2** Retorna 400 se o guichê estiver inativo ou não existir
- [ ] **13.1** `GET /api/panel` — retorna guichês ativos
- [ ] **13.2** `GET /api/panel` — retorna últimas N chamadas (ex: 10)
- [ ] **13.3** `GET /api/panel` — retorna status das filas (nome, quantidade aguardando, último ticket) + `PanelService` criado

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
