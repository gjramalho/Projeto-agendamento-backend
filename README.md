# 🗓️ Scheduler API - C# & SQL Server (Docker)

## 🚀 Sobre o Projeto
Sistema de backend desenvolvido para gestão de agendamentos, com foco em integridade de dados e persistência em ambiente containerizado. O projeto simula o fluxo real de uma clínica ou escritório, gerenciando desde a criação até a finalização ou cancelamento de atendimentos.

## 🛠️ Tecnologias Utilizadas
- **Linguagem:** C# (.NET 8)
- **ORM:** Entity Framework Core (Code First)
- **Banco de Dados:** SQL Server rodando em Docker
- **Arquitetura:** Camadas (Services, Repositories, Models)

## 💡 Desafios Técnicos Superados
- **Prevenção de Conflitos:** Implementação de lógica para impedir agendamentos duplicados no mesmo intervalo de tempo.
- **Gerenciamento de Contexto (Real-time Updates):** Otimização do ciclo de vida do `DbContext` para garantir que o console reflita as alterações do banco de dados em tempo real, evitando cache de memória indesejado.
- **Status Workflow:** Controle de estados do agendamento (Pendente, Atendido, Cancelado com Motivo).

## ⚙️ Como Executar
1. Certifique-se de ter o Docker e o Docker Compose instalados na sua máquina.
2. Clone o repositório.
3. Suba a infraestrutura (SQL Server) executando o comando na raiz do projeto:
   ```bash
   docker-compose up -d
   ```
4. Em seguida, execute a aplicação via Visual Studio ou utilizando o .NET CLI:
   ```bash
   dotnet run
   ```
5. O banco de dados e as tabelas serão criados automaticamente via `Database.EnsureCreated()` ao iniciar a aplicação.

## 📋 Funcionalidades (Menu Interativo)
Ao rodar a aplicação, um menu no Console será exibido com as seguintes opções:
- **1 - Adicionar Novo Agendamento:** Registra o nome, serviço e data desejada.
- **2 - Listar Todos os Agendamentos:** Mostra uma listagem atualizada do status de todos os atendimentos.
- **3 - Buscar Agendamento por ID:** Retorna as informações baseadas no identificador único.
- **4 - CANCELAR um Agendamento:** Permite alterar o status para cancelado exigindo um motivo justificado.
- **5 - Alterar Informações:** Atualiza os dados de um agendamento já existente.
- **6 - Marcar como ATENDIDO:** Conclui o fluxo do agendamento.
- **0 - SAIR DO SISTEMA**
