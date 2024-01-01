namespace BookTest.API.Controllers
{
    [ApiController]
    [Route("api/quotations")]
    public class QuotationController : Controller
    {
        private readonly IDbService _db;

        public QuotationController(IDbService db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<QuotationBaseDTO>>> GetAllQuotations()
        {
            try
            {
                var quotations = await _db.GetAsync<Quotation, QuotationBaseDTO>();
                return Ok(quotations);
            }
            catch (Exception)
            {
                return StatusCode(500, "An internal error occurred while retrieving quotations.");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<QuotationBaseDTO>> GetQuotation(int id)
        {
            try
            {
                var quotation = await _db.SingleAsync<Quotation, QuotationBaseDTO>(q => q.Id == id);
                if (quotation == null)
                {
                    return NotFound("Quotation not found.");
                }
                return Ok(quotation);
            }
            catch (Exception)
            {
                return StatusCode(500, "An internal error occurred while retrieving the quotation.");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Quotation>> CreateQuotation(QuotationBaseDTO createQuotationDto)
        {
            try
            {
                // Set DateAdded to the current local DateTime
                createQuotationDto.DateAdded = DateTime.Now;  // This is local time

                var newQuotation = await _db.AddAsync<Quotation, QuotationBaseDTO>(createQuotationDto);
                await _db.SaveChangesAsync();

                if (newQuotation == null)
                {
                    return BadRequest("Quotation could not be created.");
                }

                return CreatedAtAction(nameof(GetQuotation), new { id = newQuotation.Id }, newQuotation);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while creating the quotation.");
            }
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateQuotation(int id, QuotationBaseDTO updateQuotationDto)
        {
            try
            {
                if (id != updateQuotationDto.Id)
                {
                    return BadRequest("Quotation ID mismatch.");
                }

                _db.Update<Quotation, QuotationBaseDTO>(updateQuotationDto, id);
                await _db.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while updating the quotation.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuotation(int id)
        {
            try
            {
                bool deleted = await _db.DeleteAsync<Quotation>(id);
                if (!deleted)
                {
                    return NotFound("Quotation not found.");
                }

                await _db.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while deleting the quotation.");
            }
        }
    }
}
