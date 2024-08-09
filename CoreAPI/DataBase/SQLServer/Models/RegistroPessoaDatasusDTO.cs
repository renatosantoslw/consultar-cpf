using CoreAPI.DataBase.SQLServer.Repositories.Entity;

namespace CoreAPI.DataBase.SQLServer.Models
{
    public class RegistroPessoaDatasusDTO
    {
        public int? Id { get; set; } 
        public string? CPF { get; set; } = string.Empty;
        public string? Pai { get; set; } = string.Empty;
        public string? Mae { get; set; } = string.Empty;
        public string? MunicipioNascimento { get; set; } = string.Empty;
        public string? Municipio { get; set; } = string.Empty;
        public string? Logradouro { get; set; } = string.Empty;
        public string? Numero { get; set; } = string.Empty;
        public string? Bairro { get; set; } = string.Empty;
        public string? CEP { get; set; } = string.Empty;
        public string? RGNumero { get; set; } = string.Empty;
        public string? RGOrgaoEmisor { get; set; } = string.Empty;
        public string? RGUF { get; set; } = string.Empty;
        public string? RGDataEmissao { get; set; } = string.Empty;
        public string? CNS { get; set; } = string.Empty;
        public string? Telefone { get; set; } = string.Empty;
        public string? TelefoneSecundario { get; set; } = string.Empty;
        public string? Status { get; set; } = string.Empty;

        public static RegistroPessoaDatasusDTO FromRegistroPessoaDatasus(RegistroPessoaDatasus? registroPessoa)
        {
            return new RegistroPessoaDatasusDTO
            {
                Id = registroPessoa?.Id,
                CPF = string.IsNullOrEmpty(registroPessoa?.CPF) ? "SEM INFORMAÇÃO" : registroPessoa.CPF,
                Pai = string.IsNullOrEmpty(registroPessoa?.Pai) ? "SEM INFORMAÇÃO" : registroPessoa.Pai,
                Mae = string.IsNullOrEmpty(registroPessoa?.Mae) ? "SEM INFORMAÇÃO" : registroPessoa.Mae,
                MunicipioNascimento = string.IsNullOrEmpty(registroPessoa?.MunicipioNascimento) ? "SEM INFORMAÇÃO" : registroPessoa.MunicipioNascimento,
                Municipio = string.IsNullOrEmpty(registroPessoa?.Municipio) ? "SEM INFORMAÇÃO" : registroPessoa.Municipio,
                Logradouro = string.IsNullOrEmpty(registroPessoa?.Logradouro) ? "SEM INFORMAÇÃO" : registroPessoa.Logradouro,
                Numero = string.IsNullOrEmpty(registroPessoa?.Numero) ? "SEM INFORMAÇÃO" : registroPessoa.Numero,
                Bairro = string.IsNullOrEmpty(registroPessoa?.Bairro) ? "SEM INFORMAÇÃO" : registroPessoa.Bairro,
                CEP = string.IsNullOrEmpty(registroPessoa?.CEP) ? "SEM INFORMAÇÃO" : registroPessoa.CEP,
                RGNumero = string.IsNullOrEmpty(registroPessoa?.RGNumero) ? "SEM INFORMAÇÃO" : registroPessoa.RGNumero,
                RGOrgaoEmisor = string.IsNullOrEmpty(registroPessoa?.RGOrgaoEmisor) ? "SEM INFORMAÇÃO" : registroPessoa.RGOrgaoEmisor,
                RGUF = string.IsNullOrEmpty(registroPessoa?.RGUF) ? "SEM INFORMAÇÃO" : registroPessoa.RGUF,
                RGDataEmissao = string.IsNullOrEmpty(registroPessoa?.RGDataEmissao) ? "SEM INFORMAÇÃO" : registroPessoa.RGDataEmissao,
                CNS = string.IsNullOrEmpty(registroPessoa?.CNS) ? "SEM INFORMAÇÃO" : registroPessoa.CNS,
                Telefone = string.IsNullOrEmpty(registroPessoa?.Telefone) ? "SEM INFORMAÇÃO" : registroPessoa.Telefone,
                TelefoneSecundario = string.IsNullOrEmpty(registroPessoa?.TelefoneSecundario) ? "SEM INFORMAÇÃO" : registroPessoa.TelefoneSecundario,
                Status = string.IsNullOrEmpty(registroPessoa?.Status) ? "0" : registroPessoa.Status,
            };
        }
    }
}
