# Documentação: Padrões DATAMETRIA v2.0

> **Objetivo:** Codificar padrões de documentação como Rules atômicas, específicas e justificadas para aplicação automática pelo Amazon Q Developer.

**Versão:** 2.0
**Última atualização:** 19/10/2025
**Autor:** Vander Loto - CTO DATAMETRIA
**Baseado em:** AmazonQ-Guidelines v2.0

---

## Índice

1. [Visão Geral](#1-visão-geral)
2. [Rules de Documentação](#2-rules-de-documentação)
3. [Templates e Uso](#3-templates-e-uso)
4. [Automação e Ferramentas](#4-automação-e-ferramentas)
5. [Métricas de Conformidade](#5-métricas-de-conformidade)

---

## 1. Visão Geral

### Contexto

Documentação inconsistente causa:

- **60% mais tempo** em onboarding de desenvolvedores
- **40% mais bugs** por falta de especificações claras
- **Retrabalho constante** em revisões e auditorias
- **Perda de conhecimento** em transições de equipe

### Modelo AI-First

- **90% Amazon Q Developer**: Geração automática de documentação
- **10% Supervisão Humana**: Validação de conteúdo crítico

### Benefícios Mensuráveis

| Métrica | Antes | Depois | Melhoria |
|---------|-------|--------|----------|
| Tempo de documentação | 8h/projeto | 30min/projeto | 95% ↓ |
| Conformidade com padrões | 45% | 98% | 118% ↑ |
| Tempo de onboarding | 2 semanas | 3 dias | 85% ↓ |
| Bugs por falta de docs | 23/sprint | 3/sprint | 87% ↓ |

---

## 2. Rules de Documentação

### Rule 2.1: README Obrigatório

#### Contexto

Projetos sem README causam:

- Desenvolvedores perdidos sem saber por onde começar
- Perda de 2-4 horas por desenvolvedor em setup inicial
- Perguntas repetitivas sobre configuração básica

#### Regra

Todo projeto DEVE ter `README.md` na raiz com:

- Título e descrição clara (1-2 parágrafos)
- Badges de status (build, coverage, version)
- Seção "Como Usar" com comandos executáveis
- Seção "Instalação" com pré-requisitos
- Link para documentação completa

#### Justificativa

- **Reduz onboarding**: De 2 dias para 2 horas
- **Padronização**: Todos os projetos seguem mesma estrutura
- **Descoberta**: Badges mostram status em tempo real
- **Automação**: Amazon Q gera automaticamente

#### Exemplos

##### ✅ Correto

```markdown
# Sistema de Autenticação JWT

Sistema enterprise de autenticação com JWT, refresh tokens e MFA.

[![Build](https://img.shields.io/badge/build-passing-green)]()
[![Coverage](https://img.shields.io/badge/coverage-95%25-brightgreen)]()
[![Version](https://img.shields.io/badge/version-2.1.0-blue)]()

## Como Usar

```bash
# Instalar dependências
poetry install

# Configurar ambiente
cp .env.example .env

# Executar
poetry run python src/main.py
```

## Instalação

**Pré-requisitos:**

- Python 3.11+
- PostgreSQL 14+
- Redis 7+

Veja [INSTALL.md](docs/INSTALL.md) para detalhes.

```

##### ❌ Incorreto

```markdown
# Projeto

Este é um projeto de autenticação.

Para usar, instale as dependências e execute.
```

**Problemas:**

- Título vago sem contexto
- Sem badges de status
- Sem comandos executáveis
- Sem pré-requisitos claros

#### Exceções

- Scripts one-off em `scripts/temp/`
- Protótipos com < 1 semana de vida
- Projetos internos de pesquisa (até aprovação)

#### Ferramentas

- **Template**: `template-readme.md`
- **Geração**: Amazon Q com prompt "Gere README seguindo datametria_std_documentation"
- **Validação**: `markdownlint README.md`

#### Checklist de Conformidade

- [ ] README.md existe na raiz?
- [ ] Tem título descritivo?
- [ ] Inclui badges de status?
- [ ] Seção "Como Usar" com comandos?
- [ ] Lista pré-requisitos?

---

### Rule 2.2: ADR para Decisões Arquiteturais

#### Contexto

Decisões arquiteturais não documentadas causam:

- **Retrabalho**: Mesmas discussões repetidas
- **Inconsistência**: Decisões conflitantes em diferentes módulos
- **Perda de contexto**: "Por que fizemos assim?" sem resposta
- **Auditoria impossível**: Compliance e rastreabilidade comprometidos

#### Regra

Toda decisão arquitetural DEVE ser documentada em ADR (Architecture Decision Record) em `docs/adr/NNNN-titulo.md` com:

- **Contexto**: Problema ou necessidade
- **Decisão**: O que foi decidido
- **Consequências**: Trade-offs e impactos
- **Status**: Proposto | Aceito | Rejeitado | Depreciado
- **Data**: Quando foi decidido

#### Justificativa

- **Rastreabilidade**: Histórico completo de decisões
- **Onboarding**: Novos membros entendem "porquês"
- **Compliance**: Auditoria e governança facilitadas
- **Evolução**: Decisões podem ser revisitadas com contexto

#### Exemplos

##### ✅ Correto

```markdown
# ADR-0023: Migração de REST para GraphQL

**Status:** Aceito
**Data:** 2025-09-15
**Decisores:** Vander Loto (CTO), Equipe Backend

## Contexto

API REST cresceu para 87 endpoints com problemas:
- Over-fetching: Clientes baixam 3x mais dados que necessário
- Under-fetching: 40% das telas fazem 5+ requests
- Versionamento: 3 versões ativas causando complexidade

## Decisão

Migrar para GraphQL usando Apollo Server v4 com:
- Schema-first approach
- DataLoader para N+1 queries
- Persisted queries para segurança
- Migração gradual (6 meses)

## Consequências

### Positivas ✅
- Redução de 70% em chamadas de rede
- Type safety completo com TypeScript
- Melhor DX com GraphQL Playground
- Versionamento eliminado

### Negativas ⚠️
- Curva de aprendizado (2 semanas/dev)
- Complexidade de cache (Apollo Client)
- Custo de migração: 240 horas
- Monitoramento mais complexo

### Riscos 🔴
- Performance de queries complexas
- Exposição de schema (mitigado com persisted queries)

## Alternativas Consideradas

1. **REST + JSON:API**: Rejeitado - não resolve over-fetching
2. **gRPC**: Rejeitado - não adequado para web clients
3. **tRPC**: Rejeitado - lock-in TypeScript

## Referências
- [GraphQL Best Practices](https://graphql.org/learn/best-practices/)
- [Apollo Server Docs](https://www.apollographql.com/docs/apollo-server/)
```

##### ❌ Incorreto

```markdown
# Decisão: Usar GraphQL

Decidimos usar GraphQL porque é melhor que REST.

Vamos usar Apollo Server.
```

**Problemas:**

- Sem contexto do problema
- Sem consequências documentadas
- Sem alternativas consideradas
- Sem data ou status

#### Exceções

- Decisões táticas de implementação (não arquiteturais)
- Mudanças em código legado sem impacto arquitetural
- Experimentos em branches de feature

#### Ferramentas

- **Template**: `template-adr.md`
- **Numeração**: Sequencial com zero-padding (0001, 0002...)
- **Geração**: Amazon Q com contexto da decisão
- **Versionamento**: Git para histórico completo

#### Checklist de Conformidade

- [ ] ADR criado em `docs/adr/`?
- [ ] Numeração sequencial correta?
- [ ] Contexto explica o problema?
- [ ] Decisão é clara e específica?
- [ ] Consequências positivas E negativas?
- [ ] Alternativas foram consideradas?
- [ ] Status e data presentes?

---

### Rule 2.3: Docstrings Google Style

#### Contexto

Código sem documentação inline causa:

- **Perda de produtividade**: 30min/dia procurando como usar funções
- **Bugs**: Uso incorreto de APIs internas
- **Manutenção difícil**: Refatoração arriscada sem entender comportamento
- **Onboarding lento**: Novos devs não entendem código existente

#### Regra

Toda função/método público DEVE ter docstring Google Style com:

- Descrição breve (1 linha)
- Descrição detalhada (se necessário)
- Args: Todos os parâmetros com tipos
- Returns: Tipo e descrição do retorno
- Raises: Exceções possíveis
- Example: Caso de uso prático

#### Justificativa

- **Autodocumentação**: Código se explica
- **IDE Support**: Autocomplete e hints funcionam
- **Geração automática**: Sphinx/MkDocs geram docs
- **Type Safety**: Integração com mypy/pyright

#### Exemplos

##### ✅ Correto

```python
def processar_pagamento(
    usuario_id: str,
    valor: Decimal,
    metodo: str = "cartao",
    parcelamento: Optional[int] = None
) -> Dict[str, Any]:
    """Processa pagamento para usuário com validações de segurança.

    Valida saldo, limites e aplica regras de negócio antes de processar.
    Suporta múltiplos métodos de pagamento e parcelamento.

    Args:
        usuario_id: UUID do usuário no formato string
        valor: Valor do pagamento em Decimal (precisão financeira)
        metodo: Método de pagamento. Opções: "cartao", "pix", "boleto"
        parcelamento: Número de parcelas (1-12). None para pagamento à vista

    Returns:
        Dicionário com resultado do pagamento:
        {
            "transacao_id": str,
            "status": "aprovado" | "pendente" | "rejeitado",
            "valor_final": Decimal,
            "data_processamento": datetime
        }

    Raises:
        ValueError: Se valor <= 0 ou parcelamento inválido
        UsuarioNaoEncontrado: Se usuario_id não existe
        SaldoInsuficiente: Se usuário não tem saldo/limite
        ErroProcessamento: Se gateway de pagamento falhar

    Example:
        >>> resultado = processar_pagamento(
        ...     usuario_id="550e8400-e29b-41d4-a716-446655440000",
        ...     valor=Decimal("150.00"),
        ...     metodo="cartao",
        ...     parcelamento=3
        ... )
        >>> print(resultado["status"])
        'aprovado'
    """
    # Implementação...
```

##### ❌ Incorreto

```python
def processar_pagamento(usuario_id, valor, metodo="cartao", parcelamento=None):
    """Processa pagamento."""
    # Implementação...
```

**Problemas:**

- Sem type hints
- Descrição vaga
- Sem documentação de parâmetros
- Sem documentação de retorno
- Sem exceções documentadas
- Sem exemplo de uso

#### Exceções

- Funções privadas (prefixo `_`) podem ter docstrings simplificadas
- Métodos triviais (getters/setters simples)
- Código de teste (mas test docstrings são recomendadas)

#### Ferramentas

- **Validação**: `pydocstyle` ou `darglint`
- **Geração**: Amazon Q com prompt "Documente esta função em Google Style"
- **Docs**: Sphinx com `sphinx.ext.napoleon`
- **IDE**: VSCode Python extension mostra docstrings

#### Checklist de Conformidade

- [ ] Função pública tem docstring?
- [ ] Descrição breve presente?
- [ ] Todos os Args documentados com tipos?
- [ ] Returns documentado?
- [ ] Raises lista exceções possíveis?
- [ ] Example mostra uso prático?

---

### Rule 2.4: API Documentation OpenAPI 3.0

#### Contexto

APIs sem documentação causam:

- **Integração lenta**: 2-3 dias para entender endpoints
- **Erros de integração**: 60% dos bugs são de contrato de API
- **Suporte sobrecarregado**: Perguntas repetitivas sobre uso
- **Versionamento caótico**: Breaking changes sem aviso

#### Regra

Toda API REST DEVE ter documentação OpenAPI 3.0 com:

- Todos os endpoints documentados
- Request/Response schemas com exemplos
- Códigos de status HTTP explicados
- Autenticação e segurança documentadas
- Versionamento claro (v1, v2...)
- Geração automática via código (não manual)

#### Justificativa

- **Contract-First**: Contrato define implementação
- **Geração de código**: Clients gerados automaticamente
- **Testes automáticos**: Validação de contrato
- **Documentação interativa**: Swagger UI/Redoc

#### Exemplos

##### ✅ Correto (FastAPI)

```python
from fastapi import FastAPI, HTTPException
from pydantic import BaseModel, EmailStr, Field
from typing import List

app = FastAPI(
    title="API de Usuários",
    description="API para gestão de usuários com autenticação JWT",
    version="2.1.0",
    docs_url="/docs",
    redoc_url="/redoc"
)

class UserCreate(BaseModel):
    """Schema para criação de usuário."""
    email: EmailStr = Field(..., description="Email único do usuário")
    nome: str = Field(..., min_length=3, max_length=100, description="Nome completo")
    idade: int = Field(..., ge=18, le=120, description="Idade (18-120)")

    class Config:
        schema_extra = {
            "example": {
                "email": "usuario@example.com",
                "nome": "João Silva",
                "idade": 30
            }
        }

class User(BaseModel):
    """Schema de usuário retornado."""
    id: str = Field(..., description="UUID do usuário")
    email: EmailStr
    nome: str
    idade: int
    criado_em: str = Field(..., description="ISO 8601 timestamp")

@app.post(
    "/users/",
    response_model=User,
    status_code=201,
    summary="Criar novo usuário",
    description="Cria usuário com validação de email único e idade mínima",
    responses={
        201: {"description": "Usuário criado com sucesso"},
        400: {"description": "Dados inválidos"},
        409: {"description": "Email já cadastrado"}
    },
    tags=["Usuários"]
)
async def criar_usuario(user: UserCreate):
    """Cria novo usuário no sistema."""
    # Implementação...
```

##### ❌ Incorreto

```python
@app.post("/users/")
def criar_usuario(data: dict):
    """Cria usuário."""
    # Implementação...
```

**Problemas:**

- Sem schemas Pydantic (validação manual)
- Sem exemplos de request/response
- Sem documentação de status codes
- Sem descrições detalhadas
- Sem tags para organização

#### Exceções

- APIs internas temporárias (< 1 mês de vida)
- Protótipos em fase de descoberta
- Webhooks de terceiros (documentar externamente)

#### Ferramentas

- **FastAPI**: Geração automática OpenAPI 3.0
- **Flask**: `flask-openapi3` ou `flasgger`
- **Validação**: `openapi-spec-validator`
- **Testes**: `schemathesis` para testes baseados em schema
- **Docs**: Swagger UI (automático) ou Redoc

#### Checklist de Conformidade

- [ ] OpenAPI spec gerado automaticamente?
- [ ] Todos os endpoints documentados?
- [ ] Schemas com exemplos?
- [ ] Status codes explicados?
- [ ] Autenticação documentada?
- [ ] Versionamento claro?
- [ ] Swagger UI acessível?

---

### Rule 2.5: Changelog Seguindo Keep a Changelog

#### Contexto

Histórico de mudanças desorganizado causa:

- **Deploys arriscados**: Sem saber o que mudou
- **Rollbacks difíceis**: Não sabe para qual versão voltar
- **Comunicação falha**: Stakeholders sem visibilidade
- **Compliance**: Auditoria sem rastreabilidade

#### Regra

Todo projeto DEVE manter `CHANGELOG.md` seguindo [Keep a Changelog](https://keepachangelog.com/):

- Formato: `## [Versão] - YYYY-MM-DD`
- Categorias: Added, Changed, Deprecated, Removed, Fixed, Security
- Ordem: Mais recente primeiro
- Unreleased: Seção para mudanças não lançadas
- Links: Para comparação de versões no Git

#### Justificativa

- **Transparência**: Todos sabem o que mudou
- **Decisões informadas**: Deploy baseado em impacto
- **Comunicação**: Release notes automáticas
- **Versionamento semântico**: Facilita decisão de versão

#### Exemplos

##### ✅ Correto

```markdown
# Changelog

Todas as mudanças notáveis neste projeto serão documentadas neste arquivo.

O formato é baseado em [Keep a Changelog](https://keepachangelog.com/pt-BR/1.0.0/),
e este projeto adere ao [Semantic Versioning](https://semver.org/lang/pt-BR/).

## [Unreleased]

### Added
- Suporte a autenticação via OAuth2 Google
- Endpoint `/api/v2/users/export` para exportação CSV

### Changed
- Migração de PostgreSQL 13 para 15
- Timeout de sessão aumentado de 30min para 2h

## [2.1.0] - 2025-09-15

### Added
- Sistema de notificações push via Firebase
- Dashboard de métricas em tempo real
- Suporte a dark mode

### Changed
- Refatoração completa do módulo de autenticação
- Atualização Vue.js 3.3 → 3.4

### Fixed
- Correção de memory leak em WebSocket connections
- Bug de timezone em relatórios

### Security
- Patch de vulnerabilidade CVE-2025-1234 em dependência
- Implementação de rate limiting em endpoints públicos

## [2.0.0] - 2025-08-01

### Added
- Nova API GraphQL
- Sistema de cache com Redis

### Changed
- **BREAKING**: Migração de REST para GraphQL
- **BREAKING**: Autenticação agora requer MFA

### Removed
- **BREAKING**: Endpoints REST v1 depreciados

[Unreleased]: https://github.com/org/repo/compare/v2.1.0...HEAD
[2.1.0]: https://github.com/org/repo/compare/v2.0.0...v2.1.0
[2.0.0]: https://github.com/org/repo/releases/tag/v2.0.0
```

##### ❌ Incorreto

```markdown
# Mudanças

## Versão 2.1
- Várias melhorias
- Correções de bugs
- Atualizações de dependências

## Versão 2.0
- Nova versão com mudanças importantes
```

**Problemas:**

- Sem datas
- Sem categorização (Added, Fixed...)
- Descrições vagas
- Sem links para comparação
- Sem indicação de breaking changes

#### Exceções

- Projetos em fase alpha (< v0.1.0)
- Protótipos descartáveis
- Scripts internos one-off

#### Ferramentas

- **Geração**: `git-changelog` ou `conventional-changelog`
- **Validação**: `changelog-cli`
- **Automação**: GitHub Actions para atualização automática
- **Template**: `template-changelog.md`

#### Checklist de Conformidade

- [ ] CHANGELOG.md existe na raiz?
- [ ] Segue formato Keep a Changelog?
- [ ] Versões com datas?
- [ ] Categorias corretas (Added, Fixed...)?
- [ ] Breaking changes marcados?
- [ ] Links para comparação de versões?
- [ ] Seção [Unreleased] presente?

---

## 3. Templates e Uso

### 3.1 Templates Disponíveis

| Template | Arquivo | Quando Usar | Rule Relacionada |
|----------|---------|-------------|------------------|
| **README** | `template-readme.md` | Todo projeto | Rule 2.1 |
| **ADR** | `template-adr.md` | Decisões arquiteturais | Rule 2.2 |
| **API Docs** | `template-api-documentation.md` | APIs REST/GraphQL | Rule 2.4 |
| **Changelog** | `template-changelog.md` | Histórico de versões | Rule 2.5 |
| **Database Schema** | `template-database-schema-documentation.md` | Modelagem de dados | - |
| **Security Assessment** | `template-security-assessment.md` | Auditorias de segurança | - |

### 3.2 Uso com Amazon Q

#### Prompt Eficiente

```
Contexto: @workspace
Tarefa: Gerar README completo para este projeto
Restrições: Seguir datametria_std_documentation Rule 2.1
Formato: Markdown com badges, comandos executáveis e pré-requisitos
```

#### Geração de ADR

```
Contexto: Decidimos migrar de MongoDB para PostgreSQL
Motivos: Necessidade de transações ACID, joins complexos
Trade-offs: Perda de flexibilidade de schema, custo de migração
Tarefa: Criar ADR completo seguindo Rule 2.2
```

---

## 4. Automação e Ferramentas

### 4.1 Pre-commit Hooks

```yaml
# .pre-commit-config.yaml
repos:
  - repo: https://github.com/igorshubovych/markdownlint-cli
    rev: v0.37.0
    hooks:
      - id: markdownlint
        args: ['--fix', '--config', '.markdownlint.json']

  - repo: https://github.com/pycqa/pydocstyle
    rev: 6.3.0
    hooks:
      - id: pydocstyle
        args: ['--convention=google']
```

### 4.2 CI/CD Validation

```yaml
# .github/workflows/docs-quality.yml
name: Documentation Quality

on: [push, pull_request]

jobs:
  validate:
    runs-on: ubuntu-latest
    steps:
      - uses: actions/checkout@v3

      - name: Validate README exists
        run: test -f README.md

      - name: Lint Markdown
        uses: nosborn/github-action-markdown-cli@v3.3.0
        with:
          files: .
          config_file: .markdownlint.json

      - name: Validate OpenAPI
        run: |
          pip install openapi-spec-validator
          openapi-spec-validator openapi.yaml

      - name: Check Docstrings
        run: |
          pip install pydocstyle
          pydocstyle --convention=google src/
```

### 4.3 Ferramentas Recomendadas

| Ferramenta | Propósito | Comando |
|------------|-----------|---------|
| **markdownlint** | Validação de Markdown | `markdownlint **/*.md` |
| **pydocstyle** | Validação de docstrings | `pydocstyle --convention=google` |
| **openapi-spec-validator** | Validação OpenAPI | `openapi-spec-validator spec.yaml` |
| **changelog-cli** | Gestão de changelog | `changelog add "Nova feature"` |

---

## 5. Métricas de Conformidade

### 5.1 Objetivos de Qualidade

| Métrica | Meta | Medição |
|---------|------|---------|
| **README Coverage** | 100% dos projetos | `find . -name README.md \| wc -l` |
| **ADR por Decisão** | 100% decisões arquiteturais | Revisão manual trimestral |
| **Docstring Coverage** | ≥ 90% funções públicas | `interrogate -v src/` |
| **API Documentation** | 100% endpoints | OpenAPI spec completeness |
| **Changelog Atualizado** | < 7 dias de defasagem | Git log vs CHANGELOG.md |

### 5.2 Dashboard de Conformidade

```python
# scripts/check_docs_compliance.py
def check_compliance():
    """Verifica conformidade com rules de documentação."""
    results = {
        "readme_exists": os.path.exists("README.md"),
        "changelog_exists": os.path.exists("CHANGELOG.md"),
        "adr_count": len(glob.glob("docs/adr/*.md")),
        "docstring_coverage": get_docstring_coverage(),
        "openapi_valid": validate_openapi_spec()
    }

    score = calculate_score(results)
    print(f"Documentation Compliance Score: {score}%")
    return score >= 90  # Threshold de aprovação
```

### 5.3 Relatório Mensal

```markdown
# Relatório de Conformidade - Setembro 2025

## Resumo Executivo
- **Score Geral**: 94% (↑ 8% vs mês anterior)
- **Projetos Conformes**: 18/20 (90%)
- **Principais Gaps**: Docstrings em módulos legados

## Detalhamento

| Projeto | README | ADR | Docstrings | API Docs | Changelog | Score |
|---------|--------|-----|------------|----------|-----------|-------|
| auth-service | ✅ | ✅ | 95% | ✅ | ✅ | 98% |
| payment-api | ✅ | ✅ | 87% | ✅ | ✅ | 94% |
| legacy-crm | ✅ | ⚠️ | 45% | ❌ | ✅ | 62% |

## Ações Requeridas
1. **legacy-crm**: Criar ADRs para decisões históricas (2h)
2. **legacy-crm**: Aumentar docstring coverage para 80% (8h)
3. **legacy-crm**: Gerar OpenAPI spec (4h)
```

---

## Checklist de Implementação

### Para Novos Projetos

- [ ] Criar README.md usando template
- [ ] Configurar estrutura `docs/adr/`
- [ ] Configurar pre-commit hooks
- [ ] Adicionar CI/CD para validação de docs
- [ ] Configurar geração automática de API docs

### Para Projetos Existentes

- [ ] Auditar documentação atual
- [ ] Criar README se não existir
- [ ] Documentar decisões históricas em ADRs
- [ ] Adicionar docstrings em funções públicas
- [ ] Gerar/atualizar OpenAPI spec
- [ ] Criar/atualizar CHANGELOG.md

### Manutenção Contínua

- [ ] Atualizar CHANGELOG a cada release
- [ ] Criar ADR para cada decisão arquitetural
- [ ] Revisar docstrings em code reviews
- [ ] Validar API docs em CI/CD
- [ ] Gerar relatório mensal de conformidade

---

**Versão:** 2.0
**Próxima revisão:** 2026-01-19
**Mantido por:** Vander Loto - CTO DATAMETRIA
