# Sprint Planning - [Nome da Sprint]

<div align="center">

**Sprint Planning** | **Versão**: 1.0.0 | **Última Atualização**: DD/MM/AAAA

[![DATAMETRIA](https://img.shields.io/badge/DATAMETRIA-Standards-blue)](https://github.com/datametria)
[![Sprint](https://img.shields.io/badge/Sprint-Semanal-green)](.)
[![Capacidade](https://img.shields.io/badge/Capacidade-25%20SP-orange)](.)

[🔗 Template Original](https://github.com/datametria/DATAMETRIA-project-mgmt) • [🔗 Diretrizes](../stacks/datametria_std_documentation.md) • [🔗 Importação](../../docs/operations/BACKLOG-IMPORT-GUIDE.md)

</div>

---

## 📋 Índice

- [🎯 Informações da Sprint](#-informações-da-sprint)
- [📊 Capacidade e Métricas](#-capacidade-e-métricas)
- [🎯 Objetivos da Sprint](#-objetivos-da-sprint)
- [📦 Cards da Sprint](#-cards-da-sprint)
- [⚠️ Riscos e Dependências](#️-riscos-e-dependências)
- [📝 Notas da Planning](#-notas-da-planning)

---

## 🎯 Informações da Sprint

### Identificação

| Campo | Valor |
|-------|-------|
| **Nome da Sprint** | Sprint DD/MM a DD/MM |
| **Número** | Sprint #XX |
| **Período** | DD/MM/AAAA a DD/MM/AAAA |
| **Duração** | 7 dias (1 semana) |
| **Data da Planning** | DD/MM/AAAA |
| **Tipo** | Multi-Projeto |

### Participantes

| Papel | Nome | Email |
|-------|------|-------|
| **Scrum Master** | Nome | email@datametria.io |
| **Product Owner** | Nome | email@datametria.io |
| **Tech Lead** | Nome | email@datametria.io |
| **Desenvolvedores** | Nome 1, Nome 2, Nome 3 | - |

---

## 📊 Capacidade e Métricas

### Capacidade da Sprint

| Métrica | Valor | Status |
|---------|-------|--------|
| **Capacidade Total** | 25 story points | 🎯 Meta |
| **Story Points Planejados** | 0 SP | ⚪ Planejando |
| **Utilização** | 0% | ⚪ Planejando |
| **Cards Planejados** | 0 cards | ⚪ Planejando |

### Distribuição por Projeto

| Projeto | Story Points | % Capacidade | Cards |
|---------|--------------|--------------|-------|
| Projeto A | 0 SP | 0% | 0 |
| Projeto B | 0 SP | 0% | 0 |
| Projeto C | 0 SP | 0% | 0 |
| **Total** | **0 SP** | **0%** | **0** |

### Distribuição por Tipo

| Tipo | Story Points | % Total | Cards |
|------|--------------|---------|-------|
| 🎯 Features | 0 SP | 0% | 0 |
| 🐛 Bugs | 0 SP | 0% | 0 |
| 🔧 Tech Debt | 0 SP | 0% | 0 |
| ⚡ Performance | 0 SP | 0% | 0 |
| **Total** | **0 SP** | **100%** | **0** |

---

## 🎯 Objetivos da Sprint

### Objetivo Principal

> Descreva o objetivo principal desta sprint em 1-2 frases.

### Objetivos Secundários

1. **Objetivo 1**: Descrição
2. **Objetivo 2**: Descrição
3. **Objetivo 3**: Descrição

### Critérios de Sucesso

- [ ] Critério 1
- [ ] Critério 2
- [ ] Critério 3

---

## 📦 Cards da Sprint

### 🎯 Features (0 SP)

---CARD-START---
id: FEAT-001
title: [Título da Feature]
status: Todo
priority: High
story_points: 8
assignee: Nome do Desenvolvedor
project_id: nome-do-projeto
sprint: Sprint DD/MM a DD/MM
start_date: YYYY-MM-DD
due_date: YYYY-MM-DD
detailed_description: |
  Descrição detalhada da feature.
  
  **Contexto:**
  - Contexto relevante
  
  **Requisitos:**
  - Requisito 1
  - Requisito 2
acceptance_criteria: |
  - [ ] Critério 1
  - [ ] Critério 2
  - [ ] Critério 3
technical_notes: |
  - Stack: Python + FastAPI
  - Integração: API externa
  - Testes: Coverage > 80%
---CARD-END---

---CARD-START---
id: FEAT-002
title: [Título da Feature]
status: Todo
priority: Medium
story_points: 5
assignee: Nome do Desenvolvedor
project_id: nome-do-projeto
sprint: Sprint DD/MM a DD/MM
start_date: YYYY-MM-DD
due_date: YYYY-MM-DD
detailed_description: |
  Descrição detalhada da feature.
acceptance_criteria: |
  - [ ] Critério 1
  - [ ] Critério 2
---CARD-END---

### 🐛 Bugs (0 SP)

---CARD-START---
id: BUG-001
title: [Título do Bug]
status: Todo
priority: Critical
story_points: 3
assignee: Nome do Desenvolvedor
project_id: nome-do-projeto
sprint: Sprint DD/MM a DD/MM
start_date: YYYY-MM-DD
due_date: YYYY-MM-DD
detailed_description: |
  **Descrição do Problema:**
  Descrição clara do bug.
  
  **Passos para Reproduzir:**
  1. Passo 1
  2. Passo 2
  3. Passo 3
  
  **Comportamento Esperado:**
  O que deveria acontecer.
  
  **Comportamento Atual:**
  O que está acontecendo.
  
  **Ambiente:**
  - Browser: Chrome 120
  - OS: Windows 11
  - Versão: v0.3.0
acceptance_criteria: |
  - [ ] Bug reproduzido e confirmado
  - [ ] Correção implementada
  - [ ] Testes adicionados
  - [ ] Verificado em staging
technical_notes: |
  - Arquivo afetado: src/components/Card.vue
  - Possível causa: Estado não sincronizado
---CARD-END---

### 🔧 Tech Debt (0 SP)

---CARD-START---
id: TECH-001
title: [Título do Tech Debt]
status: Todo
priority: Medium
story_points: 5
assignee: Nome do Desenvolvedor
project_id: nome-do-projeto
sprint: Sprint DD/MM a DD/MM
start_date: YYYY-MM-DD
due_date: YYYY-MM-DD
detailed_description: |
  **Problema Técnico:**
  Descrição do débito técnico.
  
  **Impacto:**
  - Performance degradada
  - Manutenibilidade reduzida
  
  **Solução Proposta:**
  Refatorar módulo X usando padrão Y.
acceptance_criteria: |
  - [ ] Código refatorado
  - [ ] Testes mantidos/melhorados
  - [ ] Documentação atualizada
  - [ ] Performance melhorada
technical_notes: |
  - Padrão: Clean Architecture
  - Ferramentas: ESLint, Prettier
---CARD-END---

### ⚡ Performance (0 SP)

---CARD-START---
id: PERF-001
title: [Título da Otimização]
status: Todo
priority: Low
story_points: 3
assignee: Nome do Desenvolvedor
project_id: nome-do-projeto
sprint: Sprint DD/MM a DD/MM
start_date: YYYY-MM-DD
due_date: YYYY-MM-DD
detailed_description: |
  **Problema de Performance:**
  Descrição do problema.
  
  **Métrica Atual:**
  - Tempo de resposta: 2000ms
  - Uso de memória: 500MB
  
  **Meta:**
  - Tempo de resposta: < 500ms
  - Uso de memória: < 200MB
acceptance_criteria: |
  - [ ] Métrica de performance melhorada
  - [ ] Testes de carga executados
  - [ ] Monitoramento configurado
technical_notes: |
  - Ferramenta: Lighthouse, Chrome DevTools
  - Técnica: Lazy loading, caching
---CARD-END---

---

## ⚠️ Riscos e Dependências

### Riscos Identificados

| Risco | Probabilidade | Impacto | Mitigação |
|-------|---------------|---------|-----------|
| Dependência externa atrasada | Média | Alto | Comunicação proativa com fornecedor |
| Desenvolvedor ausente | Baixa | Médio | Backup de conhecimento |
| Complexidade subestimada | Média | Médio | Revisão técnica antecipada |

### Dependências

| Card | Depende de | Projeto | Status |
|------|------------|---------|--------|
| FEAT-002 | FEAT-001 | Projeto A | ⚪ Aguardando |
| BUG-001 | Deploy staging | Infra | ✅ Pronto |

### Bloqueios

- [ ] Nenhum bloqueio identificado

---

## 📝 Notas da Planning

### Decisões Tomadas

1. **Decisão 1**: Descrição e justificativa
2. **Decisão 2**: Descrição e justificativa
3. **Decisão 3**: Descrição e justificativa

### Itens Removidos do Escopo

| Card | Motivo | Ação |
|------|--------|------|
| FEAT-XXX | Complexidade alta | Mover para próxima sprint |
| BUG-XXX | Prioridade baixa | Retornar ao backlog |

### Itens Adicionados

| Card | Motivo | Justificativa |
|------|--------|---------------|
| BUG-001 | Crítico em produção | Cliente reportou |

### Compromissos da Equipe

- [ ] Daily standup às 9h (Discord)
- [ ] Code review em até 24h
- [ ] Atualizar status dos cards diariamente
- [ ] Comunicar bloqueios imediatamente

### Observações

- Observação 1
- Observação 2
- Observação 3

---

## 📊 Histórico de Sprints

### Sprints Anteriores

| Sprint | Período | Planejado | Concluído | Velocity |
|--------|---------|-----------|-----------|----------|
| Sprint #01 | 03/11 a 09/11 | 25 SP | 23 SP | 92% |
| Sprint #02 | 10/11 a 16/11 | 25 SP | 25 SP | 100% |
| Sprint #03 | 17/11 a 23/11 | 25 SP | 21 SP | 84% |

### Velocity Média

- **Últimas 3 sprints**: 23 SP
- **Últimas 5 sprints**: 22 SP
- **Tendência**: ↗️ Crescente

---

## 🎯 Checklist de Planning

### Antes da Planning

- [ ] Backlog refinado e priorizado
- [ ] Cards estimados (story points)
- [ ] Dependências identificadas
- [ ] Capacidade da equipe calculada
- [ ] Objetivos da sprint definidos

### Durante a Planning

- [ ] Objetivo da sprint acordado
- [ ] Cards selecionados (até 25 SP)
- [ ] Responsáveis atribuídos
- [ ] Datas definidas
- [ ] Riscos discutidos
- [ ] Compromissos firmados

### Após a Planning

- [ ] Cards importados para Firestore
- [ ] Sprint criada no sistema
- [ ] Equipe notificada
- [ ] Documentação atualizada
- [ ] Kick-off agendado

---

## 🚀 Importação para Firestore

### Comando de Importação

```powershell
.\scripts\Import-BacklogToFirestore.ps1 `
  -BacklogFile "docs\sprints\SPRINT-DD-MM.md"
```

### Validação (Dry Run)

```powershell
.\scripts\Import-BacklogToFirestore.ps1 `
  -BacklogFile "docs\sprints\SPRINT-DD-MM.md" `
  -DryRun
```

### Verificação

1. Acessar: https://datametria-project-mgmt.web.app/dashboard
2. Filtrar por sprint: "Sprint DD/MM a DD/MM"
3. Verificar 25 SP planejados
4. Confirmar cards atribuídos

---

## 📚 Referências

- [Guia de Importação de Backlog](../../docs/operations/BACKLOG-IMPORT-GUIDE.md)
- [Template de Product Backlog](template-product-backlog-structured.md)
- [Diretrizes de Documentação](../stacks/datametria_std_documentation.md)
- [DATAMETRIA Standards](../00-master-context.md)

---

## 📞 Suporte

**Dúvidas sobre Sprint Planning?**

- **Scrum Master**: Nome - email@datametria.io
- **Tech Lead**: Dalila Rodrigues - dalila.rodrigues@datametria.io
- **CTO**: Vander Loto - vander.loto@datametria.io
- **Discord**: Canal #sprint-planning

---

**Versão:** 1.0.0
**Data de Criação:** 11/11/2025
**Autor:** Vander Loto - CTO DATAMETRIA
**Próxima Revisão:** 11/02/2026

---

## 💡 Dicas de Uso

### Nomenclatura de Sprints

```
Sprint 10/11 a 16/11  ✅ Correto (segunda a domingo)
Sprint 03/11 a 09/11  ✅ Correto
Sprint 1               ❌ Incorreto (sem datas)
Sprint Novembro        ❌ Incorreto (não específico)
```

### Capacidade de 25 SP

- **Ideal**: 20-25 SP (80-100% capacidade)
- **Aceitável**: 18-25 SP (72-100% capacidade)
- **Evitar**: > 25 SP (overcommitment)
- **Evitar**: < 18 SP (underutilization)

### Multi-Projeto

- Distribuir cards entre projetos ativos
- Balancear prioridades
- Considerar dependências entre projetos
- Alocar desenvolvedores por expertise

### Estimativas

- **1-2 SP**: Tarefa simples (< 4h)
- **3-5 SP**: Tarefa média (4-8h)
- **8 SP**: Tarefa complexa (1-2 dias)
- **13 SP**: Muito complexo (quebrar em cards menores)
- **> 13 SP**: Épico (não deve entrar em sprint)

---

**🎯 Boa Sprint!**
