# ADR-001: Estrutura de Pastas Unity Multiplataforma

**Status**: Aceito  
**Data**: 17/11/2025  
**Autor**: Vander Loto

---

## Contexto

Projeto precisa suportar múltiplas plataformas (Android, iOS, Meta Quest, Pico, WebXR) com código compartilhado e configurações específicas por plataforma.

## Decisão

Adotar estrutura modular com separação clara:

```
Assets/
├── _Project/       → Código agnóstico (gameplay, UI, sistemas)
├── _Platform/      → Configs específicas (Android, iOS, Oculus, WebGL)
├── _Art/           → Assets visuais e áudio
├── _ThirdParty/    → SDKs externos isolados
└── _Tests/         → Testes automatizados
```

## Consequências

### Positivas ✅
- Código reutilizável entre plataformas
- Isolamento de dependências externas
- Facilita builds automatizados
- Reduz conflitos Git

### Negativas ⚠️
- Requer disciplina da equipe
- Curva de aprendizado inicial

## Alternativas Consideradas

1. **Estrutura flat** - Rejeitado (caótico em multiplataforma)
2. **Projetos separados** - Rejeitado (duplicação de código)

## Referências

- [DATAMETRIA Unity Standards](.amazonq/rules/stacks/datametria_std_unity_ar_vr.md)
