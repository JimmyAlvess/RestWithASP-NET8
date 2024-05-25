using Microsoft.AspNetCore.Mvc;
using Asp.Versioning;
using RestWithASPNET.Data.VO;
using RestWithASPNET.Hypermedia.Filters;
using Microsoft.AspNetCore.Authorization;
using RestWithASPNET.Business;

namespace RestWithASPNET.Controllers
{

    [ApiVersion("1")]
    [ApiController]
    [Authorize("Bearer")]
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
        [ProducesResponseType((200), Type = typeof(List<PersonVO>))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [TypeFilter(typeof(HyperMidaFilter))]
        public IActionResult Get()
        {
            return Ok(_bookBusiness.FindAll());
        }

        [HttpGet("{id}")]
        [ProducesResponseType((200), Type = typeof(PersonVO))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Get(int id)
        {
            var BookVO = _bookBusiness.FindByID(id);
            if (BookVO == null) return NotFound();
            return Ok(BookVO);
        }

        [HttpPost]
        [TypeFilter(typeof(HyperMidaFilter))]
        [ProducesResponseType((200), Type = typeof(PersonVO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Post([FromBody] BookVO BookVO)
        {
            if (BookVO == null) return BadRequest();
            return Ok(_bookBusiness.Create(BookVO));
        }

        [HttpPut]
        [ProducesResponseType((200), Type = typeof(PersonVO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [TypeFilter(typeof(HyperMidaFilter))]
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
