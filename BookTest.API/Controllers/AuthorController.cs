namespace AuthorTest.API.Controllers
{
    [ApiController]
    [Route("api/authors")]
    public class AuthorController : Controller
    {
        private readonly IDbService _db;

        public AuthorController(IDbService db)
        {
            _db = db;
        }

        //help me write the cruds for this controller using the db service
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuthorReadDTO>>> GetAllAuthors()
        {
            try
            {
                return await _db.GetAsync<Author, AuthorReadDTO>();
            }
            catch (Exception)
            {
                return StatusCode(500, "An internal error occurred while retrieving authors.");
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<AuthorDTO>> GetSingleAuthor(int id)
        {
            try
            {
                var author = await _db.SingleAsync<Author, AuthorDTO>(b => b.Id == id);
                if (author == null)
                {
                    return NotFound();
                }
                return author;
            }
            catch (Exception)
            {
                return StatusCode(500, "An internal error occurred while retrieving the author.");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Author>> CreateAuthor(AuthorDTO createAuthorDto)
        {
            try
            {
                // Check if author already exists
                bool authorExists = await _db.AnyAsync<Author>(a => a.Name == createAuthorDto.Name);
                if (authorExists)
                {
                    return BadRequest("Author already exists.");
                }

                // Add new author
                var authorEntity = await _db.AddAsync<Author, AuthorDTO>(createAuthorDto);
                await _db.SaveChangesAsync();

                if (authorEntity == null)
                {
                    return BadRequest("Author could not be created.");
                }

                return Ok(authorEntity); // Returns a 200 OK response with the Author entity
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while creating the author.");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAuthor(int id, AuthorReadDTO updateAuthorDto)
        {
            try
            {
                if (id != updateAuthorDto.Id)
                {
                    return BadRequest("Author ID mismatch.");
                }

                _db.Update<Author, AuthorReadDTO>(updateAuthorDto, id);
                await _db.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while updating the author. Please try again later.");
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            try
            {
                await _db.DeleteAsync<Author>(id);
                await _db.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while deleting the author. Please try again later.");
            }
        }



    }
}
