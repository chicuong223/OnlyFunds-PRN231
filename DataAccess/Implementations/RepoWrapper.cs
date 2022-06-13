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
    }
}
