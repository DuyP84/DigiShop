using DigiShop.Data;
using DigiShop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NPOI.SS.Formula.Functions;
using System.Runtime.InteropServices;
using X.PagedList;

namespace DigiShop.Areas.Admin.Controllers
{
    [Authorize]
    [Area("admin")]
    [Route("admin")]
    [Route("admin/home")]
    public class AdminController : Controller
    {

        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext db)
        {
            _context = db;
        }

        [Route("")]
        [Route("index")]

        public IActionResult Index()
        {
            return View();
        }
        [Route("danhmucsanpham")]
        public IActionResult Danhmucsanpham(int? page)
        {
            int pageSize = 10;
            int pageNumber = page ?? 1;
            var lstsp = _context.Products.AsNoTracking().OrderBy(x => x.ProductId);
            var pagedList = new PagedList<Product>(lstsp, pageNumber, pageSize);
            return View(pagedList);
        }
        [Route("AddNew")]
        [HttpGet]
        public IActionResult AddNew()
        {
            ViewBag.CategoryId = new SelectList(_context.Categories.ToList(), "CategoryId", "CategoryName");
            ViewBag.ColorId = new SelectList(_context.Colors.ToList(), "ColorId", "ColorName");
            ViewBag.SizeId = new SelectList(_context.Sizes.ToList(), "SizeId", "SizeName");

            return View();
        }

        [Route("AddNew")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddNew(Product product)
        {
            if(ModelState.IsValid) { 
            _context.Products.Add(product);
            _context.SaveChanges();
            return RedirectToAction("Danhmucsanpham");
            }
            return View(product);
        }

        [Route("Update")]
        [HttpGet]
        public IActionResult Update(int ProductId)
        {
            ViewBag.CategoryId = new SelectList(_context.Categories.ToList(), "CategoryId", "CategoryName");
            ViewBag.ColorId = new SelectList(_context.Colors.ToList(), "ColorId", "ColorName");
            ViewBag.SizeId = new SelectList(_context.Sizes.ToList(), "SizeId", "SizeName");
            var sanPham = _context.Products.Find(ProductId);
            return View(sanPham);
        }

        [Route("Update")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(product).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Danhmucsanpham", "Admin");
            }
            return View(product);
        }


        [Route("Delete")]
        [HttpGet]
        public IActionResult Delete(int productId)
        {
            TempData["Message"] = "";
            var chitietSP = _context.Products.FirstOrDefault(x => x.ProductId == productId);
            if (chitietSP == null)
            {
                TempData["Message"] = "Không tìm thấy sản phẩm";
                return RedirectToAction("Danhmucsanpham", "Admin");
            }
            //var anhSP = _context.Products.Where(x => x.ProductId == productId);
            //if(anhSP.Any())
            //{
            //    _context.RemoveRange(anhSP);

            //}
            _context.Products.Remove(chitietSP); // Xoa san pham
            _context.SaveChanges();
            TempData["Message"] = "Sản phẩm đã được xóa";
            return RedirectToAction("Danhmucsanpham", "Admin");
        }
        //Các thay đổi đã được thực hiện:

        //Thay vì sử dụng Where kết hợp với ToList(),
        //chúng ta sử dụng FirstOrDefault để tìm kiếm một sản phẩm duy nhất dựa trên ProductId.
        //Kiểm tra xem sản phẩm có tồn tại hay không bằng cách kiểm tra xem
        //chitietSP có bằng null hay không.
        //Thay vì sử dụng _context.Products.Find(productId)
        //để tìm kiếm sản phẩm cần xóa, chúng ta đã tìm thấy sản phẩm đó trước đó bằng
        //FirstOrDefault và sử dụng Remove để xóa sản phẩm đó.

        [Route("quanlydanhmuc")]
        public IActionResult Quanlydanhmuc(int? page)
        {
            
            var lstcategories = _context.Categories.AsNoTracking().OrderBy(x => x.CategoryId);
            return View(lstcategories);
        }

        [Route("AddNewCategories")]
        [HttpGet]
        public IActionResult AddNewCategories()
        {
            return View();
        }

        [Route("AddNewCategories")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddNewCategories(Category category)
        {
            if (ModelState.IsValid)
            {
                _context.Categories.Add(category);
                _context.SaveChanges();
                return RedirectToAction("Danhmucsanpham");
            }
            return View(category);
        }

        [Route("UpdateCategories")]
        [HttpGet]
        public IActionResult UpdateCategories(int CategoryId)
        {
            
            var category = _context.Categories.Find(CategoryId);
            return View(category);
        }

        [Route("UpdateCategories")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateCategories(Category category)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(category).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Quanlydanhmuc", "Admin");
            }
            return View(category);
        }


        [Route("DeleteCategories")]
        [HttpGet]
        public IActionResult DeleteCategories(int categoryId)
        {
            TempData["Message"] = "";
            var chitietSP = _context.Categories.FirstOrDefault(x => x.CategoryId == categoryId);
            if (chitietSP == null)
            {
                TempData["Message"] = "Không tìm thấy ";
                return RedirectToAction("Quanlydanhmuc", "Admin");
            }
            
            _context.Categories.Remove(chitietSP); // Xoa san pham
            _context.SaveChanges();
            TempData["Message"] = "Category đã được xóa";
            return RedirectToAction("Quanlydanhmuc", "Admin");
        }
    }
}

