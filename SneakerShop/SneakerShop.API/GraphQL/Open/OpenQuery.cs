using GraphQL.Types;
using SneakerShop.API.GraphQL.Types;
using SneakerShop.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SneakerShop.API.GraphQL.Open
{
    public class OpenQuery : ObjectGraphType
    {
        public OpenQuery(IProductRepo productRepo, ISupplierRepo supplierRepo)
        {
            // Products

            Field<ListGraphType<ProductType>>(
                "products",
                resolve: context => productRepo.GetAllAsync()
            );

            Field<ProductType>(
                "product",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IdGraphType>>
                { Name = "id" }),
                resolve: context =>
                {
                    var id = context.GetArgument<string>("id");
                    return productRepo.GetById(Guid.Parse(id));

                }
            );

            Field<ListGraphType<ProductType>>(
                "productsbybrand",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IdGraphType>>
                { Name = "supplierId" }),
                resolve: context =>
                {
                    var id = context.GetArgument<Guid>("supplierId");
                    return productRepo.GetProductsByBrand(id);

                }
            );

            // Supplier

            Field<ListGraphType<SupplierType>>(
                "suppliers",
                resolve: context => supplierRepo.GetAllAsync()
            );
        }
    }
}
