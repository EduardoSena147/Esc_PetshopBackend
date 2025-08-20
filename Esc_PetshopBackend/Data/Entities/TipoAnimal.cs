using System.ComponentModel.DataAnnotations;

namespace Esc_PetshopBackend.Data.Entities
{
    public class TipoAnimal
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Descricao { get; set; }
    }
}
