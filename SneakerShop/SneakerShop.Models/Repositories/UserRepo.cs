using Microsoft.EntityFrameworkCore;
using SneakerShop.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SneakerShop.Models.Repositories
{
    public class UserRepo : GenericRepo<User>, IUserRepo
    {

        public UserRepo(SneakerShopContext context) : base(context)
        {

        }
        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            try
            {
                IEnumerable<User> result = await _context.Users.ToListAsync();

                return result.OrderBy(e => e.UserName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public User GetUserById(string userID)
        {
            try
            {
                User result = _context.Users.Where(u => u.Id == userID).FirstOrDefault();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
