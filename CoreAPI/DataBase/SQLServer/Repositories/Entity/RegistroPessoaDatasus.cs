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
        public string? MunicipioNascimento { get; set; }

        [Column(TypeName = "varchar(200)")]
        public string? Municipio { get; set; }

        [Column(TypeName = "varchar(200)")]
        public string? Logradouro { get; set; } 

        [Column(TypeName = "varchar(200)")]
        public string? Numero { get; set; }

        [Column(TypeName = "varchar(200)")]
        public string? Bairro { get; set; }

        [Column(TypeName = "varchar(200)")]
        public string? CEP { get; set; } 

        [Column(TypeName = "varchar(200)")]
        public string? RGNumero { get; set; }

        [Column(TypeName = "varchar(200)")]
        public string? RGOrgaoEmisor { get; set; } 

        [Column(TypeName = "varchar(200)")]
        public string? RGUF { get; set; }

        [Column(TypeName = "varchar(200)")]
        public string? RGDataEmissao { get; set; }

        [Column(TypeName = "varchar(200)")]
        public string? CNS { get; set; }

        [Column(TypeName = "varchar(200)")]
        public string? Telefone { get; set; }

        [Column(TypeName = "varchar(200)")]
        public string? TelefoneSecundario { get; set; } 
        [Column(TypeName = "nchar(10)")]
        public string? Status { get; set; } 

    }
}
