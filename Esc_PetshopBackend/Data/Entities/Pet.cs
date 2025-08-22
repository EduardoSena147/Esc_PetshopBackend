using Esc_PetshopBackend.Data.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Esc_PetshopBackend.Data.Entities
{
    public class Pet
    {
        public int Id { get; set; }

        [Required]
        public int ClienteId { get; set; }

        [Required]
        public int TipoAnimalId { get; set; }

        [Required]
        [StringLength(100)]
        public string? Nome { get; set; }

        [StringLength(100)]
        public string? Raca { get; set; }

        [StringLength(50)]
        public string? Cor { get; set; }

        [StringLength(1)]
        public string? Sexo { get; set; } // M ou F

        public DateTime? DataNascimento { get; set; }

        [Column(TypeName = "decimal(5,2)")]
        public decimal? Peso { get; set; }

        [StringLength(20)]
        public string? Porte { get; set; }

        public bool Castrado { get; set; }

        public string? Observacoes { get; set; }

        public DateTime DataCadastro { get; set; }

        public bool Ativo { get; set; }

        // Navegações
        [ForeignKey("ClienteId")]
        public Cliente? Cliente { get; set; }

        [ForeignKey("TipoAnimalId")]
        public TipoAnimal? TipoAnimal { get; set; }
    }
}