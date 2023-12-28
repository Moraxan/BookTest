namespace BookTest.API.Controllers
{
    [ApiController]
    [Route("api/Books")]
    public class BookController : Controller
    {
        private readonly IDbService _db;

        public BookController(IDbService db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookReadDTO>>> GetAllBooks()
        {
            try
            {
                return await _db.GetAsync<Book, BookReadDTO>();
            }
            catch (Exception)
            {
                return StatusCode(500, "An internal error occurred while retrieving Books.");
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<BookDTO>> GetSingleBook(int id)
        {
            try
            {
                var Book = await _db.SingleAsync<Book, BookDTO>(b => b.Id == id);
                if (Book == null)
                {
                    return NotFound();
                }
                return Book;
            }
            catch (Exception)
            {
                return StatusCode(500, "An internal error occurred while retrieving the Book.");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Book>> CreateBook(BookDTO createBookDto)
        {
            try
            {
                // Check if Book already exists
                bool bookExists = await _db.AnyAsync<Book>(a => a.Title == createBookDto.Title);
                if (bookExists)
                {
                    return BadRequest("Book already exists.");
                }

                // Add new Book
                var bookEntity = await _db.AddAsync<Book, BookDTO>(createBookDto);
                await _db.SaveChangesAsync();

                if (bookEntity == null)
                {
                    return BadRequest("Book could not be created.");
                }

                return Ok(bookEntity); // Returns a 200 OK response with the Book entity
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while creating the Book.");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(int id, [FromBody] BookReadDTO updateBookDto)
        {
            try
            {
                if (id != updateBookDto.Id)
                {
                    return BadRequest("Book ID mismatch.");
                }

                _db.Update<Book, BookReadDTO>(updateBookDto, id);
                await _db.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while updating the Book. Please try again later.");
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            try
            {
                await _db.DeleteAsync<Book>(id);
                await _db.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while deleting the Book. Please try again later.");
            }
        }



    }
}
