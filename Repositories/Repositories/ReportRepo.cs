using Microsoft.EntityFrameworkCore;
using OnlyFundsAPI.BusinessObjects;
using Repositories.RepositoryInterfaces;
using System;
using System.Linq;

namespace Repositories.Repositories
{
    internal class ReportRepo : IReportRepo
    {
        public Report Get(int id)
        {
            try
            {
                using OnlyFundsDBContext context = new OnlyFundsDBContext();
                Report entity = context.Reports.Single(e => e.ReportID == id);
                return entity;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IQueryable<Report> GetList()
        {
            try
            {
                using OnlyFundsDBContext context = new OnlyFundsDBContext();
                IQueryable<Report> list = context.Reports.AsQueryable();
                return list;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void Insert(Report inserted)
        {
            try
            {
                using OnlyFundsDBContext context = new OnlyFundsDBContext();
                Report existed = context.Reports.SingleOrDefault(e => e.ReportID == inserted.ReportID);
                if (existed == null)
                {
                    context.Reports.Add(inserted);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public void Update(Report updated)
        {
            try
            {
                using OnlyFundsDBContext context = new OnlyFundsDBContext();
                Report existed = context.Reports.AsNoTracking().SingleOrDefault(e => e.ReportID == updated.ReportID);
                if (existed != null)
                {
                    context.Reports.Update(updated);
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
                Report existed = context.Reports.SingleOrDefault(e => e.ReportID == id);
                if (existed != null)
                {
                    context.Reports.Remove(existed);
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