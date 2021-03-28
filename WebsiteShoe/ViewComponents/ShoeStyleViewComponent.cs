using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebsiteShoe.Entities;
using WebsiteShoe.Models;

namespace WebsiteShoe.ViewComponents
{
    public class ShoeStyleViewComponent : ViewComponent
    {
        private readonly ShoeDbContext _dbContext;
        public ShoeStyleViewComponent(ShoeDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var lst = _dbContext.ShoeStyles.Select(x => new Models.ShoeStyle
            {
                StyleId = x.StyleId,
                StyleName = x.StyleName,
                Quantity = _dbContext.Shoes.Count(a => a.StyleId == x.StyleId),
                ProducerName = _dbContext.ShoeStyles.Where(b => b.StyleId == x.StyleId).FirstOrDefault().Producer.ProducerName
            }).ToList();
            return View(lst);
        }
    }
}
