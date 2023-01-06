using PhotoStudio.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoStudio.Domain.Entities
{
    public class PhotoBook :IEntity<int>
    {
        public int MaterialId { get; set; } 
        public Material Material { get; set; }

        public int SizeId { get; set; }
        public Size Size { get; set; } 

        public decimal PortadaPrice { get; set; }
        public decimal PriceByPage { get; set; }
        
    }
}
