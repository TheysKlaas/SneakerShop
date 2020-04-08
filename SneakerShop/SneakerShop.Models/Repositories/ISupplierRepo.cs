using System.Collections.Generic;
using System.Threading.Tasks;

namespace SneakerShop.Models.Repositories
{
    public interface ISupplierRepo : IGenericRepo<Supplier>
    {
        new Task<Supplier> Create(Supplier s);
    }
}