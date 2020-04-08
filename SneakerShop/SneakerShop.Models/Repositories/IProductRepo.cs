using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SneakerShop.Models.Repositories
{
    public interface IProductRepo : IGenericRepo<Product>
    {
        new Task<IEnumerable<Product>> GetAllAsync();
        Task<IEnumerable<Product>> GetProductsByBrand(Guid id);
        new Task<Product> Create(Product p);
        new Product Update(Product p);
        Task<ILookup<Guid, Product>> GetForSupplier(IEnumerable<Guid> productGuids);
        Product GetById(Guid id);
    }
}