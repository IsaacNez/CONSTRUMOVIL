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

            var javaScriptSerializer = new
                                            System.Web.Script.Serialization.JavaScriptSerializer();
            
            
            System.Diagnostics.Debug.Write(Models.Tasks.tasks.Count);
            List<Sync> values = new List<Sync>();
            string[] attr = attribute.Split(',');
            string[] ids = id.Split(',');
            System.Diagnostics.Debug.WriteLine("entrando al for");
            for (int i =0; i< Models.Tasks.tasks.Count; i++)
            {
                System.Diagnostics.Debug.WriteLine("entró al for inicial");
                if (Models.Tasks.tasks[i].seller.Count==0)
                {
                    for (int j = 0; j < Models.Tasks.tasks.Count; j++)
                    {
                        values.Add(Tasks.tasks[j]);

                    }
                        
                    string jsonString1 = javaScriptSerializer.Serialize(values);

                    System.Diagnostics.Debug.WriteLine(jsonString1);
                    System.Diagnostics.Debug.WriteLine(jsonString1);
                    System.Diagnostics.Debug.WriteLine(values.Count);

                }
                else {
                    for (int z = 0; z < Models.Tasks.tasks[i].seller.Count; z++)
                    {

                        System.Diagnostics.Debug.WriteLine("seller_id=" + Models.Tasks.tasks[i].seller[z]);

                        if (Models.Tasks.tasks[i].seller[z] != Convert.ToInt32(ids[0]))
                        {
                            Models.Tasks.tasks[i].seller.Add(Convert.ToInt32(ids[0]));
                            values.Add(Models.Tasks.tasks[i]);
                            string jsonString1 = javaScriptSerializer.Serialize(Models.Tasks.tasks[i]);

                            System.Diagnostics.Debug.WriteLine(jsonString1);
                        }
                    } }
            }
            
            return Json(values);

        }
    }
}