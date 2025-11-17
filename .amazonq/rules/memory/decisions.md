# Jepp Vendinha - Decisões Arquiteturais

**Versão:** 1.0  
**Data:** 17/11/2025  
**Autor:** Vander Loto - CTO DATAMETRIA

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

## [2025-11-10] Unity 6000.2.8f1 como Engine

**Status**: ✅ Aceita

**Contexto**:
Protótipo precisa de engine 3D mobile com boa performance e suporte Android. Prazo curto (11 dias) requer ferramentas maduras.

**Decisão**:
Usar Unity 6000.2.8f1 (versão LTS mais recente) com URP para mobile.

**Consequências**:

- ✅ Ecosystem maduro (Asset Store, documentação)
- ✅ URP otimizado para mobile
- ✅ New Input System para controles touch
- ✅ Build Android nativo
- ⚠️ Tamanho APK maior que engines nativas
- ⚠️ Curva de aprendizado para cliente (se quiser modificar)

**Alternativas Consideradas**:

1. **Unreal Engine** - Rejeitado (overkill para protótipo, APK muito grande)
2. **Godot** - Rejeitado (menos recursos mobile, menos documentação)
3. **Native Android (Java/Kotlin)** - Rejeitado (muito tempo para 3D do zero)

---

## [2025-11-10] Android Único como Target

**Status**: ✅ Aceita

**Contexto**:
Protótipo tem prazo de 11 dias. Multiplataforma aumentaria complexidade e tempo de desenvolvimento.

**Decisão**:
Build apenas para Android (API 24+), deixando iOS e WebGL para fases futuras.

**Consequências**:

- ✅ Foco total em otimização Android
- ✅ Testes em dispositivos reais mais simples
- ✅ Build pipeline único
- ⚠️ Cliente não pode testar em iOS
- ⚠️ Expansão futura requer trabalho adicional

**Alternativas Consideradas**:

1. **Android + iOS** - Rejeitado (dobra tempo de testes e otimização)
2. **WebGL** - Rejeitado (performance inferior, controles touch complexos)

---

## [2025-11-12] URP Mobile Renderer

**Status**: ✅ Aceita

**Contexto**:
Performance é crítica (meta 30+ FPS). Dispositivos Android médios têm GPU limitada.

**Decisão**:
Usar URP (Universal Render Pipeline) com preset Mobile otimizado:
- Render Scale: 0.8
- MSAA: Disabled
- HDR: Disabled
- Shadow Resolution: 512
- Max Lights: 4

**Consequências**:

- ✅ Performance 2x melhor que Built-in
- ✅ Bateria dura mais
- ✅ Suporte a dispositivos antigos
- ⚠️ Qualidade visual reduzida vs HDRP
- ⚠️ Sombras de baixa resolução

**Alternativas Consideradas**:

1. **Built-in Render Pipeline** - Rejeitado (legado, performance inferior)
2. **HDRP** - Rejeitado (muito pesado para mobile)

---

## [2025-11-13] New Input System para Controles

**Status**: ✅ Aceita

**Contexto**:
Controles mobile precisam de joystick virtual + touch para câmera. Input System legado é limitado.

**Decisão**:
Usar New Input System com:
- Virtual Joystick para movimento
- Touch para rotação de câmera
- Input Actions configuráveis

**Consequências**:

- ✅ Controles modernos e responsivos
- ✅ Fácil adicionar novos inputs
- ✅ Suporte a múltiplos dispositivos
- ⚠️ Curva de aprendizado inicial
- ⚠️ Mais complexo que Input.GetAxis()

**Alternativas Consideradas**:

1. **Input System Legado** - Rejeitado (limitado, será depreciado)
2. **Asset Store (Joystick Pack)** - Rejeitado (dependência externa desnecessária)

---

## [2025-11-14] Git LFS para Assets

**Status**: ✅ Aceita

**Contexto**:
Modelos 3D, texturas e áudio são binários grandes. Git normal não é eficiente para isso.

**Decisão**:
Usar Git LFS para:
- *.fbx (modelos 3D)
- *.png, *.jpg (texturas)
- *.mp3, *.wav (áudio)
- *.unity, *.prefab (cenas e prefabs)

