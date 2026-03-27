using Microsoft.EntityFrameworkCore;
using ProjetoAgendamento.Models;
using ProjetoAgendamento.Repositories; // resolve erro do AppDbContext
using ProjetoAgendamento.Services;     // resolve erro do AgendamentoService

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<AgendamentoService>();

// 1. Adiciona suporte ao MVC (Telas)
builder.Services.AddControllersWithViews();

// 2. Registra o seu Banco de Dados (Dê o nome correto do seu Contexto)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer("Server=localhost,1433;Database=AgendamentoDB;User Id=sa;Password=Cavalo24123#;TrustServerCertificate=True;"));

//Registro do Service!
builder.Services.AddScoped<AgendamentoService>();

var app = builder.Build();

// Configurações básicas de erro e HTTPS
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles(); // Importante para o CSS/Tailwind funcionar
app.UseRouting();
app.UseAuthorization();

// 3. Define a rota: se você abrir o site, ele vai procurar o Home/Index
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();