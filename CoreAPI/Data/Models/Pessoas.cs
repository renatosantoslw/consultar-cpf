using CoreAPI.Data.Entity;

namespace CoreAPI.Data.Models
{
    public class Pessoas
    {

        public string? CPF { get; set; } = string.Empty;
        public string? Nome { get; set; } = string.Empty;
        public string? Genero { get; set; } = string.Empty;
        public string? DataNascimento { get; set; } = string.Empty;
        public string? Status { get; set; } = string.Empty;

        public static Pessoas FromRegistroPessoa(RegistroPessoa registroPessoa)
        {
            return new Pessoas
            {
                CPF = registroPessoa.CPF,
                Nome = registroPessoa.Nome,
                Genero = registroPessoa.Genero,
                DataNascimento = registroPessoa.DataNascimento,
                Status = registroPessoa.Status
            };
        }
    }


}
