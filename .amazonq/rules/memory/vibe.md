# Jepp Vendinha - Cultura da Equipe

**Versão:** 1.0  
**Data:** 17/11/2025  
**Autor:** Vander Loto - CTO DATAMETRIA

---

## 🎭 Estilo de Colaboração

### Solo Development com AI-First

**Modelo**: 1 desenvolvedor + Amazon Q Developer

**Princípios**:
- Desenvolvimento rápido e iterativo
- Foco em MVP funcional
- Documentação contínua
- Qualidade sobre quantidade
- Deadline-driven (21/11/2025)

---

## 💬 Tom e Linguagem

### Documentação

- **Técnica**: Clara, objetiva, com exemplos de código
- **Formato**: Markdown com diagramas Mermaid
- **Idioma**: Português (documentação) + Inglês (código)
- **Estilo**: DATAMETRIA Standards

### Código

- **Naming**: PascalCase (classes), camelCase (métodos/variáveis)
- **Comentários**: Apenas quando necessário, código autoexplicativo
- **Commits**: Conventional Commits (feat:, fix:, docs:, refactor:)

---

## 🤝 Valores da Equipe

### 1. Entrega no Prazo

**O que significa**: Deadline 21/11/2025 é inegociável

**Como praticamos**: 
- Escopo MVP bem definido
- Features desejáveis são opcionais
- Testes manuais priorizados
- Build diário para validação

### 2. Qualidade Técnica

**O que significa**: Código limpo, performático e manutenível

**Como praticamos**:
- URP otimizado para mobile
- Texturas comprimidas (ASTC)
- LOD system para modelos 3D
- Performance 30+ FPS garantida

### 3. Foco no Usuário

**O que significa**: Crianças 6-7 anos conseguem usar

**Como praticamos**:
- Controles grandes e intuitivos
- Feedback visual claro (highlight)
- Áudio alegre e não-invasivo
- Sessões curtas (5-10min)

---

## 🔄 Workflow de Desenvolvimento

### 1. Planejamento

**Responsável**: Vander Loto  
**Duração**: 1 dia (10/11)

**Processo**:
1. Definir escopo MVP com cliente
2. Criar cronograma (11 dias)
3. Estruturar projeto Unity
4. Configurar Git + Git LFS

### 2. Desenvolvimento

**Responsável**: Vander Loto  
**Duração**: 7 dias (11-17/11)

**Processo**:
1. Implementar feature
2. Testar no Unity Editor
3. Commit incremental
4. Build Android diário
5. Documentar decisões

### 3. Testes

**Responsável**: Vander Loto  
**Duração**: 2 dias (18-19/11)

**Processo**:
1. Testes manuais em 3 dispositivos
2. Ajustes de performance
3. Validação de controles
4. Feedback visual/sonoro

### 4. Entrega

**Responsável**: Vander Loto  
**Duração**: 1 dia (20-21/11)

**Processo**:
1. Build final otimizado
2. Testes de aceitação
3. Documentação final
4. Entrega ao cliente

---

## 🎯 Metodologia

### Desenvolvimento Iterativo

**Ciclo**: 2-3 dias por feature

1. **Implementação** (60%)
   - Código funcional
   - Testes básicos no Editor

2. **Refinamento** (30%)
   - Otimizações
   - Feedback visual/sonoro
   - Ajustes de UX

3. **Validação** (10%)
   - Build Android
   - Teste em dispositivo
   - Commit + documentação

---

## 📋 Padrões de Código

### Unity C#

```csharp
// ✅ Bom
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    [Header("Scene Management")]
    public string externalSceneName = "External";
    
    private AudioSource audioSource;
    
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}

// ❌ Evitar
public class gamemanager : MonoBehaviour
{
    public string scene1; // Nome vago
    AudioSource a; // Nome curto demais
}
```

### Git Commits

```bash
# ✅ Bom
feat: adiciona sistema de interação com highlight
fix: corrige FPS drop na transição de cenas
docs: atualiza technical specification
refactor: otimiza texturas para ASTC

# ❌ Evitar
update
fix bug
changes
wip
```

---

## 🛠️ Ferramentas

### Desenvolvimento

- **Unity 6000.2.8f1**: Game engine
- **Visual Studio Code**: Editor C#
- **Git + Git LFS**: Versionamento
- **Android Studio**: Build e debug

### Documentação

- **Markdown**: Formato padrão
- **Mermaid**: Diagramas
- **Amazon Q**: Assistente IA

### Testes

- **Unity Profiler**: Performance
- **Android Profiler**: RAM/CPU
- **Dispositivos físicos**: Testes reais

---

## 📊 Métricas de Sucesso

### Desenvolvimento

- ✅ Commits diários
- ✅ Build Android funcional a cada 2 dias
- ✅ Documentação atualizada
- ✅ Performance 30+ FPS

### Entrega

- ✅ Deadline 21/11/2025 cumprido
- ✅ MVP completo e funcional
- ✅ Testes em 3 dispositivos
- ✅ Documentação completa

---

## 🎨 Design Guidelines

### Visual

- Colorido e amigável
- Contraste adequado
- Botões grandes (touch-friendly)
- Feedback visual claro

### Áudio

- Música alegre (Animal Crossing style)
- SFX contextuais (sino, porta)
- Volume balanceado
- Não-invasivo

### UX

- Controles intuitivos (joystick virtual)
- Sessões curtas (5-10min)
- Feedback positivo constante
- Sem texto complexo

---

**Mantido por:** Vander Loto - vander.loto@datametria.io  
**Próxima revisão:** 20/11/2025 (pré-entrega)
