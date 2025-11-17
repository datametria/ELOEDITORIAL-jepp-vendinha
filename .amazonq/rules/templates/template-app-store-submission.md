# Template de Submissão para App Stores

<div align="center">

## Guia Completo para Publicação em App Store e Google Play

[![App Store](https://img.shields.io/badge/App%20Store-0D96F6?logo=app-store)](https://developer.apple.com)
[![Google Play](https://img.shields.io/badge/Google%20Play-414141?logo=google-play)](https://play.google.com/console)
[![DATAMETRIA](https://img.shields.io/badge/DATAMETRIA-Standards-blue)](https://github.com/datametria/standards)

[📱 App Store](#-apple-app-store) • [🤖 Google Play](#-google-play-store) • [📋 Checklist](#-checklist-geral) •
[🔒 Compliance](#-compliance-e-políticas) • [📊 Analytics](#-analytics-e-monitoramento)

</div>

---

## 📋 Informações do App

| Campo | Valor |
|-------|-------|
| **Nome do App** | [Nome do Aplicativo] |
| **Bundle ID/Package Name** | [com.datametria.app] |
| **Versão** | [X.X.X] |
| **Build Number** | [XXX] |
| **Plataformas** | [iOS / Android / Ambas] |
| **Categoria** | [Categoria Principal] |
| **Responsável** | [Nome do Responsável] |
| **Data de Submissão** | [DD/MM/AAAA] |

---

## 📱 Apple App Store

### Preparação para Submissão

#### 1. Configuração do Projeto

##### App Store Connect

```bash
# Configurações necessárias no Xcode
# Bundle Identifier: com.datametria.app
# Version: 1.0.0
# Build: 1

# Signing & Capabilities
# Team: [Sua equipe de desenvolvimento]
# Provisioning Profile: App Store Distribution
```

## Info.plist Obrigatório

```xml
<key>CFBundleDisplayName</key>
<string>[Nome do App]</string>
<key>CFBundleIdentifier</key>
<string>com.datametria.app</string>
<key>CFBundleVersion</key>
<string>1</string>
<key>CFBundleShortVersionString</key>
<string>1.0.0</string>
<key>NSCameraUsageDescription</key>
<string>Este app usa a câmera para [descrever uso]</string>
<key>NSLocationWhenInUseUsageDescription</key>
<string>Este app usa localização para [descrever uso]</string>
```

### 2. Assets e Metadados

#### Ícones Necessários

| Tamanho | Uso | Obrigatório |
|---------|-----|-------------|
| 1024x1024 | App Store | ✅ |
| 180x180 | iPhone @3x | ✅ |
| 120x120 | iPhone @2x | ✅ |
| 167x167 | iPad Pro @2x | Se suporta iPad |
| 152x152 | iPad @2x | Se suporta iPad |

##### Screenshots Obrigatórios

- **iPhone 6.7"**: 1290x2796 (mín. 3 screenshots)
- **iPhone 6.5"**: 1242x2688 (mín. 3 screenshots)
- **iPhone 5.5"**: 1242x2208 (mín. 3 screenshots)
- **iPad Pro 12.9"**: 2048x2732 (se suporta iPad)

#### 3. Informações da Store

##### Metadados Obrigatórios

```yaml
App Information:
  Name: "[Nome do App]"
  Subtitle: "[Subtítulo até 30 caracteres]"
  Category: "[Categoria Principal]"
  Secondary Category: "[Categoria Secundária]"

Description:
  Description: |
    [Descrição detalhada do app até 4000 caracteres]

    Principais funcionalidades:
    • [Funcionalidade 1]
    • [Funcionalidade 2]
    • [Funcionalidade 3]

  Keywords: "[palavra1,palavra2,palavra3]" # Até 100 caracteres

Promotional Text: |
  [Texto promocional até 170 caracteres]

Support URL: "https://datametria.io/support"
Marketing URL: "https://datametria.io/app"
Privacy Policy URL: "https://datametria.io/privacy"
```

#### 4. Configurações de Release

##### App Store Connect Configuration

```yaml
Pricing and Availability:
  Price: "[Free/Paid]"
  Availability: "[Worldwide/Specific Countries]"

App Review Information:
  Contact Information:
    First Name: "[Nome]"
    Last Name: "[Sobrenome]"
    Phone Number: "[+55 11 99999-9999]"
    Email: "[email@datametria.io]"

  Demo Account:
    Username: "[demo_user]"
    Password: "[demo_password]"

  Notes: |
    [Informações adicionais para o revisor]

Age Rating:
  Rating: "[4+/9+/12+/17+]"
  Content Descriptions: "[Configurar baseado no conteúdo]"
```

### Processo de Submissão iOS

#### Passo a Passo

1. **Build e Archive**

```bash
# No Xcode
# 1. Selecionar "Any iOS Device"
# 2. Product > Archive
# 3. Aguardar build completar
# 4. Organizer abrirá automaticamente
```

1. **Upload para App Store Connect**

```bash
# No Organizer
# 1. Selecionar o archive
# 2. "Distribute App"
# 3. "App Store Connect"
# 4. "Upload"
# 5. Aguardar processamento (5-30 min)
```

1. **Configurar Release**

```bash
# No App Store Connect
# 1. Acessar "My Apps"
# 2. Selecionar o app
# 3. Ir para "App Store" tab
# 4. Preencher todas as informações
# 5. Adicionar build na seção "Build"
```

1. **Submeter para Review**

```bash
# Verificar checklist completo
# Clicar em "Submit for Review"
# Aguardar aprovação (24-48h típico)
```

---

## 🤖 Google Play Store

### Preparação para Submissão

#### 1. Configuração do Projeto

##### build.gradle (app level)

```gradle
android {
    compileSdkVersion 34

    defaultConfig {
        applicationId "com.datametria.app"
        minSdkVersion 21
        targetSdkVersion 34
        versionCode 1
        versionName "1.0.0"
    }

    signingConfigs {
        release {
            storeFile file('keystore.jks')
            storePassword '[STORE_PASSWORD]'
            keyAlias '[KEY_ALIAS]'
            keyPassword '[KEY_PASSWORD]'
        }
    }

    buildTypes {
        release {
            signingConfig signingConfigs.release
            minifyEnabled true
            proguardFiles getDefaultProguardFile('proguard-android-optimize.txt'), 'proguard-rules.pro'
        }
    }
}
```

##### AndroidManifest.xml

```xml
<manifest xmlns:android="http://schemas.android.com/apk/res/android"
    package="com.datametria.app">

    <uses-permission android:name="android.permission.INTERNET" />
    <uses-permission android:name="android.permission.CAMERA" />
    <uses-permission android:name="android.permission.ACCESS_FINE_LOCATION" />

    <application
        android:name=".MainApplication"
        android:label="[Nome do App]"
        android:icon="@mipmap/ic_launcher"
        android:roundIcon="@mipmap/ic_launcher_round"
        android:theme="@style/AppTheme">

        <activity
            android:name=".MainActivity"
            android:exported="true"
            android:launchMode="singleTop"
            android:theme="@style/LaunchTheme">
            <intent-filter android:autoVerify="true">
                <action android:name="android.intent.action.MAIN"/>
                <category android:name="android.intent.category.LAUNCHER"/>
            </intent-filter>
        </activity>
    </application>
</manifest>
```

#### 2. Assets e Recursos

##### Ícones Necessários

| Densidade | Tamanho | Pasta |
|-----------|---------|-------|
| mdpi | 48x48 | mipmap-mdpi |
| hdpi | 72x72 | mipmap-hdpi |
| xhdpi | 96x96 | mipmap-xhdpi |
| xxhdpi | 144x144 | mipmap-xxhdpi |
| xxxhdpi | 192x192 | mipmap-xxxhdpi |

##### Feature Graphic

- **Tamanho**: 1024x500 pixels
- **Formato**: PNG ou JPEG
- **Uso**: Destaque na Play Store

##### Screenshots

- **Mínimo**: 2 screenshots
- **Máximo**: 8 screenshots
- **Tamanhos suportados**: 320px-3840px
- **Proporção**: 16:9 ou 9:16

#### 3. Play Console Configuration

##### Store Listing

```yaml
App Details:
  App Name: "[Nome do App]"
  Short Description: "[Até 80 caracteres]"
  Full Description: |
    [Descrição completa até 4000 caracteres]

    🚀 Principais funcionalidades:
    • [Funcionalidade 1]
    • [Funcionalidade 2]
    • [Funcionalidade 3]

    📱 Compatibilidade:
    • Android 5.0 ou superior
    • Funciona offline
    • Interface intuitiva

Contact Details:
  Website: "https://datametria.io"
  Email: "support@datametria.io"
  Phone: "+55 11 99999-9999"

Privacy Policy: "https://datametria.io/privacy"
```

### Processo de Submissão Android

#### Passo a Passo

1. **Gerar APK/AAB Assinado**

```bash
# Flutter
flutter build appbundle --release

# React Native
cd android && ./gradlew bundleRelease

# Verificar assinatura
jarsigner -verify -verbose -certs app-release.aab
```

1. **Upload no Play Console**

```bash
# 1. Acessar Play Console
# 2. Selecionar app ou criar novo
# 3. Ir para "Release" > "Production"
# 4. "Create new release"
# 5. Upload do AAB/APK
```

1. **Configurar Release**

```yaml
Release Details:
  Release Name: "1.0.0 - Initial Release"
  Release Notes:
    pt-BR: |
      🎉 Primeira versão do app!

      ✨ Funcionalidades incluídas:
      • [Funcionalidade 1]
      • [Funcionalidade 2]
      • [Funcionalidade 3]

      📧 Suporte: support@datametria.io
```

1. **Review e Publicação**

```bash
# 1. Revisar todas as configurações
# 2. "Review release"
# 3. "Start rollout to production"
# 4. Aguardar aprovação (algumas horas a 7 dias)
```

---

## 📋 Checklist Geral

### Pré-Submissão

#### Desenvolvimento

- [ ] App testado em dispositivos reais
- [ ] Performance otimizada (startup < 3s)
- [ ] Tratamento de erros implementado
- [ ] Offline functionality (se aplicável)
- [ ] Orientação de tela configurada
- [ ] Deep links funcionando
- [ ] Push notifications testadas

#### Assets e Conteúdo

- [ ] Ícones em todas as resoluções
- [ ] Screenshots atualizados
- [ ] Feature graphic criado (Android)
- [ ] Descrições em múltiplos idiomas
- [ ] Política de privacidade atualizada
- [ ] Termos de uso atualizados

#### Compliance

- [ ] LGPD/GDPR compliance verificado
- [ ] Permissões justificadas
- [ ] Dados sensíveis protegidos
- [ ] Criptografia implementada
- [ ] Logs de auditoria configurados

---

## 🔒 Compliance e Políticas

### Políticas Obrigatórias

#### Política de Privacidade

```markdown
# Política de Privacidade - [Nome do App]

## Coleta de Dados

Nosso aplicativo coleta os seguintes tipos de dados:

### Dados Pessoais
- Nome e email (para criação de conta)
- Localização (quando autorizado)
- Fotos (quando usando câmera)

### Dados de Uso
- Analytics de uso do app
- Logs de erro e performance
- Preferências do usuário

## Uso dos Dados

Utilizamos os dados coletados para:
- Fornecer funcionalidades do app
- Melhorar experiência do usuário
- Suporte técnico
- Analytics e métricas

## Compartilhamento

Não compartilhamos dados pessoais com terceiros, exceto:
- Quando exigido por lei
- Para processamento de pagamentos
- Serviços de analytics (dados anonimizados)

## Seus Direitos

- Acessar seus dados
- Corrigir informações
- Deletar sua conta
- Portabilidade de dados

Contato: privacy@datametria.io
```

#### Termos de Uso

```markdown
# Termos de Uso - [Nome do App]

## Aceitação dos Termos

Ao usar este aplicativo, você concorda com estes termos.

## Uso Permitido

- Uso pessoal e não comercial
- Respeitar direitos de outros usuários
- Não violar leis aplicáveis

## Uso Proibido

- Atividades ilegais
- Spam ou conteúdo malicioso
- Tentativas de hack ou engenharia reversa

## Propriedade Intelectual

Todos os direitos reservados à DATAMETRIA.

## Limitação de Responsabilidade

O app é fornecido "como está" sem garantias.

Contato: legal@datametria.io
```

### Configurações de Privacidade

#### iOS - Info.plist

```xml
<!-- Permissões com justificativas claras -->
<key>NSCameraUsageDescription</key>
<string>Usado para capturar fotos de documentos</string>

<key>NSLocationWhenInUseUsageDescription</key>
<string>Usado para encontrar serviços próximos</string>

<key>NSPhotoLibraryUsageDescription</key>
<string>Usado para selecionar fotos do álbum</string>

<key>NSMicrophoneUsageDescription</key>
<string>Usado para gravação de áudio em notas</string>
```

#### Android - Permissões

```xml
<!-- Permissões essenciais -->
<uses-permission android:name="android.permission.INTERNET" />
<uses-permission android:name="android.permission.CAMERA" />
<uses-permission android:name="android.permission.ACCESS_FINE_LOCATION" />
<uses-permission android:name="android.permission.READ_EXTERNAL_STORAGE" />

<!-- Permissões opcionais -->
<uses-permission android:name="android.permission.RECORD_AUDIO"
    android:required="false" />
```

---

## 📊 Analytics e Monitoramento

### Configuração de Analytics

#### Firebase Analytics

```javascript
// Configuração básica
import analytics from '@react-native-firebase/analytics';

// Eventos customizados
const logCustomEvent = async (eventName, parameters) => {
  await analytics().logEvent(eventName, parameters);
};

// Exemplo de uso
logCustomEvent('app_opened', {
  source: 'notification',
  user_type: 'premium'
});
```

#### Métricas Importantes

| Métrica | Descrição | Meta |
|---------|-----------|------|
| **DAU** | Usuários ativos diários | >1000 |
| **Retention D1** | Retenção dia 1 | >40% |
| **Retention D7** | Retenção dia 7 | >20% |
| **Retention D30** | Retenção dia 30 | >10% |
| **Session Duration** | Duração média da sessão | >3min |
| **Crash Rate** | Taxa de crashes | <1% |

### Monitoramento de Performance

#### Crashlytics

```javascript
import crashlytics from '@react-native-firebase/crashlytics';

// Log de erro customizado
crashlytics().recordError(new Error('Erro customizado'));

// Definir usuário
crashlytics().setUserId('user123');

// Atributos customizados
crashlytics().setAttribute('role', 'admin');
```

#### Performance Monitoring

```javascript
import perf from '@react-native-firebase/perf';

// Trace customizado
const trace = await perf().startTrace('custom_trace');
trace.putAttribute('user_type', 'premium');
// ... operação a ser medida
await trace.stop();
```

---

## 🚀 Pós-Publicação

### Monitoramento Inicial

#### Primeiras 24h

- [ ] Verificar downloads/instalações
- [ ] Monitorar crash rate
- [ ] Acompanhar reviews
- [ ] Verificar analytics
- [ ] Testar em dispositivos reais

#### Primeira Semana

- [ ] Analisar métricas de retenção
- [ ] Responder reviews negativos
- [ ] Identificar bugs críticos
- [ ] Planejar hotfixes se necessário

### Estratégia de Updates

#### Versionamento Semântico

```
MAJOR.MINOR.PATCH

Exemplos:
1.0.0 - Lançamento inicial
1.0.1 - Bugfix
1.1.0 - Nova funcionalidade
2.0.0 - Breaking changes
```

#### Cronograma de Updates

| Tipo | Frequência | Exemplo |
|------|------------|----------|
| **Hotfix** | Conforme necessário | 1.0.1 |
| **Minor** | Mensal | 1.1.0 |
| **Major** | Trimestral | 2.0.0 |

---

## ✅ Checklist Final

### Pré-Submissão Completo

#### Técnico

- [ ] App testado em múltiplos dispositivos
- [ ] Performance otimizada
- [ ] Crash rate < 1%
- [ ] Tempo de startup < 3s
- [ ] Funciona offline (se aplicável)
- [ ] Deep links configurados
- [ ] Push notifications testadas

#### Assets

- [ ] Ícones em todas as resoluções
- [ ] Screenshots atualizados
- [ ] Feature graphic (Android)
- [ ] Vídeo preview (opcional)

#### Conteúdo

- [ ] Descrição otimizada para ASO
- [ ] Keywords pesquisadas
- [ ] Traduções revisadas
- [ ] Política de privacidade atualizada

#### Compliance

- [ ] LGPD/GDPR compliance
- [ ] Permissões justificadas
- [ ] Termos de uso atualizados
- [ ] Age rating configurado

### Pós-Submissão

#### Aprovação

- [ ] Monitorar status de review
- [ ] Responder questões dos revisores
- [ ] Preparar comunicação de lançamento

#### Lançamento

- [ ] Anunciar nas redes sociais
- [ ] Notificar usuários beta
- [ ] Monitorar métricas iniciais
- [ ] Preparar suporte ao usuário

---

<div align="center">

**Desenvolvido por**: Equipe DATAMETRIA Mobile
**Última Atualização**: 15/09/2025
**Versão**: 1.0.0

---

## Template completo para App Stores! Publicação garantida! 📱

</div> ] Descrições traduzidas
- [ ] Keywords otimizadas
- [ ] Vídeo preview (opcional)

#### Configurações

- [ ] Bundle ID/Package name correto
- [ ] Versioning configurado
- [ ] Signing certificates válidos
- [ ] Permissions mínimas necessárias
- [ ] Target SDK atualizado
- [ ] Backup rules configuradas

### Compliance e Políticas

#### Políticas das Stores

- [ ] Conteúdo apropriado para idade
- [ ] Sem violação de direitos autorais
- [ ] Funcionalidades reais (não placeholder)
- [ ] Sem conteúdo enganoso
- [ ] Respeita guidelines de design

#### Privacidade e Segurança

- [ ] Privacy Policy publicada
- [ ] Data Safety form preenchido (Android)
- [ ] Permissions justificadas
- [ ] Dados criptografados
- [ ] LGPD/GDPR compliance

#### Acessibilidade

- [ ] Labels de acessibilidade
- [ ] Contraste adequado
- [ ] Navegação por teclado
- [ ] Suporte a screen readers
- [ ] Tamanhos de fonte ajustáveis

---

## 🔒 Compliance e Políticas

### Privacy Policy Template

```markdown
# Política de Privacidade - [Nome do App]

## Coleta de Dados
Este aplicativo coleta as seguintes informações:
- Dados de conta (nome, email)
- Dados de uso (analytics anônimos)
- Localização (apenas quando necessário)

## Uso dos Dados
Os dados coletados são utilizados para:
- Fornecer funcionalidades do app
- Melhorar a experiência do usuário
- Suporte técnico

## Compartilhamento
Não compartilhamos dados pessoais com terceiros, exceto:
- Quando exigido por lei
- Para processamento de pagamentos (se aplicável)

## Seus Direitos (LGPD/GDPR)
Você tem direito a:
- Acessar seus dados
- Corrigir informações incorretas
- Excluir sua conta
- Portabilidade de dados

## Contato
Para questões sobre privacidade: privacy@datametria.io
```

### Data Safety (Android)

```yaml
Data Safety Declaration:
  Data Collection:
    Personal Info:
      - Name: "Collected, Shared"
      - Email: "Collected, Not Shared"

    App Activity:
      - App Interactions: "Collected, Not Shared"
      - Crash Logs: "Collected, Not Shared"

    Device Info:
      - Device ID: "Collected, Not Shared"

  Data Usage:
    - App Functionality: "All collected data"
    - Analytics: "Usage data only"

  Data Sharing:
    - No data shared with third parties

  Security Practices:
    - Data encrypted in transit: "Yes"
    - Data encrypted at rest: "Yes"
    - User can delete data: "Yes"
```

---

## 📊 Configuração de Analytics

### Firebase Analytics

#### Setup iOS

```swift
// AppDelegate.swift
import Firebase

func application(_ application: UIApplication,
                didFinishLaunchingWithOptions launchOptions: [UIApplication.LaunchOptionsKey: Any]?) -> Bool {
    FirebaseApp.configure()
    return true
}
```

#### Setup Android

```java
// MainApplication.java
import com.google.firebase.analytics.FirebaseAnalytics;

public class MainApplication extends Application {
    @Override
    public void onCreate() {
        super.onCreate();
        FirebaseAnalytics.getInstance(this);
    }
}
```

#### Custom Events

```dart
// Flutter
FirebaseAnalytics.instance.logEvent(
  name: 'app_open',
  parameters: {
    'source': 'notification',
    'campaign': 'summer_sale',
  },
);

// Track screen views
FirebaseAnalytics.instance.logScreenView(
  screenName: 'ProductList',
  screenClass: 'ProductListScreen',
);
```

### App Store Connect Analytics

#### Configuração

```yaml
Analytics Configuration:
  App Analytics: "Enabled"
  Custom Product Pages: "Configured"
  A/B Testing: "Enabled for screenshots"

Metrics to Track:
  - App Store Impressions
  - Product Page Views
  - App Units (Downloads)
  - Conversion Rate
  - Retention Rate
```

---

## 🚀 Pós-Lançamento

### Monitoramento

#### Métricas Importantes

| Métrica | Target | Ferramenta |
|---------|--------|------------|
| **Crash Rate** | < 1% | Firebase Crashlytics |
| **ANR Rate** | < 0.5% | Play Console |
| **App Store Rating** | > 4.0 | Store Analytics |
| **Retention D1** | > 40% | Firebase Analytics |
| **Retention D7** | > 20% | Firebase Analytics |

#### Alertas Configurados

```yaml
Crash Alerts:
  - Threshold: "> 1% crash rate"
  - Recipients: "dev-team@datametria.io"

Rating Alerts:
  - Threshold: "< 4.0 stars"
  - Recipients: "product-team@datametria.io"

Performance Alerts:
  - Threshold: "ANR > 0.5%"
  - Recipients: "mobile-team@datametria.io"
```

### Atualizações

#### Processo de Update

```yaml
Update Process:
  1. Development:
     - Increment version code/build number
     - Update version name if needed
     - Test thoroughly

  2. Release Notes:
     - Write clear, user-friendly notes
     - Highlight new features
     - Mention bug fixes

  3. Rollout Strategy:
     - Start with 5% rollout
     - Monitor metrics for 24h
     - Increase to 25%, then 50%, then 100%

  4. Monitoring:
     - Watch crash rates
     - Monitor user feedback
     - Be ready to halt rollout if issues
```

---

## 📞 Suporte e Recursos

### Contatos Importantes

| Área | Contato | Responsabilidade |
|------|---------|------------------|
| **Desenvolvimento** | <dev-team@datametria.io> | Bugs técnicos |
| **Produto** | <product@datametria.io> | Funcionalidades |
| **Suporte** | <support@datametria.io> | Usuários finais |
| **Legal** | <legal@datametria.io> | Compliance |

### Recursos Úteis

#### Apple

- [App Store Review Guidelines](https://developer.apple.com/app-store/review/guidelines/)
- [Human Interface Guidelines](https://developer.apple.com/design/human-interface-guidelines/)
- [App Store Connect Help](https://help.apple.com/app-store-connect/)

#### Google

- [Play Console Help](https://support.google.com/googleplay/android-developer/)
- [Play Policy Center](https://play.google.com/about/developer-content-policy/)
- [Material Design Guidelines](https://material.io/design)

### Templates de Comunicação

#### Resposta a Reviews

```markdown
# Review Response Template

## Positive Review (5 stars)
Obrigado pela avaliação! 🌟 Ficamos felizes que esteja gostando do app.
Continue acompanhando as novidades!

## Constructive Feedback (3-4 stars)
Obrigado pelo feedback! 📝 Suas sugestões são muito valiosas.
Estamos trabalhando em melhorias que serão lançadas em breve.

## Negative Review (1-2 stars)
Lamentamos a experiência negativa. 😔 Por favor, entre em contato
conosco em support@datametria.io para resolvermos o problema.
```

---

<div align="center">

## Submissão para App Stores - DATAMETRIA Standards v3.0.0

**Mantido por**: Equipe Mobile DATAMETRIA
**Última Atualização**: 07/09/2025
**Versão do Template**: 1.0.0

---

**Para dúvidas sobre submissão**: [mobile-team@datametria.io](mailto:mobile-team@datametria.io)

</div>
