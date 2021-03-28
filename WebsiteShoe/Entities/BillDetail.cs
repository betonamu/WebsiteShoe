using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebsiteShoe.Entities
{
    public class BillDetail
    {
        public int BillId { get; set; }
        public int ShoeId { get; set; }
        public int Quantity { get; set; }
        public double TotalPrice { get; set; }


        public Bill Bill { get; set; }
        public Shoe Shoe { get; set; }
    }
}
