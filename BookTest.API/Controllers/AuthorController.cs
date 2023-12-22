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

    }
}
