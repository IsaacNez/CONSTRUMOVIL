using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using WebApplication1.Models;
using System.Data.SqlClient;
using System.Data;
using System.Web.Http.Results;

namespace WebApplication1.Controllers
{
    public class ProductController : ApiController
    {
        public static IList<Product> prolist = new List<Product>();
        [AcceptVerbs("GET")]
        public Product RPCStyleMethodFetchFirstEmployees()
        {
            return prolist.FirstOrDefault();
        }
        static private string GetConnectionString()
        {
            return @"Data Source=ISAAC\ISAACSERVER;Initial Catalog=EPATEC;"
                + "Integrated Security=true;";
        }

        [HttpPut]
        [ActionName("Update")]
        public void UpdateRecords(Product product)
        {
            string action = "UPDATE PRODUCT SET PR_Name = '" + product.PR_Name + "',PR_Price = " + product.PR_Price + ",PR_Exempt = " + product.PR_Exempt + ",PR_Description = '" + product.PR_Description + "',PR_Quantity = " + product.PR_Quantity + ",PR_Status = '" + product.PR_Status + "' WHERE PR_ID =" + product.PR_ID;
            SqlConnection myConnection = new SqlConnection();
            myConnection.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            System.Diagnostics.Debug.WriteLine("cargo base");
            SqlCommand sqlCmd = new SqlCommand();
            System.Diagnostics.Debug.WriteLine("cargo sqlcommand");
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = action;
            sqlCmd.Connection = myConnection;
            product.CXP_ID = 1;
            product.CXP_Status = "vacio";
            product.CA_ID = "vacio";
            product.P_ID = 0;
            product.S_ID = 0;
           
            myConnection.Open();
            try
            {
                string jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(product);
                Sync tmp = new Sync();
                sqlCmd.ExecuteNonQuery();
                System.Diagnostics.Debug.WriteLine("Actualizó");
                tmp.action = "UPDATE";
                tmp.model = jsonString;
                tmp.table = "PRODUCT";
                if (product.ID_Seller != 0)
                {
                    tmp.seller.Add(product.ID_Seller);
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
        public JsonResult<List<Product>> Get(string attribute, string id)
        {
            string[] attr = attribute.Split(',');
            string[] ids = id.Split(',');
            List<Product> values = new List<Product>();
            Product emp = null;

            System.Diagnostics.Debug.WriteLine("entrando al get");
            SqlDataReader reader = null;
            SqlConnection myConnection = new SqlConnection();
            myConnection.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            System.Diagnostics.Debug.WriteLine("cargo base");
            SqlCommand sqlCmd = new SqlCommand();
            System.Diagnostics.Debug.WriteLine("cargo sqlcommand");
            string action = "";

            if(id != "undefined")
            {
                SucursalController constructor = new SucursalController();
                action = constructor.FormConnectionString("PRODUCT", attr, ids);
            }
            else
            {
                action = "SELECT * FROM PRODUCT;";
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
                emp = new Product();
                emp.PR_ID = Convert.ToInt32(reader.GetValue(0).ToString());
                emp.PR_Name = reader.GetValue(1).ToString();
                emp.PR_Price = Convert.ToInt32(reader.GetValue(2).ToString());
                emp.PR_Exempt = Convert.ToInt32(reader.GetValue(3).ToString());
                emp.PR_Description = reader.GetValue(4).ToString();
                System.Diagnostics.Debug.WriteLine(reader.GetValue(4).ToString());
                emp.PR_Quantity = Convert.ToInt32(reader.GetValue(5).ToString());
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
            string deleteproduct = "DELETE FROM PRODUCT WHERE " + actions[0] + "='" + ids[0] + "';";
            SqlConnection myConnection = new SqlConnection();
            myConnection.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = deleteproduct;
            sqlCmd.Connection = myConnection;
            myConnection.Open();
            try
            {
                Sync tmp = new Sync();
                sqlCmd.ExecuteNonQuery();
                System.Diagnostics.Debug.Write("borró");
                tmp.action = "delete";
                tmp.model = ids[0];
                tmp.table = "PRODUCT";
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
        public void AddProduct(Product product)
        {
            System.Diagnostics.Debug.Write(product.PR_Description);


            SqlConnection myConnection = new SqlConnection();
            myConnection.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.Text;

            sqlCmd.CommandText = "INSERT INTO PRODUCT(PR_ID,PR_Price,PR_Exempt,PR_Description,PR_Quantity,PR_Name,PR_Status,P_ID,S_ID) Values(@PR_ID,@PR_Price,@PR_Exempt,@PR_Description,@PR_Quantity,@PR_Name,@PR_Status,@P_ID,@S_ID)";

            sqlCmd.Connection = myConnection;
            sqlCmd.Parameters.AddWithValue("@PR_ID", product.PR_ID);
            sqlCmd.Parameters.AddWithValue("@PR_Price", product.PR_Price);
            sqlCmd.Parameters.AddWithValue("@PR_Exempt", product.PR_Exempt);
            sqlCmd.Parameters.AddWithValue("@PR_Description", product.PR_Description);
            sqlCmd.Parameters.AddWithValue("@PR_Quantity", product.PR_Quantity);
            sqlCmd.Parameters.AddWithValue("@PR_Name", product.PR_Name);
            sqlCmd.Parameters.AddWithValue("@P_ID", product.P_ID);
            sqlCmd.Parameters.AddWithValue("@S_ID", product.S_ID);
            sqlCmd.Parameters.AddWithValue("@PR_Status", product.PR_Status);

            myConnection.Open();
            try
            {
                Sync tmp = new Sync();
                sqlCmd.ExecuteNonQuery();
                var test = new CatexproductController();
                test.AddCategoryxproduct(product);
                string jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(product);
                System.Diagnostics.Debug.Write("insertó");
                tmp.action = "insert";
                tmp.model = jsonString;
                tmp.table = "PRODUCT";
                if (product.ID_Seller != 0)
                {
                    tmp.seller.Add(product.ID_Seller);
                }
                Models.Tasks.tasks.Add(tmp);
                System.Diagnostics.Debug.Write(Models.Tasks.tasks.Count);
            }
            catch (SqlException)
            {

            }
            myConnection.Close();
            

            

        }
    }
}
