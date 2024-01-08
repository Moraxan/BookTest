namespace BookTest.API.Controllers
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

                // Optional: Add books if provided
                if (createAuthorDto.BookIds != null)
                {
                    foreach (var bookId in createAuthorDto.BookIds)
                    {
                        var authorBookDto = new AuthorBookDTO { AuthorId = authorEntity.Id, BookId = bookId };
                        await _db.AddReferenceAsync<AuthorBook, AuthorBookDTO>(authorBookDto);
                    }
                    await _db.SaveChangesAsync();
                }

                return Ok(authorEntity); // Returns a 200 OK response with the Author entity
            }
            catch (Exception)
            {
                // Log the exception for debugging
                // LogError(ex, "Error occurred while creating the author.");

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

                var author = await _db.SingleAsync<Author, AuthorDTO>(a => a.Id == id);
                if (author == null)
                {
                    return NotFound();
                }

                // Update author properties here, e.g., author.Name = updateAuthorDto.Name;

                // Handle books
                // Delete existing relationships for this author
                await _db.DeleteByCompositeKey<AuthorBook>(ab => ab.AuthorId == id);

                // Add new relationships
                foreach (var bookId in updateAuthorDto.BookIds)
                {
                    var authorBookDto = new AuthorBookDTO { AuthorId = id, BookId = bookId };
                    await _db.AddReferenceAsync<AuthorBook, AuthorBookDTO>(authorBookDto);
                }

                await _db.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception)
            {
                // Log the exception details for debugging purposes
                // LogError(ex, "Error occurred in PutAuthor");

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
