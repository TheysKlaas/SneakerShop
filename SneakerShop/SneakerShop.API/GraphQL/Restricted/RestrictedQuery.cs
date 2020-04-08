using GraphQL.Types;
using SneakerShop.API.GraphQL.Types;
using SneakerShop.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SneakerShop.API.GraphQL.Restricted
{
    public class RestrictedQuery : ObjectGraphType
    {
        public RestrictedQuery(IOrderRepo orderRepo)
        {
        // Orders for User
               

            Field<ListGraphType<OrderType>>(
                "ordersByUserId",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IdGraphType>>
                { Name = "id" }),
                resolve: context =>
                {
                    try
                    {
                        var id = context.GetArgument<string>("id");
                        return orderRepo.GetOrderForUser(id);
                    }
                    catch(Exception ex)
                    {
                        throw ex;
                    }

                }
            );
        }
    }
}
