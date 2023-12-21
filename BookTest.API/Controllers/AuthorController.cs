

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
            try
            {
                return await _db.GetAsync<Author, AuthorDTO>();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while retrieving authors.");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AuthorDTO>> GetSingleAuthor(int id)
        {
            try
            {
                return await _db.SingleAsync<Author, AuthorDTO>(b => b.Id == id);
            }
            catch (Exception ex)
            {
                
                return StatusCode(500, "An error occurred while retrieving the author.");
            }
        }

        [HttpPost]
        public async Task<ActionResult<AuthorDTO>> PostAuthor(AuthorDTO Author)
        {
            try
            {
                return await _db.AddAsync<Author, AuthorDTO>(Author);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while adding the author.");
            }
        }




        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            try
            {
                await _db.DeleteAsync<Author>(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while deleting the author.");
            }
        }





    }

}
