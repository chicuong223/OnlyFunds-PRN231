using OnlyFundsAPI.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface IBookMarkRepository
    {
        IQueryable<Bookmark> GetList();
        Task<Bookmark> GetByID(int userId, int postId);
        Task<Bookmark> Create(Bookmark bookmark);
        Task<Bookmark> Update(Bookmark bookmark);
        Task Delete(int userId, int postId);
    }
}
