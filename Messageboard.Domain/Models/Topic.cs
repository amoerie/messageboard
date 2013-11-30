using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Messageboard.Domain.Contracts;

namespace Messageboard.Domain.Models
{
    public class Topic: IIdentifiable
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }

        public DateTime CreationDate { get; set; }

        public ICollection<Reply> Replies { get; set; }
    }
}
