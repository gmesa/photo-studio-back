using PhotoStudio.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoStudio.Domain.Entities
{
    public class Material : IEntity<int>
    {
        public Material()
        {
            PhotoBooks = new HashSet<PhotoBook>();
        }
        public string MaterialName { get; set; }

        public virtual ICollection<PhotoBook> PhotoBooks { get; set; }
    }
}
