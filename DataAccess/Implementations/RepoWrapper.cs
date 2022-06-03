using OnlyFundsAPI.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlyFundsAPI.DataAccess.Implementations
{
    class RepoWrapper
    {
        private IUserRepository _user;
        public IUserRepository Users
        {
            get
            {
                if (_user == null)
                {
                    _user = new UserRepository();
                }
                return _user;
            }
        }
    }
}
