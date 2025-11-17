# DATAMETRIA Compliance Dashboard

<div align="center">

## Dashboard Enterprise de Conformidade e Qualidade

[![Dashboard](https://img.shields.io/badge/dashboard-enterprise-blue)](https://datametria.io)
[![Real-time](https://img.shields.io/badge/real--time-metrics-green)](https://datametria.io)
[![Compliance](https://img.shields.io/badge/compliance-tracking-orange)](https://datametria.io)
[![DATAMETRIA](https://img.shields.io/badge/DATAMETRIA-Standards-blue)](https://github.com/datametria/standards)

[📊 Métricas](#-real-time-metrics) • [📋 Compliance](#-compliance-tracking) • [📈 Relatórios](#-quality-reports) •
[👥 Analytics](#-team-analytics) • [🔌 APIs](#-integration-apis)

</div>

---

## 🎯 Visão Geral

### Propósito do Dashboard

O **DATAMETRIA Compliance Dashboard** é uma plataforma centralizada para monitoramento em tempo real da conformidade com padrões de desenvolvimento, qualidade de código, segurança e métricas de equipe em todos os projetos da organização.

### Objetivos Principais

- **📊 Visibilidade Total**: Métricas em tempo real de todos os projetos
- **📋 Compliance Tracking**: Rastreamento de conformidade com padrões DATAMETRIA
- **📈 Quality Insights**: Insights de qualidade e performance
- **👥 Team Analytics**: Analytics de produtividade e colaboração
- **🔌 Integration Ready**: APIs para integração com ferramentas existentes

---

## 📊 Real-time Metrics

### Arquitetura do Sistema

```mermaid
graph TB
    subgraph "Data Sources"
        GIT[Git Repositories]
        CI[CI/CD Pipelines]
        SONAR[SonarQube]
        SECURITY[Security Scanners]
        TESTS[Test Results]
        DEPLOY[Deployment Metrics]
    end

    subgraph "Data Processing"
        COLLECTOR[Data Collector]
        PROCESSOR[Metrics Processor]
        AGGREGATOR[Data Aggregator]
    end

    subgraph "Storage"
        TIMESERIES[(Time Series DB)]
        METADATA[(Metadata DB)]
        CACHE[(Redis Cache)]
    end

    subgraph "Dashboard Layer"
        API[Dashboard API]
        WEB[Web Dashboard]
        MOBILE[Mobile App]
        ALERTS[Alert System]
    end

    GIT --> COLLECTOR
    CI --> COLLECTOR
    SONAR --> COLLECTOR
    SECURITY --> COLLECTOR
    TESTS --> COLLECTOR
    DEPLOY --> COLLECTOR

    COLLECTOR --> PROCESSOR
    PROCESSOR --> AGGREGATOR
    AGGREGATOR --> TIMESERIES
    AGGREGATOR --> METADATA

    TIMESERIES --> CACHE
    METADATA --> CACHE
    CACHE --> API

    API --> WEB
    API --> MOBILE
    API --> ALERTS
```

### Core Metrics Coletadas

#### Code Quality Metrics

```typescript
interface CodeQualityMetrics {
  // Cobertura de Testes
  testCoverage: {
    unit: number;           // % cobertura testes unitários
    integration: number;    // % cobertura testes integração
    e2e: number;           // % cobertura testes E2E
    overall: number;       // % cobertura geral
  };

  // Qualidade do Código
  codeQuality: {
    maintainabilityIndex: number;  // Índice de manutenibilidade
    cyclomaticComplexity: number;  // Complexidade ciclomática
    technicalDebt: number;         // Dívida técnica (horas)
    duplicatedLines: number;       // % linhas duplicadas
    codeSmells: number;           // Número de code smells
  };

  // Métricas de Segurança
  security: {
    vulnerabilities: {
      critical: number;
      high: number;
      medium: number;
      low: number;
    };
    securityHotspots: number;
    securityRating: 'A' | 'B' | 'C' | 'D' | 'E';
  };
}
```

#### Performance Metrics

```typescript
interface PerformanceMetrics {
  // Web Performance
  webVitals: {
    lcp: number;          // Largest Contentful Paint
    fid: number;          // First Input Delay
    cls: number;          // Cumulative Layout Shift
    fcp: number;          // First Contentful Paint
    tti: number;          // Time to Interactive
  };

  // API Performance
  apiMetrics: {
    averageResponseTime: number;
    p95ResponseTime: number;
    p99ResponseTime: number;
    errorRate: number;
    throughput: number;
  };

  // Infrastructure
  infrastructure: {
    cpuUsage: number;
    memoryUsage: number;
    diskUsage: number;
    networkLatency: number;
  };
}
```

### Dashboard Implementation

#### Frontend (Vue.js 3 + TypeScript)

```vue
<template>
  <div class="compliance-dashboard">
    <!-- Header com filtros -->
    <DashboardHeader
      :projects="projects"
      :selectedProject="selectedProject"
      :timeRange="timeRange"
      @project-change="handleProjectChange"
      @time-range-change="handleTimeRangeChange"
    />

    <!-- Métricas principais -->
    <div class="metrics-grid">
      <MetricCard
        title="Code Quality Score"
        :value="metrics.qualityScore"
        :trend="metrics.qualityTrend"
        :target="85"
        color="blue"
      />

      <MetricCard
        title="Security Rating"
        :value="metrics.securityRating"
        :trend="metrics.securityTrend"
        :target="'A'"
        color="red"
      />

      <MetricCard
        title="Test Coverage"
        :value="metrics.testCoverage"
        :trend="metrics.coverageTrend"
        :target="80"
        color="green"
      />

      <MetricCard
        title="Performance Score"
        :value="metrics.performanceScore"
        :trend="metrics.performanceTrend"
        :target="90"
        color="orange"
      />
    </div>

    <!-- Gráficos detalhados -->
    <div class="charts-section">
      <QualityTrendChart :data="qualityTrendData" />
      <SecurityVulnerabilitiesChart :data="securityData" />
      <TestCoverageChart :data="coverageData" />
      <PerformanceChart :data="performanceData" />
    </div>

    <!-- Tabela de projetos -->
    <ProjectsTable
      :projects="projectsData"
      :columns="tableColumns"
      @row-click="handleProjectClick"
    />
  </div>
</template>

<script setup lang="ts">
import { ref, onMounted, computed } from 'vue';
import { useDashboardStore } from '@/stores/dashboard';
import { useWebSocket } from '@/composables/useWebSocket';

const dashboardStore = useDashboardStore();
const { connect, disconnect, isConnected } = useWebSocket();

const selectedProject = ref<string>('all');
const timeRange = ref<string>('7d');

const metrics = computed(() => dashboardStore.currentMetrics);
const projects = computed(() => dashboardStore.projects);

onMounted(async () => {
  await dashboardStore.loadInitialData();
  connect('/ws/dashboard');
});

const handleProjectChange = (projectId: string) => {
  selectedProject.value = projectId;
  dashboardStore.setSelectedProject(projectId);
};

const handleTimeRangeChange = (range: string) => {
  timeRange.value = range;
  dashboardStore.setTimeRange(range);
};
</script>
```

#### Backend API (Python FastAPI)

```python
from fastapi import FastAPI, WebSocket, Depends
from fastapi.middleware.cors import CORSMiddleware
from sqlalchemy.orm import Session
from typing import List, Optional
import asyncio
from datetime import datetime, timedelta

from .database import get_db
from .models import Project, Metric, ComplianceCheck
from .services import MetricsCollector, ComplianceTracker
from .schemas import DashboardMetrics, ProjectSummary

app = FastAPI(title="DATAMETRIA Compliance Dashboard API")

app.add_middleware(
    CORSMiddleware,
    allow_origins=["*"],
    allow_credentials=True,
    allow_methods=["*"],
    allow_headers=["*"],
)

metrics_collector = MetricsCollector()
compliance_tracker = ComplianceTracker()

@app.get("/api/v1/dashboard/metrics", response_model=DashboardMetrics)
async def get_dashboard_metrics(
    project_id: Optional[str] = None,
    time_range: str = "7d",
    db: Session = Depends(get_db)
):
    """Obtém métricas do dashboard para projeto e período específicos."""

    end_date = datetime.utcnow()
    start_date = end_date - timedelta(days=int(time_range.rstrip('d')))

    # Coletar métricas
    quality_metrics = await metrics_collector.get_quality_metrics(
        project_id, start_date, end_date, db
    )

    security_metrics = await metrics_collector.get_security_metrics(
        project_id, start_date, end_date, db
    )

    performance_metrics = await metrics_collector.get_performance_metrics(
        project_id, start_date, end_date, db
    )

    compliance_status = await compliance_tracker.get_compliance_status(
        project_id, db
    )

    return DashboardMetrics(
        quality=quality_metrics,
        security=security_metrics,
        performance=performance_metrics,
        compliance=compliance_status,
        timestamp=datetime.utcnow()
    )

@app.get("/api/v1/projects", response_model=List[ProjectSummary])
async def get_projects(db: Session = Depends(get_db)):
    """Lista todos os projetos com métricas resumidas."""

    projects = db.query(Project).all()
    project_summaries = []

    for project in projects:
        latest_metrics = await metrics_collector.get_latest_metrics(project.id, db)
        compliance_score = await compliance_tracker.calculate_compliance_score(
            project.id, db
        )

        project_summaries.append(ProjectSummary(
            id=project.id,
            name=project.name,
            quality_score=latest_metrics.quality_score,
            security_rating=latest_metrics.security_rating,
            test_coverage=latest_metrics.test_coverage,
            compliance_score=compliance_score,
            last_updated=latest_metrics.timestamp
        ))

    return project_summaries

@app.websocket("/ws/dashboard")
async def websocket_endpoint(websocket: WebSocket):
    """WebSocket para atualizações em tempo real."""

    await websocket.accept()

    try:
        while True:
            # Enviar métricas atualizadas a cada 30 segundos
            metrics = await get_dashboard_metrics()
            await websocket.send_json(metrics.dict())
            await asyncio.sleep(30)

    except Exception as e:
        print(f"WebSocket error: {e}")
    finally:
        await websocket.close()

class MetricsCollector:
    """Coletor de métricas de diferentes fontes."""

    async def get_quality_metrics(
        self,
        project_id: Optional[str],
        start_date: datetime,
        end_date: datetime,
        db: Session
    ) -> QualityMetrics:
        """Coleta métricas de qualidade do SonarQube."""

        # Integração com SonarQube
        sonar_metrics = await self._fetch_sonar_metrics(project_id)

        # Métricas de testes do CI/CD
        test_metrics = await self._fetch_test_metrics(project_id, start_date, end_date)

        return QualityMetrics(
            maintainability_index=sonar_metrics.get('maintainability_index', 0),
            test_coverage=test_metrics.get('coverage', 0),
            code_smells=sonar_metrics.get('code_smells', 0),
            technical_debt=sonar_metrics.get('technical_debt', 0),
            duplicated_lines=sonar_metrics.get('duplicated_lines_density', 0)
        )

    async def _fetch_sonar_metrics(self, project_id: str) -> dict:
        """Busca métricas do SonarQube via API."""
        # Implementação da integração com SonarQube
        pass

    async def _fetch_test_metrics(self, project_id: str, start_date: datetime, end_date: datetime) -> dict:
        """Busca métricas de teste do CI/CD."""
        # Implementação da integração com CI/CD
        pass
```

---

## 📋 Compliance Tracking

### Framework de Compliance

#### Compliance Rules Engine

```python
from enum import Enum
from typing import List, Dict, Any
from dataclasses import dataclass
from abc import ABC, abstractmethod

class ComplianceLevel(Enum):
    CRITICAL = "critical"
    HIGH = "high"
    MEDIUM = "medium"
    LOW = "low"
    INFO = "info"

@dataclass
class ComplianceRule:
    id: str
    name: str
    description: str
    level: ComplianceLevel
    category: str
    check_function: str
    threshold: Any
    enabled: bool = True

class ComplianceChecker(ABC):
    """Classe base para checkers de compliance."""

    @abstractmethod
    async def check(self, project_data: Dict[str, Any]) -> ComplianceResult:
        pass

class CodeQualityChecker(ComplianceChecker):
    """Checker para qualidade de código."""

    async def check(self, project_data: Dict[str, Any]) -> ComplianceResult:
        results = []

        # Verificar cobertura de testes
        test_coverage = project_data.get('test_coverage', 0)
        if test_coverage < 80:
            results.append(ComplianceViolation(
                rule_id="TEST_COVERAGE",
                message=f"Test coverage ({test_coverage}%) below threshold (80%)",
                level=ComplianceLevel.HIGH,
                current_value=test_coverage,
                expected_value=80
            ))

        # Verificar complexidade ciclomática
        complexity = project_data.get('cyclomatic_complexity', 0)
        if complexity > 10:
            results.append(ComplianceViolation(
                rule_id="CYCLOMATIC_COMPLEXITY",
                message=f"Cyclomatic complexity ({complexity}) above threshold (10)",
                level=ComplianceLevel.MEDIUM,
                current_value=complexity,
                expected_value=10
            ))

        # Verificar code smells
        code_smells = project_data.get('code_smells', 0)
        if code_smells > 0:
            results.append(ComplianceViolation(
                rule_id="CODE_SMELLS",
                message=f"Found {code_smells} code smells",
                level=ComplianceLevel.LOW,
                current_value=code_smells,
                expected_value=0
            ))

        return ComplianceResult(
            checker="CodeQualityChecker",
            violations=results,
            score=self._calculate_score(results),
            timestamp=datetime.utcnow()
        )

class SecurityChecker(ComplianceChecker):
    """Checker para segurança."""

    async def check(self, project_data: Dict[str, Any]) -> ComplianceResult:
        results = []

        vulnerabilities = project_data.get('vulnerabilities', {})

        # Verificar vulnerabilidades críticas
        critical_vulns = vulnerabilities.get('critical', 0)
        if critical_vulns > 0:
            results.append(ComplianceViolation(
                rule_id="CRITICAL_VULNERABILITIES",
                message=f"Found {critical_vulns} critical vulnerabilities",
                level=ComplianceLevel.CRITICAL,
                current_value=critical_vulns,
                expected_value=0
            ))

        # Verificar vulnerabilidades altas
        high_vulns = vulnerabilities.get('high', 0)
        if high_vulns > 0:
            results.append(ComplianceViolation(
                rule_id="HIGH_VULNERABILITIES",
                message=f"Found {high_vulns} high vulnerabilities",
                level=ComplianceLevel.HIGH,
                current_value=high_vulns,
                expected_value=0
            ))

        return ComplianceResult(
            checker="SecurityChecker",
            violations=results,
            score=self._calculate_score(results),
            timestamp=datetime.utcnow()
        )

class ComplianceEngine:
    """Engine principal de compliance."""

    def __init__(self):
        self.checkers = [
            CodeQualityChecker(),
            SecurityChecker(),
            PerformanceChecker(),
            AccessibilityChecker(),
            DocumentationChecker()
        ]

    async def run_compliance_check(self, project_id: str) -> ComplianceReport:
        """Executa verificação completa de compliance."""

        project_data = await self._collect_project_data(project_id)
        results = []

        for checker in self.checkers:
            try:
                result = await checker.check(project_data)
                results.append(result)
            except Exception as e:
                logger.error(f"Error running {checker.__class__.__name__}: {e}")

        overall_score = self._calculate_overall_score(results)
        compliance_level = self._determine_compliance_level(overall_score)

        return ComplianceReport(
            project_id=project_id,
            results=results,
            overall_score=overall_score,
            compliance_level=compliance_level,
            timestamp=datetime.utcnow()
        )
```

### Compliance Dashboard UI

#### Compliance Overview Component

```vue
<template>
  <div class="compliance-overview">
    <div class="compliance-header">
      <h2>Compliance Status</h2>
      <div class="overall-score" :class="scoreClass">
        <span class="score-value">{{ overallScore }}</span>
        <span class="score-label">Overall Score</span>
      </div>
    </div>

    <div class="compliance-categories">
      <ComplianceCategory
        v-for="category in categories"
        :key="category.id"
        :category="category"
        @view-details="handleViewDetails"
      />
    </div>

    <div class="violations-summary">
      <h3>Recent Violations</h3>
      <ViolationsList
        :violations="recentViolations"
        :limit="10"
        @violation-click="handleViolationClick"
      />
    </div>

    <div class="compliance-trends">
      <ComplianceTrendChart
        :data="trendData"
        :timeRange="timeRange"
      />
    </div>
  </div>
</template>

<script setup lang="ts">
import { computed, ref } from 'vue';
import { useComplianceStore } from '@/stores/compliance';

const complianceStore = useComplianceStore();

const overallScore = computed(() => complianceStore.overallScore);
const categories = computed(() => complianceStore.categories);
const recentViolations = computed(() => complianceStore.recentViolations);
const trendData = computed(() => complianceStore.trendData);

const scoreClass = computed(() => {
  const score = overallScore.value;
  if (score >= 90) return 'score-excellent';
  if (score >= 80) return 'score-good';
  if (score >= 70) return 'score-fair';
  return 'score-poor';
});

const handleViewDetails = (categoryId: string) => {
  // Navegar para detalhes da categoria
};

const handleViolationClick = (violation: ComplianceViolation) => {
  // Mostrar detalhes da violação
};
</script>

<style scoped>
.compliance-overview {
  padding: 2rem;
}

.compliance-header {
  display: flex;
  justify-content: space-between;
  align-items: center;
  margin-bottom: 2rem;
}

.overall-score {
  display: flex;
  flex-direction: column;
  align-items: center;
  padding: 1rem;
  border-radius: 8px;
  min-width: 120px;
}

.score-value {
  font-size: 2.5rem;
  font-weight: bold;
}

.score-label {
  font-size: 0.875rem;
  opacity: 0.8;
}

.score-excellent {
  background: linear-gradient(135deg, #10b981, #059669);
  color: white;
}

.score-good {
  background: linear-gradient(135deg, #3b82f6, #1d4ed8);
  color: white;
}

.score-fair {
  background: linear-gradient(135deg, #f59e0b, #d97706);
  color: white;
}

.score-poor {
  background: linear-gradient(135deg, #ef4444, #dc2626);
  color: white;
}

.compliance-categories {
  display: grid;
  grid-template-columns: repeat(auto-fit, minmax(300px, 1fr));
  gap: 1.5rem;
  margin-bottom: 2rem;
}
</style>
```

---

## 📈 Quality Reports

### Automated Report Generation

#### Report Generator Service

```python
from jinja2 import Template
from weasyprint import HTML, CSS
from datetime import datetime, timedelta
import asyncio
from typing import List, Dict, Any

class ReportGenerator:
    """Gerador de relatórios de qualidade."""

    def __init__(self):
        self.templates = {
            'executive_summary': self._load_template('executive_summary.html'),
            'technical_details': self._load_template('technical_details.html'),
            'compliance_report': self._load_template('compliance_report.html')
        }

    async def generate_executive_report(
        self,
        project_id: str,
        time_range: str = "30d"
    ) -> bytes:
        """Gera relatório executivo em PDF."""

        # Coletar dados
        data = await self._collect_report_data(project_id, time_range)

        # Renderizar HTML
        html_content = self.templates['executive_summary'].render(
            project_name=data['project_name'],
            period=data['period'],
            metrics=data['metrics'],
            trends=data['trends'],
            recommendations=data['recommendations'],
            generated_at=datetime.utcnow()
        )

        # Converter para PDF
        pdf_bytes = HTML(string=html_content).write_pdf(
            stylesheets=[CSS(filename='styles/report.css')]
        )

        return pdf_bytes

    async def generate_compliance_report(
        self,
        project_id: str
    ) -> Dict[str, Any]:
        """Gera relatório de compliance detalhado."""

        compliance_data = await self._collect_compliance_data(project_id)

        return {
            'project_id': project_id,
            'compliance_score': compliance_data['overall_score'],
            'categories': compliance_data['categories'],
            'violations': compliance_data['violations'],
            'recommendations': compliance_data['recommendations'],
            'action_items': compliance_data['action_items'],
            'generated_at': datetime.utcnow().isoformat()
        }

    async def _collect_report_data(self, project_id: str, time_range: str) -> Dict[str, Any]:
        """Coleta dados para relatório."""

        end_date = datetime.utcnow()
        start_date = end_date - timedelta(days=int(time_range.rstrip('d')))

        # Métricas atuais
        current_metrics = await metrics_collector.get_latest_metrics(project_id)

        # Tendências históricas
        historical_data = await metrics_collector.get_historical_metrics(
            project_id, start_date, end_date
        )

        # Análise de tendências
        trends = self._analyze_trends(historical_data)

        # Recomendações baseadas em IA
        recommendations = await self._generate_recommendations(
            current_metrics, trends
        )

        return {
            'project_name': await self._get_project_name(project_id),
            'period': f"{start_date.strftime('%Y-%m-%d')} to {end_date.strftime('%Y-%m-%d')}",
            'metrics': current_metrics,
            'trends': trends,
            'recommendations': recommendations
        }
```

#### Report Templates (HTML/Jinja2)

```html
<!-- executive_summary.html -->
<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8">
    <title>Executive Summary - {{ project_name }}</title>
    <style>
        body { font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif; }
        .header { background: #1e40af; color: white; padding: 2rem; }
        .metrics-grid { display: grid; grid-template-columns: repeat(4, 1fr); gap: 1rem; }
        .metric-card { background: #f8fafc; padding: 1rem; border-radius: 8px; }
        .trend-up { color: #10b981; }
        .trend-down { color: #ef4444; }
        .recommendations { background: #fef3c7; padding: 1rem; border-radius: 8px; }
    </style>
</head>
<body>
    <div class="header">
        <h1>{{ project_name }} - Quality Report</h1>
        <p>Period: {{ period }}</p>
        <p>Generated: {{ generated_at.strftime('%Y-%m-%d %H:%M:%S') }}</p>
    </div>

    <div class="content">
        <h2>Executive Summary</h2>

        <div class="metrics-grid">
            <div class="metric-card">
                <h3>Quality Score</h3>
                <div class="metric-value">{{ metrics.quality_score }}%</div>
                <div class="trend {{ 'trend-up' if trends.quality_trend > 0 else 'trend-down' }}">
                    {{ '+' if trends.quality_trend > 0 else '' }}{{ trends.quality_trend }}%
                </div>
            </div>

            <div class="metric-card">
                <h3>Security Rating</h3>
                <div class="metric-value">{{ metrics.security_rating }}</div>
                <div class="trend">{{ trends.security_trend }}</div>
            </div>

            <div class="metric-card">
                <h3>Test Coverage</h3>
                <div class="metric-value">{{ metrics.test_coverage }}%</div>
                <div class="trend {{ 'trend-up' if trends.coverage_trend > 0 else 'trend-down' }}">
                    {{ '+' if trends.coverage_trend > 0 else '' }}{{ trends.coverage_trend }}%
                </div>
            </div>

            <div class="metric-card">
                <h3>Performance Score</h3>
                <div class="metric-value">{{ metrics.performance_score }}%</div>
                <div class="trend {{ 'trend-up' if trends.performance_trend > 0 else 'trend-down' }}">
                    {{ '+' if trends.performance_trend > 0 else '' }}{{ trends.performance_trend }}%
                </div>
            </div>
        </div>

        <h2>Key Recommendations</h2>
        <div class="recommendations">
            {% for recommendation in recommendations %}
            <div class="recommendation">
                <h4>{{ recommendation.title }}</h4>
                <p>{{ recommendation.description }}</p>
                <p><strong>Priority:</strong> {{ recommendation.priority }}</p>
                <p><strong>Estimated Impact:</strong> {{ recommendation.impact }}</p>
            </div>
            {% endfor %}
        </div>
    </div>
</body>
</html>
```

---

## 👥 Team Analytics

### Team Performance Metrics

#### Developer Productivity Analytics

```python
from dataclasses import dataclass
from typing import List, Dict
from datetime import datetime, timedelta

@dataclass
class DeveloperMetrics:
    developer_id: str
    name: str
    commits: int
    lines_added: int
    lines_removed: int
    pull_requests: int
    code_reviews: int
    bugs_fixed: int
    features_delivered: int
    quality_score: float
    collaboration_score: float

class TeamAnalytics:
    """Analytics de produtividade da equipe."""

    async def get_team_metrics(
        self,
        team_id: str,
        time_range: str = "30d"
    ) -> Dict[str, Any]:
        """Obtém métricas da equipe."""

        end_date = datetime.utcnow()
        start_date = end_date - timedelta(days=int(time_range.rstrip('d')))

        # Métricas individuais
        developers = await self._get_team_developers(team_id)
        developer_metrics = []

        for dev in developers:
            metrics = await self._calculate_developer_metrics(
                dev.id, start_date, end_date
            )
            developer_metrics.append(metrics)

        # Métricas da equipe
        team_metrics = self._calculate_team_metrics(developer_metrics)

        # Análise de colaboração
        collaboration_metrics = await self._analyze_collaboration(
            team_id, start_date, end_date
        )

        return {
            'team_id': team_id,
            'period': f"{start_date.date()} to {end_date.date()}",
            'team_metrics': team_metrics,
            'developer_metrics': developer_metrics,
            'collaboration': collaboration_metrics,
            'insights': await self._generate_team_insights(developer_metrics)
        }

    async def _calculate_developer_metrics(
        self,
        developer_id: str,
        start_date: datetime,
        end_date: datetime
    ) -> DeveloperMetrics:
        """Calcula métricas individuais do desenvolvedor."""

        # Dados do Git
        git_stats = await self._get_git_stats(developer_id, start_date, end_date)

        # Dados de Pull Requests
        pr_stats = await self._get_pr_stats(developer_id, start_date, end_date)

        # Dados de Code Review
        review_stats = await self._get_review_stats(developer_id, start_date, end_date)

        # Dados de Issues/Bugs
        issue_stats = await self._get_issue_stats(developer_id, start_date, end_date)

        # Calcular scores
        quality_score = self._calculate_quality_score(git_stats, pr_stats, review_stats)
        collaboration_score = self._calculate_collaboration_score(review_stats, pr_stats)

        return DeveloperMetrics(
            developer_id=developer_id,
            name=await self._get_developer_name(developer_id),
            commits=git_stats['commits'],
            lines_added=git_stats['lines_added'],
            lines_removed=git_stats['lines_removed'],
            pull_requests=pr_stats['created'],
            code_reviews=review_stats['reviews_given'],
            bugs_fixed=issue_stats['bugs_fixed'],
            features_delivered=issue_stats['features_completed'],
            quality_score=quality_score,
            collaboration_score=collaboration_score
        )
```

#### Team Dashboard Component

```vue
<template>
  <div class="team-analytics">
    <div class="team-header">
      <h2>Team Analytics</h2>
      <div class="team-filters">
        <select v-model="selectedTeam" @change="handleTeamChange">
          <option value="">All Teams</option>
          <option v-for="team in teams" :key="team.id" :value="team.id">
            {{ team.name }}
          </option>
        </select>

        <select v-model="timeRange" @change="handleTimeRangeChange">
          <option value="7d">Last 7 days</option>
          <option value="30d">Last 30 days</option>
          <option value="90d">Last 90 days</option>
        </select>
      </div>
    </div>

    <!-- Team Overview -->
    <div class="team-overview">
      <MetricCard
        title="Team Velocity"
        :value="teamMetrics.velocity"
        unit="story points"
        :trend="teamMetrics.velocityTrend"
      />

      <MetricCard
        title="Code Quality"
        :value="teamMetrics.qualityScore"
        unit="%"
        :trend="teamMetrics.qualityTrend"
      />

      <MetricCard
        title="Collaboration Score"
        :value="teamMetrics.collaborationScore"
        unit="%"
        :trend="teamMetrics.collaborationTrend"
      />

      <MetricCard
        title="Delivery Rate"
        :value="teamMetrics.deliveryRate"
        unit="%"
        :trend="teamMetrics.deliveryTrend"
      />
    </div>

    <!-- Developer Performance -->
    <div class="developer-performance">
      <h3>Developer Performance</h3>
      <DeveloperTable
        :developers="developerMetrics"
        :sortBy="sortBy"
        :sortOrder="sortOrder"
        @sort="handleSort"
      />
    </div>

    <!-- Collaboration Network -->
    <div class="collaboration-section">
      <h3>Collaboration Network</h3>
      <CollaborationGraph
        :nodes="collaborationData.nodes"
        :edges="collaborationData.edges"
      />
    </div>

    <!-- Insights -->
    <div class="insights-section">
      <h3>Team Insights</h3>
      <InsightsList :insights="teamInsights" />
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref, computed, onMounted } from 'vue';
import { useTeamAnalyticsStore } from '@/stores/teamAnalytics';

const teamAnalyticsStore = useTeamAnalyticsStore();

const selectedTeam = ref<string>('');
const timeRange = ref<string>('30d');
const sortBy = ref<string>('quality_score');
const sortOrder = ref<'asc' | 'desc'>('desc');

const teams = computed(() => teamAnalyticsStore.teams);
const teamMetrics = computed(() => teamAnalyticsStore.teamMetrics);
const developerMetrics = computed(() => teamAnalyticsStore.developerMetrics);
const collaborationData = computed(() => teamAnalyticsStore.collaborationData);
const teamInsights = computed(() => teamAnalyticsStore.insights);

onMounted(async () => {
  await teamAnalyticsStore.loadTeams();
  await loadTeamData();
});

const loadTeamData = async () => {
  await teamAnalyticsStore.loadTeamMetrics(selectedTeam.value, timeRange.value);
};

const handleTeamChange = () => {
  loadTeamData();
};

const handleTimeRangeChange = () => {
  loadTeamData();
};

const handleSort = (column: string) => {
  if (sortBy.value === column) {
    sortOrder.value = sortOrder.value === 'asc' ? 'desc' : 'asc';
  } else {
    sortBy.value = column;
    sortOrder.value = 'desc';
  }
};
</script>
```

---

## 🔌 Integration APIs

### REST API Endpoints

#### Core API Routes

```python
from fastapi import APIRouter, Depends, HTTPException, Query
from typing import List, Optional
import asyncio

router = APIRouter(prefix="/api/v1", tags=["dashboard"])

@router.get("/projects/{project_id}/metrics")
async def get_project_metrics(
    project_id: str,
    time_range: str = Query("7d", regex="^(1d|7d|30d|90d)$"),
    metrics: Optional[List[str]] = Query(None)
):
    """Obtém métricas específicas de um projeto."""

    try:
        if metrics:
            # Buscar métricas específicas
            result = await metrics_collector.get_specific_metrics(
                project_id, time_range, metrics
            )
        else:
            # Buscar todas as métricas
            result = await metrics_collector.get_all_metrics(
                project_id, time_range
            )

        return {
            "project_id": project_id,
            "time_range": time_range,
            "metrics": result,
            "timestamp": datetime.utcnow()
        }

    except Exception as e:
        raise HTTPException(status_code=500, detail=str(e))

@router.get("/compliance/{project_id}")
async def get_compliance_status(project_id: str):
    """Obtém status de compliance de um projeto."""

    try:
        compliance_report = await compliance_engine.run_compliance_check(project_id)

        return {
            "project_id": project_id,
            "compliance_score": compliance_report.overall_score,
            "compliance_level": compliance_report.compliance_level,
            "violations": [v.dict() for v in compliance_report.get_violations()],
            "recommendations": compliance_report.get_recommendations(),
            "last_check": compliance_report.timestamp
        }

    except Exception as e:
        raise HTTPException(status_code=500, detail=str(e))

@router.post("/reports/generate")
async def generate_report(
    request: ReportRequest,
    background_tasks: BackgroundTasks
):
    """Gera relatório em background."""

    task_id = str(uuid.uuid4())

    background_tasks.add_task(
        generate_report_task,
        task_id,
        request.project_id,
        request.report_type,
        request.format,
        request.email
    )

    return {
        "task_id": task_id,
        "status": "queued",
        "message": "Report generation started"
    }

@router.get("/reports/{task_id}/status")
async def get_report_status(task_id: str):
    """Verifica status de geração de relatório."""

    status = await report_queue.get_task_status(task_id)

    return {
        "task_id": task_id,
        "status": status.status,
        "progress": status.progress,
        "download_url": status.download_url if status.status == "completed" else None
    }

@router.get("/teams/{team_id}/analytics")
async def get_team_analytics(
    team_id: str,
    time_range: str = Query("30d", regex="^(7d|30d|90d)$")
):
    """Obtém analytics de uma equipe."""

    try:
        analytics = await team_analytics.get_team_metrics(team_id, time_range)

        return {
            "team_id": team_id,
            "time_range": time_range,
            "analytics": analytics,
            "timestamp": datetime.utcnow()
        }

    except Exception as e:
        raise HTTPException(status_code=500, detail=str(e))
```

### GraphQL API

#### GraphQL Schema

```python
import strawberry
from typing import List, Optional
from datetime import datetime

@strawberry.type
class Metric:
    name: str
    value: float
    unit: str
    timestamp: datetime
    trend: Optional[float] = None

@strawberry.type
class Project:
    id: str
    name: str
    description: str
    quality_score: float
    security_rating: str
    test_coverage: float
    last_updated: datetime

@strawberry.type
class ComplianceViolation:
    rule_id: str
    message: str
    level: str
    file_path: Optional[str] = None
    line_number: Optional[int] = None

@strawberry.type
class Query:
    @strawberry.field
    async def projects(self) -> List[Project]:
        """Lista todos os projetos."""
        return await project_service.get_all_projects()

    @strawberry.field
    async def project(self, id: str) -> Optional[Project]:
        """Obtém projeto por ID."""
        return await project_service.get_project(id)

    @strawberry.field
    async def project_metrics(
        self,
        project_id: str,
        time_range: str = "7d"
    ) -> List[Metric]:
        """Obtém métricas de um projeto."""
        return await metrics_collector.get_metrics(project_id, time_range)

    @strawberry.field
    async def compliance_violations(
        self,
        project_id: str,
        level: Optional[str] = None
    ) -> List[ComplianceViolation]:
        """Obtém violações de compliance."""
        return await compliance_tracker.get_violations(project_id, level)

schema = strawberry.Schema(query=Query)
```

### Webhook Integration

#### Webhook Handler

```python
from fastapi import Request
import hmac
import hashlib

@router.post("/webhooks/github")
async def github_webhook(request: Request):
    """Handler para webhooks do GitHub."""

    # Verificar assinatura
    signature = request.headers.get("X-Hub-Signature-256")
    if not verify_github_signature(await request.body(), signature):
        raise HTTPException(status_code=401, detail="Invalid signature")

    payload = await request.json()
    event_type = request.headers.get("X-GitHub-Event")

    # Processar evento
    if event_type == "push":
        await handle_push_event(payload)
    elif event_type == "pull_request":
        await handle_pr_event(payload)
    elif event_type == "workflow_run":
        await handle_workflow_event(payload)

    return {"status": "processed"}

async def handle_push_event(payload: dict):
    """Processa evento de push."""

    repository = payload["repository"]["full_name"]
    commits = payload["commits"]

    # Atualizar métricas de commits
    await metrics_collector.update_commit_metrics(repository, commits)

    # Trigger compliance check
    await compliance_engine.schedule_check(repository)

async def handle_pr_event(payload: dict):
    """Processa evento de pull request."""

    if payload["action"] == "opened":
        # Nova PR aberta
        await metrics_collector.increment_pr_count(payload["repository"]["full_name"])
    elif payload["action"] == "closed" and payload["pull_request"]["merged"]:
        # PR mergeada
        await metrics_collector.update_merge_metrics(payload)

def verify_github_signature(payload: bytes, signature: str) -> bool:
    """Verifica assinatura do GitHub."""

    secret = os.getenv("GITHUB_WEBHOOK_SECRET").encode()
    expected_signature = "sha256=" + hmac.new(
        secret, payload, hashlib.sha256
    ).hexdigest()

    return hmac.compare_digest(expected_signature, signature)
```

---

## 🚀 Deployment e Configuração

### Docker Configuration

#### Dockerfile

```dockerfile
# Multi-stage build para dashboard
FROM node:18-alpine AS frontend-builder

WORKDIR /app/frontend
COPY frontend/package*.json ./
RUN npm ci --only=production

COPY frontend/ ./
RUN npm run build

FROM python:3.11-slim AS backend

WORKDIR /app

# Instalar dependências do sistema
RUN apt-get update && apt-get install -y \
    gcc \
    libpq-dev \
    && rm -rf /var/lib/apt/lists/*

# Instalar dependências Python
COPY requirements.txt .
RUN pip install --no-cache-dir -r requirements.txt

# Copiar código backend
COPY backend/ ./

# Copiar frontend build
COPY --from=frontend-builder /app/frontend/dist ./static

# Criar usuário não-root
RUN useradd --create-home --shell /bin/bash dashboard \
    && chown -R dashboard:dashboard /app
USER dashboard

EXPOSE 8000

HEALTHCHECK --interval=30s --timeout=3s --start-period=5s --retries=3 \
    CMD curl -f http://localhost:8000/health || exit 1

CMD ["uvicorn", "main:app", "--host", "0.0.0.0", "--port", "8000"]
```

## Docker Compose

```yaml
version: '3.8'

services:
  dashboard:
    build: .
    ports:
      - "8000:8000"
    environment:
      - DATABASE_URL=postgresql://user:pass@postgres:5432/dashboard
      - REDIS_URL=redis://redis:6379
      - GITHUB_TOKEN=${GITHUB_TOKEN}
      - SONARQUBE_URL=${SONARQUBE_URL}
      - SONARQUBE_TOKEN=${SONARQUBE_TOKEN}
    depends_on:
      - postgres
      - redis
    volumes:
      - ./config:/app/config
    restart: unless-stopped

  postgres:
    image: postgres:13
    environment:
      - POSTGRES_DB=dashboard
      - POSTGRES_USER=user
      - POSTGRES_PASSWORD=pass
    volumes:
      - postgres_data:/var/lib/postgresql/data
    restart: unless-stopped

  redis:
    image: redis:7-alpine
    restart: unless-stopped

  nginx:
    image: nginx:alpine
    ports:
      - "80:80"
      - "443:443"
    volumes:
      - ./nginx.conf:/etc/nginx/nginx.conf
      - ./ssl:/etc/nginx/ssl
    depends_on:
      - dashboard
    restart: unless-stopped

volumes:
  postgres_data:
```

### Kubernetes Deployment

#### Kubernetes Manifests

```yaml
# dashboard-deployment.yaml
apiVersion: apps/v1
kind: Deployment
metadata:
  name: compliance-dashboard
  labels:
    app: compliance-dashboard
spec:
  replicas: 3
  selector:
    matchLabels:
      app: compliance-dashboard
  template:
    metadata:
      labels:
        app: compliance-dashboard
    spec:
      containers:
      - name: dashboard
        image: datametria/compliance-dashboard:latest
        ports:
        - containerPort: 8000
        env:
        - name: DATABASE_URL
          valueFrom:
            secretKeyRef:
              name: dashboard-secrets
              key: database-url
        - name: REDIS_URL
          valueFrom:
            configMapKeyRef:
              name: dashboard-config
              key: redis-url
        resources:
          requests:
            memory: "512Mi"
            cpu: "250m"
          limits:
            memory: "1Gi"
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
  name: compliance-dashboard-service
spec:
  selector:
    app: compliance-dashboard
  ports:
  - protocol: TCP
    port: 80
    targetPort: 8000
  type: ClusterIP
---
apiVersion: networking.k8s.io/v1
kind: Ingress
metadata:
  name: compliance-dashboard-ingress
  annotations:
    kubernetes.io/ingress.class: nginx
    cert-manager.io/cluster-issuer: letsencrypt-prod
spec:
  tls:
  - hosts:
    - dashboard.datametria.io
    secretName: dashboard-tls
  rules:
  - host: dashboard.datametria.io
    http:
      paths:
      - path: /
        pathType: Prefix
        backend:
          service:
            name: compliance-dashboard-service
            port:
              number: 80
```

---

## ✅ Checklist de Implementação

### Fase 1: Core Infrastructure (8 pontos)

- [ ] **Backend API**: FastAPI com endpoints principais
- [ ] **Database**: PostgreSQL com schema inicial
- [ ] **Cache**: Redis para cache de métricas
- [ ] **Authentication**: Sistema de autenticação JWT
- [ ] **Docker**: Containerização completa
- [ ] **CI/CD**: Pipeline de deploy automatizado

### Fase 2: Real-time Metrics (8 pontos)

- [ ] **Data Collectors**: Integrações com Git, CI/CD, SonarQube
- [ ] **Metrics Processing**: Pipeline de processamento de métricas
- [ ] **WebSocket**: Atualizações em tempo real
- [ ] **Time Series DB**: Armazenamento de métricas históricas
- [ ] **Alerting**: Sistema de alertas configurado

### Fase 3: Compliance Tracking (8 pontos)

- [ ] **Compliance Engine**: Engine de verificação de compliance
- [ ] **Rules Management**: Gerenciamento de regras de compliance
- [ ] **Violation Tracking**: Rastreamento de violações
- [ ] **Automated Checks**: Verificações automatizadas
- [ ] **Compliance Reports**: Relatórios de compliance

### Fase 4: Quality Reports (5 pontos)

- [ ] **Report Generator**: Gerador de relatórios PDF
- [ ] **Templates**: Templates de relatórios HTML
- [ ] **Scheduled Reports**: Relatórios agendados
- [ ] **Email Integration**: Envio de relatórios por email

### Fase 5: Team Analytics (5 pontos)

- [ ] **Developer Metrics**: Métricas individuais de desenvolvedores
- [ ] **Team Performance**: Métricas de performance da equipe
- [ ] **Collaboration Analysis**: Análise de colaboração
- [ ] **Productivity Insights**: Insights de produtividade

---

<div align="center">

**Desenvolvido por**: Equipe DATAMETRIA DevOps
**Última Atualização**: [DD/MM/AAAA]
**Versão**: 1.0.0

---

## Dashboard de Conformidade Enterprise! Métricas em tempo real! 📊

</div>
