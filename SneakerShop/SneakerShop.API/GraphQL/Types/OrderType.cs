using GraphQL.Authorization;
using GraphQL.DataLoader;
using GraphQL.Types;
using SneakerShop.Models;
using SneakerShop.Models.Data;
using SneakerShop.Models.Repositories;
//using SneakerShop.NoSQLModels;
//using SneakerShop.NoSQLModels.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SneakerShop.API.GraphQL.Types
{
    public class OrderType : ObjectGraphType<Order>
    {
        public OrderType(SneakerShopContext data, IOrderRepo /*, IOrderItemNoSQLRepo*/ orderRepo, IDataLoaderContextAccessor dataLoaderContextAccessor)
        {

            Field(o => o.OrderId, type: typeof(IdGraphType)).Description("The id of the Order.");
            Field(o => o.UserID, type: typeof(IdGraphType)).Description("The id of the User.");
            Field(o => o.TotalPrice, nullable: false).Description("The total price of the Order.");
            Field(o => o.Timestamp, type: typeof(DateTimeGraphType)).Description("The datetime of the Order.");
            Field(
                name: "User",
                type: typeof(UserType),
                resolve: context => context.Source.User
            );

            Field<ListGraphType<OrderItemType>>(nameof(Order.Products),

                resolve: context => /*orderRepo.GetAllForOrderID(context.Source.OrderId).GetAwaiter().GetResult());*/
            {
                var loader = dataLoaderContextAccessor.Context.GetOrAddCollectionBatchLoader<Guid, OrderItem>(
                    "GetOrderItemsByOrderId", orderRepo.GetOrderItems);
                return loader.LoadAsync(context.Source.OrderId);

            });
        }
    }
}
