# 🔄 Plano de Refatoração - Estrutura Unity

**Data**: 17/11/2025  
**Autor**: Vander Loto

---

## 📊 Situação Atual

```
Jepp/Assets/
├── JeppGame/          → Código do jogo (misturado)
├── Scenes/            → Cena sample Unity
├── Settings/          → Configs URP
├── Resources/         → Vazio
├── TextMesh Pro/      → Plugin TMP
└── TutorialInfo/      → Tutorial Unity
```

## 🎯 Estrutura Alvo (DATAMETRIA Standards)

```
Jepp/Assets/
├── _Project/
│   ├── Scripts/
│   │   ├── Core/
│   │   ├── Gameplay/
│   │   ├── UI/
│   │   └── Systems/
│   ├── Scenes/
│   ├── Prefabs/
│   └── Config/
├── _Platform/
│   ├── Android/
│   ├── iOS/
│   ├── Oculus/
│   ├── WebGL/
│   └── Shared/
├── _Art/
│   ├── Models/
│   ├── Materials/
│   ├── Textures/
│   └── Audio/
├── _ThirdParty/
│   └── TextMeshPro/
└── _Tests/
    ├── EditMode/
    └── PlayMode/
```

## 📋 Tarefas de Migração

### Fase 1: Criar Estrutura Base
- [ ] Criar pasta `_Project/`
- [ ] Criar pasta `_Platform/`
- [ ] Criar pasta `_Art/`
- [ ] Criar pasta `_ThirdParty/`
- [ ] Criar pasta `_Tests/`

### Fase 2: Migrar Assets
- [ ] Mover `JeppGame/Scripts/` → `_Project/Scripts/`
- [ ] Mover `JeppGame/Scenes/` → `_Project/Scenes/`
- [ ] Mover `JeppGame/Models/` → `_Art/Models/`
- [ ] Mover `JeppGame/Mat/` → `_Art/Materials/`
- [ ] Mover `JeppGame/Sounds/` → `_Art/Audio/`
- [ ] Mover `TextMesh Pro/` → `_ThirdParty/TextMeshPro/`
- [ ] Mover `Settings/` → `_Platform/Shared/Settings/`

### Fase 3: Organizar Scripts
- [ ] Criar `_Project/Scripts/Core/` (GameManager, General)
- [ ] Criar `_Project/Scripts/Gameplay/` (Interactable, ShelfStore)
- [ ] Criar `_Project/Scripts/UI/` (VirtualJoystick)
- [ ] Criar `_Project/Scripts/Player/` (FPS Controller, Camera)

### Fase 4: Configurar Plataformas
- [ ] Criar `_Platform/Android/` (configs mobile)
- [ ] Criar `_Platform/iOS/` (configs iOS)
- [ ] Criar `_Platform/Oculus/` (configs VR)
- [ ] Criar `_Platform/WebGL/` (configs web)

### Fase 5: Limpeza
- [ ] Deletar `Scenes/SampleScene.unity`
- [ ] Deletar `TutorialInfo/`
- [ ] Deletar `Resources/` (se vazio)
- [ ] Atualizar referências no código

## ⚠️ Cuidados

1. **Fazer lock de todas as cenas antes de mover**
   ```bash
   git lfs lock Jepp/Assets/JeppGame/Scenes/*.unity
   ```

2. **Testar após cada fase**
   - Abrir Unity e verificar erros
   - Testar build Android

3. **Commit incremental**
   ```bash
   git add .
   git commit -m "refactor: fase 1 - criar estrutura base"
   ```

## 📅 Cronograma

- **Fase 1**: 30min
- **Fase 2**: 1h
- **Fase 3**: 1h
- **Fase 4**: 30min
- **Fase 5**: 30min

**Total estimado**: 3h30min

---

**Status**: 🟡 Planejado  
**Próximo passo**: Executar Fase 1
