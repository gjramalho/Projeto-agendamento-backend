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
        public void Adicionar(Agendamento novo)
        {
            using (var context = new AppDbContext())
            {
                DateTime novoTermino = novo.DataHoraInicio.AddMinutes(novo.DuracaoMinutos);

                bool conflito = context.Agendamentos.Any(a =>
                    novo.DataHoraInicio < a.DataHoraInicio.AddMinutes(a.DuracaoMinutos) &&
                    novoTermino > a.DataHoraInicio);

                if (conflito)
                {
                    throw new Exception("Conflito de horário! Já existe um atendimento em andamento neste período.");
                }

                context.Agendamentos.Add(novo);
                context.SaveChanges();
            }
        }

        public List<Agendamento> ListarTodos()
        {
            using (var context = new AppDbContext())
            {
                return context.Agendamentos.AsNoTracking().ToList();
            }
        }

        public Agendamento BuscarPorId(int id)
        {
            using (var context = new AppDbContext())
            {
                return context.Agendamentos.Find(id);
            }
        }

        public bool Cancelar(int id)
        {
            using (var context = new AppDbContext())
            {
                var agendamento = context.Agendamentos.Find(id);
                if (agendamento == null) return false;

                agendamento.Status = "Cancelado";
                context.SaveChanges();
                return true;
            }
        }

        public bool FinalizarAtendimento(int id)
        {
            using (var context = new AppDbContext())
            {
                var agendamento = context.Agendamentos.FirstOrDefault(a => a.Id == id);

                if (agendamento == null) return false;

                agendamento.Status = "Atendido";

                context.Entry(agendamento).State = EntityState.Modified;

                context.SaveChanges();
                return true;
            }
        }

        public bool CancelarComMotivo(int id, string motivo)
        {
            using (var context = new AppDbContext())
            {
                var agendamento = context.Agendamentos.Find(id);
                if (agendamento == null) return false;

                agendamento.Status = "Cancelado";
                agendamento.MotivoCancelamento = motivo;

                context.Entry(agendamento).State = EntityState.Modified;
                context.SaveChanges();
                return true;
            }
        }

        public void Atualizar(Agendamento agendamentoAtualizado)
        {
            using (var context = new AppDbContext())
            {
                context.Agendamentos.Update(agendamentoAtualizado);
                context.SaveChanges();
            }
        }

        public void AlterarStatus(int id, string novoStatus)
        {
            using (var context = new AppDbContext())
            {
                var agendamento = context.Agendamentos.Find(id);
                if (agendamento != null)
                {
                    agendamento.Status = novoStatus;
                    context.SaveChanges();
                }
            }
        }
    }
}