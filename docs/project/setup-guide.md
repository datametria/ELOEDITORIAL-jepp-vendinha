# 🚀 Setup Completo - Unity + Git + VSCode

**Autor**: Vander Loto  
**Data**: 17/11/2025  
**Projeto**: ELOEDITORIAL - Jepp Vendinha

---

## 📦 O que é versionado

✅ **APENAS 3 pastas:**
```
/Assets          → Scripts, cenas, prefabs, materiais
/Packages        → Dependências Unity
/ProjectSettings → Configurações do projeto
```

❌ **NÃO versionar (gerado localmente):**
```
/Library/        → Cache Unity (reconstruído automaticamente)
/Temp/           → Arquivos temporários
/Logs/           → Logs de execução
/Obj/            → Compilação C#
/UserSettings/   → Preferências pessoais
```

**Resultado**: Repo leve, outro dev reconstrói tudo localmente.

---

## 🔧 Setup Inicial (Primeiro Dev)

### 1. Instalar Git LFS
```bash
git lfs install
```

### 2. Clonar repositório
```bash
git clone https://github.com/seu-usuario/ELOEDITORIAL-jepp-vendinha.git
cd ELOEDITORIAL-jepp-vendinha
```

### 3. Baixar arquivos LFS
```bash
git lfs pull
```

### 4. Abrir no Unity
- Unity Hub → **Add** → Selecionar pasta `Jepp/`
- Unity reconstrói `/Library/` automaticamente (5-10min primeira vez)

### 5. Configurar VSCode (opcional)
- Instalar extensão: **C# Dev Kit**
- VSCode = Editor C# + Git cockpit
- Unity = Engine (abrir via Unity Hub)

---

## 👥 Setup para Outro Dev

**Fluxo enxuto:**

```bash
# 1. Instalar Git LFS
git lfs install

# 2. Clonar
git clone https://github.com/seu-usuario/ELOEDITORIAL-jepp-vendinha.git
cd ELOEDITORIAL-jepp-vendinha

# 3. Baixar assets LFS
git lfs pull

# 4. Abrir Unity Hub → Add → pasta Jepp/
# Unity reconstrói Library/ automaticamente
```

**Pronto!** Projeto funcionando em 10-15min.

---

## 🌿 Workflow Diário

### Antes de começar a trabalhar:

```bash
# 1. Sincronizar
git checkout dev
git pull origin dev

# 2. Criar feature branch
git checkout -b feature/minha-feature
```

### Durante o desenvolvimento:

```bash
# 3. Fazer lock de cenas/prefabs (IMPORTANTE!)
git lfs lock Jepp/Assets/Scenes/MainScene.unity

# 4. Editar no Unity
# ...

# 5. Commit
git add .
git commit -m "feat: implementa minha feature"

# 6. Push
git push origin feature/minha-feature

# 7. Unlock
git lfs unlock Jepp/Assets/Scenes/MainScene.unity
```

### Finalizar feature:

```bash
# 8. Criar Pull Request para dev (via GitHub)
# 9. Após merge, deletar branch local
git checkout dev
git pull origin dev
git branch -d feature/minha-feature
```

---

## 🔒 3 Regras de Ouro (Evita Conflitos)

### 1. **Lock antes de editar cenas/prefabs**
```bash
# SEMPRE fazer lock antes de editar:
git lfs lock Jepp/Assets/Scenes/MainScene.unity
git lfs lock Jepp/Assets/Prefabs/Player.prefab
```

### 2. **Sincronizar antes de abrir Unity**
```bash
# SEMPRE antes de abrir Unity:
git pull origin dev
```

### 3. **Trabalhar em feature branches**
```bash
# NUNCA trabalhar direto em main ou dev:
git checkout -b feature/nome-da-feature
```

---

## 🛠️ Comandos Úteis

### Git LFS
```bash
# Ver locks ativos
git lfs locks

# Ver arquivos LFS
git lfs ls-files

# Unlock forçado (se dev saiu sem unlock)
git lfs unlock --force Jepp/Assets/Scenes/MainScene.unity
```

### Git Básico
```bash
# Status
git status

# Ver branches
git branch -a

# Trocar branch
git checkout dev

# Atualizar
git pull origin dev

# Ver histórico
git log --oneline --graph
```

### VSCode como Git Cockpit
- **Source Control** (Ctrl+Shift+G): Ver mudanças
- **Timeline**: Histórico de arquivo
- **GitLens** (extensão): Blame, histórico avançado

---

## ⚠️ Troubleshooting

### Unity não abre projeto
```bash
# Deletar Library e reconstruir
rm -rf Jepp/Library/
# Abrir Unity Hub → Open → Jepp/
```

### Conflito em .unity ou .prefab
```bash
# Aceitar versão remota
git checkout --theirs Jepp/Assets/Scenes/MainScene.unity
git add .
git commit -m "fix: resolve merge conflict"
```

### Arquivo LFS não baixou
```bash
git lfs pull
```

### Lock travado
```bash
# Ver quem tem lock
git lfs locks

# Forçar unlock (cuidado!)
git lfs unlock --force arquivo.unity
```

---

## 📊 Estrutura do Projeto

```
ELOEDITORIAL-jepp-vendinha/
├── .git/                    # Git (não mexer)
├── .gitignore              # Ignora Library/, Temp/, etc
├── .gitattributes          # Git LFS config
├── README.md               # Visão geral
├── BRANCHING.md            # Estratégia de branches
├── SETUP.md                # Este arquivo
└── Jepp/                   # Projeto Unity
    ├── Assets/             ✅ Versionado
    ├── Packages/           ✅ Versionado
    ├── ProjectSettings/    ✅ Versionado
    ├── Library/            ❌ Gerado localmente
    ├── Temp/               ❌ Gerado localmente
    ├── Logs/               ❌ Gerado localmente
    └── UserSettings/       ❌ Gerado localmente
```

---

## 🎯 Checklist de Verificação

### Antes de commitar:
- [ ] Fiz lock de `.unity` e `.prefab`?
- [ ] Testei no Unity (sem erros)?
- [ ] Commit message descritivo?
- [ ] Push feito?
- [ ] Unlock feito?

### Antes de abrir Unity:
- [ ] `git pull origin dev`?
- [ ] Estou na branch correta?
- [ ] Sem conflitos pendentes?

### Antes de criar PR:
- [ ] Feature completa e testada?
- [ ] Sem arquivos de `Library/` ou `Temp/`?
- [ ] Branch atualizada com `dev`?

---

## 📚 Documentação Relacionada

- [README.md](README.md) - Visão geral do projeto
- [BRANCHING.md](BRANCHING.md) - Estratégia de branches detalhada
- [DATAMETRIA Unity Standards](.amazonq/rules/stacks/datametria_std_unity_ar_vr.md)

---

**Versão Unity**: `6000.2.8f1` (OBRIGATÓRIO - mesma versão para todos)  
**Mantido por**: Vander Loto - vander.loto@datametria.io
