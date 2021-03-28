using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace WebsiteShoe.Models
{
    public class RegisterModel
    {
        [DisplayName("Họ và tên")]
        public string DisplayName { get; set; }
        [DisplayName("Ngày sinh")]
        public DateTime DateOfBirth { get; set; }
        [DisplayName("Tài khoản")]
        public string UserName { get; set; }
        [DisplayName("Mật khẩu")]
        public string Password { get; set; }
        [DisplayName("Nhập lại mật khẩu")]
        public string ConfirmPassword { get; set; }
        [DisplayName("Email")]
        public string Email { get; set; }
        [DisplayName("Số điện thoại")]
        public string PhoneNumber { get; set; }
        [DisplayName("Địa chỉ")]
        public string Address { get; set; }
        [DisplayName("Giới tính")]
        public bool Sex { get; set; }
    }
}
