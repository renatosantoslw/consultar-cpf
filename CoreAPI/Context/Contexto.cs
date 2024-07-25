using CoreAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CoreAPI.Context
{
    class RegistroDbContext : DbContext
    {
        public DbSet<RegistroPessoa> RegistroPessoa { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configurar índice não clusterizado na coluna Nome
            modelBuilder.Entity<RegistroPessoa>()
                        .HasIndex(p => p.Nome)
                        .IsClustered(false)
                        .HasDatabaseName("IX_Pessoa_Nome");

            base.OnModelCreating(modelBuilder);
        }

        public RegistroDbContext(DbContextOptions<RegistroDbContext> options)
            : base(options)
        {
            base.Database.SetCommandTimeout(3000);
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Configuração da string de conexão com o banco de dados SQL Server
            var connectionString =
                "Server=localhost;" +
                "Database=BANCOTXT;" +
                "Uid=sa;Pwd=epilef;" +
                "TrustServerCertificate=True;" +
                "Connection Timeout=3000";

            optionsBuilder.UseSqlServer(connectionString);
        }


    }
}
