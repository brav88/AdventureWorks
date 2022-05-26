using AdventureWorks.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using System.IO;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;

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

        public ActionResult UpdatePhoto(IFormFile photo)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("userName")) && photo != null)
            {
                ViewBag.User = JsonSerializer.Deserialize<User>(HttpContext.Session.GetString("userName"));

                string fileName = ViewBag.User.BusinessEntityID + new FileInfo(photo.FileName).Extension;
                string filePath = Path.Combine("/Files/", fileName);
                string localFileName = Path.Combine(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Files"), fileName);

                using (var stream = new FileStream(localFileName, FileMode.Create))
                {
                    photo.CopyTo(stream);
                }

                List<SqlParameter> param = new List<SqlParameter>()
                {
                    new SqlParameter("@BusinessEntityID", ViewBag.User.BusinessEntityID),
                    new SqlParameter("@PhotoPath", filePath)
                };

                DatabaseHelper.DatabaseHelper.ExecStoreProcedure("spUpdatePhoto", param);

                ViewBag.User.PhotoPath = filePath;

                return View("Index");
            }
            else
            {
                ViewBag.Error = new Models.Error()
                {
                    Message = "Unhandled error trying to upload photo",
                    BackUrl = "Home",
                    Text = "Go back to Home"
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
