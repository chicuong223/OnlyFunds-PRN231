using System.Collections.Generic;
using System.Threading.Tasks;
using OnlyFundsAPI.BusinessObjects;

namespace OnlyFundsAPI.DataAccess.Interfaces
{
    public interface ITagRepository
    {
        Task<IEnumerable<PostTag>> GetList();
        Task<PostTag> GetByID(int id);
        Task<PostTag> Create(PostTag tag);
        Task<PostTag> Update(PostTag tag);
        Task Delete(int id);
    }
}