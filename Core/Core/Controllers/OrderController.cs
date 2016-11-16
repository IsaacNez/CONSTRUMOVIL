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
using Newtonsoft.Json.Linq;

namespace WebApplication1.Controllers
{
    public class OrderController : ApiController
    {
        public static IList<Order> prolist = new List<Order>();
        [AcceptVerbs("GET")]
        public Order RPCStyleMethodFetchFirstEmployees()
        {
            return prolist.FirstOrDefault();
        }
        static private string GetConnectionString()
        {
            return @"Data Source=DESKTOP-E6QPTVT;Initial Catalog=EPATEC;"
                + "Integrated Security=true;";
        }

        [HttpGet]
        [ActionName("Update")]
        public void UpdateRecords(string attr, string avalue, string clause, string id)
        {
            string[] uattr = attr.Split(',');
            string[] uvalue = avalue.Split(',');
            string[] cattr = clause.Split(',');
            string[] cvalue = id.Split(',');
            string action = "";
            CategoryController updateString = new CategoryController();
            action = updateString.UpdateConnectionString("UPDATE EORDER ", uattr, uvalue, cattr, cvalue);
            System.Diagnostics.Debug.WriteLine(action);

            SqlConnection myConnection = new SqlConnection();
            myConnection.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            System.Diagnostics.Debug.WriteLine("cargo base");
            SqlCommand sqlCmd = new SqlCommand();
            System.Diagnostics.Debug.WriteLine("cargo sqlcommand");

            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = action;
            sqlCmd.Connection = myConnection;
            myConnection.Open();
            sqlCmd.ExecuteNonQuery();
            myConnection.Close();
        }

        [HttpGet]
        [ActionName("Get")]
        public JsonResult<List<Order>> Get(string attribute, string id)
        {
            string[] attr = attribute.Split(',');
            string[] ids = id.Split(',');
            List<Order> values = new List<Order>();
            Order emp = null;

            System.Diagnostics.Debug.WriteLine("entrando al get");
            SqlDataReader reader = null;
            SqlConnection myConnection = new SqlConnection();
            myConnection.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            System.Diagnostics.Debug.WriteLine("cargo base");
            SqlCommand sqlCmd = new SqlCommand();
            System.Diagnostics.Debug.WriteLine("cargo sqlcommand");
            string action = "";
            if (id != "undefined")
            {
                SucursalController constructor = new SucursalController();
                action = constructor.FormConnectionString("EORDER", attr, ids);
            }
            else
            {
                action = "SELECT * FROM EORDER;";
            }

            System.Diagnostics.Debug.WriteLine(action);
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = action;
            System.Diagnostics.Debug.WriteLine("cargo comando");

            sqlCmd.Connection = myConnection;
            myConnection.Open();
            System.Diagnostics.Debug.WriteLine("estado " + myConnection.State);

            reader = sqlCmd.ExecuteReader();

            while (reader.Read())
            {
                emp = new Order();
                emp.O_ID = Convert.ToInt32(reader.GetValue(6));
                emp.O_Priority = Convert.ToInt32(reader.GetValue(0).ToString());
                emp.O_Status = reader.GetValue(1).ToString();
                emp.O_Date = Convert.ToDateTime(reader.GetValue(2).ToString());
                emp.S_ID = Convert.ToInt32(reader.GetValue(3).ToString());
                emp.C_ID = Convert.ToInt32(reader.GetValue(5).ToString());
                values.Add(emp);
            }
            
            myConnection.Close();
            return Json(values);
        }
        [HttpGet]
        [ActionName("Delete")]
        public void Delete(string attribute, string id) 
        {
            string[] actions = attribute.Split(',');
            string[] ids = id.Split(',');
            SqlConnection myConnection = new SqlConnection();
            myConnection.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.Text;
            SucursalController deleteString = new SucursalController();
            string action = "DELETE FROM EORDER WHERE " + actions[0] + "='" + ids[0] + "';";

            sqlCmd.CommandText = action;
            sqlCmd.Connection = myConnection;
            myConnection.Open();
            try
            {
                Sync tmp = new Sync();
                sqlCmd.ExecuteNonQuery();
                System.Diagnostics.Debug.Write("borró");
                tmp.action = "delete";
                tmp.model = ids[0];
                tmp.table = "EORDER";
                if (Convert.ToInt32(ids[1]) != 0)
                {
                    tmp.seller.Add(Convert.ToInt32(ids[1]));
                }
                Models.Tasks.tasks.Add(tmp);
            }
            catch (SqlException)
            {
            }
            myConnection.Close();
        }
        public void orderxproduct(Order order)
        {
            string[] products = order.PR_Name.Split(',');
            string[] values = order.PR_Amount.Split(',');
            string action = "INSERT INTO ORDERXPRODUCT(OXP_ID,O_ID,PR_Name,PR_Amount PR_Price) Values(@OXP_ID,@O_ID,@PR_Name,@PR_Amount,@PR_Price)";
            SqlConnection myConnection = new SqlConnection();
            myConnection.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            SqlCommand sqlCmd = new SqlCommand();
            myConnection.Open();
            
            sqlCmd.CommandType = CommandType.Text;
            for (int i = 0; i < products.Length; i++)
            {
                Random rnd = new Random();
                SqlCommand sqlCmd2 = new SqlCommand();
                sqlCmd2.Connection = myConnection;
                sqlCmd2.CommandText = action;
                sqlCmd2.Parameters.AddWithValue("@OXP_ID", rnd);
                sqlCmd2.Parameters.AddWithValue("@O_ID", order.O_ID);
                sqlCmd2.Parameters.AddWithValue("@PR_Name", products[i]);
                sqlCmd2.Parameters.AddWithValue("@PR_Amount", Convert.ToInt32(values[i]));
                sqlCmd2.Parameters.AddWithValue("@PR_Price", 0);
                sqlCmd2.ExecuteNonQuery();
            }
        }

