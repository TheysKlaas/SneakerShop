using GraphQL.Types;
using SneakerShop.Models.Repositories;
using SneakerShop.NoSQLModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SneakerShop.API.GraphQL.Types.NoSQLTypes
{
    public class NoSQLOrderItemType : ObjectGraphType<OrderItemNoSQL>
    {
        public NoSQLOrderItemType(IProductRepo productRepo)
        {
            Field(oi => oi.OrderID, type: typeof(IdGraphType)).Description("The id of the Order.");
            Field(oi => oi.ProductID, type: typeof(IdGraphType)).Description("The id of the Product.");
            Field(oi => oi.Size, type: typeof(IntGraphType)).Description("The size of the product.");

            Field(
                name: "Product",
                type: typeof(ProductType),
                resolve: context => productRepo.GetById(context.Source.ProductID));

        }
    }
}
