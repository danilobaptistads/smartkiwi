# 📋 SPRINTS & QUADRO KANBAN

> Organização do fluxo de trabalho em 4 sprints semanais focadas em valor de negócio e qualidade técnica.
> Regra de Mercado: Uma tarefa só muda de status para Concluída [x] quando todos os seus critérios de aceite internos forem atendidos.

---

## 🏃 Sprint 1 — Limpeza de Entidades e Código (Semana 1)

**Objetivo da Sprint:** Remover validações poluídas das entidades, garantir encapsulamento correto e unificar o padrão Service/Repository.

### 📌 Quadro Kanban da Sprint 1

- [ ] **Tarefa 1.1: Extrair validações de `User.cs` para DTOs/Validators** *(Est: 3h)*
  - [ ] `User.cs` não importa mais `SmartKiwiApp.Services` nem `System.Text.RegularExpressions`
  - [ ] `User.cs` é um POCO simples, sem métodos de validação ou dependências de serviço
  - [ ] DTOs criados: `CreateUserDto`, `UpdateUserDto`, `UpdateEmailDto`, `ChangePasswordDto`
  - [ ] Todos os testes de `UserEntityTests/` passam validando os novos DTOs

- [ ] **Tarefa 1.2: Centralizar validações duplicadas em `ClientQueueService`** *(Est: 1h)*
  - [ ] Validações de nome e prioridade em `ClientQueueService` estão centralizadas
  - [ ] Todos os testes de `ClientQueueTests/` passam sem quebras de lógica

- [ ] **Tarefa 5.1: Implementar `ClientRepository` (concreto)** *(Est: 1h)*
  - [ ] `ClientRepository` implementa a interface `IClientRepository` de forma correta
  - [ ] `ClientRepository` está devidamente registrado no container de Injeção de Dependência (DI)

- [ ] **Tarefa 5.2: Unificar padrão arquitetural de retornos e validações** *(Est: 1h)*
  - [ ] Repositories retornam `null` quando um registro não é encontrado no banco
  - [ ] Services assumem a responsabilidade de validar o retorno e lançar as exceções adequadas

- [ ] **Tarefa 5.3: Atualizar e validar a suite de testes automatizados** *(Est: 2h)*
  - [ ] Todos os testes existentes rodam com sucesso após as refatorações de código das entidades

---

## 🏃 Sprint 2 — Autenticação Identity + Web API (Semana 2)

**Objetivo da Sprint:** Substituir o JWT/Argon2 manual pelo ecossistema nativo do ASP.NET Core Identity com suporte a cookies e migrar o projeto para o modelo Web API.

### 📌 Quadro Kanban da Sprint 2

- [ ] **Tarefa 2.1: Substituir mecanismo de autenticação customizado por ASP.NET Core Identity** *(Est: 4h)*
  - [ ] Pacote `Microsoft.AspNetCore.Identity.EntityFrameworkCore` devidamente adicionado
  - [ ] Pacote antigo `Microsoft.AspNetCore.Authentication.JwtBearer` e suas referências removidos
  - [ ] Arquivos `JwtConfig.cs`, `TokenService.cs`, `PasswordService.cs` e `HashService.cs` excluídos
  - [ ] A classe `User.cs` passa a estender diretamente `IdentityUser<Guid>`
  - [ ] O contexto `SmartKiwiContext` estende `IdentityDbContext, Guid>`
  - [ ] Métodos `AddIdentity` e `AddCookie` configurados com sucesso no pipeline da aplicação
  - [ ] Roles padrão `Admin` e `Employee` criadas programaticamente durante a inicialização (startup)

- [ ] **Tarefa 2.2: Ajustar a camada de serviço de usuários para o novo ecossistema** *(Est: 2h)*
  - [ ] Classe `UserService` migrada para utilizar os gerenciadores nativos `UserManager` e `SignInManager`

- [ ] **Tarefa 3.1: Migrar arquitetura do projeto de Console Application para Web API** *(Est: 2h)*
  - [ ] O SDK do arquivo de projeto `.csproj` foi alterado para `Microsoft.NET.Sdk.Web`
  - [ ] Arquivo `Program.cs` reconfigurado usando `WebApplication.CreateBuilder`, Controllers e Swagger
  - [ ] Exposição de endpoints funcionais: `POST /api/auth/login`, `POST /api/auth/register`, `POST /api/auth/logout`, `GET /api/auth/me`
  - [ ] Suite de testes de autenticação passa sem erros integrada ao Identity

- [ ] **Tarefa 3.5: Implementar Middleware global para tratamento de exceções** *(Est: 1h)*
  - [ ] Middleware intercepta erros e retorna respostas JSON padronizadas para o cliente (400, 401, 404, 409, 500)

---

## 🏃 Sprint 3 — API de Negócio (Semana 3)

**Objetivo da Sprint:** Desenvolver e expor os endpoints RESTful essenciais para o gerenciamento de filas, emissão de senhas (check-in) e chamadas.

### 📌 Quadro Kanban da Sprint 3

