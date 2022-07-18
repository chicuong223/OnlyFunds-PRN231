using OnlyFundsAPI.BusinessObjects;
using System.Linq;
using System.Threading.Tasks;

namespace OnlyFundsAPI.DataAccess.Interfaces
{
    public interface ICommentRepository
    {
        IQueryable<Comment> GetList();

        Task<Comment> GetByID(int key);

        Task<Comment> Create(Comment comment);

        Task<Comment> Update(Comment comment);

        Task Delete(int id);
    }
}