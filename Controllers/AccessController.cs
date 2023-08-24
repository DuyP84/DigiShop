using DigiShop.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DigiShop.Controllers
{
    public class AccessController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccessController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Login()
        {
            if (HttpContext.Session.GetString("UserName") == null)
            {
            return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        
        
    }
}
