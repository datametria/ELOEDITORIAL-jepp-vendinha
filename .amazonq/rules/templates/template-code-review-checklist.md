# Checklist de Code Review

<div align="center">

## Checklist Completo para Revisão de Código

[![Code Review](https://img.shields.io/badge/code--review-checklist-blue)](https://github.com/datametria/standards)
[![Quality](https://img.shields.io/badge/quality-assured-green)](https://github.com/datametria/standards)
[![DATAMETRIA](https://img.shields.io/badge/DATAMETRIA-Standards-blue)](https://github.com/datametria/standards)

[🔍 Funcionalidade](#funcionalidade) • [🏗️ Arquitetura](#arquitetura-e-design) • [🔒 Segurança](#seguranca) •
[⚡ Performance](#performance) • [🧪 Testes](#testes)

</div>

---

## 📋 Informações da Revisão

| Campo | Descrição |
|-------|-----------|
| **Pull Request** | [#123 - Título do PR] |
| **Autor** | [Nome do desenvolvedor] |
| **Reviewer** | [Nome do revisor] |
| **Data da Revisão** | [DD/MM/AAAA] |
| **Tipo de Mudança** | [Feature/Bug Fix/Refactor/Docs] |
| **Complexidade** | [Baixa/Média/Alta] |
| **Linhas Alteradas** | [+X -Y] |

---

## 🔍 Funcionalidade

### Requisitos e Lógica de Negócio

- [ ] **Requisitos atendidos**: Código implementa todos os requisitos especificados
- [ ] **Lógica correta**: Algoritmos e fluxos estão corretos
- [ ] **Edge cases**: Casos extremos foram considerados e tratados
- [ ] **Validações**: Inputs são validados adequadamente
- [ ] **Outputs**: Retornos estão no formato esperado
- [ ] **Comportamento esperado**: Funcionalidade se comporta conforme especificado

### Usabilidade

- [ ] **Interface intuitiva**: UI/UX é clara e fácil de usar
- [ ] **Mensagens de erro**: Erros são informativos e úteis
- [ ] **Feedback visual**: Loading states e confirmações adequadas
- [ ] **Acessibilidade**: Componentes são acessíveis (WCAG 2.1)

---

## 🏗️ Arquitetura e Design

### Estrutura do Código

- [ ] **Single Responsibility**: Cada função/classe tem uma responsabilidade
- [ ] **DRY Principle**: Não há duplicação desnecessária de código
- [ ] **SOLID Principles**: Princípios SOLID são seguidos
- [ ] **Separation of Concerns**: Responsabilidades estão bem separadas
- [ ] **Modularidade**: Código está bem modularizado

### Design Patterns

- [ ] **Patterns apropriados**: Padrões de design são usados corretamente
- [ ] **Anti-patterns evitados**: Não há anti-patterns conhecidos
- [ ] ## Arquitetura consistente: Segue arquitetura estabelecida do projeto

### Nomenclatura

- [ ] **Nomes descritivos**: Variáveis, funções e classes têm nomes claros
- [ ] **Convenções seguidas**: Nomenclatura segue padrões do projeto
- [ ] **Consistência**: Nomenclatura é consistente em todo o código
- [ ] **Sem abreviações**: Evita abreviações desnecessárias

```python
# ✅ Bom
def calculate_monthly_interest_rate(annual_rate: float) -> float:
    return annual_rate / 12

# ❌ Ruim
def calc_mir(ar: float) -> float:
    return ar / 12
```

---

## 📝 Qualidade do Código

### Legibilidade

- [ ] **Código auto-explicativo**: Código é fácil de entender
- [ ] **Comentários úteis**: Comentários explicam o "porquê", não o "como"
- [ ] **Formatação consistente**: Indentação e espaçamento corretos
- [ ] **Tamanho das funções**: Funções não são muito longas (< 50 linhas)
- [ ] **Complexidade ciclomática**: Funções não são muito complexas

### Documentação

- [ ] **Docstrings/JSDoc**: Funções públicas estão documentadas
- [ ] **Type hints**: Tipos estão especificados (Python/TypeScript)
- [ ] **README atualizado**: Documentação do projeto está atualizada
- [ ] **Comentários inline**: Código complexo tem comentários explicativos

```python
def process_payment(
    amount: float,
    payment_method: str,
    user_id: int
) -> PaymentResult:
    """Processa pagamento para usuário.

    Args:
        amount: Valor do pagamento em reais
        payment_method: Método de pagamento ('credit', 'debit', 'pix')
        user_id: ID único do usuário

    Returns:
        PaymentResult: Resultado do processamento

    Raises:
        ValueError: Se amount for negativo ou payment_method inválido
        UserNotFoundError: Se usuário não existir
    """
```

### Manutenibilidade

- [ ] **Baixo acoplamento**: Módulos são independentes
- [ ] **Alta coesão**: Elementos relacionados estão juntos
- [ ] **Facilidade de mudança**: Código pode ser modificado facilmente
- [ ] **Extensibilidade**: Código pode ser estendido sem grandes mudanças

---

## 🔒 Segurança

### Validação e Sanitização

- [ ] **Input validation**: Todos os inputs são validados
- [ ] **SQL injection**: Queries usam prepared statements
- [ ] **XSS prevention**: Outputs são sanitizados
- [ ] **CSRF protection**: Proteção contra CSRF implementada
- [ ] **Authentication**: Autenticação é verificada onde necessário
- [ ] **Authorization**: Autorização é verificada adequadamente

### Dados Sensíveis

- [ ] **Sem hardcoded secrets**: Não há credenciais no código
- [ ] **Logs seguros**: Dados sensíveis não são logados
- [ ] **Criptografia**: Dados sensíveis são criptografados
- [ ] **HTTPS**: Comunicação usa HTTPS
- [ ] **Headers de segurança**: Headers de segurança estão configurados

```python
# ✅ Bom
password_hash = bcrypt.hashpw(password.encode('utf-8'), bcrypt.gensalt())

# ❌ Ruim
password = "admin123"  # Hardcoded password
```

## LGPD/GDPR Compliance

- [ ] **Consentimento**: Coleta de dados tem consentimento
- [ ] **Minimização**: Apenas dados necessários são coletados
- [ ] **Retenção**: Política de retenção é respeitada
- [ ] **Direito ao esquecimento**: Funcionalidade de exclusão implementada

---

## ⚡ Performance

### Eficiência Algorítmica

- [ ] **Complexidade adequada**: Algoritmos têm complexidade apropriada
- [ ] **Estruturas de dados**: Estruturas de dados são eficientes
- [ ] **Loops otimizados**: Loops são eficientes e necessários
- [ ] **Recursão controlada**: Recursão tem condições de parada claras

### Otimizações

- [ ] **Database queries**: Queries são otimizadas
- [ ] **N+1 queries**: Problema N+1 é evitado
- [ ] **Caching**: Cache é usado onde apropriado
- [ ] **Lazy loading**: Carregamento sob demanda implementado
- [ ] **Memory leaks**: Não há vazamentos de memória

```python
# ✅ Bom - Query otimizada
users = User.objects.select_related('profile').filter(active=True)

# ❌ Ruim - N+1 queries
users = User.objects.filter(active=True)
for user in users:
    print(user.profile.name)  # Query adicional para cada user
```

## Frontend Performance

- [ ] **Bundle size**: Tamanho do bundle é otimizado
- [ ] **Code splitting**: Código é dividido adequadamente
- [ ] **Image optimization**: Imagens são otimizadas
- [ ] **Critical CSS**: CSS crítico é inline
- [ ] **Web Vitals**: Core Web Vitals são considerados

---

## 🧪 Testes

### Cobertura de Testes

- [ ] **Unit tests**: Testes unitários cobrem lógica principal
- [ ] **Integration tests**: Testes de integração para fluxos importantes
- [ ] **Edge cases**: Casos extremos são testados
- [ ] **Error scenarios**: Cenários de erro são testados
- [ ] **Coverage adequado**: Cobertura de testes > 80%

### Qualidade dos Testes

- [ ] **Testes independentes**: Testes não dependem uns dos outros
- [ ] **Nomes descritivos**: Nomes dos testes são claros
- [ ] **Arrange-Act-Assert**: Estrutura AAA é seguida
- [ ] **Mocks apropriados**: Mocks são usados corretamente
- [ ] **Testes rápidos**: Testes executam rapidamente

```python
def test_calculate_discount_with_valid_coupon():
    # Arrange
    price = 100.0
    coupon = Coupon(code="SAVE20", discount_rate=0.20)

    # Act
    discount = calculate_discount(price, coupon)

    # Assert
    assert discount == 20.0
```

### Testes de Regressão

- [ ] **Existing tests pass**: Testes existentes continuam passando
- [ ] **No breaking changes**: Mudanças não quebram funcionalidades existentes
- [ ] **Backward compatibility**: Compatibilidade com versões anteriores

---

## 🌐 Frontend Específico

### React/Vue.js Components

- [ ] **Component structure**: Componentes são bem estruturados
- [ ] **Props validation**: Props são validadas
- [ ] **State management**: Estado é gerenciado adequadamente
- [ ] **Lifecycle methods**: Métodos de ciclo de vida são usados corretamente
- [ ] **Hooks usage**: Hooks são usados apropriadamente (React)

### CSS/Styling

- [ ] **Responsive design**: Design é responsivo
- [ ] **CSS organization**: CSS está bem organizado
- [ ] **No inline styles**: Estilos inline são evitados
- [ ] **Consistent spacing**: Espaçamento é consistente
- [ ] **Cross-browser**: Funciona em diferentes browsers

---

## 🐍 Backend Específico

### API Design

- [ ] **RESTful principles**: APIs seguem princípios REST
- [ ] **HTTP status codes**: Códigos de status corretos
- [ ] **Error responses**: Respostas de erro são padronizadas
- [ ] **Versioning**: Versionamento de API é considerado
- [ ] **Rate limiting**: Rate limiting é implementado

### Database

- [ ] **Schema design**: Schema é bem projetado
- [ ] **Indexes**: Índices apropriados são criados
- [ ] **Migrations**: Migrações são seguras e reversíveis
- [ ] **Transactions**: Transações são usadas onde necessário
- [ ] **Connection pooling**: Pool de conexões é configurado

---

## 📱 Mobile Específico

### React Native/Flutter

- [ ] **Platform-specific code**: Código específico de plataforma é necessário
- [ ] **Performance**: App é otimizado para mobile
- [ ] **Memory usage**: Uso de memória é eficiente
- [ ] **Battery optimization**: Otimizações de bateria implementadas
- [ ] **Offline support**: Funcionalidade offline quando apropriada

### UI/UX Mobile

- [ ] **Touch targets**: Alvos de toque são adequados (44px+)
- [ ] **Gestures**: Gestos são intuitivos
- [ ] **Loading states**: Estados de carregamento são claros
- [ ] **Error handling**: Erros são tratados graciosamente
- [ ] **Accessibility**: Acessibilidade mobile implementada

---

## 🔧 DevOps e Infraestrutura

### CI/CD

- [ ] **Pipeline configuration**: Pipeline CI/CD está configurado
- [ ] **Automated tests**: Testes automatizados no pipeline
- [ ] **Code quality gates**: Gates de qualidade implementados
- [ ] **Security scanning**: Scan de segurança automatizado
- [ ] **Deployment strategy**: Estratégia de deploy é segura

### Monitoring e Observabilidade

- [ ] **Logging**: Logs adequados são implementados
- [ ] **Metrics**: Métricas importantes são coletadas
- [ ] **Tracing**: Distributed tracing quando necessário
- [ ] **Alerting**: Alertas são configurados
- [ ] **Health checks**: Health checks são implementados

```python
# ✅ Bom - Logging estruturado
import logging
import json

logger = logging.getLogger(__name__)

def process_order(order_id: str, user_id: str):
    logger.info(
        "Processing order",
        extra={
            "order_id": order_id,
            "user_id": user_id,
            "action": "process_order"
        }
    )
    # ... lógica de processamento
```

---

## 📊 Métricas de Qualidade

### Code Quality Metrics

| Métrica | Valor Atual | Meta | Status |
|---------|-------------|------|--------|
| **Cobertura de Testes** | [XX]% | >80% | ✅/⚠️/❌ |
| **Complexidade Ciclomática** | [X] | <10 | ✅/⚠️/❌ |
| **Duplicação de Código** | [X]% | <5% | ✅/⚠️/❌ |
| **Débito Técnico** | [X]h | <8h | ✅/⚠️/❌ |
| **Vulnerabilidades** | [X] | 0 | ✅/⚠️/❌ |

### Performance Metrics

| Métrica | Valor Atual | Meta | Status |
|---------|-------------|------|--------|
| **Response Time** | [XXX]ms | <200ms | ✅/⚠️/❌ |
| **Memory Usage** | [XX]MB | <100MB | ✅/⚠️/❌ |
| **CPU Usage** | [XX]% | <70% | ✅/⚠️/❌ |
| **Database Queries** | [X] | <5 | ✅/⚠️/❌ |

---

## 📋 Checklist Final

### Aprovação do Code Review

- [ ] **Todos os itens verificados**: Checklist completo foi seguido
- [ ] **Testes passando**: Todos os testes estão passando
- [ ] **CI/CD verde**: Pipeline está passando
- [ ] **Documentação atualizada**: Docs foram atualizadas se necessário
- [ ] **Performance verificada**: Performance foi testada
- [ ] **Segurança validada**: Aspectos de segurança foram verificados

### Ações Pós-Review

- [ ] **Feedback fornecido**: Feedback construtivo foi dado
- [ ] **Melhorias sugeridas**: Sugestões de melhoria foram feitas
- [ ] **Conhecimento compartilhado**: Conhecimento foi compartilhado
- [ ] **Padrões reforçados**: Padrões da equipe foram reforçados

### Decisão Final

- [ ] **✅ APROVADO**: Código está pronto para merge
- [ ] **🔄 APROVADO COM SUGESTÕES**: Código pode ser merged com melhorias futuras
- [ ] **⚠️ MUDANÇAS NECESSÁRIAS**: Código precisa de alterações antes do merge
- [ ] **❌ REJEITADO**: Código não está pronto e precisa de revisão significativa

---

## 📝 Comentários do Reviewer

### Pontos Positivos

```
[Destacar aspectos bem implementados do código]
```

### Áreas de Melhoria

```
[Sugestões construtivas para melhorar o código]
```

### Sugestões Futuras

```
[Ideias para melhorias futuras que não bloqueiam o merge atual]
```

### Recursos de Aprendizado

- [Link para documentação relevante]
- [Artigo sobre best practices]
- [Tutorial sobre técnica específica]

---

## 🔗 Recursos Adicionais

### Ferramentas de Code Review

- **[SonarQube](https://www.sonarqube.org/)**: Análise estática de código
- **[CodeClimate](https://codeclimate.com/)**: Métricas de qualidade
- **[ESLint](https://eslint.org/)**: Linting para JavaScript
- **[Pylint](https://pylint.org/)**: Linting para Python
- **[RuboCop](https://rubocop.org/)**: Linting para Ruby

### Guias de Style

- **[Google Style Guides](https://google.github.io/styleguide/)**
- **[Airbnb JavaScript Style Guide](https://github.com/airbnb/javascript)**
- **[PEP 8 - Python Style Guide](https://pep8.org/)**
- **[Effective Go](https://golang.org/doc/effective_go.html)**

### Best Practices

- **[Clean Code](https://github.com/ryanmcdermott/clean-code-javascript)**
- **[SOLID Principles](https://en.wikipedia.org/wiki/SOLID)**
- **[Design Patterns](https://refactoring.guru/design-patterns)**
- **[Code Review Best Practices](https://google.github.io/eng-practices/review/)**

---

<div align="center">

**Desenvolvido por**: Equipe DATAMETRIA Quality
**Última Atualização**: 15/09/2025
**Versão**: 1.0.0

---

## Code review completo! Qualidade garantida! ✅

</div>ction pooling**: Pool de conexões é configurado

---

## 📱 Mobile Específico

### Flutter/React Native

- [ ] **Widget structure**: Widgets são bem estruturados
- [ ] **State management**: Estado é gerenciado adequadamente
- [ ] **Navigation**: Navegação é intuitiva
- [ ] **Platform differences**: Diferenças de plataforma são consideradas
- [ ] **Performance**: App é performático em dispositivos

### UX Mobile

- [ ] **Touch targets**: Alvos de toque têm tamanho adequado
- [ ] **Loading states**: Estados de carregamento são mostrados
- [ ] **Offline support**: Suporte offline é considerado
- [ ] **Battery usage**: Uso de bateria é otimizado

---

## 🔧 DevOps e Infraestrutura

### Deployment

- [ ] **Environment variables**: Variáveis de ambiente são usadas
- [ ] **Configuration**: Configuração é externalizada
- [ ] **Health checks**: Health checks são implementados
- [ ] **Logging**: Logging adequado é implementado
- [ ] **Monitoring**: Monitoramento é configurado

### Docker/Kubernetes

- [ ] **Dockerfile optimization**: Dockerfile é otimizado
- [ ] **Multi-stage builds**: Builds multi-stage são usados
- [ ] **Security scanning**: Imagens são escaneadas
- [ ] **Resource limits**: Limites de recursos são definidos

---

## 📊 Métricas e Monitoramento

### Observabilidade

- [ ] **Structured logging**: Logs são estruturados
- [ ] **Metrics collection**: Métricas são coletadas
- [ ] **Distributed tracing**: Tracing distribuído é implementado
- [ ] **Error tracking**: Erros são rastreados
- [ ] **Performance monitoring**: Performance é monitorada

### Analytics

- [ ] **User analytics**: Analytics de usuário são implementados
- [ ] **Business metrics**: Métricas de negócio são coletadas
- [ ] **A/B testing**: Testes A/B são considerados

---

## ✅ Checklist Final

### Aprovação Técnica

- [ ] **Funcionalidade**: Código funciona conforme esperado
- [ ] **Qualidade**: Código atende padrões de qualidade
- [ ] **Segurança**: Não há vulnerabilidades conhecidas
- [ ] **Performance**: Performance é adequada
- [ ] **Testes**: Cobertura de testes é suficiente
- [ ] ## Documentação: Código está adequadamente documentado

### Aprovação de Negócio

- [ ] **Requisitos**: Todos os requisitos foram atendidos
- [ ] **UX/UI**: Interface atende expectativas
- [ ] **Compliance**: Atende requisitos regulatórios
- [ ] **Accessibility**: É acessível para todos os usuários

### Próximos Passos

- [ ] **Merge aprovado**: PR pode ser mergeado
- [ ] **Deploy planejado**: Deploy está planejado
- [ ] **Monitoramento**: Monitoramento pós-deploy configurado
- [ ] **Rollback plan**: Plano de rollback definido

---

## 💬 Comentários do Reviewer

### Pontos Positivos

```
[Destacar aspectos bem implementados]
```

### Sugestões de Melhoria

```
[Sugestões específicas para melhorar o código]
```

### Questões e Dúvidas

```
[Perguntas sobre implementação ou decisões técnicas]
```

### Ações Necessárias

```
[Ações que devem ser tomadas antes do merge]
```

---

## 📚 Recursos de Referência

### Padrões DATAMETRIA

- [Coding Standards](template-code-standards.md)
- [Security Guidelines](datametria_std_security.md)
- [Architecture Patterns](datametria_std_web_dev.md)

### Ferramentas de Qualidade

- **SonarQube**: Análise estática de código
- **ESLint/Flake8**: Linting automático
- **Prettier/Black**: Formatação automática
- **Jest/pytest**: Framework de testes

### Checklists Relacionados

- [Security Review](template-security-review-checklist.md)
- [Performance Review](template-performance-review-checklist.md)
- [Accessibility Review](template-accessibility-review-checklist.md)

---

<div align="center">

**Desenvolvido por**: Equipe DATAMETRIA
**Última Atualização**: [DD/MM/AAAA]
**Versão**: 1.0.0

---

## Code review completo! Qualidade garantida! ✅

</div>
