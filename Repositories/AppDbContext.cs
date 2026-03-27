using Microsoft.EntityFrameworkCore;
using ProjetoAgendamento.Models;

namespace ProjetoAgendamento.Repositories
{
   public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions <AppDbContext> options) : base(options)
        {
        }
         public DbSet<Agendamento> Agendamentos { get; set; }
    }
}