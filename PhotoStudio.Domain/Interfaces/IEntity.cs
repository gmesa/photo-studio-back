using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoStudio.Domain.Interfaces
{
    /// <summary>
    /// Generic abstraction for a domain entity
    /// </summary>
    /// <typeparam name="TId"></typeparam>
    public  class IEntity<TId>
    {
        public TId Id { get; set; }

        public byte[] RowVersion { get; set; }
    }
}
