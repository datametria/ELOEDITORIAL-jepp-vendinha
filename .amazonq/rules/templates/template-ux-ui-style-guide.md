# [Nome do Projeto] - Guia de Estilo UX/UI

**Versão:** 1.0  
**Data:** [DD/MM/AAAA]  
**Autor:** [Nome do Designer/Equipe]  
**Cliente:** [Nome do Cliente]

[![Design System](https://img.shields.io/badge/Design%20System-v1.0-blue)]()
[![Figma](https://img.shields.io/badge/Figma-Ready-green)]()
[![WCAG](https://img.shields.io/badge/WCAG-2.2%20AA-green)](https://www.w3.org/WAI/WCAG22/quickref/)

[🎨 Figma](#) • [📱 Protótipo](#) • [💻 Storybook](#)

---

## 📋 Índice

1. [Visão Geral](#1-visao-geral)
2. [Identidade Visual](#2-identidade-visual)
3. [Tipografia](#3-tipografia)
4. [Cores](#4-cores)
5. [Espaçamento e Grid](#5-espacamento-e-grid)
6. [Componentes](#6-componentes)
7. [Ícones](#7-icones)
8. [Responsividade](#8-responsividade)
9. [Acessibilidade](#9-acessibilidade)
10. [Animações](#10-animacoes)

---

## 1. Visão Geral

### 🎯 Objetivo

[Descrever o objetivo do guia de estilo e como ele deve ser usado]

### 🎨 Princípios de Design

| Princípio | Descrição |
|-----------|-----------|
| **[Princípio 1]** | [Descrição] |
| **[Princípio 2]** | [Descrição] |
| **[Princípio 3]** | [Descrição] |

### 📱 Plataformas

- [ ] Web Desktop
- [ ] Web Mobile
- [ ] iOS App
- [ ] Android App
- [ ] PWA

---

## 2. Identidade Visual

### Logo

#### Versões

```
Logo Principal
├── Colorida (uso preferencial)
├── Monocromática (fundos coloridos)
├── Negativa (fundos escuros)
└── Simplificada (ícone/favicon)
```

#### Área de Proteção

```css
/* Espaço mínimo ao redor do logo */
--logo-safe-area: [altura do logo] × 0.5;
```

#### Tamanhos Mínimos

- **Digital:** [X]px de altura
- **Impresso:** [X]mm de altura

### Paleta de Marca

```css
:root {
  /* Cor Primária */
  --brand-primary: #[HEX];
  --brand-primary-light: #[HEX];
  --brand-primary-dark: #[HEX];

  /* Cor Secundária */
  --brand-secondary: #[HEX];
  --brand-secondary-light: #[HEX];
  --brand-secondary-dark: #[HEX];

  /* Cor de Destaque */
  --brand-accent: #[HEX];
}
```

---

## 3. Tipografia

### Fontes

#### Família Primária

```css
--font-primary: '[Nome da Fonte]', sans-serif;
```

**Uso:** Títulos, headings, destaques

**Pesos disponíveis:**
- Light (300)
- Regular (400)
- Medium (500)
- Bold (700)

#### Família Secundária

```css
--font-secondary: '[Nome da Fonte]', sans-serif;
```

**Uso:** Corpo de texto, parágrafos

**Pesos disponíveis:**
- Regular (400)
- Medium (500)

### Escala Tipográfica

```css
/* Headings */
--text-h1: 2.5rem;    /* 40px */
--text-h2: 2rem;      /* 32px */
--text-h3: 1.5rem;    /* 24px */
--text-h4: 1.25rem;   /* 20px */
--text-h5: 1.125rem;  /* 18px */
--text-h6: 1rem;      /* 16px */

/* Body */
--text-body-lg: 1.125rem;  /* 18px */
--text-body: 1rem;         /* 16px */
--text-body-sm: 0.875rem;  /* 14px */
--text-caption: 0.75rem;   /* 12px */
```

### Line Height

```css
--line-height-tight: 1.2;
--line-height-normal: 1.5;
--line-height-relaxed: 1.75;
```

### Exemplos de Uso

```html
<h1 class="text-h1">Título Principal</h1>
<h2 class="text-h2">Subtítulo</h2>
<p class="text-body">Parágrafo de texto normal.</p>
<span class="text-caption">Texto auxiliar</span>
```

---

## 4. Cores

### Paleta Semântica

```css
:root {
  /* Primárias */
  --primary-50: #[HEX];
  --primary-100: #[HEX];
  --primary-500: #[HEX];
  --primary-700: #[HEX];
  --primary-900: #[HEX];

  /* Neutras */
  --gray-50: #[HEX];
  --gray-100: #[HEX];
  --gray-500: #[HEX];
  --gray-700: #[HEX];
  --gray-900: #[HEX];

  /* Estados */
  --success: #[HEX];
  --warning: #[HEX];
  --error: #[HEX];
  --info: #[HEX];
}
```

### Gradientes

```css
--gradient-primary: linear-gradient(135deg, [cor1] 0%, [cor2] 100%);
--gradient-secondary: linear-gradient(135deg, [cor1] 0%, [cor2] 100%);
```

### Contraste WCAG 2.2 AA

| Combinação | Ratio | Status |
|------------|-------|--------|
| Texto primário / Fundo | [X]:1 | ✅ AA |
| Texto secundário / Fundo | [X]:1 | ✅ AA |
| Links / Fundo | [X]:1 | ✅ AA |

---

## 5. Espaçamento e Grid

### Sistema de Espaçamento

```css
:root {
  --space-1: 0.25rem;  /* 4px */
  --space-2: 0.5rem;   /* 8px */
  --space-3: 0.75rem;  /* 12px */
  --space-4: 1rem;     /* 16px */
  --space-6: 1.5rem;   /* 24px */
  --space-8: 2rem;     /* 32px */
  --space-12: 3rem;    /* 48px */
  --space-16: 4rem;    /* 64px */
}
```

### Grid System

```css
.container {
  max-width: [1200px];
  margin: 0 auto;
  padding: 0 var(--space-4);
}

.grid {
  display: grid;
  grid-template-columns: repeat(12, 1fr);
  gap: var(--space-6);
}
```

### Breakpoints

```css
:root {
  --breakpoint-sm: 640px;   /* Mobile landscape */
  --breakpoint-md: 768px;   /* Tablet */
  --breakpoint-lg: 1024px;  /* Desktop */
  --breakpoint-xl: 1280px;  /* Large desktop */
  --breakpoint-2xl: 1536px; /* Extra large */
}
```

---

## 6. Componentes

### Botões

#### Variantes

**Primary**
```css
.btn-primary {
  background: var(--primary-500);
  color: white;
  min-width: 24px;
  min-height: 24px;
  padding: var(--space-2) var(--space-4);
  border-radius: 8px;
}
```

**Secondary**
```css
.btn-secondary {
  background: var(--gray-200);
  color: var(--gray-900);
  min-width: 24px;
  min-height: 24px;
  padding: var(--space-2) var(--space-4);
  border-radius: 8px;
}
```

**Outline**
```css
.btn-outline {
  background: transparent;
  border: 2px solid var(--primary-500);
  color: var(--primary-500);
  min-width: 24px;
  min-height: 24px;
  padding: var(--space-2) var(--space-4);
  border-radius: 8px;
}
```

#### Tamanhos

```css
.btn-sm { height: 32px; padding: 0 var(--space-3); font-size: 0.875rem; }
.btn-md { height: 40px; padding: 0 var(--space-4); font-size: 1rem; }
.btn-lg { height: 48px; padding: 0 var(--space-6); font-size: 1.125rem; }
```

#### Estados

```css
.btn:hover { transform: scale(1.05); }
.btn:active { transform: scale(0.95); }
.btn:focus { outline: 2px solid var(--primary-500); outline-offset: 2px; z-index: 999; }
.btn:disabled { opacity: 0.5; cursor: not-allowed; }
```

### Cards

```css
.card {
  background: white;
  border-radius: 12px;
  padding: var(--space-6);
  box-shadow: 0 1px 3px rgba(0, 0, 0, 0.1);
  border-left: 4px solid var(--primary-500);
}

.card:hover {
  box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
}
```

### Inputs

```css
.input {
  width: 100%;
  min-height: 40px;
  padding: var(--space-2) var(--space-3);
  border: 1px solid var(--gray-300);
  border-radius: 8px;
  font-size: 1rem;
}

.input:focus {
  outline: none;
  border-color: var(--primary-500);
  box-shadow: 0 0 0 3px rgba(var(--primary-rgb), 0.1);
  z-index: 999;
}
```

### Modais

```css
.modal-overlay {
  position: fixed;
  inset: 0;
  background: rgba(0, 0, 0, 0.5);
  z-index: 998;
}

.modal {
  position: fixed;
  top: 50%;
  left: 50%;
  transform: translate(-50%, -50%);
  background: white;
  border-radius: 12px;
  padding: var(--space-8);
  max-width: 500px;
  z-index: 999;
}
```

---

## 7. Ícones

### Biblioteca

**Fonte:** [Nome da biblioteca - ex: Heroicons, Material Icons, Font Awesome]

### Tamanhos

```css
--icon-xs: 16px;
--icon-sm: 20px;
--icon-md: 24px;
--icon-lg: 32px;
--icon-xl: 48px;
```

### Uso

```html
<icon name="[nome]" size="md" />
```

### Acessibilidade

```html
<!-- Ícone decorativo -->
<icon name="star" aria-hidden="true" />

<!-- Ícone funcional -->
<button aria-label="Fechar modal">
  <icon name="close" aria-hidden="true" />
</button>
```

---

## 8. Responsividade

### Abordagem

- [x] Mobile First
- [ ] Desktop First

### Adaptações por Breakpoint

#### Mobile (< 768px)

- Layout em coluna única
- Navegação em hambúrguer
- Tipografia reduzida em 10%
- Espaçamentos reduzidos

#### Tablet (768px - 1024px)

- Layout em 2 colunas
- Navegação híbrida
- Tipografia padrão
- Espaçamentos padrão

#### Desktop (> 1024px)

- Layout em 3+ colunas
- Navegação completa
- Tipografia aumentada em 10%
- Espaçamentos aumentados

---

## 9. Acessibilidade

### WCAG 2.2 AA Compliance

#### Checklist

**Critérios Essenciais:**
- [x] Contraste mínimo 4.5:1 para texto normal
- [x] Contraste mínimo 3:1 para texto grande
- [x] Target size mínimo 24x24px
- [x] Focus nunca obscurecido (z-index: 999)
- [x] Navegação por teclado completa
- [x] ARIA labels em elementos interativos
- [x] Skip links implementados
- [x] Textos alternativos em imagens

**Novos Critérios WCAG 2.2:**
- [x] **2.4.11** Focus Not Obscured (Minimum)
- [x] **2.5.8** Target Size (Minimum) - 24x24px
- [x] **3.2.6** Consistent Help
- [x] **3.3.7** Redundant Entry
- [x] **3.3.8** Accessible Authentication

### Screen Readers

```html
<!-- Texto apenas para screen readers -->
<span class="sr-only">Texto descritivo</span>

<!-- Ocultar de screen readers -->
<div aria-hidden="true">Conteúdo decorativo</div>
```

### Navegação por Teclado

```css
/* Focus visível */
:focus-visible {
  outline: 2px solid var(--primary-500);
  outline-offset: 2px;
  z-index: 999;
}

/* Skip link */
.skip-link {
  position: absolute;
  top: -40px;
  left: 6px;
  z-index: 1001;
}

.skip-link:focus {
  top: 6px;
}
```

---

## 10. Animações

### Duração

```css
--duration-fast: 150ms;
--duration-normal: 300ms;
--duration-slow: 500ms;
```

### Easing

```css
--ease-in: cubic-bezier(0.4, 0, 1, 1);
--ease-out: cubic-bezier(0, 0, 0.2, 1);
--ease-in-out: cubic-bezier(0.4, 0, 0.2, 1);
```

### Transições Comuns

```css
/* Hover suave */
.hover-scale {
  transition: transform var(--duration-normal) var(--ease-out);
}
.hover-scale:hover {
  transform: scale(1.05);
}

/* Fade in */
.fade-in {
  animation: fadeIn var(--duration-normal) var(--ease-out);
}

@keyframes fadeIn {
  from { opacity: 0; }
  to { opacity: 1; }
}
```

### Preferências de Movimento

```css
@media (prefers-reduced-motion: reduce) {
  * {
    animation-duration: 0.01ms !important;
    transition-duration: 0.01ms !important;
  }
}
```

---

## 📦 Exportação

### Tokens de Design

**Formato:** JSON, CSS Variables, SCSS

**Localização:** `/design-tokens/`

### Figma

**Link:** [URL do Figma]

**Componentes:** [X] componentes documentados

### Storybook

**Link:** [URL do Storybook]

**Cobertura:** [X]% dos componentes

---

## 🔄 Versionamento

### Histórico

| Versão | Data | Mudanças |
|--------|------|----------|
| 1.0 | [DD/MM/AAAA] | Versão inicial |

### Próximas Atualizações

- [ ] [Item 1]
- [ ] [Item 2]
- [ ] [Item 3]

---

## 📞 Contato

**Designer Responsável:** [Nome]  
**Email:** [email@exemplo.com]  
**Equipe:** [Nome da Equipe]

---

**Desenvolvido com ❤️ por [Equipe/Empresa]**

*Este guia de estilo segue os padrões DATAMETRIA e está em conformidade com WCAG 2.2 AA.*
