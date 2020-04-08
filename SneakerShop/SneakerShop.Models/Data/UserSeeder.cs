using Microsoft.AspNetCore.Identity;
using SneakerShop.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SneakerShop.Models.Data
{
    public class UserSeeder
    {
        private readonly IOrderRepo orderRepo;

        //private readonly IGenericRepo<Order> orderRepo;
        //private readonly IGenericRepo<OrderItem> orderItemRepo;

        public List<Guid> Lst_OrderGuids { get; set; } = new List<Guid>(); //init !!
        public List<Guid> Lst_ProductGuids { get; set; } = new List<Guid>(); //init !!
        public UserSeeder(IOrderRepo orderRepo/*, IGenericRepo<OrderItem> orderItemRepo*/)
        {
            this.orderRepo = orderRepo;
            //this.orderRepo = orderRepo;
            //this.orderItemRepo = orderItemRepo;
        }
        private static int RandomNumBetween(int smallNum, int bigNum)
        {
            Random rnd = new Random();
            int randomValue = rnd.Next(smallNum, bigNum + 1);
            return randomValue;
        }
        public async Task SeedData()
        {
           
            try
            {
               
                IEnumerable<Order> orders = await orderRepo.GetAllAsync();
                IEnumerable<OrderItem> orderitems = await orderRepo.GetAllOrderItemsAsync();
                foreach (Order o in orders)
                {
                    decimal calcprice = 0;
                    foreach (OrderItem oi in orderitems)
                    {
                        if(oi.OrderID == o.OrderId)
                        {
                            calcprice += oi.Product.UnitPrice;
                        }
                    }
                    o.TotalPrice = calcprice;
                    await orderRepo.Update(o);
                    await orderRepo.SaveAsync();
                    //int nmbr = RandomNumBetween(1, 4);
                    //for (var i = 0; i < nmbr; i++)
                    //{
                    //    var prod = Lst_ProductGuids[new Random().Next(Lst_ProductGuids.Count - 1)];
                    //    var ord = Lst_OrderGuids[new Random().Next(Lst_OrderGuids.Count - 1)];
                    //    //check if exist
                    //    var orderitem = await orderItemRepo.GetByExpressionAsync(oi => oi.OrderID == o.OrderId && oi.ProductID == prod);
                    //    if (orderitem.Count() == 0)
                    //    {
                    //        await orderItemRepo.Create(new OrderItem
                    //        {
                    //            OrderID = o.OrderId,
                    //            ProductID = prod,
                    //            Size = RandomNumBetween(36,46)
                    //        });
                    //        await orderItemRepo.SaveAsync();
                    //    }
                    //}

                }

                
            }
            catch (Exception exc)
            {
                //await orderItemRepo.SaveAsync();
                Console.WriteLine("fout bij het seeden:", exc);
            }
        }
        //{
        //    //SeedRoles(roleManager);
        //    //SeedUsers(userManager, allcustomers);
        //}

        //public static void SeedUsers(UserManager<User> userManager, IEnumerable<Customer> allcustomers)
        //{
        //    foreach (Customer c in allcustomers)
        //    {
        //        try
        //        {
        //            User user = new User();

        //            user.FirstName = c.FirstName;
        //            user.LastName = c.LastName;
        //            user.Email = c.FirstName + "." + c.LastName + "@hotmail.com";
        //            user.UserName = c.FirstName + "." + c.LastName + "@hotmail.com";
        //            user.PhoneNumber = c.Phone;
        //            user.City = c.City;
        //            user.Country = c.Country;
        //            user.EmailConfirmed = true;

        //            IdentityResult result = userManager.CreateAsync(user, "-Azerty2019").Result;

        //            if (result.Succeeded)
        //            {
        //                userManager.AddToRoleAsync(user, "User").Wait();
        //            }
        //            else
        //            {
        //                Debug.WriteLine("fail" + result.Errors);
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            throw ex;
        //        }
        //    }
        //}

        public static void SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync("User").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "User";
                IdentityResult roleResult = roleManager.CreateAsync(role).Result;
            }


            if (!roleManager.RoleExistsAsync("Admin").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Administrator";
                IdentityResult roleResult = roleManager.CreateAsync(role).Result;
            }
        }
    }
}
