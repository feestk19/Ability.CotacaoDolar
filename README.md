# 💵 Teste Técnico Ability: Cotação do Dólar

Aplicação desenvolvida em .NET 8 para coleta, armazenamento e consulta da cotação do dólar, utilizando Clean Architecture, processamento assíncrono com Worker Service e observabilidade com Serilog + Seq.

---

## 🚀 Tecnologias utilizadas

![.NET](https://img.shields.io/badge/.NET-8.0-purple)     ![ASP.NET](https://img.shields.io/badge/ASP.NET-Core-blue)    ![Docker](https://img.shields.io/badge/Docker-Containerized-blue?logo=docker)     ![SQLite](https://img.shields.io/badge/Database-SQLite-lightgrey)     ![Serilog](https://img.shields.io/badge/Logging-Serilog-orange)    ![Seq](https://img.shields.io/badge/Observability-Seq-black)      ![Architecture](https://img.shields.io/badge/Architecture-Clean%20Architecture-green)

---

## 🧱 Arquitetura

O projeto foi estruturado seguindo os princípios de Clean Architecture:

Ability.CotacaoDolar
├── API (camada de entrada)
├── Core (regras de negócio)
├── Infrastructure (acesso a dados, integrações)
└── Worker (processamento em background)

---

## ⚙️ Funcionalidades

- Coleta automática da cotação do dólar via API externa
- Armazenamento em banco SQLite
- Consulta da última cotação
- Consulta de histórico por período
- Prevenção de duplicidade de dados
- Logging estruturado com Serilog
- Visualização de logs com Seq
- Configuração dinâmica do intervalo do Worker

---

## 🔄 Funcionamento do Worker

O Worker executa continuamente:

1. Consulta a cotação do dólar
2. Persiste no banco de dados
3. Aguarda o intervalo configurado
4. Repete o processo

---

## ⏱ Configuração do intervalo

O intervalo de execução do Worker pode ser alterado via variável de ambiente:

```yml
Worker__IntervaloMinutos: 5
```

Por padrão: 60 minutos

---

## 🐳 Como rodar com Docker

### 1. Clonar o repositório

```bash
git clone https://github.com/feestk19/Ability.CotacaoDolar.git
cd seu-repo
```

### 2. Subir os containers

```bash
docker compose up --build
```

---

## 🌐 Acessos

| Serviço | URL |
|--------|-----|
| API (Swagger) | http://localhost:8080 |
| API (Scalar) | http://localhost:8080/scalar |
| Logs (Seq) | http://localhost:5341 |

---

## 📊 Observabilidade

A aplicação utiliza Serilog com envio de logs para o Seq, permitindo:

- Visualização em tempo real
- Filtros avançados
- Análise de erros
- Rastreamento de execução

---

# 🗄 Banco de Dados

- SQLite
- Compartilhado entre API e Worker via volume Docker
- Migrations aplicadas automaticamente na inicialização

## 🔍 Como consultar o banco de dados

O banco de dados SQLite é compartilhado entre os containers da API e do Worker através de volume Docker.

### 📥 Opção 1: Copiar o banco para a máquina local

Você pode copiar o arquivo do banco para sua máquina e abrir em ferramentas como DB Browser for SQLite:

```bash
docker cp ability-cotacao-api:/data/cotacaodolar.db ./cotacaodolar.db
```
### 📊 Opção 2: Consultar diretamente via SQLite CLI

Acesse o container da API:

```bash
docker exec -it ability-cotacao-api sh
```

Dentro do container:

```bash
sqlite3 /data/cotacaodolar.db
```

Executar consultas:

```SQL
SELECT * FROM CotacoesDolar;
```

### 📁 Observação

O banco de dados está localizado em:

```bash
/data/cotacaodolar.db
```

E é compartilhado entre API e Worker via Docker Compose.

---

## 🔧 Configurações importantes

### Variáveis de ambiente

```yml
Worker__IntervaloMinutos=5  
ASPNETCORE_ENVIRONMENT=Docker
```
---

## 🧠 Decisões técnicas

- Uso de Worker Service para processamento assíncrono
- Logging estruturado com Serilog
- Observabilidade com Seq
- SQLite para simplicidade e portabilidade
- Docker Compose para orquestração
- Configuração via variáveis de ambiente

---

## 👨‍💻 Autor

Desenvolvido por Fellipe Strombeck 🚀
