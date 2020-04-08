using GraphQL.DataLoader;
using GraphQL.Types;
using SneakerShop.Models;
using SneakerShop.Models.Data;
using SneakerShop.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SneakerShop.API.GraphQL.Types
{
    public class SupplierType : ObjectGraphType<Supplier>
    {
        public SupplierType(SneakerShopContext data, IProductRepo productRepo, IDataLoaderContextAccessor dataLoaderContextAccessor)
        {
            Field(s => s.SupplierId, type: typeof(IdGraphType)).Description("The id of the Company.");
            Field(s => s.CompanyName, nullable: false).Description("The name of the Company.");

            Field<ListGraphType<ProductType>>(nameof(Supplier.Product),
                resolve: context =>
                {
                    var loader = dataLoaderContextAccessor.Context.GetOrAddCollectionBatchLoader<Guid, Product>(
                        "GetProductsBySupplierId", productRepo.GetForSupplier);
                    return loader.LoadAsync(context.Source.SupplierId);

                });      
        }
    }
}
