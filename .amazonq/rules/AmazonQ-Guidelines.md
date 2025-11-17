# Guia de Boas Práticas: AmazonQ Developer + Claude Sonnet 4.5 no VSCode

> **Objetivo:** Este guia serve como referência tanto para desenvolvedores quanto para o próprio AmazonQ ao criar, avaliar e aplicar Rules eficientes em projetos de desenvolvimento.

---

## 1. Visão Geral

O **AmazonQ Developer** integrado ao **Claude Sonnet 4.5** representa um assistente de desenvolvimento de última geração que combina capacidades técnicas avançadas com memória contextual persistente. Esta ferramenta foi projetada para compreender projetos complexos através de uma janela de contexto expandida de aproximadamente **200.000 tokens**, o que equivale a cerca de **100.000 linhas de código**.

A integração permite que o assistente mantenha consciência não apenas do código em si, mas também da arquitetura, decisões técnicas, estilo de desenvolvimento e preferências da equipe através de um sistema chamado **Memory Bank**. Adicionalmente, o conceito de **Rules** em formato Markdown permite que desenvolvedores codifiquem padrões, convenções e melhores práticas que serão automaticamente aplicadas pelo assistente em todas as interações.

Esta combinação de contexto extenso, memória persistente e regras customizáveis torna o AmazonQ Developer particularmente eficaz para projetos de médio e grande porte, onde consistência, rastreabilidade de decisões e manutenção de padrões são críticos para o sucesso.

---

## 2. Arquitetura de Contexto

### 2.1 Memória de Conversação

O AmazonQ Developer mantém um contexto ativo de aproximadamente **200.000 tokens** durante cada sessão de conversa. Este contexto funciona como a memória de trabalho do assistente, permitindo que ele acompanhe discussões longas, refatorações complexas e múltiplos arquivos simultaneamente. É importante compreender que este contexto é **volátil** e será perdido ao fechar o IDE ou iniciar uma nova sessão, tornando essencial o uso de mecanismos de persistência como o Memory Bank para informações críticas.

### 2.2 Contexto de Workspace

O assistente possui capacidade de análise automática do workspace, incluindo:

- **Arquivos abertos no editor:** Todos os arquivos visíveis são automaticamente incluídos no contexto.
- **Estrutura de diretórios:** Compreensão da organização de pastas e módulos do projeto.
- **Arquivos de dependências:** Análise de `package.json`, `requirements.txt`, `pom.xml`, `Gemfile`, entre outros.
- **Configurações de ambiente:** Leitura de arquivos `.env`, `config.yaml`, `settings.json` e similares.

Esta análise automática permite que o AmazonQ compreenda o ecossistema completo do projeto sem necessidade de explicações manuais detalhadas.

### 2.3 Contexto Semântico

Além da análise sintática, o assistente realiza análise semântica profunda do código:

- **Rastreamento de dependências:** Identifica relações entre funções, classes e módulos através de imports e exports.
- **Padrões arquiteturais:** Reconhece automaticamente frameworks como React, Angular, Django, Spring Boot e suas convenções.
- **Fluxo de dados:** Compreende como informações fluem através da aplicação.

### 2.4 Limitações Técnicas

É fundamental compreender as limitações práticas para otimizar o uso:

| Limitação | Valor | Implicação |
|-----------|-------|------------|
| Persistência de sessão | Não persiste entre reinicializações | Use Memory Bank para continuidade |
| Tamanho máximo por arquivo | ~200KB | Arquivos maiores serão truncados |
| Tamanho total combinado | ~400KB | Limite para múltiplos arquivos simultâneos |
| Aplicação de Rules | Automática de `.amazonq/rules/` | Rules são sempre ativas implicitamente |

### 2.5 Comandos de Otimização de Contexto

O AmazonQ oferece comandos especiais para controle fino do contexto:

- **`@file <caminho>`**: Inclui explicitamente um arquivo específico no contexto.
- **`@folder <caminho>`**: Inclui todos os arquivos de um diretório.
- **`@workspace`**: Permite que o assistente selecione automaticamente arquivos relevantes baseado na tarefa.

**Exemplo de uso eficiente:**

```
@file src/components/UserProfile.tsx
@file src/hooks/useAuth.ts
Refatore o componente UserProfile para usar o novo hook de autenticação
```

---

## 3. Memory Bank: Persistência de Conhecimento

### 3.1 Conceito e Propósito

