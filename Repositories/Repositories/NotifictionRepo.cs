using Microsoft.EntityFrameworkCore;
using OnlyFundsAPI.BusinessObjects;
using Repositories.RepositoryInterfaces;
using System;
using System.Linq;

namespace Repositories.Repositories
{
    internal class NotifictionRepo : INotifictionRepo
    {
        public Notification Get(int id)
        {
            try
            {
                using OnlyFundsDBContext context = new OnlyFundsDBContext();
                Notification entity = context.Notifications.SingleOrDefault(e => e.NotificationID == id);
                return entity;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IQueryable<Notification> GetList()
        {
            try
            {
                using OnlyFundsDBContext context = new OnlyFundsDBContext();
                IQueryable<Notification> list = context.Notifications.AsQueryable();
                return list;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void Insert(Notification inserted)
        {
            try
            {
                using OnlyFundsDBContext context = new OnlyFundsDBContext();
                Notification existed = context.Notifications.SingleOrDefault(e => e.NotificationID == inserted.NotificationID);
                if (existed == null)
                {
                    context.Notifications.Add(inserted);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public void Update(Notification updated)
        {
            try
            {
                using OnlyFundsDBContext context = new OnlyFundsDBContext();
                Notification existed = context.Notifications.AsNoTracking().SingleOrDefault(e => e.NotificationID == updated.NotificationID);
                if (existed != null)
                {
                    context.Notifications.Update(updated);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public void Delete(int id)
        {
            try
            {
                using OnlyFundsDBContext context = new OnlyFundsDBContext();
                Notification existed = context.Notifications.SingleOrDefault(e => e.NotificationID == id);
                if (existed != null)
                {
                    context.Notifications.Remove(existed);
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