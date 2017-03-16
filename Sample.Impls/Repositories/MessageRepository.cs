using Sample.Domains.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sample.Domains;
using Sample.Impls.Context;

namespace Sample.Impls.Repositories
{
    [Context(Type = typeof(InMemoryContext))]
    public class MessageRepository : IMessageRepository
    {
        public MessageRepository(IUnitOfWork unitOfWork)
        {
            this.context = unitOfWork.GetContext<MessageRepository>();
            unitOfWork.RegisterContext(context);
        }
        private IContextWithTransaction context;
        public IContextWithTransaction Context
        {
            get
            {
                return context;
            }
        }

        public Message AddMessage(User user, Message message)
        {
            context.Add(message);
            return message;
        }

        public IEnumerable<Message> GetMessages()
        {
            return context.Gets<Message>();
        }
    }
}
