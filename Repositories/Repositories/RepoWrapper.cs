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
