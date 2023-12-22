namespace BookTest.API.Controllers
{
    [ApiController]
    [Route("api/quotations")]
    public class QuotationsController : Controller
    {
        private readonly IDbService _db;
        private readonly IMapper _mapper;

        //Make this controller like Quotation controller
        public QuotationsController(IDbService db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<QuotationBaseDTO>>> GetAllQuotations()
        {
            try
            {
                return await _db.GetAllAsync<Quotation, QuotationBaseDTO>();
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while retrieving quotations.");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<QuotationBaseDTO>> GetSingleQuotation(int id)
        {
            try
            {
                return await _db.GetSingleAsync<Quotation, QuotationBaseDTO>(b => b.Id == id);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while retrieving the quotation.");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Quotation>> CreateQuotation(CreateQuotationDTO createQuotationDto)
        {
            try
            {
                var QuotationEntity = await _db.AddAsync<Quotation, CreateQuotationDTO>(createQuotationDto);
                await _db.SaveChangesAsync();

                if (QuotationEntity == null)
                {
                    return BadRequest("Quotation could not be created.");
                }

                return Ok(QuotationEntity); // Returns a 200 OK response with the Quotation entity
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while creating the quotation.");
            }
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<QuotationBaseDTO>> UpdateQuotation(int id, UpdateQuotationDTO updateQuotationDto)
        {
            try
            {
                // Explicitly specify type arguments for UpdateAsync
                var quotationEntity = await _db.UpdateAsync<Quotation, UpdateQuotationDTO>(updateQuotationDto);

                if (quotationEntity == null)
                {
                    return BadRequest("Quotation could not be updated.");
                }

                // Use AutoMapper to map Quotation entity to QuotationBaseDTO for response
                var updatedQuotationDto = _mapper.Map<QuotationBaseDTO>(quotationEntity);

                return Ok(updatedQuotationDto);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while updating the quotation.");
            }
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteQuotation(int id)
        {
            try
            {
                await _db.DeleteAsync<Quotation>(id);
                await _db.SaveChangesAsync();
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while deleting the quotation.");
            }
        }
    }

        
}
