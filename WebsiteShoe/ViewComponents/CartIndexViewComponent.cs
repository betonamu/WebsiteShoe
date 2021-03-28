using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebsiteShoe.Entities;
using WebsiteShoe.Helper;
using WebsiteShoe.Models;

namespace WebsiteShoe.ViewComponents
{
    public class CartIndexViewComponent : ViewComponent
    {
        private readonly ShoeDbContext _dbContext;
        public CartIndexViewComponent(ShoeDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var lstCart = SessionHelper.GetObjectFromJson<List<Cart>>(HttpContext.Session, "Cart");
            return View(lstCart);
        }
    }
}
