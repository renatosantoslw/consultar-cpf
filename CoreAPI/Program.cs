using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using CoreAPI.Models;
using CoreAPI.Controllers;
using CoreAPI.Context;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDirectoryBrowser();
builder.Services.AddRazorPages();

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
builder.Logging.AddFilter("Microsoft.EntityFrameworkCore.Database.Command", LogLevel.Warning);
builder.Logging.AddFilter("Microsoft.Hosting.Lifetime", LogLevel.Warning);

var app = builder.Build();
await AsseguraDBExiste(app.Services, app.Logger);

app.MapControllers(); 
app.MapRazorPages();
app.UseStaticFiles();
app.UseRouting();
app.MapSwagger();
app.UseSwaggerUI();

Maps.GetMaps(app);

app.Run();

async Task AsseguraDBExiste(IServiceProvider services, ILogger logger)
{
    logger.LogInformation($"Garantindo que o banco de dados exista e esteja na string de conexão...");
    using var db = services.CreateScope().ServiceProvider.GetRequiredService<RegistroDbContext>();
    await db.Database.EnsureCreatedAsync();
    await db.Database.MigrateAsync();
    db.Database.SetCommandTimeout(3000);
}




