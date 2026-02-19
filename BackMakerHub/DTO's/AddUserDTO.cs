using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BackMakerHub.DTO_s
{
    public class AddUserDTO
    {
        [JsonPropertyName("username")]
        [Required(ErrorMessage = "Le nom d'utilisateur est obligatoire")]
        public string UserName { get; set; } = null!;
        [JsonPropertyName("password")]
        [Required(ErrorMessage = "Veuillez entrer un mot de passe")]
        public string Password { get; set; } = null!;
        [Compare("Password", ErrorMessage="Veuillez confirmer votre mot de passe")]
        [JsonPropertyName("password_confirmation")]
        public string Password_confirmation { get; set; } = null!;
        [Required(ErrorMessage = "Veuillez entrer une adresse mail valide")]
        [JsonPropertyName("email")]
        public string Email { get; set; } = null!;
    }
}
