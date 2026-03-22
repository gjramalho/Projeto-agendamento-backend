# Scheduler - Sistema de Agendamentos (.NET 8 + SQL Server)

> Aplicação console para gestão de agendamentos com persistência em SQL Server e arquitetura em camadas.

**Criador:** Gabriel Ramalho Barbosa

**Objetivo:** Projeto para portfólio.

## Índice
- [Visão geral](#visão-geral)
- [Funcionalidades](#funcionalidades)
- [Tecnologias](#tecnologias)
- [Estrutura do projeto](#estrutura-do-projeto)
- [Pré-requisitos](#pré-requisitos)
- [Como executar](#como-executar)
- [Configuração do banco](#configuração-do-banco)

## Visão geral
Sistema desenvolvido para simular o fluxo real de uma clínica ou escritório, desde a criação do agendamento até a finalização ou cancelamento com motivo. O foco está em integridade dos dados e controle de estados.

## Funcionalidades
- Cadastro, listagem e busca de agendamentos por ID.
- Cancelamento com motivo e finalização de atendimento.
- Prevenção de conflitos de horário durante o cadastro.
- Atualização de dados refletindo as alterações no banco em tempo real.

## Tecnologias
- **Linguagem:** C# (.NET 8)
- **ORM:** Entity Framework Core (Code First)
- **Banco de Dados:** SQL Server em Docker
- **Arquitetura:** Camadas (`Services`, `Repositories`, `Models`)

## Estrutura do projeto
```
ProjetoAgendamento/
├─ Models/
├─ Repositories/
├─ Services/
├─ Program.cs
└─ README.md
```

## Pré-requisitos
- .NET SDK 8
- Docker e Docker Compose

## Como executar
1. Clone o repositório.
2. Inicie o SQL Server via Docker:
   ```bash
   docker-compose up -d
   ```
3. Execute a aplicação:
   ```bash
   dotnet run
   ```
4. O banco e as tabelas são criados automaticamente via `Database.EnsureCreated()` ao iniciar a aplicação.

## Configuração do banco
A string de conexão está definida em `Repositories/AppDbContext.cs`. Caso necessário, ajuste usuário, senha, porta ou nome do banco para o seu ambiente.
