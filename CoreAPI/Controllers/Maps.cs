using CoreAPI.ClassesLogs;
using CoreAPI.DataBase.SQLServer.Context;
using CoreAPI.DataBase.SQLServer.Repositories.Entity;
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

            //POST Restarta a API
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
            .ExcludeFromDescription() //Oculta do Swagger
            .WithTags("Restart API");

            //GET Banco de dados JBR_PF - Tabela RegistroPessoas - CPF
            app.MapGet("/getByCPF/{cpf}", async (string cpf, Context db) =>
            {
                try
                {

                    if (string.IsNullOrWhiteSpace(cpf))
                        return Results.BadRequest("CPF não pode ser nulo ou vazio.");

                    var registro = await db.RegistroPessoa
                        .AsNoTracking()
                        .FirstOrDefaultAsync(r => r.CPF == cpf);
                   
                    return registro is not null
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
            .WithTags("Consulta Por CPF - JBR_PF");

            //GET Banco de dados JBR_PF - Tabela RegistroPessoas - Nome
            app.MapGet("/getByNome/{nome}", async (string nome, Context db) =>
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(nome))
                        return Results.BadRequest("Nome não pode ser nulo ou vazio.");

                    var registros = await db.RegistroPessoa
                        .AsNoTracking()
                        .Where(r => r.Nome == nome)
                        .ToListAsync();

                    return registros.Any()
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
            .WithTags("Consulta Por Nome - JBR_PF");

            //GET Banco de dados "DATASUS" - Tabela RegistroPessoaDatasus - CPF
            app.MapGet("/getByCPFDataSUS/{cpf}", async (string cpf, Context db) =>
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(cpf))
                        return Results.BadRequest("CPF não pode ser nulo ou vazio.");

                    var registroDataSUS = await db.RegistroPessoaDatasus
                        .AsNoTracking()
                        .FirstOrDefaultAsync(r => r.CPF == cpf);

                    return registroDataSUS is not null
                          ? Results.Ok(registroDataSUS)
                          : Results.NotFound("CPF DATASUS não localizado.");
                }
                catch (Exception ex)
                {
                    ErroLogsInstance.GerarLogErro(ex, $"Maps", $"app.MapGet(getByCPFDataSUS) = {cpf}");
                    return Results.Problem("Ocorreu um erro ao processar a solicitação.");
                }

            })
            .WithName("getByCPFDataSUS")
            .Produces<RegistroPessoaDatasus>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError)
            .WithTags("Consulta Por CPF - 'DATASUS'");
        }

        public static ErrosLogGravar ErroLogsInstance
        {
            get { return _errosLog; }
        }



    }

}
