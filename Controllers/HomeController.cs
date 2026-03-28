using Microsoft.AspNetCore.Mvc;
using ProjetoAgendamento.Services;

namespace ProjetoAgendamento.Controllers
{
    
    public class HomeController : Controller
    {
        private readonly AgendamentoService _service;

        //O ASP.Net vai entragar aqui!!!
        public HomeController(AgendamentoService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
           
            var lista = _service.ListarTodos();

            
            return View(lista);
        }
    }
}