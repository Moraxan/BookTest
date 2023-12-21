

namespace BookTest.API.Controllers
{
    [ApiController]
    [Route("api/quotations")]
    public class QuotationsController : Controller
    {
        private readonly IDbService _db;

        public QuotationsController(IDbService db)
        {
            _db = db;
        }
              
        [HttpGet]
        public async Task<ActionResult<IEnumerable<QuotationDTO>>> GetAllQuotations()
        {
            return await _db.GetAsync<Quotation, QuotationDTO>();
        }
       
        [HttpGet("{id}")]
        public async Task<ActionResult<QuotationDTO>> GetSingleQuotation(int id)
        {
            return await _db.SingleAsync<Quotation, QuotationDTO>(q => q.Id == id);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutQuotation(int id, QuotationDTO quotation)
        {
            if (id != quotation.Id)
            {
                return BadRequest();
            }
            await _db.UpdateAsync<Quotation, QuotationDTO>(quotation);
            return NoContent();
        }
        
        [HttpPost]
        public async Task<ActionResult<QuotationDTO>> PostQuotation(QuotationDTO quotation)
        {
            return await _db.AddAsync<Quotation, QuotationDTO>(quotation);
        }
       
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuotation(int id)
        {
            await _db.DeleteAsync<Quotation>(id);
            return NoContent();
        }



    }

}
