using Sample.Domains.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Domains
{
    public class Message : TIdentity
    {
        public int Id { get; set; }
        public User User { get; set; }
        public string ContentMessage { get; set; }
    }
}
