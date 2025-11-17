# Product Backlog - Formato Estruturado para Automação

**Versão:** 3.0.0
**Data:** 11/11/2025
**Autor:** Vander Loto - CTO DATAMETRIA

> **IMPORTANTE**: Este formato é otimizado para parsing automático via script PowerShell.
> Mantenha a estrutura de blocos `---CARD-START---` e `---CARD-END---` intacta.

---

## 📋 Instruções de Uso

### Como Criar Cards

Cada card deve estar entre os delimitadores:
```
---CARD-START---
[campos do card]
---CARD-END---
```

### Campos Obrigatórios

- `id`: Identificador único (FEAT-001, BUG-001, TECH-001, etc.)
- `title`: Título do card
- `status`: Todo, In Progress, In Review, Done
- `priority`: Critical, High, Medium, Low

### Campos Opcionais

- `story_points`: Número de pontos (0-100)
- `assignee`: Nome do responsável
- `project_id`: ID do projeto no sistema
- `epic`: Nome do épico
- `milestone_id`: ID do milestone
- `sprint`: Nome/número do sprint
- `start_date`: Data início (YYYY-MM-DD)
- `due_date`: Data entrega (YYYY-MM-DD)
- `detailed_description`: Descrição detalhada
- `acceptance_criteria`: Critérios de aceitação (lista)
- `technical_notes`: Notas técnicas
- `deliverables`: Entregáveis esperados

---

## 🎯 Backlog de Features

---CARD-START---
id: FEAT-001
title: Sistema de Autenticação JWT
status: Todo
priority: Critical
story_points: 8
assignee: João Silva
project_id: DATAMETRIA-project-mgmt
epic: Segurança e Autenticação
milestone_id: MVP-v1.0
sprint: Sprint 1
start_date: 2025-11-15
due_date: 2025-11-22
detailed_description: |
  Implementar sistema completo de autenticação usando JWT com refresh tokens.
  O sistema deve suportar login via email/senha e integração com Firebase Auth.
acceptance_criteria: |
  - Login com email e senha funcional
  - Token JWT gerado com expiração de 15min
  - Refresh token implementado (7 dias)
  - Logout invalida tokens
  - Rate limiting configurado (10 req/min)
technical_notes: |
  - Usar Firebase Auth para gerenciamento de usuários
  - Implementar middleware de autenticação no FastAPI
  - Configurar Redis para blacklist de tokens
deliverables: |
  - Endpoint POST /api/v1/auth/login
  - Endpoint POST /api/v1/auth/refresh
  - Endpoint POST /api/v1/auth/logout
  - Documentação OpenAPI
  - Testes unitários (85% coverage)
---CARD-END---

---CARD-START---
id: FEAT-002
title: Dashboard do Desenvolvedor
status: In Progress
priority: High
story_points: 13
assignee: Maria Santos
project_id: DATAMETRIA-project-mgmt
epic: Dashboards
milestone_id: MVP-v1.0
sprint: Sprint 1
start_date: 2025-11-15
due_date: 2025-11-29
detailed_description: |
  Dashboard personalizado para desenvolvedores visualizarem suas tarefas,
  métricas pessoais e controle de time tracking.
acceptance_criteria: |
  - Visualização de cards em kanban
  - Métricas pessoais (velocity, cycle time)
  - Botões de time tracking (iniciar, pausar, concluir)
  - Filtros por status e prioridade
  - Atualização em tempo real
technical_notes: |
  - Vue.js 3 com Composition API
  - Pinia para state management
  - Integração com API /api/v1/projects/items/gantt
deliverables: |
  - Componente DashboardView.vue
  - Composable useDashboard.ts
  - Testes unitários (85% coverage)
---CARD-END---

---CARD-START---
id: FEAT-003
title: Criação de Cards via Interface
status: Done
priority: High
story_points: 5
assignee: Carlos Oliveira
project_id: DATAMETRIA-project-mgmt
epic: Gestão de Cards
milestone_id: MVP-v1.0
sprint: Sprint 1
start_date: 2025-11-10
due_date: 2025-11-11
detailed_description: |
  Modal para criação de novos cards com todos os campos necessários.
  Deve validar dados e criar card no Firestore.
