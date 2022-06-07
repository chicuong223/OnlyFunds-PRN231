using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.RepositoryInterfaces
{
    interface IRepoWrapper
    {
        public IBookmarkRepo Bookmarks { get; }
        public ICommentRepo Comments { get; }
        public ICommentLikeRepo CommentLikes { get; }
        public IDonationRepo Donations { get; }
        public IFollowRepo Follows { get; }
        public INotifictionRepo Notifictions { get; }
        public IPostRepo Posts { get; }
        public IPostCategoryRepo PostCategories { get; }
        public IPostLikeRepo PostLikes { get; }
        public IReportRepo Reports { get; }
        public IUserRepo Users { get; }
    }
}
