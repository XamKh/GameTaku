using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FinalProject.Data;
using Microsoft.EntityFrameworkCore;
// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FinalProject.Controllers
{
    public class CartController : Controller
    {
        private ApplicationDbContext _context;

        public CartController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            Cart productCart = null;
            if (User.Identity.IsAuthenticated)
            {
                var currentSodaUser = _context.Users.Include(x => x.Cart).ThenInclude(x => x.CartItems).ThenInclude(x => x.Product).First(x => x.UserName == User.Identity.Name);
                if (currentSodaUser.Cart != null)
                {
                    productCart = currentSodaUser.Cart;
                }
            }
            else if (Request.Cookies.ContainsKey("ProductCartID"))
            {
                if (Guid.TryParse(Request.Cookies["ProductCartID"], out Guid cookieId))
                {
                    productCart = _context.Carts.Include(x => x.CartItems).ThenInclude(x => x.Product).FirstOrDefault(x => x.CookieID == cookieId);
                }
            }

            return View(productCart);

        }


        [HttpPost]
        public IActionResult Index(Cart model)
        {
            Cart productCart = null;

            if (User.Identity.IsAuthenticated)
            {
                var currentSodaUser = _context.Users.Include(x => x.Cart).ThenInclude(x => x.CartItems).ThenInclude(x => x.Product).First(x => x.UserName == User.Identity.Name);
                if (currentSodaUser.Cart != null)
                {
                    productCart = currentSodaUser.Cart;
                }
            }
            else if (Request.Cookies.ContainsKey("ProductCartID"))
            {
                if (Guid.TryParse(Request.Cookies["ProductCartID"], out Guid cookieId))
                {
                    productCart = _context.Carts.Include(x => x.CartItems).ThenInclude(x => x.Product).FirstOrDefault(x => x.CookieID == cookieId);
                }
            }

            foreach (var item in productCart.CartItems)
            {
                var modelItem = model.CartItems.FirstOrDefault(x => x.ID == item.ID);
                if (modelItem != null && modelItem.Quantity != item.Quantity)
                {
                    item.LastModified = DateTime.UtcNow;
                    item.Quantity = modelItem.Quantity;
                    if (item.Quantity == 0)
                    {
                        _context.CartItems.Remove(item);
                    }
                }
            }
            _context.SaveChanges();


            return View(productCart);
        }
    }

}
