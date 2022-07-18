using OnlyFundsAPI.BusinessObjects;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface IPostTagMapRepository
    {
        IQueryable<PostTagMap> GetList();

        Task<PostTagMap> GetByID(int tagId, int postId);

        Task<PostTagMap> Create(PostTagMap postTagMap);

        Task<PostTagMap> Update(PostTagMap postTagMap);

        Task Delete(int tagId, int postId);
    }
}