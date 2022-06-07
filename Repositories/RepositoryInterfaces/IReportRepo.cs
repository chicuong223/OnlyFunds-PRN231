using OnlyFundsAPI.BusinessObjects;
using System.Linq;

namespace Repositories.RepositoryInterfaces
{
    public interface IReportRepo
    {
        IQueryable<Report> GetList();
        Report Get(int id);
        void Insert(Report inserted);
        void Update(Report updated);
        void Delete(int id);
    }
}