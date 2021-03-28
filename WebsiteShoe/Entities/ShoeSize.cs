using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebsiteShoe.Entities
{
    public class ShoeSize
    {
        [Column("SizeId")]
        public int SizeId { get; set; }
        public int Size { get; set; }

        //Foreign Key

        public IList<ShoeDetail> ShoeDetails { get; set; }
    }
}
