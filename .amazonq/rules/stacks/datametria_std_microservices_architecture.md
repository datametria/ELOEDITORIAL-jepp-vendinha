# Diretrizes de Arquitetura de Microserviços - DATAMETRIA

<div align="center">

## Padrões e Práticas para Arquitetura de Microserviços Enterprise

[![Microservices](https://img.shields.io/badge/architecture-microservices-blue)](https://microservices.io)
[![Docker](https://img.shields.io/badge/docker-ready-blue)](https://docker.com)
[![Kubernetes](https://img.shields.io/badge/kubernetes-native-blue)](https://kubernetes.io)
[![API Gateway](https://img.shields.io/badge/api--gateway-enabled-green)](https://aws.amazon.com/api-gateway/)

[🏗️ Arquitetura](#diretrizes-de-arquitetura-de-microservicos-datametria) • [🔧 Padrões](#padroes-e-praticas-para-arquitetura-de-microservicos-enterprise) •
[🚀 Deploy](#deployment-e-orquestracao) • [📊 Monitoramento](#monitoramento)

</div>

---

## 📋 Índice

- [🎯 Visão Geral](#visao-geral)
- [🏗️ Arquitetura de Microserviços](#diretrizes-de-arquitetura-de-microservicos-datametria)
- [🔧 Padrões de Design](#padroes-de-design)
- [🌐 Comunicação entre Serviços](#comunicacao-entre-servicos)
- [🗄️ Gestão de Dados](#gestao-de-dados)
- [🚀 Deployment e Orquestração](#deployment-e-orquestracao)
- [🔒 Segurança](#seguranca)
- [📊 Observabilidade](#observabilidade)
- [🧪 Testes](#piramide-de-testes-para-microservicos)
- [📚 Referências](#referencias)

---

## 🎯 Visão Geral

### Definição

Microserviços são uma abordagem arquitetural para desenvolvimento de aplicações como um conjunto de pequenos serviços independentes, cada um executando em seu próprio processo e comunicando-se através de mecanismos bem definidos.

### Princípios Fundamentais

- **🔄 Single Responsibility**: Cada serviço tem uma responsabilidade específica
- **🚀 Deployability**: Serviços podem ser deployados independentemente
- **📊 Decentralization**: Governança e gestão de dados descentralizadas
- **🛡️ Failure Isolation**: Falhas isoladas não afetam todo o sistema
- **🔧 Technology Diversity**: Liberdade para escolher tecnologias apropriadas

### Quando Usar Microserviços

#### ✅ Cenários Recomendados

- Aplicações complexas com múltiplos domínios
- Equipes grandes (>8 pessoas) trabalhando no mesmo produto
- Necessidade de escalabilidade independente
- Diferentes tecnologias para diferentes funcionalidades
- Ciclos de release independentes

#### ❌ Cenários Não Recomendados

- Aplicações simples com poucos requisitos
- Equipes pequenas (<3 pessoas)
- Domínio de negócio ainda não bem definido
- Falta de expertise em sistemas distribuídos

---

## 🏗️ Arquitetura de Microserviços

### Arquitetura de Referência

```mermaid
flowchart TD
    WEB[Web App]
    MOBILE[Mobile App]
    API_CLIENT[API Client]

    GATEWAY[API Gateway]
    LB[Load Balancer]

    USER_SVC[User Service]
    ORDER_SVC[Order Service]
    PAYMENT_SVC[Payment Service]
    INVENTORY_SVC[Inventory Service]
    NOTIFICATION_SVC[Notification Service]

    USER_DB[(User DB)]
    ORDER_DB[(Order DB)]
    PAYMENT_DB[(Payment DB)]
    INVENTORY_DB[(Inventory DB)]

    SERVICE_MESH[Service Mesh]
    MONITORING[Monitoring]
    LOGGING[Centralized Logging]
    CONFIG[Config Management]

    WEB --> GATEWAY
    MOBILE --> GATEWAY
    API_CLIENT --> GATEWAY

    GATEWAY --> LB
    LB --> USER_SVC
    LB --> ORDER_SVC
    LB --> PAYMENT_SVC
    LB --> INVENTORY_SVC
    LB --> NOTIFICATION_SVC

    USER_SVC --> USER_DB
    ORDER_SVC --> ORDER_DB
    PAYMENT_SVC --> PAYMENT_DB
    INVENTORY_SVC --> INVENTORY_DB

    USER_SVC -.-> SERVICE_MESH
    ORDER_SVC -.-> SERVICE_MESH
    PAYMENT_SVC -.-> SERVICE_MESH
    INVENTORY_SVC -.-> SERVICE_MESH
    NOTIFICATION_SVC -.-> SERVICE_MESH
```

### Decomposição de Serviços

#### Estratégias de Decomposição

#### 1. Domain-Driven Design (DDD)

```mermaid
graph LR
    subgraph "User Management"
        AUTH[Authentication]
        PROFILE[User Profile]
        PERMISSIONS[Permissions]
    end

    subgraph "Order Management"
        CART[Shopping Cart]
        ORDER[Order Processing]
        FULFILLMENT[Order Fulfillment]
    end

    subgraph "Payment Management"
        BILLING[Billing]
        PAYMENT[Payment Processing]
        REFUND[Refund Management]
    end
```

#### 2. Business Capability

- Cada serviço representa uma capacidade de negócio
- Alinhado com estrutura organizacional
- Minimiza dependências entre equipes

#### 3. Data Ownership

- Cada serviço possui seus próprios dados
- Evita compartilhamento de banco de dados
- Garante autonomia e independência

### Tamanhos de Serviços

| Tipo | Linhas de Código | Equipe | Complexidade | Exemplo |
|------|------------------|--------|--------------|---------|
| **Nano** | < 1K | 1-2 devs | Baixa | Validação de CPF |
| **Micro** | 1K-10K | 2-3 devs | Média | Autenticação |
| **Mini** | 10K-50K | 3-5 devs | Alta | Gestão de Pedidos |
| **Macro** | > 50K | 5+ devs | Muito Alta | ERP Module |

---

## 🔧 Padrões de Design

### API Gateway Pattern

#### Responsabilidades

- **Roteamento**: Direcionar requests para serviços apropriados
- **Autenticação**: Validar tokens e credenciais
- **Rate Limiting**: Controlar taxa de requests
- **Transformação**: Adaptar requests/responses
- **Monitoramento**: Coletar métricas e logs

#### Implementação com Kong

```yaml
# kong.yml
_format_version: "3.0"

services:
  - name: user-service
    url: http://user-service:8080
    routes:
      - name: user-routes
        paths:
          - /api/v1/users

  - name: order-service
    url: http://order-service:8080
    routes:
      - name: order-routes
        paths:
          - /api/v1/orders

plugins:
  - name: rate-limiting
    config:
      minute: 100
      hour: 1000

  - name: jwt
    config:
      secret_is_base64: false
```

## Circuit Breaker Pattern

### Implementação com Python

```python
import time
from enum import Enum
from typing import Callable, Any

class CircuitState(Enum):
    CLOSED = "closed"
    OPEN = "open"
    HALF_OPEN = "half_open"

class CircuitBreaker:
    """Circuit Breaker para proteção contra falhas em cascata."""

    def __init__(
        self,
        failure_threshold: int = 5,
        timeout: int = 60,
        expected_exception: Exception = Exception
    ):
        self.failure_threshold = failure_threshold
        self.timeout = timeout
        self.expected_exception = expected_exception

        self.failure_count = 0
        self.last_failure_time = None
        self.state = CircuitState.CLOSED

    def call(self, func: Callable, *args, **kwargs) -> Any:
        """Executa função com proteção do circuit breaker."""

        if self.state == CircuitState.OPEN:
            if self._should_attempt_reset():
                self.state = CircuitState.HALF_OPEN
            else:
                raise Exception("Circuit breaker is OPEN")

        try:
            result = func(*args, **kwargs)
            self._on_success()
            return result

        except self.expected_exception as e:
            self._on_failure()
            raise e

    def _should_attempt_reset(self) -> bool:
        """Verifica se deve tentar resetar o circuit breaker."""
        return (
            self.last_failure_time and
            time.time() - self.last_failure_time >= self.timeout
        )

    def _on_success(self):
        """Callback para sucesso na execução."""
        self.failure_count = 0
        self.state = CircuitState.CLOSED

    def _on_failure(self):
        """Callback para falha na execução."""
        self.failure_count += 1
        self.last_failure_time = time.time()

        if self.failure_count >= self.failure_threshold:
            self.state = CircuitState.OPEN

# Exemplo de uso
circuit_breaker = CircuitBreaker(failure_threshold=3, timeout=30)

def external_service_call():
    # Simulação de chamada para serviço externo
    import requests
    response = requests.get("https://api.external-service.com/data")
    return response.json()

try:
    result = circuit_breaker.call(external_service_call)
    print(f"Success: {result}")
except Exception as e:
    print(f"Circuit breaker prevented call: {e}")
```

## Saga Pattern

### Implementação de Saga Coreografada

```python
from abc import ABC, abstractmethod
from typing import Dict, Any
import json

class SagaStep(ABC):
    """Classe base para steps de uma saga."""

    @abstractmethod
    def execute(self, context: Dict[str, Any]) -> Dict[str, Any]:
        """Executa o step da saga."""
        pass

    @abstractmethod
    def compensate(self, context: Dict[str, Any]) -> Dict[str, Any]:
        """Executa compensação em caso de falha."""
        pass

class OrderSaga:
    """Saga para processamento de pedidos."""

    def __init__(self):
        self.steps = []
        self.executed_steps = []

    def add_step(self, step: SagaStep):
        """Adiciona step à saga."""
        self.steps.append(step)

    def execute(self, initial_context: Dict[str, Any]) -> Dict[str, Any]:
        """Executa a saga completa."""
        context = initial_context.copy()

        try:
            for step in self.steps:
                context = step.execute(context)
                self.executed_steps.append(step)

            return context

        except Exception as e:
            # Executar compensação em ordem reversa
            self._compensate(context)
            raise e

    def _compensate(self, context: Dict[str, Any]):
        """Executa compensação para steps já executados."""
        for step in reversed(self.executed_steps):
            try:
                step.compensate(context)
            except Exception as e:
                # Log erro de compensação mas continua
                print(f"Compensation failed for {step.__class__.__name__}: {e}")

# Implementação de steps específicos
class ReserveInventoryStep(SagaStep):
    def execute(self, context: Dict[str, Any]) -> Dict[str, Any]:
        # Reservar itens no inventário
        inventory_service = InventoryService()
        reservation_id = inventory_service.reserve_items(
            context['order_items']
        )
        context['inventory_reservation_id'] = reservation_id
        return context

    def compensate(self, context: Dict[str, Any]) -> Dict[str, Any]:
        # Cancelar reserva
        if 'inventory_reservation_id' in context:
            inventory_service = InventoryService()
            inventory_service.cancel_reservation(
                context['inventory_reservation_id']
            )
        return context

class ProcessPaymentStep(SagaStep):
    def execute(self, context: Dict[str, Any]) -> Dict[str, Any]:
        # Processar pagamento
        payment_service = PaymentService()
        payment_id = payment_service.charge(
            context['payment_info'],
            context['total_amount']
        )
        context['payment_id'] = payment_id
        return context

    def compensate(self, context: Dict[str, Any]) -> Dict[str, Any]:
        # Estornar pagamento
        if 'payment_id' in context:
            payment_service = PaymentService()
            payment_service.refund(context['payment_id'])
        return context

# Uso da saga
def process_order(order_data: Dict[str, Any]):
    saga = OrderSaga()
    saga.add_step(ReserveInventoryStep())
    saga.add_step(ProcessPaymentStep())
    saga.add_step(CreateOrderStep())

    try:
        result = saga.execute(order_data)
        return {"status": "success", "order_id": result['order_id']}
    except Exception as e:
        return {"status": "failed", "error": str(e)}
```

---

## 🌐 Comunicação entre Serviços

### Padrões de Comunicação

#### 1. Síncrona (Request-Response)

#### REST APIs

```python
# user_service/api.py
from flask import Flask, jsonify, request
from flask_restx import Api, Resource, fields

app = Flask(__name__)
api = Api(app, doc='/docs/')

user_model = api.model('User', {
    'id': fields.Integer(required=True),
    'name': fields.String(required=True),
    'email': fields.String(required=True)
})

@api.route('/users')
class UserList(Resource):
    @api.marshal_list_with(user_model)
    def get(self):
        """Listar todos os usuários."""
        return user_service.get_all_users()

    @api.expect(user_model)
    @api.marshal_with(user_model)
    def post(self):
        """Criar novo usuário."""
        user_data = request.json
        return user_service.create_user(user_data), 201

@api.route('/users/<int:user_id>')
class User(Resource):
    @api.marshal_with(user_model)
    def get(self, user_id):
        """Obter usuário por ID."""
        user = user_service.get_user(user_id)
        if not user:
            api.abort(404, "User not found")
        return user
```

## gRPC

```protobuf
// user.proto
syntax = "proto3";

package user;

service UserService {
    rpc GetUser(GetUserRequest) returns (User);
    rpc CreateUser(CreateUserRequest) returns (User);
    rpc ListUsers(ListUsersRequest) returns (ListUsersResponse);
}

message User {
    int32 id = 1;
    string name = 2;
    string email = 3;
    string created_at = 4;
}

message GetUserRequest {
    int32 id = 1;
}

message CreateUserRequest {
    string name = 1;
    string email = 2;
}

message ListUsersRequest {
    int32 page = 1;
    int32 page_size = 2;
}

message ListUsersResponse {
    repeated User users = 1;
    int32 total = 2;
}
```

### 2. Assíncrona (Event-Driven)

#### Apache Kafka

```python
# event_publisher.py
from kafka import KafkaProducer
import json
from datetime import datetime

class EventPublisher:
    """Publisher de eventos para Kafka."""

    def __init__(self, bootstrap_servers: str = 'localhost:9092'):
        self.producer = KafkaProducer(
            bootstrap_servers=bootstrap_servers,
            value_serializer=lambda v: json.dumps(v).encode('utf-8'),
            key_serializer=lambda k: k.encode('utf-8') if k else None
        )

    def publish_user_created(self, user_data: dict):
        """Publica evento de usuário criado."""
        event = {
            'event_type': 'user.created',
            'timestamp': datetime.utcnow().isoformat(),
            'data': user_data,
            'version': '1.0'
        }

        self.producer.send(
            topic='user-events',
            key=str(user_data['id']),
            value=event
        )
        self.producer.flush()

# event_consumer.py
from kafka import KafkaConsumer
import json

class EventConsumer:
    """Consumer de eventos do Kafka."""

    def __init__(self, topics: list, group_id: str):
        self.consumer = KafkaConsumer(
            *topics,
            bootstrap_servers='localhost:9092',
            group_id=group_id,
            value_deserializer=lambda m: json.loads(m.decode('utf-8')),
            auto_offset_reset='earliest'
        )

    def start_consuming(self):
        """Inicia consumo de eventos."""
        for message in self.consumer:
            try:
                event = message.value
                self.handle_event(event)
            except Exception as e:
                print(f"Error processing event: {e}")

    def handle_event(self, event: dict):
        """Processa evento recebido."""
        event_type = event.get('event_type')

        if event_type == 'user.created':
            self.handle_user_created(event['data'])
        elif event_type == 'order.placed':
            self.handle_order_placed(event['data'])

    def handle_user_created(self, user_data: dict):
        """Processa evento de usuário criado."""
        # Enviar email de boas-vindas
        email_service.send_welcome_email(user_data['email'])

        # Criar perfil padrão
        profile_service.create_default_profile(user_data['id'])
```

---

## 🗄️ Gestão de Dados

### Database per Service Pattern

#### Estratégias de Dados

#### 1. Banco de Dados Dedicado

```yaml
# docker-compose.yml
version: '3.8'

services:
  user-service:
    image: user-service:latest
    environment:
      - DATABASE_URL=postgresql://user:pass@user-db:5432/users
    depends_on:
      - user-db

  user-db:
    image: postgres:13
    environment:
      - POSTGRES_DB=users
      - POSTGRES_USER=user
      - POSTGRES_PASSWORD=pass
    volumes:
      - user_data:/var/lib/postgresql/data

  order-service:
    image: order-service:latest
    environment:
      - DATABASE_URL=postgresql://order:pass@order-db:5432/orders
    depends_on:
      - order-db

  order-db:
    image: postgres:13
    environment:
      - POSTGRES_DB=orders
      - POSTGRES_USER=order
      - POSTGRES_PASSWORD=pass
    volumes:
      - order_data:/var/lib/postgresql/data

volumes:
  user_data:
  order_data:
```

## 2. Diferentes Tecnologias de Banco

```python
# user_service/models.py - PostgreSQL
from sqlalchemy import create_engine, Column, Integer, String, DateTime
from sqlalchemy.ext.declarative import declarative_base
from sqlalchemy.orm import sessionmaker

Base = declarative_base()

class User(Base):
    __tablename__ = 'users'

    id = Column(Integer, primary_key=True)
    name = Column(String(100), nullable=False)
    email = Column(String(255), unique=True, nullable=False)
    created_at = Column(DateTime, nullable=False)

# order_service/models.py - MongoDB
from mongoengine import Document, StringField, IntField, DateTimeField, ListField

class Order(Document):
    user_id = IntField(required=True)
    status = StringField(choices=['pending', 'confirmed', 'shipped', 'delivered'])
    items = ListField(StringField())
    total_amount = IntField()
    created_at = DateTimeField()

    meta = {
        'collection': 'orders',
        'indexes': ['user_id', 'status', 'created_at']
    }
```

## Data Consistency Patterns

### Event Sourcing

```python
from abc import ABC, abstractmethod
from typing import List, Dict, Any
from datetime import datetime
import json

class Event(ABC):
    """Classe base para eventos."""

    def __init__(self, aggregate_id: str, version: int):
        self.aggregate_id = aggregate_id
        self.version = version
        self.timestamp = datetime.utcnow()

    @abstractmethod
    def to_dict(self) -> Dict[str, Any]:
        """Serializa evento para dicionário."""
        pass

class UserCreatedEvent(Event):
    def __init__(self, aggregate_id: str, version: int, name: str, email: str):
        super().__init__(aggregate_id, version)
        self.name = name
        self.email = email

    def to_dict(self) -> Dict[str, Any]:
        return {
            'event_type': 'UserCreated',
            'aggregate_id': self.aggregate_id,
            'version': self.version,
            'timestamp': self.timestamp.isoformat(),
            'data': {
                'name': self.name,
                'email': self.email
            }
        }

class EventStore:
    """Store de eventos."""

    def __init__(self):
        self.events = []  # Em produção, usar banco de dados

    def save_events(self, aggregate_id: str, events: List[Event], expected_version: int):
        """Salva eventos no store."""
        current_version = self.get_current_version(aggregate_id)

        if current_version != expected_version:
            raise Exception("Concurrency conflict")

        for event in events:
            self.events.append(event)

    def get_events(self, aggregate_id: str) -> List[Event]:
        """Obtém eventos de um aggregate."""
        return [e for e in self.events if e.aggregate_id == aggregate_id]

    def get_current_version(self, aggregate_id: str) -> int:
        """Obtém versão atual do aggregate."""
        events = self.get_events(aggregate_id)
        return len(events)

class UserAggregate:
    """Aggregate de usuário usando Event Sourcing."""

    def __init__(self, user_id: str):
        self.id = user_id
        self.name = None
        self.email = None
        self.version = 0
        self.uncommitted_events = []

    def create_user(self, name: str, email: str):
        """Cria novo usuário."""
        if self.name is not None:
            raise Exception("User already exists")

        event = UserCreatedEvent(self.id, self.version + 1, name, email)
        self.apply_event(event)
        self.uncommitted_events.append(event)

    def apply_event(self, event: Event):
        """Aplica evento ao aggregate."""
        if isinstance(event, UserCreatedEvent):
            self.name = event.name
            self.email = event.email
            self.version = event.version

    def load_from_history(self, events: List[Event]):
        """Reconstrói aggregate a partir do histórico."""
        for event in events:
            self.apply_event(event)

    def get_uncommitted_events(self) -> List[Event]:
        """Obtém eventos não commitados."""
        return self.uncommitted_events.copy()

    def mark_events_as_committed(self):
        """Marca eventos como commitados."""
        self.uncommitted_events.clear()
```

---

## 🚀 Deployment e Orquestração

### Containerização com Docker

#### Dockerfile Multi-stage

```dockerfile
# Dockerfile para serviço Python
FROM python:3.11-slim as builder

WORKDIR /app

# Instalar dependências de build
RUN apt-get update && apt-get install -y \
    gcc \
    && rm -rf /var/lib/apt/lists/*

# Copiar requirements e instalar dependências
COPY requirements.txt .
RUN pip install --no-cache-dir --user -r requirements.txt

# Stage de produção
FROM python:3.11-slim

WORKDIR /app

# Copiar dependências do stage builder
COPY --from=builder /root/.local /root/.local

# Copiar código da aplicação
COPY . .

# Criar usuário não-root
RUN useradd --create-home --shell /bin/bash app \
    && chown -R app:app /app
USER app

# Configurar PATH
ENV PATH=/root/.local/bin:$PATH

# Health check
HEALTHCHECK --interval=30s --timeout=3s --start-period=5s --retries=3 \
    CMD curl -f http://localhost:8080/health || exit 1

EXPOSE 8080

CMD ["python", "-m", "gunicorn", "--bind", "0.0.0.0:8080", "app:app"]
```

## Kubernetes Deployment

### Deployment Manifest

```yaml
# user-service-deployment.yaml
apiVersion: apps/v1
kind: Deployment
metadata:
  name: user-service
  labels:
    app: user-service
    version: v1
spec:
  replicas: 3
  selector:
    matchLabels:
      app: user-service
  template:
    metadata:
      labels:
        app: user-service
        version: v1
    spec:
      containers:
      - name: user-service
        image: datametria/user-service:v1.2.0
        ports:
        - containerPort: 8080
        env:
        - name: DATABASE_URL
          valueFrom:
            secretKeyRef:
              name: user-service-secrets
              key: database-url
        - name: REDIS_URL
          valueFrom:
            configMapKeyRef:
              name: user-service-config
              key: redis-url
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
            port: 8080
          initialDelaySeconds: 30
          periodSeconds: 10
        readinessProbe:
          httpGet:
            path: /ready
            port: 8080
          initialDelaySeconds: 5
          periodSeconds: 5
---
apiVersion: v1
kind: Service
metadata:
  name: user-service
spec:
  selector:
    app: user-service
  ports:
  - protocol: TCP
    port: 80
    targetPort: 8080
  type: ClusterIP
---
apiVersion: v1
kind: ConfigMap
metadata:
  name: user-service-config
data:
  redis-url: "redis://redis-service:6379"
  log-level: "INFO"
---
apiVersion: v1
kind: Secret
metadata:
  name: user-service-secrets
type: Opaque
data:
  database-url: cG9zdGdyZXNxbDovL3VzZXI6cGFzc0B1c2VyLWRiOjU0MzIvdXNlcnM=
```

## Service Mesh com Istio

### Virtual Service

```yaml
# user-service-virtualservice.yaml
apiVersion: networking.istio.io/v1beta1
kind: VirtualService
metadata:
  name: user-service
spec:
  hosts:
  - user-service
  http:
  - match:
    - headers:
        version:
          exact: v2
    route:
    - destination:
        host: user-service
        subset: v2
      weight: 100
  - route:
    - destination:
        host: user-service
        subset: v1
      weight: 90
    - destination:
        host: user-service
        subset: v2
      weight: 10
---
apiVersion: networking.istio.io/v1beta1
kind: DestinationRule
metadata:
  name: user-service
spec:
  host: user-service
  subsets:
  - name: v1
    labels:
      version: v1
  - name: v2
    labels:
      version: v2
  trafficPolicy:
    circuitBreaker:
      consecutiveErrors: 3
      interval: 30s
      baseEjectionTime: 30s
```

---

## 🔒 Segurança

### Autenticação e Autorização

#### JWT com OAuth 2.0

```python
# auth_service.py
import jwt
from datetime import datetime, timedelta
from flask import Flask, request, jsonify
from functools import wraps

class AuthService:
    """Serviço de autenticação JWT."""

    def __init__(self, secret_key: str, algorithm: str = 'HS256'):
        self.secret_key = secret_key
        self.algorithm = algorithm

    def generate_token(self, user_id: int, roles: list = None) -> str:
        """Gera token JWT."""
        payload = {
            'user_id': user_id,
            'roles': roles or [],
            'iat': datetime.utcnow(),
            'exp': datetime.utcnow() + timedelta(hours=24)
        }

        return jwt.encode(payload, self.secret_key, algorithm=self.algorithm)

    def verify_token(self, token: str) -> dict:
        """Verifica e decodifica token JWT."""
        try:
            payload = jwt.decode(token, self.secret_key, algorithms=[self.algorithm])
            return payload
        except jwt.ExpiredSignatureError:
            raise Exception("Token expired")
        except jwt.InvalidTokenError:
            raise Exception("Invalid token")

def require_auth(roles: list = None):
    """Decorator para exigir autenticação."""
    def decorator(f):
        @wraps(f)
        def decorated_function(*args, **kwargs):
            token = request.headers.get('Authorization')

            if not token:
                return jsonify({'error': 'Token missing'}), 401

            try:
                # Remove 'Bearer ' prefix
                token = token.split(' ')[1] if token.startswith('Bearer ') else token

                auth_service = AuthService(current_app.config['SECRET_KEY'])
                payload = auth_service.verify_token(token)

                # Verificar roles se especificadas
                if roles:
                    user_roles = payload.get('roles', [])
                    if not any(role in user_roles for role in roles):
                        return jsonify({'error': 'Insufficient permissions'}), 403

                # Adicionar payload ao contexto da request
                request.current_user = payload

            except Exception as e:
                return jsonify({'error': str(e)}), 401

            return f(*args, **kwargs)
        return decorated_function
    return decorator

# Uso do decorator
@app.route('/admin/users')
@require_auth(roles=['admin'])
def get_admin_users():
    """Endpoint que requer role de admin."""
    return jsonify({'users': admin_user_service.get_all_users()})
```

## mTLS (Mutual TLS)

### Configuração com Istio

```yaml
# mtls-policy.yaml
apiVersion: security.istio.io/v1beta1
kind: PeerAuthentication
metadata:
  name: default
  namespace: production
spec:
  mtls:
    mode: STRICT
---
apiVersion: security.istio.io/v1beta1
kind: AuthorizationPolicy
metadata:
  name: user-service-authz
  namespace: production
spec:
  selector:
    matchLabels:
      app: user-service
  rules:
  - from:
    - source:
        principals: ["cluster.local/ns/production/sa/order-service"]
    to:
    - operation:
        methods: ["GET", "POST"]
        paths: ["/api/v1/users/*"]
```

---

## 📊 Observabilidade

### Distributed Tracing

#### OpenTelemetry com Python

```python
# tracing.py
from opentelemetry import trace
from opentelemetry.exporter.jaeger.thrift import JaegerExporter
from opentelemetry.sdk.trace import TracerProvider
from opentelemetry.sdk.trace.export import BatchSpanProcessor
from opentelemetry.instrumentation.flask import FlaskInstrumentor
from opentelemetry.instrumentation.requests import RequestsInstrumentor
from opentelemetry.instrumentation.sqlalchemy import SQLAlchemyInstrumentor

def setup_tracing(service_name: str, jaeger_endpoint: str):
    """Configura distributed tracing."""

    # Configurar provider
    trace.set_tracer_provider(TracerProvider())
    tracer = trace.get_tracer(__name__)

    # Configurar exporter para Jaeger
    jaeger_exporter = JaegerExporter(
        agent_host_name="jaeger",
        agent_port=6831,
    )

    # Configurar processor
    span_processor = BatchSpanProcessor(jaeger_exporter)
    trace.get_tracer_provider().add_span_processor(span_processor)

    # Instrumentar bibliotecas automaticamente
    FlaskInstrumentor().instrument()
    RequestsInstrumentor().instrument()
    SQLAlchemyInstrumentor().instrument()

    return tracer

# user_service.py
from flask import Flask
import requests

app = Flask(__name__)
tracer = setup_tracing("user-service", "http://jaeger:14268")

@app.route('/users/<int:user_id>')
def get_user(user_id):
    """Obtém usuário com tracing."""

    with tracer.start_as_current_span("get_user") as span:
        span.set_attribute("user.id", user_id)

        # Buscar usuário no banco
        with tracer.start_as_current_span("database_query") as db_span:
            user = user_repository.get_by_id(user_id)
            db_span.set_attribute("db.operation", "SELECT")
            db_span.set_attribute("db.table", "users")

        if not user:
            span.set_attribute("error", True)
            span.set_attribute("error.message", "User not found")
            return {"error": "User not found"}, 404

        # Buscar dados adicionais de outro serviço
        with tracer.start_as_current_span("external_service_call") as ext_span:
            response = requests.get(f"http://profile-service/profiles/{user_id}")
            ext_span.set_attribute("http.method", "GET")
            ext_span.set_attribute("http.url", f"http://profile-service/profiles/{user_id}")
            ext_span.set_attribute("http.status_code", response.status_code)

        return {"user": user, "profile": response.json()}
```

## Métricas com Prometheus

### Instrumentação de Métricas

```python
# metrics.py
from prometheus_client import Counter, Histogram, Gauge, generate_latest
from flask import Flask, Response
import time

# Definir métricas
REQUEST_COUNT = Counter(
    'http_requests_total',
    'Total HTTP requests',
    ['method', 'endpoint', 'status']
)

REQUEST_DURATION = Histogram(
    'http_request_duration_seconds',
    'HTTP request duration',
    ['method', 'endpoint']
)

ACTIVE_CONNECTIONS = Gauge(
    'active_connections',
    'Active database connections'
)

def setup_metrics(app: Flask):
    """Configura métricas para aplicação Flask."""

    @app.before_request
    def before_request():
        """Callback executado antes de cada request."""
        request.start_time = time.time()

    @app.after_request
    def after_request(response):
        """Callback executado após cada request."""
        duration = time.time() - request.start_time

        REQUEST_COUNT.labels(
            method=request.method,
            endpoint=request.endpoint or 'unknown',
            status=response.status_code
        ).inc()

        REQUEST_DURATION.labels(
            method=request.method,
            endpoint=request.endpoint or 'unknown'
        ).observe(duration)

        return response

    @app.route('/metrics')
    def metrics():
        """Endpoint para métricas do Prometheus."""
        return Response(generate_latest(), mimetype='text/plain')

# Uso em serviço
app = Flask(__name__)
setup_metrics(app)

@app.route('/users')
def get_users():
    # Atualizar métrica de conexões ativas
    ACTIVE_CONNECTIONS.set(database.get_active_connections())

    users = user_service.get_all_users()
    return {"users": users}
```

## Logging Estruturado

### Configuração de Logs

```python
# logging_config.py
import logging
import json
from datetime import datetime
from flask import request, g

class StructuredFormatter(logging.Formatter):
    """Formatter para logs estruturados em JSON."""

    def format(self, record):
        """Formata log record como JSON."""
        log_entry = {
            'timestamp': datetime.utcnow().isoformat(),
            'level': record.levelname,
            'service': 'user-service',
            'message': record.getMessage(),
            'logger': record.name,
            'module': record.module,
            'function': record.funcName,
            'line': record.lineno
        }

        # Adicionar contexto da request se disponível
        if hasattr(g, 'trace_id'):
            log_entry['trace_id'] = g.trace_id

        if request:
            log_entry['request'] = {
                'method': request.method,
                'path': request.path,
                'remote_addr': request.remote_addr,
                'user_agent': request.headers.get('User-Agent')
            }

        # Adicionar campos extras
        if hasattr(record, 'extra_fields'):
            log_entry.update(record.extra_fields)

        return json.dumps(log_entry)

def setup_logging():
    """Configura logging estruturado."""

    # Configurar handler
    handler = logging.StreamHandler()
    handler.setFormatter(StructuredFormatter())

    # Configurar logger raiz
    root_logger = logging.getLogger()
    root_logger.setLevel(logging.INFO)
    root_logger.addHandler(handler)

    # Configurar loggers específicos
    logging.getLogger('sqlalchemy.engine').setLevel(logging.WARNING)
    logging.getLogger('werkzeug').setLevel(logging.WARNING)

# Uso em aplicação
setup_logging()
logger = logging.getLogger(__name__)

@app.route('/users', methods=['POST'])
def create_user():
    """Cria usuário com logging estruturado."""

    user_data = request.json

    logger.info(
        "Creating user",
        extra={
            'extra_fields': {
                'user_email': user_data.get('email'),
                'operation': 'create_user'
            }
        }
    )

    try:
        user = user_service.create_user(user_data)

        logger.info(
            "User created successfully",
            extra={
                'extra_fields': {
                    'user_id': user.id,
                    'user_email': user.email,
                    'operation': 'create_user'
                }
            }
        )

        return {"user": user.to_dict()}, 201

    except Exception as e:
        logger.error(
            "Failed to create user",
            extra={
                'extra_fields': {
                    'error': str(e),
                    'user_email': user_data.get('email'),
                    'operation': 'create_user'
                }
            }
        )
        raise
```

---

## 🧪 Estratégias de Teste

### Pirâmide de Testes para Microserviços

```mermaid
graph TD
    subgraph "Test Pyramid"
        E2E[End-to-End Tests<br/>10%]
        INTEGRATION[Integration Tests<br/>20%]
        CONTRACT[Contract Tests<br/>20%]
        UNIT[Unit Tests<br/>50%]
    end

    E2E --> INTEGRATION
    INTEGRATION --> CONTRACT
    CONTRACT --> UNIT
```

### Contract Testing com Pact

#### Consumer Test

```python
# consumer_test.py (Order Service)
import pytest
from pact import Consumer, Provider
import requests

pact = Consumer('order-service').has_pact_with(Provider('user-service'))

@pytest.fixture
def user_service_url():
    return f"http://localhost:{pact.port}"

def test_get_user_success():
    """Testa contrato para obter usuário com sucesso."""

    expected_user = {
        'id': 123,
        'name': 'John Doe',
        'email': 'john@example.com'
    }

    (pact
     .given('user 123 exists')
     .upon_receiving('a request for user 123')
     .with_request('GET', '/api/v1/users/123')
     .will_respond_with(200, body=expected_user))

    with pact:
        response = requests.get(f"{user_service_url}/api/v1/users/123")
        assert response.status_code == 200
        assert response.json() == expected_user

def test_get_user_not_found():
    """Testa contrato para usuário não encontrado."""

    (pact
     .given('user 999 does not exist')
     .upon_receiving('a request for user 999')
     .with_request('GET', '/api/v1/users/999')
     .will_respond_with(404, body={'error': 'User not found'}))

    with pact:
        response = requests.get(f"{user_service_url}/api/v1/users/999")
        assert response.status_code == 404
        assert response.json() == {'error': 'User not found'}
```

## Provider Test

```python
# provider_test.py (User Service)
import pytest
from pact import Verifier
from user_service import create_app, db
from user_service.models import User

@pytest.fixture
def app():
    """Cria aplicação para testes."""
    app = create_app('testing')
    with app.app_context():
        db.create_all()
        yield app
        db.drop_all()

def test_user_service_contract():
    """Verifica se User Service atende aos contratos."""

    verifier = Verifier(provider='user-service', provider_base_url='http://localhost:5000')

    # Estados para os testes
    def provider_states():
        return {
            'user 123 exists': setup_user_123_exists,
            'user 999 does not exist': setup_user_999_not_exists
        }

    verifier.verify_pacts(
        './pacts/order-service-user-service.json',
        provider_states=provider_states()
    )

def setup_user_123_exists():
    """Setup para estado 'user 123 exists'."""
    user = User(id=123, name='John Doe', email='john@example.com')
    db.session.add(user)
    db.session.commit()

def setup_user_999_not_exists():
    """Setup para estado 'user 999 does not exist'."""
    # Garantir que usuário 999 não existe
    User.query.filter_by(id=999).delete()
    db.session.commit()
```

## Integration Testing

### Testcontainers

```python
# integration_test.py
import pytest
import requests
from testcontainers.postgres import PostgresContainer
from testcontainers.redis import RedisContainer
from user_service import create_app

@pytest.fixture(scope="session")
def postgres_container():
    """Container PostgreSQL para testes."""
    with PostgresContainer("postgres:13") as postgres:
        yield postgres

@pytest.fixture(scope="session")
def redis_container():
    """Container Redis para testes."""
    with RedisContainer("redis:6") as redis:
        yield redis

@pytest.fixture
def app(postgres_container, redis_container):
    """Aplicação configurada com containers de teste."""

    app = create_app({
        'TESTING': True,
        'DATABASE_URL': postgres_container.get_connection_url(),
        'REDIS_URL': f"redis://{redis_container.get_container_host_ip()}:{redis_container.get_exposed_port(6379)}"
    })

    with app.app_context():
        from user_service import db
        db.create_all()
        yield app
        db.drop_all()

def test_create_user_integration(app):
    """Teste de integração para criação de usuário."""

    with app.test_client() as client:
        # Criar usuário
        response = client.post('/api/v1/users', json={
            'name': 'Test User',
            'email': 'test@example.com'
        })

        assert response.status_code == 201
        user_data = response.json

        # Verificar se usuário foi criado
        response = client.get(f"/api/v1/users/{user_data['id']}")
        assert response.status_code == 200
        assert response.json['email'] == 'test@example.com'

        # Verificar se cache foi atualizado (Redis)
        from user_service.cache import get_user_from_cache
        cached_user = get_user_from_cache(user_data['id'])
        assert cached_user is not None
        assert cached_user['email'] == 'test@example.com'
```

---

## 📚 Referências

### Livros Recomendados

- **"Building Microservices"** - Sam Newman
- **"Microservices Patterns"** - Chris Richardson
- **"Production-Ready Microservices"** - Susan Fowler
- **"Monolith to Microservices"** - Sam Newman

### Recursos Online

- **[Microservices.io](https://microservices.io)** - Padrões e práticas
- **[12 Factor App](https://12factor.net)** - Metodologia para SaaS
- **[CNCF Landscape](https://landscape.cncf.io)** - Ferramentas cloud native
- **[OpenAPI Specification](https://swagger.io/specification/)** - Padrão para APIs REST

### Ferramentas Recomendadas

| Categoria | Ferramenta | Descrição |
|-----------|------------|-----------|
| **API Gateway** | Kong, Istio Gateway | Roteamento e políticas |
| **Service Mesh** | Istio, Linkerd | Comunicação entre serviços |
| **Monitoring** | Prometheus, Grafana | Métricas e dashboards |
| **Tracing** | Jaeger, Zipkin | Distributed tracing |
| **Logging** | ELK Stack, Fluentd | Logs centralizados |
| **Testing** | Pact, Testcontainers | Contract e integration testing |

---

<div align="center">

**Mantido por**: Equipe de Arquitetura DATAMETRIA
**Última Atualização**: 15/01/2025
**Versão**: 1.0.0

---

**Para dúvidas sobre microserviços**: [vander.loto@datametria.io](mailto:vander.loto@datametria.io)

</div>
