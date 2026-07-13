# BACKLOG - Sistema de Gerenciamento de Filas (SmartKiwi)

---

## [CONCLUÍDO]

- Estrutura base do projeto (.NET 9 + EF Core InMemory)
- Modelos de domínio: `User`, `Client`, `ClientQueue`, `Call`, `Atendante`, `Workstation`
- Camada de Repositórios com interfaces (`IUserRepository`, `IClientQueueRepository`, `IClientRepository`)
- Camada de Serviços: `UserService`, `ClientQueueService`, `CheckinService`, `AtendanteService`, `PasswordService`, `HashService`, `TokenService` (a serem substituídos pelo Identity)
- Motor de filas (`QueueEngine`) com lógica de prioridade, round-robin e timeout
- Autenticação hash Argon2id (a ser substituído pelo Identity)
- Testes unitários (xUnit + Moq) para User, ClientQueue, QueueEngine, Token, Password, Hash, Checkin, Atendante
- Interfaces para serviços: `IHashService`, `IPasswordService`, `ITokenService` (a serem removidas após migração para Identity)

---

## [A FAZER]

---

### 1. Refatoração/Limpeza de Entidades

**User Story:** Como desenvolvedor, quero que as entidades de domínio sejam POCOs simples, sem validações ou dependências de serviço, para que o modelo fique coeso e desacoplado.

#### 1.1 Extrair validações de `User.cs` para uma camada separada

**Critérios de Aceite:**

- [ ] Remover `ValidateName()` de `User.cs` — a validação de nome obrigatório deve ser movida para um **Validator** ou **DTO** dedicado
- [ ] Remover `ValidateEmail()` de `User.cs` — a validação de formato de email (regex) deve ser movida para um **Validator** ou **DTO** dedicado
- [ ] Remover `ValidatePassword()` de `User.cs` — a entidade não deve receber `IPasswordService` como parâmetro; a validação de senha deve ser feita no **service layer** antes de chamar a entidade
- [ ] Remover `ChangePassword()` de `User.cs` — a lógica de hash e troca de senha deve ser movida para `UserService` ou `PasswordService`
- [ ] Após extração, `User.cs` deve conter apenas propriedades (`Id`, `Name`, `Email`, `_password`, `_role`) e construtor, sem depender de interfaces externas
- [ ] `User.cs` não deve mais importar `SmartKiwiApp.Services` nem `System.Text.RegularExpressions`
- [ ] `User.Email` setter pode perder a validação inline (será validado externamente via DTO/FluentValidation)
- [ ] Criar classes **DTOs** (`CreateUserDto`, `UpdateUserDto`, `UpdateEmailDto`, `ChangePasswordDto`) com validações de dados via **FluentValidation** ou **Data Annotations**
- [ ] Todos os testes existentes em `UserEntityTests/` devem ser atualizados para testar as validações nos validators/DTOs, não na entidade

#### 1.2 Encapsular campos públicos de `ClientQueue.cs`

**Critérios de Aceite:**

- [ ] `currentPriority` (linha 4) deve ser convertido de **public field** para **propriedade privada** com método público de atualização (ex: `DecrementPriority()`)
- [ ] `lastCallTime` (linha 5) deve ser convertido de **public field** para **propriedade privada** com método público de atualização (ex: `UpdateLastCallTime()`)
- [ ] Atualizar `QueueEngine.cs` para usar os métodos encapsulados em vez de acessar os fields diretamente
- [ ] Verificar se há outros accessos diretos aos fields (grep por `currentPriority` e `lastCallTime`) e corrigi-los

#### 1.3 Centralizar validações duplicadas de nome e prioridade em `ClientQueueService`

**Critérios de Aceite:**

- [ ] A validação `if (string.IsNullOrWhiteSpace(name))` existe em duas partes de `ClientQueueService.cs` (Create e Update) — extrair para um método privado `ValidateQueueName()` ou um validator reutilizável
- [ ] A validação de prioridade negativa existe em Create e Update — extrair para `ValidateQueuePriority()`
- [ ] Validar também que `priority` não seja zero (fila sem prioridade pode causar starvation), a menos que seja intencional

---

### 2. Login / Autenticação de Operadores

**User Story:** Como operador do sistema, quero fazer login com email e senha para acessar funcionalidades protegidas por role (Admin/Employee).

#### 2.1 Substituir sistema de autenticação por ASP.NET Core Identity com Cookies

**Critérios de Aceite:**

