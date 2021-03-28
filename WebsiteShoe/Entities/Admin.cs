using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebsiteShoe.Entities
{
    public class Admin
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Vui long nhap Username")]
        public string Username { get; set; }
        public string Password { get; set; }
        public string DisplayName { get; set; }
    }
}
