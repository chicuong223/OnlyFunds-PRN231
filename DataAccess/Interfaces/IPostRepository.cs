using OnlyFundsAPI.BusinessObjects;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface IPostRepository
    {
        IQueryable<Post> GetList();

        Task<Post> GetByID(int id);

        Task<Post> Create(Post post);

        Task<Post> Update(Post post);

        Task Delete(int id);
    }
}