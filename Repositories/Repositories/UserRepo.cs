using Microsoft.EntityFrameworkCore;
using OnlyFundsAPI.BusinessObjects;
using Repositories.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repositories
{
    class UserRepo : IUserRepo
    {
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
