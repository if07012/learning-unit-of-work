using Sample.Domains;
using Sample.Presenters.Commands;
using Sample.Presenters.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace Sample.Presenters
{
    public class UserPresenter
    {
        public UserPresenter(IUserView view, IUserCommand command)
        {
            View = view;
            Command = command;
            view.AttachPresenter(this);
        }

        public IUserCommand Command { get; private set; }
        public IUserView View { get; private set; }

        public User Login()
        {
            return Command.Authentication(View.UserName, View.Password);
        }

        public IEnumerable<Message> GetMessage()
        {
            return Command.GetMessage(View.User);
        }

        public void AddMessage()
        {
            Command.AddMessage(View.User, View.Message);
        }
    }
}
