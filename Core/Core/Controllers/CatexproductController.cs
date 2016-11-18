using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication1.Models;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using System.Web.Http.Cors;
using System.Web.Http.Results;

namespace WebApplication1.Controllers
{
    public class CatexproductController : ApiController
    {
        [HttpPost]
        [ActionName("Post")]
        public void AddCategoryxproduct(Product category)
        {
            Sync tmp = new Sync();
            System.Diagnostics.Debug.WriteLine(category.CA_ID);
            System.Diagnostics.Debug.WriteLine("entrando al post");
            SqlConnection myConnection = new SqlConnection();
            myConnection.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.Text;
            System.Diagnostics.Debug.WriteLine(myConnection.State);
            sqlCmd.CommandText = "INSERT INTO CATEXPRODUCT(CXP_ID,CXP_Status,CA_ID,PR_ID) Values(@CXP_ID,@CXP_Status,@CA_ID,@PR_ID)";
            System.Diagnostics.Debug.WriteLine("generando comando");
            sqlCmd.Connection = myConnection;
            Random rnd = new Random();
            sqlCmd.Parameters.AddWithValue("@CXP_ID", rnd.Next());
            sqlCmd.Parameters.AddWithValue("@CA_ID", category.CA_ID);
            sqlCmd.Parameters.AddWithValue("@CXP_Status", "available");
            sqlCmd.Parameters.AddWithValue("@PR_ID", category.PR_ID);
            myConnection.Open();
            var javaScriptSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            string jsonString = javaScriptSerializer.Serialize(category);
            sqlCmd.ExecuteNonQuery();
            System.Diagnostics.Debug.Write("insertó");
            tmp.action = "insert";
            tmp.model = jsonString;
            tmp.table = "CATEXPRODUCT";
            if (category.ID_Seller != 0)
            {
                tmp.seller.Add(category.ID_Seller);
            }
            //Models.Tasks.tasks.Add(tmp);
            string jsonString1 = javaScriptSerializer.Serialize(tmp);
            System.Diagnostics.Debug.WriteLine(Models.Tasks.tasks.Count);

            myConnection.Close();
        }
    }
}
