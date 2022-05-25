using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using AdventureWorks.Models;
using System.Text.Json;

namespace AdventureWorks.Controllers
{
    public class LoginController : Controller
    {
        // GET: LoginController
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LoginUser(string email, string password)
        {
            User user = LoadUser(email, password);

            if (user != null)
            {
                HttpContext.Session.SetString("userName", JsonSerializer.Serialize(user));
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Error = new Models.Error()
                {
                    Message = "Incorrect username or password",
                    BackUrl = "Login",
                    Text = "Try again?"
                };

                return View("Error");
            }
        }
        
        public User LoadUser(string email, string password)
        {
            List<SqlParameter> param = new List<SqlParameter>()
            {
                new SqlParameter("@email", email),
                new SqlParameter("@password", password)
            };

            DataTable ds = DatabaseHelper.DatabaseHelper.ExecuteStoreProcedure("spIsUserValid", param);

            if (ds.Rows.Count == 1)
            {
                User user = new User()
                {
                    Name = ds.Rows[0]["Name"].ToString(),
                    Email = email,
                    JobTitle = ds.Rows[0]["JobTitle"].ToString(),
                    HireDate = Convert.ToDateTime(ds.Rows[0]["HireDate"].ToString()).ToShortDateString(),
                    Department = ds.Rows[0]["Department"].ToString(),                    
                };

                return user;
            }

            return null;
        }
    }
}
