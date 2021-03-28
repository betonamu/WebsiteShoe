using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebsiteShoe.Entities
{
    public class Producer
    {
        public int ProducerId { get; set; }
        public string ProducerName { get; set; }

        public ICollection<ShoeStyle> ShoeStyles { get; set; }
    }
}
