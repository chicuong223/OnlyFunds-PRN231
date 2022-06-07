using OnlyFundsAPI.BusinessObjects;
using System.Linq;

namespace Repositories.RepositoryInterfaces
{
    public interface IDonationRepo
    {
        IQueryable<Donation> GetList();
        Donation Get(int id);
        void Insert(Donation inserted);
        void Update(Donation updated);
        void Delete(int id);
    }
}