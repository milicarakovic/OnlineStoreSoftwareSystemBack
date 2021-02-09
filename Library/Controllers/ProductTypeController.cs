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
    [Route("api/producttype")]
    [ApiController]
    public class ProductTypeController : ControllerBase
    {


        private IlibraryProductType _productType;

        public ProductTypeController(IlibraryProductType productType)
        {
            _productType = productType;
        }
        // GET: api/ProductType
        [HttpGet]
        public ActionResult<IEnumerable<ProductType>> GetAllProductType()
        {
            var productTypes = _productType.GetAllProductType();
            if (productTypes == null) return NotFound();
            if (productTypes.Count() == 0) return NoContent();
            return Ok(productTypes);
        }

        // GET: api/ProductType/5
        [HttpGet("{id}", Name = "GetProductType")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/ProductType
        [HttpPost]
        public ActionResult Post([FromBody] ProductType type)
        {
            if (_productType.AddProductType(type)) return Ok(type);
            return NotFound();
        }

        // PUT: api/ProductType/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
