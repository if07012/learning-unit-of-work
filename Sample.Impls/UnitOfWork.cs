using Sample.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sample.Domains.Repositories;
using SimpleInjector;
using Sample.Domains.Helpers;

namespace Sample.Impls
{
    public class UnitOfWork : IUnitOfWork
    {
        private IContextWithTransaction context;

        public UnitOfWork(IContextWithTransaction context)
        {
            this.context = context;
        }
        public IContextWithTransaction GetContext<T>(T repository = null) where T : class
        {

            if (typeof(T).GetCustomAttributes(true).Any(n => n.GetType() == typeof(ContextAttribute)))
            {
                var context = typeof(T).GetCustomAttributes(true).FirstOrDefault(n => n.GetType() == typeof(ContextAttribute)) as ContextAttribute;

                if (contexts.Any(n => n.GetType() == context.Type))
                {
                    return contexts.FirstOrDefault(n => n.GetType() == context.Type);
                }
                var instace = Activator.CreateInstance(context.Type) as IContextWithTransaction;
                RegisterContext(instace);
                return instace;
            }
            return context;
        }

        public T GetRepository<T>() where T : class
        {
            var instance = Helpers.GetContainer().GetInstance<T>();
            //if (instance is IBaseRepository)
            //    (instance as IBaseRepository).Context = Helpers.GetContainer().GetInstance<IContext>();
            return instance;
        }
        public T GetRepository<T>(IContextWithTransaction context) where T : class
        {
            return default(T);
        }
        private List<IContextWithTransaction> contexts = new List<IContextWithTransaction>();
        public void RegisterContext(IContextWithTransaction context)
        {
            if (!contexts.Any(n => n.Equals(context)))
                contexts.Add(context);
        }

        public void SaveChanges()
        {
            foreach (var item in contexts)
            {
                if (item is IContextWithTransaction)
                    (item as IContextWithTransaction).SaveChanges();
            }
        }
    }
}
