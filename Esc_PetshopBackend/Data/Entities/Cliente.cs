using Esc_PetshopBackend.Data.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Esc_PetshopBackend.Data.Entities
{
    public class Cliente
    {
        public int Id { get; set; }

        [Required]
        public int UsuarioId { get; set; }

        [ForeignKey("UsuarioId")]
        public Usuario Usuario { get; set; }

        [StringLength(14)]
        public string Cpf { get; set; }

        [StringLength(15)]
        public string Telefone { get; set; }

        public DateTime? DataNascimento { get; set; }

        [StringLength(9)]
        public string Cep { get; set; }

        [StringLength(200)]
        public string Endereco { get; set; }

        [StringLength(10)]
        public string Numero { get; set; }

        [StringLength(100)]
        public string Complemento { get; set; }

        [StringLength(100)]
        public string Bairro { get; set; }

        [StringLength(100)]
        public string Cidade { get; set; }

        [StringLength(2)]
        public string Estado { get; set; }

        public bool CadastroPendente { get; set; } = true;
    }
}