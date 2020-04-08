//using SneakerShop.Models;
using SneakerShop.Models;
using SneakerShop.NoSQLModels.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace SneakerShop.NoSQLModels.Data
{
    public class NoSQLSeeder
    {
        private readonly IOrderItemNoSQLRepo orderItemNoSQLRepo;
        private readonly IOrderNoSQLRepo orderNoSQLRepo;
        private readonly OrdersDBContext orderItemDBContext;
        public List<Guid> Lst_ProductGuids { get; set; } = new List<Guid>(); //init !!
        public NoSQLSeeder(IOrderItemNoSQLRepo orderItemNoSQLRepo, IOrderNoSQLRepo orderNoSQLRepo, OrdersDBContext orderItemDBContext)
        {
            this.orderItemNoSQLRepo = orderItemNoSQLRepo;
            this.orderNoSQLRepo = orderNoSQLRepo;
            this.orderItemDBContext = orderItemDBContext;
        }
        private static int RandomNumBetween(int smallNum, int bigNum)
        {
            Random rnd = new Random();
            int randomValue = rnd.Next(smallNum, bigNum + 1);
            return randomValue;
        }
        public void InitDatabase(IEnumerable<OrderNoSQL> orders)
        {
            try
            {
                //if (!orderItemDBContext.CollectionExistsAsync("OrderItems").Result)
                //{
                //foreach (Order o in orders)
                //{
                //    orderNoSQLRepo.CreateAsync(new OrderNoSQL
                //    {
                //        Timestamp = o.Timestamp,
                //        UserID = o.UserID
                //    });
                //}
                //foreach (OrderItem oi in orderItems)
                //{
                //    orderItemNoSQLRepo.CreateAsync(new OrderItemNoSQL
                //    {
                //        OrderID = oi.OrderID,
                //        ProductID = oi.ProductID,
                //        Size = oi.Size
                //    });
                //}
                
                foreach (OrderNoSQL o in orders)
                {
                    int nmbr = RandomNumBetween(1, 4);
                    for (var i = 0; i < nmbr; i++)
                    {
                        var prod = Lst_ProductGuids[new Random().Next(Lst_ProductGuids.Count - 1)];
                        //check if exist
                        var orderitem = orderItemNoSQLRepo.Get(o.OrderId,prod);
                        
                        if (orderitem != null)
                        {
                            orderItemNoSQLRepo.CreateAsync(new OrderItemNoSQL
                            {
                                OrderID = o.OrderId,
                                ProductID = prod,
                                Size = RandomNumBetween(35, 47)

                            });
                        }
                    }

                }
                //}
            }
            catch (Exception exc)
            {
                Console.WriteLine("fout bij het seeden:", exc);
            }
        }
    }
}
