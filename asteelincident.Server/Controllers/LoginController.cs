using asteelincident.Server.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace asteelincident.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly AuthService _authService;

        // Le nom du constructeur doit correspondre à celui de la classe
        public LoginController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] UserLoginRequest request)
        {
            if (string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.Password))
                return BadRequest(new { message = "Nom d'utilisateur et mot de passe requis" });

            var user = _authService.Authenticate(request.Username, request.Password);

            if (user == null)
                return Unauthorized(new { message = "Nom d'utilisateur ou mot de passe incorrect" });

            return Ok(new { message = "Connexion réussie", userId = user.UserID });
        }

        public class UserLoginRequest
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }
    }
}
