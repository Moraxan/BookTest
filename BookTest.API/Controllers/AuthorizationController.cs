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
        private readonly IDbService _db;

        public AuthController(IDbService db, IOptions<JwtSettings> jwtSettings)
        {
            _jwtSettings = jwtSettings.Value;
            _db = db;
        }

        
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest request)
        {
            // Validate the user credentials
            var user = await ValidateUser(request.Username, request.Password);
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
        private bool VerifyPassword(string providedPassword, string storedPassword)
        {
            // In a real application, replace this with secure password hashing and comparison
            return providedPassword == storedPassword;
        }

        private async Task<User> ValidateUser(string username, string password)
        {
            // Fetch the user based on the username
            var user = await _db.SingleAsync<User, User>(u => u.Username == username);

            if (user != null)
            {
                // Verify the user's password (make sure to use secure password comparison, e.g., hashed passwords)
                bool isPasswordValid = VerifyPassword(password, user.Password);
                if (isPasswordValid)
                {
                    return user;
                }
            }

            return null;
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
