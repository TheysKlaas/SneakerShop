using GraphQL;
using GraphQL.Types;
using SneakerShop.API.GraphQL.Types;
using SneakerShop.API.GraphQL.Types.Input;
using SneakerShop.Models;
using SneakerShop.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SneakerShop.API.GraphQL.Admin
{
    public class AdminMutation : ObjectGraphType
    {
        public AdminMutation(IProductRepo productRepo, ISupplierRepo supplierRepo)
        {
            // Products
            FieldAsync<ProductType>(
                "updateProduct",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<ProductInputType>> { Name = "product" },
                    new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "productId" }),
                resolve: async context =>
                {
                    var product = context.GetArgument<Product>("product");
                    var productId = context.GetArgument<Guid>("productId");

                    Product dbProduct = (await productRepo.GetByExpressionAsync(p => p.ProductId == productId)).First();
                    if (dbProduct == null)
                    {
                        context.Errors.Add(new ExecutionError("Couldn't find product in db."));
                        return null;
                    }
                    dbProduct.ProductName = product.ProductName;
                    dbProduct.UnitPrice = product.UnitPrice;
                    dbProduct.SupplierId = product.SupplierId;

                    return productRepo.Update(dbProduct);
                });

            FieldAsync<ProductType>(
                "createProduct",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<ProductInputType>> { Name = "product" }),
                resolve: async context =>
                {
                    var product = context.GetArgument<Product>("product");
                    return await context.TryAsyncResolve(
                        async c => await productRepo.Create(product));
                });

            // Supplier
            FieldAsync<SupplierType>(
                "createSupplier",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<SupplierInputType>> { Name = "supplier" }),
                resolve: async context =>
                {
                    var supplier = context.GetArgument<Supplier>("supplier");
                    return await context.TryAsyncResolve(
                        async c => await supplierRepo.Create(supplier));
                });

            FieldAsync<SupplierType>(
                "updateSupplier",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<SupplierInputType>> { Name = "supplier" },
                    new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "supplierId" }),
                resolve: async context =>
                {
                    var supplier = context.GetArgument<Supplier>("supplier");
                    var supplierId = context.GetArgument<Guid>("supplierId");

                    Supplier dbSupplier = (await supplierRepo.GetByExpressionAsync(s => s.SupplierId == supplierId)).First();
                    if (dbSupplier == null)
                    {
                        context.Errors.Add(new ExecutionError("Couldn't find supplier in db."));
                        return null;
                    }
                    dbSupplier.CompanyName = supplier.CompanyName;

                    return supplierRepo.Update(dbSupplier);
                });
        }
    }
}