O **Memory Bank** é um sistema de arquivos estruturados que funciona como a memória de longo prazo do projeto. Enquanto o contexto de conversação é volátil, o Memory Bank persiste informações críticas que devem ser mantidas entre sessões, compartilhadas entre membros da equipe e consultadas pelo assistente em futuras interações.

### 3.2 Estrutura de Arquivos

O Memory Bank é composto por cinco arquivos principais, cada um com propósito específico:

| Arquivo | Propósito | Quando Atualizar |
|---------|-----------|------------------|
| `idea.md` | Visão do produto, objetivos e critérios de sucesso | Mudanças de escopo ou pivôs estratégicos |
| `vibe.md` | Estilo de colaboração, preferências e cultura da equipe | Novos padrões de comunicação ou workflow |
| `state.md` | Snapshot técnico atual: arquitetura, stack, status | Após mudanças significativas na estrutura |
| `decisions.md` | Registro cronológico de decisões arquiteturais (ADR) | Cada decisão técnica importante |
| `q-vibes-memory-banking.md` | Instruções para o AmazonQ sobre uso da memória | Raramente, apenas para ajustes de processo |

### 3.3 Exemplo de Estrutura de `decisions.md`

```markdown
# Decisões Arquiteturais

## [2024-01-15] Migração de REST para GraphQL

**Contexto:** API REST cresceu para 50+ endpoints com problemas de over-fetching.

**Decisão:** Migrar para GraphQL usando Apollo Server.

**Consequências:**
- ✅ Redução de chamadas de rede
- ✅ Melhor experiência de desenvolvimento com typings
- ⚠️ Curva de aprendizado para equipe
- ⚠️ Necessidade de cache layer (Apollo Client)

**Status:** Em progresso (40% concluído)
```

### 3.4 Benefícios Mensuráveis

- **Redução de repetição:** Até 60% menos tempo explicando contexto em novas sessões.
- **Onboarding acelerado:** Novos membros da equipe podem consultar decisões históricas.
- **Auditoria e compliance:** Rastreabilidade completa de decisões técnicas.
- **Prototipagem mais rápida:** Assistente já conhece preferências e restrições.

### 3.5 Quando Usar Memory Bank

**Use quando:**

- Projeto tem mais de 10.000 linhas de código
- Equipe com 3+ desenvolvedores
- Ciclo de desenvolvimento > 3 meses
- Necessidade de rastreabilidade de decisões

**Não use quando:**

- Protótipos descartáveis
- Scripts one-off
- Projetos com < 1 semana de duração

---

## 4. Rules: Codificando Padrões e Convenções

### 4.1 Princípios de Design de Rules

Rules eficientes seguem princípios fundamentais de clareza, especificidade e aplicabilidade:

**Princípio da Atomicidade:** Cada Rule deve abordar um único aspecto. Rules complexas devem ser divididas em múltiplas Rules atômicas.

**Princípio da Justificativa:** Sempre explique o **porquê** da regra, não apenas o **como**. Isso permite que o assistente tome decisões contextuais inteligentes.

**Princípio da Exemplificação:** Forneça exemplos positivos (✅) e negativos (❌) para eliminar ambiguidade.

**Princípio da Versionabilidade:** Rules devem ser versionadas no git junto com o código, permitindo rastreamento de mudanças.

### 4.2 Anatomia de uma Rule Eficiente

```markdown
# [Categoria]: [Nome da Rule]

## Contexto
[Explique o problema ou necessidade que esta rule resolve]

## Regra
[Declaração clara e específica da regra]

## Justificativa
[Por que esta regra existe - benefícios, problemas evitados]

## Exemplos

### ✅ Correto
[Código ou padrão que segue a regra]

### ❌ Incorreto
[Código ou padrão que viola a regra]

## Exceções
[Casos onde a regra não se aplica, se houver]

## Ferramentas
[Linters, formatters ou ferramentas que ajudam a enforçar]
```

### 4.3 Template de Rule Completo

```markdown
# Tipagem: Type Hints Obrigatórios em Python

## Contexto
Projetos Python sem tipagem explícita sofrem com:
- Bugs em tempo de execução que poderiam ser detectados estaticamente
- Dificuldade de manutenção e refatoração
- Perda de autocomplete e intellisense

## Regra
Todas as funções e métodos públicos devem incluir type hints para:
- Todos os parâmetros
- Valor de retorno
- Variáveis de instância em classes

## Justificativa
Type hints permitem:
- Detecção precoce de erros com mypy
- Melhor documentação auto-gerada
- Refatoração segura com ferramentas como PyCharm
- Redução de 40% em bugs de tipo (baseado em estudo interno)

## Exemplos

### ✅ Correto
```python
from typing import List, Optional

