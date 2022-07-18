using OnlyFundsAPI.BusinessObjects;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface IReportRepository
    {
        IQueryable<Report> GetList();

        Task<Report> GetByID(int id);

        Task<Report> Create(Report post);

        Task<Report> Update(Report post);

        Task Delete(int id);
    }
}