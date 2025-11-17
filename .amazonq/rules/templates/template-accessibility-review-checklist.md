# Checklist de Accessibility Review

<div align="center">

## Checklist Completo para Revisão de Acessibilidade

[![Accessibility](https://img.shields.io/badge/accessibility-WCAG%202.1%20AA-green)](https://www.w3.org/WAI/WCAG21/quickref/)
[![Inclusive](https://img.shields.io/badge/design-inclusive-blue)](https://inclusivedesignprinciples.org/)
[![DATAMETRIA](https://img.shields.io/badge/DATAMETRIA-Standards-blue)](https://github.com/datametria/standards)

[👁️ Visual](#️-acessibilidade-visual) • [🎧 Auditiva](#-acessibilidade-auditiva) • [⌨️ Navegação](#️-navegação-por-teclado) •
[📱 Mobile](#-acessibilidade-mobile) • [🧠 Cognitiva](#-acessibilidade-cognitiva)

</div>

---

## 📋 Informações da Revisão

| Campo | Descrição |
|-------|-----------|
| **Aplicação** | [Nome da aplicação] |
| **Versão** | [Versão da aplicação] |
| **Reviewer** | [Nome do accessibility specialist] |
| **Data da Revisão** | [DD/MM/AAAA] |
| **Padrão** | [WCAG 2.1 AA] |
| **Ferramentas** | [axe-core, WAVE, Lighthouse] |
| **Dispositivos Testados** | [Desktop, Mobile, Screen Reader] |

---

## 🎯 Princípios WCAG 2.1

### Perceptível

- [ ] **Alternativas textuais**: Conteúdo não-textual tem alternativas
- [ ] **Mídia temporal**: Alternativas para mídia baseada em tempo
- [ ] **Adaptável**: Conteúdo pode ser apresentado de diferentes formas
- [ ] **Distinguível**: Facilita aos usuários ver e ouvir conteúdo

### Operável

- [ ] **Acessível por teclado**: Funcionalidade disponível via teclado
- [ ] **Tempo suficiente**: Usuários têm tempo suficiente para ler
- [ ] **Convulsões**: Conteúdo não causa convulsões
- [ ] **Navegável**: Ajuda usuários a navegar e encontrar conteúdo

### Compreensível

- [ ] **Legível**: Texto é legível e compreensível
- [ ] **Previsível**: Páginas aparecem e operam de forma previsível
- [ ] **Assistência de entrada**: Ajuda usuários a evitar e corrigir erros

### Robusto

- [ ] **Compatível**: Conteúdo pode ser interpretado por tecnologias assistivas

---

## 👁️ Acessibilidade Visual

### Contraste de Cores

- [ ] **Contraste normal**: Razão de contraste ≥ 4.5:1 para texto normal
- [ ] **Contraste grande**: Razão de contraste ≥ 3:1 para texto grande (18pt+)
- [ ] **Elementos não-textuais**: Contraste ≥ 3:1 para ícones e gráficos
- [ ] **Estados de foco**: Indicadores de foco visíveis com contraste adequado
- [ ] **Links**: Links têm contraste adequado e são distinguíveis

```css
/* ✅ Bom - Contraste adequado */
.text-primary {
  color: #1a1a1a; /* Contraste 16.94:1 com fundo branco */
  background: #ffffff;
}

/* ❌ Ruim - Contraste insuficiente */
.text-light {
  color: #cccccc; /* Contraste 1.61:1 com fundo branco */
  background: #ffffff;
}

/* ✅ Bom - Indicador de foco visível */
.button:focus {
  outline: 2px solid #0066cc;
  outline-offset: 2px;
}
```

### Texto Alternativo

- [ ] **Imagens informativas**: Alt text descritivo e conciso
- [ ] **Imagens decorativas**: Alt text vazio (alt="")
- [ ] **Imagens complexas**: Descrição longa disponível
- [ ] **Ícones funcionais**: Alt text descreve a função
- [ ] **Logotipos**: Alt text com nome da organização

### Redimensionamento

- [ ] **Zoom 200%**: Conteúdo legível e funcional em 200% zoom
- [ ] **Reflow**: Conteúdo se adapta sem scroll horizontal
- [ ] **Texto responsivo**: Texto escala adequadamente
- [ ] **Elementos interativos**: Mantêm funcionalidade no zoom

---

## 🎧 Acessibilidade Auditiva

### Conteúdo de Áudio

- [ ] **Legendas**: Vídeos têm legendas precisas e sincronizadas
- [ ] **Transcrições**: Áudio tem transcrições disponíveis
- [ ] **Descrição de áudio**: Vídeos têm descrição de áudio quando necessário
- [ ] **Controles de mídia**: Controles acessíveis por teclado
- [ ] **Auto-play**: Mídia não inicia automaticamente com som

### Alertas e Notificações

- [ ] **Alertas visuais**: Alertas têm indicação visual além do som
- [ ] **Notificações persistentes**: Notificações importantes são persistentes
- [ ] **Feedback visual**: Ações têm feedback visual claro

```html
<!-- ✅ Bom - Vídeo com legendas e controles acessíveis -->
<video controls>
  <source src="video.mp4" type="video/mp4">
  <track kind="captions" src="captions.vtt" srclang="pt" label="Português">
  <track kind="descriptions" src="descriptions.vtt" srclang="pt" label="Descrições">
</video>

<!-- ✅ Bom - Alerta com indicação visual e sonora -->
<div role="alert" class="alert alert-error">
  <span class="alert-icon" aria-hidden="true">⚠️</span>
  <span class="alert-message">Erro ao salvar dados</span>
</div>
```

---

## ⌨️ Navegação por Teclado

### Navegação Básica

- [ ] **Tab order**: Ordem de tabulação lógica e intuitiva
- [ ] **Foco visível**: Indicador de foco sempre visível
- [ ] **Sem armadilhas**: Usuário pode sair de qualquer elemento
- [ ] **Skip links**: Links para pular navegação disponíveis
- [ ] **Atalhos de teclado**: Atalhos documentados e consistentes

### Elementos Interativos

- [ ] **Botões**: Ativados com Enter e Space
- [ ] **Links**: Ativados com Enter
- [ ] **Formulários**: Navegação entre campos funcional
- [ ] **Menus**: Navegação com setas funcionais
- [ ] **Modais**: Foco gerenciado adequadamente

```javascript
// ✅ Bom - Gerenciamento de foco em modal
function openModal(modalId) {
  const modal = document.getElementById(modalId);
  const firstFocusable = modal.querySelector('button, input, select, textarea, [tabindex]:not([tabindex="-1"])');

  modal.style.display = 'block';
  firstFocusable.focus();

  // Trap focus dentro do modal
  modal.addEventListener('keydown', trapFocus);
}

function trapFocus(e) {
  if (e.key === 'Tab') {
    const focusableElements = modal.querySelectorAll('button, input, select, textarea, [tabindex]:not([tabindex="-1"])');
    const firstElement = focusableElements[0];
    const lastElement = focusableElements[focusableElements.length - 1];

    if (e.shiftKey && document.activeElement === firstElement) {
      e.preventDefault();
      lastElement.focus();
    } else if (!e.shiftKey && document.activeElement === lastElement) {
      e.preventDefault();
      firstElement.focus();
    }
  }
}
```

### Componentes Complexos

- [ ] **Carrosséis**: Navegação por teclado implementada
- [ ] **Tabs**: Navegação com setas e ativação com Enter/Space
- [ ] **Dropdowns**: Navegação e seleção por teclado
- [ ] **Data tables**: Navegação entre células funcional
- [ ] **Drag and drop**: Alternativa por teclado disponível

---

## 📱 Acessibilidade Mobile

### Touch Targets

- [ ] **Tamanho mínimo**: Alvos de toque ≥ 44x44px (iOS) ou 48x48dp (Android)
- [ ] **Espaçamento**: Espaço adequado entre elementos interativos
- [ ] **Área de toque**: Área de toque maior que elemento visual
- [ ] **Gestos alternativos**: Alternativas para gestos complexos

### Screen Readers Mobile

- [ ] **VoiceOver (iOS)**: Funciona corretamente com VoiceOver
- [ ] **TalkBack (Android)**: Funciona corretamente com TalkBack
- [ ] **Ordem de leitura**: Ordem lógica para leitores de tela
- [ ] **Landmarks**: Landmarks definidos para navegação
- [ ] **Headings**: Hierarquia de cabeçalhos clara

```jsx
// React Native - Acessibilidade
<TouchableOpacity
  accessible={true}
  accessibilityLabel="Adicionar item ao carrinho"
  accessibilityHint="Toque duas vezes para adicionar"
  accessibilityRole="button"
  style={{ minWidth: 44, minHeight: 44 }}
>
  <Text>Adicionar</Text>
</TouchableOpacity>

// Flutter - Acessibilidade
Semantics(
  label: 'Adicionar item ao carrinho',
  hint: 'Toque duas vezes para adicionar',
  button: true,
  child: Container(
    constraints: BoxConstraints(minWidth: 48, minHeight: 48),
    child: ElevatedButton(
      onPressed: () => addToCart(),
      child: Text('Adicionar'),
    ),
  ),
)
```

### Orientação e Zoom

- [ ] **Orientação**: Funciona em portrait e landscape
- [ ] **Zoom**: Suporta zoom até 200% sem perda de funcionalidade
- [ ] **Reflow**: Conteúdo se adapta sem scroll horizontal
- [ ] **Entrada de texto**: Campos de entrada acessíveis

---

## 🧠 Acessibilidade Cognitiva

### Clareza e Simplicidade

- [ ] **Linguagem simples**: Texto claro e direto
- [ ] **Instruções claras**: Instruções específicas e compreensíveis
- [ ] **Consistência**: Interface consistente em toda aplicação
- [ ] **Previsibilidade**: Comportamento previsível dos elementos
- [ ] **Ajuda contextual**: Ajuda disponível quando necessário

### Tempo e Interação

- [ ] **Sem limite de tempo**: Ou usuário pode estender tempo
- [ ] **Pausar animações**: Animações podem ser pausadas
- [ ] **Controle de movimento**: Movimento pode ser desabilitado
- [ ] **Múltiplas tentativas**: Usuário pode tentar novamente após erro

### Prevenção de Erros

- [ ] **Validação em tempo real**: Feedback imediato em formulários
- [ ] **Mensagens de erro claras**: Erros explicados de forma simples
- [ ] **Sugestões de correção**: Sugestões para corrigir erros
- [ ] **Confirmação de ações**: Ações destrutivas requerem confirmação

```html
<!-- ✅ Bom - Formulário com validação acessível -->
<form>
  <label for="email">Email (obrigatório)</label>
  <input
    type="email"
    id="email"
    required
    aria-describedby="email-error email-help"
    aria-invalid="false"
  >
  <div id="email-help">Digite seu endereço de email</div>
  <div id="email-error" role="alert" style="display: none;">
    Por favor, digite um email válido
  </div>

  <button type="submit">Enviar</button>
</form>l for="email">Email (obrigatório)</label>
  <input
    type="email"
    id="email"
    required
    aria-describedby="email-error email-help"
    aria-invalid="false"
  >
  <div id="email-help">Digite seu endereço de email</div>
  <div id="email-error" role="alert" style="display: none;">
    Por favor, digite um email válido
  </div>

  <button type="submit">Enviar</button>
</form>
```

---

## 🏗️ Estrutura Semântica

### HTML Semântico

- [ ] **Landmarks**: main, nav, aside, footer definidos
- [ ] **Headings**: Hierarquia h1-h6 lógica e sequencial
- [ ] **Lists**: Listas usam ul/ol/dl apropriadamente
- [ ] **Tables**: Tabelas têm cabeçalhos e legendas
- [ ] **Forms**: Formulários têm labels e fieldsets

### ARIA (Accessible Rich Internet Applications)

- [ ] **Roles**: Roles ARIA apropriados definidos
- [ ] **Properties**: Propriedades ARIA corretas
- [ ] **States**: Estados ARIA atualizados dinamicamente
- [ ] **Labels**: aria-label ou aria-labelledby quando necessário
- [ ] **Descriptions**: aria-describedby para informações adicionais

```html
<!-- ✅ Bom - Estrutura semântica completa -->
<main>
  <h1>Página Principal</h1>

  <nav aria-label="Navegação principal">
    <ul>
      <li><a href="/home">Início</a></li>
      <li><a href="/products">Produtos</a></li>
      <li><a href="/contact">Contato</a></li>
    </ul>
  </nav>

  <section>
    <h2>Produtos em Destaque</h2>
    <div role="region" aria-label="Lista de produtos">
      <!-- Produtos -->
    </div>
  </section>

  <aside>
    <h2>Filtros</h2>
    <form role="search">
      <!-- Filtros -->
    </form>
  </aside>
</main>

<footer>
  <p>&copy; 2025 DATAMETRIA</p>
</footer>
```

---

## 🧪 Testes de Acessibilidade

### Testes Automatizados

- [ ] **axe-core**: Testes automatizados com axe-core
- [ ] **Lighthouse**: Auditoria de acessibilidade do Lighthouse
- [ ] **WAVE**: Análise com WAVE Web Accessibility Evaluator
- [ ] **Pa11y**: Testes de linha de comando com Pa11y
- [ ] **CI/CD Integration**: Testes integrados ao pipeline

```javascript
// Exemplo de teste automatizado com Jest e axe-core
import { axe, toHaveNoViolations } from 'jest-axe';

expect.extend(toHaveNoViolations);

test('should not have accessibility violations', async () => {
  const { container } = render(<MyComponent />);
  const results = await axe(container);
  expect(results).toHaveNoViolations();
});

// Teste específico para contraste
test('should have sufficient color contrast', async () => {
  const { container } = render(<Button>Click me</Button>);
  const results = await axe(container, {
    rules: {
      'color-contrast': { enabled: true }
    }
  });
  expect(results).toHaveNoViolations();
});
```

### Testes Manuais

- [ ] **Navegação por teclado**: Teste completo apenas com teclado
- [ ] **Screen reader**: Teste com NVDA, JAWS, ou VoiceOver
- [ ] **Zoom**: Teste com zoom 200% e 400%
- [ ] **Alto contraste**: Teste em modo alto contraste
- [ ] **Usuários reais**: Teste com usuários com deficiência

### Ferramentas de Teste

- [ ] **Browser DevTools**: Ferramentas de acessibilidade do navegador
- [ ] **Colour Contrast Analyser**: Verificação de contraste
- [ ] **Screen reader**: NVDA (gratuito), JAWS, VoiceOver
- [ ] **Keyboard navigation**: Teste apenas com teclado
- [ ] **Mobile accessibility**: TalkBack, VoiceOver mobile

---

## 📊 Relatório de Acessibilidade

### Conformidade WCAG 2.1 AA

| Critério | Status | Notas |
|----------|--------|-------|
| **1.1.1 - Conteúdo Não-textual** | ✅/❌ | [Observações] |
| **1.3.1 - Informações e Relações** | ✅/❌ | [Observações] |
| **1.4.3 - Contraste (Mínimo)** | ✅/❌ | [Observações] |
| **2.1.1 - Teclado** | ✅/❌ | [Observações] |
| **2.4.1 - Ignorar Blocos** | ✅/❌ | [Observações] |
| **3.1.1 - Idioma da Página** | ✅/❌ | [Observações] |
| **4.1.1 - Análise** | ✅/❌ | [Observações] |

### Problemas Encontrados

#### Críticos

```
[Listar problemas críticos que impedem o uso]
```

#### Importantes

```
[Listar problemas importantes que dificultam o uso]
```

#### Menores

```
[Listar problemas menores de usabilidade]
```

### Recomendações

```
[Recomendações específicas para melhorar acessibilidade]
```

---

## ✅ Checklist Final

### Aprovação de Acessibilidade

- [ ] **WCAG 2.1 AA**: Conformidade com WCAG 2.1 nível AA
- [ ] **Testes automatizados**: Passam em todas as ferramentas
- [ ] **Testes manuais**: Aprovados em testes manuais
- [ ] **Screen readers**: Funciona com leitores de tela
- [ ] **Navegação por teclado**: Totalmente navegável por teclado
- [ ] **Contraste**: Todos os elementos têm contraste adequado

### Documentação

- [ ] **Guia de acessibilidade**: Documentação para desenvolvedores
- [ ] **Testes documentados**: Procedimentos de teste documentados
- [ ] **Treinamento**: Equipe treinada em acessibilidade
- [ ] **Processo contínuo**: Processo de revisão contínua estabelecido

---

## 📚 Recursos de Acessibilidade

### Diretrizes e Padrões

- **[WCAG 2.1 Guidelines](https://www.w3.org/WAI/WCAG21/quickref/)**
- **[WAI-ARIA Authoring Practices](https://www.w3.org/WAI/ARIA/apg/)**
- **[Inclusive Design Principles](https://inclusivedesignprinciples.org/)**

### Ferramentas

- **[axe DevTools](https://www.deque.com/axe/devtools/)**
- **[WAVE Web Accessibility Evaluator](https://wave.webaim.org/)**
- **[Colour Contrast Analyser](https://www.tpgi.com/color-contrast-checker/)**
- **[Pa11y Command Line Tool](https://pa11y.org/)**

### Screen Readers

- **[NVDA](https://www.nvaccess.org/)** (Windows - Gratuito)
- **[JAWS](https://www.freedomscientific.com/products/software/jaws/)** (Windows)
- **[VoiceOver](https://www.apple.com/accessibility/vision/)** (macOS/iOS)
- **[TalkBack](https://support.google.com/accessibility/android/answer/6283677)** (Android)

---

<div align="center">

**Desenvolvido por**: Equipe DATAMETRIA Accessibility
**Última Atualização**: [DD/MM/AAAA]
**Versão**: 1.0.0

---

## Accessibility review completo! Aplicação inclusiva! ♿

</div>
