using Microsoft.EntityFrameworkCore;
using ProjetoAgendamento.Models;
using ProjetoAgendamento.Repositories;
using ProjetoAgendamento.Services;

var builder = WebApplication.CreateBuilder(args);

// 1. CONFIGURAÇÃO DOS SERVIÇOS (Tudo que usa 'builder')
// Pega a string de conexão do appsettings.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Registra o Banco de Dados
// Se você usar o nome completo, o erro some:
builder.Services.AddDbContext<ProjetoAgendamento.Repositories.AppDbContext>(options =>
    options.UseSqlServer(connectionString));

// Registra o Service (Regras de negócio)
builder.Services.AddScoped<AgendamentoService>();

// Adiciona suporte ao MVC (Telas e Controllers)
builder.Services.AddControllersWithViews();

// ---------------------------------------------------------
// 2. CONSTRUÇÃO DO APLICATIVO (O ponto sem retorno)
var app = builder.Build();
// ---------------------------------------------------------

// 3. CONFIGURAÇÃO DO PIPELINE (Como o app se comporta)
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles(); // Importante para o CSS/Tailwind funcionar
app.UseRouting();
app.UseAuthorization();

// Define a rota padrão
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();