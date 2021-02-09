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
    [Route("api/product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IlibraryProduct _product;

        public ProductController(IlibraryProduct product)
        {
            _product = product;
        }
        // GET: api/Product
        [HttpGet]
        public ActionResult <IEnumerable<Product>> GetAllProduct()
        {
            var products = _product.GetAllProduct();
            if (products == null) return NotFound();
            if (products.Count() == 0) return NoContent();
            return Ok(products);
        }

        // GET: api/Product/5
        [HttpGet("{id}", Name = "GetProduct")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Product
        [HttpPost]
        public ActionResult Post([FromBody] Product product)
        {
            if (_product.AddProduct(product)) return Ok(product);
            return NotFound();
        }

        // PUT: api/Product/5
        [HttpPut("{id}")]
        public ActionResult PutProduct(int id, [FromBody]Product product)
        {
            if (_product.UpdateProduct(id, product)) return Ok(product);
            return NotFound();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public ActionResult DeleteProduct(int id)
        {
            if (_product.DeleteProduct(id)) return Ok();
            return NotFound();
        }
    }
}
