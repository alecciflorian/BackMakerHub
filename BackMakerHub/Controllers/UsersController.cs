using BackMakerHub.DbConnection;
using BackMakerHub.DTO_s;
using BackMakerHub.Models;
using BackMakerHub.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace BackMakerHub.Controllers
{
    [Route("users")]
    public class UsersController : ControllerBase
    {
        private readonly userService _userService;
        public UsersController(userService userService)
        {
            _userService = userService;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<UserDTO>> GetUser()
        {
            //Récupérer le token de l'utilisateur
            var usrId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            //si l'utilisateur n'a pas de token on bloque l'accès
            if (string.IsNullOrEmpty(usrId))
            {
                return Unauthorized();
            }

            var user = await _userService.GettUserById(int.Parse(usrId));
            var userDTO = new UserDTO
            {
                UserName = user.UserName,
                Email = user.Email,
                Password = user.Password
            };

            //Si l'utilisateur à un token alors on le transforme en Id
            //On autorise l'accès
            return Ok(userDTO);
        }
        [HttpPost("register-user")]

        public async Task<IActionResult> RegisterUser([FromBody]AddUserDTO u)
        {
            var count = await _userService.GetUserCount();

            if(count >= 2)
            {
                throw new Exception("Impossible d'ajouter un utilisateur, veuillez contacter l'administrateur de la plateforme");
            }
            string role = (count == 0) ? "admin" : "user";

            if(!ModelState.IsValid) return BadRequest();

            var newUser = await _userService.AddUser(u, role);

            return Ok("Utilisateur enregistré !");
        }

        [HttpPost("/login")]
        public async Task<ActionResult> Login([FromBody] LoginDTO loginDto)
        {
            var user = await _userService.Authenticate(loginDto.UserName, loginDto.Password);
            if(user == null)
            {
                return Unauthorized("Utilisateur introuvable. Veuillez vérifier votre identifiant/mot de passe");
            }
            return Ok(user);
        }
    }
}
