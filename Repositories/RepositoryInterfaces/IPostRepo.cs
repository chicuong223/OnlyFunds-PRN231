using OnlyFundsAPI.BusinessObjects;
using System.Linq;

namespace Repositories.RepositoryInterfaces
{
    public interface IPostRepo
    {
        IQueryable<Post> GetList();
        Post Get(int id);
        void Insert(Post inserted);
        void Update(Post updated);
        void Delete(int id);
    }
}