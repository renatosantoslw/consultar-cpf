using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreAPI.DataBase.SQLServer.Repositories.Entity
{
    public class RegistroPessoa
    {
        [Key]
        [Column(TypeName = "varchar(20)")]
        public string? CPF { get; set; } = string.Empty;

        [Column(TypeName = "varchar(100)")]
        public string? Nome { get; set; } = string.Empty;

        [Column(TypeName = "varchar(30)")]
        public string? Genero { get; set; } = string.Empty;

        [Column(TypeName = "varchar(30)")]
        public string? DataNascimento { get; set; } = string.Empty;

        [Column(TypeName = "nchar(10)")]
        public string? Status { get; set; } = string.Empty;

    }
}
