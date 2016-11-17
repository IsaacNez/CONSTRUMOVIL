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
    public class RoleController : ApiController
    {
        [HttpGet]
        [ActionName("Get")]
        public JsonResult<List<Role>> Get(string attribute, string id)
        {
            string[] attr = attribute.Split(',');
            string[] ids = id.Split(',');
            List<Role> values = new List<Role>();
            Role emp = null;

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
                action = constructor.FormConnectionString("EROLE", attr, ids);
            }
            else
            {
                action = "SELECT * FROM EROLE;";
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
                emp = new Role();
                emp.R_ID = Convert.ToInt32(reader.GetValue(0));
                emp.R_Description = reader.GetValue(1).ToString();
                values.Add(emp);
            }
            myConnection.Close();
            return Json(values);
        }
        [HttpPost]
        [ActionName("Post")]
        public void AddRole(Role role)
        {
            Sync tmp = new Sync();
            System.Diagnostics.Debug.WriteLine("entrando al post ");

            SqlConnection myConnection = new SqlConnection();
            myConnection.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.Text;
            

            sqlCmd.CommandText = "INSERT INTO EROLE(R_ID,R_Description) Values(@R_ID,@R_Description)";
            System.Diagnostics.Debug.WriteLine("generando comando");

            sqlCmd.Connection = myConnection;
            sqlCmd.Parameters.AddWithValue("@R_ID", role.R_ID);
            sqlCmd.Parameters.AddWithValue("@R_Description", role.R_Description);
            myConnection.Open();
            try
            {
                string jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(role);
                sqlCmd.ExecuteNonQuery();
                System.Diagnostics.Debug.Write("insertó");
                tmp.action = "insert";
                tmp.model = jsonString;
                tmp.table = "EROLE";
                {
                    tmp.seller.Add(role.ID_Seller);
                }
                //Models.Tasks.tasks.Add(tmp);
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
