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
    public class OrdersController : ControllerBase
    {
        private readonly IOrderRepo orderRepo;
        private readonly ISupplierRepo supplierRepo;
        private readonly IProductRepo productRepo;
        private readonly IMapper mapper;

        public OrdersController(IOrderRepo orderRepo, ISupplierRepo supplierRepo , IProductRepo productRepo,IMapper mapper)
        {
            this.orderRepo = orderRepo;
            this.supplierRepo = supplierRepo;
            this.productRepo = productRepo;
            this.mapper = mapper;
        }

        // GET: api/Orders

        [HttpGet]
        [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
        public async Task<ActionResult<IEnumerable<OrderDTO>>> GetOrder()
        {
            var result =  await orderRepo.GetAllAsync();
            
            return Ok((mapper.Map<IEnumerable<OrderDTO>>(result)).ToList());
        }

        // GET: api/Orders
        [HttpGet("userId")]
        public async Task<ActionResult<IEnumerable<OrderDTO>>> GetOrder(string userId)
        {
            var result = await orderRepo.GetByExpressionAsync(o=>o.UserID == userId);

            return Ok((mapper.Map<IEnumerable<OrderDTO>>(result)).ToList());
        }

        // GET: api/Orders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDTO>> GetOrder(Guid id)
        {
            var order = (await orderRepo.GetByExpressionAsync(o => o.OrderId == id)).First();

            if (order == null)
            {
                return NotFound();
            }

            return mapper.Map<OrderDTO>(order);
        }

        // POST: api/Orders
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder([Bind("UserID", "Timestamp")] [FromBody]Order order)
        {
            if (order == null)
            {
                return BadRequest("Unsufficient data");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest("Unvalid");
            }

            await orderRepo.Create(order);
            try
            {
                await orderRepo.SaveAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Creating the item {order.Timestamp} did not succeed. Exception: {ex}");
            }

            return Ok(order);
        }

        // POST: api/Orders
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost("orderitem")]
        public async Task<ActionResult<Order>> PostOrderItem([Bind("orderID", "productID", "size")] [FromBody]OrderItem orderitem)
        {
            if (orderitem == null)
            {
                return BadRequest("Unsufficient data");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest("Unvalid");
            }

            await orderRepo.CreateOrderItem(orderitem);
            try
            {
                await orderRepo.SaveAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Creating the item {orderitem.OrderID} did not succeed. Exception: {ex}");
            }

            return Ok(orderitem);
        }
    }
}
