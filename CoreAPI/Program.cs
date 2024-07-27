using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using CoreAPI.Controllers;
using CoreAPI.Logs;
using CoreAPI.Funcoes;
using CoreAPI.Data.Context;

ErrosLogGravar erroLogs = new ErrosLogGravar();
ReinicializarAPI reStartAPI = new ReinicializarAPI();

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddSingleton<ErrosWiew>();

builder.Services.AddDirectoryBrowser();
builder.Services.AddRazorPages();
builder.Services.AddDbContext<RegistroDbContext>(options =>
options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

// Registrar o contexto do banco de dados
builder.Services.AddDbContext<RegistroDbContext>();
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


//Oculta alguns logs
//builder.Logging.AddFilter("Microsoft.EntityFrameworkCore.Migrations", LogLevel.Warning);
//builder.Logging.AddFilter("CoreAPI", LogLevel.Warning);
builder.Logging.AddFilter("Microsoft.EntityFrameworkCore.Database.Command", LogLevel.None);
builder.Logging.AddFilter("Microsoft.Hosting.Lifetime", LogLevel.None);
builder.Logging.AddFilter("Microsoft.EntityFrameworkCore.Database.Connection", LogLevel.None);
builder.Logging.AddFilter("Microsoft.EntityFrameworkCore.Query", LogLevel.None);
builder.Logging.AddFilter("Microsoft.AspNetCore.Diagnostics.DeveloperExceptionPageMiddleware", LogLevel.None);

 var app = builder.Build();

app.MapControllers();
app.MapRazorPages();
app.UseStaticFiles();
app.UseRouting();
app.MapSwagger();
app.UseSwaggerUI();
Maps.GetMaps(app);

await Task.Delay(15000);
await AsseguraDBExiste(app.Services, app.Logger, app.Services.GetRequiredService<ErrosWiew>(), configuration);

app.Run();

async Task AsseguraDBExiste(IServiceProvider services, ILogger logger, ErrosWiew? erros, IConfiguration configuration)
{
    logger.LogInformation($"Estabelecendo conexão com o Banco de Dados...");
    try
    {
        using var db = services.CreateScope().ServiceProvider.GetRequiredService<RegistroDbContext>();
        var bancoExiste = await db.Database.EnsureCreatedAsync();
        await db.Database.MigrateAsync();
        db.Database.SetCommandTimeout(5);
        logger.LogInformation("Banco de Dados conectado/criado com sucesso...");
    }
    catch (Exception ex)
    {   
        erroLogs.GerarLogErro(ex, "Program", "AsseguraDBExiste");
        logger.LogCritical($"Erro: {ex.Message}");
        logger.LogCritical($"Api sera reinicializada.");
        await Task.Delay(3000);       
        reStartAPI.Reiniciar();
    }
    
}





