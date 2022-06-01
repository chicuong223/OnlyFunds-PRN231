using OnlyFundsAPI.BusinessObjects;
using System.Linq;
using System.Threading.Tasks;

namespace Repositories.RepositoryInterfaces
{
    interface IUserRepo
    {
        Task<User> GetUserByID(int id);
        Task<User> GetUserByPasswordAndUsername(string username, string password);
        IQueryable<User> GetUsers();
    }
}