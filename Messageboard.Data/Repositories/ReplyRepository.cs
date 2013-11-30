using System.Data.Entity;
using Messageboard.Domain.Models;
using Messageboard.Domain.Repositories;

namespace Messageboard.Data.Repositories
{
    public class ReplyRepository: Repository<Reply>, IReplyRepository
    {
        public ReplyRepository(DbContext context) : base(context)
        {
        }
    }
}
