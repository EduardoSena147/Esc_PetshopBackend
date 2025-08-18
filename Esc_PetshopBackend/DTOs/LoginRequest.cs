namespace Esc_PetshopBackend.DTOs
{
    public class LoginRequest
    {
        public string Email { get; set; }
        public string Senha { get; set; }
    }

    public class ChangePasswordRequest
    {
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
    }
}