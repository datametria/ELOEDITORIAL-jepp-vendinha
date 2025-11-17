# Jepp Vendinha - Estado Técnico Atual

**Versão:** 1.0  
**Data:** 17/11/2025  
**Autor:** Vander Loto - CTO DATAMETRIA

---

## 📊 Snapshot Técnico

### Versão Atual

**Jepp Vendinha v1.0.0 (Protótipo)**
- Data de Início: 10/11/2025
- Data de Entrega: 21/11/2025
- Status: 🟡 Em Desenvolvimento (4 dias restantes)
- Próxima Revisão: 20/11/2025

---

## 🛠️ Stack Tecnológico

### Engine e Frameworks

| Tecnologia | Versão | Uso | Status |
|------------|--------|-----|--------|
| **Unity** | 6000.2.8f1 | Game engine | ✅ Produção |
| **URP** | 17.0+ | Render pipeline mobile | ✅ Produção |
| **New Input System** | 1.7+ | Controles mobile | ✅ Produção |
| **Git LFS** | 3.4+ | Versionamento assets | ✅ Produção |

### Plataforma

| Tecnologia | Versão | Uso | Status |
|------------|--------|-----|--------|
| **Android** | API 24+ (7.0+) | Target único protótipo | ✅ Produção |
| **IL2CPP** | - | Scripting backend | ✅ Produção |
| **ARM64** | - | Arquitetura | ✅ Produção |

### Assets e Plugins

| Tecnologia | Versão | Uso | Status |
|------------|--------|-----|--------|
| **TextMesh Pro** | 3.0+ | UI text rendering | ✅ Produção |
| **Shader Graph** | 17.0+ | Outline shader | ✅ Produção |

---

## 📈 Métricas Atuais

### Qualidade de Código

| Métrica | Meta | Atual | Tendência |
|---------|------|-------|-----------|
| **Scripts C#** | - | 7 classes | ➡️ |
| **Cenas Unity** | 2 | 2 | ✅ |
| **Modelos 3D** | 2 | 2 | ✅ |
| **Áudio** | 4 | 4 | ✅ |

### Performance

| Métrica | Meta | Atual | Tendência |
|---------|------|-------|-----------|
| **FPS** | 30+ | ~45 | ✅ |
| **APK Size** | < 150MB | ~120MB | ✅ |
| **RAM Usage** | < 500MB | ~380MB | ✅ |
| **Load Time** | < 10s | ~7s | ✅ |

---

## 🎯 Projetos Ativos

### Em Produção

1. **Jepp Vendinha Protótipo**
   - Status: 🟡 Em Desenvolvimento
   - Stack: Unity 6000.2.8f1 + URP + Android
   - Equipe: 1 dev (Vander Loto)
   - Deadline: 21/11/2025

---

## 🔧 Configurações Padrão

### Unity Project Settings

```json
{
  "productName": "Jepp Vendinha",
  "companyName": "ELOEDITORIAL",
  "version": "1.0.0",
  "bundleIdentifier": "com.eloeditorial.jeppvendinha",
  "targetSdkVersion": 33,
  "minSdkVersion": 24,
  "scriptingBackend": "IL2CPP",
  "targetArchitectures": "ARM64"
}
```

### URP Mobile Settings

```csharp
// Mobile_RPAsset.asset
{
  "renderScale": 0.8,
  "msaa": "Disabled",
  "hdr": false,
  "shadowResolution": 512,
  "maxLights": 4,
  "depthTexture": false
}
```

### Git LFS Tracking

```
*.psd
*.png
*.jpg
*.fbx
*.prefab
*.unity
*.asset
*.mp3
*.wav
```

---

## 📦 Assets Implementados

### Modelos 3D
- ✅ Vendinha_Externa.fbx (visual externo)
- ✅ Vendinha_Interna.fbx (visual interno)
- 🟡 Personagem Pamela (a implementar)

### Texturas
- ✅ Atlas_Jepp.png
- ✅ extJANELA.png
- ✅ PlacasJANELAext.png
- ✅ TEXTO_ext.png

### Áudio
- ✅ Música ambiente (Animal Crossing style)
- ✅ SFX: sino (bell.mp3)
- ✅ SFX: porta (door.mp3)
- ✅ Ambiente: natureza (nature.mp3)

### Scripts C#
- ✅ GameManager.cs
- ✅ MobileFPSController_InputSystem.cs
- ✅ InteractionController.cs
- ✅ Interactable.cs
- ✅ ShelfStore.cs
- ✅ VirtualJoystick.cs
- ✅ CameraBobbing.cs

### Cenas
- ✅ External.unity (cena externa)
- ✅ JeppGame.unity (cena interna)

---

## 🏗️ Arquitetura Atual

### Estrutura de Pastas

```
Jepp/Assets/
├── JeppGame/              → Código e assets do jogo
│   ├── Scripts/           → 7 scripts C#
│   ├── Scenes/            → 2 cenas Unity
│   ├── Models/            → 2 modelos FBX
│   ├── Mat/               → Materiais e shaders
│   └── Sounds/            → 4 arquivos de áudio
├── Settings/              → URP configs
└── TextMesh Pro/          → Plugin TMP
```

### Componentes Principais

1. **GameManager** - Singleton, gerenciamento global
2. **MobileFPSController** - Controle FPS mobile
3. **InteractionController** - Sistema de interação raycast
4. **Interactable** - Base para objetos interativos
5. **VirtualJoystick** - Controle touch mobile

---

## 🚧 Pendências Técnicas

### Críticas (Deadline 21/11)
- [ ] Implementar placeholder atividade 1
- [ ] Implementar placeholder atividade 2
- [ ] Testes em 3 dispositivos Android
- [ ] Build final otimizado

### Desejáveis (Fase Futura)
- [ ] Personagem Pamela animado
- [ ] Tutorial inicial
- [ ] Sistema de pontuação
- [ ] Analytics integration

---

## 📊 Métricas de Desenvolvimento

### Progresso Geral

| Fase | Progresso | Status |
|------|-----------|--------|
| **Ambientes 3D** | 100% | ✅ |
| **Navegação Mobile** | 100% | ✅ |
| **Sistema Interação** | 100% | ✅ |
| **Áudio** | 100% | ✅ |
| **Placeholders** | 50% | 🟡 |
| **Testes** | 0% | 🔴 |

### Tempo Investido

- **Total estimado**: 80 horas
- **Investido**: ~60 horas
- **Restante**: ~20 horas
- **Dias restantes**: 4 dias

---

## 🔄 Últimas Mudanças

### 17/11/2025
- ✅ Sistema de interação com highlight implementado
- ✅ Outline shader criado
- ✅ Documentação técnica completa
- ✅ Memory Bank criado

### 15/11/2025
- ✅ Ambientes 3D finalizados
- ✅ Navegação mobile implementada
- ✅ Transição entre cenas funcionando

### 10/11/2025
- ✅ Projeto iniciado
- ✅ Setup Unity + Git LFS
- ✅ Estrutura base criada

---

**Mantido por:** Vander Loto - vander.loto@datametria.io  
**Próxima revisão:** 20/11/2025 (pré-entrega)
