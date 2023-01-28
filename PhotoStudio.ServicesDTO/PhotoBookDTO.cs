using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoStudio.ServicesDTO
{
    public class PhotoBookDTO
    {
        public int Id { get; set; }
        public int MaterialId { get; set; }
        public int SizeId { get; set; }
        public decimal PortadaPrice { get; set; }
        public decimal PriceByPage { get; set; }
    }
}
