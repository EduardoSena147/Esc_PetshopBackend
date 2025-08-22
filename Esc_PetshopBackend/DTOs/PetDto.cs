using System;
using System.ComponentModel.DataAnnotations;

namespace Esc_PetshopBackend.DTOs
{
    public class PetDto
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public int TipoAnimalId { get; set; }
        public string Nome { get; set; }
        public string Raca { get; set; }
        public string Cor { get; set; }
        public string Sexo { get; set; }
        public DateTime? DataNascimento { get; set; }
        public decimal? Peso { get; set; }
        public string Porte { get; set; }
        public bool Castrado { get; set; }
        public string Observacoes { get; set; }
        public DateTime DataCadastro { get; set; }

        // Propriedades de navegação para exibição
        public string ClienteNome { get; set; }
        public string TipoAnimalDescricao { get; set; }
    }

    public class PetCreateDto
    {
        [Required]
        public int ClienteId { get; set; }

        [Required]
        public int TipoAnimalId { get; set; }

        [Required]
        [StringLength(100)]
        public string Nome { get; set; }

        [StringLength(100)]
        public string Raca { get; set; }

        [StringLength(50)]
        public string Cor { get; set; }

        [StringLength(1)]
        public string Sexo { get; set; }

        public DateTime? DataNascimento { get; set; }

        public decimal? Peso { get; set; }

        [StringLength(20)]
        public string Porte { get; set; }

        public bool Castrado { get; set; }

        public string Observacoes { get; set; }
    }

    public class PetUpdateDto
    {
        [StringLength(100)]
        public string Nome { get; set; }

        [StringLength(100)]
        public string Raca { get; set; }

        [StringLength(50)]
        public string Cor { get; set; }

        [StringLength(1)]
        [RegularExpression("^[MF]$", ErrorMessage = "Sexo deve ser 'M' ou 'F'")]
        public string Sexo { get; set; } // ← Garanta que seja "M" ou "F" maiúsculo

        public DateTime? DataNascimento { get; set; }

        public decimal? Peso { get; set; }

        [StringLength(20)]
        public string Porte { get; set; }

        public bool Castrado { get; set; }

        public string Observacoes { get; set; }
    }
}