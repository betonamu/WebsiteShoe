using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebsiteShoe.Entities;
using WebsiteShoe.Helper;
using WebsiteShoe.Models;

namespace WebsiteShoe.Controllers
{
    public class CartController : Controller
    {
        private readonly ShoeDbContext _dbContext;

        public CartController(ShoeDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [Route("/Cart")]
        public IActionResult Index()
        {
            List<Cart> lstCart = SessionHelper.GetObjectFromJson<List<Cart>>(HttpContext.Session, "Cart");
            ViewBag.Cart = lstCart;
            if (lstCart != null)
            {
                ViewBag.TotalPrice = lstCart.Sum(c => c.Quantity * c.Price);
            }
            else
            {
                ViewBag.TotalProce = 0;
            }
            return View();
        }

        [HttpPost]
        public JsonResult AddToCart([FromBody] GetValueJson valueJson)
        {
            var item = _dbContext.Shoes.Find(valueJson.ShoeId);
            if (SessionHelper.GetObjectFromJson<List<Cart>>(HttpContext.Session, "Cart") == null)
            {
                var cart = new List<Cart>();
                cart.Add(new Cart()
                {
                    ShoeId = valueJson.ShoeId,
                    ShoeName = item.ShoeName,
                    Images = item.Images,
                    Price = item.Price,
                    Quantity = valueJson.Quantity,
                });
                SessionHelper.SetObjectAsJson(HttpContext.Session, "Cart", cart);
            }
            else
            {
                var cart = SessionHelper.GetObjectFromJson<List<Cart>>(HttpContext.Session, "Cart");
                int index = IsExitst(valueJson.ShoeId);
                if (index != -1)
                {
                    cart[index].Quantity += valueJson.Quantity;
                }
                else
                {
                    cart.Add(new Cart()
                    {
                        ShoeId = valueJson.ShoeId,
                        ShoeName = item.ShoeName,
                        Images = item.Images,
                        Price = item.Price,
                        Quantity = valueJson.Quantity,
                    });
                }
                SessionHelper.SetObjectAsJson(HttpContext.Session, "Cart", cart);
            }
            var lstCart = SessionHelper.GetObjectFromJson<List<Cart>>(HttpContext.Session, "Cart");
            return Json(lstCart);
        }

        [HttpPost]
        public JsonResult UpdateQuantity([FromBody] GetValueJson valueJson)
        {
            var cart = SessionHelper.GetObjectFromJson<List<Cart>>(HttpContext.Session, "Cart");
            int index = IsExitst(valueJson.ShoeId);
            if (index != -1)
            {
                cart[index].Quantity = valueJson.Quantity;
            }
            SessionHelper.SetObjectAsJson(HttpContext.Session, "Cart", cart);
            return Json(cart[index]);
        }

        public int IsExitst(int id)
        {
            List<Cart> cart = SessionHelper.GetObjectFromJson<List<Cart>>(HttpContext.Session, "Cart");
            for (int i = 0; i < cart.Count; i++)
            {
                if (cart[i].ShoeId == id)
                {
                    return i;
                }
            }
            return -1;
        }

        public IActionResult RemoveFromCart(int id)
        {
            List<Cart> lst = SessionHelper.GetObjectFromJson<List<Cart>>(HttpContext.Session, "Cart");
            int index = IsExitst(id);
            lst.RemoveAt(index);
            SessionHelper.SetObjectAsJson(HttpContext.Session, "Cart", lst);
            return RedirectToAction("Index");
        }

        public IActionResult DeleteAllCart()
        {
            if (SessionHelper.GetObjectFromJson<List<Cart>>(HttpContext.Session, "Cart") != null)
            {
                SessionHelper.GetObjectFromJson<List<Cart>>(HttpContext.Session, "Cart").Clear();
            }
            return View("Index");
        }
    }

    //This class use to Get value Json from Ajax. Because [FormBody] chỉ đọc tối đa 1 tham số
    public class GetValueJson
    {
        public int ShoeId { get; set; }
        public int Quantity { get; set; }
    }
}
