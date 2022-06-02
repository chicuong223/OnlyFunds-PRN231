using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OnlyFundsAPI.BusinessObjects;
using OnlyFundsAPI.DataAccess.Interfaces;
using OnlyFundsAPI.Utilities;

namespace OnlyFundsAPI.DataAccess.Implementations
{
    public class UserRepository : IUserRepository
    {
        public UserRepository() { }

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

        public async Task<User> GetUserByUsernameAndPassword(string username, string password)
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

        public async Task<User> Create(User user)
        {
            try
            {
                using (var context = new OnlyFundsDBContext())
                {
                    var existingUser = await context.Users.SingleOrDefaultAsync(u => u.Username == user.Username);
                    if (existingUser != null)
                    {
                        throw new ArgumentException("Username is used!");
                    }
                    existingUser = await context.Users.SingleOrDefaultAsync(u => u.Email.Equals(user.Email));
                    if (existingUser != null)
                    {
                        throw new ArgumentException("Email is used!");
                    }
                    await context.Users.AddAsync(user);
                    await context.SaveChangesAsync();
                    return user;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<User> Update(User user)
        {
            try
            {
                using (var context = new OnlyFundsDBContext())
                {
                    var existingUser = context.Users
                        .SingleOrDefault(u => u.Username.ToLower().Equals(user.Username.ToLower()));
                    if (existingUser != null && existingUser.UserID != user.UserID)
                    {
                        throw new ArgumentException("Username is used!");
                    }
                    existingUser = context.Users
                        .SingleOrDefault(u => u.Email.ToLower().Equals(user.Email.ToLower()));
                    if (existingUser != null && existingUser.UserID != user.UserID)
                    {
                        throw new ArgumentException("Email is used!");
                    }
                    context.Entry(user).State = EntityState.Modified;
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return user;
        }

        public async Task Delete(int id)
        {
            try
            {
                using (var context = new OnlyFundsDBContext())
                {
                    var user = await context.Users.FindAsync(id);
                    if (user != null)
                    {
                        user.Active = false;
                        context.Entry(user).State = EntityState.Modified;
                        await context.SaveChangesAsync();
                    }
                    else
                    {
                        throw new ArgumentException("User ID not found!");

                    }
                }
            }
            catch
            {
                throw;
            }
        }
    }
}