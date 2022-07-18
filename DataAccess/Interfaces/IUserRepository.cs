using OnlyFundsAPI.BusinessObjects;
using System.Linq;
using System.Threading.Tasks;

namespace OnlyFundsAPI.DataAccess.Interfaces
{
    public interface IUserRepository
    {
        IQueryable<User> GetUsers();

        Task<User> GetUserByID(int id);

        Task<User> GetUserByUsernameAndPassword(string username, string password);

        Task<User> Create(User user);

        Task<User> Update(User user);

        Task Delete(int id);
    }
}