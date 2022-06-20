﻿using AdventureWorks.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text.Json;

namespace AdventureWorks.Controllers
{
    public class ProductController : Controller
    {
        // GET: ProductController
        public ActionResult Index()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("userName")))
            {
                ViewBag.User = JsonSerializer.Deserialize<User>(HttpContext.Session.GetString("userName"));
                ViewBag.Categories = LoadCategories();

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

        private List<Catalog> LoadCategories()
        {
            DataTable ds = DatabaseHelper.DatabaseHelper.ExecuteStoreProcedure("spGetCategories", null);
            List<Catalog> catalogList = new List<Catalog>();

            foreach (DataRow row in ds.Rows)
            {
                catalogList.Add(new Catalog()
                {
                    Id = Convert.ToInt16(row["Id"]),
                    Value = row["Value"].ToString(),
                });
            }

            return catalogList;
        }

        public ActionResult LoadSubCategories(int id)
        {
            List<SqlParameter> param = new List<SqlParameter>()
            {
                new SqlParameter("@id", id),
            };

            DataTable ds = DatabaseHelper.DatabaseHelper.ExecuteStoreProcedure("spGetSubCategories", param);
            List<Catalog> catalogList = new List<Catalog>();

            foreach (DataRow row in ds.Rows)
            {
                catalogList.Add(new Catalog()
                {
                    Id = Convert.ToInt16(row["Id"]),
                    Value = row["Value"].ToString(),
                });
            }

            return Json(catalogList);
        }
    }
}
