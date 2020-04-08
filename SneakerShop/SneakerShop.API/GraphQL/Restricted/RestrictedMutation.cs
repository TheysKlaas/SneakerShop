using GraphQL.Types;
using SneakerShop.API.GraphQL.Types;
using SneakerShop.API.GraphQL.Types.Input;
using SneakerShop.Models;
using SneakerShop.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SneakerShop.API.GraphQL.Restricted
{
    public class RestrictedMutation : ObjectGraphType
    {
        public RestrictedMutation(IOrderRepo orderRepo)
        {
            // Orders
            FieldAsync<OrderType>(
                "createOrder",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<OrderInputType>> { Name = "order" }),
                resolve: async context =>
                {
                    var order = context.GetArgument<Order>("order");
                    return await context.TryAsyncResolve(
                        async c => await orderRepo.Create(order));
                });

            FieldAsync<OrderItemType>(
                "createOrderItem",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<OrderItemInputType>> { Name = "orderItem" }),
                resolve: async context =>
                {
                    var orderItem = context.GetArgument<OrderItem>("orderItem");
                    return await context.TryAsyncResolve(
                        async c => await orderRepo.CreateOrderItem(orderItem));
                });
        }
    }
}
