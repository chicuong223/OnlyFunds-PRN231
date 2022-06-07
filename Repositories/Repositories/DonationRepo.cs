using Microsoft.EntityFrameworkCore;
using OnlyFundsAPI.BusinessObjects;
using Repositories.RepositoryInterfaces;
using System;
using System.Linq;

namespace Repositories.Repositories
{
    internal class DonationRepo : IDonationRepo
    {
        public Donation Get(int id)
        {
            try
            {
                using OnlyFundsDBContext context = new OnlyFundsDBContext();
                Donation entity = context.Donations.SingleOrDefault(e => e.DonationID == id);
                return entity;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IQueryable<Donation> GetList()
        {
            try
            {
                using OnlyFundsDBContext context = new OnlyFundsDBContext();
                IQueryable<Donation> list = context.Donations.AsQueryable();
                return list;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void Insert(Donation inserted)
        {
            try
            {
                using OnlyFundsDBContext context = new OnlyFundsDBContext();
                Donation existed = context.Donations.SingleOrDefault(e => e.DonationID == inserted.DonationID);
                if (existed == null)
                {
                    context.Donations.Add(inserted);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public void Update(Donation updated)
        {
            try
            {
                using OnlyFundsDBContext context = new OnlyFundsDBContext();
                Donation existed = context.Donations.AsNoTracking().SingleOrDefault(e => e.DonationID == updated.DonationID);
                if (existed != null)
                {
                    context.Donations.Update(updated);
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
                Donation existed = context.Donations.SingleOrDefault(e => e.DonationID == id);
                if (existed != null)
                {
                    context.Donations.Remove(existed);
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