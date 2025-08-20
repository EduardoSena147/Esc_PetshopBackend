using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Esc_PetshopBackend.Data.Entities
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public DateTime DataCriacao { get; set; } = DateTime.UtcNow;
        public bool Ativo { get; set; } = true;
        
        [Column("cargo_id")]
        public int CargoId { get; set; }

        [ForeignKey("CargoId")]
        public Cargo Cargo { get; set; }
    }
}