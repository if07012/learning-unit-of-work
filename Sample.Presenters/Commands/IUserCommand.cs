using Sample.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Presenters.Commands
{
    public interface IUserCommand
    {
        User Authentication(string userName, string password);
        Message AddMessage(User user, string message);
        IEnumerable<Message> GetMessage(User user);
    }
}
