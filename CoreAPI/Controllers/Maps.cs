using CoreAPI.Context;
using CoreAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CoreAPI.Controllers
{
    public static class Maps
    {
        public static void GetMaps(this WebApplication app)
        {
            app.MapGet("/index", () => "Minimal CoreAPI - Rodando...");

            app.MapGet("/getByCPF/{cpf}", async (string cpf, RegistroDbContext db) =>
            {
                if (string.IsNullOrWhiteSpace(cpf))
                    return Results.BadRequest("CPF não pode ser nulo ou vazio.");

                return await db.RegistroPessoa.FindAsync(cpf)
                    is RegistroPessoa registro
                    ? Results.Ok(registro)
                    : Results.NotFound("CPF não localizado.");
            })
            .WithName("getByCPF")
            .Produces<RegistroPessoa>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .WithTags("Consulta Por CPF");

            app.MapGet("/getByNome/{nome}", async (string nome, RegistroDbContext db) =>
            {
                if (string.IsNullOrWhiteSpace(nome))
                    return Results.BadRequest("Nome não pode ser nulo ou vazio.");

                var registros = await db.RegistroPessoa.Where(r => r.Nome == nome).ToListAsync();

                return registros is not null && registros.Any()
                    ? Results.Ok(registros)
                    : Results.NotFound("Nome não localizado.");
            })
            .WithName("getByNome")
            .Produces<List<RegistroPessoa>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .WithTags("Consulta Por Nome");
        }
       
    }
}
