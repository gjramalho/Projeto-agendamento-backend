# Projeto Agendamento

Sistema web para gerenciamento de agendamentos, desenvolvido com ASP.NET Core Razor Pages e Entity Framework Core. Permite cadastrar, visualizar, atualizar status e excluir agendamentos de forma simples e intuitiva.

## Funcionalidades

- Cadastro de novos agendamentos com validação de dados
- Listagem de agendamentos em painel moderno
- Atualização de status (Pendente, Atendido, Cancelado)
- Exclusão de agendamentos
- Prevenção de conflitos de horário (intervalo mínimo de 40 minutos)
- Feedback visual com SweetAlert2

## Tecnologias Utilizadas

- .NET 8
- ASP.NET Core Razor Pages
- Entity Framework Core
- SQL Server (ou outro banco relacional)
- SweetAlert2 (notificações)
- HTML5, CSS3 (Tailwind/Custom), JavaScript

## Estrutura do Projeto

- `Models/Agendamento.cs`: Modelo de dados do agendamento
- `Repositories/AppDbContext.cs`: Contexto do banco de dados
- `Controllers/AgendamentosController.cs`: Lógica de CRUD e regras de negócio
- `Views/Home/Index.cshtml`: Interface principal do painel de agendamentos
- `Services/AgendamentoService.cs`: (Opcional) Lógica adicional de negócio

## Como Executar

1. Clone o repositório:
   ```bash
   git clone https://github.com/gjramalho/Projeto-agendamento-backend.git
   ```
2. Configure a string de conexão no `appsettings.json`.
3. Execute as migrações do banco de dados:
   ```bash
   dotnet ef database update
   ```
4. Inicie o projeto:
   ```bash
   dotnet run
   ```
5. Acesse `http://localhost:5000` no navegador.

## Telas

- Painel de agendamentos com filtro visual por status
- Modal para novo agendamento
- Ações rápidas para atualizar status e excluir

## Validações e Regras

- Não permite agendar para datas passadas
- Impede agendamentos com intervalo menor que 40 minutos
- Validação de campos obrigatórios e tamanho mínimo

## Licença

Este projeto está sob a licença MIT.

---
Desenvolvido por Gabriel Ramalho
