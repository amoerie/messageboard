using System.Data.Entity;
using System.Threading.Tasks;
using Messageboard.Domain;
using Messageboard.Domain.Repositories;

namespace Messageboard.Data
{
    public class MessageboardService: IMessageboardService
    {
        private readonly DbContext _context;

        public MessageboardService(
            DbContext context,
            ITopicRepository topics, 
            IReplyRepository replies)
        {
            _context = context;
            Replies = replies;
            Topics = topics;
        }

        public ITopicRepository Topics { get; private set; }
        public IReplyRepository Replies { get; private set; }

        public Task<int> SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}
