using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FinalProject.Data;
using Microsoft.AspNetCore.Identity;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FinalProject.Controllers
{
    public class ReceiptController : Controller
    {
        private ApplicationDbContext _context;
        public ReceiptController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(Guid id)
        {
            return View(_context.Orders.Include(x => x.OrderItems).Single(x => x.ID == id));
        }
    }
}