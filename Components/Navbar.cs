﻿using DigiShop.Data;
using Microsoft.AspNetCore.Mvc;

namespace DigiShop.Components
{
    public class Navbar : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public Navbar(ApplicationDbContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke()
        { 
            return View(_context.Categories.ToList()); 
        }
    }
}
