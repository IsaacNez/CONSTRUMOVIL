using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using WebApplication1.Models;
using System.Data.SqlClient;
using System.Data;
using System.Web.Http.Results;
using System.Web.UI;
using System.Web.Script.Serialization;

namespace WebApplication1.Controllers
{
    public class SyncController : ApiController
    {
        [HttpGet]
        [ActionName("Get")]
        public JsonResult<List<Sync>> Get(string attribute, string id)
        {
            System.Diagnostics.Debug.Write("entró al sync");

            var javaScriptSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            
            
            System.Diagnostics.Debug.Write(Models.Tasks.tasks.Count);
            List<Sync> values = new List<Sync>();
            string[] attr = attribute.Split(',');
            string[] ids = id.Split(',');
            var seller_id = Convert.ToInt32(ids[0]);
            System.Diagnostics.Debug.WriteLine("entrando al for");
            for (int i = 0; i < Tasks.tasks.Count ; i++)
            {
                if (Tasks.tasks[i].seller.Count == 0)
                {
                    Tasks.tasks[i].seller.Add(seller_id);
                    values.Add(Tasks.tasks[i]);
                }
                else if (Tasks.tasks[i].seller.Contains(seller_id))
                {
                    continue;
                }
                else if (Tasks.tasks[i].seller.Contains(seller_id) == false)
                {
                    Tasks.tasks[i].seller.Add(seller_id);
                    values.Add(Tasks.tasks[i]);
                }

            }
            
            return Json(values);

        }
    }
}