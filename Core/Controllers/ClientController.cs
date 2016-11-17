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
    public class ClientController : ApiController
    {

        [HttpPut]
        [ActionName("Update")]
        public void UpdateRecords(Client client)
        {
            string action = "UPDATE CLIENT SET C_Name = '" + client.C_Name + "',C_LName = '" + client.C_LName+"',C_Address = '"+client.C_Address+"',C_Phone = "+client.C_Phone+",C_Date = "+client.C_Date+",C_Penalization = "+client.C_Penalization+"C_Status = '"+client.C_Status + "' WHERE C_ID =" + client.C_ID;
            SqlConnection myConnection = new SqlConnection();
            myConnection.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            System.Diagnostics.Debug.WriteLine("cargo base");
            SqlCommand sqlCmd = new SqlCommand();
            System.Diagnostics.Debug.WriteLine("cargo sqlcommand");
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = action;
            sqlCmd.Connection = myConnection;
            myConnection.Open();
            try
            {
                string jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(client);
                Sync tmp = new Sync();
                sqlCmd.ExecuteNonQuery();
                System.Diagnostics.Debug.WriteLine("Actualizó");
                tmp.action = "UPDATE";
                tmp.model = jsonString;
                tmp.table = "ClIENT";
                if (client.ID_Seller != 0)
                {
                    tmp.seller.Add(client.ID_Seller);
                }
                Models.Tasks.tasks.Add(tmp);
            }
            catch (SqlException)
            {
            }

            myConnection.Close();
        }


        [HttpGet]
        [ActionName("Get")]
        public JsonResult<List<Client>> Get(string attribute, string id)
        {
            string[] attr = attribute.Split(',');
            string[] ids = id.Split(',');
            List<Client> values = new List<Client>();
            Client emp = null;

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
                action = constructor.FormConnectionString("CLIENT", attr, ids);
            }
            else
            {
                action = "SELECT * FROM CLIENT;";
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
                emp = new Client();
                emp.C_ID = Convert.ToInt32(reader.GetValue(0));
                emp.C_Name = reader.GetValue(1).ToString();
                emp.C_LName = reader.GetValue(2).ToString();
                emp.C_Address = reader.GetValue(3).ToString();
                emp.C_Phone = Convert.ToInt32(reader.GetValue(4).ToString());
                emp.C_Date = (DateTime)reader.GetValue(5);
               
                emp.C_Penalization = Convert.ToInt32(reader.GetValue(6).ToString());
                
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
            sqlCmd.CommandText = "DELETE FROM CLIENT WHERE " + actions[0] + "=" + ids[0];
            sqlCmd.Connection = myConnection;
            myConnection.Open();
            try
            {
                Sync tmp = new Sync();
                sqlCmd.ExecuteNonQuery();
                System.Diagnostics.Debug.Write("borró");
                tmp.action = "delete";
                tmp.model = ids[0];
                tmp.table = "CLIENT";
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
        [HttpPost]
        [ActionName("Post")]
        public void AddClient(Client client)
        {
            //System.Diagnostics.Debug.WriteLine(client.C_ID);
            //System.Diagnostics.Debug.WriteLine(client.FName);
            System.Diagnostics.Debug.WriteLine("entrando al post");

            SqlConnection myConnection = new SqlConnection();
            myConnection.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.Text;
            System.Diagnostics.Debug.WriteLine(myConnection.State);

            sqlCmd.CommandText = "INSERT INTO CLIENT(C_ID,C_Name,C_LName,C_Address,C_Phone,C_Date,C_Penalization,C_Status) Values(@C_ID,@C_Name,@C_LName,@C_Address,@C_Phone,@C_Date,@C_Penalization,@C_Status)";
            System.Diagnostics.Debug.WriteLine("generando comando");

            sqlCmd.Connection = myConnection;
            sqlCmd.Parameters.AddWithValue("@C_ID", client.C_ID);
            sqlCmd.Parameters.AddWithValue("@C_Name", client.C_Name);
            sqlCmd.Parameters.AddWithValue("@C_LName", client.C_LName);
            sqlCmd.Parameters.AddWithValue("@C_Address", client.C_Address);
            sqlCmd.Parameters.AddWithValue("@C_Phone", client.C_Phone);
            sqlCmd.Parameters.AddWithValue("@C_Date", client.C_Date);
            sqlCmd.Parameters.AddWithValue("@C_Penalization", client.C_Penalization);
            sqlCmd.Parameters.AddWithValue("@C_Status", "available");

            myConnection.Open();
            Sync tmp = new Sync();

            string jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(client);
            sqlCmd.ExecuteNonQuery();
            System.Diagnostics.Debug.Write("insertó");
            tmp.action = "insert";
            tmp.model = jsonString;
            tmp.table = "CLIENT";
            if (client.ID_Seller != 0)
            {
                tmp.seller.Add(client.ID_Seller);
            }
            Models.Tasks.tasks.Add(tmp);
            System.Diagnostics.Debug.WriteLine("cliente serialize:" + Newtonsoft.Json.JsonConvert.SerializeObject(client));
            myConnection.Close();
        }
    }
}
