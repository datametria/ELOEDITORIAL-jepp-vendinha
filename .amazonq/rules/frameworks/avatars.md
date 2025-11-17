# Interactive Avatars Framework Rules - DATAMETRIA Standards

**Versão:** 1.0.0
**Data:** 07/11/2025
**Autor:** Vander Loto - CTO DATAMETRIA

---

## Rule 1: Ready Player Me para Avatar 3D

### Contexto

Criar avatares 3D do zero é custoso e demorado. Ready Player Me oferece avatares realistas prontos.

### Regra

Avatares 3D DEVEM usar:
- **Ready Player Me**: Geração de avatares (.glb)
- **Avatar Loader**: Carregamento assíncrono
- **Customização**: Personalização via API
- **Otimização**: LOD e compressão

### Justificativa

- Avatares realistas em minutos
- Customização completa (rosto, corpo, roupas)
- Formato .glb otimizado
- Integração Unity nativa

### Exemplos

#### ✅ Correto (Ready Player Me)

```csharp
using ReadyPlayerMe.Core;
using UnityEngine;
using System.Threading.Tasks;

public class AvatarManager : MonoBehaviour
{
    [SerializeField] private Transform avatarSpawnPoint;
    [SerializeField] private AvatarConfig config;

    private GameObject currentAvatar;
    private AvatarObjectLoader loader;

    private void Awake()
    {
        loader = new AvatarObjectLoader();
        loader.OnCompleted += OnAvatarLoaded;
        loader.OnFailed += OnAvatarLoadFailed;
    }

    public async Task LoadAvatar(string avatarUrl)
    {
        // Show loading indicator
        ShowLoadingIndicator(true);

        // Load avatar
        await loader.LoadAvatar(avatarUrl);
    }

    private void OnAvatarLoaded(object sender, CompletionEventArgs args)
    {
        // Destroy previous avatar
        if (currentAvatar != null)
        {
            Destroy(currentAvatar);
        }

        // Setup new avatar
        currentAvatar = args.Avatar;
        currentAvatar.transform.SetParent(avatarSpawnPoint);
        currentAvatar.transform.localPosition = Vector3.zero;
        currentAvatar.transform.localRotation = Quaternion.identity;

        // Setup components
        SetupAnimator(currentAvatar);
        SetupLOD(currentAvatar);

        ShowLoadingIndicator(false);
    }

    private void OnAvatarLoadFailed(object sender, FailureEventArgs args)
    {
        Debug.LogError($"Avatar load failed: {args.Message}");
        ShowLoadingIndicator(false);
    }

    private void SetupAnimator(GameObject avatar)
    {
        var animator = avatar.GetComponent<Animator>();
        if (animator == null)
        {
            animator = avatar.AddComponent<Animator>();
        }
        animator.runtimeAnimatorController = config.animatorController;
    }

    private void SetupLOD(GameObject avatar)
    {
        // Add LOD Group for performance
        var lodGroup = avatar.AddComponent<LODGroup>();
        var renderers = avatar.GetComponentsInChildren<SkinnedMeshRenderer>();

        LOD[] lods = new LOD[3];
        lods[0] = new LOD(0.6f, renderers); // High quality
        lods[1] = new LOD(0.3f, renderers); // Medium quality
        lods[2] = new LOD(0.1f, renderers); // Low quality

        lodGroup.SetLODs(lods);
        lodGroup.RecalculateBounds();
    }
}

[CreateAssetMenu(fileName = "AvatarConfig", menuName = "Avatar/Config")]
public class AvatarConfig : ScriptableObject
{
    public RuntimeAnimatorController animatorController;
    public float loadTimeout = 30f;
    public bool enableLOD = true;
}
```

#### ❌ Incorreto (Avatar manual)

```csharp
// Criar avatar 3D manualmente - muito custoso
public class ManualAvatarCreator : MonoBehaviour
{
    // Centenas de linhas para criar avatar do zero
    // Modelagem, rigging, texturização manual
    // Não escalável
}
```

### Checklist

- [ ] Ready Player Me SDK instalado?
- [ ] Avatar Loader implementado?
- [ ] LOD configurado?
- [ ] Loading indicator presente?

---

## Rule 2: Inworld.ai para Animação e Voz

