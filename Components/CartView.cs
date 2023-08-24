using DigiShop.Data;
using DigiShop.Models;
using DigiShop.SessionExtensions;
using Microsoft.AspNetCore.Mvc;

namespace DigiShop.Components
{
    public class CartView:ViewComponent
    {
        
        public IViewComponentResult Invoke()
        {
            return View(HttpContext.Session.GetJson<Checkout>("cart"));
        }
    }
}
