using Microsoft.EntityFrameworkCore;
using ProjetoAgendamento.Models;

namespace ProjetoAgendamento.Repositories
{
    public class AppDbContext : DbContext
    {
        // ver se o nome é "Agendamentos" (plural) 
        // e o tipo é "Agendamento" (singular - classe)  
        public DbSet<Agendamento> Agendamentos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //  mudei o AgendamentoDB para master aqui na string
            string connectionString = "Server=localhost,1433;Database=AgendamentoDB;User Id=sa;Password=Cavalo24123#;TrustServerCertificate=True;";

            optionsBuilder.UseSqlServer(connectionString, sqlOptions =>
            {
                sqlOptions.EnableRetryOnFailure();
            });
        }
    }
}