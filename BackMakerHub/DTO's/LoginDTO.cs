using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BackMakerHub.DTO_s
{
    public class LoginDTO
    {
        [Required(ErrorMessage=("Nom d'utilisateur invalide"))]
        [JsonPropertyName("username")]
        public string UserName { get; set; }

        [Required(ErrorMessage =("Mot de passe incorrect"))]
        [JsonPropertyName("password")]
        public string Password { get; set; }
    }
}
