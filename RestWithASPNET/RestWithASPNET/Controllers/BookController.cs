using Microsoft.AspNetCore.Mvc;
using Asp.Versioning;
using RestWithASPNETErudio.Business;
using RestWithASPNET.Data.VO;

namespace RestWithASPNETErudio.Controllers
{

    [ApiVersion("1")]
    [ApiController]
    [Route("api/[controller]/v{version:apiVersion}")]
    public class BookController : ControllerBase
    {
        private readonly ILogger<BookController> _logger;
        private readonly IBookBusiness _bookBusiness;
        public BookController(ILogger<BookController> logger, IBookBusiness bookBusiness)
        {
            _logger = logger;
            _bookBusiness = bookBusiness;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_bookBusiness.FindAll());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var BookVO = _bookBusiness.FindByID(id);
            if (BookVO == null) return NotFound();
            return Ok(BookVO);
        }

        [HttpPost]
        public IActionResult Post([FromBody] BookVO BookVO)
        {
            if (BookVO == null) return BadRequest();
            return Ok(_bookBusiness.Create(BookVO));
        }

        [HttpPut]
        public IActionResult Put([FromBody] BookVO BookVO)
        {
            if (BookVO == null) return BadRequest();
            return Ok(_bookBusiness.Update(BookVO));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _bookBusiness.Delete(id);
            return NoContent();
        }
    }
}
