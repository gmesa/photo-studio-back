using PhotoStudio.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoStudio.Domain.Entities
{
    public class Size : IEntity<int>
    {        
        public string SizeName { get; set; }

        public virtual ICollection<PhotoBook> PhotoBooks { get; set; }

    }
}
