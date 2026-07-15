# 📋 SPRINTS

> Índice geral do planejamento das sprints.
> Cada sprint possui seu próprio quadro Kanban na pasta `kanban/`.

---

## 🏃 Sprints Planejadas

| Sprint | Foco | Período | Status | Quadro |
|---|---|---|---|---|
| **Sprint 1** | Substituir JWT/Argon2 por Identity + Web API | 13/07 - 19/07 | 🔴 Em andamento | [kanban/sprint-01.md](./kanban/sprint-01.md) |
| **Sprint 2** | API de Autenticação e Ajustes | 20/07 - 26/07 | ⏳ Agendada | [kanban/sprint-02.md](./kanban/sprint-02.md) |
| **Sprint 3** | API de Negócio (Filas, Check-in, Chamada) | 27/07 - 02/08 | ⏳ Agendada | [kanban/sprint-03.md](./kanban/sprint-03.md) |
| **Sprint 4** | Guichê + Painel | 03/08 - 09/08 | ⏳ Agendada | [kanban/sprint-04.md](./kanban/sprint-04.md) |

---

## 📊 Dependências

| Sprint | Depende de |
|---|---|
| Sprint 1 | Nenhuma |
| Sprint 2 | Sprint 1 |
| Sprint 3 | Sprint 2 |
| Sprint 4 | Sprint 3 |

---

## 📁 Estrutura

```
kanban/
├── sprint-01.md    ← Kanban detalhado da Sprint 1
├── sprint-02.md    ← Kanban detalhado da Sprint 2
├── sprint-03.md    ← Kanban detalhado da Sprint 3
└── sprint-04.md    ← Kanban detalhado da Sprint 4
```

> **Nota:** Para abrir o quadro Kanban no VS Code, instale a extensão **Markdown Kanban Board** e abra o arquivo da sprint desejada em `kanban/`.
