using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebsiteShoe.Entities;
using WebsiteShoe.Models;

namespace WebsiteShoe.ViewComponents
{
    public class NikeStyleViewComponent : ViewComponent
    {
        private ShoeDbContext _dbContext;
        public NikeStyleViewComponent(ShoeDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var lst = _dbContext.ShoeStyles.Where(a => a.ProducerId == 1).Select(x => new ShoeStyleHeader
            {
                StyleId = x.StyleId,
                StyleName = x.StyleName,
            }).ToList();
            return View(lst);
        }
    }
}
