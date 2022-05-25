using AdventureWorks.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace AdventureWorks.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("userName")))
            {
                ViewBag.User = JsonSerializer.Deserialize<User>(HttpContext.Session.GetString("userName"));

                return View();
            }
            else
            {
                ViewBag.Error = new Models.Error()
                {
                    Message = "You must log in to see this page",
                    BackUrl = "Login",
                    Text = "Go back to Login"
                };

                return View("Error");
            }
        }

        public ActionResult Logout()
        {
            HttpContext.Session.Clear();

            return RedirectToAction("Index", "Login");
        }
    }
}
