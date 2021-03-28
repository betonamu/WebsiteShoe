using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebsiteShoe.Entities
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public bool Sex { get; set; }
        public string DisplayName { get; set; }
        public DateTime DateOfBirth { get; set; }

        public ICollection<Bill> Bills { get; set; }
    }
}
