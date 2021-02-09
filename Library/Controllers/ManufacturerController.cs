using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryData.Interface;
using LibraryData.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    [Route("api/manufacturer")]
    [ApiController]
    public class ManufacturerController : ControllerBase
    {
        private IlibraryManufacturer _manufacturer;

        public ManufacturerController(IlibraryManufacturer manufacturer)
        {
            _manufacturer = manufacturer;
        }

        // GET: api/Manufacturer
        [HttpGet]
        public ActionResult<IEnumerable<Manufacturer>> GetAllManufacturer()
        {
            var Manufacturers = _manufacturer.GetAllManufacturer();
            if (Manufacturers == null) return NotFound();
            if (Manufacturers.Count() == 0) return NoContent();
            return Ok(Manufacturers);
        }

        // GET: api/Manufacturer/5
        [HttpGet("{id}", Name = "GetManufacturer")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Manufacturer
        [HttpPost]
        public ActionResult Post([FromBody] Manufacturer manufacturer)
        {
            if (_manufacturer.AddManufacturer(manufacturer)) return Ok(manufacturer);
            return NotFound();
        }

        // PUT: api/Manufacturer/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
           
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            if (_manufacturer.DeleteManufacturer(id)) return Ok();
            return NotFound();
        }
    }
}
