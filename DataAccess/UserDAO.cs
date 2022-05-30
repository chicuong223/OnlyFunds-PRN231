using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OnlyFundsAPI.BusinessObjects;
using OnlyFundsAPI.Utilities;

namespace OnlyFundsAPI.DataAccess
{
    public class UserDAO
    {
        private UserDAO() { }
        private static UserDAO _instance = null;
        private static readonly object _instanceLock = new object();
        public UserDAO Instance
        {
            get
            {
                lock (_instanceLock)
                {
                    if (_instance == null) _instance = new UserDAO();
                    return _instance;
                }
            }
        }

        public IQueryable<User> GetUsers()
        {
            IQueryable<User> result = null;
            try
            {
                using (var context = new OnlyFundsDBContext())
                {
                    result = context.Users;
                }
            }
            catch
            {
                throw;
            }
            return result;
        }

        public async Task<User> GetUserByID(int id)
        {
            User result = null;
            try
            {
                using (var context = new OnlyFundsDBContext())
                {
                    result = await context.Users.FindAsync(id);
                }
            }
            catch
            {
                throw;
            }
            return result;
        }

        public async Task<User> GetUserByPasswordAndUsername(string username, string password)
        {
            User result = null;
            try
            {
                using (var context = new OnlyFundsDBContext())
                {
                    result = await context.Users
                        .SingleOrDefaultAsync(user => user.Username.Equals(username)
                            && user.Password.Equals(PasswordUtils.HashString(password)));
                }
            }
            catch
            {
                throw;
            }
            return result;
        }

    }
}