﻿using OnlyFundsAPI.DataAccess.Interfaces;
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
