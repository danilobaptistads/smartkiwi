# Sprint 2 — Error Handling, Register, Ajustes

> **Sprint Goal:** Implementar tratamento global de erros, finalizar endpoints de autenticação pendentes e ajustar pendências técnicas.
>
> **Período:** 20/07/2026 a 26/07/2026

---

## A Fazer

### 4. Endpoints de Autenticação (complemento)

- [ ] **4.2-a [TEST]** Escrever testes para `POST /api/auth/register` (Admin cria, não-Admin → 403, email duplicado → 400)
- [ ] **4.2-b [IMPL]** Garantir `POST /api/auth/register` com proteção `[Authorize(Roles = "Admin")]`
- [ ] **4.4** Configurar `[Authorize]` e `[Authorize(Roles = "Admin")]` nas rotas existentes

### 5. Tratamento Global de Erros

- [ ] **5.1-a [TEST]** Escrever testes para middleware de erro (ArgumentException → 400, InvalidOperation → 409, Unauthorized → 401, NotFound → 404)
- [ ] **5.1-b [IMPL]** Implementar middleware de erro global com JSON padronizado
- [ ] **5.1-c** Registrar middleware no pipeline do `Program.cs`

### 6. ClientRepository

- [ ] **6.1-a [TEST]** Escrever testes para `ClientRepository` (Add, GetRemainClients, RemoveClient)
- [ ] **6.1-c** Registrar `ClientRepository` no DI (implementação já existe)

### 7. Centralizar Validações em ClientQueueService

- [ ] **7.1-a [TEST]** Escrever testes para validações extraídas (nome e prioridade)
- [ ] **7.1-b [IMPL]** Extrair `ValidateQueueName()` e `ValidateQueuePriority()` para métodos privados reutilizáveis
- [ ] **7.1-c** Usar ambos os métodos no Create e Update
- [ ] **7.1-d** Verificar que todos os testes de `ClientQueueTests` passam

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

- Depende da Sprint 1 (Identity + JWT + Web API + AuthController) estar concluída.
- Login e Me foram implementados na Sprint 1 — Sprint 2 complementa com register e testes.
- `ClientRepository` já existe no código — cartão 6.1-b removido.
- Sprint segue TDD: [TEST] antes de [IMPL] para cada funcionalidade.
