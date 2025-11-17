# Template - Criação de Memory Bank

**Versão:** 1.0
**Data:** 19/10/2025
**Autor:** Vander Loto - CTO DATAMETRIA

---

## 🎯 Propósito

Este template fornece instruções completas para o **Amazon Q Developer criar um Memory Bank** para qualquer projeto novo, garantindo contexto persistente e consistência com DATAMETRIA Standards.

---

## 📋 Quando Usar

Use este template quando:

- Iniciar um novo projeto
- Migrar projeto existente para DATAMETRIA Standards
- Usuário solicitar "crie o memory bank" ou "configure memory bank"

---

## 🔄 Processo de Criação

### Passo 1: Perguntas ao Usuário

Antes de criar o Memory Bank, **SEMPRE pergunte**:

```
Para criar o Memory Bank do seu projeto, preciso de algumas informações:

1. **Nome do Projeto**: Como se chama o projeto?
2. **Tipo de Projeto**: (Web App, Mobile App, API, Data Pipeline, AI/ML, etc.)
3. **Stack Tecnológico**: Quais tecnologias principais? (ex: Flask + Vue.js, Flutter, FastAPI)
4. **Objetivo Principal**: Qual problema o projeto resolve?
5. **Equipe**: Quantas pessoas? Quais papéis? (Dev, QA, DevOps, etc.)
6. **Prazo**: Quando deve estar pronto?
7. **Métricas de Sucesso**: Como medir sucesso? (usuários, performance, etc.)
```

### Passo 2: Criar Estrutura

```bash
mkdir -p .amazonq/rules/memory
```

### Passo 3: Gerar Arquivos

Crie os 5 arquivos do Memory Bank na ordem:

1. `idea.md` - Visão do produto
2. `vibe.md` - Cultura da equipe
3. `state.md` - Estado técnico
4. `decisions.md` - Decisões arquiteturais
5. `q-vibes-memory-banking.md` - Instruções (copiar do DATAMETRIA Standards)

---

## 📄 Template: idea.md

```markdown
# [NOME_PROJETO] - Visão do Produto

**Versão:** 1.0
**Data:** [DATA_ATUAL]
**Autor:** [NOME_RESPONSAVEL]

---

## 🎯 Visão

[DESCREVER_VISAO_PRODUTO]

Exemplo: "Criar uma plataforma de e-commerce que permita pequenos comerciantes venderem online com facilidade."

---

## 🚀 Objetivos

### Objetivo Principal

[OBJETIVO_PRINCIPAL]

Exemplo: "Permitir que 1000 comerciantes criem suas lojas online em 6 meses."

### Objetivos Secundários

1. [OBJETIVO_1]
2. [OBJETIVO_2]
3. [OBJETIVO_3]

---

## 💡 Problema que Resolve

### Dores Atuais

1. **[DOR_1]**: [DESCRICAO]
2. **[DOR_2]**: [DESCRICAO]
3. **[DOR_3]**: [DESCRICAO]

### Solução

[COMO_PROJETO_RESOLVE]

---

## 🎯 Critérios de Sucesso

### Métricas Quantitativas

| Métrica | Baseline | Meta | Prazo |
|---------|----------|------|-------|
| **[METRICA_1]** | [VALOR_ATUAL] | [VALOR_META] | [PRAZO] |
| **[METRICA_2]** | [VALOR_ATUAL] | [VALOR_META] | [PRAZO] |
| **[METRICA_3]** | [VALOR_ATUAL] | [VALOR_META] | [PRAZO] |

---

## 👥 Personas

### Persona 1: [NOME_PERSONA]

**Cargo**: [CARGO]
**Experiência**: [ANOS]

**Necessidades**:
- [NECESSIDADE_1]
- [NECESSIDADE_2]

**Como o Projeto Ajuda**:
- [BENEFICIO_1]
- [BENEFICIO_2]

---

## 🗺️ Roadmap

### [FASE_1] ([PRAZO])

- [ ] [ENTREGAVEL_1]
- [ ] [ENTREGAVEL_2]

### [FASE_2] ([PRAZO])

- [ ] [ENTREGAVEL_1]
- [ ] [ENTREGAVEL_2]

---

**Mantido por:** [RESPONSAVEL]
**Próxima revisão:** [DATA_PROXIMA_REVISAO]
```

