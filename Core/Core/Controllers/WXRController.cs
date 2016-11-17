
using System.Web.Http;
using WebApplication1.Models;
using System.Data.SqlClient;
using System.Data;


namespace WebApplication1.Controllers
{
    public class WXRController : ApiController
    {
        [HttpPost]
        [ActionName("Post")]
        public void AddWorkerxRole(Employee employee)
        {
            Sync tmp = new Sync();
            System.Diagnostics.Debug.WriteLine("entrando al post ");
            SqlConnection myConnection = new SqlConnection();
            myConnection.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = "INSERT INTO WXR(WXR_ID,R_ID,W_ID) Values(@WXR_ID,@R_ID,@W_ID)";
            System.Diagnostics.Debug.WriteLine("generando comando");
            System.Random rnd = new System.Random();
            sqlCmd.Connection = myConnection;
            sqlCmd.Parameters.AddWithValue("@R_ID", employee.R_ID);
            sqlCmd.Parameters.AddWithValue("@W_ID", employee.W_ID);
            sqlCmd.Parameters.AddWithValue("@WXR_ID", rnd.Next());
            myConnection.Open();
            try
            {
                var javaScriptSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                string jsonString = javaScriptSerializer.Serialize(employee);
                sqlCmd.ExecuteNonQuery();
                System.Diagnostics.Debug.Write("insertó");
                tmp.action = "insert";
                tmp.model = jsonString;
                tmp.table = "WXR";
                {
                    tmp.seller.Add(employee.ID_Seller);
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