**Consequências**:

- ✅ Repositório leve (apenas ponteiros)
- ✅ Clone rápido
- ✅ File locking para .unity e .prefab
- ⚠️ Requer Git LFS instalado
- ⚠️ Custo de storage LFS (se repo privado)

**Alternativas Consideradas**:

1. **Git normal** - Rejeitado (repo ficaria gigante, clone lento)
2. **Dropbox/Google Drive** - Rejeitado (sem versionamento, conflitos)

---

## [2025-11-15] Singleton Pattern para GameManager

**Status**: ✅ Aceita

**Contexto**:
GameManager precisa ser acessível de qualquer script e persistir entre cenas.

**Decisão**:
Implementar Singleton pattern com DontDestroyOnLoad:

```csharp
public static GameManager Instance { get; private set; }

void Awake()
{
    if (Instance == null)
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    else
    {
        Destroy(gameObject);
    }
}
```

**Consequências**:

- ✅ Acesso global fácil (GameManager.Instance)
- ✅ Persiste entre cenas
- ✅ Garante única instância
- ⚠️ Acoplamento global (anti-pattern em projetos grandes)
- ⚠️ Dificulta testes unitários

**Alternativas Consideradas**:

1. **Dependency Injection** - Rejeitado (overkill para protótipo)
2. **ScriptableObject** - Rejeitado (não persiste estado runtime)
3. **Static Class** - Rejeitado (não é MonoBehaviour, sem lifecycle)

---

## [2025-11-16] Raycast para Sistema de Interação

**Status**: ✅ Aceita

**Contexto**:
Jogador precisa interagir com objetos (porta, prateleiras, placeholders). Detecção precisa ser precisa.

**Decisão**:
Usar Raycast da câmera para detectar objetos interativos:
- Ray parte do centro da tela
- Detecta objetos com componente Interactable
- Highlight visual com outline shader
- Touch confirma interação

**Consequências**:

- ✅ Detecção precisa
- ✅ Funciona em qualquer distância
- ✅ Feedback visual claro
- ⚠️ Requer colliders em todos objetos
- ⚠️ Performance depende de número de objetos

**Alternativas Consideradas**:

1. **Trigger Colliders** - Rejeitado (menos preciso, requer proximidade)
2. **UI Buttons** - Rejeitado (quebra imersão 3D)

---

## [2025-11-17] Outline Shader para Highlight

**Status**: ✅ Aceita

**Contexto**:
Objetos interativos precisam de feedback visual claro quando jogador olha para eles.

**Decisão**:
Criar outline shader com Shader Graph:
- Outline branco ao redor do objeto
- Ativa quando raycast detecta
- Desativa quando jogador olha para outro lugar

**Consequências**:

- ✅ Feedback visual claro
- ✅ Não requer assets extras
- ✅ Performance boa (shader simples)
- ⚠️ Requer Shader Graph (URP)
- ⚠️ Não funciona em objetos transparentes

**Alternativas Consideradas**:

1. **Emissive Material** - Rejeitado (menos visível)
2. **Particle Effect** - Rejeitado (mais pesado)
3. **UI Icon** - Rejeitado (quebra imersão)

---

## [2025-11-17] ASTC Compression para Texturas

**Status**: ✅ Aceita

**Contexto**:
Texturas PNG originais são grandes (5-10MB cada). APK precisa ser < 150MB.

**Decisão**:
Comprimir todas texturas para ASTC 6x6:
- Reduz tamanho em 80%
- Mantém qualidade visual aceitável
- Suportado por todos Android modernos

**Consequências**:

- ✅ APK 60% menor
- ✅ Carregamento mais rápido
- ✅ Menos uso de RAM
- ⚠️ Qualidade visual levemente reduzida
- ⚠️ Tempo de build maior (compressão)

**Alternativas Consideradas**:

1. **ETC2** - Rejeitado (qualidade inferior ao ASTC)
2. **PNG sem compressão** - Rejeitado (APK muito grande)

---

**Mantido por:** Vander Loto - vander.loto@datametria.io  
**Próxima revisão:** 20/11/2025 (pré-entrega)
