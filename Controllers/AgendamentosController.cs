using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoAgendamento.Models;
using ProjetoAgendamento.Repositories; 

namespace ProjetoAgendamento.Controllers
{
    public class AgendamentosController : Controller
    {
        private readonly AppDbContext _context;

        public AgendamentosController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var lista = await _context.Agendamentos.OrderBy(x => x.DataHoraInicio).ToListAsync();
            return View(lista);
        }

        [HttpPost]
        public async Task<IActionResult> CriarAgendamento([FromBody] Agendamento novoAgendamento)
        {
            if (ModelState.IsValid)
            {
                try
                {            
                    int intervaloMinutos = 15;
                 
                    DateTime inicioJanela = novoAgendamento.DataHoraInicio.AddMinutes(-intervaloMinutos);
                    DateTime fimJanela = novoAgendamento.DataHoraInicio.AddMinutes(intervaloMinutos);
                   
                    var conflito = await _context.Agendamentos
                        .AnyAsync(x => x.DataHoraInicio > inicioJanela && x.DataHoraInicio < fimJanela);

                    if (conflito)
                    {
                        return Json(new
                        {
                            success = false,
                            message = $"Intervalo insuficiente! Escolha um horário com mais de {intervaloMinutos} min de diferença."
                        });
                    }

                   
                    if (novoAgendamento.NomeCliente.Length < 4 || novoAgendamento.Servico.Length < 4)
                    {
                        return Json(new { success = false, message = "Nome e Serviço devem ter 4+ letras." });
                    }

                    novoAgendamento.Status = "Pendente";
                    _context.Agendamentos.Add(novoAgendamento);
                    await _context.SaveChangesAsync();

                    return Json(new { success = true });
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, message = "Erro ao salvar: " + ex.Message });
                }
            }
            return Json(new { success = false, message = "Dados inválidos." });
        }
       
        [HttpDelete]
        public async Task<IActionResult> Excluir(int id)
        {
            var agendamento = await _context.Agendamentos.FindAsync(id);
            if (agendamento == null) return Json(new { success = false, message = "Não encontrado." });

            _context.Agendamentos.Remove(agendamento);
            await _context.SaveChangesAsync();
            return Json(new { success = true, message = "Excluído com sucesso!" });
        }

        
        [HttpPost]
        public async Task<IActionResult> MudarStatus(int id, string novoStatus)
        {
            var agendamento = await _context.Agendamentos.FindAsync(id);
            if (agendamento == null) return Json(new { success = false, message = "Não encontrado." });

            agendamento.Status = novoStatus;
            _context.Agendamentos.Update(agendamento);
            await _context.SaveChangesAsync();
            return Json(new { success = true });
        }
    }

}