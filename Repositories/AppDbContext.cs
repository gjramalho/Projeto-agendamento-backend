using Microsoft.EntityFrameworkCore;
using ProjetoAgendamento.Models;

namespace ProjetoAgendamento.Repositories
{
    public class AppDbContext : DbContext
    {
        public DbSet<Agendamento> Agendamentos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = "Server=localhost,1433;Database=AgendamentoDB;User Id=sa;Password=Cavalo24123#;TrustServerCertificate=True;";

            optionsBuilder.UseSqlServer(connectionString, sqlOptions =>
            {
                sqlOptions.EnableRetryOnFailure();
            });
        }
    }
}