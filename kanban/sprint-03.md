# Sprint 3 — API de Negócio

> **Sprint Goal:** Expor endpoints REST para gerenciamento de filas, check-in e chamada de atendimento.
>
> **Período:** 27/07/2026 a 02/08/2026

---

## A Fazer

### 8. CRUD de Filas (API)

- [ ] **8.1-a [TEST]** Escrever testes para `GET /api/queues` (lista filas do usuário autenticado, sem token → 401)
- [ ] **8.1-b [IMPL]** Implementar `GET /api/queues`
- [ ] **8.2-a [TEST]** Escrever testes para `GET /api/queues/{id}` (fila existe → OK, não existe → 404)
- [ ] **8.2-b [IMPL]** Implementar `GET /api/queues/{id}`
- [ ] **8.3-a [TEST]** Escrever testes para `POST /api/queues` (criação válida → 201, dados inválidos → 400)
- [ ] **8.3-b [IMPL]** Implementar `POST /api/queues` — body: `{ name, prefix, priority }`
- [ ] **8.4-a [TEST]** Escrever testes para `PUT /api/queues/{id}/name` (atualiza nome)
- [ ] **8.4-b [IMPL]** Implementar `PUT /api/queues/{id}/name`
- [ ] **8.5-a [TEST]** Escrever testes para `PUT /api/queues/{id}/priority`
- [ ] **8.5-b [IMPL]** Implementar `PUT /api/queues/{id}/priority`
- [ ] **8.6-a [TEST]** Escrever testes para `PUT /api/queues/{id}/prefix`
- [ ] **8.6-b [IMPL]** Implementar `PUT /api/queues/{id}/prefix`
- [ ] **8.7-a [TEST]** Escrever testes para `DELETE /api/queues/{id}` (fila existe → 204, não existe → 404)
- [ ] **8.7-b [IMPL]** Implementar `DELETE /api/queues/{id}`
- [ ] **8.8** Garantir que todos os endpoints de fila exigem autenticação via JWT

### 9. Check-in (API)

- [ ] **9.1-a [TEST]** Escrever testes para `POST /api/queues/{queueId}/checkin` (check-in válido → ticket, fila não existe → 404)
- [ ] **9.1-b [IMPL]** Implementar `POST /api/queues/{queueId}/checkin` — body: `{ clientName }` opcional, retorna `{ ticket, clientId, queueName }`
- [ ] **9.2-a [TEST]** Escrever testes para formatação do ticket (prefixo + 3 dígitos, incremento de `LastTicktNumber`)
- [ ] **9.2-b [IMPL]** Garantir formatação correta do ticket (ex: `P004`)

### 10. Chamada de Atendimento (API)

- [ ] **10.1-a [TEST]** Escrever testes para `POST /api/attendance/call` (chamar próximo cliente, fila vazia → 204)
- [ ] **10.1-b [IMPL]** Implementar `POST /api/attendance/call` — body: `{ atendanteName, ticketWindow }`, retorna `{ clientName, ticket, ticketWindow }`
- [ ] **10.2-a [TEST]** Escrever testes para integração com `QueueEngine` (prioridade + timeout)
- [ ] **10.2-b [IMPL]** Garantir que `QueueEngine` é usado para selecionar a próxima fila

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

- Depende da Sprint 2 (auth endpoints + error handling) estar concluída.
- Services já existem: `ClientQueueService`, `CheckinService`, `AtendanteService`.
- Sprint segue TDD: [TEST] → [IMPL] para cada operação.
