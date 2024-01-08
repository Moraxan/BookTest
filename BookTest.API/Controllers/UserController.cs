

namespace BookTest.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IDbService _db;

        public UserController(IDbService db)
        {
            _db = db;
        }

        // GET: user
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var users = await _db.GetAsync<User, UserReadDTO>(); // Use UserReadDTO
                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: user/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            try
            {
                var user = await _db.SingleAsync<User, UserReadDTO>(u => u.Id == id); // Use UserReadDTO
                if (user == null)
                {
                    return NotFound();
                }

                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: user
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserDTO userDto) // Use UserDTO
        {
            try
            {
                if (userDto == null)
                {
                    return BadRequest("User data is null.");
                }

                // Check if user already exists
                bool userExists = await _db.AnyAsync<User>(u => u.Username == userDto.Username);
                if (userExists)
                {
                    return BadRequest("User already exists.");
                }

                var createdUser = await _db.AddAsync<User, UserDTO>(userDto); // Use UserDTO
                await _db.SaveChangesAsync();
                return CreatedAtAction(nameof(GetUserById), new { id = createdUser.Id }, createdUser);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT: user/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UserReadDTO userDto) // Use UserDTO
        {
            try
            {
                if (id != userDto.Id)
                {
                    return BadRequest("ID mismatch");
                }

                _db.Update<User, UserDTO>(userDto, id); // Use UserDTO
                await _db.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE: user/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                var deleted = await _db.DeleteAsync<User>(id);
                if (!deleted)
                {
                    return NotFound();
                }

                await _db.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
