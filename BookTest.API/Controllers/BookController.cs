

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
            return await _db.GetAsync<Book, BookDTO>();
        }
      
        [HttpGet("{id}")]
            public async Task<ActionResult<BookDTO>> GetSingleBook(int id)
        {
            return await _db.SingleAsync<Book, BookDTO>(b => b.Id == id);
        }
       
        [HttpPut("{id}")]
            public async Task<IActionResult> PutBook(int id, BookDTO book)
        {
            if (id != book.Id)
            {
                return BadRequest();
            }
            await _db.UpdateAsync<Book, BookDTO>(book);
            return NoContent();
        }

        [HttpPost]
            public async Task<ActionResult<BookDTO>> PostBook(BookDTO book)
        {
            return await _db.AddAsync<Book, BookDTO>(book);
        }
       
        [HttpDelete("{id}")]
            public async Task<IActionResult> DeleteBook(int id)
        {
            await _db.DeleteAsync<Book>(id);
            return NoContent();
        }
            
                
        
        
    }

}