- [ ] Adicionar pacote `Microsoft.AspNetCore.Identity.EntityFrameworkCore`
- [ ] Remover classe `JwtConfig.cs` e `TokenService.cs` (não serão mais necessários)
- [ ] Remover pacote `Microsoft.AspNetCore.Authentication.JwtBearer` do `.csproj`
- [ ] Refatorar `User.cs` para estender `IdentityUser<Guid>` (ou usar `IdentityUser` com string Id)
- [ ] Refatorar `SmartKiwiContext.cs` para estender `IdentityDbContext<User, IdentityRole<Guid>, Guid>` (ou string)
- [ ] Configurar Identity no pipeline: `AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<SmartKiwiContext>()`
- [ ] Configurar autenticação via cookie: `AddAuthentication().AddCookie()` com opções `HttpOnly`, `SameSite=Strict`, `ExpireTimeSpan`
- [ ] Configurar autorização com as roles `Admin` e `Employee`: `AddAuthorization()` com políticas
- [ ] Implementar endpoint `POST /api/auth/login` que recebe `{ email, password }`, usa `SignInManager` e define o cookie
- [ ] Implementar endpoint `POST /api/auth/register` (apenas Admin) usando `UserManager`
- [ ] Implementar endpoint `POST /api/auth/logout` que limpa o cookie
- [ ] Implementar endpoint `GET /api/auth/me` que retorna dados do usuário autenticado
- [ ] Proteger rotas com `[Authorize]` e `[Authorize(Roles = "Admin")]`
- [ ] Testar cenários: senha inválida, email não cadastrado, acesso não autenticado redireciona 401

#### 2.2 Migrar UserService para usar UserManager do Identity

**Critérios de Aceite:**

- [ ] `UserService.CreateNewUser()` deve usar `UserManager.CreateAsync()` em vez de `_userRepository.Add()`
- [ ] `UserService.AuthenticateUser()` deve usar `SignInManager.PasswordSignInAsync()` em vez de validação manual
- [ ] `UserService.UpdateUserName()`, `UpdateUserEmail()`, `UpdateUserPassword()`, `DeleteCurrentUser()` devem usar `UserManager`
- [ ] `PasswordService`, `HashService` e interfaces `IPasswordService`, `IHashService` podem ser removidos (Identity já gerencia hash)
- [ ] Migrar `UserRepository` para usar `UserManager` como repositório padrão do Identity
- [ ] Garantir que a role `Admin` seja criada via `RoleManager.CreateAsync()` no startup se não existir

---

### 3. Ajustes de API

**User Story:** Como desenvolvedor, quero uma API RESTful completa para gerenciar filas, check-in e chamadas, substituindo a interface de console.

#### 3.1 Migrar de Console Application para Web API

**Critérios de Aceite:**

- [ ] Mudar `SmartKiwi.csproj`: descomentar `<OutputType>Exe</OutputType>` não é suficiente — é preciso trocar o SDK para `Microsoft.NET.Sdk.Web`
- [ ] Adicionar `Program.cs` com `WebApplication.CreateBuilder(args)` configurando serviços (DbContext, Repositories, Services, Authentication, Authorization, Swagger)
- [ ] Configurar EF Core InMemory para desenvolvimento (manter compatível com testes atuais)
- [ ] Adicionar suporte a Controllers (`builder.Services.AddControllers()`)
- [ ] Adicionar middleware de desenvolvimento (Swagger/OpenAPI) opcional

#### 3.2 CRUD de Filas (API)

**Critérios de Aceite:**

- [ ] `GET    /api/queues` — Listar todas as filas do usuário autenticado
- [ ] `GET    /api/queues/{id}` — Obter fila por ID
- [ ] `POST   /api/queues` — Criar nova fila (body: `{ name, prefix, priority }`)
- [ ] `PUT    /api/queues/{id}/name` — Atualizar nome da fila
- [ ] `PUT    /api/queues/{id}/priority` — Atualizar prioridade da fila
- [ ] `PUT    /api/queues/{id}/prefix` — Atualizar prefixo da fila
- [ ] `DELETE /api/queues/{id}` — Remover fila
- [ ] Todos os endpoints devem exigir autenticação via cookie (Identity)
- [ ] Respostas seguindo padrão REST (201 para criação, 404 se não encontrado, 400 para validação)

#### 3.3 Check-in (API)

**Critérios de Aceite:**

- [ ] `POST /api/queues/{queueId}/checkin` — Realizar check-in em uma fila (body opcional: `{ clientName }`)
- [ ] Retornar o ticket gerado (ex: `{ ticket: "P004", clientId, queueName }`)
- [ ] Validar que a fila existe antes de fazer check-in
- [ ] Incrementar `LastTicktNumber` corretamente

#### 3.4 Chamada de Atendimento (API)

**Critérios de Aceite:**

- [ ] `POST /api/attendance/call` — Chamar próximo cliente da fila (body: `{ atendanteName, ticketWindow }`)
- [ ] Retornar os dados do cliente chamado e o guichê (`{ clientName, ticket, ticketWindow }`)
- [ ] Criar registro de `Call` no banco
- [ ] Remover o cliente da fila após chamada
- [ ] Retornar 204 se não houver clientes na fila

