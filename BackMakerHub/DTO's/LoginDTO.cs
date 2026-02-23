using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BackMakerHub.DTO_s
{
    public class LoginDTO
    {
        [Required(ErrorMessage =("Mail ou Nom d'utilisateur incorrect"))]
        [JsonPropertyName("email")]
        public string Email { get; set; }
        [Required(ErrorMessage=("Mail ou Nom d'utilisateur incorrect"))]
        [JsonPropertyName("username")]
        public string UserName { get; set; }

        [Required(ErrorMessage =("Mot de passe incorrect"))]
        [JsonPropertyName("password")]
        public string Password { get; set; }
    }
}
