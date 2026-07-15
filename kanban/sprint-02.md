# Sprint 2 — API de Autenticação e Ajustes

> **Sprint Goal:** Expor endpoints de autenticação, padronizar respostas de erro e ajustar pendências técnicas.
>
> **Período:** 20/07/2026 a 26/07/2026

---

## A Fazer

- [ ] **4.1** `POST /api/auth/login` — recebe `{ email, password }`, usa `SignInManager`, retorna cookie
- [ ] **4.2** `POST /api/auth/register` — apenas Admin, cria novo operador via `UserManager`
- [ ] **4.3** `POST /api/auth/logout` — limpa o cookie de autenticação
- [ ] **4.4** `GET /api/auth/me` — retorna dados do usuário autenticado
- [ ] **4.5** Rotas protegidas com `[Authorize]` e `[Authorize(Roles = "Admin")]`
- [ ] **4.6** Testes: senha inválida → 401, email não cadastrado → 401, acesso não autenticado → 401
- [ ] **5.1** Middleware captura `ArgumentException` → 400 Bad Request
- [ ] **5.2** Middleware captura `InvalidOperationException` → 409 Conflict
- [ ] **5.3** Middleware captura `UnauthorizedAccessException` → 401 Unauthorized
- [ ] **5.4** Middleware captura `NotFoundException` → 404 Not Found
- [ ] **5.5** Erros não mapeados → 500 Internal Server Error
- [ ] **6.1** `ClientRepository : IClientRepository` com `Add`, `GetRemainClients`, `RemoveClient`
- [ ] **6.2** Injeção de `SmartKiwiContext` via construtor
- [ ] **6.3** Registrado no DI
- [ ] **7.1** Validação de nome extraída para método privado reutilizável (`ValidateQueueName()`)
- [ ] **7.2** Validação de prioridade extraída para método privado reutilizável (`ValidateQueuePriority()`)
- [ ] **7.3** Ambos os métodos usados tanto no Create quanto no Update
- [ ] **7.4** Todos os testes de `ClientQueueTests` passam

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

- Depende da Sprint 1 (Identity + Web API) estar concluída.
- `AuthController` será o primeiro controller Web API do projeto.
