using CoreAPI.DataBase.SQLServer.Repositories.Entity;

namespace CoreAPI.DataBase.SQLServer.Models
{
    public class RegistroPessoaDatasusDTO
    {
        public int? Id { get; set; } 
        public string? CPF { get; set; } 
        public string? Pai { get; set; } 
        public string? Mae { get; set; } 
        public string? MunicipioNascimento { get; set; } 
        public string? Municipio { get; set; } 
        public string? Logradouro { get; set; } 
        public string? Numero { get; set; } 
        public string? Bairro { get; set; } 
        public string? CEP { get; set; } 
        public string? RGNumero { get; set; }
        public string? RGOrgaoEmisor { get; set; } 
        public string? RGUF { get; set; }
        public string? RGDataEmissao { get; set; }
        public string? CNS { get; set; }
        public string? Telefone { get; set; } 
        public string? TelefoneSecundario { get; set; } 
        public string? Status { get; set; }

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