#### 3.5 Tratamento de erros global

**Critérios de Aceite:**

- [ ] Criar **Exception Handling Middleware** ou **Filter** global para capturar exceções e retornar responses padronizadas:
  - `ArgumentException` → 400 Bad Request
  - `InvalidOperationException` → 409 Conflict
  - `UnauthorizedAccessException` → 401 Unauthorized
  - `NotFoundException` → 404 Not Found
  - Erros não mapeados → 500 Internal Server Error

---

### 4. Habilitar Guichê no Painel

**User Story:** Como operador, quero ativar/desativar guichês (Workstations) e visualizar o número correspondente no painel, para gerenciar quais guichês estão disponíveis para atendimento.

#### 4.1 Entidade Guichê (Workstation)

**Critérios de Aceite:**

- [ ] Avaliar se `Workstation.cs` atual atende aos requisitos de negócio ou precisa ser estendido
- [ ] Garantir que `Workstation` tenha: `Id`, `Name`, `TicketWindow`, `IsActive` (bool para ativar/desativar)
- [ ] `Workstation.IsActive` deve permitir habilitar/desabilitar um guichê sem deletá-lo
- [ ] Garantir o DbSet `Workstations` já existe em `SmartKiwiContext.cs`
- [ ] Criar repositório `IWorkstationRepository` e `WorkstationRepository` (CRUD básico)
- [ ] Criar `WorkstationService` com métodos `Activate()`, `Deactivate()`, `ListActive()`, `ListAll()`

#### 4.2 API de Guichês

**Critérios de Aceite:**

- [ ] `GET    /api/workstations` — Listar todos os guichês
- [ ] `GET    /api/workstations/active` — Listar apenas guichês ativos
- [ ] `POST   /api/workstations` — Criar novo guichê (body: `{ name, ticketWindow }`)
- [ ] `PUT    /api/workstations/{id}/activate` — Ativar guichê
- [ ] `PUT    /api/workstations/{id}/deactivate` — Desativar guichê
- [ ] `DELETE /api/workstations/{id}` — Remover guichê (apenas se não estiver em uso)

#### 4.3 Painel de Exibição

**Critérios de Aceite:**

- [ ] Criar endpoint `GET /api/panel` que retorna o estado atual do painel:
  ```json
  {
    "activeWorkstations": [
      { "id": 1, "name": "Guichê 1", "ticketWindow": "01" },
      { "id": 2, "name": "Guichê 2", "ticketWindow": "02" }
    ],
    "lastCalls": [
      { "ticket": "P004", "ticketWindow": "01", "atendante": "João", "calledAt": "2026-07-13T10:30:00" }
    ],
    "queuesStatus": [
      { "queueName": "Prioritária", "waitingCount": 3, "lastTicket": "P005" },
      { "queueName": "Comum", "waitingCount": 7, "lastTicket": "C012" }
    ]
  }
  ```
- [ ] O painel deve refletir apenas os últimos N chamados (ex: últimos 10)
- [ ] Os guichês inativos não devem aparecer no painel
- [ ] Criar `PanelService` para montar os dados do painel a partir dos repositórios
- [ ] Se aplicável, criar um endpoint SSE (`GET /api/panel/stream`) para atualização em tempo real

#### 4.4 Relacionar Atendimento com Guichê

**Critérios de Aceite:**

- [ ] No momento da chamada (`POST /api/attendance/call`), o `ticketWindow` informado deve corresponder a um guichê ativo
- [ ] Validar se o guichê existe e está ativo antes de processar a chamada
- [ ] Retornar erro 400 se o guichê estiver inativo ou não existir

---

### 5. Melhorias Técnicas (Extra)

#### 5.1 Implementar `ClientRepository`

**Critérios de Aceite:**

- [ ] Criar `ClientRepository : IClientRepository` com implementações de `Add`, `GetRemainClients` e `RemoveClient`
- [ ] Injetar `SmartKiwiContext` via construtor
- [ ] Registrar no DI

#### 5.2 Remover dependência circular e duplicação Service/Repository

**Critérios de Aceite:**

- [ ] Em `UserRepository.cs`, validar unicidade de email apenas no repository ou apenas no service, não em ambos
- [ ] Em `ClientQueueRepository.cs`, a validação de existência (`GetQueueById` lança exceção) duplica a validação do service — decidir se repository lança exceção ou retorna null
- [ ] Unificar padrão: repositories retornam null quando não encontram; services validam e lançam exceções de negócio

#### 5.3 Testes para novas funcionalidades

**Critérios de Aceite:**

- [ ] Testes unitários para `PanelService`
- [ ] Testes unitários para `WorkstationService`
- [ ] Testes de integração para os novos endpoints de API
- [ ] Testes de validação de DTOs (se usar FluentValidation)
- [ ] Atualizar testes existentes após refatoração de entidades