### Contexto

Lipsync manual e TTS genérico resultam em avatares não realistas. Inworld.ai oferece animação e voz natural.

### Regra

Animação e voz DEVEM usar:
- **Inworld.ai SDK**: Animação facial e TTS
- **Lipsync Automático**: Sincronização labial
- **Emoções**: Happy, Sad, Angry, Surprised
- **Gestos**: Animações corporais contextuais

### Justificativa

- Lipsync automático e preciso
- Emoções realistas
- TTS natural em múltiplos idiomas
- Gestos contextuais

### Exemplos

#### ✅ Correto (Inworld.ai)

```csharp
using Inworld;
using Inworld.Entities;
using UnityEngine;
using System.Collections.Generic;

public class InworldAvatarController : MonoBehaviour
{
    [Header("Inworld Components")]
    [SerializeField] private InworldCharacter character;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private Animator animator;

    [Header("Emotion Mapping")]
    [SerializeField] private EmotionAnimationMap emotionMap;

    private Queue<AudioClip> audioQueue = new();
    private bool isPlaying = false;

    private void Start()
    {
        InitializeInworld();
    }

    private void InitializeInworld()
    {
        // Subscribe to events
        character.OnTextReceived += OnTextReceived;
        character.OnAudioReceived += OnAudioReceived;
        character.OnEmotionChanged += OnEmotionChanged;
        character.OnGestureTriggered += OnGestureTriggered;

        // Configure character
        character.Configuration.Capabilities.Emotions = true;
        character.Configuration.Capabilities.Gestures = true;
        character.Configuration.Capabilities.Audio = true;
    }

    private void OnTextReceived(string text)
    {
        Debug.Log($"Avatar says: {text}");
        // Display text in UI
        UIManager.Instance.ShowSubtitle(text);
    }

    private void OnAudioReceived(AudioClip clip)
    {
        audioQueue.Enqueue(clip);

        if (!isPlaying)
        {
            PlayNextAudio();
        }
    }

    private void PlayNextAudio()
    {
        if (audioQueue.Count == 0)
        {
            isPlaying = false;
            return;
        }

        isPlaying = true;
        var clip = audioQueue.Dequeue();

        audioSource.clip = clip;
        audioSource.Play();

        // Trigger lipsync animation
        animator.SetBool("IsSpeaking", true);

        // Schedule next audio
        Invoke(nameof(OnAudioFinished), clip.length);
    }

    private void OnAudioFinished()
    {
        animator.SetBool("IsSpeaking", false);
        PlayNextAudio();
    }

    private void OnEmotionChanged(EmotionEvent emotionEvent)
    {
        var emotion = emotionEvent.Emotion;
        var intensity = emotionEvent.Intensity;

        Debug.Log($"Emotion: {emotion} ({intensity})");

        // Trigger emotion animation
        if (emotionMap.TryGetAnimation(emotion, out var animationName))
        {
            animator.SetTrigger(animationName);
            animator.SetFloat("EmotionIntensity", intensity);
        }
    }

    private void OnGestureTriggered(GestureEvent gestureEvent)
    {
        var gesture = gestureEvent.Gesture;
        Debug.Log($"Gesture: {gesture}");

        // Trigger gesture animation
        animator.SetTrigger($"Gesture_{gesture}");
    }

    public void SendMessage(string message)
    {
        character.SendText(message);
    }
}

[CreateAssetMenu(fileName = "EmotionMap", menuName = "Avatar/Emotion Map")]
public class EmotionAnimationMap : ScriptableObject
{
    [System.Serializable]
    public class EmotionMapping
    {
        public string emotion;
        public string animationName;
    }

    public List<EmotionMapping> mappings = new();

    public bool TryGetAnimation(string emotion, out string animationName)
    {
        var mapping = mappings.Find(m => m.emotion == emotion);
        animationName = mapping?.animationName;
        return mapping != null;
    }
}
```

#### ❌ Incorreto (TTS genérico)

```csharp
// TTS genérico sem lipsync
public class GenericTTS : MonoBehaviour
{
    public void Speak(string text)
    {
        // TTS genérico - sem lipsync, sem emoções
        var audio = TextToSpeech.Generate(text);
        audioSource.PlayOneShot(audio);
        // Avatar não sincroniza lábios
    }
}
```

