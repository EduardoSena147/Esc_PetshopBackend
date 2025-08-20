namespace Esc_PetshopBackend.DTOs
{
    public class TipoAnimalDto
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
    }

    public class TipoAnimalCreateDto
    {
        public string Descricao { get; set; }
    }

    public class TipoAnimalUpdateDto
    {
        public string Descricao { get; set; }
    }
}
