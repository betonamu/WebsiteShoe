using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebsiteShoe.Entities
{
    public class Shoe
    {
        [Column("ShoeId")]
        public int ShoeId { get; set; }
        public string ShoeName { get; set; }
        public int StyleId { get; set; }
        public string Images { get; set; }
        public double Price { get; set; }
        public string Image360 { get; set; }

        public ShoeStyle ShoeStyle { get; set; }
        public IList<BillDetail> BillDetails { get; set; }
        public IList<ShoeDetail> ShoeDetails { get; set; }
    }
}