def processar_usuarios(
    usuarios: List[dict],
    filtro: Optional[str] = None
) -> List[dict]:
    """Processa lista de usuários aplicando filtro opcional."""
    if filtro:
        return [u for u in usuarios if filtro in u.get('nome', '')]
    return usuarios
```

### ❌ Incorreto

```python
def processar_usuarios(usuarios, filtro=None):
    if filtro:
        return [u for u in usuarios if filtro in u.get('nome', '')]
    return usuarios
```

## Exceções

- Scripts descartáveis em `scripts/temp/`
- Testes que usam mocks dinâmicos onde tipagem é impraticável
- Código legado em `legacy/` (até migração planejada)

## Ferramentas

- **mypy**: Verificação estática de tipos
- **pyright**: Type checker alternativo mais rápido
- **VSCode**: Configurar `python.analysis.typeCheckingMode: "strict"`

## Checklist de Conformidade

- [ ] Função tem type hints em todos os parâmetros?
- [ ] Função tem type hint de retorno?
- [ ] Imports de typing estão presentes?
- [ ] mypy passa sem erros?

```

### 4.4 Organização de Rules por Categoria

Organize Rules em arquivos separados por domínio:

```

.amazonq/
└── rules/
    ├── code-style.md          # Formatação, naming conventions
    ├── architecture.md        # Padrões arquiteturais, estrutura de pastas
    ├── security.md            # Práticas de segurança
    ├── testing.md             # Estratégias de teste
    ├── performance.md         # Otimizações e benchmarks
    ├── documentation.md       # Padrões de documentação
    └── frameworks/
        ├── react.md           # Rules específicas de React
        ├── django.md          # Rules específicas de Django
        └── fastapi.md         # Rules específicas de FastAPI

```

### 4.5 Anti-Patterns em Rules

**❌ Rule Vaga:**
```markdown
# Escreva código limpo
- Use boas práticas
- Evite código ruim
```

**✅ Rule Específica:**

```markdown
# Funções: Máximo de 3 Parâmetros

## Regra
Funções não devem ter mais de 3 parâmetros. Se precisar de mais, use:
- Objeto de configuração
- Builder pattern
- Dataclass (Python) / Interface (TypeScript)

## Justificativa
Funções com muitos parâmetros:
- São difíceis de testar (explosão combinatória)
- Têm alta probabilidade de erros de ordem
- Indicam violação de Single Responsibility Principle
```

**❌ Rule Sem Contexto:**

```markdown
# Use async/await
Sempre use async/await em vez de callbacks.
```

**✅ Rule Com Contexto:**

```markdown
# Assincronicidade: Preferir async/await sobre Callbacks

## Contexto
Callbacks aninhados (callback hell) tornam código difícil de:
- Ler e entender fluxo
- Tratar erros consistentemente
- Debugar com stack traces

## Regra
Para operações assíncronas em JavaScript/TypeScript, use async/await.

## Exceções
- Event listeners (onClick, addEventListener)
- Streams do Node.js (onde callbacks são idiomáticos)
- APIs legadas que não retornam Promises
```

### 4.6 Checklist de Qualidade para Rules

Use este checklist ao criar ou revisar Rules:

- [ ] **Clareza**: A regra é compreensível sem ambiguidade?
- [ ] **Especificidade**: Fornece critérios objetivos de conformidade?
- [ ] **Justificativa**: Explica o "porquê", não apenas o "como"?
- [ ] **Exemplos**: Inclui casos positivos E negativos?
- [ ] **Exceções**: Documenta casos onde não se aplica?
- [ ] **Ferramentas**: Lista ferramentas de automação quando aplicável?
- [ ] **Testabilidade**: É possível verificar conformidade automaticamente?
- [ ] **Versionamento**: Está no git com histórico de mudanças?

### 4.7 Templates de Rules por Linguagem/Framework

#### Python + FastAPI

```markdown
# FastAPI: Validação com Pydantic Models

## Regra
Endpoints devem usar Pydantic models para validação de entrada/saída.

## Exemplo
```python
from pydantic import BaseModel, EmailStr, validator

class UserCreate(BaseModel):
    email: EmailStr
    nome: str
    idade: int

    @validator('idade')
    def idade_valida(cls, v):
        if v < 18:
            raise ValueError('Usuário deve ser maior de idade')
        return v