acceptance_criteria: |
  - Modal com formulário completo
  - Validação de campos obrigatórios
  - Criação no Firestore via API
  - Feedback de sucesso/erro
  - Recarregamento automático da lista
technical_notes: |
  - Componente CreateCardModal.vue
  - Endpoint POST /api/v1/projects/items
  - Validação com Pydantic no backend
deliverables: |
  - CreateCardModal.vue
  - Endpoint backend implementado
  - Testes E2E
---CARD-END---

## 🐛 Backlog de Bugs

---CARD-START---
id: BUG-001
title: Erro 500 ao concluir trabalho
status: In Progress
priority: Critical
story_points: 3
assignee: Ana Costa
project_id: DATAMETRIA-project-mgmt
start_date: 2025-11-11
due_date: 2025-11-11
detailed_description: |
  Endpoint /complete-work retorna erro 500 quando actual_hours é None.
  Erro: float() argument must be a string or a real number, not 'NoneType'
acceptance_criteria: |
  - Endpoint funciona com actual_hours = None
  - Endpoint funciona com actual_hours = 0
  - Endpoint funciona com actual_hours > 0
  - Testes cobrem todos os casos
technical_notes: |
  - Adicionar tratamento de None no backend
  - Converter None para 0.0 antes de calcular
  - Adicionar logs de debug
deliverables: |
  - Fix no endpoint complete-work
  - Testes unitários adicionados
  - Deploy em produção
---CARD-END---

## 🔧 Backlog de Melhorias Técnicas

---CARD-START---
id: TECH-001
title: Adicionar campo time_tracking no endpoint gantt
status: Done
priority: High
story_points: 2
assignee: Vander Loto
project_id: DATAMETRIA-project-mgmt
start_date: 2025-11-11
due_date: 2025-11-11
detailed_description: |
  Endpoint /items/gantt não retorna campo time_tracking, impedindo
  que botões de controle apareçam corretamente no frontend.
acceptance_criteria: |
  - Campo time_tracking incluído na resposta
  - Frontend recebe dados de tracking
  - Botões aparecem/desaparecem corretamente
technical_notes: |
  - Adicionar time_tracking no items.append()
  - Fazer deploy do backend
deliverables: |
  - Backend atualizado
  - Deploy em Cloud Run
---CARD-END---

---CARD-START---
id: PERF-001
title: Otimizar carregamento de cards
status: Todo
priority: Medium
story_points: 5
assignee: A definir
project_id: DATAMETRIA-project-mgmt
epic: Performance
detailed_description: |
  Carregamento de cards está lento quando há muitos registros.
  Implementar paginação e cache.
acceptance_criteria: |
  - Paginação implementada (20 cards por página)
  - Cache Redis configurado (TTL 5min)
  - Tempo de resposta < 500ms
technical_notes: |
  - Usar Firestore pagination
  - Implementar cache com Redis
  - Adicionar índices no Firestore
deliverables: |
  - Paginação backend
  - Cache implementado
  - Testes de performance
---CARD-END---

## 💡 Icebox - Ideias Futuras

---CARD-START---
id: IDEA-001
title: Sincronização bidirecional com GitHub
status: Todo
priority: Low
story_points: 21
project_id: DATAMETRIA-project-mgmt
detailed_description: |
  Implementar sincronização Sistema → GitHub Projects V2.
  Atualmente apenas GitHub → Sistema funciona.
acceptance_criteria: |
  - Cards criados no sistema aparecem no GitHub
  - Atualizações sincronizam em tempo real
  - Batch job noturno como fallback
technical_notes: |
  - Usar GitHub GraphQL API
  - Implementar webhook reverso
  - Armazenar github_item_id no Firestore
deliverables: |
  - Sincronização bidirecional
  - Documentação de arquitetura
---CARD-END---

---

## 📊 Estatísticas do Backlog

**Total de Cards**: [Calculado automaticamente pelo script]
**Por Status**:
- Todo: X cards
- In Progress: X cards
- In Review: X cards
- Done: X cards

**Por Prioridade**:
- Critical: X cards
- High: X cards
- Medium: X cards
- Low: X cards

**Story Points Total**: X pontos
