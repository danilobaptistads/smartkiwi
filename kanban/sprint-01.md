# Sprint 1 — Substituir Argon2 por Identity com JWT + Web API

## A Fazer

### Pipeline (desbloqueia tudo)


### **3.4** Controllers configurados (`AddControllers()`)


### **3.5** Swagger/OpenAPI configurado


### Auth Pipeline


### **1.6-c** Definir chave de assinatura JWT (`appsettings.json` ou User Secrets)


### **1.9** Configurar Identity no pipeline (regras de senha: 8 chars, maiúscula, minúscula, dígito, especial, email único)


### **1.6-d** Configurar `AddJwtBearer()` no pipeline (validação: issuer, audience, signing key)


### **1.11** Configurar autorização com roles `Admin` e `Employee` e criá-las via `RoleManager`


### TokenService


### **1.6-a [TEST]** Escrever testes para `TokenService.GenerateToken()` — JWT válido, claims corretas, expiração


### **1.6-b [IMPL]** Implementar `TokenService.GenerateToken()` (emitir JWT assinado via Identity)


### AuthController


### **3.7-a [TEST]** Escrever testes de integração para `POST /api/auth/login`, `POST /api/auth/register` e `GET /api/auth/me`


### **3.7-b [IMPL]** Criar `AuthController` — `POST /api/auth/login` retorna `{ token, expiresAt }`, `POST /api/auth/register` (Admin), `GET /api/auth/me` lê do JWT


### Testes e Finalização


### **2.9-a [TEST]** Refatorar testes existentes do UserService para funcionar com Identity


### **2.9-b** Remover testes legados (`PasswordServiceTests`, `HashServiceTests`, `TokenServiceTests`)


### **2.10** Limpar pacotes legados do `SmartKiwiTest.csproj` (`Konscious.Security.Cryptography.Argon2`)


### **2.11** Projeto compila sem erros


## Em Andamento

### **3.3** Serviços registrados no DI (DbContext, Services, Identity, TokenService)


## Concluído

### **3.2** `Program.cs` configurado com `WebApplication.CreateBuilder(args)`


### **2.7** Remover `UserRepository` e `IUserRepository`


### **2.8** Tratar `IdentityResult` retornado pelos métodos do UserManager


### **2.2** `AuthenticateUser()` usar `SignInManager.PasswordSignInAsync()`


### **2.1-b** Criar DTO `LoginRequest` com `Email`, `Password`


### **2.6** `DeleteCurrentUser()` usar `UserManager`


### **2.5** `UpdateUserPassword()` usar `UserManager`


### **2.1-d** Criar DTO `ChangePasswordRequest` com `CurrentPassword`, `NewPassword`


### **2.3** `UpdateUserName()` usar `UserManager`


### **2.4** `UpdateUserEmail()` usar `UserManager`


### **2.1-c** Criar DTO `UpdateUserNameRequest` com `Name`, `Email`


### **2.1-a** Criar DTO `CreateUserRequest` com `Name`, `Email`, `Password`, `Role`


### **3.1** SDK do `.csproj` alterado para `Microsoft.NET.Sdk.Web`


### **3.6** EF Core InMemory mantido para desenvolvimento


### **1.5** Adicionar pacote `Microsoft.AspNetCore.Identity.EntityFrameworkCore`


### **1.1** `User.cs` estender `IdentityUser<Guid>` com `Name` e `UserRole`


### **1.2** Campo `Email` herdado do Identity


### **1.3** Campo `PasswordHash` herdado do Identity


### **1.4** `SmartKiwiContext` estender `IdentityDbContext<User, IdentityRole<Guid>, Guid>`


### **2.1** `CreateNewUser()` usar `UserManager.CreateAsync()`


### **1.7** Remover arquivos: `JwtConfig.cs`, `TokenService.cs` (antigo), `PasswordService.cs`, `HashService.cs`


### **1.8** Remover interfaces: `ITokenService`, `IPasswordService`, `IHashService`


## 🚧 Impedimentos

### Nenhum impedimento registrado.


## 📝 Observações

### **3.2 (Program.cs) é o cartão mais crítico** — sem ele, nenhum pipeline funciona. Posicionado como primeiro de `A Fazer`.


### UserService implementado, testes sendo refatorados (2.9-a).


### Sprint Goal revisado: cookies → JWT (aprovado PO).


### DTOs agrupados em `UserDtos.cs`.


### AuthController unificado na Sprint 1 (login, register, me).


