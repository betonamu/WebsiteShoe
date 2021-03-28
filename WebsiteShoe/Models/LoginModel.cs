using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebsiteShoe.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Vui lòng nhập Username hoặc Email!")]
        public string Username { get; set; }

        public string Email { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập Password!")]
        public string Password { get; set; }
    }
}
