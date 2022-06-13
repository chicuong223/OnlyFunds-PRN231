using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlyFundsAPI.BusinessObjects;

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