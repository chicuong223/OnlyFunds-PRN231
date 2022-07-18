using Microsoft.EntityFrameworkCore;
using OnlyFundsAPI.BusinessObjects;
using OnlyFundsAPI.DataAccess.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace OnlyFundsAPI.DataAccess.Implementations
{
    public class FollowRepository : IFollowRepository
    {
        private readonly OnlyFundsDBContext context;

        public FollowRepository(OnlyFundsDBContext context)
        {
            this.context = context;
            this.context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public async Task<Follow> Create(Follow follow)
        {
            try
            {
                if (follow.FolloweeID == follow.FollowerID) throw new ArgumentException("Users can't follow themselves");
                var exist = await context.Follows
                    .SingleOrDefaultAsync(f => f.FollowerID == follow.FollowerID && f.FolloweeID == follow.FolloweeID);
                if (exist != null) throw new ArgumentException("Follow exists!");
                var follower = await context.Users.FindAsync(follow.FollowerID);
                var followee = await context.Users.FindAsync(follow.FolloweeID);
                if (follower == null || !follower.Active || follower.Banned)
                {
                    throw new ArgumentOutOfRangeException("Follower ID not found!");
                }

                if (followee == null || !followee.Active || followee.Banned)
                {
                    throw new ArgumentOutOfRangeException("Followee ID not found!");
                }
                await context.Follows.AddAsync(follow);
                await context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
            return follow;
        }

        public async Task Delete(int followerID, int followeeID)
        {
            try
            {
                var follow = await context.Follows
                    .SingleOrDefaultAsync(follow => follow.FolloweeID == followeeID && follow.FollowerID == followerID);
                if (follow == null) throw new ArgumentException("Follow not found!");
                context.Entry(follow).State = EntityState.Deleted;
                await context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public IQueryable<Follow> GetList()
        {
            IQueryable<Follow> result;
            try
            {
                result = context.Follows.AsQueryable();
            }
            catch
            {
                throw;
            }
            return result;
        }

        public async Task<Follow> GetSingle(int followerID, int followeeID)
        {
            Follow result = null;
            try
            {
                result = await context.Follows
                    .SingleOrDefaultAsync(follow => follow.FolloweeID == followeeID && follow.FollowerID == followerID);
            }
            catch
            {
                throw;
            }
            return result;
        }
    }
}