using Sample.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Presenters.Views
{
    public interface IUserView : IBaseView<UserPresenter>
    {

        string UserName { get; }
        string Password { get; }
        string Message { get; }
        User User { get; set; }
        bool IsLogin { get; set; }
    }
}
