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
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class EmployeesController : ApiController
    {
        public static IList<Employee> listEmp = new List<Employee>();
        [AcceptVerbs("GET")]
        public Employee RPCStyleMethodFetchFirstEmployees()
        {
            return listEmp.FirstOrDefault();
        }
        static private string GetConnectionString()
        {
            return @"Data Source=ISAAC\ISAACSERVER;Initial Catalog=EPATECA;"
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
            action = updateString.UpdateConnectionString("UPDATE WORKER ", uattr, uvalue, cattr, cvalue);
            System.Diagnostics.Debug.WriteLine(action+" "+attr+" "+avalue);

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
        public JsonResult<List<Employee>> Get(string attribute, string id)
        {
            Employee emp = null;
            List<Employee> values = new List<Employee>();
            string[] attr = attribute.Split(',');
            string[] ids = id.Split(',');
            System.Diagnostics.Debug.WriteLine(string.IsNullOrEmpty(id)+" "+attribute+" "+id.Length);
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
                action =  constructor.FormConnectionString("WORKER", attr, ids);
            }
            else
            {
                action = "SELECT * FROM WORKER;";
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
                emp = new Employee();
                emp.W_ID = Convert.ToInt32(reader.GetValue(0));
                emp.W_Name = reader.GetValue(1).ToString();
                emp.W_LName = reader.GetValue(2).ToString();
                emp.W_Address = reader.GetValue(3).ToString();
            
                emp.W_Password = reader.GetValue(4).ToString();
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
            sqlCmd.CommandText = "DELETE FROM WORKER WHERE " + attribute + "='" + id + "';";
            sqlCmd.Connection = myConnection;
            myConnection.Open();
            int rowDeleted = sqlCmd.ExecuteNonQuery();
            myConnection.Close();
        }
        [HttpPost]
        [ActionName("Post")]
        public void AddEmployee(Employee employee)
        {
            Sync tmp = new Sync();
            System.Diagnostics.Debug.WriteLine(employee.W_ID);
            System.Diagnostics.Debug.WriteLine(employee.W_Name);
            System.Diagnostics.Debug.WriteLine("entrando al post ");

            SqlConnection myConnection = new SqlConnection();
            myConnection.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString; 
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.Text;
            System.Diagnostics.Debug.WriteLine(employee.W_Password);

            sqlCmd.CommandText = "INSERT INTO WORKER(W_ID,W_Name,W_LName,W_Address,W_Password) Values(@W_ID,@W_Name,@W_LName,@W_Address,@W_Password)";
            System.Diagnostics.Debug.WriteLine("generando comando");

            sqlCmd.Connection = myConnection;
            sqlCmd.Parameters.AddWithValue("@W_ID", employee.W_ID);
            sqlCmd.Parameters.AddWithValue("@W_Name", employee.W_Name);
            sqlCmd.Parameters.AddWithValue("@W_LName", employee.W_LName);
            sqlCmd.Parameters.AddWithValue("@W_Address", employee.W_Address);
            sqlCmd.Parameters.AddWithValue("@W_Password", employee.W_Password);
            myConnection.Open();
            try
            {
                var javaScriptSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                string jsonString = javaScriptSerializer.Serialize(employee);
                sqlCmd.ExecuteNonQuery();
                System.Diagnostics.Debug.Write("insertó");
                tmp.action = "insert";
                tmp.model = jsonString;
                tmp.table = "WORKER";
                {
                    tmp.seller.Add(employee.ID_Seller);
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
