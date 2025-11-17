# Template de Guia de Markdown Linting

<div align="center">

## Guia Completo para Prevenção de Erros Markdown - Framework Completo de Linting

[![Markdown](https://img.shields.io/badge/markdown-linting-blue)](https://github.com/DavidAnson/markdownlint)
[![MD047](https://img.shields.io/badge/MD047-prevention-green)](https://github.com/DavidAnson/markdownlint/blob/main/doc/Rules.md#md047)
[![MD045](https://img.shields.io/badge/MD045-prevention-orange)](https://github.com/DavidAnson/markdownlint/blob/main/doc/Rules.md#md045)
[![MD040](https://img.shields.io/badge/MD040-prevention-red)](https://github.com/DavidAnson/markdownlint/blob/main/doc/Rules.md#md040)
[![MD036](https://img.shields.io/badge/MD036-prevention-purple)](https://github.com/DavidAnson/markdownlint/blob/main/doc/Rules.md#md036)
[![MD051](https://img.shields.io/badge/MD051-prevention-yellow)](https://github.com/DavidAnson/markdownlint/blob/main/doc/Rules.md#md051)
[![MD024](https://img.shields.io/badge/MD024-prevention-pink)](https://github.com/DavidAnson/markdownlint/blob/main/doc/Rules.md#md024)
[![DATAMETRIA](https://img.shields.io/badge/DATAMETRIA-Standards-blue)](https://github.com/datametria/standards)
[![Amazon Q](https://img.shields.io/badge/Amazon%20Q-Ready-yellow)](https://aws.amazon.com/q/)

[🔗 Template Original](link) • [🔗 Diretrizes](link) • [🔗 Exemplos](link)

[🔧 Configuração](#-configuração) • [📋 Regras](#-regras-principais) • [🛠️ Ferramentas](#️-ferramentas) •
[🚀 Automação](#-automação)

</div>

---

## 📋 Índice

- [🎯 Visão Geral](#-visão-geral)
- [📋 Informações do Projeto](#-informações-do-projeto)
- [🔧 Configuração](#-configuração)
- [📋 Regras Principais](#-regras-principais)
- [🛠️ Ferramentas](#️-ferramentas)
- [🚀 Automação](#-automação)
- [🔍 Verificação e Debugging](#-verificação-e-debugging)
- [📚 Referências](#-referências)

---

## 🎯 Visão Geral

### Framework Completo de Markdown Linting

#### 🏁 Regras de Alta Prioridade

- **MD001**: Hierarquia de cabeçalhos (incremento de 1 nível)
- **MD009**: Espaços no final das linhas
- **MD034**: URLs sem formatação adequada
- **MD047**: Arquivo deve terminar com quebra de linha
- **MD045**: Imagens devem ter texto alternativo

#### 🟡 Regras de Média Prioridade

- **MD003**: Estilo consistente de cabeçalhos
- **MD022**: Cabeçalhos cercados por linhas em branco
- **MD025**: Múltiplos cabeçalhos de nível superior
- **MD004**: Estilo consistente de listas
- **MD007**: Indentação de listas
- **MD040**: Linguagem em blocos de código

#### 🟢 Regras de Baixa Prioridade

- **MD010**: Tabs rígidos
- **MD012**: Múltiplas linhas em branco consecutivas
- **MD030**: Espaços após marcadores de lista
- **MD036**: Ênfase no lugar de cabeçalhos
- **MD039**: Espaços dentro de links
- **MD042**: Links vazios

#### Benefícios do Framework

- ✅ **Estruturação**: Hierarquia clara e consistente (MD001, MD003, MD022)
- ✅ **Acessibilidade**: Melhor experiência para todos os usuários (MD045)
- ✅ **Legibilidade**: Código com syntax highlighting (MD040)
- ✅ **Navegação**: Links funcionais e válidos (MD034, MD039, MD042, MD051)
- ✅ **Qualidade**: Formatação limpa e profissional (MD009, MD010, MD012)
- ✅ **Consistência**: Padrões uniformes em todo projeto (MD004, MD007, MD030)
- ✅ **Compatibilidade**: Padrões Git e POSIX (MD047)
- ✅ **SEO**: Melhor indexação e descoberta (MD045, MD025)

### Problemas Comuns

```bash
# ❌ Arquivo sem quebra de linha no final
echo -n "# Título" > arquivo.md

# ❌ Arquivo com múltiplas quebras de linha
echo -e "# Título\n\n\n" > arquivo.md

# ✅ Arquivo correto com uma quebra de linha
echo "# Título" > arquivo.md
```

---

## 📋 Informações do Projeto

| Campo | Descrição | Exemplo |
|-------|-----------|----------|
| **Nome do Projeto** | [Nome do projeto] | Sistema de Documentação DATAMETRIA |
| **Versão Markdownlint** | v0.37.0+ | v0.37.0 |
| **Configuração** | .markdownlint.json | Arquivo de configuração DATAMETRIA |
| **Regras Implementadas** | 22+ regras ativas | MD001-MD051 (selecionadas) |
| **Responsável** | [Nome do responsável] | Vander Loto (CTO) |
| **Score de Qualidade** | [Percentual] | 95% (objetivo: >90%) |
| **Arquivos Verificados** | [Quantidade] | 68 arquivos .md |

---

## 🔧 Configuração

### .markdownlint.json

```json
{
  "default": true,
  "MD001": true,
  "MD003": { "style": "atx" },
  "MD004": { "style": "dash" },
  "MD007": { "indent": 2 },
  "MD009": true,
  "MD010": true,
  "MD012": { "maximum": 1 },
  "MD013": {
    "line_length": 120,
    "code_blocks": false,
    "tables": false,
    "headings": false
  },
  "MD022": true,
  "MD024": true,
  "MD025": true,
  "MD030": { "ul_single": 1, "ul_multi": 1 },
  "MD033": {
    "allowed_elements": ["div", "img", "br", "details", "summary"]
  },
  "MD034": true,
  "MD036": true,
  "MD039": true,
  "MD040": true,
  "MD041": false,
  "MD042": true,
  "MD045": true,
  "MD047": true,
  "MD051": true
}
```

### Instalação de Ferramentas

#### Node.js/npm

```bash
# Instalar markdownlint-cli globalmente
npm install -g markdownlint-cli

# Ou localmente no projeto
npm install --save-dev markdownlint-cli

# Verificar instalação
markdownlint --version
```

#### Python

```bash
# Instalar via pip
pip install markdownlint-cli

# Ou usando conda
conda install -c conda-forge markdownlint-cli
```

#### VS Code Extension

```json
{
  "recommendations": [
    "DavidAnson.vscode-markdownlint"
  ]
}
```

---

## 📋 Regras Principais

### MD001 - Heading Levels Increment

#### Descrição

Cabeçalhos devem incrementar apenas um nível por vez.

#### Exemplos

#### ❌ Incorreto:

```markdown
# Título Principal
### Subseção (pula H2)
```

#### ✅ Correto:

```markdown
# Título Principal
## Seção
### Subseção
```

### MD003 - Heading Style Consistency

#### Descrição

Usar estilo consistente de cabeçalhos (ATX: #).

#### Exemplos

#### ❌ Incorreto:

```markdown
# Título ATX

Título Setext
===============
```

#### ✅ Correto:

```markdown
# Título Principal
## Seção Secundária
```

### MD004 - Unordered List Style

#### Descrição

Usar estilo consistente para listas (-,*,+).

#### Exemplos

#### ❌ Incorreto:

```markdown
- Item 1
* Item 2
+ Item 3
```

#### ✅ Correto:

```markdown
- Item 1
- Item 2
- Item 3
```

### MD007 - Unordered List Indentation

#### Descrição

Indentação consistente em listas (2 espaços).

#### Exemplos

#### ❌ Incorreto:

```markdown
- Item 1
    - Subitem (4 espaços)
```

#### ✅ Correto:

```markdown
- Item 1
  - Subitem (2 espaços)
```

### MD009 - Trailing Spaces

#### Descrição

Remover espaços no final das linhas.

#### Como Corrigir

```bash
# Remover espaços com sed
sed -i 's/[[:space:]]*$//' arquivo.md

# Ou usar markdownlint
markdownlint --fix arquivo.md
```

### MD010 - Hard Tabs

#### Descrição

Usar espaços em vez de tabs.

#### Como Corrigir

```bash
# Converter tabs para espaços
expand -t 2 arquivo.md > arquivo_fixed.md
```

### MD012 - Multiple Consecutive Blank Lines

#### Descrição

Máximo de 1 linha em branco consecutiva.

#### Exemplos

#### ❌ Incorreto:

```markdown
# Título



Texto
```

#### ✅ Correto:

```markdown
# Título

Texto
```

### MD022 - Headings Surrounded by Blank Lines

#### Descrição

Cabeçalhos devem ter linhas em branco antes e depois.

#### Exemplos

#### ❌ Incorreto:

```markdown
Texto anterior
## Cabeçalho
Texto posterior
```

#### ✅ Correto:

```markdown
Texto anterior

## Cabeçalho

Texto posterior
```

### MD025 - Multiple Top Level Headings

#### Descrição

Apenas um cabeçalho H1 por documento.

#### Exemplos

#### ❌ Incorreto:

```markdown
# Primeiro Título
# Segundo Título
```

#### ✅ Correto:

```markdown
# Título Principal
## Seção 1
## Seção 2
```

### MD030 - Spaces After List Markers

#### Descrição

Um espaço após marcadores de lista.

#### Exemplos

#### ❌ Incorreto:

```markdown
-Item sem espaço
-  Item com 2 espaços
```

#### ✅ Correto:

```markdown
- Item com 1 espaço
- Outro item
```

### MD034 - Bare URL Used

#### Descrição

Usar formatação adequada para URLs.

#### Exemplos

#### ❌ Incorreto:

```markdown
Visite https://example.com para mais informações.
```

#### ✅ Correto:

```markdown
Visite [nosso site](https://example.com) para mais informações.
```

### MD039 - Spaces Inside Link Text

#### Descrição

Evitar espaços dentro do texto do link.

#### Exemplos

#### ❌ Incorreto:

```markdown
[ link com espaços ](https://example.com)
```

#### ✅ Correto:

```markdown
[link sem espaços](https://example.com)
```

### MD042 - No Empty Links

#### Descrição

Evitar links vazios.

#### Exemplos

#### ❌ Incorreto:

```markdown
[texto do link]()
```

#### ✅ Correto:

```markdown
[texto do link](https://example.com)
```

### MD047 - End of File Newline

#### Descrição

Arquivos devem terminar com exatamente uma quebra de linha.

#### Exemplos

#### ❌ Incorreto:

```markdown
# Título

Conteúdo sem quebra de linha no final[EOF]
```

#### ❌ Incorreto:

```markdown
# Título

Conteúdo com múltiplas quebras de linha


[EOF]
```

#### ✅ Correto:

```markdown
# Título

Conteúdo com uma quebra de linha no final
[EOF]
```

#### Como Corrigir

```bash
# Método 1: Usando sed (Unix/Linux/macOS)
sed -i -e '$a\' arquivo.md

# Método 2: Usando markdownlint
markdownlint --fix arquivo.md

# Método 3: Usando echo
echo >> arquivo.md && sed -i '$ { /^$/d; }' arquivo.md

# Método 4: Script Python
python -c "
import sys
with open(sys.argv[1], 'r+') as f:
    content = f.read().rstrip('\n') + '\n'
    f.seek(0)
    f.write(content)
    f.truncate()
" arquivo.md
```

### MD013 - Line Length

#### Configuração

```json
{
  "MD013": {
    "line_length": 120,
    "code_blocks": false,
    "tables": false,
    "headings": false
  }
}
```

#### Como Corrigir

```markdown
<!-- ❌ Linha muito longa -->
Esta é uma linha muito longa que excede o limite de 120 caracteres e precisa ser quebrada em múltiplas linhas para melhor legibilidade.

<!-- ✅ Linha corrigida -->
Esta é uma linha muito longa que excede o limite de 120 caracteres e precisa ser quebrada
em múltiplas linhas para melhor legibilidade.
```

### MD033 - Inline HTML

#### Configuração

```json
{
  "MD033": {
    "allowed_elements": ["div", "img", "br", "details", "summary"]
  }
}
```

#### Exemplos

```markdown
<!-- ✅ HTML permitido -->
<div align="center">
  <img src="logo.png" alt="Logo">
</div>

<details>
<summary>Clique para expandir</summary>
Conteúdo oculto
</details>

<!-- ❌ HTML não permitido -->
<span style="color: red;">Texto vermelho</span>
<script>alert('Não permitido')</script>
```

### MD024 - Multiple Headings Same Content

#### Descrição

Evita cabeçalhos duplicados no mesmo documento.

#### Exemplos

#### ❌ Incorreto:

```markdown
# Introdução
Texto...

# Introdução
Mais texto...
```

#### ✅ Correto:

```markdown
# Introdução
Texto...

# Conclusão
Mais texto...
```

### MD036 - Emphasis Instead of Heading

#### Descrição

Evita uso de ênfase (negrito/itálico) no lugar de cabeçalhos.

#### Exemplos

#### ❌ Incorreto:

```markdown
**Seção Importante**

Texto da seção...
```

#### ✅ Correto:

```markdown
## Seção Importante

Texto da seção...
```

### MD040 - Fenced Code Blocks Language

#### Descrição

Blocos de código devem ter linguagem especificada para syntax highlighting.

#### Exemplos

#### ❌ Incorreto:

```
function hello() {
    console.log("Hello World");
}
```

#### ✅ Correto:

```javascript
function hello() {
    console.log("Hello World");
}
```

#### Como Corrigir

```markdown
<!-- ❌ Evitar blocos sem linguagem -->
```

const data = { name: "test" };

```

<!-- ✅ Sempre especificar linguagem -->
```json
{
  "name": "test"
}
```

<!-- Linguagens comuns -->
```bash
echo "Hello World"
```

```python
print("Hello World")
```

```yaml
name: example
version: 1.0.0
```

```

### MD051 - Link Fragments Valid

#### Descrição

Garante que fragmentos de links (#section) sejam válidos.

#### Exemplos

#### ❌ Incorreto:

```markdown
[Ver seção](#secao-inexistente)
```

#### ✅ Correto:

```markdown
## Minha Seção

[Ver seção](#minha-seção)
```

### MD045 - Images Alt Text

#### Descrição

Todas as imagens devem ter texto alternativo para acessibilidade.

#### Exemplos

#### ❌ Incorreto:

```markdown
![](logo.png)
<img src="banner.jpg">
```

#### ✅ Correto:

```markdown
![Logo da empresa](logo.png)
<img src="banner.jpg" alt="Banner promocional">
```

#### Como Corrigir

```markdown
<!-- Sempre adicionar alt text descritivo -->
![Descrição clara da imagem](caminho/para/imagem.png)

<!-- Para imagens decorativas, usar alt vazio -->
![](decoracao.png) <!-- ❌ Evitar -->
<img src="decoracao.png" alt=""> <!-- ✅ Melhor -->
```

### MD041 - First Line Heading

#### Configuração

```json
{
  "MD041": false
}
```

**Nota**: Desabilitado nos templates DATAMETRIA pois usamos badges e divs no início.

---

## 🛠️ Ferramentas

### Linha de Comando

#### Verificação Básica

```bash
# Verificar um arquivo
markdownlint arquivo.md

# Verificar múltiplos arquivos
markdownlint *.md

# Verificar recursivamente
markdownlint **/*.md

# Usar configuração específica
markdownlint --config .markdownlint.json **/*.md
```

#### Correção Automática

```bash
# Corrigir automaticamente
markdownlint --fix **/*.md

# Corrigir com configuração
markdownlint --fix --config .markdownlint.json **/*.md

# Corrigir apenas regras específicas
markdownlint --fix --rules MD047,MD009,MD010 **/*.md
```

#### Verificação de Regras Específicas

```bash
# Verificar apenas regras de alta prioridade
markdownlint --rules MD001,MD009,MD034,MD047,MD045 **/*.md

# Verificar estruturação
markdownlint --rules MD001,MD003,MD022,MD025 **/*.md

# Verificar listas
markdownlint --rules MD004,MD007,MD030 **/*.md

# Ignorar regras específicas
markdownlint --disable MD041,MD013 **/*.md
```

### Tabela de Regras por Categoria

| Categoria | Regras | Descrição | Prioridade |
|-----------|--------|-----------|------------|
| **Estrutura** | MD001, MD003, MD022, MD025 | Hierarquia e formatação de cabeçalhos | 🏁 Alta |
| **Limpeza** | MD009, MD010, MD012 | Espaços e formatação limpa | 🏁 Alta |
| **Links** | MD034, MD039, MD042, MD051 | Formatação e validação de links | 🟡 Média |
| **Listas** | MD004, MD007, MD030 | Consistência em listas | 🟢 Baixa |
| **Código** | MD040 | Syntax highlighting | 🟡 Média |
| **Acessibilidade** | MD045 | Alt text em imagens | 🏁 Alta |
| **Compatibilidade** | MD047 | Padrões Git/POSIX | 🏁 Alta |

### Scripts de Automação

#### Script Bash para Correção Completa

```bash
#!/bin/bash
# fix-markdown-complete.sh

echo "Corrigindo todas as regras markdown..."

# Encontrar todos os arquivos .md
find . -name "*.md" -type f | while read -r file; do
    echo "Processando: $file"

    # Corrigir automaticamente o que for possível
    markdownlint --fix --config .markdownlint.json "$file"

    # Remover espaços no final (MD009)
    sed -i 's/[[:space:]]*$//' "$file"

    # Converter tabs para espaços (MD010)
    expand -t 2 "$file" > "${file}.tmp" && mv "${file}.tmp" "$file"

    # Garantir newline final (MD047)
    if [ -n "$(tail -c1 "$file")" ]; then
        echo >> "$file"
    fi
done

echo "Correção completa finalizada!"
```

#### Script Python para Validação

```python
#!/usr/bin/env python3
# validate_markdown.py

import subprocess
import sys
import json

def validate_markdown():
    """Valida arquivos markdown com relatório detalhado."""
    try:
        result = subprocess.run([
            'markdownlint', '--config', '.markdownlint.json',
            '--json', '**/*.md'
        ], capture_output=True, text=True)

        if result.returncode == 0:
            print("Todos os arquivos estão em conformidade!")
            return True

        errors = json.loads(result.stdout)

        # Agrupar erros por categoria
        categories = {
            'Alta Prioridade': ['MD001', 'MD009', 'MD034', 'MD047', 'MD045'],
            'Média Prioridade': ['MD003', 'MD022', 'MD025', 'MD004', 'MD007', 'MD040'],
            'Baixa Prioridade': ['MD010', 'MD012', 'MD030', 'MD036', 'MD039', 'MD042']
        }

        for category, rules in categories.items():
            category_errors = [e for e in errors if any(r in e.get('ruleNames', []) for r in rules)]
            if category_errors:
                print(f"\n{category}: {len(category_errors)} erros")
                for error in category_errors[:5]:  # Mostrar apenas os primeiros 5
                    print(f"  - {error['fileName']}: {error['ruleNames'][0]}")

        return False

    except Exception as e:
        print(f"Erro na validação: {e}")
        return False

if __name__ == "__main__":
    success = validate_markdown()
    sys.exit(0 if success else 1)
```

### Integração com Editores

#### VS Code Settings Completas

```json
{
  "markdownlint.config": {
    "MD001": true,
    "MD003": { "style": "atx" },
    "MD004": { "style": "dash" },
    "MD007": { "indent": 2 },
    "MD009": true,
    "MD010": true,
    "MD012": { "maximum": 1 },
    "MD013": {
      "line_length": 120,
      "code_blocks": false,
      "tables": false,
      "headings": false
    },
    "MD022": true,
    "MD024": true,
    "MD025": true,
    "MD030": { "ul_single": 1, "ul_multi": 1 },
    "MD033": {
      "allowed_elements": ["div", "img", "br", "details", "summary"]
    },
    "MD034": true,
    "MD036": true,
    "MD039": true,
    "MD040": true,
    "MD041": false,
    "MD042": true,
    "MD045": true,
    "MD047": true,
    "MD051": true
  },
  "files.insertFinalNewline": true,
  "files.trimFinalNewlines": true,
  "files.trimTrailingWhitespace": true,
  "editor.insertSpaces": true,
  "editor.tabSize": 2
}
```

#### Vim Configuration

```vim
" .vimrc - Configuração completa para markdown
autocmd FileType markdown setlocal textwidth=120
autocmd FileType markdown setlocal expandtab
autocmd FileType markdown setlocal tabstop=2
autocmd FileType markdown setlocal shiftwidth=2
autocmd BufWritePre *.md :%s/\s\+$//e
autocmd BufWritePre *.md :set eol

" Plugin markdownlint
Plug 'dense-analysis/ale'
let g:ale_linters = {'markdown': ['markdownlint']}
let g:ale_fixers = {'markdown': ['markdownlint']}
```

---

## 🚀 Automação

### Pre-commit Hooks Completos

#### .pre-commit-config.yaml

```yaml
repos:
  - repo: https://github.com/igorshubovych/markdownlint-cli
    rev: v0.37.0
    hooks:
      - id: markdownlint
        args: [--fix, --config, .markdownlint.json]
        files: \.md$

  - repo: https://github.com/pre-commit/pre-commit-hooks
    rev: v4.4.0
    hooks:
      - id: end-of-file-fixer
        types: [markdown]
      - id: trailing-whitespace
        types: [markdown]
      - id: mixed-line-ending
        types: [markdown]
        args: [--fix=lf]

  - repo: local
    hooks:
      - id: markdown-validation
        name: Markdown Validation
        entry: python scripts/validate_markdown.py
        language: python
        files: \.md$
        pass_filenames: false
```

#### Instalação

```bash
# Instalar pre-commit
pip install pre-commit

# Instalar hooks no repositório
pre-commit install

# Executar em todos os arquivos
pre-commit run --all-files

# Executar apenas markdown linting
pre-commit run markdownlint --all-files
```

### GitHub Actions Completo

#### .github/workflows/markdown-quality.yml

```yaml
name: Markdown Quality

on:
  push:
    paths: ['**/*.md']
  pull_request:
    paths: ['**/*.md']

jobs:
  markdown-lint:
    runs-on: ubuntu-latest
    steps:
      - uses: actions/checkout@v4

      - name: Setup Node.js
        uses: actions/setup-node@v4
        with:
          node-version: '18'

      - name: Install markdownlint-cli
        run: npm install -g markdownlint-cli

      - name: Run markdownlint
        run: markdownlint **/*.md --config .markdownlint.json

      - name: Check high priority rules
        run: markdownlint **/*.md --rules MD001,MD009,MD034,MD047,MD045

      - name: Generate report
        if: failure()
        run: |
          echo "## Markdown Linting Report" >> $GITHUB_STEP_SUMMARY
          markdownlint **/*.md --config .markdownlint.json --json | \
          python -c "import sys, json; data=json.load(sys.stdin); \
          print(f'**Errors found:** {len(data)}'); \
          [print(f'- {e[\"fileName\"]}: {e[\"ruleNames\"][0]}') for e in data[:10]]"
```

### Makefile Completo

```makefile
# Makefile - Markdown Quality Tools

.PHONY: lint-md fix-md validate-md check-priority install-tools

# Instalar ferramentas necessárias
install-tools:
 @echo "Instalando ferramentas de markdown..."
 npm install -g markdownlint-cli
 pip install pre-commit

# Verificar markdown com todas as regras
lint-md:
 @echo "Verificando arquivos Markdown..."
 markdownlint **/*.md --config .markdownlint.json

# Corrigir markdown automaticamente
fix-md:
 @echo "Corrigindo arquivos Markdown..."
 markdownlint --fix **/*.md --config .markdownlint.json
 @echo "Removendo espaços no final..."
 find . -name "*.md" -exec sed -i 's/[[:space:]]*$$//' {} \;
 @echo "Garantindo newline final..."
 find . -name "*.md" -exec sed -i -e '$$a\' {} \;

# Verificar apenas regras de alta prioridade
check-priority:
 @echo "Verificando regras de alta prioridade..."
 markdownlint **/*.md --rules MD001,MD009,MD034,MD047,MD045

# Validação completa com relatório
validate-md:
 @echo "Executando validação completa..."
 python scripts/validate_markdown.py

# Executar pre-commit em todos os arquivos
precommit-all:
 pre-commit run --all-files

# Setup completo do projeto
setup: install-tools
 pre-commit install
 @echo "Setup completo! Execute 'make lint-md' para verificar."
```

---

## 🔍 Verificação e Debugging

### Comandos de Diagnóstico Avançados

#### Verificação por Categoria

```bash
# Verificar estruturação (cabeçalhos)
markdownlint **/*.md --rules MD001,MD003,MD022,MD025 --json | \
jq '.[] | select(.ruleNames[] | test("MD00[1359]|MD02[25]")) | .fileName' | sort | uniq

# Verificar limpeza (espaços, tabs)
markdownlint **/*.md --rules MD009,MD010,MD012 --json | \
jq '.[] | select(.ruleNames[] | test("MD0(09|10|12)")) | .fileName' | sort | uniq

# Verificar links
markdownlint **/*.md --rules MD034,MD039,MD042,MD051 --json | \
jq '.[] | select(.ruleNames[] | test("MD0(34|39|42)|MD051")) | .fileName' | sort | uniq
```

### Relatório de Qualidade

```bash
#!/bin/bash
# quality-report.sh

echo "=== Relatório de Qualidade Markdown ==="
echo "Data: $(date)"
echo

# Contar arquivos
total_files=$(find . -name "*.md" | wc -l)
echo "Total de arquivos .md: $total_files"

# Verificar conformidade por categoria
echo
echo "=== Conformidade por Categoria ==="

categories=(
    "Estrutura:MD001,MD003,MD022,MD025"
    "Limpeza:MD009,MD010,MD012"
    "Links:MD034,MD039,MD042,MD051"
    "Listas:MD004,MD007,MD030"
    "Código:MD040"
    "Acessibilidade:MD045"
    "Compatibilidade:MD047"
)

for category in "${categories[@]}"; do
    name=$(echo $category | cut -d: -f1)
    rules=$(echo $category | cut -d: -f2)

    errors=$(markdownlint **/*.md --rules $rules --json 2>/dev/null | jq 'length' 2>/dev/null || echo "0")

    if [ "$errors" -eq 0 ]; then
        echo "✅ $name: Todos os arquivos conformes"
    else
        echo "❌ $name: $errors erros encontrados"
    fi
done

# Score geral
total_errors=$(markdownlint **/*.md --config .markdownlint.json --json 2>/dev/null | jq 'length' 2>/dev/null || echo "0")
if [ "$total_errors" -eq 0 ]; then
    echo
    echo "🏆 Score de Qualidade: 100% - Excelente!"
else
    conforming_files=$((total_files - $(markdownlint **/*.md --config .markdownlint.json --json 2>/dev/null | jq 'map(.fileName) | unique | length' 2>/dev/null || echo "$total_files")))
    score=$((conforming_files * 100 / total_files))
    echo
    echo "📊 Score de Qualidade: $score% ($conforming_files/$total_files arquivos conformes)"
fi
```

---

## 📚 Referências

### Documentação Oficial

- **[Markdownlint Rules](https://github.com/DavidAnson/markdownlint/blob/main/doc/Rules.md)**: Lista completa de regras
- **[Markdownlint CLI](https://github.com/igorshubovych/markdownlint-cli)**: Ferramenta de linha de comando
- **[CommonMark Spec](https://commonmark.org/)**: Especificação Markdown padrão

### Ferramentas Relacionadas

| Ferramenta | Propósito | Link | Categoria |
|------------|-----------|------|----------|
| **markdownlint-cli** | Linting via CLI | [GitHub](https://github.com/igorshubovych/markdownlint-cli) | Essencial |
| **vscode-markdownlint** | Extensão VS Code | [Marketplace](https://marketplace.visualstudio.com/items?itemName=DavidAnson.vscode-markdownlint) | Editor |
| **markdown-link-check** | Verificar links | [GitHub](https://github.com/tcort/markdown-link-check) | Validação |
| **remark-lint** | Alternativa ao markdownlint | [GitHub](https://github.com/remarkjs/remark-lint) | Alternativa |
| **textlint** | Linting de texto | [GitHub](https://github.com/textlint/textlint) | Complementar |
| **alex** | Linguagem inclusiva | [GitHub](https://github.com/get-alex/alex) | Qualidade |

### Configurações Avançadas

#### .markdownlint.jsonc (com comentários)

```jsonc
{
  // Habilitar todas as regras por padrão
  "default": true,

  // MD001 - Hierarquia de cabeçalhos
  "MD001": true,                  // Obrigatório para estruturação

  // MD003 - Estilo de cabeçalhos
  "MD003": { "style": "atx" },     // Usar # em vez de underline

  // MD004 - Estilo de listas
  "MD004": { "style": "dash" },   // Usar - para listas

  // MD007 - Indentação de listas
  "MD007": { "indent": 2 },       // 2 espaços de indentação

  // MD009 - Espaços no final
  "MD009": true,                  // Remover espaços trailing

  // MD010 - Tabs rígidos
  "MD010": true,                  // Usar espaços em vez de tabs

  // MD012 - Múltiplas linhas em branco
  "MD012": { "maximum": 1 },      // Máximo 1 linha em branco

  // MD013 - Comprimento de linha
  "MD013": {
    "line_length": 120,           // Máximo 120 caracteres
    "code_blocks": false,         // Ignorar blocos de código
    "tables": false,              // Ignorar tabelas
    "headings": false             // Ignorar cabeçalhos
  },

  // MD022 - Cabeçalhos com linhas em branco
  "MD022": true,                  // Cabeçalhos cercados por linhas em branco

  // MD024 - Cabeçalhos duplicados
  "MD024": true,                  // Evitar cabeçalhos idênticos

  // MD025 - Múltiplos H1
  "MD025": true,                  // Apenas um H1 por documento

  // MD030 - Espaços após marcadores
  "MD030": {
    "ul_single": 1,              // 1 espaço para listas simples
    "ul_multi": 1                // 1 espaço para listas múltiplas
  },

  // MD033 - HTML inline
  "MD033": {
    "allowed_elements": [
      "div", "img", "br",         // Elementos básicos
      "details", "summary",       // Elementos de disclosure
      "kbd", "sup", "sub"         // Elementos de formatação
    ]
  },

  // MD034 - URLs sem formatação
  "MD034": true,                  // Usar [texto](url) em vez de URL nua

  // MD036 - Ênfase como cabeçalho
  "MD036": true,                  // Usar ## em vez de **texto**

  // MD039 - Espaços em links
  "MD039": true,                  // Evitar [ texto ](url)

  // MD040 - Linguagem em código
  "MD040": true,                  // Especificar linguagem em ```

  // MD041 - Primeiro cabeçalho
  "MD041": false,                 // Desabilitado para templates DATAMETRIA

  // MD042 - Links vazios
  "MD042": true,                  // Evitar [texto]() ou [](url)

  // MD045 - Alt text em imagens
  "MD045": true,                  // Obrigatório para acessibilidade

  // MD047 - Newline no final
  "MD047": true,                  // Obrigatório para compatibilidade Git

  // MD051 - Fragmentos de link válidos
  "MD051": true                   // Verificar links internos #section
}
```

---

## ✅ Checklist de Implementação

### Setup Inicial

- [ ] **Configuração** criada (.markdownlint.json completa)
- [ ] **Ferramentas** instaladas (markdownlint-cli, pre-commit)
- [ ] **VS Code** configurado (extensão + settings completas)
- [ ] **Scripts** de correção e validação criados

### Automação

- [ ] **Pre-commit hooks** configurados com todas as regras
- [ ] **GitHub Actions** implementado com relatórios
- [ ] **Makefile** criado com comandos completos
- [ ] **Scripts** de verificação por categoria implementados

### Verificação

- [ ] **Todos os arquivos** verificados com 22 regras
- [ ] **Regras de alta prioridade** (MD001, MD009, MD034, MD047, MD045) corrigidas
- [ ] **Estruturação** (MD003, MD022, MD025) implementada
- [ ] **Limpeza** (MD010, MD012) aplicada
- [ ] **Links** (MD039, MD042, MD051) validados

### Qualidade

- [ ] **Score de qualidade** > 95%
- [ ] **Relatório** de conformidade gerado
- [ ] **Documentação** de processo atualizada
- [ ] **Treinamento** da equipe realizado

### Manutenção

- [ ] **Processo** documentado e automatizado
- [ ] **Monitoramento** contínuo implementado
- [ ] **Revisões** periódicas agendadas (mensais)
- [ ] **Métricas** de qualidade acompanhadas

---

<div align="center">

**Desenvolvido por**: Equipe DATAMETRIA
**Última Atualização**: [DD/MM/AAAA]
**Versão**: 2.0.0

---

## Framework completo de Markdown Linting implementado! 22 regras ativas! 🚀

</div>
