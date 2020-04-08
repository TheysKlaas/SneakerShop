using GraphQL.DataLoader;
using GraphQL.Server.Authorization.AspNetCore;
using GraphQL.Types;
using SneakerShop.Models;
using SneakerShop.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SneakerShop.API.GraphQL.Types
{
    public class ProductType : ObjectGraphType<Product>
    {
        public ProductType(SneakerShopContext data)
        {
            Name = "Product";

            Field(p => p.ProductId, type: typeof(IdGraphType)).Description("The id of the product.");
            Field(p => p.ProductName, nullable: false).Description("The name of the Product.");
            Field(p => p.UnitPrice, nullable: false).Description("The price of the Product.");

            Field(
                name: "Supplier",
                type: typeof(SupplierType),
                resolve: context => context.Source.Supplier
                );
        }
    }
}
