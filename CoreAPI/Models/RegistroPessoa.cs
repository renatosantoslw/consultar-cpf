using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreAPI.Models
{
    public class RegistroPessoa
    {
        [Key]
        [Column(TypeName = "varchar(20)")]
        public string? CPF { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string? Nome { get; set; }

        [Column(TypeName = "varchar(30)")]
        public string? Genero { get; set; }

        [Column(TypeName = "varchar(30)")]
        public string? DataNascimento { get; set; }

        [Column(TypeName = "nchar(10)")]
        public string? Status { get; set; }
    }
}