@app.post("/users/", response_model=User)
async def criar_usuario(user: UserCreate):
    return await db.users.create(**user.dict())
```

## Ferramentas

- Validação automática de tipos
- Documentação OpenAPI gerada automaticamente
- Serialização/deserialização sem código manual

```

#### React + TypeScript

```markdown
# React: Props com TypeScript Interfaces

## Regra
Componentes React devem definir props usando interfaces TypeScript.

## Exemplo
```typescript
interface UserCardProps {
  user: {
    id: string;
    nome: string;
    avatar?: string;
  };
  onEdit?: (userId: string) => void;
  variant?: 'compact' | 'detailed';
}

export const UserCard: React.FC<UserCardProps> = ({
  user,
  onEdit,
  variant = 'compact'
}) => {
  // implementação
};
```

## Benefícios

- Autocomplete completo de props
- Erros em tempo de desenvolvimento
- Refatoração segura

```

### 4.8 Debugging e Iteração de Rules

Se uma Rule não está sendo aplicada corretamente:

**1. Verifique a localização:**
```bash
# Rules devem estar em:
.amazonq/rules/*.md
```

**2. Valide a sintaxe Markdown:**

```bash
# Use um linter de Markdown
npx markdownlint .amazonq/rules/
```

**3. Teste a clareza:**

- Peça para o AmazonQ explicar a regra com suas próprias palavras
- Forneça um exemplo de código e pergunte se está conforme

**4. Itere com exemplos:**

- Adicione mais exemplos positivos/negativos
- Documente edge cases encontrados

**5. Monitore conformidade:**

```markdown
# Adicione seção de métricas na Rule
## Métricas de Conformidade
- Meta: 95% de funções com type hints
- Atual: 78% (verificado em 2024-01-15)
- Comando: `mypy --strict src/ | grep "Function is missing"`
```

---

## 5. Estratégias de Uso Eficiente

### 5.1 Fluxo Orientado a Testes (TDD com AmazonQ)

Ao solicitar refatorações ou novas funcionalidades, forneça testes primeiro:

```
Tenho os seguintes testes que devem passar:

@file tests/test_auth.py

Refatore o módulo de autenticação para suportar OAuth2.
```

Este approach garante que o assistente compreenda exatamente o comportamento esperado.

### 5.2 Planejamento com Checkpoints

Para tarefas grandes, divida em etapas explícitas:

```
Vamos migrar o banco de dados de SQLite para PostgreSQL em 4 etapas:

1. Criar models do SQLAlchemy compatíveis com ambos
2. Implementar camada de abstração de database
3. Script de migração de dados
4. Atualizar testes e configurações

Vamos começar pela etapa 1. Confirme o plano antes de prosseguir.
```

### 5.3 Integração com CLI

Configure o modelo padrão para usar Claude Sonnet 4.5:

```bash
# Definir modelo padrão
q settings chat.defaultModel claude-4-sonnet

# Iniciar chat com modelo específico
q chat --model claude-4-sonnet

# Usar em scripts
echo "Analise este log de erro" | q chat --stdin
```

### 5.4 Documentação de Decisões em Tempo Real

Durante discussões técnicas, peça ao AmazonQ para documentar:

```
Acabamos de decidir usar Redis para cache de sessões.
Adicione esta decisão ao decisions.md seguindo o formato ADR.
```

### 5.5 Uso de Contexto Progressivo

Para análises profundas, construa contexto progressivamente:

```
# Passo 1: Visão geral
@workspace
Analise a arquitetura geral do projeto

# Passo 2: Foco específico
@folder src/services/payment
Explique o fluxo de pagamento em detalhes

# Passo 3: Investigação profunda
@file src/services/payment/stripe_integration.py
Por que estamos tendo timeouts nesta integração?
```

---

## 6. Referência Rápida

### 6.1 Equivalência de Tokens

| Tokens | Linhas de Código (aprox.) | Arquivos Médios (20KB cada) |
|--------|---------------------------|------------------------------|
| 10k | 5.000 linhas | ~2 arquivos |
| 50k | 25.000 linhas | ~10 arquivos |
| 100k | 50.000 linhas | ~20 arquivos |
| 200k | 100.000–110.000 linhas | ~40 arquivos |

### 6.2 Comandos Essenciais

