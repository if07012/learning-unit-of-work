using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Domains.Repositories
{
    public interface IContextWithTransaction: IContext
    {
        Exception SaveChanges();
    }    
}
