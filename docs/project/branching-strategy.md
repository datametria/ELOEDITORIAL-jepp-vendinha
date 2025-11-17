# 🌿 Git Branching Strategy - Unity Project

## Estrutura de Branches

```
main (estável - produção)
  ↑
dev (integração - desenvolvimento)
  ↑
feature/* (features individuais)
```

## Regras

### 🔒 Branch `main`
- **Protegida**: Apenas via Pull Request
- **Estável**: Sempre buildável
- **Deploy**: Versões de produção
- **Commits diretos**: ❌ PROIBIDO

### 🔧 Branch `dev`
- **Integração**: Merge de features
- **Testes**: Validação antes de main
- **Commits diretos**: ⚠️ Evitar (usar features)

### ⚡ Branches `feature/*`
- **Nomenclatura**: `feature/nome-da-feature`
- **Exemplos**: 
  - `feature/ar-placement`
  - `feature/vr-interaction`
  - `feature/ui-menu`
- **Ciclo**: Criar → Desenvolver → PR para `dev` → Deletar

## Workflow

```bash
# 1. Criar feature branch
git checkout dev
git pull origin dev
git checkout -b feature/minha-feature

# 2. Desenvolver
git add .
git commit -m "feat: implementa minha feature"

# 3. Push
git push origin feature/minha-feature

# 4. Criar Pull Request para dev
# (via GitHub/GitLab/Bitbucket)

# 5. Após merge, deletar branch
git checkout dev
git pull origin dev
git branch -d feature/minha-feature
```

## 🔐 File Locking (.unity e .prefab)

### Antes de editar cenas/prefabs:

```bash
# Lock arquivo antes de editar
git lfs lock Jepp/Assets/Scenes/MainScene.unity

# Verificar locks ativos
git lfs locks

# Editar arquivo no Unity
# ...

# Commit e push
git add .
git commit -m "feat: atualiza MainScene"
git push

# Unlock após push
git lfs unlock Jepp/Assets/Scenes/MainScene.unity
```

## ⚠️ Evitar Merge Hell

### Arquivos que SEMPRE devem usar lock:
- ✅ `.unity` (cenas)
- ✅ `.prefab` (prefabs)
- ✅ `.asset` (ScriptableObjects)

### Arquivos que podem ser editados simultaneamente:
- ✅ `.cs` (scripts C#)
- ✅ `.md` (documentação)
- ✅ `.json` (configs)

## 📌 Versão Unity Travada

**Versão do projeto**: Unity `6000.2.8f1`

⚠️ **IMPORTANTE**: Todos os devs devem usar a MESMA versão do Unity para evitar corrupção de arquivos.

Verificar em: `Jepp/ProjectSettings/ProjectVersion.txt`
