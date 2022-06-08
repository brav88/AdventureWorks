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

                foreach (DataRow dr in ds.Rows)
                {
                    labelsList.Add(dr["years"].ToString());
                }

                List<List<long>> seriesList = new List<List<long>>();

                foreach (DataRow dr in ds.Rows)
                {
                    seriesList.Add(new List<long> { Convert.ToInt64(dr["sales"]) });
                }

                //LINQ
                /*seriesList = (from DataRow dr in ds.Rows
                              select new List<Int64>()
                              {
                                  Convert.ToInt64(dr["sales"]),
                              }).ToList();*/

                Data data = new Data()
                {
                    labels = labelsList,
                    series = seriesList
                };

                return Json(data);
            }

            return null;
        }
    }
}
