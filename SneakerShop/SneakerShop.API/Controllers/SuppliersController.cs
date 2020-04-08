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
    public class SuppliersController : ControllerBase
    {
        private readonly ISupplierRepo supplierRepo;
        private readonly IMapper mapper;

        public SuppliersController(ISupplierRepo supplierRepo, IMapper mapper)
        {
            this.supplierRepo = supplierRepo;
            this.mapper = mapper;
        }

        // GET: api/Suppliers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SupplierDTO>>> GetSupplier()
        {
            var result = await supplierRepo.GetAllAsync();
            return Ok((mapper.Map<IEnumerable<SupplierDTO>>(result)).ToList());
        }

        // GET: api/Suppliers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Supplier>> GetSupplier(Guid id)
        {
            var supplier = (await supplierRepo.GetByExpressionAsync(s=> s.SupplierId == id)).First(); 

            if (supplier == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<IEnumerable<SupplierDTO>>(supplier));
        }

        // PUT: api/Suppliers/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSupplier(Guid id, Supplier supplier)
        {
            if (id != supplier.SupplierId)
            {
                return BadRequest();
            }

            await supplierRepo.Update(supplier);

            try
            {
                await supplierRepo.SaveAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SupplierExists(id))
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

        //// POST: api/Suppliers
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for
        //// more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Order>> PostSupplier([Bind("CompanyName")] [FromBody]Supplier supplier)
        {
            if (supplier == null)
            {
                return BadRequest("Unsufficient data");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest("Unvalid");
            }

            await supplierRepo.Create(supplier);
            try
            {
                await supplierRepo.SaveAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Creating the item {supplier.CompanyName} did not succeed. Exception: {ex}");
            }

            return Ok(supplier);
        }

        // DELETE: api/Suppliers/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Supplier>> DeleteSupplier(Guid id)
        {
            var supplier = (await supplierRepo.GetByExpressionAsync(s => s.SupplierId == id)).First();
            if (supplier == null)
            {
                return NotFound();
            }

            await supplierRepo.Delete(supplier);
            await supplierRepo.SaveAsync();

            return supplier;
        }

        private bool SupplierExists(Guid id)
        {
            var supplier = (supplierRepo.GetByExpressionAsync(s => s.SupplierId == id).GetAwaiter().GetResult()).First();

            if (supplier == null)
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
