using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messageboard.Domain.Contracts
{
    public interface IIdentifiable
    {
        /// <summary>
        /// Gets or sets the unique id for this object
        /// </summary>
        int Id { get; set; }
    }
}
