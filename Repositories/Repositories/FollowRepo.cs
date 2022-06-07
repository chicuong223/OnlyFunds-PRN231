using Microsoft.EntityFrameworkCore;
using OnlyFundsAPI.BusinessObjects;
using Repositories.RepositoryInterfaces;
using System;
using System.Linq;

namespace Repositories.Repositories
{
    internal class FollowRepo : IFollowRepo
    {
        public Follow Get(int followerId, int followeeId)
        {
            try
            {
                using OnlyFundsDBContext context = new OnlyFundsDBContext();
                Follow entity = context.Follows.SingleOrDefault(e => e.FollowerID == followerId && e.FolloweeID == followeeId);
                return entity;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IQueryable<Follow> GetList()
        {
            try
            {
                using OnlyFundsDBContext context = new OnlyFundsDBContext();
                IQueryable<Follow> list = context.Follows.AsQueryable();
                return list;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void Insert(Follow inserted)
        {
            try
            {
                using OnlyFundsDBContext context = new OnlyFundsDBContext();
                Follow existed = context.Follows.SingleOrDefault(e => e.FollowerID == inserted.FollowerID && e.FolloweeID == inserted.FolloweeID);
                if (existed == null)
                {
                    context.Follows.Add(inserted);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public void Update(Follow updated)
        {
            try
            {
                using OnlyFundsDBContext context = new OnlyFundsDBContext();
                Follow existed = context.Follows.AsNoTracking().SingleOrDefault(e => e.FollowerID == updated.FollowerID && e.FolloweeID == updated.FolloweeID);
                if (existed != null)
                {
                    context.Follows.Update(updated);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public void Delete(int followerId, int followeeId)
        {
            try
            {
                using OnlyFundsDBContext context = new OnlyFundsDBContext();
                Follow existed = context.Follows.SingleOrDefault(e => e.FollowerID == followerId && e.FolloweeID == followeeId);
                if (existed != null)
                {
                    context.Follows.Remove(existed);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}