using DigiShop.Data;
using DigiShop.Models;
using DigiShop.SessionExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
namespace DigiShop.Controllers
{
	public class CartController : Controller
	{
		public Checkout? Cart { get; set; }
		//ket noi database
		private readonly ApplicationDbContext _context;

		public CartController(ApplicationDbContext context)
		{
			_context = context;
		}

		public IActionResult Index()
		{
			return View("Cart", HttpContext.Session.GetJson<Checkout>("cart"));
		}
        [Authorize]
        public IActionResult AddToCart(int productId)
		{
			Product? product = _context.Products.FirstOrDefault(p => p.ProductId == productId); 
			//neu san pham ton tai
			if (product != null)
			{
				Cart = HttpContext.Session.GetJson<Checkout>("cart") ?? new Checkout();
				Cart.AddItem(product, 1);
				HttpContext.Session.SetJson("cart", Cart);
			}
			return View("Cart", Cart);
		}
		//ham nay dung cho "-", giam 1 san pham 
		public IActionResult UpdateCart(int productId)
		{
			Product? product = _context.Products.FirstOrDefault(p => p.ProductId == productId);
			
			if (product != null)
			{
				Cart = HttpContext.Session.GetJson<Checkout>("cart") ?? new Checkout();
				Cart.AddItem(product, -1);
				HttpContext.Session.SetJson("cart", Cart);
			}
			return View("Cart", Cart);
		}

		public IActionResult RemoveFromCart(int productId)
		{
			Product? product = _context.Products.FirstOrDefault(p => p.ProductId == productId);
			//neu san pham ton tai
			if (product != null)
			{
				Cart = HttpContext.Session.GetJson<Checkout>("cart");
				Cart.RemoveLine(product);
				HttpContext.Session.SetJson("cart", Cart);
			}
			return View("Cart", Cart);
		}

	}
}
