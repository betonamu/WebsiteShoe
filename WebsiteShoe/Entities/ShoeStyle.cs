using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebsiteShoe.Entities
{
    public class ShoeStyle
    {
        [Column("StyleId")]
        public int StyleId { get; set; }
        public string StyleName { get; set; }
        public int ProducerId { get; set; }

        public Producer Producer { get; set; }
        public ICollection<Shoe> Shoes { get; set; }
    }
}
