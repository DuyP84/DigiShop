using DigiShop.Data;
using DigiShop.Models;
using DigiShop.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using X.PagedList;
using X.PagedList.Mvc.Core;

namespace DigiShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}
        private readonly ApplicationDbContext _context;
        

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }
        //Product Name = sort theo Name
        public IActionResult Index(int? page)
        {
            int pageSize = 6;
            int pageNumber = page ?? 1;
            var lstsp = _context.Products.AsNoTracking().OrderBy(x => x.ProductName);
            var pagedList = new PagedList<Product>(lstsp, pageNumber, pageSize);
            return View(pagedList);
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
    }
}