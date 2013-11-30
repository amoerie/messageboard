using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Messageboard.Domain.Repositories;

namespace Messageboard.Domain
{
    public interface IMessageboardService
    {
        ITopicRepository Topics { get; }
        IReplyRepository Replies { get; }
        Task<int> SaveChangesAsync();
    }
}
