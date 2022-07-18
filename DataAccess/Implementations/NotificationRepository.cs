using DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using OnlyFundsAPI.BusinessObjects;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Implementations
{
    internal class NotificationRepository : INotificationRepository
    {
        private readonly OnlyFundsDBContext context;

        public NotificationRepository(OnlyFundsDBContext context)
        {
            this.context = context;
        }

        public async Task<Notification> Create(Notification notification)
        {
            try
            {
                if (!await context.Notifications.AnyAsync(e => e.NotificationID == notification.NotificationID))
                {
                    await context.Notifications.AddAsync(notification);
                    await context.SaveChangesAsync();
                }
                else
                {
                    throw new ArgumentException("Notification existed");
                }
            }
            catch
            {
                throw;
            }
            return notification;
        }

        public async Task Delete(int id)
        {
            try
            {
                Notification notification = await context.Notifications.SingleOrDefaultAsync(e => e.NotificationID == id);
                if (notification != null)
                {
                    context.Notifications.Remove(notification);
                    await context.SaveChangesAsync();
                }
                else
                {
                    throw new ArgumentException("Notification not found");
                }
            }
            catch
            {
                throw;
            }
        }

        public async Task<Notification> GetByID(int id)
        {
            Notification result = null;
            try
            {
                result = await context.Notifications
                    .SingleOrDefaultAsync(e => e.NotificationID == id);
            }
            catch
            {
                throw;
            }
            return result;
        }

        public IQueryable<Notification> GetList()
        {
            IQueryable<Notification> result;
            try
            {
                result = context.Notifications.AsQueryable();
            }
            catch
            {
                throw;
            }
            return result;
        }

        public async Task<Notification> Update(Notification notification)
        {
            try
            {
                if (await context.Notifications.AnyAsync(e => e.NotificationID == notification.NotificationID))
                {
                    context.Notifications.Update(notification);
                    await context.SaveChangesAsync();
                    return notification;
                }
                else
                {
                    throw new ArgumentException("Notification not found");
                }
            }
            catch
            {
                throw;
            }
        }
    }
}