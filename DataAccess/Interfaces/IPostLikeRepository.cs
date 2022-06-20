using OnlyFundsAPI.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface IPostLikeRepository
    {
        IQueryable<PostLike> GetList();
        Task<PostLike> GetByID(int likerId, int postId);
        Task<PostLike> Create(PostLike bookmark);
        Task<PostLike> Update(PostLike bookmark);
        Task Delete(int likerId, int postId);
    }
}
