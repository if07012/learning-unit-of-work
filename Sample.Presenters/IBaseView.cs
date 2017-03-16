using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Presenters
{
    public interface IBaseView<TPresenter>
    {
        void AttachPresenter(TPresenter presenter);
    }
}
