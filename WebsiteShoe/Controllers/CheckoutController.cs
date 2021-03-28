using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using WebsiteShoe.Entities;
using WebsiteShoe.Models;
using WebsiteShoe.Helper;
using MimeKit;
using MailKit.Net.Smtp;

namespace WebsiteShoe.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly ShoeDbContext _dbContext;
        public CheckoutController(ShoeDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            try
            {
                List<Cart> lstCart = SessionHelper.GetObjectFromJson<List<Cart>>(HttpContext.Session, "Cart");
                ViewBag.Cart = lstCart;
                ViewBag.TotalPrice = lstCart.Sum(c => c.Quantity * c.Price);
                string previousUrlPage = Request.Headers["Referer"].ToString();
                if (HttpContext.Session.GetString("username") == null)
                {
                    HttpContext.Session.SetString("previousUrlPage", previousUrlPage);
                    return RedirectToAction("Login", "Auth");
                }
                else
                {
                    return View();
                }
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        [HttpPost]
        public IActionResult Order(Bill objBill)
        {
            Bill bill = new Bill();
            var sessionCustomer = SessionHelper.GetObjectFromJson<Customer>(HttpContext.Session, "Customer");
            var customerId = sessionCustomer.CustomerId;
            var lstCart = SessionHelper.GetObjectFromJson<List<Cart>>(HttpContext.Session, "Cart");
            bill.CustomerId = (int)customerId;
            bill.CreateAt = DateTime.Now;
            bill.Status = false;
            bill.DeliveryDate = objBill.DeliveryDate;
            _dbContext.Bills.Add(bill);
            _dbContext.SaveChanges();

            foreach (var item in lstCart)
            {
                BillDetail billDetail = new BillDetail()
                {
                    BillId = bill.BillId,
                    ShoeId = item.ShoeId,
                    Quantity = item.Quantity,
                    TotalPrice = item.Quantity * item.Price,
                };
                _dbContext.BillDetails.Add(billDetail);
            }
            _dbContext.SaveChanges();

            var emailCustomer = sessionCustomer.Email;
            var nameCustomer = sessionCustomer.DisplayName;
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Xp Snake", "nguyencongquoc0904@gmail.com"));
            message.To.Add(new MailboxAddress(nameCustomer, emailCustomer));
            message.Subject = "Đơn hàng từ Xp Snake";

            message.Body = new TextPart("plain")
            {
                Text = @"Xin chào " + nameCustomer + ",Chúng tôi đã ghi nhận đơn hàng của bạn. Mã đơn hàng của bạn là :" + bill.BillId ,
            };

            using (var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587, false);

                // Note: only needed if the SMTP server requires authentication
                client.Authenticate("nguyencongquoc0904@gmail.com", "zfrqhdxvqopmgmru");

                client.Send(message);
                client.Disconnect(true);
            }

                return View("OrderSuccess");
        }

        public IActionResult OrderSuccess()
        {
            SessionHelper.GetObjectFromJson<List<Cart>>(HttpContext.Session, "Cart").Clear();
            return View();
        }
    }
}
