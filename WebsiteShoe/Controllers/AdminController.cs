using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebsiteShoe.Entities;
using WebsiteShoe.Helper;

namespace WebsiteShoe.Controllers
{
    public class AdminController : Controller
    {
        private readonly ShoeDbContext _dbContext;

        public AdminController(ShoeDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            var sessionAdmin = SessionHelper.GetObjectFromJson<Admin>(HttpContext.Session, "Admin");
            if (sessionAdmin == null)
            {
                return RedirectToAction("Login");
            }
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Login(Admin admin)
        {
            if (ModelState.IsValid)
            {
                var dbLogin = _dbContext.Admins.Where(a => a.Username.Equals(admin.Username) && a.Password == admin.Password).ToList();
                if (dbLogin.Count() != 0)
                {
                    HttpContext.Session.SetString("AdminUser", dbLogin.FirstOrDefault().DisplayName);
                    SessionHelper.SetObjectAsJson(HttpContext.Session, "Admin", dbLogin.FirstOrDefault());
                    return RedirectToAction("Index");
                }
            }
            return View();
        }

        [HttpGet]
        public IActionResult AddShoe()
        {
            ViewBag.ShoeStyle = new SelectList(_dbContext.ShoeStyles.ToList(), "StyleId", "StyleName");
            return View();
        }

        [HttpPost]
        public IActionResult AddShoe(Shoe objShoe, IFormFile fileUpload, string ckeditor)
        {
            ViewBag.ShoeStyle = new SelectList(_dbContext.ShoeStyles.ToList(), "StyleId", "StyleName");
            try
            {
                string uploadFolder = "";
                var getShoeStyle = _dbContext.ShoeStyles.Include(a => a.Producer).Where(a => a.StyleId == objShoe.StyleId).FirstOrDefault();
                if (getShoeStyle == null)
                {
                    ModelState.AddModelError(nameof(objShoe.StyleId), "Vui lòng chọn loại giày");
                    return View();
                }
                else
                {
                    uploadFolder = @"wwwroot/images/avtshoes/" + getShoeStyle.Producer.ProducerName.Replace(" ", "") + "/" + getShoeStyle.StyleName.Replace(" ", "");
                }
                if (!Directory.Exists(uploadFolder))
                {
                    Directory.CreateDirectory(uploadFolder);
                }
                var fileName = fileUpload.FileName;
                var savePath = "~" + uploadFolder.Substring(7) + "/" + fileName;
                if (System.IO.File.Exists(uploadFolder + "/" + fileName))
                {
                    ModelState.AddModelError(nameof(objShoe.Images), "Ảnh đã tồn tại! Vui lòng chọn ảnh khác.");
                    return View();
                }
                // combine là hàm nối chuỗi để tạo ra path
                // This is Example
                //string[] paths = {@"d:\archives", "2001", "media", "images"};
                //string fullPath = Path.Combine(paths);
                //Output:    d:\archives\2001\media\images
                var filePath = Path.Combine(uploadFolder, fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    fileUpload.CopyTo(fileStream);
                }

                Shoe shoe = new Shoe()
                {
                    ShoeName = objShoe.ShoeName,
                    StyleId = objShoe.StyleId,
                    Images = savePath,
                    Price = objShoe.Price,
                    Image360 = objShoe.Image360,
                };

                _dbContext.Shoes.Add(shoe);
                _dbContext.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return View();
        }

        [HttpGet]
        public IActionResult EditShoe(int id)
        {
            ViewBag.ShoeStyle = new SelectList(_dbContext.ShoeStyles.ToList(), "StyleId", "StyleName");
            var lst = _dbContext.Shoes.FirstOrDefault(a => a.ShoeId == id);
            return View(lst);
        }

        [HttpPost]
        public IActionResult EditShoe(Shoe objShoe, IFormFile fileUpload)
        {
            ViewBag.ShoeStyle = new SelectList(_dbContext.ShoeStyles.ToList(), "StyleId", "StyleName");
            try
            {
                string uploadFolder = "";
                var fileName = "";
                var getShoeStyle = _dbContext.ShoeStyles.Include(a => a.Producer).Where(a => a.StyleId == objShoe.StyleId).FirstOrDefault();
                if (getShoeStyle == null)
                {
                    ModelState.AddModelError(nameof(objShoe.StyleId), "Vui lòng chọn loại giày");
                    return View(_dbContext.Shoes.FirstOrDefault(a => a.ShoeId == objShoe.ShoeId));
                }
                else
                {
                    uploadFolder = @"wwwroot/images/avtshoes/" + getShoeStyle.Producer.ProducerName.Replace(" ", "") + "/" + getShoeStyle.StyleName.Replace(" ", "");
                }

                if (!Directory.Exists(uploadFolder))
                {
                    Directory.CreateDirectory(uploadFolder);
                }

                if (fileUpload != null)
                {
                    fileName = fileUpload.FileName;
                }
                var savePath = "~" + uploadFolder.Substring(7) + "/" + fileName;
                if (System.IO.File.Exists(uploadFolder + "/" + fileName))
                {
                    ModelState.AddModelError(nameof(objShoe.Images), "Ảnh đã tồn tại! Vui lòng chọn ảnh khác.");
                    return View(_dbContext.Shoes.FirstOrDefault(a => a.ShoeId == objShoe.ShoeId));
                }
                if (fileName != "")
                {
                    var filePath = Path.Combine(uploadFolder, fileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        fileUpload.CopyTo(fileStream);
                    }
                }

                var dbEdit = _dbContext.Shoes.FirstOrDefault(a => a.ShoeId == objShoe.ShoeId);
                dbEdit.ShoeName = objShoe.ShoeName;
                if (fileUpload != null)
                {
                    dbEdit.Images = savePath;
                }
                dbEdit.StyleId = objShoe.StyleId;
                dbEdit.Price = objShoe.Price;
                _dbContext.Shoes.Update(dbEdit);
                _dbContext.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);       
            }
            return RedirectToAction("ShoeManagement");
        }

        [HttpGet]
        public IActionResult DeleteShoe(int id)
        {
            var lst = _dbContext.Shoes.Single(a => a.ShoeId == id);
            return View(lst);
        }

        [HttpPost]
        public IActionResult DeleteShoeConfirm(int id)
        {
            var dbDelete = _dbContext.Shoes.First(a => a.ShoeId == id);
            _dbContext.Shoes.Remove(dbDelete);
            _dbContext.SaveChanges();
            return RedirectToAction("ShoeManagement");
        }

        [HttpGet]
        public IActionResult AddShoeStyle()
        {
            ViewBag.Producer = new SelectList(_dbContext.Producers.ToList(), "ProducerId", "ProducerName");
            return View();
        }

        [HttpPost]
        public IActionResult AddShoeStyle(ShoeStyle objShoeStyle)
        {
            ViewBag.Producer = new SelectList(_dbContext.Producers.ToList(), "ProducerId", "ProducerName");
            ShoeStyle shoeStyle = new ShoeStyle()
            {
                StyleName = objShoeStyle.StyleName,
                ProducerId = objShoeStyle.ProducerId,
            };

            _dbContext.ShoeStyles.Add(shoeStyle);
            _dbContext.SaveChanges();
            return View();
        }

        [HttpGet]
        public IActionResult EditShoeStyle(int id)
        {
            ViewBag.Producer = new SelectList(_dbContext.Producers.ToList(), "ProducerId", "ProducerName");
            var lst = _dbContext.ShoeStyles.Single(a => a.StyleId == id);
            return View(lst);
        }

        [HttpPost]
        public IActionResult EditShoeStyle(ShoeStyle objStyleShoe)
        {
            ViewBag.Producer = new SelectList(_dbContext.Producers.ToList(), "ProducerId", "ProducerName");
            var dbEdit = _dbContext.ShoeStyles.FirstOrDefault(a => a.StyleId == objStyleShoe.StyleId);
            dbEdit.StyleName = objStyleShoe.StyleName;
            dbEdit.ProducerId = objStyleShoe.ProducerId;
            _dbContext.ShoeStyles.Update(dbEdit);
            _dbContext.SaveChanges();
            return RedirectToAction("ShoeStyleManagement");
        }

        [HttpGet]
        public IActionResult DeleteShoeStyle(int id)
        {
            var lst = _dbContext.ShoeStyles.Single(a=>a.StyleId == id);
            return View(lst);
        }

        [HttpPost]
        public IActionResult DeleteShoeStyleConfirm(int id)
        {
            var dbDelete = _dbContext.ShoeStyles.First(a => a.StyleId == id);
            _dbContext.ShoeStyles.Remove(dbDelete);
            _dbContext.SaveChanges();
            return RedirectToAction("ShoeManagement");
        }

        public IActionResult BillManagement()
        {
            var lst = _dbContext.Bills.Include(a => a.Customer)
                                    .Include(b => b.BillDetails).ToList();
            return View(lst);
        }

        public IActionResult ShoeManagement()
        {
            var lst = _dbContext.Shoes.Include(a => a.ShoeStyle).ToList();
            return View(lst);
        }

        public IActionResult ShoeStyleManagement()
        {
            var lst = _dbContext.ShoeStyles.Include(a=>a.Producer).ToList();
            return View(lst);
        }
        [HttpGet]
        public IActionResult DeleteBill(int id)
        {
            var lst = _dbContext.Bills.FirstOrDefault(a => a.BillId == id);
            return View(lst);
        }
    }
}
