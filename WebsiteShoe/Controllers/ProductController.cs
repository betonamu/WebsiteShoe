using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebsiteShoe.Entities;
using WebsiteShoe.Models;

namespace WebsiteShoe.Controllers
{
    public class ProductController : Controller
    {
        private readonly ShoeDbContext _dbContext;
        public ProductController(ShoeDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        //[Route("/Product")]
        //[Route("/Product/page{i}")]
        public IActionResult ProductIndex(int page = 1)
        {
            double totalRecord  = _dbContext.Shoes.Count();
            double limit = 8;
            if (page < 1)
            {
                page = 1;
            }
            double totalPage = Math.Ceiling(totalRecord / limit);
            ViewBag.TotalPage = totalPage;
            if(totalPage < page)
            {
                page = (int)totalPage;
            }
            double start = (page - 1) * limit;
            var lst = _dbContext.Shoes.Include(a=>a.ShoeStyle)
                .ThenInclude(a=>a.Producer)
                .Skip((int)start).Take((int)limit).ToList();
            return View(lst);
        }

        [HttpGet]
        public IActionResult ProductDetail(int id)
        {
            Shoe lst = _dbContext.Shoes.Find(id);
            return View(lst);
        }

        [HttpGet]
        public IActionResult Cart()
        {
            return View();
        }

        [Route("/Product/Nike/{Style}")]
        [Route("/Product/Adidas/{Style}")]
        public IActionResult GetProductByStyleId(string Style)
        {
            var lst = _dbContext.Shoes.Include(a => a.ShoeStyle)
                .ThenInclude(a => a.Producer)
                .Where(a =>a.ShoeStyle.StyleName == Style).ToList();
            return View("ProductIndex",lst);
        }

        [Route("/Product/{nameProducer}")]
        public IActionResult GetProducer(string nameProducer)
        {
            var lst = _dbContext.Shoes.Include(a => a.ShoeStyle)
                .ThenInclude(a => a.Producer).Where(a => a.ShoeStyle.Producer.ProducerName.Equals(nameProducer)).ToList();
            return View("ProductIndex", lst);
        }

        [HttpGet]
        public JsonResult Search(string term)
        {
            var lst = _dbContext.Shoes.Where(c => c.ShoeName.Contains(term)).ToList();
            return Json(lst);   
        }
    }
}