---

## 📄 Template: vibe.md

```markdown
# [NOME_PROJETO] - Cultura da Equipe

**Versão:** 1.0
**Data:** [DATA_ATUAL]
**Autor:** [NOME_RESPONSAVEL]

---

## 🎭 Estilo de Colaboração

### [MODELO_TRABALHO]

Exemplo: "AI-First Development: 90% Amazon Q + 10% Supervisão Humana"

**Princípios**:
- [PRINCIPIO_1]
- [PRINCIPIO_2]
- [PRINCIPIO_3]

---

## 💬 Tom e Linguagem

### Documentação

- [ESTILO_1]: [DESCRICAO]
- [ESTILO_2]: [DESCRICAO]

### Comunicação Interna

- [CANAL_1]: [USO]
- [CANAL_2]: [USO]

---

## 🤝 Valores da Equipe

### 1. [VALOR_1]

**O que significa**: [DESCRICAO]

**Como praticamos**: [PRATICA]

### 2. [VALOR_2]

**O que significa**: [DESCRICAO]

**Como praticamos**: [PRATICA]

---

## 🔄 Workflow de Desenvolvimento

### 1. Planejamento

**Responsável**: [PAPEL]
**Duração**: [TEMPO]

**Processo**:
1. [PASSO_1]
2. [PASSO_2]

### 2. Desenvolvimento

**Responsável**: [PAPEL]
**Duração**: [TEMPO]

**Processo**:
1. [PASSO_1]
2. [PASSO_2]

---

**Mantido por:** [RESPONSAVEL]
**Próxima revisão:** [DATA_PROXIMA_REVISAO]
```

---

## 📄 Template: state.md

```markdown
# [NOME_PROJETO] - Estado Técnico Atual

**Versão:** 1.0
**Data:** [DATA_ATUAL]
**Autor:** [NOME_RESPONSAVEL]

---

## 📊 Snapshot Técnico

### Versão Atual

**[NOME_PROJETO] v[VERSAO]**
- Data de Release: [DATA]
- Status: [STATUS]
- Próxima Revisão: [DATA]

---

## 🛠️ Stack Tecnológico

### Backend

| Tecnologia | Versão | Uso | Status |
|------------|--------|-----|--------|
| **[TECH_1]** | [VERSAO] | [USO] | ✅ Produção |
| **[TECH_2]** | [VERSAO] | [USO] | ✅ Produção |

### Frontend

| Tecnologia | Versão | Uso | Status |
|------------|--------|-----|--------|
| **[TECH_1]** | [VERSAO] | [USO] | ✅ Produção |

### Database

| Tecnologia | Versão | Uso | Status |
|------------|--------|-----|--------|
| **[TECH_1]** | [VERSAO] | [USO] | ✅ Produção |

---

## 📈 Métricas Atuais

### Qualidade de Código

| Métrica | Meta | Atual | Tendência |
|---------|------|-------|-----------|
| **Coverage** | [META]% | [ATUAL]% | [TENDENCIA] |
| **Bugs/Sprint** | < [META] | [ATUAL] | [TENDENCIA] |

---

## 🎯 Projetos Ativos

### Em Produção

1. **[PROJETO_1]**
   - Status: ✅ Produção
   - Stack: [STACK]
   - Equipe: [TAMANHO]

---

## 🔧 Configurações Padrão

### [LINGUAGEM] ([ARQUIVO_CONFIG])

```[LINGUAGEM]
[CONFIGURACAO_EXEMPLO]
```

---

**Mantido por:** [RESPONSAVEL]
**Próxima revisão:** [DATA_PROXIMA_REVISAO]

```

---

## 📄 Template: decisions.md

```markdown
# [NOME_PROJETO] - Decisões Arquiteturais

**Versão:** 1.0
**Data:** [DATA_ATUAL]
**Autor:** [NOME_RESPONSAVEL]

---

## 📋 Formato ADR

Todas as decisões seguem o formato:

```markdown
## [YYYY-MM-DD] Título da Decisão

**Status**: Aceita | Rejeitada | Superseded

**Contexto**: Por que precisamos tomar esta decisão?

