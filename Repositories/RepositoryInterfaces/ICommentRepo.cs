using OnlyFundsAPI.BusinessObjects;
using System.Linq;

namespace Repositories.RepositoryInterfaces
{
    public interface ICommentRepo
    {
        IQueryable<Comment> GetList();
        Comment Get(int id);
        void Insert(Comment inserted);
        void Update(Comment updated);
        void Delete(int id);
    }
}