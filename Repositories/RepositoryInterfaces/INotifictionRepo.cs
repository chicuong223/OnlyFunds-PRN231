using OnlyFundsAPI.BusinessObjects;
using System.Linq;

namespace Repositories.RepositoryInterfaces
{
    public interface INotifictionRepo
    {
        IQueryable<Notification> GetList();
        Notification Get(int id);
        void Insert(Notification inserted);
        void Update(Notification updated);
        void Delete(int id);
    }
}