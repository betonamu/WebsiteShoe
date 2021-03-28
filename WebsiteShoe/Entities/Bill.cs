using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebsiteShoe.Entities
{
    public class Bill
    {
        [Column("BillId")]
        public int BillId { get; set; }
        public DateTime CreateAt { get; set; }
        public bool Status { get; set; }
        public DateTime DeliveryDate { get; set; }
        public int CustomerId { get; set; }

        public Customer Customer { get; set; }
        public IList<BillDetail> BillDetails { get; set; }
    }
}
