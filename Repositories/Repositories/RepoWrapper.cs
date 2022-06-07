using Repositories.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repositories
{
    class RepoWrapper : IRepoWrapper
    {
       
        private IBookmarkRepo _bookmark;
        public IBookmarkRepo Bookmarks
        {
            get
            {
                if (_bookmark == null)
                {
                    _bookmark = new BookmarkRepo();
                }
                return _bookmark;
            }
        }

        private ICommentRepo _comment;
        public ICommentRepo Comments
        {
            get
            {
                if (_comment == null)
                {
                    _comment = new CommentRepo();
                }
                return _comment;
            }
        }

        private ICommentLikeRepo _commentLike;
        public ICommentLikeRepo CommentLikes
        {
            get
            {
                if (_commentLike == null)
                {
                    _commentLike = new CommentLikeRepo();
                }
                return _commentLike;
            }
        }

        private IDonationRepo _donation;
        public IDonationRepo Donations
        {
            get
            {
                if (_donation == null)
                {
                    _donation = new DonationRepo();
                }
                return _donation;
            }
        }

        private IFollowRepo _follow;
        public IFollowRepo Follows
        {
            get
            {
                if (_follow == null)
                {
                    _follow = new FollowRepo();
                }
                return _follow;
            }
        }

        private INotifictionRepo _notifiction;
        public INotifictionRepo Notifictions
        {
            get
            {
                if (_notifiction == null)
                {
                    _notifiction = new NotifictionRepo();
                }
                return _notifiction;
            }
        }

        private IPostRepo _post;
        public IPostRepo Posts
        {
            get
            {
                if (_post == null)
                {
                    _post = new PostRepo();
                }
                return _post;
            }
        }

        private IPostCategoryRepo _postCategory;
        public IPostCategoryRepo PostCategories
        {
            get
            {
                if (_postCategory == null)
                {
                    _postCategory = new PostCategoryRepo();
                }
                return _postCategory;
            }
        }

        private IPostLikeRepo _postLike;
        public IPostLikeRepo PostLikes
        {
            get
            {
                if (_postLike == null)
                {
                    _postLike = new PostLikeRepo();
                }
                return _postLike;
            }
        }

        private IReportRepo _report;
        public IReportRepo Reports
        {
            get
            {
                if (_report == null)
                {
                    _report = new ReportRepo();
                }
                return _report;
            }
        }

        private IUserRepo _user;
        public IUserRepo Users
        {
            get
            {
                if (_user == null)
                {
                    _user = new UserRepo();
                }
                return _user;
            }
        }

    }
}
