using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace AdventureWorks.Controllers
{
    public class SalesController : Controller
    {
        public class Data
        {
            public List<string> labels { get; set; }
            public List<List<int>> series { get; set; }
        }

        public ActionResult GetSales()
        {
            List<List<int>> seriesList = SeriesListUsingLinq();

            Data data = new Data()
            {
                labels = new List<string>(new string[] { "2017", "2018", "2019", "2020", "2021" }),
                series = seriesList,
            };

            return Json(data);
        }

        public List<List<int>> SeriesListUsingLinq()
        {            
            DataTable dt = new DataTable("Sales");
            dt.Columns.Add("column1", typeof(Int32));
            dt.Columns.Add("column2", typeof(Int32));
            dt.Columns.Add("column3", typeof(Int32));
            dt.Columns.Add("column4", typeof(Int32));
            dt.Columns.Add("column5", typeof(Int32));
            //Data  
            dt.Rows.Add(1, 2, 3, 4, 5);
            dt.Rows.Add(6, 1, 5, 2, 3);

            List<List<int>> seriesList = new List<List<int>>();

            seriesList = (from DataRow dr in dt.Rows
                           select new List<int>()
                           {                               
                               Convert.ToInt32(dr["column1"]),
                               Convert.ToInt32(dr["column2"]),
                               Convert.ToInt32(dr["column3"]),
                               Convert.ToInt32(dr["column4"]),
                               Convert.ToInt32(dr["column5"]),
                           }).ToList();

            return seriesList;
        }
    }
}
