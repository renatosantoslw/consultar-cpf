using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using CoreAPI.Logs;
using CoreAPI.Data.Entity;

namespace CoreAPI.Data.Context
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
            base.Database.SetCommandTimeout(5);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            /*
            if (!optionsBuilder.IsConfigured)
            {
                var configuration = new ConfigurationBuilder()
                    .SetBasePath(AppContext.BaseDirectory)
                    .AddJsonFile("appsettings.json")
                    .Build();

                var connectionString = configuration.GetConnectionString("DefaultConnection");
                optionsBuilder.UseSqlServer(connectionString);
            }
            */


        }

        public bool TestarConexao(string strConexao)
        {
            ErrosLogGravar log = new();

            using (var objConexao = new SqlConnection(strConexao))
            {
                try
                {
                    objConexao.Open();
                    return true;
                }
                catch (Exception ex)
                {

                    log.GerarLogErro(ex, "Index", "TestarConexao");
                    return false;
                }
            }
        }






    }
}
