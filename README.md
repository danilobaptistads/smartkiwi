# 🥝 SmartKiwi  
### Sistema de Gerenciamento de Filas de atendimen com Interface em Console.
O **SmartKiwi** nasceu como parte do meu aprendizado em C# e estruturas de dados, um estudo de **filas encadeadas**, mas evoluiu para um sistema completo de gerenciamento de filas, com:

- Filas encadeadas manuais (`Node` → `Queue`)
- Múltiplos serviços de lógica (Check-in, Atendimento, Construção de filas, Prioridade)
- Sistema de envelhecimento (*aging*)
- Interface interativa em console separada por camadas (UI, Services, Models)
- Organização modular e arquitetura limpa

O projeto é ideal para estudos de:
- Estruturas de dados  
- POO avançada  
- Modularização  
- Regras de negócio complexas  
- Fluxos de atendimento realistas  

## 📂 Estrutura do Projeto
```
SmartKiwi/
├── Program.cs
├── App.cs
│
├── Models/
│ ├── Client.cs
│ ├── Node.cs
│ └── Queue.cs
│
├── Services/
│ ├── Aging.cs
│ ├── Attendant.cs
│ ├── CheckIn.cs
│ ├── CycleChecker.cs
│ ├── PrioritiesMatcher.cs
│ ├── QueueBuilder.cs
│ └── QueueController.cs
│
└── Controller/
├── InitialUi.cs
├── MainUi.cs
├── CheckInUi.cs
├── AttendantUi.cs
└── QueueBuilderUi.cs

```

## 🧠 Conceitos Aplicados

### ✔️ Filas Encadeadas (Linked Queues)
Implementadas manualmente usando nós (`Node`) e ponteiros para o próximo item.

### ✔️ Sistema de Prioridade Dinâmica  
A prioridade de cada cliente é ajustada por:
- Tempo de espera  
- Regras no módulo `Aging`  
- Comparações no `PrioritiesMatcher`  

### ✔️ Separação por Camadas
- **UI** – Interface via console  
- **Services** – Regras de negócio  
- **Models** – Estrutura dos dados  

### ✔️ Simulação Real de Atendimento
- Check-in  
- Chamadas  
- Aumento automático de prioridade  
- Mudança de ciclo  

---
## 🚀 Como Executar

### 1. Instale o .NET SDK (se ainda não tiver)
```bash
https://dotnet.microsoft.com/download
```
### 2. Clone o repositório
```bash
git clone https://github.com/seu-usuario/SmartKiwi.git
```
### 3. Acesse a pasta do projeto
```bash
cd SmartKiwi
```
### 4. Execute o projeto
```bash
dotnet run
```

## ✨ Funcionalidades

- [x] Criação de filas com prioridade  
- [x] Cadastro de clientes (check-in)  
- [x] Atendimento com regras personalizadas  
- [x] Sistema de aging (aumento automático de prioridade)  
- [x] Interface interativa via console  
- [ ] Persistência em banco de dados  
- [ ] Dashboard web  
- [ ] Relatórios de atendimento

## 📄 Licença

Este projeto está licenciado sob a **Licença MIT**.  
Você é livre para usar, copiar, modificar e distribuir este software, desde que mantenha o aviso de copyright.


- [ ] Estratégias de prioridade configuráveis  