### Checklist

- [ ] Inworld.ai SDK instalado?
- [ ] Lipsync automático funcionando?
- [ ] Emoções mapeadas?
- [ ] Gestos implementados?

---

## Rule 3: LangChain para Orquestração de IA

### Contexto

Integração direta com LLMs é complexa e não escalável. LangChain simplifica orquestração.

### Regra

Cognição IA DEVE usar:
- **LangChain**: Orquestração de IA
- **Memory**: Context management
- **Vector DB**: Busca semântica
- **Chains**: Fluxos complexos

### Justificativa

- Orquestração simplificada
- Context management automático
- Busca semântica integrada
- Chains reutilizáveis

### Exemplos

#### ✅ Correto (LangChain)

```python
from langchain.chat_models import ChatOpenAI
from langchain.memory import ConversationBufferMemory
from langchain.chains import ConversationalRetrievalChain
from langchain.vectorstores import Chroma
from langchain.embeddings import OpenAIEmbeddings
from langchain.prompts import ChatPromptTemplate
from typing import Dict, List
import redis.asyncio as redis

class AvatarOrchestrator:
    """Orquestrador de IA para avatares interativos."""

    def __init__(self, redis_url: str = "redis://localhost"):
        self.llm = ChatOpenAI(
            model="gpt-4-turbo-preview",
            temperature=0.7,
            max_tokens=500
        )

        self.embeddings = OpenAIEmbeddings()

        self.vectorstore = Chroma(
            embedding_function=self.embeddings,
            persist_directory="./chroma_db"
        )

        self.redis = redis.from_url(redis_url)

        self.prompt = ChatPromptTemplate.from_messages([
            ("system", """Você é um assistente virtual amigável e prestativo.
            Responda de forma natural e conversacional.
            Use emoções apropriadas: happy, sad, neutral, surprised.
            Mantenha respostas concisas (máximo 3 frases)."""),
            ("human", "{question}")
        ])

    async def get_memory(self, session_id: str) -> ConversationBufferMemory:
        """Recupera memória da sessão do Redis."""
        memory_key = f"memory:{session_id}"
        memory_data = await self.redis.get(memory_key)

        memory = ConversationBufferMemory(
            memory_key="chat_history",
            return_messages=True
        )

        if memory_data:
            # Restore memory from Redis
            memory.chat_memory.messages = json.loads(memory_data)

        return memory

    async def save_memory(self, session_id: str, memory: ConversationBufferMemory):
        """Salva memória da sessão no Redis."""
        memory_key = f"memory:{session_id}"
        memory_data = json.dumps([
            msg.dict() for msg in memory.chat_memory.messages
        ])
        await self.redis.setex(memory_key, 3600, memory_data)  # 1 hour TTL

    async def process_input(
        self,
        session_id: str,
        user_input: str,
        context: Dict = None
    ) -> Dict:
        """Processa input do usuário e retorna resposta."""

        # Get session memory
        memory = await self.get_memory(session_id)

        # Create chain
        chain = ConversationalRetrievalChain.from_llm(
            llm=self.llm,
            retriever=self.vectorstore.as_retriever(
                search_kwargs={"k": 3}
            ),
            memory=memory,
            combine_docs_chain_kwargs={"prompt": self.prompt}
        )

        # Process input
        response = await chain.ainvoke({
            "question": user_input,
            "context": context or {}
        })

        # Save memory
        await self.save_memory(session_id, memory)

        # Extract emotion and actions
        emotion = self._detect_emotion(response["answer"])
        actions = self._extract_actions(response)

        return {
            "text": response["answer"],
            "emotion": emotion,
            "actions": actions,
            "sources": response.get("source_documents", [])
        }

    def _detect_emotion(self, text: str) -> str:
        """Detecta emoção do texto."""
        text_lower = text.lower()

        if any(word in text_lower for word in ["feliz", "ótimo", "excelente", "maravilhoso"]):
            return "happy"
        elif any(word in text_lower for word in ["triste", "infelizmente", "lamento"]):
            return "sad"
        elif any(word in text_lower for word in ["surpresa", "incrível", "uau"]):
            return "surprised"
        else:
            return "neutral"

    def _extract_actions(self, response: Dict) -> List[str]:
        """Extrai ações do response."""
        actions = []
        text = response["answer"].lower()

        if "abrir" in text or "mostrar" in text:
            actions.append("show_content")
        if "enviar" in text or "email" in text:
            actions.append("send_email")
        if "agendar" in text or "marcar" in text:
            actions.append("schedule")

        return actions

    async def add_knowledge(self, documents: List[str]):
        """Adiciona documentos à base de conhecimento."""
        self.vectorstore.add_texts(documents)
        self.vectorstore.persist()
```

