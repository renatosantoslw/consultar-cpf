using CoreAPI.DataBase.SQLServer.Repositories.Entity;

namespace CoreAPI.DataBase.SQLServer.Models
{
    public class RegistroPessoaDTO
    {
        public string? CPF { get; set; }
        public string? Nome { get; set; }
        public string? Genero { get; set; }
        public string? DataNascimento { get; set; }
        public string? Status { get; set; }

        public static RegistroPessoaDTO FromRegistroPessoa(RegistroPessoa? registroPessoa)
        {
            return new RegistroPessoaDTO
            {
                CPF = string.IsNullOrEmpty(registroPessoa?.CPF) ? "SEM INFORMAÇÃO" : registroPessoa.CPF,
                Nome = string.IsNullOrEmpty(registroPessoa?.Nome) ? "SEM INFORMAÇÃO" : registroPessoa.Nome,
                Genero = string.IsNullOrEmpty(registroPessoa?.Genero) ? "SEM INFORMAÇÃO" : registroPessoa.Genero,
                DataNascimento = string.IsNullOrEmpty(registroPessoa?.DataNascimento) ? "SEM INFORMAÇÃO" : registroPessoa.DataNascimento,
                Status = string.IsNullOrEmpty(registroPessoa?.Status) ? "0" : registroPessoa.Status,
            };
        }
    }
}
