using System.Collections.Generic;
using System.Threading.Tasks;
using OnlyFundsAPI.BusinessObjects;

namespace OnlyFundsAPI.DataAccess.Interfaces
{
    public interface ICommentRepository
    {
        Task<IEnumerable<Comment>> GetList();
        Task<Comment> GetByID(int key);
        Task<Comment> Create(Comment comment);
        Task<Comment> Update(Comment comment);
        Task Delete(int id);
    }
}