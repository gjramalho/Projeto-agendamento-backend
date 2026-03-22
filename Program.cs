using ProjetoAgendamento.Models;
using ProjetoAgendamento.Repositories;
using ProjetoAgendamento.Services;
using System;

namespace ProjetoAgendamento
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new AppDbContext())
            {
                context.Database.EnsureCreated();
            }

            AgendamentoService service = new AgendamentoService();
            bool continuar = true;

            while (continuar)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("==========================================");
                Console.WriteLine("        SISTEMA DE AGENDAMENTOS SQL       ");
                Console.WriteLine("==========================================");
                Console.ResetColor();
                Console.WriteLine(" 1 - Adicionar Novo Agendamento");
                Console.WriteLine(" 2 - Listar Todos os Agendamentos");
                Console.WriteLine(" 3 - Buscar Agendamento por ID");
                Console.WriteLine(" 4 - CANCELAR um Agendamento (Com Motivo)");
                Console.WriteLine(" 5 - Alterar Informações"); 
                Console.WriteLine(" 6 - Marcar como ATENDIDO");
                Console.WriteLine(" 0 - SAIR DO SISTEMA");
                Console.WriteLine("==========================================");
                Console.Write(" Escolha uma opção: ");

                string opcao = Console.ReadLine() ?? "";

                if (opcao == "0") break;

                switch (opcao)
                {
                    case "1":
                        Console.WriteLine("\n--- NOVO AGENDAMENTO ---");
                        try
                        {
                            Console.Write("Nome: ");
                            string cliente = Console.ReadLine() ?? "Anônimo";
                            Console.Write("Serviço: ");
                            string servico = Console.ReadLine() ?? "Geral";
                            Console.Write("Data e Hora (ex: 20/10/2026 15:30): ");
                            DateTime dataDigitada = DateTime.Parse(Console.ReadLine() ?? DateTime.Now.ToString());

                            Agendamento novo = new Agendamento(0, cliente, servico, dataDigitada);
                            service.Adicionar(novo);
                            ExibirMensagem("\n[SUCESSO] Salvo no SQL Server!", ConsoleColor.Green);
                        }
                        catch (Exception ex) { ExibirMensagem($"Erro: {ex.Message}", ConsoleColor.Red); }
                        break;

                    case "2":
                        Console.WriteLine("\n--- LISTA DE AGENDAMENTOS ---");
                        foreach (var item in service.ListarTodos())
                        {
                            Console.WriteLine($"ID: {item.Id} | Cliente: {item.NomeCliente} | Status: {item.Status} | Motivo: {item.MotivoCancelamento ?? "---"}");
                        }
                        break;

                    case "3":
                        Console.Write("\nDigite o ID para busca: ");
                        if (int.TryParse(Console.ReadLine(), out int idBusca))
                        {
                            var enc = service.BuscarPorId(idBusca);
                            if (enc != null) Console.WriteLine($"Encontrado: {enc.NomeCliente} - {enc.Servico} [{enc.Status}]");
                            else ExibirMensagem("Não encontrado.", ConsoleColor.Yellow);
                        }
                        break;

                    case "4":
                        Console.Write("ID para CANCELAR: ");
                        if (int.TryParse(Console.ReadLine(), out int idCancel))
                        {
                            Console.Write("Motivo: ");
                            string motivo = Console.ReadLine() ?? "Não informado";
                            if (service.CancelarComMotivo(idCancel, motivo)) ExibirMensagem("Cancelado com sucesso!", ConsoleColor.Green);
                            else ExibirMensagem("ID não encontrado.", ConsoleColor.Red);
                        }
                        break;

                    case "5": 
                        Console.Write("Digite o ID do agendamento que deseja ALTERAR: ");
                        if (int.TryParse(Console.ReadLine(), out int idEditar))
                        {
                            var agendamento = service.BuscarPorId(idEditar);
                            if (agendamento != null)
                            {
                                Console.WriteLine($"\nEditando cliente: {agendamento.NomeCliente}");
                                Console.Write("Digite o novo NOME (ou pressione Enter para manter): ");
                                string novoNome = Console.ReadLine() ?? "";

                                if (!string.IsNullOrWhiteSpace(novoNome))
                                {
                                    agendamento.NomeCliente = novoNome;
                                    service.Atualizar(agendamento);
                                    ExibirMensagem("[SUCESSO] Informações atualizadas!", ConsoleColor.Green);
                                }
                            }
                            else ExibirMensagem("ID não encontrado para edição.", ConsoleColor.Yellow);
                        }
                        break;

                    case "6":
                        Console.Write("ID para marcar como ATENDIDO: ");
                        if (int.TryParse(Console.ReadLine(), out int idAtendido))
                        {
                            if (service.FinalizarAtendimento(idAtendido)) ExibirMensagem("Status atualizado!", ConsoleColor.Green);
                            else ExibirMensagem("ID não encontrado.", ConsoleColor.Red);
                        }
                        break;

                    default:
                        ExibirMensagem("Opção Inválida!", ConsoleColor.Red);
                        break;
                }

                Console.WriteLine("\nPressione qualquer tecla para voltar ao menu...");
                Console.ReadKey();
            }
        }

        static void ExibirMensagem(string mensagem, ConsoleColor cor)
        {
            Console.ForegroundColor = cor;
            Console.WriteLine(mensagem);
            Console.ResetColor();
        }
    }
}