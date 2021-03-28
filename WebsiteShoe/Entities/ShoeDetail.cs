using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebsiteShoe.Entities
{
    public class ShoeDetail
    {
        public int SizeId { get; set; }
        public int ShoeId { get; set; }
        public long Quantity { get; set; }

        //Foreign Key
        public Shoe Shoe { get; set; }
        public ShoeSize ShoeSize { get; set; }
    }
}
