using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Messageboard.Domain.Models;

namespace Messageboard.Data.Database
{
    public class MessageboardContext: DbContext
    {
        public MessageboardContext(string nameOrConnectionString) : base(nameOrConnectionString)
        {
        }

        public DbSet<Topic> Topics { get; set; }
        public DbSet<Reply> Replies { get; set; }

        public MessageboardContext(): this("MessageboardContext")
        {
        }
    }
}