#### ❌ Incorreto (Integração direta)

```python
# Integração direta com OpenAI - não escalável
import openai

def process_input(user_input: str) -> str:
    response = openai.ChatCompletion.create(
        model="gpt-4",
        messages=[{"role": "user", "content": user_input}]
    )
    return response.choices[0].message.content
    # Sem context, sem memory, sem busca semântica
```

### Checklist

- [ ] LangChain instalado?
- [ ] Memory configurado?
- [ ] Vector DB integrado?
- [ ] Chains implementados?

---

## Rule 4: FastAPI para Backend

### Contexto

Backend precisa ser rápido, escalável e type-safe. FastAPI oferece performance e validação automática.

### Regra

Backend DEVE usar:
- **FastAPI**: Framework async
- **Pydantic**: Validação de dados
- **WebSocket**: Comunicação real-time
- **Redis**: Cache e context

### Justificativa

- Performance 10x melhor vs Flask
- Validação automática
- OpenAPI automático
- WebSocket nativo

### Exemplos

#### ✅ Correto (FastAPI)

```python
from fastapi import FastAPI, WebSocket, HTTPException
from fastapi.middleware.cors import CORSMiddleware
from pydantic import BaseModel, Field
from typing import Optional, List
import asyncio

app = FastAPI(
    title="Avatar AI Backend",
    version="1.0.0",
    docs_url="/docs"
)

# CORS
app.add_middleware(
    CORSMiddleware,
    allow_origins=["*"],
    allow_credentials=True,
    allow_methods=["*"],
    allow_headers=["*"]
)

# Models
class ChatRequest(BaseModel):
    session_id: str = Field(..., description="Session ID")
    message: str = Field(..., min_length=1, max_length=500)
    context: dict = Field(default_factory=dict)

class ChatResponse(BaseModel):
    text: str
    emotion: str
    audio_url: Optional[str] = None
    actions: List[str] = []

class WebSocketMessage(BaseModel):
    type: str  # "text", "audio", "emotion"
    data: dict

# Endpoints
@app.post("/api/v1/chat", response_model=ChatResponse)
async def chat(request: ChatRequest):
    """Endpoint principal de chat."""
    orchestrator = AvatarOrchestrator()

    response = await orchestrator.process_input(
        request.session_id,
        request.message,
        request.context
    )

    # Generate audio (Inworld.ai)
    audio_url = await generate_audio(response["text"])

    # Trigger webhooks
    await trigger_webhooks(request.session_id, response)

    return ChatResponse(
        text=response["text"],
        emotion=response["emotion"],
        audio_url=audio_url,
        actions=response["actions"]
    )

@app.websocket("/ws/{session_id}")
async def websocket_endpoint(websocket: WebSocket, session_id: str):
    """WebSocket para comunicação real-time."""
    await websocket.accept()

    orchestrator = AvatarOrchestrator()

    try:
        while True:
            # Receive message
            data = await websocket.receive_json()
            message = WebSocketMessage(**data)

            if message.type == "text":
                # Process text input
                response = await orchestrator.process_input(
                    session_id,
                    message.data["text"],
                    message.data.get("context", {})
                )

                # Send response
                await websocket.send_json({
                    "type": "response",
                    "data": response
                })

            elif message.type == "audio":
                # Process audio input (speech-to-text)
                text = await speech_to_text(message.data["audio"])

                response = await orchestrator.process_input(
                    session_id,
                    text,
                    message.data.get("context", {})
                )

                await websocket.send_json({
                    "type": "response",
                    "data": response
                })

    except Exception as e:
        print(f"WebSocket error: {e}")
    finally:
        await websocket.close()

@app.get("/health")
async def health():
    """Health check endpoint."""
    return {"status": "healthy"}
```

