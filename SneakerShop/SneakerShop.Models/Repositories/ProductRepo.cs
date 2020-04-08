using Microsoft.EntityFrameworkCore;
using SneakerShop.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SneakerShop.Models.Repositories
{
    public class ProductRepo : GenericRepo<Product>, IProductRepo
    {
        public ProductRepo(SneakerShopContext context) : base(context)
        {

        }
        public async new Task<IEnumerable<Product>> GetAllAsync()
        {
            try
            {
                IEnumerable<Product> result = await _context.Product
                    //.ToListAsync();
                    .Include(p => p.Supplier).ToListAsync();

                return result.OrderBy(p => p.ProductName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Product GetById(Guid id)
        {
            try
            {
                Product result = _context.Product.Where(p => p.ProductId == id)
                    .Include(p => p.Supplier).First();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<IEnumerable<Product>> GetProductsByBrand(Guid id)
        {
            try
            {
                IEnumerable<Product> result = await _context.Product
                    .Where(p=> p.SupplierId == id)
                    .Include(p => p.Supplier)
                    .ToListAsync();

                return result.OrderBy(p => p.ProductName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async new Task<Product> Create(Product p)
        {
            try
            {
                await _context.Product.AddAsync(p);
                await _context.SaveChangesAsync();
                return p;
            }
            catch(Exception ex)
            {
                throw ex;
            }

        }
        public new Product Update(Product p)
        {
            try
            {
                _context.Product.Update(p);
                _context.SaveChanges();
                return p;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<ILookup<Guid, Product>> GetForSupplier(IEnumerable<Guid> supplierGuids)
        {
            var products = await _context.Product.Where(
                p => supplierGuids.Contains(p.SupplierId)).ToListAsync();
            return products.ToLookup(p => p.SupplierId);
        }
    }
}
