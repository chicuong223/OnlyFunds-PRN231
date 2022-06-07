using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlyFundsAPI.DataAccess.Interfaces
{
    public interface IRepoWrapper
    {
        IUserRepository Users { get; }
        ICommentRepository Comments { get; }
    }
}
