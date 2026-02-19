using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BackMakerHub.DTO_s
{
    public class UserDTO
    {
        [Required(ErrorMessage = "Veuillez rentrer votre nom d'utilisateur")]
        [JsonPropertyName("username")]
        public string UserName { get; set; } = null!;
        [JsonPropertyName("email")]
        [Required(ErrorMessage = "Veuillez rentrer votre adresse mail")]
        public string Email { get; set; } = null!;
        [Required(ErrorMessage = "Veuillez entrer votre mot de passe")]
        [JsonPropertyName("password")]
        public string Password { get; set; } = null!;
        
    }
}