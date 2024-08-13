using Microsoft.EntityFrameworkCore;
using CoreAPI.DataBase.SQLServer.Repositories.Entity;

namespace CoreAPI.DataBase.SQLServer.Context
{
    class Context : DbContext
    {
        public Context(DbContextOptions<Context> options)
            : base(options)
        {
            base.Database.SetCommandTimeout(200);
        }

        public DbSet<RegistroPessoa> RegistroPessoa { get; set; }
        public DbSet<RegistroPessoaDatasus> RegistroPessoaDatasus { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // Configura os Indices não Clusterizados:
            modelBuilder.Entity<RegistroPessoa>()
                        .HasIndex(p => p.Nome)
                        .IsClustered(false)
                        .HasDatabaseName("indexNome");

            modelBuilder.Entity<RegistroPessoaDatasus>()
                        .HasIndex(p => p.CPF)
                        .IsClustered(false)
                        .IsUnique(true)
                        .HasDatabaseName("indexCPF");

            base.OnModelCreating(modelBuilder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                    .AddJsonFile("appsettings.json")
                    .Build();

                var connectionString = configuration.GetConnectionString("DefaultConnection");
                optionsBuilder.UseSqlServer(connectionString);
            }

        }
    }
}