- [ ] **Tarefa 3.2: Desenvolver os endpoints REST para o CRUD completo de Filas** *(Est: 3h)*
  - [ ] `GET /api/queues` — Lista com sucesso todas as filas pertencentes ao usuário autenticado
  - [ ] `GET /api/queues/{id}` — Obtém as informações detalhadas de uma fila específica por seu ID
  - [ ] `POST /api/queues` — Cria uma nova fila aceitando o corpo JSON `{ name, prefix, priority }`
  - [ ] `PUT /api/queues/{id}/name` — Realiza a atualização parcial do nome da fila
  - [ ] `PUT /api/queues/{id}/priority` — Realiza a atualização parcial do nível de prioridade da fila
  - [ ] `PUT /api/queues/{id}/prefix` — Realiza a atualização parcial do prefixo identificador da fila
  - [ ] `DELETE /api/queues/{id}` — Remove uma fila do sistema, validando dependências antes da exclusão

- [ ] **Tarefa 3.3: Implementar lógica e endpoint de emissão de senhas (Check-in)** *(Est: 2h)*
  - [ ] `POST /api/queues/{queueId}/checkin` — Realiza o check-in do cliente na fila e retorna o ticket gerado
  - [ ] Lógica de incremento automático do campo `LastTicketNumber` opera corretamente de forma concorrente

- [ ] **Tarefa 3.4: Desenvolver rotina e endpoint para Chamada de Atendimento** *(Est: 2h)*
  - [ ] `POST /api/attendance/call` — Consome o próximo cliente da fila por ordem de prioridade e retorna `{ clientName, ticket, ticketWindow }`
  - [ ] Um novo registro histórico de atendimento (`Call`) é inserido no banco de dados a cada execução
  - [ ] Todos os endpoints criados exigem autenticação via cookie ativa e retornam padrões REST (201, 204, 400, 401, 404)

---

## 🏃 Sprint 4 — Guichê + Painel (Semana 4)

**Objetivo da Sprint:** Implementar o gerenciamento completo de guichês físicos (workstations), validações de concorrência de chamadas e o painel agregador em tempo real.

### 📌 Quadro Kanban da Sprint 4

- [ ] **Tarefa 4.1: Expandir o modelo de dados para suportar Guichês físicos** *(Est: 2h)*
  - [ ] Entidade `Workstation` estendida com propriedade de controle de estado ativo `IsActive` (bool)
  - [ ] Classes de persistência `IWorkstationRepository` e `WorkstationRepository` criadas
  - [ ] Camada `WorkstationService` expõe métodos de negócio `Activate()`, `Deactivate()`, `ListActive()`, `ListAll()`

- [ ] **Tarefa 4.2: Desenvolver API REST para gerenciamento dos Guichês** *(Est: 2h)*
  - [ ] `GET /api/workstations` — Lista todos os guichês cadastrados na base de dados
  - [ ] `GET /api/workstations/active` — Filtra e expõe apenas os guichês que estão ativos no momento
  - [ ] `POST /api/workstations` — Cria um novo guichê de atendimento
  - [ ] `PUT /api/workstations/{id}/activate` — Altera o estado do guichê para ativo
  - [ ] `PUT /api/workstations/{id}/deactivate` — Altera o estado do guichê para inativo
  - [ ] `DELETE /api/workstations/{id}` — Bloqueia a remoção do guichê caso ele já possua histórico de uso ativo

- [ ] **Tarefa 4.4: Vincular rotinas de atendimento às regras de guichês ativos** *(Est: 1h)*
  - [ ] A rota `POST /api/attendance/call` valida obrigatoriamente se o parâmetro `ticketWindow` aponta para um guichê ativo no sistema

- [ ] **Tarefa 4.3: Implementar Endpoint do Painel de Exibição Pública** *(Est: 3h)*
  - [ ] `GET /api/panel` — Retorna dados consolidados: lista de guichês ativos, histórico das últimas N chamadas realizadas e status atual das filas
  - [ ] Camada de serviço `PanelService` isola a lógica de agregação de dados do painel principal
  - [ ] Guichês marcados como inativos são omitidos automaticamente das respostas do painel público
  - [ ] Rota administrativa `POST /api/auth/register` protegida apenas para usuários com perfil de Admin

---

## 📊 Resumo de Dependência das Sprints

| Ciclo | Foco Principal do Trabalho | Pré-requisito Obrigatório |
|:---:|---|:---:|
| **Sprint 1** | Engenharia Reversa, Limpeza de Entidades e Camada Repository | Nenhum |
| **Sprint 2** | Migração Web API e ASP.NET Core Identity com Cookies | Sprint 1 concluída |
| **Sprint 3** | Criação das Rotas REST de Filas, Check-in e Chamada de Clientes | Sprint 2 concluída |
| **Sprint 4** | Cadastro e Controle de Guichês Ativos + Painel Consolidado | Sprint 3 concluída |

> **Nota de Governança:** Se você finalizar todas as tarefas de uma Sprint antes do prazo final da semana, está autorizado a puxar tarefas da Sprint subsequente como adiantamento de escopo.
