namespace BookTest.API.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : Controller
    {
        private readonly IDbService _db;

        public UserController(IDbService db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserBaseDTO>>> GetAllUsers()
        {
            try
            {
                var users = await _db.GetAsync<User, UserBaseDTO>();
                return Ok(users);
            }
            catch (Exception)
            {
                return StatusCode(500, "An internal error occurred while retrieving users.");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserBaseDTO>> GetUser(int id)
        {
            try
            {
                var user = await _db.SingleAsync<User, UserBaseDTO>(u => u.Id == id);
                if (user == null)
                {
                    return NotFound("User not found.");
                }
                return Ok(user);
            }
            catch (Exception)
            {
                return StatusCode(500, "An internal error occurred while retrieving the user.");
            }
        }

        [HttpPost]
        public async Task<ActionResult<User>> CreateUser(UserBaseDTO createUserDto)
        {
            try
            {
                var newUser = await _db.AddAsync<User, UserBaseDTO>(createUserDto);
                await _db.SaveChangesAsync();

                if (newUser == null)
                {
                    return BadRequest("User could not be created.");
                }

                return CreatedAtAction(nameof(GetUser), new { id = newUser.Id }, newUser);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while creating the user.");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, UserBaseDTO updateUserDto)
        {
            try
            {
                if (id != updateUserDto.Id)
                {
                    return BadRequest("User ID mismatch.");
                }

                _db.Update<User, UserBaseDTO>(updateUserDto, id);
                await _db.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while updating the user.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                bool deleted = await _db.DeleteAsync<User>(id);
                if (!deleted)
                {
                    return NotFound("User not found.");
                }

                await _db.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while deleting the user.");
            }
        }
    }
}
