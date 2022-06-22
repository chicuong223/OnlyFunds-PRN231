using DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlyFundsAPI.DataAccess.Interfaces
{
    public interface IRepoWrapper
    {
        IBookmarkRepository Bookmark { get; }
        ICommentLikeRepository CommentLikes { get; }
        ICommentRepository Comments { get; }
        IFollowRepository Follows { get; }
        INotificationRepository Notifications { get; }
        IPostLikeRepository PostLikes { get; }
        IPostRepository Posts { get; }
        IPostTagMapRepository PostTagMaps { get; }
        IReportRepository Reports { get; }
        IUserRepository Users { get; }
        ITagRepository Tags { get; }
    }
}
