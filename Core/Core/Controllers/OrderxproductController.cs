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
    public class OrderxproductController : ApiController
    {
        [HttpPost]
        [ActionName("Post")]
        public void AddOrderxproduct(Order order)
        {
            int rowInserted = 0;
            System.Diagnostics.Debug.WriteLine(order.C_ID);
            string[] products = order.PR_Name.Split(',');
            string[] values = order.PR_Amount.Split(',');
            SqlConnection myConnection = new SqlConnection();
            myConnection.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
           
            string action = "INSERT INTO ORDERXPRODUCT(OXP_ID,O_ID,PR_ID,PR_Name,PR_Amount,PR_Price) Values(@OXP_ID,@O_ID,@PR_ID,@PR_Name,@PR_Amount,@PR_Price)";
            myConnection.Open();

            for (int i = 0; i < products.Length; i++)
            {
                Random rnd = new Random();
                SqlCommand sqlCmd2 = new SqlCommand();
                sqlCmd2.Connection = myConnection;
                sqlCmd2.CommandText = action;
                sqlCmd2.Parameters.AddWithValue("@OXP_ID", rnd.Next());
                sqlCmd2.Parameters.AddWithValue("@PR_Name", products[i]);
                sqlCmd2.Parameters.AddWithValue("@PR_Amount", Convert.ToInt32(values[i]));
                sqlCmd2.Parameters.AddWithValue("@O_ID", order.O_ID);
                sqlCmd2.Parameters.AddWithValue("@PR_ID", order.PR_ID);
                sqlCmd2.Parameters.AddWithValue("@PR_Price", order.PR_Price);
                rowInserted = sqlCmd2.ExecuteNonQuery();
            }
            var javaScriptSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            string jsonString = javaScriptSerializer.Serialize(order);
            Sync tmp = new Sync();
            System.Diagnostics.Debug.Write("insertó");
            tmp.action = "insert";
            tmp.model = jsonString;
            tmp.table = "CATEXPRODUCT";
            if (order.ID_Seller != 0)
            {
                tmp.seller.Add(order.ID_Seller);
            }
            Models.Tasks.tasks.Add(tmp);
            string jsonString1 = javaScriptSerializer.Serialize(tmp);
            System.Diagnostics.Debug.WriteLine(Models.Tasks.tasks.Count);

            myConnection.Close();
        }
    }
}




