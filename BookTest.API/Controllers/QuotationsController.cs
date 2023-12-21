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
            try
            {
                return await _db.GetAsync<Quotation, QuotationDTO>();
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while retrieving quotations.");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<QuotationDTO>> GetSingleQuotation(int id)
        {
            try
            {
                return await _db.SingleAsync<Quotation, QuotationDTO>(q => q.Id == id);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while retrieving the quotation.");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutQuotation(int id, QuotationDTO quotation)
        {
            if (id != quotation.Id)
            {
                return BadRequest("Quotation ID does not match the ID in the request.");
            }

            try
            {
                await _db.UpdateAsync<Quotation, QuotationDTO>(quotation);
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while updating the quotation.");
            }
        }

        [HttpPost]
        public async Task<ActionResult<QuotationDTO>> PostQuotation(QuotationDTO quotation)
        {
            try
            {
                return await _db.AddAsync<Quotation, QuotationDTO>(quotation);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while adding the quotation.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuotation(int id)
        {
            try
            {
                await _db.DeleteAsync<Quotation>(id);
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while deleting the quotation.");
            }
        }
    }
}
