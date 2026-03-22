using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoAgendamento.Models
{
    public class Agendamento
    {
        // 1. O EF precisa deste construtor vazio para funcionar!
        public Agendamento() { }

        // 2. O seu construtor que você já usa no Program.cs continua aqui:
        public Agendamento(int id, string cliente, string servico, DateTime inicio)
        {
            Id = id;
            NomeCliente = cliente;
            Servico = servico;
            DataHoraInicio = inicio;
        }



        // Suas propriedades (Id, Cliente, etc...)

        public string? MotivoCancelamento { get; set; }
        public string Status { get; set; } = "Pendente"; // Valor padrão
        public int DuracaoMinutos { get; set; } = 30; 
        public int Id { get; set; }
        public string NomeCliente { get; set; } = string.Empty;
        public string Servico { get; set; } = string.Empty;
        public DateTime DataHoraInicio { get; set; }
    }
}

