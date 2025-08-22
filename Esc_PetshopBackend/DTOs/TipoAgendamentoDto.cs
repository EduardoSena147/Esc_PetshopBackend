namespace Esc_PetshopBackend.DTOs
{
    public class TipoAgendamentoDto
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
    }

    public class TipoAgendamentoCreateDto
    {
        public string Descricao { get; set; }
    }

    public class TipoAgendamentoUpdateDto
    {
        public string Descricao { get; set; }
    }
}
