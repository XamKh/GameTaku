using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FinalProject.Models;
using FinalProject.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Text;
using Microsoft.Net.Http.Headers;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FinalProject.Controllers
{
    [Authorize]
    public class ProductsController : Controller
    {
        private ApplicationDbContext _context;
        private readonly IHostingEnvironment _hostingEnvironment;


        public ProductsController(ApplicationDbContext context, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        private void PopulateEmptyProducts()
        {

            if (_context.Roles.Count() == 0)
            {
                _context.Roles.Add(new IdentityRole("Administrator"));
                _context.SaveChanges();
            }

            if ((_context.UserRoles.Count() == 0) && (_context.Users.Count() > 0))
            {
                _context.UserRoles.Add(
                    new IdentityUserRole<string>
                    {
                        RoleId = _context.Roles.Single(x => x.Name == "Administrator").Id,
                        UserId = _context.Users.First().Id
                    });
                _context.SaveChanges();
            }


            if (!_context.Products.Any())
            {
                _context.Products.AddRange(
                    new Product
                    {
                        Created = DateTime.UtcNow,
                        LastModified = DateTime.UtcNow,
                        Price = 59.99m,
                        Name = "Call Of Duty : Black Ops 4",
                        Description = "New amazing game",
                        ImageUrl = "~/images/cod.png",
                        TrailerUrl = "https://www.youtube.com/embed/BjiaMBk6rHk"


                    },
                    new Product
                    {
                        Created = DateTime.UtcNow,
                        LastModified = DateTime.UtcNow,
                        Price = 59.99m,
                        Name = "Battlefield 1",
                        Description = "Largest Scale game ever made. With insanely distructable environment",
                        ImageUrl = "~/images/btl1.png",
                        TrailerUrl = "https://www.youtube.com/embed/c7nRTF2SowQ"

                    }, new Product
                    {
                        Created = DateTime.UtcNow,
                        LastModified = DateTime.UtcNow,
                        Price = 89.99m,
                        Name = "World War 3",
                        Description = " World War 3 is a multiplayer military FPS set in a modern, global conflict. Strong teamplay, national armed forces, real locations, full body awareness, and a versatile customization system all contribute to the authenticity of the modern combat experience",
                        ImageUrl = "~/images/ww3.png",
                        TrailerUrl = "https://www.youtube.com/embed/G17mpdSbJBU"

                    }, new Product
                    {
                        Created = DateTime.UtcNow,
                        LastModified = DateTime.UtcNow,
                        Price = 39.99m,
                        Name = "Overwatch",
                        Description = "Great team based game with fantasy heroes in the arena.",
                        ImageUrl = "~/images/overwatch.png",
                        TrailerUrl = "https://www.youtube.com/embed/FqnKB22pOC0"

                    }, new Product
                    {
                        Created = DateTime.UtcNow,
                        LastModified = DateTime.UtcNow,
                        Price = 59.99m,
                        Name = "Mount and Blade 2 : BannerLord",
                        Description = "Mount & Blade II: Bannerlord is a medieval action role-playing game currently being developed by TaleWorlds Entertainment.",
                        ImageUrl = "~/images/bannerlord.png",
                        TrailerUrl = "https://www.youtube.com/embed/M_PSRDwORDQ"

                    }, new Product
                    {
                        Created = DateTime.UtcNow,
                        LastModified = DateTime.UtcNow,
                        Price = 100m,
                        Name = "Age Of Empires IV",
                        Description = "The best real time strategy game coming soon",
                        ImageUrl = "~/images/ageofempires.png",
                        TrailerUrl = "https://www.youtube.com/embed/RYwZ6GZXWhA"

                    }, new Product
                    {
                        Created = DateTime.UtcNow,
                        LastModified = DateTime.UtcNow,
                        Price = 14.99m,
                        Name = "Naruto",
                        Description = "Naruto is a Japanese manga series written and illustrated by Masashi Kishimoto. It tells the story of Naruto Uzumaki, an adolescent ninja who searches for recognition from his peers and the village and also dreams of becoming the Hokage, the leader of his",
                        ImageUrl = "~/images/naruto.png",
                        TrailerUrl = "https://www.youtube.com/embed/mksl3tYdyK4"

                    }, new Product
                    {
                        Created = DateTime.UtcNow,
                        LastModified = DateTime.UtcNow,
                        Price = 14.99m,
                        Name = "That Time I Got Reincarnated as a Slime",
                        Description = "That Time I Got Reincarnated as a Slime is a Japanese light novel series written by Fuse and illustrated by Mitz Vah. It was serialized online between 2013 and 2016 on the user-generated novel publishing website Shōsetsuka ni Narō. It was acquired by Micro",
                        ImageUrl = "~/images/slime.png",
                        TrailerUrl = "https://www.youtube.com/embed/uOzwqb74K34"

                    }, new Product
                    {
                        Created = DateTime.UtcNow,
                        LastModified = DateTime.UtcNow,
                        Price = 14.99m,
                        Name = "One Punch Man",
                        Description = "One-Punch Man is an ongoing Japanese superhero webcomic created by ONE which began publication in early 2009. The series quickly went viral, surpassing 7.9 million hits in June 2012.",
                        ImageUrl = "~/images/onepunch.png",
                        TrailerUrl = "https://www.youtube.com/embed/Poo5lqoWSGw"

                    }
                    );
                _context.SaveChanges();
            }

        }

        //private void PopulateEmptyProducts()
        //{
        //    if(_context.GameProducts.Count() == 0)
        //    {
        //        _context.GameProducts.AddRange(new GameProduct
        //        {
        //            ImageUrl = "~/images/test1.png",
        //            Name = "Game1",
        //            Description = "Great Game",
        //            Price = 59.99m
        //        });
        //    }
        //}

        [HttpPost]
        public IActionResult Index(int id, int quantity)
        {

            Cart productCart = null;

            if (User.Identity.IsAuthenticated)
            {
                var currentSodaUser = _context.Users.Include(x => x.Cart).ThenInclude(x => x.CartItems).ThenInclude(x => x.Product).First(x => x.UserName == User.Identity.Name);
                if (currentSodaUser.Cart != null)
                {
                    productCart = currentSodaUser.Cart;
                }
                else
                {
                    productCart = new Cart
                    {
                        CookieID = Guid.NewGuid(),
                        Created = DateTime.UtcNow
                    };
                    currentSodaUser.Cart = productCart;
                    _context.SaveChanges();
                }
            }
            if ((productCart == null) && (Request.Cookies.ContainsKey("ProductCartID")))
            {
                if (Guid.TryParse(Request.Cookies["ProductCartID"], out Guid cookieId))
                {
                    productCart = _context.Carts.Include(x => x.CartItems).ThenInclude(x => x.Product).FirstOrDefault(x => x.CookieID == cookieId);
                }
            }

            if (productCart == null)   //I either couldn't find the cart from the cookie, or the user had no cookie.
            {
                productCart = new Cart
                {
                    CookieID = Guid.NewGuid(),
                    Created = DateTime.UtcNow
                };
                _context.Carts.Add(productCart);
            }
            productCart.LastModified = DateTime.UtcNow;

            CartItem cartItem = null;   //I also need to check if this item is already in the cart!
            cartItem = productCart.CartItems.FirstOrDefault(x => x.Product.ID == id);

            if (cartItem == null) //If still null, this is the first time this item has been added to the cart
            {
                cartItem = new CartItem
                {
                    Quantity = 0,
                    Product = _context.Products.Find(id),
                    Created = DateTime.UtcNow,
                };
                productCart.CartItems.Add(cartItem);
            }
            cartItem.Quantity += quantity;
            cartItem.LastModified = DateTime.UtcNow;
            _context.SaveChanges();

            if (!User.Identity.IsAuthenticated)
            {
                Response.Cookies.Append("ProductCartID", productCart.CookieID.Value.ToString());
            }
            return RedirectToAction("Index", "Cart");
        }

        public FileResult Download(string name)
        {
            var stream = new MemoryStream(Encoding.ASCII.GetBytes($"Thank you for downloading: {name}." +
                $"{Environment.NewLine}We hope you enjoy it!"));
            return new FileStreamResult(stream, new MediaTypeHeaderValue("text/plain"))
            {
                FileDownloadName = $"{name}.txt"
            };
            //var fileName = $"{name}.txt";
            //var filepath = $"{_hostingEnvironment.WebRootPath}/{fileName}";
            //byte[] fileBytes = System.IO.File.ReadAllBytes(filepath);


        }

        public IActionResult Index(string id, string searchString)
        {
            PopulateEmptyProducts();
            ViewData["Roles"] =  _context.Roles.ToArrayAsync();
            //if(_context.UserRoles.)
            ProductsViewModel model = new ProductsViewModel
            {
                ProductTypes = _context.ProductTypes
                    .Include(x => x.Products)
                    .ThenInclude(x => x.ProductProductGenres)
                    .ThenInclude(x => x.ProductGenre)
                    .Select(x => new ProductTypeViewModel
                    {
                        Name = x.Name,
                        //Products = x.Products.Where(y => ((id == null) || y.ProductTypeName == id)).ToArray(),
                        //            if(id == null && searchString == null)
                        //{
                        //    x.Products
                        //}
                        //else if(id != null && searchString != null)
                        //{
                        //    x.Products.Where(y => y.ProductProductGenres.Select
                        //            (w => w.ProductGenre.Name).Contains(searchString))).ToArray();
                        //}
                        //else if (id != null)
                        //{

                        //    x.Products.Where(y => y.ProductTypeName == id)
                        //}
                        //else
                        //{
                        //    x.Products.Where(y => y.ProductProductGenres.Select
                        //            (w => w.ProductGenre.Name).Contains(searchString))).ToArray();
                        //}
                        Products = ((id == null && searchString == null) ? x.Products
                        : (id != null && searchString != null)
                        ? x.Products.Where(y => y.ProductProductGenres.Select
                        (w => w.ProductGenre.Name).Contains(searchString)).ToArray()
                        : id != null
                        ? x.Products.Where(y => y.ProductTypeName == id)
                        : x.Products.Where(y => y.ProductProductGenres.Select
                        (w => w.ProductGenre.Name).Contains(searchString))).ToArray(),
                        AvailablesGenres = x.Products.SelectMany(y => y.ProductProductGenres.Select(z => z.ProductGenre.Name)).Distinct().ToArray(),
                        


                    }).ToArray()
            };

            return View(model);
        }
    }
}




