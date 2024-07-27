using CoreAPI.Data.Context;
using CoreAPI.Data.Entity;
using CoreAPI.Logs;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Reflection;

namespace CoreAPI.Controllers
{
    public static class Maps
    {
        private static readonly ErrosLogGravar _errosLog = new ErrosLogGravar();

        public static void GetMaps(this WebApplication app)
        {
            app.MapGet("/index", () => "Minimal CoreAPI - Rodando...");

            app.MapPost("/restartAPI", () =>
            {
                string exePath = Assembly.GetExecutingAssembly().Location;

                Process.Start(new ProcessStartInfo
                {
                    FileName = "dotnet",
                    Arguments = exePath,
                    UseShellExecute = true,
                    Verb = "runas",
                    CreateNoWindow = true
                });

                Environment.Exit(0);
            })
            .WithName("restartAPI")
            .ExcludeFromDescription()
            .WithTags("Restart API");

            app.MapGet("/getByCPF/{cpf}", async (string cpf, RegistroDbContext db) =>
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(cpf))
                        return Results.BadRequest("CPF não pode ser nulo ou vazio.");

                    return await db.RegistroPessoa.FindAsync(cpf)
                        is RegistroPessoa registro
                        ? Results.Ok(registro)
                        : Results.NotFound("CPF não localizado.");
                }
                catch (Exception ex)
                {
                    ErroLogsInstance.GerarLogErro(ex, $"Maps", $"app.MapGet(getByCPF) = {cpf}");              
                    return null;
                }

            })
            .WithName("getByCPF")
            .Produces<RegistroPessoa>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError)
            .WithTags("Consulta Por CPF");

            app.MapGet("/getByNome/{nome}", async (string nome, RegistroDbContext db) =>
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(nome))
                        return Results.BadRequest("Nome não pode ser nulo ou vazio.");

                    var registros = await db.RegistroPessoa.Where(r => r.Nome == nome).ToListAsync();

                    return registros is not null && registros.Any()
                        ? Results.Ok(registros)
                        : Results.NotFound("Nome não localizado.");
                }
                catch (Exception ex)
                {
                    ErroLogsInstance.GerarLogErro(ex, $"Maps", $"app.MapGet(getByNome) = {nome}");
                    return null;
                }
            })
            .WithName("getByNome")
            .Produces<List<RegistroPessoa>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError)
            .WithTags("Consulta Por Nome");

        }

        public static ErrosLogGravar ErroLogsInstance
        {
            get { return _errosLog; }
        }

    }

}
