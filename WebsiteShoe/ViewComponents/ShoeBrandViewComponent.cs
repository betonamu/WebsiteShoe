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
    public class ShoeBrandViewComponent : ViewComponent
    {
        private readonly ShoeDbContext _dbContext;
        public ShoeBrandViewComponent(ShoeDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var lst = _dbContext.Producers.Select(x => new ShoeBrand
            {
                ProductId = x.ProducerId,
                ProductName = x.ProducerName,
                Quantity = _dbContext.Shoes.Count(a => a.ShoeStyle.ProducerId == x.ProducerId)
            }).ToList();
            return View(lst);
        }
    }
}
