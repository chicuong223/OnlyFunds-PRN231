using OnlyFundsAPI.BusinessObjects;
using System.Linq;
using System.Threading.Tasks;

namespace OnlyFundsAPI.DataAccess.Interfaces
{
    public interface ITagRepository
    {
        IQueryable<PostTag> GetList();

        Task<PostTag> GetByID(int id);

        Task<PostTag> Create(PostTag tag);

        Task<PostTag> Update(PostTag tag);

        Task Delete(int id);
    }
}