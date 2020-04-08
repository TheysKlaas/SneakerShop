using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SneakerShop.Models.Repositories
{
    public interface IUserRepo : IGenericRepo<User>
    {
        User GetUserById(string userID);
        Task<IEnumerable<User>> GetUsersAsync();
    }
}
