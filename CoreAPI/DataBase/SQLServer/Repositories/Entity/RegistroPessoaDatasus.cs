using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreAPI.DataBase.SQLServer.Repositories.Entity
{
    public class RegistroPessoaDatasus
    {
        [Key]
        [Column(TypeName = "int")]
        public int? Id { get; set; } = 0;

        [Column(TypeName = "varchar(20)")]
        public string? CPF { get; set; } = string.Empty;

        [Column(TypeName = "varchar(200)")]
        public string? Pai { get; set; } = string.Empty;

        [Column(TypeName = "varchar(200)")]
        public string? Mae { get; set; } = string.Empty;

        [Column(TypeName = "varchar(200)")]
        public string? MunicipioNascimento { get; set; } = string.Empty;

        [Column(TypeName = "varchar(200)")]
        public string? Municipio { get; set; } = string.Empty;

        [Column(TypeName = "varchar(200)")]
        public string? Logradouro { get; set; } = string.Empty;

        [Column(TypeName = "varchar(200)")]
        public string? Numero { get; set; } = string.Empty;

        [Column(TypeName = "varchar(200)")]
        public string? Bairro { get; set; } = string.Empty;

        [Column(TypeName = "varchar(200)")]
        public string? CEP { get; set; } = string.Empty;

        [Column(TypeName = "varchar(200)")]
        public string? RGNumero { get; set; } = string.Empty;

        [Column(TypeName = "varchar(200)")]
        public string? RGOrgaoEmisor { get; set; } = string.Empty;

        [Column(TypeName = "varchar(200)")]
        public string? RGUF { get; set; } = string.Empty;

        [Column(TypeName = "varchar(200)")]
        public string? RGDataEmissao { get; set; } = string.Empty;

        [Column(TypeName = "varchar(200)")]
        public string? CNS { get; set; } = string.Empty;

        [Column(TypeName = "varchar(200)")]
        public string? Telefone { get; set; } = string.Empty;

        [Column(TypeName = "varchar(200)")]
        public string? TelefoneSecundario { get; set; } = string.Empty;
        [Column(TypeName = "nchar(10)")]
        public string? Status { get; set; } = string.Empty;

    }
}
