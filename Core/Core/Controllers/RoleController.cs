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
            

            sqlCmd.CommandText = "INSERT INTO ROLE(R_ID,R_Description) Values(@R_ID,@R_Description)";
            System.Diagnostics.Debug.WriteLine("generando comando");

            sqlCmd.Connection = myConnection;
            sqlCmd.Parameters.AddWithValue("@R_ID", role.R_ID);
            sqlCmd.Parameters.AddWithValue("@R_Description", role.R_Description);
            myConnection.Open();
            try
            {
                var javaScriptSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                string jsonString = javaScriptSerializer.Serialize(role);
                sqlCmd.ExecuteNonQuery();
                System.Diagnostics.Debug.Write("insertó");
                tmp.action = "insert";
                tmp.model = jsonString;
                tmp.table = "ROLE";
                {
                    tmp.seller.Add(role.ID_Seller);
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
