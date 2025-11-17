# Template de Documentação Inline - Google Style

<div align="center">

## Padrão Google Style para Docstrings e Type Hints

[![Python](https://img.shields.io/badge/python-3.8+-blue)](https://python.org)
[![Style](https://img.shields.io/badge/style-google-green)](https://google.github.io/styleguide/pyguide.html)
[![Type Hints](https://img.shields.io/badge/type--hints-PEP484-orange)](https://pep8.org)
[![DATAMETRIA](https://img.shields.io/badge/DATAMETRIA-Standards-blue)](https://github.com/datametria/standards)
[![Amazon Q](https://img.shields.io/badge/Amazon%20Q-Ready-yellow)](https://aws.amazon.com/q/)

[🎯 Visão Geral](#-visão-geral) • [🏗️ Estrutura](#️-estrutura-básica) • [🏛️ Classes](#️-classes) • [🔧 Métodos](#-métodos-e-funções) • [🏷️ Type Hints](#️-type-hints)

</div>

---

## 📋 Índice

- [Visão Geral](#-visão-geral)
- [Estrutura Básica](#️-estrutura-básica)
- [Classes](#️-classes)
- [Métodos e Funções](#-métodos-e-funções)
- [Type Hints](#️-type-hints)
- [Exemplos Práticos](#-exemplos-práticos)
- [Ferramentas](#️-ferramentas)
- [Referências](#-referências)

---

## 🎯 Visão Geral

### Padrão Google Style

O Google Style é um padrão de documentação inline que utiliza docstrings estruturadas para documentar código Python de forma clara e consistente.

### Benefícios

- ✅ **Legibilidade**: Formato claro e estruturado
- ✅ **Automação**: Compatível com ferramentas de documentação
- ✅ **Padronização**: Consistência em todo o projeto
- ✅ **Type Safety**: Integração com type hints

---

## 🏗️ Estrutura Básica

### Template Geral

```python
def funcao_exemplo(parametro1: tipo, parametro2: tipo = valor_padrao) -> tipo_retorno:
    """Descrição breve da função em uma linha.

    Descrição mais detalhada da função, explicando seu propósito,
    comportamento e contexto de uso. Pode ocupar múltiplas linhas.

    Args:
        parametro1 (tipo): Descrição do primeiro parâmetro.
        parametro2 (tipo, optional): Descrição do segundo parâmetro.
            Defaults to valor_padrao.

    Returns:
        tipo_retorno: Descrição do valor retornado.

    Raises:
        TipoExcecao: Descrição de quando esta exceção é lançada.
        OutraExcecao: Descrição de outra exceção possível.

    Example:
        Exemplo básico de uso:

        >>> resultado = funcao_exemplo("valor1", parametro2="valor2")
        >>> print(resultado)
        'resultado_esperado'

    Note:
        Informações adicionais importantes sobre a função.

    Todo:
        * Melhorias futuras planejadas
        * Funcionalidades a serem implementadas
    """
    pass
```

---

## 🏛️ Classes

### Template de Classe

```python
from typing import Optional, List, Dict, Any
from datetime import datetime

class ExemploClasse:
    """Descrição breve da classe.

    Descrição detalhada da classe, seu propósito no sistema,
    responsabilidades e como deve ser utilizada.

    Attributes:
        atributo_publico (str): Descrição do atributo público.
        _atributo_protegido (int): Descrição do atributo protegido.
        CONSTANTE_CLASSE (str): Descrição da constante da classe.

    Example:
        Uso básico da classe:

        >>> instancia = ExemploClasse("valor_inicial")
        >>> resultado = instancia.metodo_principal()
        >>> print(resultado)
        'resultado_esperado'
    """

    CONSTANTE_CLASSE: str = "valor_constante"

    def __init__(
        self,
        parametro_obrigatorio: str,
        parametro_opcional: Optional[int] = None,
        configuracoes: Optional[Dict[str, Any]] = None
    ) -> None:
        """Inicializa uma nova instância da classe.

        Args:
            parametro_obrigatorio (str): Parâmetro obrigatório para inicialização.
            parametro_opcional (int, optional): Parâmetro opcional.
                Defaults to None.
            configuracoes (Dict[str, Any], optional): Dicionário de configurações.
                Defaults to None.

        Raises:
            ValueError: Se parametro_obrigatorio for vazio.
            TypeError: Se configuracoes não for um dicionário.

        Example:
            >>> instancia = ExemploClasse("valor", parametro_opcional=42)
            >>> print(instancia.atributo_publico)
            'valor'
        """
        if not parametro_obrigatorio:
            raise ValueError("parametro_obrigatorio não pode ser vazio")

        self.atributo_publico = parametro_obrigatorio
        self._atributo_protegido = parametro_opcional or 0
        self._configuracoes = configuracoes or {}

    @property
    def propriedade_exemplo(self) -> str:
        """str: Propriedade de exemplo com getter e setter.

        Esta propriedade demonstra como documentar properties.
        O tipo é especificado no início da docstring.

        Returns:
            str: Valor atual da propriedade.

        Example:
            >>> instancia.propriedade_exemplo = "novo_valor"
            >>> print(instancia.propriedade_exemplo)
            'novo_valor'
        """
        return self._valor_interno

    @propriedade_exemplo.setter
    def propriedade_exemplo(self, valor: str) -> None:
        """Define o valor da propriedade.

        Args:
            valor (str): Novo valor para a propriedade.

        Raises:
            ValueError: Se valor for None ou vazio.
        """
        if not valor:
            raise ValueError("Valor não pode ser None ou vazio")
        self._valor_interno = valor
```

---

## 🔧 Métodos e Funções

### Método Simples

```python
def metodo_simples(self, entrada: str) -> bool:
    """Verifica se a entrada é válida.

    Args:
        entrada (str): String a ser validada.

    Returns:
        bool: True se válida, False caso contrário.
    """
    return len(entrada) > 0
```

### Método Complexo

```python
def processar_dados(
    self,
    dados: List[Dict[str, Any]],
    filtros: Optional[Dict[str, Any]] = None,
    ordenar_por: Optional[str] = None,
    reverso: bool = False
) -> List[Dict[str, Any]]:
    """Processa uma lista de dados aplicando filtros e ordenação.

    Este método recebe uma lista de dicionários, aplica filtros opcionais
    e retorna os dados processados e ordenados conforme especificado.

    Args:
        dados (List[Dict[str, Any]]): Lista de dicionários para processar.
        filtros (Dict[str, Any], optional): Filtros a serem aplicados.
            Chaves devem corresponder a campos nos dados.
            Defaults to None.
        ordenar_por (str, optional): Campo para ordenação.
            Defaults to None.
        reverso (bool, optional): Se True, ordena em ordem decrescente.
            Defaults to False.

    Returns:
        List[Dict[str, Any]]: Lista processada e ordenada.

    Raises:
        ValueError: Se dados estiver vazio.
        KeyError: Se ordenar_por não existir nos dados.
        TypeError: Se dados não for uma lista de dicionários.

    Example:
        Processamento básico:

        >>> dados = [{"nome": "João", "idade": 30}, {"nome": "Maria", "idade": 25}]
        >>> filtros = {"idade": lambda x: x > 25}
        >>> resultado = instancia.processar_dados(dados, filtros, "nome")
        >>> print(resultado)
        [{"nome": "João", "idade": 30}]

    Note:
        O método modifica a lista original. Use copy() se precisar preservar
        os dados originais.

    Todo:
        * Adicionar suporte para filtros mais complexos
        * Implementar cache para melhor performance
    """
    if not dados:
        raise ValueError("Lista de dados não pode estar vazia")

    # Implementação do método...
    pass
```

### Função com *args e **kwargs

```python
def funcao_flexivel(
    parametro_obrigatorio: str,
    *args: Any,
    **kwargs: Any
) -> Dict[str, Any]:
    """Função que aceita argumentos variáveis.

    Args:
        parametro_obrigatorio (str): Parâmetro sempre necessário.
        *args: Argumentos posicionais variáveis.
        **kwargs: Argumentos nomeados variáveis.
            Opções suportadas:
            - debug (bool): Ativa modo debug. Defaults to False.
            - timeout (int): Timeout em segundos. Defaults to 30.

    Returns:
        Dict[str, Any]: Dicionário com resultados processados.
            Contém as chaves:
            - 'status': Status da operação ('success' ou 'error')
            - 'data': Dados processados
            - 'metadata': Informações adicionais

    Example:
        >>> resultado = funcao_flexivel("teste", "arg1", "arg2", debug=True)
        >>> print(resultado['status'])
        'success'
    """
    pass
```

---

## 🏷️ Type Hints

### Tipos Básicos

```python
from typing import (
    Any, Optional, Union, List, Dict, Tuple, Set,
    Callable, Iterator, Generator, TypeVar, Generic
)

def exemplos_tipos_basicos(
    string_param: str,
    int_param: int,
    float_param: float,
    bool_param: bool,
    optional_param: Optional[str] = None,
    union_param: Union[str, int] = "default"
) -> None:
    """Demonstra tipos básicos.

    Args:
        string_param (str): Parâmetro string obrigatório.
        int_param (int): Parâmetro inteiro.
        float_param (float): Parâmetro float.
        bool_param (bool): Parâmetro booleano.
        optional_param (str, optional): Parâmetro opcional. Defaults to None.
        union_param (Union[str, int], optional): Aceita string ou int.
            Defaults to "default".
    """
    pass
```

### Tipos Complexos

```python
def exemplos_tipos_complexos(
    lista_strings: List[str],
    dict_dados: Dict[str, Any],
    tupla_coordenadas: Tuple[float, float],
    conjunto_ids: Set[int],
    callback: Callable[[str], bool]
) -> Iterator[Dict[str, Any]]:
    """Demonstra tipos complexos.

    Args:
        lista_strings (List[str]): Lista de strings.
        dict_dados (Dict[str, Any]): Dicionário com valores de qualquer tipo.
        tupla_coordenadas (Tuple[float, float]): Coordenadas x, y.
        conjunto_ids (Set[int]): Conjunto único de IDs.
        callback (Callable[[str], bool]): Função que recebe string e retorna bool.

    Yields:
        Dict[str, Any]: Dicionário processado a cada iteração.

    Example:
        >>> dados = {"chave": "valor"}
        >>> coords = (10.5, 20.3)
        >>> ids = {1, 2, 3}
        >>> callback = lambda x: len(x) > 0
        >>> for item in exemplos_tipos_complexos(["a"], dados, coords, ids, callback):
        ...     print(item)
    """
    for item in lista_strings:
        if callback(item):
            yield {
                "item": item,
                "dados": dict_dados,
                "coordenadas": tupla_coordenadas,
                "ids": list(conjunto_ids)
            }
```

### Tipos Genéricos

```python
T = TypeVar('T')
K = TypeVar('K')
V = TypeVar('V')

class Container(Generic[T]):
    """Container genérico para qualquer tipo.

    Type Parameters:
        T: Tipo dos elementos armazenados no container.

    Attributes:
        items (List[T]): Lista de itens do tipo T.

    Example:
        >>> container_str = Container[str]()
        >>> container_str.add("item")
        >>> print(container_str.get_all())
        ['item']
    """

    def __init__(self) -> None:
        """Inicializa container vazio."""
        self.items: List[T] = []

    def add(self, item: T) -> None:
        """Adiciona item ao container.

        Args:
            item (T): Item a ser adicionado.
        """
        self.items.append(item)

    def get_all(self) -> List[T]:
        """Retorna todos os itens.

        Returns:
            List[T]: Lista com todos os itens.
        """
        return self.items.copy()
```

---

## 📚 Exemplos Práticos

### Classe de Serviço Completa

```python
from typing import Optional, List, Dict, Any
from datetime import datetime
from dataclasses import dataclass
from enum import Enum
import logging

class StatusProcessamento(Enum):
    """Status possíveis para processamento.

    Attributes:
        PENDENTE: Processamento ainda não iniciado.
        EM_ANDAMENTO: Processamento em execução.
        CONCLUIDO: Processamento finalizado com sucesso.
        ERRO: Processamento finalizado com erro.
    """
    PENDENTE = "pendente"
    EM_ANDAMENTO = "em_andamento"
    CONCLUIDO = "concluido"
    ERRO = "erro"

@dataclass
class ResultadoProcessamento:
    """Resultado de um processamento.

    Attributes:
        id_processamento (str): Identificador único do processamento.
        status (StatusProcessamento): Status atual do processamento.
        dados_processados (Optional[Dict[str, Any]]): Dados resultantes.
        erro (Optional[str]): Mensagem de erro, se houver.
        timestamp (datetime): Momento da criação do resultado.
    """
    id_processamento: str
    status: StatusProcessamento
    dados_processados: Optional[Dict[str, Any]] = None
    erro: Optional[str] = None
    timestamp: datetime = None

    def __post_init__(self) -> None:
        """Inicializa timestamp se não fornecido."""
        if self.timestamp is None:
            self.timestamp = datetime.now()

class ProcessadorDados:
    """Serviço para processamento de dados.

    Esta classe fornece funcionalidades para processar diferentes tipos
    de dados de forma assíncrona e com controle de status.

    Attributes:
        logger (logging.Logger): Logger para registrar operações.
        _processamentos_ativos (Dict[str, StatusProcessamento]):
            Mapeamento de processamentos em andamento.

    Example:
        Uso básico do processador:

        >>> processador = ProcessadorDados()
        >>> resultado = processador.processar_dados(
        ...     dados={"chave": "valor"},
        ...     tipo_processamento="validacao"
        ... )
        >>> print(resultado.status)
        StatusProcessamento.CONCLUIDO
    """

    def __init__(self, nivel_log: str = "INFO") -> None:
        """Inicializa o processador de dados.

        Args:
            nivel_log (str, optional): Nível de logging.
                Defaults to "INFO".
        """
        self.logger = logging.getLogger(__name__)
        self.logger.setLevel(getattr(logging, nivel_log.upper()))
        self._processamentos_ativos: Dict[str, StatusProcessamento] = {}

    def processar_dados(
        self,
        dados: Dict[str, Any],
        tipo_processamento: str,
        opcoes: Optional[Dict[str, Any]] = None
    ) -> ResultadoProcessamento:
        """Processa dados conforme tipo especificado.

        Args:
            dados (Dict[str, Any]): Dados a serem processados.
            tipo_processamento (str): Tipo de processamento a ser aplicado.
                Valores suportados: 'validacao', 'transformacao', 'agregacao'.
            opcoes (Dict[str, Any], optional): Opções adicionais para processamento.
                Defaults to None.

        Returns:
            ResultadoProcessamento: Resultado do processamento com status e dados.

        Raises:
            ValueError: Se tipo_processamento não for suportado.
            TypeError: Se dados não for um dicionário.

        Example:
            Processamento de validação:

            >>> dados = {"email": "user@example.com", "idade": 25}
            >>> resultado = processador.processar_dados(
            ...     dados=dados,
            ...     tipo_processamento="validacao"
            ... )
            >>> print(resultado.status == StatusProcessamento.CONCLUIDO)
            True

        Note:
            O processamento é síncrono. Para processamento assíncrono,
            use o método processar_dados_async().
        """
        if not isinstance(dados, dict):
            raise TypeError("Dados devem ser um dicionário")

        tipos_suportados = ["validacao", "transformacao", "agregacao"]
        if tipo_processamento not in tipos_suportados:
            raise ValueError(
                f"Tipo '{tipo_processamento}' não suportado. "
                f"Tipos válidos: {tipos_suportados}"
            )

        id_processamento = self._gerar_id_processamento()
        self._processamentos_ativos[id_processamento] = StatusProcessamento.EM_ANDAMENTO

        try:
            self.logger.info(
                f"Iniciando processamento {id_processamento} "
                f"do tipo '{tipo_processamento}'"
            )

            dados_processados = self._executar_processamento(
                dados, tipo_processamento, opcoes or {}
            )

            self._processamentos_ativos[id_processamento] = StatusProcessamento.CONCLUIDO

            return ResultadoProcessamento(
                id_processamento=id_processamento,
                status=StatusProcessamento.CONCLUIDO,
                dados_processados=dados_processados
            )

        except Exception as e:
            self.logger.error(
                f"Erro no processamento {id_processamento}: {str(e)}"
            )
            self._processamentos_ativos[id_processamento] = StatusProcessamento.ERRO

            return ResultadoProcessamento(
                id_processamento=id_processamento,
                status=StatusProcessamento.ERRO,
                erro=str(e)
            )

    def _gerar_id_processamento(self) -> str:
        """Gera ID único para processamento.

        Returns:
            str: ID único baseado em timestamp.
        """
        import uuid
        return f"proc_{uuid.uuid4().hex[:8]}"

    def _executar_processamento(
        self,
        dados: Dict[str, Any],
        tipo: str,
        opcoes: Dict[str, Any]
    ) -> Dict[str, Any]:
        """Executa o processamento específico.

        Args:
            dados (Dict[str, Any]): Dados a processar.
            tipo (str): Tipo de processamento.
            opcoes (Dict[str, Any]): Opções de processamento.

        Returns:
            Dict[str, Any]: Dados processados.
        """
        # Implementação específica baseada no tipo
        if tipo == "validacao":
            return {"valido": True, "dados_originais": dados}
        elif tipo == "transformacao":
            return {"transformado": True, **dados}
        elif tipo == "agregacao":
            return {"total_campos": len(dados), "dados": dados}
        else:
            raise ValueError(f"Tipo de processamento não implementado: {tipo}")
```

---

## 🛠️ Ferramentas

### Validação e Geração

#### mypy - Type Checking

```bash
# Instalação
pip install mypy

# Verificação de tipos
mypy meu_arquivo.py

# Configuração no pyproject.toml
[tool.mypy]
python_version = "3.8"
warn_return_any = true
warn_unused_configs = true
disallow_untyped_defs = true
```

#### pydocstyle - Validação de Docstrings

```bash
# Instalação
pip install pydocstyle

# Verificação
pydocstyle meu_arquivo.py

# Configuração para Google Style
[tool.pydocstyle]
convention = "google"
```

#### sphinx - Geração de Documentação

```bash
# Instalação
pip install sphinx sphinx-rtd-theme

# Configuração conf.py
extensions = [
    'sphinx.ext.autodoc',
    'sphinx.ext.napoleon',  # Suporte ao Google Style
    'sphinx.ext.viewcode',
]

napoleon_google_docstring = True
napoleon_numpy_docstring = False
```

### Configuração IDE

#### VS Code

```json
{
    "python.linting.enabled": true,
    "python.linting.mypyEnabled": true,
    "python.linting.pydocstyleEnabled": true,
    "python.formatting.provider": "black",
    "autoDocstring.docstringFormat": "google",
    "python.analysis.typeCheckingMode": "strict"
}
```

#### PyCharm

```
Settings > Tools > Python Integrated Tools
> Docstring format: Google

Settings > Editor > Inspections > Python
> Enable "Type checker compatibility"
```

---

## 📚 Referências

### Documentação Oficial

- [Google Python Style Guide](https://google.github.io/styleguide/pyguide.html)
- [PEP 484 - Type Hints](https://www.python.org/dev/peps/pep-0484/)
- [PEP 257 - Docstring Conventions](https://www.python.org/dev/peps/pep-0257/)
- [typing — Support for type hints](https://docs.python.org/3/library/typing.html)

### Ferramentas

| Ferramenta | Propósito | Documentação |
|------------|-----------|-------------|
| **mypy** | Type checking | [mypy-lang.org](http://mypy-lang.org/) |
| **pydocstyle** | Docstring validation | [pydocstyle.org](http://www.pydocstyle.org/) |
| **sphinx** | Documentation generation | [sphinx-doc.org](https://www.sphinx-doc.org/) |
| **black** | Code formatting | [black.readthedocs.io](https://black.readthedocs.io/) |
| **isort** | Import sorting | [pycqa.github.io/isort](https://pycqa.github.io/isort/) |

### Exemplos Adicionais

- [Exemplos Google Style](https://sphinxcontrib-napoleon.readthedocs.io/en/latest/example_google.html)
- [Type Hints Cheat Sheet](https://mypy.readthedocs.io/en/stable/cheat_sheet_py3.html)
- [Python Type Checking Guide](https://realpython.com/python-type-checking/)

---

<div align="center">

**Mantido por**: Equipe de Desenvolvimento DATAMETRIA
**Versão**: 1.2.0
**Última Atualização**: 29/09/2025
**Próxima Revisão**: Dezembro 2025

---

### 📝 GOOGLE STYLE DOCSTRINGS - DOCUMENTAÇÃO INLINE PROFISSIONAL! 🎯

*Para dúvidas sobre documentação: [dev@datametria.io](mailto:dev@datametria.io)*

</div>
