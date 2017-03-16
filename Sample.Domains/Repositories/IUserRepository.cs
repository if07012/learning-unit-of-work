using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Domains.Repositories
{
    public interface IUserRepository : IBaseRepository
    {
        bool Authentication(string userName, string password);
        User AddUser(User user);
        User UpdateLastWriteMessage(User user);
        IEnumerable<User> GetUsers();
    }
}
