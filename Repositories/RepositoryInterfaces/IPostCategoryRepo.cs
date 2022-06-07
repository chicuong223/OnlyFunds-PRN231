using OnlyFundsAPI.BusinessObjects;
using System.Linq;

namespace Repositories.RepositoryInterfaces
{
    public interface IPostCategoryRepo
    {
        IQueryable<PostCategory> GetList();
        PostCategory Get(int id);
        void Insert(PostCategory inserted);
        void Update(PostCategory updated);
        void Delete(int id);
    }
}