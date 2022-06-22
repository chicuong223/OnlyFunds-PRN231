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
        private IUserRepository _user;
        private ICommentRepository _comment;
        private ICommentLikeRepository _commentLikeRepository;
        private ITagRepository _tagRepository;
        private IFollowRepository _followRepository;
        private IBookmarkRepository _bookmark;
        private IPostLikeRepository _postLike;
        private IPostRepository _post;
        private IPostTagMapRepository _postTagMap;
        private IReportRepository _report;
        private INotificationRepository _notification;
        private readonly OnlyFundsDBContext _dbContext;
        public RepoWrapper(OnlyFundsDBContext context)
        {
            _dbContext = context;
        }
        public IUserRepository Users
        {
            get
            {
                if (_user == null)
                {
                    _user = new UserRepository(_dbContext);
                }
                return _user;
            }
        }

        public ICommentRepository Comments
        {
            get
            {
                if (_comment == null)
                {
                    _comment = new CommentRepository(_dbContext);
                }
                return _comment;
            }
        }

        public ICommentLikeRepository CommentLikes
        {
            get
            {
                if (_commentLikeRepository == null) _commentLikeRepository = new CommentLikeRepository(_dbContext);
                return _commentLikeRepository;
            }
        }

        public ITagRepository Tags
        {
            get
            {
                if (_tagRepository == null) _tagRepository = new TagRepository(_dbContext);
                return _tagRepository;
            }
        }

        public IFollowRepository Follows
        {
            get
            {
                if (_followRepository == null) _followRepository = new FollowRepository(_dbContext);
                return _followRepository;
            }
        }

        public IBookmarkRepository Bookmark
        {
            get
            {
                if (_bookmark == null) _bookmark = new BookmarkRepository(_dbContext);
                return _bookmark;
            }
        }

        public INotificationRepository Notifications
        {
            get
            {
                if (_notification == null) _notification = new NotificationRepository(_dbContext);
                return _notification;
            }
        }

        public IPostLikeRepository PostLikes
        {
            get
            {
                if (_postLike == null) _postLike = new PostLikeRepository(_dbContext);
                return _postLike;
            }
        }

        public IPostRepository Posts
        {
            get
            {
                if (_post == null) _post = new PostRepository(_dbContext);
                return _post;
            }
        }

        public IPostTagMapRepository PostTagMaps
        {
            get
            {
                if (_postTagMap == null) _postTagMap = new PostTagMapRepository(_dbContext);
                return _postTagMap;
            }
        }

        public IReportRepository Reports
        {
            get
            {
                if (_report == null) _report = new ReportRepository(_dbContext);
                return _report;
            }
        }
    }
}