**Decisão**: O que decidimos fazer?

**Consequências**:
- ✅ Benefícios
- ⚠️ Trade-offs
- ❌ Riscos

**Alternativas Consideradas**:
1. Opção A - Por que não escolhemos
2. Opção B - Por que não escolhemos
```

---

## [DATA] [TITULO_DECISAO_1]

**Status**: ✅ Aceita

**Contexto**:
[CONTEXTO]

**Decisão**:
[DECISAO]

**Consequências**:

- ✅ [BENEFICIO]
- ⚠️ [TRADEOFF]

**Alternativas Consideradas**:

1. [ALTERNATIVA] - [MOTIVO_REJEICAO]

---

**Mantido por:** [RESPONSAVEL]
**Próxima revisão:** [DATA_PROXIMA_REVISAO]

```

---

## 📄 Template: q-vibes-memory-banking.md

**IMPORTANTE**: Este arquivo deve ser **copiado diretamente** do DATAMETRIA Standards:

```bash
cp DATAMETRIA-standards/docs/memory/q-vibes-memory-banking.md ./.amazonq/rules/memory/
```

**NÃO modifique** este arquivo - ele contém instruções universais para Amazon Q.

---

## 🤖 Instruções para Amazon Q

### Quando Usuário Pedir "Crie o Memory Bank"

1. **Fazer Perguntas** (Passo 1)
2. **Criar Estrutura** de pastas
3. **Gerar idea.md** com respostas do usuário
4. **Gerar vibe.md** adaptado ao contexto
5. **Gerar state.md** com stack informado
6. **Gerar decisions.md** vazio (será preenchido ao longo do projeto)
7. **Copiar q-vibes-memory-banking.md** do DATAMETRIA Standards
8. **Confirmar** com usuário: "Memory Bank criado! Revise os arquivos e ajuste conforme necessário."

### Exemplo de Prompt de Resposta

```
✅ Memory Bank criado com sucesso!

Arquivos gerados em `.amazonq/rules/memory/`:
- idea.md (visão do produto)
- vibe.md (cultura da equipe)
- state.md (estado técnico)
- decisions.md (decisões arquiteturais - vazio)
- q-vibes-memory-banking.md (instruções)

📝 Próximos passos:
1. Revise idea.md e ajuste objetivos/métricas
2. Revise vibe.md e ajuste valores/workflow
3. Revise state.md e confirme stack tecnológico
4. À medida que tomar decisões arquiteturais, documente em decisions.md

💡 Dica: Consulte o Memory Bank no início de cada sessão para manter contexto!
```

---

## ✅ Checklist de Validação

Após criar Memory Bank, verifique:

- [ ] Todos os 5 arquivos foram criados
- [ ] idea.md tem objetivos claros e mensuráveis
- [ ] vibe.md reflete cultura da equipe
- [ ] state.md lista stack tecnológico completo
- [ ] decisions.md tem formato ADR correto
- [ ] q-vibes-memory-banking.md foi copiado (não modificado)
- [ ] Usuário revisou e aprovou conteúdo

---

## 🎯 Exemplos por Tipo de Projeto

### Web App (Flask + Vue.js)

**idea.md**: Foco em usuários, features web, métricas de engajamento
**vibe.md**: AI-First, code review rigoroso, testes automatizados
**state.md**: Flask 3.0+, Vue.js 3.3+, PostgreSQL 16+
**decisions.md**: Decisão sobre Vue.js 3 Composition API, PostgreSQL vs MySQL

### Mobile App (Flutter)

**idea.md**: Foco em downloads, retenção, app stores
**vibe.md**: Mobile-first, performance crítica, UX impecável
**state.md**: Flutter 3.16+, Dart 3.2+, Firebase
**decisions.md**: Decisão sobre BLoC Pattern, Firebase vs Supabase

### API (FastAPI)

**idea.md**: Foco em throughput, latência, integrações
**vibe.md**: API-first, documentação automática, versionamento
**state.md**: FastAPI 0.104+, PostgreSQL, Redis
**decisions.md**: Decisão sobre FastAPI vs Flask, async vs sync

---

**Mantido por:** Vander Loto - CTO DATAMETRIA
**Próxima revisão:** 19/01/2026
