using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sample.Domains.Repositories;
using Sample.Domains.Core;

namespace Sample.Domains
{
    public interface IUnitOfWork
    {
        T GetRepository<T>() where T : class;
        T GetRepository<T>(IContextWithTransaction context) where T : class;
        void SaveChanges();
        void RegisterContext(IContextWithTransaction context);
        IContextWithTransaction GetContext<T>(T repository = null) where T : class;
    }
}