#### ❌ Incorreto (Flask síncrono)

```python
from flask import Flask, request

app = Flask(__name__)

@app.route("/chat", methods=["POST"])
def chat():
    # Síncrono - bloqueia thread
    data = request.json
    response = process_input(data["message"])
    return {"text": response}
    # Sem validação, sem async, sem type hints
```

### Checklist

- [ ] FastAPI instalado?
- [ ] Pydantic models criados?
- [ ] WebSocket implementado?
- [ ] CORS configurado?

---

## Rule 5: Event Bus para Automações

### Contexto

Integrações diretas criam acoplamento. Event Bus permite automações desacopladas.

### Regra

Automações DEVEM usar:
- **Event Bus**: Sistema de eventos
- **Webhooks**: Integrações externas
- **Async**: Processamento assíncrono
- **Retry**: Tentativas automáticas

### Justificativa

- Desacoplamento de sistemas
- Automações escaláveis
- Retry automático
- Monitoramento centralizado

### Exemplos

#### ✅ Correto (Event Bus)

```python
from typing import Callable, Dict, List
import asyncio
import httpx

class EventBus:
    """Sistema de eventos para automações."""

    def __init__(self):
        self._subscribers: Dict[str, List[Callable]] = {}
        self._webhooks: Dict[str, List[str]] = {}

    def subscribe(self, event_type: str, handler: Callable):
        """Registra handler para evento."""
        if event_type not in self._subscribers:
            self._subscribers[event_type] = []
        self._subscribers[event_type].append(handler)

    def register_webhook(self, event_type: str, webhook_url: str):
        """Registra webhook para evento."""
        if event_type not in self._webhooks:
            self._webhooks[event_type] = []
        self._webhooks[event_type].append(webhook_url)

    async def publish(self, event_type: str, data: dict):
        """Publica evento."""
        # Call local handlers
        if event_type in self._subscribers:
            tasks = [
                handler(data)
                for handler in self._subscribers[event_type]
            ]
            await asyncio.gather(*tasks, return_exceptions=True)

        # Call webhooks
        if event_type in self._webhooks:
            tasks = [
                self._call_webhook(url, event_type, data)
                for url in self._webhooks[event_type]
            ]
            await asyncio.gather(*tasks, return_exceptions=True)

    async def _call_webhook(
        self,
        url: str,
        event_type: str,
        data: dict,
        max_retries: int = 3
    ):
        """Chama webhook com retry."""
        async with httpx.AsyncClient() as client:
            for attempt in range(max_retries):
                try:
                    response = await client.post(
                        url,
                        json={
                            "event": event_type,
                            "data": data
                        },
                        timeout=10.0
                    )
                    response.raise_for_status()
                    return
                except Exception as e:
                    if attempt == max_retries - 1:
                        print(f"Webhook failed after {max_retries} attempts: {e}")
                    else:
                        await asyncio.sleep(2 ** attempt)  # Exponential backoff

# Usage
event_bus = EventBus()

# Register webhooks
event_bus.register_webhook("interaction_logged", "https://crm.example.com/webhook")
event_bus.register_webhook("question_asked", "https://faq.example.com/webhook")

# Publish events
await event_bus.publish("interaction_logged", {
    "session_id": "123",
    "user_id": "456",
    "message": "Hello",
    "response": "Hi there!"
})
```

#### ❌ Incorreto (Integração direta)

```python
# Integração direta - acoplamento
def process_chat(message: str):
    response = generate_response(message)

    # Acoplamento direto com CRM
    crm_client.log_interaction(message, response)

    # Acoplamento direto com FAQ
    faq_client.update_question(message)

    return response
```

### Checklist

- [ ] Event Bus implementado?
- [ ] Webhooks registrados?
- [ ] Retry configurado?
- [ ] Async processing?

---

## Métricas de Conformidade

| Métrica | Meta | Validação |
|---------|------|-----------|
| Ready Player Me usage | 100% | Code review |
| Inworld.ai integration | 100% | Feature test |
| LangChain orchestration | 100% | Architecture review |
| FastAPI backend | 100% | API docs |
| Event Bus automations | 100% | Integration tests |

---

**Próxima revisão:** 07/02/2026
