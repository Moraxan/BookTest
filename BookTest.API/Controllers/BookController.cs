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
                // Check if book already exists
                bool bookExists = await _db.AnyAsync<Book>(b => b.Title == createBookDto.Title);
                if (bookExists)
                {
                    return BadRequest("Book already exists.");
                }

                // Add new book
                var bookEntity = await _db.AddAsync<Book, BookDTO>(createBookDto);
                await _db.SaveChangesAsync();

                if (bookEntity == null)
                {
                    return BadRequest("Book could not be created.");
                }

                // Optional: Add authors if provided
                if (createBookDto.AuthorIds != null)
                {
                    foreach (var authorId in createBookDto.AuthorIds)
                    {
                        var authorBookDto = new AuthorBookDTO { BookId = bookEntity.Id, AuthorId = authorId };
                        await _db.AddReferenceAsync<AuthorBook, AuthorBookDTO>(authorBookDto);
                    }
                    await _db.SaveChangesAsync();
                }

                return Ok(bookEntity);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while creating the book.");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutBook(int id, BookReadDTO updateBookDto)
        {
            try
            {
                if (id != updateBookDto.Id)
                {
                    return BadRequest("Book ID mismatch.");
                }

                var book = await _db.SingleAsync<Book, BookDTO>(b => b.Id == id);
                if (book == null)
                {
                    return NotFound();
                }

                // Update book properties here, e.g., book.Title = updateBookDto.Title;

                // Handle authors
                // Delete existing relationships for this book
                await _db.DeleteByCompositeKey<AuthorBook>(ba => ba.BookId == id);

                // Add new relationships
                foreach (var authorId in updateBookDto.AuthorIds)
                {
                    var authorBookDto = new AuthorBookDTO { BookId = id, AuthorId = authorId };
                    await _db.AddReferenceAsync<AuthorBook, AuthorBookDTO>(authorBookDto);
                }

                await _db.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while updating the book.");
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
