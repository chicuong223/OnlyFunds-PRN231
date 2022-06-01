using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.RepositoryInterfaces
{
    interface IRepoWrapper
    {
        public IUserRepo Users { get; }
    }
}
