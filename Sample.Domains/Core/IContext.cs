using Sample.Domains.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Domains.Repositories
{
    public interface IContext
    {
        void Add<T>(T model) where T: TIdentity;
        IEnumerable<T> Gets<T>() where T : TIdentity;
        void Update<T>(T user) where T : TIdentity;
    }    
}
