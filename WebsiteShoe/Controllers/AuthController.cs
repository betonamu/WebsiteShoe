using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebsiteShoe.Common;
using WebsiteShoe.Entities;
using WebsiteShoe.Helper;
using WebsiteShoe.Models;

namespace WebsiteShoe.Controllers
{
    public class AuthController : Controller
    {
        private readonly ShoeDbContext _dbContext;

        public AuthController(ShoeDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("/Login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [Route("/Login")]
        public IActionResult Login(LoginModel objLogin)
        {
            //if (objLogin.Username == "1")
            //{
            //ModelState.AddModelError(nameof(objLogin.Username), "Con bo");
            //}
            if (ModelState.IsValid)
            {
                var dbLogin = _dbContext.Customers.Where(c => c.Email == objLogin.Username || c.UserName == objLogin.Username && c.Password == objLogin.Password).ToList();
                if (dbLogin.Count == 1)
                {
                    var displayName = _dbContext.Customers.FirstOrDefault(c => c.UserName == objLogin.Username || c.Email == objLogin.Username).DisplayName;
                    HttpContext.Session.SetString("username", displayName);
                    SessionHelper.SetObjectAsJson(HttpContext.Session, "Customer", dbLogin.FirstOrDefault());
                    string returnUrl = HttpContext.Session.GetString("previousUrlPage");
                    if (returnUrl != null)
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            return View();
        }

        [HttpGet]
        [Route("/SignIn")]
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        [Route("/SignIn")]
        public IActionResult SignIn(RegisterModel objRegister)
        {
            Customer register = new Customer();
            register.DisplayName = objRegister.DisplayName;
            register.UserName = objRegister.UserName;
            register.Password = objRegister.Password;
            register.DateOfBirth = objRegister.DateOfBirth;
            register.PhoneNumber = objRegister.PhoneNumber;
            if (GlobalFunc.IsEmailValid(objRegister.Email))
            {
                register.Email = objRegister.Email;
            }
            else
            {
                ModelState.AddModelError(nameof(objRegister.Email), "Vui lòng nhập email để chúng tôi đăng ký giúp bạn");
                return this.SignIn();
            }
            register.Address = objRegister.Address;
            register.Sex = objRegister.Sex;
            _dbContext.Customers.Add(register);
            _dbContext.SaveChanges();
            return View("Login");
        }


        public IActionResult LogOut()
        {
            HttpContext.Session.Remove("username");
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public JsonResult CheckUserNameExists([FromBody] string username)
        {
            bool isValid = !_dbContext.Customers.ToList().Exists(c => c.UserName.Equals(username, StringComparison.CurrentCultureIgnoreCase));
            return Json(isValid);
        }

        [HttpPost]
        public JsonResult CheckEmailExists([FromBody] string email)
        {
            bool isValid = !_dbContext.Customers.ToList().Exists(c => c.Email.Equals(email, StringComparison.CurrentCultureIgnoreCase));
            return Json(isValid);
        }
    }
}
