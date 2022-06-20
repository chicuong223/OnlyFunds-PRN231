using OnlyFundsAPI.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    interface IPostTagMapRepository
    {
        IQueryable<PostTagMap> GetList();
        Task<PostTagMap> GetByID(int tagId, int postId);
        Task<PostTagMap> Create(PostTagMap bookmark);
        Task<PostTagMap> Update(PostTagMap bookmark);
        Task Delete(int tagId, int postId);
    }
}
