# Sprint 3 — API de Negócio

> **Sprint Goal:** Expor endpoints REST para gerenciamento de filas, check-in e chamada de atendimento.
>
> **Período:** 27/07/2026 a 02/08/2026

---

## A Fazer

- [ ] **8.1** `GET /api/queues` — lista filas do usuário autenticado
- [ ] **8.2** `GET /api/queues/{id}` — obtém fila por ID
- [ ] **8.3** `POST /api/queues` — cria fila (body: `{ name, prefix, priority }`)
- [ ] **8.4** `PUT /api/queues/{id}/name` — atualiza nome
- [ ] **8.5** `PUT /api/queues/{id}/priority` — atualiza prioridade
- [ ] **8.6** `PUT /api/queues/{id}/prefix` — atualiza prefixo
- [ ] **8.7** `DELETE /api/queues/{id}` — remove fila
- [ ] **8.8** Todos os endpoints exigem autenticação via cookie
- [ ] **8.9** Respostas REST: 201 (criação), 204 (sucesso sem corpo), 400 (validação), 401 (não autenticado), 404 (não encontrado)
- [ ] **9.1** `POST /api/queues/{queueId}/checkin` — check-in com nome opcional (`{ clientName }`)
- [ ] **9.2** Retorna o ticket gerado: `{ ticket: "P004", clientId, queueName }`
- [ ] **9.3** Valida que a fila existe (404 se não existir)
- [ ] **9.4** Incrementa `LastTicktNumber` corretamente
- [ ] **9.5** Prefixo da fila + número formatado com 3 dígitos (ex: `P004`)
- [ ] **10.1** `POST /api/attendance/call` — chama próximo cliente (body: `{ atendanteName, ticketWindow }`)
- [ ] **10.2** Retorna `{ clientName, ticket, ticketWindow }` do cliente chamado
- [ ] **10.3** Cria registro de `Call` no banco
- [ ] **10.4** Remove o cliente da fila
- [ ] **10.5** Retorna 204 se não houver clientes na fila
- [ ] **10.6** Usa `QueueEngine` para selecionar a próxima fila (prioridade + timeout)

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

- Depende da Sprint 2 (endpoints de auth + error handling) estar concluída.
- `QueuesController`, `CheckinController` e `AttendanceController` serão criados.
