# 🤖 DATAMETRIA Standards - Agents Development v2.0

**Versão:** 2.0.0 | **Última Atualização:** 07/11/2025 | **Autor:** Vander Loto - CTO DATAMETRIA

<div align="center">

![DATAMETRIA Agents](https://img.shields.io/badge/DATAMETRIA-Agents%20Development-blue?style=for-the-badge)

## Diretrizes para Desenvolvimento de Sistemas Multi-Agentes (MAS)

[![AI-First](https://img.shields.io/badge/AI--First-95%25-yellow)](https://aws.amazon.com/q/)
[![Multi-Agent](https://img.shields.io/badge/Multi--Agent-Systems-purple)](https://github.com/datametria/DATAMETRIA-standards)
[![LLM Ready](https://img.shields.io/badge/LLM-Ready-green)](https://github.com/datametria/DATAMETRIA-standards)
[![Version](https://img.shields.io/badge/version-2.0.0-blue)](https://github.com/datametria/DATAMETRIA-standards)
[![Agno Framework](https://img.shields.io/badge/Agno-Framework-orange)](https://github.com/datametria/DATAMETRIA-standards)

[🎯 Visão Geral](#-visão-geral) • [🏗️ Arquitetura](#️-arquitetura-de-agents) • [💬 Comunicação](#-comunicação-agent-to-agent) • [🧠 Componentes v2.0](#-componentes-avançados-v20) • [🔧 Implementação](#-implementação) • [📊 Monitoramento](#-monitoramento-e-observabilidade) • [🔄 Migração](#-migração-v10--v20) • [✅ Checklist](#-checklist-de-desenvolvimento)

</div>

---

## 🎯 Visão Geral

### Definição de Agents

**Agents** são entidades de software autônomas que:

- **Percebem** seu ambiente através de sensores
- **Agem** no ambiente através de atuadores
- **Decidem** autonomamente baseado em objetivos
- **Comunicam** com outros agents para coordenação
- **Aprendem** e se adaptam ao longo do tempo

### Tipos de Agents DATAMETRIA

| Tipo | Descrição | Casos de Uso | Tecnologias |
|------|-----------|--------------|-------------|
| **🧠 LLM Agents** | Agents baseados em modelos de linguagem | Assistentes, análise de texto, geração de conteúdo | OpenAI, Anthropic, Hugging Face |
| **🔄 Reactive Agents** | Respondem a estímulos do ambiente | Monitoramento, alertas, automação simples | Event-driven, Webhooks |
| **🎯 Goal-Based Agents** | Orientados por objetivos específicos | Planejamento, otimização, resolução de problemas | Planning algorithms, Search |
| **🤝 Collaborative Agents** | Trabalham em equipe para objetivos comuns | Sistemas distribuídos, workflow automation | Multi-agent frameworks |
| **📈 Learning Agents** | Melhoram performance através de experiência | Recomendações, predições, otimização contínua | ML/AI, Reinforcement Learning |

### Princípios Fundamentais

```python
# Princípios DATAMETRIA para Agents
class AgentPrinciples:
    AUTONOMY = "Agents devem operar independentemente"
    REACTIVITY = "Responder rapidamente a mudanças no ambiente"
    PROACTIVITY = "Tomar iniciativas para alcançar objetivos"
    SOCIAL_ABILITY = "Comunicar e coordenar com outros agents"
    ADAPTABILITY = "Aprender e evoluir com experiência"
    TRANSPARENCY = "Operações auditáveis e explicáveis"
    RELIABILITY = "Comportamento consistente e previsível"
    SECURITY = "Comunicação segura e dados protegidos"
```

---

## 🏗️ Arquitetura de Agents

### Arquitetura Padrão DATAMETRIA

```python
# Arquitetura base para todos os agents DATAMETRIA
from abc import ABC, abstractmethod
from typing import Dict, List, Any, Optional
from dataclasses import dataclass
from enum import Enum
import asyncio
import logging

class AgentState(Enum):
    IDLE = "idle"
    PROCESSING = "processing"
    COMMUNICATING = "communicating"
    LEARNING = "learning"
    ERROR = "error"

@dataclass
class AgentMessage:
    sender_id: str
    receiver_id: str
    message_type: str
    content: Dict[str, Any]
    timestamp: float
    correlation_id: Optional[str] = None

class BaseAgent(ABC):
    def __init__(self, agent_id: str, config: Dict[str, Any]):
        self.agent_id = agent_id
        self.config = config
        self.state = AgentState.IDLE
        self.logger = logging.getLogger(f"agent.{agent_id}")
        self.message_queue = asyncio.Queue()
        self.knowledge_base = {}

    @abstractmethod
    async def perceive(self) -> Dict[str, Any]:
        """Perceber o ambiente"""
        pass

    @abstractmethod
    async def decide(self, perception: Dict[str, Any]) -> Dict[str, Any]:
        """Tomar decisões baseadas na percepção"""
        pass

    @abstractmethod
    async def act(self, decision: Dict[str, Any]) -> bool:
        """Executar ações no ambiente"""
        pass

    async def communicate(self, message: AgentMessage) -> bool:
        """Comunicar com outros agents"""
        await self.message_queue.put(message)
        return True

    async def learn(self, experience: Dict[str, Any]) -> None:
        """Aprender com experiências"""
        self.knowledge_base.update(experience)
```

### Padrões de Arquitetura

#### 1. **Layered Architecture**

```
┌─────────────────────────────────────┐
│           Interface Layer           │  ← APIs, UI, External Systems
├─────────────────────────────────────┤
│         Communication Layer         │  ← Agent-to-Agent Communication
├─────────────────────────────────────┤
│           Decision Layer            │  ← Planning, Reasoning, ML Models
├─────────────────────────────────────┤
│          Perception Layer           │  ← Sensors, Data Collection
├─────────────────────────────────────┤
│           Storage Layer             │  ← Knowledge Base, Memory, State
└─────────────────────────────────────┘
```

#### 2. **Microservices-Based Agents**

```python
# Agent como microserviço
from fastapi import FastAPI, BackgroundTasks
from pydantic import BaseModel

class AgentService:
    def __init__(self):
        self.app = FastAPI(title="DATAMETRIA Agent Service")
        self.agent = None
        self.setup_routes()

    def setup_routes(self):
        @self.app.post("/agent/message")
        async def receive_message(message: AgentMessage):
            return await self.agent.communicate(message)

        @self.app.get("/agent/status")
        async def get_status():
            return {
                "agent_id": self.agent.agent_id,
                "state": self.agent.state.value,
                "uptime": self.get_uptime()
            }
```

---

## 💬 Comunicação Agent-to-Agent

### Modelos de Comunicação DATAMETRIA

#### 1. **Direct Message Passing**

```python
# Comunicação direta entre agents
class DirectCommunication:
    def __init__(self):
        self.agents_registry = {}

    async def send_message(self, sender_id: str, receiver_id: str,
                          message_type: str, content: Dict[str, Any]):
        message = AgentMessage(
            sender_id=sender_id,
            receiver_id=receiver_id,
            message_type=message_type,
            content=content,
            timestamp=time.time()
        )

        receiver_agent = self.agents_registry.get(receiver_id)
        if receiver_agent:
            await receiver_agent.communicate(message)
            return True
        return False
```

#### 2. **Event-Driven Communication (Pub/Sub)**

```python
# Comunicação baseada em eventos
import redis
import json

class EventBus:
    def __init__(self, redis_url: str = "redis://localhost:6379"):
        self.redis_client = redis.from_url(redis_url)
        self.pubsub = self.redis_client.pubsub()

    async def publish_event(self, event_type: str, data: Dict[str, Any]):
        event = {
            "type": event_type,
            "data": data,
            "timestamp": time.time()
        }
        self.redis_client.publish(f"agent_events:{event_type}", json.dumps(event))

    async def subscribe_to_events(self, event_types: List[str], callback):
        for event_type in event_types:
            self.pubsub.subscribe(f"agent_events:{event_type}")

        for message in self.pubsub.listen():
            if message['type'] == 'message':
                event_data = json.loads(message['data'])
                await callback(event_data)
```

#### 3. **Brokered Communication**

```python
# Comunicação mediada por broker
class AgentBroker:
    def __init__(self):
        self.agents = {}
        self.message_routes = {}
        self.message_history = []

    def register_agent(self, agent_id: str, agent_instance):
        self.agents[agent_id] = agent_instance
        self.logger.info(f"Agent {agent_id} registered")

    async def route_message(self, message: AgentMessage):
        # Log da mensagem
        self.message_history.append(message)

        # Roteamento baseado em regras
        if message.receiver_id in self.agents:
            await self.agents[message.receiver_id].communicate(message)
        else:
            # Broadcast para agents interessados
            interested_agents = self.find_interested_agents(message.message_type)
            for agent_id in interested_agents:
                await self.agents[agent_id].communicate(message)
```

### Protocolos de Comunicação

#### DATAMETRIA Agent Communication Protocol (DACP) v2.0

```python
# Protocolo padrão DATAMETRIA v2.0 com W3C Trace Context
from dataclasses import dataclass
from typing import Optional, Dict, Any
import uuid

@dataclass
class W3CTraceContext:
    """W3C Trace Context para distributed tracing"""
    traceparent: str  # version-trace_id-parent_id-trace_flags
    tracestate: Optional[str] = None

    @classmethod
    def generate(cls) -> 'W3CTraceContext':
        trace_id = uuid.uuid4().hex
        parent_id = uuid.uuid4().hex[:16]
        return cls(
            traceparent=f"00-{trace_id}-{parent_id}-01",
            tracestate=None
        )

class DACPMessage:
    def __init__(self):
        self.version = "2.0"
        self.performative = None  # ask, tell, achieve, inform, request
        self.sender = None
        self.receiver = None
        self.content = None
        self.ontology = None
        self.language = "json"
        self.conversation_id = None
        self.trace_context = W3CTraceContext.generate()  # v2.0: Distributed tracing
        self.metadata = {}  # v2.0: Metadados extensíveis

    def to_dict(self) -> Dict[str, Any]:
        return {
            "version": self.version,
            "performative": self.performative,
            "sender": self.sender,
            "receiver": self.receiver,
            "content": self.content,
            "ontology": self.ontology,
            "language": self.language,
            "conversation_id": self.conversation_id,
            "timestamp": time.time(),
            "trace_context": {
                "traceparent": self.trace_context.traceparent,
                "tracestate": self.trace_context.tracestate
            },
            "metadata": self.metadata
        }

# Performatives padrão
class Performatives:
    ASK = "ask"           # Solicitar informação
    TELL = "tell"         # Informar algo
    ACHIEVE = "achieve"   # Solicitar ação
    INFORM = "inform"     # Notificar evento
    REQUEST = "request"   # Fazer pedido
    CONFIRM = "confirm"   # Confirmar recebimento
    REFUSE = "refuse"     # Recusar solicitação
    AGREE = "agree"       # Concordar com proposta
```

### Padrões de Interação

#### 1. **Request/Response Pattern**

```python
async def request_response_pattern(agent_a, agent_b, request_data):
    # Agent A faz uma solicitação
    request_msg = DACPMessage()
    request_msg.performative = Performatives.ASK
    request_msg.sender = agent_a.agent_id
    request_msg.receiver = agent_b.agent_id
    request_msg.content = request_data

    # Enviar solicitação
    await agent_b.communicate(request_msg)

    # Agent B processa e responde
    response_msg = DACPMessage()
    response_msg.performative = Performatives.TELL
    response_msg.sender = agent_b.agent_id
    response_msg.receiver = agent_a.agent_id
    response_msg.content = {"result": "processed_data"}

    await agent_a.communicate(response_msg)
```

#### 2. **Contract Net Protocol (CNP)**

```python
class ContractNetProtocol:
    async def initiate_cnp(self, manager_agent, task_description, participant_agents):
        # 1. Call for Proposals (CFP)
        cfp_msg = DACPMessage()
        cfp_msg.performative = "cfp"
        cfp_msg.content = {"task": task_description, "deadline": time.time() + 3600}

        proposals = []
        for agent in participant_agents:
            await agent.communicate(cfp_msg)
            # Aguardar propostas
            proposal = await self.wait_for_proposal(agent, timeout=30)
            if proposal:
                proposals.append(proposal)

        # 2. Evaluate proposals and select winner
        best_proposal = self.evaluate_proposals(proposals)

        # 3. Accept/Reject proposals
        for proposal in proposals:
            if proposal == best_proposal:
                accept_msg = DACPMessage()
                accept_msg.performative = "accept_proposal"
                await proposal.sender.communicate(accept_msg)
            else:
                reject_msg = DACPMessage()
                reject_msg.performative = "reject_proposal"
                await proposal.sender.communicate(reject_msg)
```

---

## 🧠 Componentes Avançados v2.0

### LLMOrchestrator - Gerenciamento Multi-LLM

```python
from typing import List, Dict, Any, Optional
from enum import Enum
import asyncio

class LLMProvider(Enum):
    OPENAI = "openai"
    ANTHROPIC = "anthropic"
    GOOGLE = "google"
    HUGGINGFACE = "huggingface"
    LOCAL = "local"

class LLMOrchestrator:
    """Orquestra múltiplos LLMs com otimização de custo e performance"""

    def __init__(self, config: Dict[str, Any]):
        self.providers = {}
        self.routing_strategy = config.get("routing_strategy", "cost_optimized")
        self.fallback_enabled = config.get("fallback_enabled", True)
        self.metrics = LLMMetrics()

    def register_provider(self, provider: LLMProvider, client: Any, config: Dict[str, Any]):
        """Registra um provider LLM"""
        self.providers[provider] = {
            "client": client,
            "config": config,
            "cost_per_1k_tokens": config.get("cost_per_1k_tokens", 0.0),
            "max_tokens": config.get("max_tokens", 4096),
            "latency_avg": config.get("latency_avg", 1.0),
            "reliability": config.get("reliability", 0.99)
        }

    async def route_request(self, prompt: str, requirements: Dict[str, Any]) -> Dict[str, Any]:
        """Roteia requisição para o LLM mais adequado"""
        if self.routing_strategy == "cost_optimized":
            provider = self._select_cheapest_provider(requirements)
        elif self.routing_strategy == "performance_optimized":
            provider = self._select_fastest_provider(requirements)
        elif self.routing_strategy == "quality_optimized":
            provider = self._select_best_quality_provider(requirements)
        else:
            provider = self._select_balanced_provider(requirements)

        try:
            result = await self._execute_llm_request(provider, prompt, requirements)
            self.metrics.record_success(provider, result)
            return result
        except Exception as e:
            if self.fallback_enabled:
                return await self._fallback_request(prompt, requirements, failed_provider=provider)
            raise

    def _select_cheapest_provider(self, requirements: Dict[str, Any]) -> LLMProvider:
        """Seleciona provider com menor custo que atende requisitos"""
        max_tokens = requirements.get("max_tokens", 1000)
        min_quality = requirements.get("min_quality", 0.8)

        eligible = [
            (provider, info) for provider, info in self.providers.items()
            if info["max_tokens"] >= max_tokens and info["reliability"] >= min_quality
        ]

        if not eligible:
            raise ValueError("No eligible provider found")

        return min(eligible, key=lambda x: x[1]["cost_per_1k_tokens"])[0]

    async def _execute_llm_request(self, provider: LLMProvider, prompt: str,
                                   requirements: Dict[str, Any]) -> Dict[str, Any]:
        """Executa requisição no provider selecionado"""
        provider_info = self.providers[provider]
        client = provider_info["client"]

        start_time = time.time()
        response = await client.generate(
            prompt=prompt,
            max_tokens=requirements.get("max_tokens", 1000),
            temperature=requirements.get("temperature", 0.7)
        )
        latency = time.time() - start_time

        return {
            "provider": provider.value,
            "response": response,
            "latency": latency,
            "tokens_used": response.get("usage", {}).get("total_tokens", 0),
            "cost": self._calculate_cost(provider, response)
        }

    async def _fallback_request(self, prompt: str, requirements: Dict[str, Any],
                               failed_provider: LLMProvider) -> Dict[str, Any]:
        """Executa fallback para outro provider"""
        available_providers = [p for p in self.providers.keys() if p != failed_provider]

        for provider in available_providers:
            try:
                return await self._execute_llm_request(provider, prompt, requirements)
            except Exception:
                continue

        raise Exception("All providers failed")
```

### PromptCurator - Gerenciamento de Prompts

```python
from typing import List, Dict, Any, Optional
from dataclasses import dataclass
import json

@dataclass
class PromptTemplate:
    id: str
    name: str
    template: str
    variables: List[str]
    version: str
    metadata: Dict[str, Any]
    performance_metrics: Dict[str, float]

class PromptCurator:
    """Gerencia lifecycle de prompts com versionamento e feedback"""

    def __init__(self, storage_backend):
        self.storage = storage_backend
        self.templates = {}
        self.feedback_history = []

    async def create_template(self, name: str, template: str,
                            variables: List[str], metadata: Dict[str, Any]) -> PromptTemplate:
        """Cria novo template de prompt"""
        template_id = f"prompt_{uuid.uuid4().hex[:8]}"
        prompt_template = PromptTemplate(
            id=template_id,
            name=name,
            template=template,
            variables=variables,
            version="1.0.0",
            metadata=metadata,
            performance_metrics={}
        )

        self.templates[template_id] = prompt_template
        await self.storage.save_template(prompt_template)
        return prompt_template

    async def render_prompt(self, template_id: str, variables: Dict[str, Any]) -> str:
        """Renderiza prompt com variáveis"""
        template = self.templates.get(template_id)
        if not template:
            raise ValueError(f"Template {template_id} not found")

        # Validar variáveis
        missing_vars = set(template.variables) - set(variables.keys())
        if missing_vars:
            raise ValueError(f"Missing variables: {missing_vars}")

        # Renderizar template
        rendered = template.template
        for var_name, var_value in variables.items():
            rendered = rendered.replace(f"{{{var_name}}}", str(var_value))

        return rendered

    async def record_feedback(self, template_id: str, execution_result: Dict[str, Any],
                            user_feedback: Optional[Dict[str, Any]] = None):
        """Registra feedback de execução do prompt"""
        feedback = {
            "template_id": template_id,
            "timestamp": time.time(),
            "execution_result": execution_result,
            "user_feedback": user_feedback,
            "metrics": {
                "latency": execution_result.get("latency", 0),
                "tokens_used": execution_result.get("tokens_used", 0),
                "cost": execution_result.get("cost", 0),
                "success": execution_result.get("success", False)
            }
        }

        self.feedback_history.append(feedback)
        await self._update_template_metrics(template_id, feedback)

    async def _update_template_metrics(self, template_id: str, feedback: Dict[str, Any]):
        """Atualiza métricas do template baseado em feedback"""
        template = self.templates.get(template_id)
        if not template:
            return

        metrics = feedback["metrics"]
        current_metrics = template.performance_metrics

        # Atualizar médias móveis
        for metric_name, metric_value in metrics.items():
            if metric_name in current_metrics:
                # Média móvel exponencial (alpha=0.3)
                current_metrics[metric_name] = 0.3 * metric_value + 0.7 * current_metrics[metric_name]
            else:
                current_metrics[metric_name] = metric_value

        await self.storage.update_template_metrics(template_id, current_metrics)

    async def optimize_template(self, template_id: str) -> PromptTemplate:
        """Otimiza template baseado em feedback histórico"""
        template = self.templates.get(template_id)
        if not template:
            raise ValueError(f"Template {template_id} not found")

        # Analisar feedback
        template_feedback = [
            f for f in self.feedback_history
            if f["template_id"] == template_id
        ]

        if len(template_feedback) < 10:
            return template  # Dados insuficientes

        # Identificar padrões de falha
        failures = [f for f in template_feedback if not f["metrics"]["success"]]
        if len(failures) / len(template_feedback) > 0.2:  # >20% falhas
            # Sugerir otimizações
            optimizations = await self._generate_optimizations(template, failures)
            template.metadata["suggested_optimizations"] = optimizations

        return template
```

### StateManager - Gerenciamento de Estado com Event Sourcing

```python
from typing import List, Dict, Any, Optional
from dataclasses import dataclass
from enum import Enum
import json

class EventType(Enum):
    STATE_CREATED = "state_created"
    STATE_UPDATED = "state_updated"
    STATE_DELETED = "state_deleted"
    TRANSITION_EXECUTED = "transition_executed"

@dataclass
class StateEvent:
    event_id: str
    event_type: EventType
    agent_id: str
    timestamp: float
    data: Dict[str, Any]
    metadata: Dict[str, Any]

class StateManager:
    """Gerencia estado de agents com Event Sourcing para auditabilidade"""

    def __init__(self, event_store):
        self.event_store = event_store
        self.current_states = {}  # Cache de estados atuais
        self.snapshots = {}  # Snapshots periódicos

    async def create_state(self, agent_id: str, initial_state: Dict[str, Any]) -> str:
        """Cria novo estado para agent"""
        event = StateEvent(
            event_id=f"evt_{uuid.uuid4().hex}",
            event_type=EventType.STATE_CREATED,
            agent_id=agent_id,
            timestamp=time.time(),
            data=initial_state,
            metadata={"version": "1.0.0"}
        )

        await self.event_store.append(event)
        self.current_states[agent_id] = initial_state
        return event.event_id

    async def update_state(self, agent_id: str, updates: Dict[str, Any],
                          reason: Optional[str] = None) -> str:
        """Atualiza estado do agent"""
        current_state = self.current_states.get(agent_id, {})

        event = StateEvent(
            event_id=f"evt_{uuid.uuid4().hex}",
            event_type=EventType.STATE_UPDATED,
            agent_id=agent_id,
            timestamp=time.time(),
            data={
                "previous_state": current_state.copy(),
                "updates": updates,
                "new_state": {**current_state, **updates}
            },
            metadata={"reason": reason}
        )

        await self.event_store.append(event)
        self.current_states[agent_id] = {**current_state, **updates}

        # Criar snapshot a cada 100 eventos
        event_count = await self.event_store.count_events(agent_id)
        if event_count % 100 == 0:
            await self._create_snapshot(agent_id)

        return event.event_id

    async def get_state(self, agent_id: str) -> Dict[str, Any]:
        """Obtém estado atual do agent"""
        if agent_id in self.current_states:
            return self.current_states[agent_id]

        # Reconstruir estado a partir de eventos
        return await self._rebuild_state(agent_id)

    async def get_state_at_time(self, agent_id: str, timestamp: float) -> Dict[str, Any]:
        """Obtém estado do agent em um momento específico (time travel)"""
        events = await self.event_store.get_events_until(agent_id, timestamp)
        return await self._rebuild_state_from_events(events)

    async def _rebuild_state(self, agent_id: str) -> Dict[str, Any]:
        """Reconstrói estado a partir de eventos"""
        # Começar do último snapshot
        snapshot = self.snapshots.get(agent_id)
        if snapshot:
            state = snapshot["state"].copy()
            events = await self.event_store.get_events_after(agent_id, snapshot["timestamp"])
        else:
            state = {}
            events = await self.event_store.get_all_events(agent_id)

        # Aplicar eventos
        for event in events:
            state = self._apply_event(state, event)

        self.current_states[agent_id] = state
        return state

    def _apply_event(self, state: Dict[str, Any], event: StateEvent) -> Dict[str, Any]:
        """Aplica evento ao estado"""
        if event.event_type == EventType.STATE_CREATED:
            return event.data
        elif event.event_type == EventType.STATE_UPDATED:
            return event.data["new_state"]
        elif event.event_type == EventType.STATE_DELETED:
            return {}
        return state

    async def _create_snapshot(self, agent_id: str):
        """Cria snapshot do estado atual"""
        current_state = self.current_states.get(agent_id, {})
        self.snapshots[agent_id] = {
            "state": current_state.copy(),
            "timestamp": time.time()
        }
        await self.event_store.save_snapshot(agent_id, self.snapshots[agent_id])

    async def get_audit_trail(self, agent_id: str, start_time: Optional[float] = None,
                             end_time: Optional[float] = None) -> List[StateEvent]:
        """Obtém trilha de auditoria completa"""
        return await self.event_store.get_events_in_range(agent_id, start_time, end_time)
```

### Agno Framework Integration

```python
# Integração com Agno Framework (recomendado para v2.0)
from agno import Agent, Task, Workflow
from agno.llm import OpenAI, Anthropic
from agno.tools import Tool

class AgnoIntegration:
    """Wrapper para integração com Agno Framework"""

    def __init__(self, config: Dict[str, Any]):
        self.config = config
        self.agents = {}
        self.workflows = {}

    def create_agno_agent(self, agent_id: str, role: str,
                         llm_config: Dict[str, Any]) -> Agent:
        """Cria agent usando Agno Framework"""
        # Configurar LLM
        if llm_config["provider"] == "openai":
            llm = OpenAI(model=llm_config["model"], api_key=llm_config["api_key"])
        elif llm_config["provider"] == "anthropic":
            llm = Anthropic(model=llm_config["model"], api_key=llm_config["api_key"])
        else:
            raise ValueError(f"Unsupported LLM provider: {llm_config['provider']}")

        # Criar agent
        agent = Agent(
            id=agent_id,
            role=role,
            llm=llm,
            tools=self._setup_tools(agent_id),
            memory=True,
            verbose=self.config.get("verbose", False)
        )

        self.agents[agent_id] = agent
        return agent

    def create_workflow(self, workflow_id: str, agents: List[Agent],
                       tasks: List[Task]) -> Workflow:
        """Cria workflow com múltiplos agents"""
        workflow = Workflow(
            id=workflow_id,
            agents=agents,
            tasks=tasks,
            process="sequential",  # ou "parallel", "hierarchical"
            verbose=self.config.get("verbose", False)
        )

        self.workflows[workflow_id] = workflow
        return workflow

    async def execute_workflow(self, workflow_id: str,
                              inputs: Dict[str, Any]) -> Dict[str, Any]:
        """Executa workflow"""
        workflow = self.workflows.get(workflow_id)
        if not workflow:
            raise ValueError(f"Workflow {workflow_id} not found")

        result = await workflow.kickoff(inputs)
        return {
            "workflow_id": workflow_id,
            "result": result,
            "timestamp": time.time()
        }

    def _setup_tools(self, agent_id: str) -> List[Tool]:
        """Configura ferramentas para o agent"""
        return [
            Tool(
                name="web_search",
                description="Search the web for information",
                func=self._web_search_tool
            ),
            Tool(
                name="database_query",
                description="Query the database",
                func=self._database_query_tool
            )
        ]
```

### Padrões de Resiliência v2.0

```python
from typing import Callable, Any
import asyncio
from enum import Enum

class CircuitState(Enum):
    CLOSED = "closed"  # Normal operation
    OPEN = "open"      # Failing, reject requests
    HALF_OPEN = "half_open"  # Testing if recovered

class CircuitBreaker:
    """Circuit Breaker pattern para proteção contra falhas"""

    def __init__(self, failure_threshold: int = 5, timeout: float = 60.0,
                 success_threshold: int = 2):
        self.failure_threshold = failure_threshold
        self.timeout = timeout
        self.success_threshold = success_threshold
        self.failure_count = 0
        self.success_count = 0
        self.state = CircuitState.CLOSED
        self.last_failure_time = None

    async def call(self, func: Callable, *args, **kwargs) -> Any:
        """Executa função com circuit breaker"""
        if self.state == CircuitState.OPEN:
            if time.time() - self.last_failure_time > self.timeout:
                self.state = CircuitState.HALF_OPEN
                self.success_count = 0
            else:
                raise Exception("Circuit breaker is OPEN")

        try:
            result = await func(*args, **kwargs)
            self._on_success()
            return result
        except Exception as e:
            self._on_failure()
            raise e

    def _on_success(self):
        """Callback de sucesso"""
        self.failure_count = 0

        if self.state == CircuitState.HALF_OPEN:
            self.success_count += 1
            if self.success_count >= self.success_threshold:
                self.state = CircuitState.CLOSED

    def _on_failure(self):
        """Callback de falha"""
        self.failure_count += 1
        self.last_failure_time = time.time()

        if self.failure_count >= self.failure_threshold:
            self.state = CircuitState.OPEN

class OutboxPattern:
    """Outbox Pattern para garantir consistência eventual"""

    def __init__(self, database, message_broker):
        self.database = database
        self.message_broker = message_broker
        self.processing = False

    async def publish_with_transaction(self, message: Dict[str, Any],
                                      db_operation: Callable):
        """Publica mensagem com garantia transacional"""
        async with self.database.transaction() as tx:
            # Executar operação de banco
            await db_operation(tx)

            # Salvar mensagem na outbox
            await tx.execute(
                "INSERT INTO outbox (message_id, payload, created_at) VALUES ($1, $2, $3)",
                message["id"], json.dumps(message), time.time()
            )

        # Processar outbox em background
        if not self.processing:
            asyncio.create_task(self._process_outbox())

    async def _process_outbox(self):
        """Processa mensagens pendentes na outbox"""
        self.processing = True

        try:
            while True:
                messages = await self.database.fetch(
                    "SELECT * FROM outbox WHERE processed = false ORDER BY created_at LIMIT 10"
                )

                if not messages:
                    break

                for message in messages:
                    try:
                        await self.message_broker.publish(json.loads(message["payload"]))
                        await self.database.execute(
                            "UPDATE outbox SET processed = true WHERE message_id = $1",
                            message["message_id"]
                        )
                    except Exception as e:
                        # Retry logic
                        await self.database.execute(
                            "UPDATE outbox SET retry_count = retry_count + 1 WHERE message_id = $1",
                            message["message_id"]
                        )

                await asyncio.sleep(1)
        finally:
            self.processing = False

class RateLimiter:
    """Rate Limiter para controle de taxa de requisições"""

    def __init__(self, max_requests: int, time_window: float):
        self.max_requests = max_requests
        self.time_window = time_window
        self.requests = {}

    async def acquire(self, key: str) -> bool:
        """Tenta adquirir permissão para fazer requisição"""
        now = time.time()

        if key not in self.requests:
            self.requests[key] = []

        # Remover requisições antigas
        self.requests[key] = [
            req_time for req_time in self.requests[key]
            if now - req_time < self.time_window
        ]

        # Verificar limite
        if len(self.requests[key]) >= self.max_requests:
            return False

        self.requests[key].append(now)
        return True
```

---

## 🔧 Implementação

### Stack Tecnológico Recomendado

| Componente | Tecnologia | Justificativa |
|------------|------------|---------------|
| **Runtime** | Python 3.11+ | Ecossistema rico em IA/ML |
| **Framework** | FastAPI + AsyncIO | Performance e escalabilidade |
| **Communication** | Redis + WebSocket | Baixa latência, pub/sub |
| **LLM Integration** | LangChain + OpenAI | Agents conversacionais |
| **Monitoring** | Prometheus + Grafana | Observabilidade completa |
| **Storage** | PostgreSQL + Redis | Persistência e cache |
| **Containerization** | Docker + Kubernetes | Deploy e orquestração |

### Exemplo de Agent LLM

```python
from langchain.agents import AgentExecutor, create_openai_functions_agent
from langchain.tools import Tool
from langchain_openai import ChatOpenAI

class LLMAgent(BaseAgent):
    def __init__(self, agent_id: str, config: Dict[str, Any]):
        super().__init__(agent_id, config)
        self.llm = ChatOpenAI(
            model="gpt-4",
            temperature=0.1,
            api_key=config.get("openai_api_key")
        )
        self.tools = self.setup_tools()
        self.agent_executor = self.create_agent_executor()

    def setup_tools(self) -> List[Tool]:
        return [
            Tool(
                name="web_search",
                description="Search the web for information",
                func=self.web_search
            ),
            Tool(
                name="send_message",
                description="Send message to another agent",
                func=self.send_agent_message
            )
        ]

    async def perceive(self) -> Dict[str, Any]:
        # Perceber mensagens na fila
        messages = []
        while not self.message_queue.empty():
            message = await self.message_queue.get()
            messages.append(message)

        return {"messages": messages, "timestamp": time.time()}

    async def decide(self, perception: Dict[str, Any]) -> Dict[str, Any]:
        messages = perception.get("messages", [])

        decisions = []
        for message in messages:
            # Usar LLM para decidir como responder
            prompt = f"""
            You are an AI agent with ID: {self.agent_id}
            You received a message: {message.content}
            From agent: {message.sender_id}

            Decide how to respond or what action to take.
            """

            response = await self.agent_executor.ainvoke({"input": prompt})
            decisions.append({
                "original_message": message,
                "response": response["output"]
            })

        return {"decisions": decisions}

    async def act(self, decision: Dict[str, Any]) -> bool:
        decisions = decision.get("decisions", [])

        for decision_item in decisions:
            # Executar ação baseada na decisão do LLM
            await self.execute_llm_decision(decision_item)

        return True
```

### Exemplo de Agent Colaborativo

```python
class CollaborativeAgent(BaseAgent):
    def __init__(self, agent_id: str, config: Dict[str, Any]):
        super().__init__(agent_id, config)
        self.team_members = config.get("team_members", [])
        self.shared_goals = config.get("shared_goals", [])
        self.coordination_strategy = config.get("strategy", "consensus")

    async def coordinate_with_team(self, task: Dict[str, Any]):
        # Dividir tarefa entre membros da equipe
        subtasks = await self.decompose_task(task)

        # Distribuir subtarefas
        assignments = {}
        for i, subtask in enumerate(subtasks):
            assigned_agent = self.team_members[i % len(self.team_members)]
            assignments[assigned_agent] = subtask

            # Enviar subtarefa
            msg = AgentMessage(
                sender_id=self.agent_id,
                receiver_id=assigned_agent,
                message_type="task_assignment",
                content={"subtask": subtask, "deadline": time.time() + 1800}
            )
            await self.communicate(msg)

        # Aguardar resultados
        results = await self.collect_results(assignments)

        # Consolidar resultados
        final_result = await self.consolidate_results(results)

        return final_result
```

---

## 📊 Monitoramento e Observabilidade

### Métricas de Agents

```python
from prometheus_client import Counter, Histogram, Gauge
import time

class AgentMetrics:
    def __init__(self, agent_id: str):
        self.agent_id = agent_id

        # Contadores
        self.messages_sent = Counter(
            'agent_messages_sent_total',
            'Total messages sent by agent',
            ['agent_id', 'message_type']
        )

        self.messages_received = Counter(
            'agent_messages_received_total',
            'Total messages received by agent',
            ['agent_id', 'message_type']
        )

        # Histogramas
        self.decision_time = Histogram(
            'agent_decision_time_seconds',
            'Time taken to make decisions',
            ['agent_id']
        )

        self.action_time = Histogram(
            'agent_action_time_seconds',
            'Time taken to execute actions',
            ['agent_id']
        )

        # Gauges
        self.queue_size = Gauge(
            'agent_message_queue_size',
            'Current message queue size',
            ['agent_id']
        )

        self.knowledge_base_size = Gauge(
            'agent_knowledge_base_size',
            'Size of agent knowledge base',
            ['agent_id']
        )

    def record_message_sent(self, message_type: str):
        self.messages_sent.labels(
            agent_id=self.agent_id,
            message_type=message_type
        ).inc()

    def record_decision_time(self, duration: float):
        self.decision_time.labels(agent_id=self.agent_id).observe(duration)
```

### Dashboard de Monitoramento

```yaml
# Grafana Dashboard Config
dashboard:
  title: "DATAMETRIA Agents Monitoring"
  panels:
    - title: "Agent Messages Flow"
      type: "graph"
      targets:
        - expr: "rate(agent_messages_sent_total[5m])"
        - expr: "rate(agent_messages_received_total[5m])"

    - title: "Decision Time Distribution"
      type: "histogram"
      targets:
        - expr: "agent_decision_time_seconds"

    - title: "Active Agents"
      type: "stat"
      targets:
        - expr: "count(up{job='datametria-agents'})"

    - title: "Error Rate"
      type: "graph"
      targets:
        - expr: "rate(agent_errors_total[5m])"
```

### Logging Estruturado

```python
import structlog

class AgentLogger:
    def __init__(self, agent_id: str):
        self.logger = structlog.get_logger("datametria.agent").bind(
            agent_id=agent_id,
            component="agent"
        )

    def log_message_sent(self, message: AgentMessage):
        self.logger.info(
            "message_sent",
            receiver=message.receiver_id,
            message_type=message.message_type,
            correlation_id=message.correlation_id
        )

    def log_decision_made(self, decision: Dict[str, Any], duration: float):
        self.logger.info(
            "decision_made",
            decision_type=decision.get("type"),
            duration_ms=duration * 1000,
            confidence=decision.get("confidence", 0.0)
        )

    def log_error(self, error: Exception, context: Dict[str, Any]):
        self.logger.error(
            "agent_error",
            error_type=type(error).__name__,
            error_message=str(error),
            context=context
        )
```

---

## 🔒 Segurança e Compliance

### Autenticação e Autorização

```python
import jwt
from cryptography.fernet import Fernet

class AgentSecurity:
    def __init__(self, secret_key: str):
        self.secret_key = secret_key
        self.cipher_suite = Fernet(Fernet.generate_key())

    def generate_agent_token(self, agent_id: str, permissions: List[str]) -> str:
        payload = {
            "agent_id": agent_id,
            "permissions": permissions,
            "issued_at": time.time(),
            "expires_at": time.time() + 3600  # 1 hora
        }
        return jwt.encode(payload, self.secret_key, algorithm="HS256")

    def verify_agent_token(self, token: str) -> Dict[str, Any]:
        try:
            payload = jwt.decode(token, self.secret_key, algorithms=["HS256"])
            if payload["expires_at"] < time.time():
                raise jwt.ExpiredSignatureError("Token expired")
            return payload
        except jwt.InvalidTokenError as e:
            raise ValueError(f"Invalid token: {e}")

    def encrypt_message(self, message: str) -> bytes:
        return self.cipher_suite.encrypt(message.encode())

    def decrypt_message(self, encrypted_message: bytes) -> str:
        return self.cipher_suite.decrypt(encrypted_message).decode()
```

### Auditoria e Compliance

```python
class AgentAuditLog:
    def __init__(self, storage_backend):
        self.storage = storage_backend

    async def log_agent_action(self, agent_id: str, action: str,
                              details: Dict[str, Any]):
        audit_entry = {
            "timestamp": time.time(),
            "agent_id": agent_id,
            "action": action,
            "details": details,
            "ip_address": self.get_agent_ip(agent_id),
            "session_id": self.get_session_id(agent_id)
        }

        await self.storage.store_audit_entry(audit_entry)

    async def get_agent_audit_trail(self, agent_id: str,
                                   start_time: float,
                                   end_time: float) -> List[Dict[str, Any]]:
        return await self.storage.query_audit_entries(
            agent_id=agent_id,
            start_time=start_time,
            end_time=end_time
        )
```

---

## 🧪 Testes e Validação

### Testes Unitários para Agents

```python
import pytest
import asyncio
from unittest.mock import Mock, AsyncMock

class TestBaseAgent:
    @pytest.fixture
    def agent(self):
        config = {"test_mode": True}
        return TestAgent("test_agent_001", config)

    @pytest.mark.asyncio
    async def test_agent_communication(self, agent):
        # Arrange
        message = AgentMessage(
            sender_id="sender_001",
            receiver_id=agent.agent_id,
            message_type="test_message",
            content={"data": "test_data"}
        )

        # Act
        result = await agent.communicate(message)

        # Assert
        assert result is True
        assert not agent.message_queue.empty()

    @pytest.mark.asyncio
    async def test_agent_decision_making(self, agent):
        # Arrange
        perception = {"messages": [], "environment": "test"}

        # Act
        decision = await agent.decide(perception)

        # Assert
        assert isinstance(decision, dict)
        assert "action" in decision
```

### Testes de Integração

```python
class TestAgentCommunication:
    @pytest.mark.asyncio
    async def test_agent_to_agent_communication(self):
        # Setup
        agent_a = TestAgent("agent_a", {})
        agent_b = TestAgent("agent_b", {})

        broker = AgentBroker()
        broker.register_agent("agent_a", agent_a)
        broker.register_agent("agent_b", agent_b)

        # Test message routing
        message = AgentMessage(
            sender_id="agent_a",
            receiver_id="agent_b",
            message_type="greeting",
            content={"message": "Hello Agent B!"}
        )

        await broker.route_message(message)

        # Verify message was received
        received_message = await agent_b.message_queue.get()
        assert received_message.content["message"] == "Hello Agent B!"
```

### Testes de Performance

```python
import time
import statistics

class AgentPerformanceTest:
    async def test_message_throughput(self, agent, num_messages=1000):
        start_time = time.time()

        # Send multiple messages
        for i in range(num_messages):
            message = AgentMessage(
                sender_id="perf_test",
                receiver_id=agent.agent_id,
                message_type="performance_test",
                content={"sequence": i}
            )
            await agent.communicate(message)

        end_time = time.time()
        duration = end_time - start_time
        throughput = num_messages / duration

        assert throughput > 100  # messages per second
        return throughput

    async def test_decision_latency(self, agent, num_decisions=100):
        latencies = []

        for _ in range(num_decisions):
            perception = {"test_data": "sample"}

            start_time = time.time()
            await agent.decide(perception)
            end_time = time.time()

            latencies.append(end_time - start_time)

        avg_latency = statistics.mean(latencies)
        p95_latency = statistics.quantiles(latencies, n=20)[18]  # 95th percentile

        assert avg_latency < 0.1  # 100ms average
        assert p95_latency < 0.5   # 500ms p95

        return {"avg": avg_latency, "p95": p95_latency}
```

---

## 🚀 Deploy e Operações

### Containerização

```dockerfile
# Dockerfile para Agent DATAMETRIA
FROM python:3.11-slim

WORKDIR /app

# Install dependencies
COPY requirements.txt .
RUN pip install --no-cache-dir -r requirements.txt

# Copy application code
COPY src/ ./src/
COPY config/ ./config/

# Set environment variables
ENV PYTHONPATH=/app/src
ENV AGENT_CONFIG_PATH=/app/config

# Health check
HEALTHCHECK --interval=30s --timeout=10s --start-period=5s --retries=3 \
    CMD python -c "import requests; requests.get('http://localhost:8000/health')"

# Run agent
CMD ["python", "src/main.py"]
```

### Kubernetes Deployment

```yaml
apiVersion: apps/v1
kind: Deployment
metadata:
  name: datametria-agent
  labels:
    app: datametria-agent
spec:
  replicas: 3
  selector:
    matchLabels:
      app: datametria-agent
  template:
    metadata:
      labels:
        app: datametria-agent
    spec:
      containers:
      - name: agent
        image: datametria/agent:latest
        ports:
        - containerPort: 8000
        env:
        - name: AGENT_ID
          valueFrom:
            fieldRef:
              fieldPath: metadata.name
        - name: REDIS_URL
          value: "redis://redis-service:6379"
        resources:
          requests:
            memory: "256Mi"
            cpu: "250m"
          limits:
            memory: "512Mi"
            cpu: "500m"
        livenessProbe:
          httpGet:
            path: /health
            port: 8000
          initialDelaySeconds: 30
          periodSeconds: 10
        readinessProbe:
          httpGet:
            path: /ready
            port: 8000
          initialDelaySeconds: 5
          periodSeconds: 5
---
apiVersion: v1
kind: Service
metadata:
  name: datametria-agent-service
spec:
  selector:
    app: datametria-agent
  ports:
  - protocol: TCP
    port: 80
    targetPort: 8000
  type: ClusterIP
```

### Configuração de Ambiente

```python
# config/settings.py
from pydantic import BaseSettings
from typing import List, Optional

class AgentSettings(BaseSettings):
    # Agent Configuration
    agent_id: str
    agent_type: str = "general"
    log_level: str = "INFO"

    # Communication
    redis_url: str = "redis://localhost:6379"
    message_queue_size: int = 1000
    communication_timeout: float = 30.0

    # LLM Configuration
    openai_api_key: Optional[str] = None
    model_name: str = "gpt-4"
    temperature: float = 0.1
    max_tokens: int = 1000

    # Security
    jwt_secret: str
    encryption_key: Optional[str] = None

    # Monitoring
    metrics_enabled: bool = True
    prometheus_port: int = 9090

    # Database
    database_url: str = "postgresql://localhost/agents"

    class Config:
        env_file = ".env"
        env_prefix = "DATAMETRIA_AGENT_"

settings = AgentSettings()
```

---

## 📈 Otimização e Escalabilidade

### Estratégias de Escalabilidade

#### 1. **Horizontal Scaling**

```python
class AgentCluster:
    def __init__(self, cluster_config: Dict[str, Any]):
        self.cluster_id = cluster_config["cluster_id"]
        self.agents = {}
        self.load_balancer = LoadBalancer()
        self.service_discovery = ServiceDiscovery()

    async def add_agent(self, agent: BaseAgent):
        self.agents[agent.agent_id] = agent
        await self.service_discovery.register_agent(agent)
        self.load_balancer.add_agent(agent)

    async def remove_agent(self, agent_id: str):
        if agent_id in self.agents:
            agent = self.agents.pop(agent_id)
            await self.service_discovery.deregister_agent(agent)
            self.load_balancer.remove_agent(agent)

    async def route_message(self, message: AgentMessage):
        # Load balancing para agents do mesmo tipo
        target_agent = self.load_balancer.select_agent(
            message.receiver_id,
            message.message_type
        )

        if target_agent:
            await target_agent.communicate(message)
            return True
        return False
```

#### 2. **Caching e Otimização**

```python
from functools import lru_cache
import redis

class AgentCache:
    def __init__(self, redis_client):
        self.redis = redis_client
        self.local_cache = {}

    @lru_cache(maxsize=1000)
    def get_cached_decision(self, perception_hash: str):
        # Cache local para decisões frequentes
        return self.local_cache.get(perception_hash)

    async def cache_decision(self, perception_hash: str, decision: Dict[str, Any]):
        # Cache distribuído
        await self.redis.setex(
            f"decision:{perception_hash}",
            3600,  # 1 hora
            json.dumps(decision)
        )

        # Cache local
        self.local_cache[perception_hash] = decision
```

### Performance Tuning

```python
class PerformanceOptimizer:
    def __init__(self, agent: BaseAgent):
        self.agent = agent
        self.performance_metrics = {}

    async def optimize_message_processing(self):
        # Batch processing de mensagens
        batch_size = 10
        messages_batch = []

        while len(messages_batch) < batch_size and not self.agent.message_queue.empty():
            message = await self.agent.message_queue.get()
            messages_batch.append(message)

        if messages_batch:
            # Processar em lote
            await self.process_message_batch(messages_batch)

    async def adaptive_timeout(self, operation_type: str):
        # Timeout adaptativo baseado em histórico
        historical_times = self.performance_metrics.get(operation_type, [])

        if historical_times:
            avg_time = sum(historical_times) / len(historical_times)
            return min(avg_time * 2, 60.0)  # Máximo 60 segundos

        return 30.0  # Default timeout
```

---

## ✅ Checklist de Desenvolvimento

### 📋 Checklist Básico

#### Arquitetura e Design

- [ ] **Agent herda de BaseAgent**
- [ ] **Implementa métodos abstratos obrigatórios** (perceive, decide, act)
- [ ] **Define estado inicial e transições**
- [ ] **Configura logging estruturado**
- [ ] **Implementa health checks**

#### Comunicação

- [ ] **Usa protocolo DACP para mensagens**
- [ ] **Implementa tratamento de erros de comunicação**
- [ ] **Define timeouts apropriados**
- [ ] **Suporta comunicação assíncrona**
- [ ] **Implementa retry logic**

#### Segurança

- [ ] **Autentica mensagens entre agents**
- [ ] **Criptografa dados sensíveis**
- [ ] **Implementa rate limiting**
- [ ] **Registra ações para auditoria**
- [ ] **Valida entrada de dados**

#### Monitoramento

- [ ] **Expõe métricas Prometheus**
- [ ] **Implementa distributed tracing**
- [ ] **Configura alertas críticos**
- [ ] **Monitora performance**
- [ ] **Registra logs estruturados**

#### Testes

- [ ] **Testes unitários > 80% cobertura**
- [ ] **Testes de integração**
- [ ] **Testes de performance**
- [ ] **Testes de falha e recuperação**
- [ ] **Testes de segurança**

### 📊 Checklist Avançado

#### Escalabilidade

- [ ] **Suporta scaling horizontal**
- [ ] **Implementa load balancing**
- [ ] **Usa cache distribuído**
- [ ] **Otimiza uso de recursos**
- [ ] **Suporta service discovery**

#### Reliability

- [ ] **Implementa circuit breaker**
- [ ] **Suporta graceful shutdown**
- [ ] **Implementa backup e recovery**
- [ ] **Testa cenários de falha**
- [ ] **Monitora SLA/SLO**

#### Compliance

- [ ] **Segue padrões DATAMETRIA**
- [ ] **Implementa LGPD/GDPR**
- [ ] **Documenta decisões (ADR)**
- [ ] **Passa em auditoria de segurança**
- [ ] **Mantém logs de compliance**

---

## 🔄 Migração v1.0 → v2.0

### Guia de Migração

#### 1. Atualizar DACP para v2.0

```python
# v1.0 (Antigo)
class DACPMessage:
    def __init__(self):
        self.version = "1.0"
        self.conversation_id = None

# v2.0 (Novo)
class DACPMessage:
    def __init__(self):
        self.version = "2.0"
        self.conversation_id = None
        self.trace_context = W3CTraceContext.generate()  # NOVO
        self.metadata = {}  # NOVO
```

#### 2. Integrar LLMOrchestrator

```python
# v1.0 (Antigo) - LLM direto
class LLMAgent(BaseAgent):
    def __init__(self, agent_id: str, config: Dict[str, Any]):
        super().__init__(agent_id, config)
        self.llm = ChatOpenAI(model="gpt-4", api_key=config["api_key"])

# v2.0 (Novo) - Com LLMOrchestrator
class LLMAgent(BaseAgent):
    def __init__(self, agent_id: str, config: Dict[str, Any]):
        super().__init__(agent_id, config)
        self.llm_orchestrator = LLMOrchestrator(config)
        self.llm_orchestrator.register_provider(
            LLMProvider.OPENAI,
            ChatOpenAI(model="gpt-4", api_key=config["api_key"]),
            {"cost_per_1k_tokens": 0.03, "max_tokens": 8192}
        )
```

#### 3. Adicionar StateManager

```python
# v1.0 (Antigo) - Estado simples
class BaseAgent:
    def __init__(self, agent_id: str, config: Dict[str, Any]):
        self.state = AgentState.IDLE
        self.knowledge_base = {}

# v2.0 (Novo) - Com StateManager
class BaseAgent:
    def __init__(self, agent_id: str, config: Dict[str, Any]):
        self.state = AgentState.IDLE
        self.state_manager = StateManager(event_store)
        asyncio.create_task(self.state_manager.create_state(agent_id, {}))
```

#### 4. Implementar PromptCurator

```python
# v1.0 (Antigo) - Prompts hardcoded
async def decide(self, perception: Dict[str, Any]):
    prompt = f"You are agent {self.agent_id}. Decide what to do."
    response = await self.llm.generate(prompt)

# v2.0 (Novo) - Com PromptCurator
async def decide(self, perception: Dict[str, Any]):
    prompt = await self.prompt_curator.render_prompt(
        "decision_template",
        {"agent_id": self.agent_id, "perception": perception}
    )
    response = await self.llm_orchestrator.route_request(prompt, {})
    await self.prompt_curator.record_feedback("decision_template", response)
```

#### 5. Adicionar Padrões de Resiliência

```python
# v1.0 (Antigo) - Sem proteção
async def call_external_service(self, data):
    return await external_service.call(data)

# v2.0 (Novo) - Com Circuit Breaker
class ResilientAgent(BaseAgent):
    def __init__(self, agent_id: str, config: Dict[str, Any]):
        super().__init__(agent_id, config)
        self.circuit_breaker = CircuitBreaker(failure_threshold=5, timeout=60.0)
        self.rate_limiter = RateLimiter(max_requests=100, time_window=60.0)

    async def call_external_service(self, data):
        if not await self.rate_limiter.acquire(self.agent_id):
            raise Exception("Rate limit exceeded")

        return await self.circuit_breaker.call(
            external_service.call,
            data
        )
```

### Checklist de Migração

#### Obrigatório

- [ ] Atualizar DACP para v2.0 com W3C Trace Context
- [ ] Integrar LLMOrchestrator para gerenciamento de LLMs
- [ ] Implementar StateManager com Event Sourcing
- [ ] Adicionar Circuit Breaker para chamadas externas
- [ ] Configurar Rate Limiting

#### Recomendado

- [ ] Migrar para Agno Framework
- [ ] Implementar PromptCurator
- [ ] Adicionar Outbox Pattern para mensagens
- [ ] Configurar distributed tracing
- [ ] Atualizar testes para v2.0

#### Opcional

- [ ] Otimizar custos com LLMOrchestrator
- [ ] Implementar feedback loops em prompts
- [ ] Adicionar snapshots de estado
- [ ] Configurar dashboards de observabilidade

### Breaking Changes

| Componente | v1.0 | v2.0 | Impacto |
|------------|------|------|---------||
| **DACPMessage** | version="1.0" | version="2.0" + trace_context | 🟡 Médio |
| **BaseAgent** | Estado simples | StateManager obrigatório | 🔴 Alto |
| **LLM Integration** | Direto | LLMOrchestrator recomendado | 🟢 Baixo |
| **Communication** | Sem tracing | W3C Trace Context | 🟡 Médio |

### Suporte de Versões

- **v1.0**: Suporte até 31/03/2026 (4 meses)
- **v2.0**: Suporte ativo (recomendado)
- **v2.1**: Planejado para Q1 2026

---

## 📚 Referências e Recursos

### Frameworks e Bibliotecas

| Framework | Descrição | Uso Recomendado |
|-----------|-----------|-----------------|
| **[LangChain](https://langchain.com)** | Framework para aplicações LLM | Agents conversacionais |
| **[AutoGen](https://github.com/microsoft/autogen)** | Multi-agent conversation framework | Colaboração entre agents |
| **[CrewAI](https://crewai.com)** | Framework para equipes de agents | Workflows complexos |
| **[Swarm](https://github.com/openai/swarm)** | Lightweight multi-agent orchestration | Coordenação simples |
| **[JADE](http://jade.tilab.com)** | Java Agent DEvelopment Framework | Sistemas enterprise |

### Padrões e Especificações

- **[FIPA Standards](http://www.fipa.org/repository/standardspecs.html)** - Foundation for Intelligent Physical Agents
- **[Agent Communication Language (ACL)](http://www.fipa.org/specs/fipa00061/)** - Padrão de comunicação
- **[Contract Net Protocol](http://www.fipa.org/specs/fipa00029/)** - Protocolo de negociação
- **[Multi-Agent Systems](https://www.springer.com/series/6928)** - Literatura acadêmica

### Recursos DATAMETRIA

- **[Web Development Standard](datametria_std_web_dev.md)** - Integração com APIs
- **[Security Standard](datametria_std_security.md)** - Segurança de agents
- **[Logging Standard](datametria_std_logging.md)** - Logging estruturado
- **[AI/ML Standard](datametria_std_ai_ml_development.md)** - Modelos de IA
- **[Microservices Standard](datametria_std_microservices_architecture.md)** - Arquitetura distribuída

---

## 🤝 Contribuição e Suporte

### Como Contribuir

1. **Fork** este repositório
2. **Crie** uma branch para sua feature (`git checkout -b feature/agent-improvement`)
3. **Implemente** seguindo os padrões DATAMETRIA
4. **Teste** thoroughly com cobertura > 80%
5. **Documente** mudanças e decisões
6. **Submeta** um Pull Request

### Suporte Técnico

- **📧 Email**: <agents-support@datametria.io>
- **💬 Discord**: [#agents-development](https://discord.gg/datametria-agents)
- **📖 Wiki**: [wiki.datametria.io/agents](https://wiki.datametria.io/agents)
- **🐛 Issues**: [GitHub Issues](https://github.com/datametria/agents/issues)

### Roadmap

#### v2.0.0 - "Enterprise-Grade Agents" (Q4 2025) ✅ CONCLUÍDO

- [x] LLMOrchestrator para gerenciamento multi-LLM
- [x] PromptCurator com versionamento e feedback
- [x] StateManager com Event Sourcing
- [x] DACP v2.0 com W3C Trace Context
- [x] Agno Framework integration
- [x] Circuit Breaker, Outbox Pattern, Rate Limiting

#### v2.1.0 - "Enhanced Observability" (Q1 2026)

- [ ] Distributed tracing completo
- [ ] Real-time metrics dashboard
- [ ] Anomaly detection automática
- [ ] Performance profiling

#### v2.2.0 - "Advanced Coordination" (Q2 2026)

- [ ] Hierarchical agent structures
- [ ] Dynamic team formation
- [ ] Conflict resolution mechanisms
- [ ] Consensus algorithms

#### v3.0.0 - "Autonomous Operations" (Q3 2026)

- [ ] Self-healing agent networks
- [ ] Automatic scaling decisions
- [ ] Predictive maintenance
- [ ] Auto-optimization

---

<div align="center">

## 🎯 DATAMETRIA Agents Development Standard v2.0.0

**Desenvolvido com ❤️ pela equipe DATAMETRIA**

[![DATAMETRIA](https://img.shields.io/badge/DATAMETRIA-Agents%20Ready-blue?style=for-the-badge)](https://github.com/datametria/DATAMETRIA-standards)
[![Version](https://img.shields.io/badge/version-2.0.0-green?style=for-the-badge)](https://github.com/datametria/DATAMETRIA-standards)
[![Agno](https://img.shields.io/badge/Agno-Framework-orange?style=for-the-badge)](https://github.com/datametria/DATAMETRIA-standards)

### 🆕 Novidades v2.0

✅ **LLMOrchestrator** - Gerenciamento multi-LLM com otimização de custo
✅ **PromptCurator** - Lifecycle de prompts com feedback loops
✅ **StateManager** - Event Sourcing para auditabilidade completa
✅ **DACP v2.0** - W3C Trace Context para distributed tracing
✅ **Agno Framework** - Integração nativa recomendada
✅ **Resiliência** - Circuit Breaker, Outbox Pattern, Rate Limiting

---

*Para dúvidas ou sugestões sobre este standard, entre em contato com a equipe de arquitetura da Datametria.*

**CTO**: Vander Loto | **Email**: <vander.loto@datametria.io> | **Discord**: [#agents-development](https://discord.gg/datametria-agents)

</div>