| Comando | Uso | Exemplo |
|---------|-----|---------|
| `@file` | Incluir arquivo específico | `@file src/main.py` |
| `@folder` | Incluir diretório | `@folder src/components` |
| `@workspace` | Seleção automática inteligente | `@workspace analise a arquitetura` |
| `q chat` | CLI interativo | `q chat --model claude-4-sonnet` |
| `q settings` | Configurações | `q settings chat.defaultModel` |

### 6.3 Estrutura de Diretórios Recomendada

```
projeto/
├── .amazonq/
│   ├── rules/
│   │   ├── code-style.md
│   │   ├── architecture.md
│   │   ├── security.md
│   │   └── testing.md
│   └── memory/
│       ├── idea.md
│       ├── vibe.md
│       ├── state.md
│       ├── decisions.md
│       └── q-vibes-memory-banking.md
├── src/
├── tests/
└── docs/
```

---

## 7. Casos de Uso Práticos

### 7.1 Onboarding de Novo Desenvolvedor

**Antes:**

```
Desenvolvedor: "Como funciona o sistema de autenticação?"
Senior: [30 minutos de explicação verbal]
```

**Depois:**

```
Desenvolvedor: "@workspace explique o fluxo de autenticação"
AmazonQ: [Análise baseada em código + decisions.md + architecture.md]
```

### 7.2 Refatoração de Código Legado

**Antes:**

```
"Refatore este arquivo para seguir padrões modernos"
[Resultado inconsistente com resto do projeto]
```

**Depois:**

```
@file src/legacy/user_manager.py
Refatore seguindo nossas rules em .amazonq/rules/
Use patterns do state.md como referência
```

### 7.3 Code Review Automatizado

**Criação de Rule para CI/CD:**

```markdown
# CI: Verificação de Conformidade com Rules

## Script de Verificação
```bash
#!/bin/bash
# .github/workflows/check-rules.sh

# Verifica type hints
mypy src/ --strict

# Verifica formatação
black --check src/

# Verifica complexidade
radon cc src/ -a -nb

# Verifica security
bandit -r src/
```

## Integração com GitHub Actions

```yaml
name: Rules Compliance
on: [pull_request]
jobs:
  check:
    runs-on: ubuntu-latest
    steps:
      - uses: actions/checkout@v2
      - name: Check Rules
        run: .github/workflows/check-rules.sh
```

```

---

## 8. Prompts Eficientes para o AmazonQ

### 8.1 Estrutura de Prompt Ideal

Um prompt eficiente para o AmazonQ segue esta estrutura:

```

[Contexto] + [Tarefa] + [Restrições] + [Formato de Saída]

```

**Exemplo:**
```

Contexto: @file src/api/users.py
Tarefa: Adicionar endpoint para reset de senha
Restrições: Seguir rules em .amazonq/rules/security.md, usar tokens JWT
Formato: Código completo + testes unitários

```

### 8.2 Exemplos de Prompts por Categoria

#### Análise de Código

**❌ Vago:**
```

Analise este código

```

**✅ Específico:**
```

@file src/services/payment.py
Analise este módulo focando em:

1. Possíveis race conditions
2. Tratamento de erros de API externa
3. Conformidade com PCI-DSS
Forneça sugestões específicas com exemplos de código

```

#### Refatoração

**❌ Vago:**
```

Melhore este código

```

**✅ Específico:**
```

@file src/legacy/report_generator.py
Refatore este módulo para:

1. Separar lógica de negócio de formatação
2. Adicionar type hints (conforme .amazonq/rules/code-style.md)
3. Implementar padrão Strategy para diferentes formatos de saída
Mantenha compatibilidade com testes existentes em tests/test_reports.py

```

#### Debugging

**❌ Vago:**
```

Por que isso não funciona?

```

**✅ Específico:**
```

@file src/api/orders.py (linhas 45-67)
@file logs/error.log (últimas 50 linhas)
Estou recebendo timeout intermitente ao processar pedidos grandes (>100 itens).
O erro ocorre apenas em produção, não em staging.
Analise possíveis causas relacionadas a:

1. Configuração de pool de conexões
2. Limites de memória
3. Locks de banco de dados

```

#### Geração de Testes

**❌ Vago:**
```

Crie testes para isso

```

**✅ Específico:**
```

@file src/utils/validators.py
Gere testes unitários completos para todas as funções, incluindo:

1. Casos válidos (happy path)
2. Casos inválidos (edge cases)
3. Casos de erro (exceções esperadas)
Use pytest e siga padrão AAA (Arrange, Act, Assert)
Cobertura mínima: 95%

```

### 8.3 Uso de Contexto Incremental

Para tarefas complexas, construa contexto em camadas:

