using DataAccess.Implementations;
using DataAccess.Interfaces;
using OnlyFundsAPI.BusinessObjects;
using OnlyFundsAPI.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlyFundsAPI.DataAccess.Implementations
{
    public class RepoWrapper : IRepoWrapper
    {
        private IUserRepository _users;
        private ICommentRepository _comments;
        private ICommentLikeRepository _commentLikes;
        private ITagRepository _tags;
        private IFollowRepository _follows;
        private IBookmarkRepository _bookmarks;
        private IPostLikeRepository _postLikes;
        private IPostRepository _posts;
        private IPostTagMapRepository _postTagMaps;
        private IReportRepository _reports;
        private INotificationRepository _notifications;
        private readonly OnlyFundsDBContext _dbContext;
        public RepoWrapper(OnlyFundsDBContext context)
        {
            _dbContext = context;
        }
        public IUserRepository Users
        {
            get
            {
                if (_users == null)
                {
                    _users = new UserRepository(_dbContext);
                }
                return _users;
            }
        }

        public ICommentRepository Comments
        {
            get
            {
                if (_comments == null)
                {
                    _comments = new CommentRepository(_dbContext);
                }
                return _comments;
            }
        }

        public ICommentLikeRepository CommentLikes
        {
            get
            {
                if (_commentLikes == null) _commentLikes = new CommentLikeRepository(_dbContext);
                return _commentLikes;
            }
        }

        public ITagRepository Tags
        {
            get
            {
                if (_tags == null) _tags = new TagRepository(_dbContext);
                return _tags;
            }
        }

        public IFollowRepository Follows
        {
            get
            {
                if (_follows == null) _follows = new FollowRepository(_dbContext);
                return _follows;
            }
        }

        public IBookmarkRepository Bookmarks
        {
            get
            {
                if (_bookmarks == null) _bookmarks = new BookmarkRepository(_dbContext);
                return _bookmarks;
            }
        }

        public INotificationRepository Notifications
        {
            get
            {
                if (_notifications == null) _notifications = new NotificationRepository(_dbContext);
                return _notifications;
            }
        }

        public IPostLikeRepository PostLikes
        {
            get
            {
                if (_postLikes == null) _postLikes = new PostLikeRepository(_dbContext);
                return _postLikes;
            }
        }

        public IPostRepository Posts
        {
            get
            {
                if (_posts == null) _posts = new PostRepository(_dbContext);
                return _posts;
            }
        }

        public IPostTagMapRepository PostTagMaps
        {
            get
            {
                if (_postTagMaps == null) _postTagMaps = new PostTagMapRepository(_dbContext);
                return _postTagMaps;
            }
        }

        public IReportRepository Reports
        {
            get
            {
                if (_reports == null) _reports = new ReportRepository(_dbContext);
                return _reports;
            }
        }
    }
}
