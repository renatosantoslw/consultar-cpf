using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using CoreAPI.Controllers;
using CoreAPI.Logs;
using CoreAPI.Funcoes;
using CoreAPI.DataBase.SQLServer.Context;

ErrosLogGravar erroLogs = new ErrosLogGravar();
ReinicializarAPI reStartAPI = new ReinicializarAPI();

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddSingleton<ErrosWiew>();

builder.Services.AddDirectoryBrowser();
builder.Services.AddRazorPages();

builder.Services.AddDbContext<Context>(options =>
options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

builder.WebHost.UseUrls("http://0.0.0.0:5000");
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "API - Consulta CPF",
        Description = "API para estudos."
    });
});

//Oculta alguns logs:
builder.Logging.AddFilter("Microsoft.EntityFrameworkCore.Database.Command", LogLevel.None);
builder.Logging.AddFilter("Microsoft.Hosting.Lifetime", LogLevel.None);
builder.Logging.AddFilter("Microsoft.EntityFrameworkCore.Database.Connection", LogLevel.None);
builder.Logging.AddFilter("Microsoft.EntityFrameworkCore.Query", LogLevel.None);
builder.Logging.AddFilter("Microsoft.AspNetCore.Diagnostics.DeveloperExceptionPageMiddleware", LogLevel.None);
//builder.Logging.AddFilter("Microsoft.EntityFrameworkCore.Migrations", LogLevel.Warning);
//builder.Logging.AddFilter("CoreAPI", LogLevel.Warning);

var app = builder.Build();

app.MapControllers();
app.MapRazorPages();
app.UseStaticFiles();
app.UseRouting();
app.MapSwagger();
app.UseSwaggerUI();
Maps.GetMaps(app);

await Task.Delay(10000); // Aguarda 10 segundos, se necessário, para garantir que todos os serviços estejam prontos.
await VerificaDBExiste(app.Services, app.Logger, app.Services.GetRequiredService<ErrosWiew>(), configuration);

app.Run();

async Task VerificaDBExiste(IServiceProvider services, ILogger logger, ErrosWiew? erros, IConfiguration configuration)
{
    try
    {
        using var scope = services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<Context>();

        logger.LogInformation("Tentando estabelecer uma Conexão com o banco de dados...");
        
        var conectou = await db.Database.CanConnectAsync();
        if (!conectou)
        {
            logger.LogInformation("Não foi possivel estabelecer uma Conexão com o banco de dados...");
            logger.LogInformation("Uma nova tentativa estabelecer uma Conexão o banco de dados será feita...");
        }

        await db.Database.MigrateAsync();

        #region"Talvez eu use"
        /*
        var pendingMigrations = db.Database.GetPendingMigrations();
     
        if (pendingMigrations.Any())
        {
            logger.LogInformation("Migrações pendentes encontradas...");

            var isInitialCreatePending = pendingMigrations.Any(m => m.Contains("InitialCreate"));

            if (!isInitialCreatePending)
            {
                logger.LogInformation("Aplicando migrações...");
                await db.Database.MigrateAsync();
                logger.LogInformation("Migrações aplicadas com sucesso...");
            }
            else
            {
                logger.LogInformation("Aplicando migração inicial...");
                await db.Database.MigrateAsync();
                logger.LogInformation("Migração aplicadas com sucesso...");
            }
        }
        */
        #endregion
    }
    catch (Exception ex)
    {
        erroLogs.GerarLogErro(ex, "Program", "VerificaDBExiste");
        logger.LogCritical($"Erro: {ex.Message}");
        logger.LogCritical($"Erro Critico na Inicializacao... API será reinicializada...");
        logger.LogCritical($"API será reinicializada...");
        await Task.Delay(5000);
        reStartAPI.Reiniciar();
    }
}






