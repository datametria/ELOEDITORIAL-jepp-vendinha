# 🎮 ELOEDITORIAL - Jepp Vendinha (Protótipo Livro 1)

**Projeto**: Protótipo Gamificação - Livro 1 Projeto Jepp  
**Cliente**: ELOEDITORIAL  
**Data Início**: 10/11/2025  
**Data Entrega**: 21/11/2025  
**Prazo**: 11 dias  
**Autor**: Vander Loto - CTO DATAMETRIA

---

## 🎯 Visão do Projeto

Protótipo de gamificação educacional para o Livro 1 do Projeto Jepp, focado em crianças de 6-7 anos. Experiência imersiva 3D de uma vendinha virtual com personagem Pamela e atividades interativas.

### Escopo do Protótipo

**Ambientes:**
- ✅ Visual externo da vendinha
- ✅ Visual interno da vendinha

**Personagens:**
- ✅ Pamela (personagem do livro)

**Interatividade:**
- ✅ 2 placeholders para atividades educacionais (a definir)
- ✅ Navegação 3D (FPS mobile)
- ✅ Sistema de interação com objetos

**Público-Alvo:**
- 👶 Crianças de 6-7 anos
- 📚 Alunos do Projeto Jepp - Livro 1

### Plataforma

| Plataforma | Status | Prioridade | Observação |
|------------|--------|------------|------------|
| **Android** | 🟢 Ativo | Alta | Único build do protótipo |
| **iOS** | ⏸️ Fora do escopo | - | Fase futura |
| **VR/AR** | ⏸️ Fora do escopo | - | Fase futura |

---

## 🏗️ Stack Tecnológico

- **Engine**: Unity 6000.2.8f1
- **Render Pipeline**: URP (Universal Render Pipeline)
- **Input**: New Input System (Mobile Touch + Virtual Joystick)
- **Audio**: Unity Audio System
- **Versionamento**: Git + Git LFS
- **Target**: Android (API Level 24+)

---

## 📦 Assets do Protótipo

### Modelos 3D
- ✅ Vendinha_Externa.fbx (visual externo)
- ✅ Vendinha_Interna.fbx (visual interno)
- ✅ Personagem Pamela (a implementar)

### Materiais e Texturas
- ✅ Atlas_Jepp.png
- ✅ extJANELA.png
- ✅ PlacasJANELAext.png
- ✅ TEXTO_ext.png

### Áudio
- ✅ Música ambiente (Animal Crossing style)
- ✅ SFX: sino (bell.mp3)
- ✅ SFX: porta (door.mp3)
- ✅ Ambiente: natureza (nature.mp3)

### Scripts Implementados
- ✅ GameManager.cs (gerenciamento geral)
- ✅ MobileFPSController_InputSystem.cs (controle mobile)
- ✅ InteractionController.cs (sistema de interação)
- ✅ Interactable.cs (objetos interativos)
- ✅ ShelfStore.cs (prateleiras da loja)
- ✅ VirtualJoystick.cs (controle touch)
- ✅ CameraBobbing.cs (movimento de câmera)

## 📋 Funcionalidades do Protótipo

### ✅ Implementado
1. **Navegação Mobile**
   - Virtual joystick para movimento
   - Controle de câmera por touch
   - Sistema FPS adaptado para mobile

2. **Ambientes 3D**
   - Cena externa da vendinha
   - Cena interna da vendinha
   - Transição entre ambientes

3. **Sistema de Interação**
   - Highlight de objetos interativos
   - Feedback visual (outline shader)
   - Sistema de raycast para detecção

4. **Áudio Imersivo**
   - Música de fundo ambiente
   - Efeitos sonoros contextuais

### 🟡 Placeholders (A Implementar)
1. **Atividade Educacional 1**
   - Placeholder preparado
   - Aguardando definição pedagógica

2. **Atividade Educacional 2**
   - Placeholder preparado
   - Aguardando definição pedagógica

### 🔮 Fase Futura (Fora do Protótipo)
- Personagem Pamela animado
- Mais atividades educacionais
- Sistema de pontuação/recompensas
- Multiplataforma (iOS, WebGL)
- Integração com backend

## 📅 Cronograma

| Data | Marco | Status |
|------|-------|--------|
| 10/11/2025 | Início do projeto | ✅ Concluído |
| 15/11/2025 | Ambientes 3D + Navegação | ✅ Concluído |
| 17/11/2025 | Sistema de interação | ✅ Concluído |
| 19/11/2025 | Placeholders atividades | 🟡 Em andamento |
| 20/11/2025 | Testes e ajustes | 🟡 Planejado |
| 21/11/2025 | **Entrega do protótipo** | 🎯 Deadline |

## 🎯 Critérios de Sucesso

### Obrigatórios (MVP)
- ✅ Build Android funcional
- ✅ Navegação mobile fluida
- ✅ Visual externo + interno da vendinha
- ✅ Sistema de interação básico
- ✅ 2 placeholders para atividades
- ⏳ Performance adequada (30+ FPS em dispositivos médios)

### Desejáveis
- ⏳ Personagem Pamela integrado
- ⏳ Tutorial inicial
- ⏳ Feedback visual/sonoro polido

## 🎨 Diretrizes de Design

### Público-Alvo: Crianças 6-7 anos
- 🎨 Visual colorido e amigável
- 🎮 Controles simples e intuitivos
- 🔊 Áudio alegre e não-invasivo
- ⏱️ Sessões curtas (5-10min)
- 🏆 Feedback positivo constante

### Acessibilidade
- Botões grandes (touch-friendly)
- Contraste adequado
- Instruções visuais claras
- Sem texto complexo

---

## 👥 Equipe

**Desenvolvimento**: Vander Loto - CTO DATAMETRIA  
**Cliente**: ELOEDITORIAL  
**Contato**: vander.loto@datametria.io

## 📝 Notas Técnicas

### Otimizações Mobile
- URP Mobile Renderer configurado
- Texturas otimizadas (compressão ASTC)
- LOD system para modelos 3D
- Occlusion culling ativo
- Target: 30-60 FPS em dispositivos médios

### Requisitos Android
- **API Level**: 24+ (Android 7.0+)
- **RAM**: 2GB mínimo
- **Storage**: 150MB
- **GPU**: OpenGL ES 3.0+

---

**Última Atualização**: 17/11/2025  
**Status**: 🟡 Em Desenvolvimento (4 dias para entrega)