**Camada 1 - Visão Geral:**
```

@workspace
Descreva a arquitetura geral do sistema de notificações

```

**Camada 2 - Componente Específico:**
```

@folder src/notifications/
Explique como funciona o sistema de templates de email

```

**Camada 3 - Implementação Detalhada:**
```

@file src/notifications/email_service.py
@file src/notifications/templates/welcome.html
Como posso adicionar suporte a variáveis dinâmicas nos templates?

```

### 8.4 Solicitação de Documentação

**Para gerar documentação de código:**
```

@file src/core/authentication.py
Gere documentação completa em formato Google Docstring incluindo:

1. Descrição de cada classe e método
2. Parâmetros com tipos
3. Valores de retorno
4. Exceções possíveis
5. Exemplos de uso

```

**Para gerar ADR (Architecture Decision Record):**
```

Acabamos de decidir migrar de MongoDB para PostgreSQL.
Motivos: necessidade de transações ACID, joins complexos, melhor suporte a analytics.
Trade-offs: perda de flexibilidade de schema, necessidade de migrações.

Crie um ADR completo em decisions.md seguindo formato padrão.

```

---

## 9. Resumo Executivo

O **AmazonQ Developer** com **Claude Sonnet 4.5** representa uma evolução significativa em assistentes de desenvolvimento ao combinar:

**Contexto Extenso:** Janela de 200.000 tokens permite compreensão de projetos complexos sem perda de informação.

**Memória Persistente:** Memory Bank garante continuidade entre sessões e compartilhamento de conhecimento na equipe.

**Rules Customizáveis:** Sistema de regras em Markdown permite codificar padrões específicos do projeto que são automaticamente aplicados.

**Integração Profunda:** Comandos `@file`, `@folder` e `@workspace` permitem controle fino do contexto, enquanto CLI possibilita automação.

**Aplicabilidade:** Ideal para projetos com mais de 10.000 linhas de código, equipes de 3+ desenvolvedores e ciclos de desenvolvimento superiores a 3 meses.

Seguindo as práticas documentadas neste guia, equipes podem esperar:
- **60% de redução** em tempo gasto explicando contexto
- **40% menos bugs** relacionados a padrões e convenções
- **Onboarding 3x mais rápido** de novos desenvolvedores
- **Rastreabilidade completa** de decisões arquiteturais

---

## 10. Checklist de Implementação

### Para Novos Projetos

- [ ] Criar estrutura `.amazonq/rules/` com rules básicas
- [ ] Inicializar Memory Bank com `idea.md` e `vibe.md`
- [ ] Configurar CLI com `q settings chat.defaultModel claude-4-sonnet`
- [ ] Definir rules de code-style específicas da stack
- [ ] Adicionar rules de security e testing
- [ ] Documentar decisões arquiteturais iniciais em `decisions.md`
- [ ] Configurar CI/CD para validar conformidade com rules

### Para Projetos Existentes

- [ ] Auditar código existente para identificar padrões
- [ ] Criar rules baseadas em padrões identificados
- [ ] Documentar decisões históricas em `decisions.md`
- [ ] Atualizar `state.md` com arquitetura atual
- [ ] Definir `vibe.md` com preferências da equipe
- [ ] Migrar documentação existente para Memory Bank
- [ ] Treinar equipe no uso de comandos `@file`, `@folder`, `@workspace`

### Para Manutenção Contínua

- [ ] Revisar e atualizar rules mensalmente
- [ ] Adicionar novas decisões em `decisions.md` imediatamente
- [ ] Atualizar `state.md` após mudanças arquiteturais
- [ ] Monitorar conformidade com rules via CI/CD
- [ ] Coletar feedback da equipe sobre eficácia das rules
- [ ] Refinar prompts baseado em resultados obtidos

---

## 11. Recursos Adicionais

### 11.1 Documentação Oficial
- [Amazon Q Developer Documentation](https://docs.aws.amazon.com/amazonq/)
- [Claude Sonnet 4.5 Model Card](https://www.anthropic.com/claude)

### 11.2 Templates e Exemplos
- Repositório de Rules templates: `.amazonq/rules/`
- Exemplos de Memory Bank: `.amazonq/memory/`

### 11.3 Comunidade
- GitHub Discussions: Compartilhe suas Rules e aprenda com outros
- Stack Overflow tag: `amazon-q-developer`

---

**Versão:** 2.0
**Última atualização:** 2024-01-19
**Mantido por:** Equipe de Desenvolvimento
