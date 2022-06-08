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
            public List<List<long>> series { get; set; }
        }

        public ActionResult GetSales()
        {
            DataTable ds = DatabaseHelper.DatabaseHelper.ExecuteStoreProcedure("spGetSales", null);

            if (ds.Rows.Count > 0)
            {
                List<string> labelsList = new List<string>();
                List<List<long>> seriesList = new List<List<long>>();
                List<long> series = new List<long>();

                foreach (DataRow dr in ds.Rows)
                {
                    labelsList.Add(dr["years"].ToString());
                    series.Add(Convert.ToInt64(dr["sales"]));
                }

                seriesList.Add(series);

                return Json(new Data() { labels = labelsList, series= seriesList });
            }

            return null;
        }
    }
}
