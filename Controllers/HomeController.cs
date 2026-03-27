using Microsoft.AspNetCore.Mvc;
using ProjetoAgendamento.Services;

namespace ProjetoAgendamento.Controllers
{
    // ele vai herda de 'Controller' (com suporte a Telas/Views)
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
            //busca lista do banco
            var lista = _service.ListarTodos();

            //Passamos a lista para a view (HTML)
            return View(lista);
        }
    }
}