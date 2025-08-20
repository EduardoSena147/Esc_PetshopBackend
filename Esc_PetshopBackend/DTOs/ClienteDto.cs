using System.ComponentModel.DataAnnotations;

namespace Esc_PetshopBackend.DTOs
{
    public class ClienteDto
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public string Username { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Cpf { get; set; }
        public string Telefone { get; set; }
        public DateTime? DataNascimento { get; set; }
        public string Cep { get; set; }
        public string Endereco { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public bool CadastroPendente { get; set; }
        public DateTime DataCadastro { get; set; }
    }

    public class ClienteCreateDto
    {
        [Required]
        public string Cep { get; set; }

        [Required]
        public string Endereco { get; set; }

        [Required]
        public string Numero { get; set; }

        public string Complemento { get; set; }

        [Required]
        public string Bairro { get; set; }

        [Required]
        public string Cidade { get; set; }

        [Required]
        [StringLength(2, MinimumLength = 2)]
        public string Estado { get; set; }

        [StringLength(14)]
        public string Cpf { get; set; }

        [StringLength(15)]
        public string Telefone { get; set; }

        public DateTime? DataNascimento { get; set; }
    }

    public class ClienteUpdateDto
    {
        public string Cep { get; set; }
        public string Endereco { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string Cpf { get; set; }
        public string Telefone { get; set; }
        public DateTime? DataNascimento { get; set; }
    }
}
