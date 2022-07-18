using Microsoft.EntityFrameworkCore;
using OnlyFundsAPI.BusinessObjects;
using OnlyFundsAPI.DataAccess.Interfaces;
using OnlyFundsAPI.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlyFundsAPI.DataAccess.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly OnlyFundsDBContext context;

        public UserRepository(OnlyFundsDBContext context)
        {
            this.context = context;
        }

        public IQueryable<User> GetUsers()
        {
            IEnumerable<User> result = new List<User>();
            try
            {
                result = context.Users.AsQueryable();
            }
            catch
            {
                throw;
            }
            return result.AsQueryable();
        }

        public async Task<User> GetUserByID(int id)
        {
            User result = null;
            try
            {
                result = await context.Users.FindAsync(id);
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
                result = await context.Users
                    .SingleOrDefaultAsync(user => user.Username.Equals(username)
                        && user.Password.Equals(PasswordUtils.HashString(password)));
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
            catch (Exception ex)
            {
                if (ex is ArgumentException)
                {
                    throw new ArgumentException(ex.Message);
                }
                throw new Exception(ex.Message);
            }
        }

        public async Task<User> Update(User user)
        {
            try
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
                user.Password = PasswordUtils.HashString(user.Password);
                context.Entry(user).State = EntityState.Modified;
                await context.SaveChangesAsync();
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException(ex.Message);
            }
            catch (DbUpdateConcurrencyException de)
            {
                throw new DbUpdateConcurrencyException(de.Message);
            }
            return user;
        }

        public async Task Delete(int id)
        {
            try
            {
                var user = await context.Users.FindAsync(id);
                if (user != null && user.Active)
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
            catch
            {
                throw;
            }
        }
    }
}