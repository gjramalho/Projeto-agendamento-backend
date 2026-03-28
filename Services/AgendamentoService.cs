using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ProjetoAgendamento.Models;
using ProjetoAgendamento.Repositories;

namespace ProjetoAgendamento.Services
{
    public class AgendamentoService
    {
        private readonly AppDbContext _context;
        public AgendamentoService(AppDbContext context)
        {
            _context = context;
        }

        public void Adicionar(Agendamento novo)
        {
            DateTime novoTermino = novo.DataHoraInicio.AddMinutes(novo.DuracaoMinutos);

            bool conflito = _context.Agendamentos.Any(a =>
                novo.DataHoraInicio < a.DataHoraInicio.AddMinutes(a.DuracaoMinutos) &&
                novoTermino > a.DataHoraInicio);

            if (conflito)
            {
                throw new Exception("Conflito de horário! Já existe um atendimento neste período.");
            }

            _context.Agendamentos.Add(novo);
            _context.SaveChanges();
        }

        public List<Agendamento> ListarTodos()
        {
            return _context.Agendamentos.AsNoTracking().ToList();
        }

        public Agendamento? BuscarPorId(int id)
        {
            return _context.Agendamentos.Find(id);
        }

        public bool CancelarComMotivo(int id, string motivo)
        {
            var agendamento = _context.Agendamentos.Find(id);
            if (agendamento == null) return false;

            agendamento.Status = "Cancelado";
            agendamento.MotivoCancelamento = motivo;

            _context.SaveChanges();
            return true;
        }

        public bool FinalizarAtendimento(int id)
        {
            var agendamento = _context.Agendamentos.Find(id);
            if (agendamento == null) return false;

            agendamento.Status = "Atendido";
            _context.SaveChanges();
            return true;
        }

        public void Atualizar(Agendamento agendamento)
        {
            _context.Agendamentos.Update(agendamento);
            _context.SaveChanges();
        }
    }
}