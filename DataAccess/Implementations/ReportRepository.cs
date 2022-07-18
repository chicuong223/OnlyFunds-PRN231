using DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using OnlyFundsAPI.BusinessObjects;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Implementations
{
    internal class ReportRepository : IReportRepository
    {
        private readonly OnlyFundsDBContext context;

        public ReportRepository(OnlyFundsDBContext context)
        {
            this.context = context;
        }

        public async Task<Report> Create(Report report)
        {
            try
            {
                if (!await context.Reports.AnyAsync(e => e.ReportID == report.ReportID))
                {
                    await context.Reports.AddAsync(report);
                    await context.SaveChangesAsync();
                    return report;
                }
                else
                {
                    throw new ArgumentException("Report existed");
                }
            }
            catch
            {
                throw;
            }
        }

        public async Task Delete(int id)
        {
            try
            {
                Report report = await context.Reports.SingleOrDefaultAsync(e => e.ReportID == id);
                if (report != null)
                {
                    context.Reports.Remove(report);
                    await context.SaveChangesAsync();
                }
                else
                {
                    throw new ArgumentException("Report not found");
                }
            }
            catch
            {
                throw;
            }
        }

        public async Task<Report> GetByID(int id)
        {
            try
            {
                Report report = await context.Reports.SingleOrDefaultAsync(e => e.ReportID == id);
                return report;
            }
            catch
            {
                throw;
            }
        }

        public IQueryable<Report> GetList()
        {
            try
            {
                return context.Reports.AsQueryable();
            }
            catch
            {
                throw;
            }
        }

        public async Task<Report> Update(Report report)
        {
            try
            {
                if (await context.Reports.AnyAsync(e => e.ReportID == report.ReportID))
                {
                    context.Reports.Update(report);
                    await context.SaveChangesAsync();
                    return report;
                }
                else
                {
                    throw new ArgumentException("Report not found");
                }
            }
            catch
            {
                throw;
            }
        }
    }
}