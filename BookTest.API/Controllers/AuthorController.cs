

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

              
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuthorDTO>>> GetAllAuthors()
        {
            return await _db.GetAsync<Author, AuthorDTO>();
        }
      
        [HttpGet("{id}")]
            public async Task<ActionResult<AuthorDTO>> GetSingleAuthor(int id)
        {
            return await _db.SingleAsync<Author, AuthorDTO>(b => b.Id == id);
        }
       
        [HttpPut("{id}")]
            public async Task<IActionResult> PutAuthor(int id, AuthorDTO Author)
        {
            if (id != Author.Id)
            {
                return BadRequest();
            }
            await _db.UpdateAsync<Author, AuthorDTO>(Author);
            return NoContent();
        }

        [HttpPost]
            public async Task<ActionResult<AuthorDTO>> PostAuthor(AuthorDTO Author)
        {
            return await _db.AddAsync<Author, AuthorDTO>(Author);
        }
       
        [HttpDelete("{id}")]
            public async Task<IActionResult> DeleteAuthor(int id)
        {
            await _db.DeleteAsync<Author>(id);
            return NoContent();
        }
            
                
        
        
    }

}
