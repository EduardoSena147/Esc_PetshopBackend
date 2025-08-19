namespace Esc_PetshopBackend.DTOs
{
    public class CargoDto
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
    }

    public class CargoCreateDto
    {
        public string Descricao { get; set; }
    }

    public class CargoUpdateDto
    {
        public string Descricao { get; set; }
    }
}
