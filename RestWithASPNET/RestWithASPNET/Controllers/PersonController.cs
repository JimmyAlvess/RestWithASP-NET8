using Asp.Versioning;
using RestWithASPNETErudio.Business;
using Microsoft.AspNetCore.Mvc;
using RestWithASPNET.Hypermedia.Filters;
using RestWithASPNETErudio.Data.VO;

namespace RestWithASPNETErudio.Controllers
{

    [ApiVersion("1")]
    [ApiController]
    [Route("api/[controller]/v{version:apiVersion}")]
    public class PersonController : ControllerBase
    {
        private readonly ILogger<PersonController> _logger;

        private IPersonBusiness _personBusiness;

        public PersonController(ILogger<PersonController> logger, IPersonBusiness personBusiness)
        {
            _logger = logger;
            _personBusiness = personBusiness;
        }

        [HttpGet]
        [TypeFilter(typeof(HyperMidaFilter))]
        public IActionResult Get()
        {
            return Ok(_personBusiness.FindAll());
        }
        [HttpGet("{id}")]
        [TypeFilter(typeof(HyperMidaFilter))]
        public IActionResult Get(int id)
        {
            var PersonVO = _personBusiness.FindByID(id);
            if (PersonVO == null) return NotFound();
            return Ok(PersonVO);
        }

        [HttpPost]
        [TypeFilter(typeof(HyperMidaFilter))]
        public IActionResult Post([FromBody] PersonVO person)
        {
            if (person == null) return BadRequest();
            return Ok(_personBusiness.Create(person));
        }

        [HttpPut]
        [TypeFilter(typeof(HyperMidaFilter))]
        public IActionResult Put([FromBody] PersonVO person)
        {
            if (person == null) return BadRequest();
            return Ok(_personBusiness.Update(person));
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _personBusiness.Delete(id);
            return NoContent();
        }
    }
}
