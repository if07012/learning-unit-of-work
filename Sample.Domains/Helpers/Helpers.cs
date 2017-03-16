using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Domains.Helpers
{
    public class Helpers
    {
        private static Container containerLocal;

        public static Container GetContainer()
        {
            return containerLocal;
        }
        public static void SetContainer(Container container)
        {
            containerLocal = container;
        }

    }
}
