using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SneakerShop.API.Models;
using SneakerShop.Models;
using SneakerShop.Models.Data;
using SneakerShop.Models.Repositories;

namespace SneakerShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepo productRepo;
        private readonly IMapper mapper;

        public ProductsController(IProductRepo productRepo, IMapper mapper)
        {
            this.productRepo = productRepo; 
            this.mapper = mapper;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProduct()
        {
            var products =  await productRepo.GetAllAsync();
            return Ok(mapper.Map<IEnumerable<ProductDTO>>(products));
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(Guid id)
        {
            var product = (await productRepo.GetByExpressionAsync(p => p.ProductId == id)).First();

            if (product == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<ProductDTO>(product));
        }
        // Getbybrand
        [HttpGet("{supplierId}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductsByBrand(Guid supplierId)
        {
            var products = await productRepo.GetByExpressionAsync(p=>p.SupplierId == supplierId);
            return Ok(mapper.Map<IEnumerable<ProductDTO>>(products));
        }

        // PUT: api/Products/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(Guid id, Product product)
        {
            if (id != product.ProductId)
            {
                return BadRequest();
            }

             productRepo.Update(product);

            try
            {
                await productRepo.SaveAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Products
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Order>> PostProduct([Bind("ProductName", "UnitPrice", "SupplierId")] [FromBody]Product product)
        {
            if (product == null)
            {
                return BadRequest("Unsufficient data");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest("Unvalid");
            }

            await productRepo.Create(product);
            try
            {
                await productRepo.SaveAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Creating the item {product.ProductName} did not succeed. Exception: {ex}");
            }

            return Ok(product);
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Product>> DeleteProduct(Guid id)
        {
            var product = (await productRepo.GetByExpressionAsync(p => p.ProductId == id)).First();
            if (product == null)
            {
                return NotFound();
            }

            await productRepo.Delete(product);
            await productRepo.SaveAsync();

            return product;
        }

        private bool ProductExists(Guid id)
        {
            var product = (productRepo.GetByExpressionAsync(p => p.ProductId == id).GetAwaiter().GetResult()).First();
            if (product == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
