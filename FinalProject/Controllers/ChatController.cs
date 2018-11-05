using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinalProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FinalProject.Controllers
{
    [Authorize]
    public class ChatController : Controller
    {
        private static List<ChatMessage> messages = new List<ChatMessage>();
        public IActionResult Index()
        {
            return View(messages);
        }
        [HttpPost]
        public IActionResult Index(ChatMessage model)
        {
            model.Time = DateTime.UtcNow;
            model.UserName = User.Identity.Name;
            messages.Add(model);
            return View(messages);
        }
    }
}

    namespace FinalProject.Models
{
    public class ChatMessage
    {
        public DateTime Time { get; set; }
        public string UserName { get; set; }
        public string Message { get; set; }
    }
}
