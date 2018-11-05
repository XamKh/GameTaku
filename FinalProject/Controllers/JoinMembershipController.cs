using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FinalProject.Models;

namespace FinalProject.Controllers
{
    public class JoinMembershipController : Controller
    {
        public IActionResult Membership()
        {
            return View();
        }
    }
}
