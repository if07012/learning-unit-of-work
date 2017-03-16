using Sample.Presenters.Commands;
using Sample.Presenters.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sample.Domains;
using Sample.Domains.Repositories;

namespace Sample.Impls.Commands
{
    public class UserCommand : IUserCommand
    {
        private IMessageRepository messageRepository;
        private IUnitOfWork unitOfWork;
        private IUserRepository userRepository;
        private IUserView view;
        public UserCommand(IUserView view, IUnitOfWork unitOfWork)
        {
            this.view = view;
            this.unitOfWork = unitOfWork;
            userRepository = unitOfWork.GetRepository<IUserRepository>();
            messageRepository = unitOfWork.GetRepository<IMessageRepository>();
        }

        public Message AddMessage(User user, string message)
        {
            var userInStorage = userRepository.GetUsers().FirstOrDefault(n => n.UserName.Equals(user.UserName) && n.Password.Equals(user.Password));
            if (userInStorage == null)
                return null;
            var model = new Message()
            {
                User = userInStorage,
                ContentMessage = message
            };
            messageRepository.AddMessage(userInStorage, model);
            userInStorage.LastWriteMessage = DateTime.Now;
            userRepository.UpdateLastWriteMessage(userInStorage);
            unitOfWork.SaveChanges();
            return model;
        }

        public User Authentication(string userName, string password)
        {
            var result = userRepository.Authentication(userName, password);
            return result ? userRepository.GetUsers().FirstOrDefault(n => n.UserName.Equals(userName) && n.Password.Equals(password)) : null;
        }

        public IEnumerable<Message> GetMessage(User user)
        {
            return messageRepository.GetMessages().Where(n => n.User.Id == user.Id);
        }
    }
}
