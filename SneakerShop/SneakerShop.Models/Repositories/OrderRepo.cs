using Microsoft.EntityFrameworkCore;
using SneakerShop.Models.Data;
//using SneakerShop.NoSQLModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SneakerShop.Models.Repositories
{
    public class OrderRepo : GenericRepo<Order>, IOrderRepo
    {
        public OrderRepo(SneakerShopContext context) : base(context)
        {

        }
        public async new Task<IEnumerable<Order>> GetAllAsync()
        {
            try
            {
                IEnumerable<Order> result = await _context.Order

                    .Include(o => o.Products)
                        .ThenInclude(oi => oi.Product)
                        .ThenInclude(p => p.Supplier)

                    .Include(o => o.User)
                    .ToListAsync();

                return result.OrderByDescending(o=> o.Timestamp);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<OrderItem>> GetAllOrderItemsAsync()
        {
            try
            {
                IEnumerable<OrderItem> result = await _context.OrderItem.ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Order GetOrder(Guid OrderId)
        {
            try
            {
                Order result = _context.Order.Where(o => o.OrderId == OrderId)
                    .Include(o => o.Products)
                        .ThenInclude(oi => oi.Product)
                    .Include(o => o.User).First();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IEnumerable<Order> GetOrderForUser(string userId)
        {
            try
            {
                IEnumerable<Order> result = _context.Order.Where(o => o.UserID == userId)
                    .Include(o => o.Products)
                    .Include(o => o.User);

                return result.OrderByDescending(o=>o.Timestamp);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public async new Task<Order> Create(Order o)
        {
            await _context.Order.AddAsync(o);
            await _context.SaveChangesAsync();
            return o;
        }
        public async Task<OrderItem> CreateOrderItem(OrderItem oi)
        {
            await _context.OrderItem.AddAsync(oi);
            await _context.SaveChangesAsync();
            return oi;
        }
        

        public async Task<ILookup<Guid, OrderItem>> GetOrderItems(IEnumerable<Guid> orderIDGuids)
        {
            var orderItems = await _context.OrderItem.Where(
                oi => orderIDGuids.Contains(oi.OrderID)).ToListAsync();
            return orderItems.ToLookup(oi => oi.OrderID);
        }
    }
}
