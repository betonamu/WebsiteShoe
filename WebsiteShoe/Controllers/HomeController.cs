using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MimeKit;
using WebsiteShoe.Entities;
using WebsiteShoe.Models;

namespace WebsiteShoe.Controllers
{
    public class HomeController : Controller
    {
        private readonly ShoeDbContext _dbContext;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger,ShoeDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            var lst = _dbContext.Shoes.Take(8).ToList();
            return View(lst);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Route("/Home/HandleError/{code:int}")]
        public IActionResult HandleError(int code)
        {
            ViewData["ErrorMessage"] = $"Error occurred. The ErrorCode is: {code}";
            return View("~/Home/Error404.cshtml");
        }
        
        public IActionResult Size()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }
    }
}
