

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
        public async Task<ActionResult<IEnumerable<BookDTO>>> GetAllBooks()
        {
            try
            {
                return await _db.GetAsync<Book, BookDTO>();
            
            }
            catch (Exception)
            {

                return StatusCode(500, "An internal error occurred while retrieving books.");
            }
        }
            
      
        [HttpGet("{id}")]
            public async Task<ActionResult<BookDTO>> GetSingleBook(int id)
        {
            try
            {
                var book = await _db.SingleAsync<Book, BookDTO>(b => b.Id == id);
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
            public async Task<IActionResult> PutBook(int id, BookDTO book)
        {
            try
            {
                await _db.UpdateAsync<Book, BookDTO>(book);
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while updating the book. Please try again later.");
            }
            
            
        }

        [HttpPost]
            public async Task<ActionResult<BookDTO>> PostBook(BookDTO book)
        {
            try
            {
                return await _db.AddAsync<Book, BookDTO>(book);
            }
            catch (Exception)
            {

                return StatusCode(500, "An error occurred while processing your request. Please try again later.")
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

}
