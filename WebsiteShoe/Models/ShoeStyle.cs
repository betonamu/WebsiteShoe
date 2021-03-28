using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebsiteShoe.Models
{
    public class ShoeStyle
    {
        public int StyleId { get; set; }
        public string StyleName { get; set; }
        public int Quantity { get; set; }
        public string ProducerName { get; set; }
    }
}
