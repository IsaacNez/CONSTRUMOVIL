﻿using System;
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
    public class CategoryController : ApiController
    {
        public string UpdateConnectionString(string baseString, string[] uattr, string[] uvalue, string[] cattr, string[] cvalue)
        {
            string formingString = baseString;
            formingString = formingString + "SET ";
            for(int i = 0; i < uattr.Length; i++)
            {
                if(i == (uattr.Length - 1))
                {
                    formingString = formingString + uattr[i] + "='" + uvalue[i]+"' ";
                }
                else
                {
                    formingString = formingString + uattr[i] + "='" + uvalue[i] + "', ";
                }
            }
            formingString = formingString + "WHERE ";
            for(int i = 0; i < cattr.Length; i++)
            {
                if(i == (cattr.Length - 1))
                {
                    formingString = formingString + cattr[i] + "='" + cvalue[i] + "';";
                }
                else
                {
                    formingString = formingString + cattr[i] + "='" + cvalue[i] + "' AND ";
                }
            }
            return formingString;
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
            action = updateString.UpdateConnectionString("UPDATE CATEGORY ", uattr, uvalue, cattr, cvalue);
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
        public static IList<Category> prolist = new List<Category>();
        [AcceptVerbs("GET")]
        public Category RPCStyleMethodFetchFirstEmployees()
        {
            return prolist.FirstOrDefault();
        }
        static private string GetConnectionString()
        {
            return @"Data Source=ISAAC\ISAACSERVER;Initial Catalog=EPATEC;"
                + "Integrated Security=true;";
        }

        [HttpGet]
        [ActionName("Get")]
        public JsonResult<List<Category>> Get(string attribute, string id)
        {
            string[] attr = attribute.Split(',');
            string[] ids = id.Split(',');
            List<Category> values = new List<Category>();
            Category emp = null;

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
                action = constructor.FormConnectionString("CATEGORY", attr, ids);
            }
            else
            {
                action = "SELECT * FROM CATEGORY;";
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
                emp = new Category();
                emp.CA_ID = reader.GetValue(0).ToString();
                emp.CA_Description = reader.GetValue(1).ToString();
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
            SqlConnection DeletePC = new SqlConnection();
            DeletePC.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            SqlCommand PCmd = new SqlCommand();
            PCmd.CommandType = CommandType.Text;
            SucursalController deleteString = new SucursalController();
            PCmd.CommandText = "DELETE FROM PC WHERE " + attribute + "='" + id + "';";
            PCmd.Connection = DeletePC;
            DeletePC.Open();
            PCmd.ExecuteNonQuery();
            DeletePC.Close();

            SqlConnection myConnection = new SqlConnection();
            myConnection.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = "DELETE FROM CATEGORY WHERE " + actions[0] + "='" + ids[0] + "';";
            sqlCmd.Connection = myConnection;
            myConnection.Open();
            try
            {   
                Sync tmp = new Sync();
                sqlCmd.ExecuteNonQuery();
                System.Diagnostics.Debug.Write("borró");
                tmp.action = "delete";
                tmp.model = ids[0];
                tmp.table = "CATEGORY";
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
        public void AddCategory(Category category)
        {
            Sync tmp = new Sync();

            System.Diagnostics.Debug.WriteLine(category.CA_ID);
            System.Diagnostics.Debug.WriteLine("entrando al post");

            SqlConnection myConnection = new SqlConnection();
            myConnection.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.Text;
            System.Diagnostics.Debug.WriteLine(myConnection.State);

            sqlCmd.CommandText = "INSERT INTO CATEGORY(CA_ID,CA_Description,CA_Status) Values(@CA_ID,@CA_Description,@CA_Status)";
            System.Diagnostics.Debug.WriteLine("generando comando");

            sqlCmd.Connection = myConnection;
            sqlCmd.Parameters.AddWithValue("@CA_ID", category.CA_ID);
            sqlCmd.Parameters.AddWithValue("@CA_Description", category.CA_Description);
            sqlCmd.Parameters.AddWithValue("@CA_Status", category.CA_Status);

            myConnection.Open();
            var javaScriptSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            string jsonString = javaScriptSerializer.Serialize(category);
            sqlCmd.ExecuteNonQuery();
            System.Diagnostics.Debug.Write("insertó");
            tmp.action = "insert";
            tmp.model = jsonString;
            tmp.table = "CATEGORY";
            if (category.ID_Seller != 0)
            {
                tmp.seller.Add(category.ID_Seller);
            }
            Models.Tasks.tasks.Add(tmp);
            string jsonString1 = javaScriptSerializer.Serialize(tmp);
            System.Diagnostics.Debug.WriteLine(Models.Tasks.tasks.Count);
            System.Diagnostics.Debug.WriteLine(jsonString1.Replace(@"\", ""));
            myConnection.Close();
        }
    }
}
