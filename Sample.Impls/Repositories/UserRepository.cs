using Sample.Domains.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sample.Domains;

namespace Sample.Impls.Repositories
{
    public class UserRepository : IUserRepository
    {
        public UserRepository(IUnitOfWork unitOfWork)
        {
            this.context = unitOfWork.GetContext<UserRepository>();
            unitOfWork.RegisterContext(context);
        }
        private IContextWithTransaction context;
        public IContextWithTransaction Context
        {
            get
            {
                return context;
            }
        }

        public User AddUser(User user)
        {
            context.Add(user);
            return user;
        }

        public bool Authentication(string userName, string password)
        {
            return context.Gets<User>().Any(n => n.UserName.Equals(userName) && n.Password.Equals(password));
        }

        public IEnumerable<User> GetUsers()
        {
            return context.Gets<User>();
        }

        public User UpdateLastWriteMessage(User user)
        {
            context.Update(user);
            return user;
        }
    }
}
