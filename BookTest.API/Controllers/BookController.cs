namespace BookTest.API.Controllers
{
    [ApiController]
    [Route("api/books")]
    public class BookController : Controller
    {
        private readonly IDbService _db;

        public BookController(IDbService db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookBaseDTO>>> GetAllBooks()
        {
            try
            {
                return await _db.GetAllAsync<Book, BookBaseDTO>();
            }
            catch (Exception)
            {
                return StatusCode(500, "An internal error occurred while retrieving books.");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BookBaseDTO>> GetSingleBook(int id)
        {
            try
            {
                var book = await _db.GetSingleAsync<Book, BookBaseDTO>(b => b.Id == id);
                if (book == null)
                {
                    return NotFound();
                }
                return book;
            }
            catch (Exception)
            {
                return StatusCode(500, "An internal error occurred while retrieving the book.");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutBook(int id, UpdateBookDTO updateBookDto)
        {
            try
            {
                if (id != updateBookDto.Id)
                {
                    return BadRequest("Book ID mismatch.");
                }

                await _db.UpdateAsync<Book, BookBaseDTO>(updateBookDto);
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while updating the book. Please try again later.");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Book>> CreateBook(CreateBookDTO createBookDto)
        {
            try
            {
                var BookEntity = await _db.AddAsync<Book, CreateBookDTO>(createBookDto);
                await _db.SaveChangesAsync();

                if (BookEntity == null)
                {
                    return BadRequest("Book could not be created.");
                }

                return Ok(BookEntity); // Returns a 200 OK response with the Book entity
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while creating the book.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            try
            {
                await _db.DeleteAsync<Book>(id);
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while attempting to delete the book. Please try again later.");
            }
        }
    }
}
