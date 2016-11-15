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
            Employee coso = new Employee();
            coso.W_Address = "test";
            coso.Charge = "puta";
            coso.W_Name = "tu vieja";
            coso.W_Password = "test123";
            coso.W_ID = 1;
            coso.W_LName = "que te pario";
            coso.S_ID = 3;
            var javaScriptSerializer = new
                                            System.Web.Script.Serialization.JavaScriptSerializer();
            string jsonString = javaScriptSerializer.Serialize(coso);
            
            System.Diagnostics.Debug.Write(jsonString);
            List<Sync> values = new List<Sync>();
            string[] attr = attribute.Split(',');
            string[] ids = id.Split(',');
            for (int i =0; i< Models.Tasks.tasks.Count; i++)
            {
                for (int z=0; z< Models.Tasks.tasks[i].seller.Count;z++)
                {
                    if (Models.Tasks.tasks[i].seller[z] == Convert.ToInt32(ids[0]))
                    {
                        values.Add(Models.Tasks.tasks[i]);
                    }
                }
            }
            
            return Json(values);

        }
    }
}