        [ActionName("post")]
        public void AddOrder(Order order)
        {
            
            System.Diagnostics.Debug.WriteLine(order.C_ID);
            string[] products = order.PR_Name.Split(',');
            string[] values = order.PR_Amount.Split(',');
            SqlConnection myConnection = new SqlConnection();
            myConnection.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            SqlCommand sqlCmd = new SqlCommand();
            myConnection.Open();
            sqlCmd.CommandType = CommandType.Text;
            System.Diagnostics.Debug.WriteLine(myConnection.State);
            sqlCmd.CommandText = "INSERT INTO EORDER(O_ID,O_Priority,O_Status,O_Date,O_PPhone,S_ID,W_ID,C_ID) Values(@O_ID,@O_Priority,@O_Status,@O_Date,@O_PPhone,@S_ID,@W_ID,@C_ID)";
            System.Diagnostics.Debug.WriteLine("generando comando");
            sqlCmd.Connection = myConnection;
            sqlCmd.Parameters.AddWithValue("@O_ID", order.O_ID);
            sqlCmd.Parameters.AddWithValue("@O_Priority", order.O_Priority);
            sqlCmd.Parameters.AddWithValue("@O_Status", order.O_Status);
            sqlCmd.Parameters.AddWithValue("@O_Date", order.O_Date);
            sqlCmd.Parameters.AddWithValue("@O_PPhone", order.O_PPhone);
            sqlCmd.Parameters.AddWithValue("@W_ID", order.W_ID);
            sqlCmd.Parameters.AddWithValue("@S_ID", order.S_ID);
            sqlCmd.Parameters.AddWithValue("@C_ID", order.C_ID);
            

            try
            {
                Sync tmp = new Sync();
                var javaScriptSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                string jsonString = javaScriptSerializer.Serialize(order);
                sqlCmd.ExecuteNonQuery();
                System.Diagnostics.Debug.Write("insertó");
                tmp.action = "insert";
                tmp.model = jsonString;
                tmp.table = "EORDER";
                if (order.ID_Seller != 0)
                {
                    tmp.seller.Add(order.ID_Seller);
                }
                Models.Tasks.tasks.Add(tmp);
                System.Diagnostics.Debug.Write(Models.Tasks.tasks.Count);
            }
            catch (SqlException)
            {
                System.Diagnostics.Debug.WriteLine("error while storing the employee");

            }
            myConnection.Close();

        }
    }
}
