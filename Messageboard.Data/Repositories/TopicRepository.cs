using System.Data.Entity;
using Messageboard.Domain.Models;
using Messageboard.Domain.Repositories;

namespace Messageboard.Data.Repositories
{
    public class TopicRepository: Repository<Topic>, ITopicRepository
    {
        public TopicRepository(DbContext context) : base(context)
        {
        }
    }
}
