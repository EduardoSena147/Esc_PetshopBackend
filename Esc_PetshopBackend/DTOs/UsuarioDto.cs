namespace Esc_PetshopBackend.DTOs
{
    public class UsuarioDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public DateTime DataCriacao { get; set; } // Será retornado em UTC
        public int CargoId { get; set; }
        public bool Ativo { get; set; }
    }

    public class UsuarioCreateDto
    {
        public string Username { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public int CargoId { get; set; }
    }

    public class UsuarioUpdateDto
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public bool? Ativo { get; set; } // Nullable para atualizações parciais
        public int CargoId { get; set; }
    }
}