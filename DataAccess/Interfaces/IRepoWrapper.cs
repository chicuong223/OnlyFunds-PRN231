using DataAccess.Interfaces;

namespace OnlyFundsAPI.DataAccess.Interfaces
{
    public interface IRepoWrapper
    {
        IBookmarkRepository Bookmarks { get; }
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