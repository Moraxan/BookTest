using BookTest.Data.Authentication;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BookTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly JwtSettings _jwtSettings;

        public AuthController(IOptions<JwtSettings> jwtSettings)
        {
            _jwtSettings = jwtSettings.Value;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] UserLoginRequest request)
        {
            // Validate the user credentials
            var user = ValidateUser(request.Username, request.Password);
            if (user == null)
            {
                return Unauthorized();
            }

            var token = GenerateJwtToken(user);

            return Ok(new AuthSuccessResponse
            {
                Token = token,
                RefreshToken = user.RefreshToken // Optional, for implementing refresh token logic
            });
        }

        private string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("id", user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Username)
                    // Add additional claims as needed
                }),
                Expires = DateTime.UtcNow.Add(_jwtSettings.TokenLifetime),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private User ValidateUser(string username, string password)
        {
            // For now, this is a placeholder 
            // In a real application, you would typically validate against a database

            // Example:
            // var user = userRepository.GetUserByUsername(username);
            // if (user != null && VerifyPassword(password, user.Password)) { return user; }

            return null; // Replace with actual validation
        }
    }

    public class UserLoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class AuthSuccessResponse
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; } // Optional, for implementing refresh token logic
    }

}
