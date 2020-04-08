using SneakerShop.Models.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SneakerShop.Models.Repositories
{
    public class SupplierRepo : GenericRepo<Supplier>, ISupplierRepo
    {
        public SupplierRepo(SneakerShopContext context) : base(context)
        {

        }
        public async new Task<Supplier> Create(Supplier s)
        {
            try
            {
                await _context.Supplier.AddAsync(s);
                await _context.SaveChangesAsync();
                return s;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
