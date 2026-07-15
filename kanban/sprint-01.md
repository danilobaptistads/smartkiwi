# Sprint 1 — Substituir JWT/Argon2 por Identity + Web API

## A Fazer

### **2.1** `CreateNewUser()` usar `UserManager.CreateAsync()` em vez de `_userRepository.Add()`


### **2.2** `AuthenticateUser()` usar `SignInManager.PasswordSignInAsync()` em vez de validação manual de hash


### **2.3** `UpdateUserName()` usar `UserManager`


### **2.4** `UpdateUserEmail()` usar `UserManager`


### **2.5** `UpdateUserPassword()` usar `UserManager`


### **2.6** `DeleteCurrentUser()` usar `UserManager`


### **2.7** Remover `UserRepository` e `IUserRepository`


### **1.7** Remover arquivos: `JwtConfig.cs`, `TokenService.cs`, `PasswordService.cs`, `HashService.cs`


### **1.8** Remover interfaces: `ITokenService`, `IPasswordService`, `IHashService`


### **1.6** Remover pacote `Microsoft.AspNetCore.Authentication.JwtBearer`


### **3.2** `Program.cs` configurado com `WebApplication.CreateBuilder(args)`


### **3.3** Serviços registrados no DI (DbContext, Repositories, Services, Identity)


### **3.4** Controllers configurados (`AddControllers()`)


### **3.5** Swagger/OpenAPI configurado


### **1.9** Configurar Identity no pipeline (regras de senha: 8 chars, maiúscula, minúscula, dígito, especial, email único)


### **1.10** Configurar autenticação via cookie (`AddCookie`) com `HttpOnly`, `SameSite=Strict`


### **1.11** Configurar autorização com roles `Admin` e `Employee` e criá-las via `RoleManager`


### **2.8** Testes de `UserServiceTests` e `UserRepositoryTests` atualizados; testes legados removidos (`PasswordServiceTests`, `HashServiceTests`, `TokenServiceTests`); projeto compila sem erros


## Em Andamento

## Concluído

### **1.5** Adicionar pacote `Microsoft.AspNetCore.Identity.EntityFrameworkCore`


### **1.4** `SmartKiwiContext` estender `IdentityDbContext<User, IdentityRole<Guid>, Guid>`


### **1.3** Campo `PasswordHash` herdado do Identity, não declarado manualmente em `User.cs`


### **1.2** Campo `Email` herdado do Identity, não declarado manualmente em `User.cs`


### **1.1** `User.cs` estender `IdentityUser<Guid>` com `Name` (string) e `UserRole` (enum Role)


### **3.6** EF Core InMemory mantido para desenvolvimento


### **3.1** SDK do `.csproj` alterado para `Microsoft.NET.Sdk.Web`


### Nenhum cartão concluído nesta sprint.


## 🚧 Impedimentos

### Nenhum impedimento registrado.


## 📝 Observações

### Ordem dos cartões respeita dependências: fundação → modelos → migração UserService → limpeza legados → pipeline → testes.


### `Baseantiga/` contém código legado do console — não será migrado.


