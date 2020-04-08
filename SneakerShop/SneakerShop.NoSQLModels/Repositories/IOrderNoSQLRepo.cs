using System.Collections.Generic;
using System.Threading.Tasks;

namespace SneakerShop.NoSQLModels.Repositories
{
    public interface IOrderNoSQLRepo
    {
        Task<OrderNoSQL> CreateAsync(OrderNoSQL o);
        Task<IEnumerable<OrderNoSQL>> GetAll();
    }
}