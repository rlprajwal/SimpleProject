using CRUDoperationWebApplication.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;

namespace CRUDoperationWebApplication.DataAccessLayer
{
    public class Crudoperationdatalayer
    {

        public String InsertCrudOperation(crudModel Model)
        {
            string result = "";
            var conn = ConfigurationManager.ConnectionStrings["conn"].ConnectionString.ToString();
            if (conn != null)
            {
                using (SqlConnection con = new SqlConnection(conn))
                {
                    SqlCommand cmd = new SqlCommand("File_CrudOperation_Query", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 3000;

                    cmd.Parameters.AddWithValue("@CustomerID", 0);
                    cmd.Parameters.AddWithValue("@Name", Model.Name);
                    cmd.Parameters.AddWithValue("@DOB", Model.Birthdate);
                    cmd.Parameters.AddWithValue("@EmailID", Model.EmailID);
                    cmd.Parameters.AddWithValue("@Path", Model.FilePath);
                    cmd.Parameters.AddWithValue("@filename", Model.Filename);
                    cmd.Parameters.AddWithValue("@Query", 1);
                    con.Open();
                    result = cmd.ExecuteScalar().ToString();
                    con.Close();
                }
            }
            return result;
        }

        public List<crudModel> SelectAllCrudData()
        {
            DataSet ds = new DataSet();
            List<crudModel> dataItem = new List<crudModel>();

            var conn = ConfigurationManager.ConnectionStrings["conn"].ConnectionString.ToString();
            if (conn != null)
            {
                using (SqlConnection con = new SqlConnection(conn))
                {
                    SqlCommand cmd = new SqlCommand("File_CrudOperation_Query", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 3000;

                    cmd.Parameters.AddWithValue("@CustomerID", 0);
                    cmd.Parameters.AddWithValue("@Name", "");
                    cmd.Parameters.AddWithValue("@DOB", "");
                    cmd.Parameters.AddWithValue("@EmailID", "");
                    cmd.Parameters.AddWithValue("@Path", ""); 
                    cmd.Parameters.AddWithValue("@filename", "");
                    cmd.Parameters.AddWithValue("@Query", 2);
                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(ds);
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        crudModel cobj = new crudModel();
                        cobj.CustomerID = Convert.ToInt32(ds.Tables[0].Rows[i]["CustomerID"].ToString());
                        cobj.Name = ds.Tables[0].Rows[i]["Name"].ToString();                        
                        cobj.Birthdate = Convert.ToDateTime(ds.Tables[0].Rows[i]["Birthdate"].ToString());
                        cobj.EmailID = ds.Tables[0].Rows[i]["EmailID"].ToString();
                        cobj.FilePath = ds.Tables[0].Rows[i]["Filedesignation"].ToString();
                        cobj.Filename = ds.Tables[0].Rows[i]["FileName"].ToString();
                        dataItem.Add(cobj);
                    }
                    con.Close();
                }
            }
            return dataItem;
        }

        public List<crudModel> SelectCrudData1(int ID)
        {
            DataSet ds = new DataSet();
            List<crudModel> dataItem = new List<crudModel>();

            var conn = ConfigurationManager.ConnectionStrings["conn"].ConnectionString.ToString();
            if (conn != null)
            {
                using (SqlConnection con = new SqlConnection(conn))
                {
                    SqlCommand cmd = new SqlCommand("File_CrudOperation_Query", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 3000;

                    cmd.Parameters.AddWithValue("@CustomerID", ID);
                    cmd.Parameters.AddWithValue("@Name", "");
                    cmd.Parameters.AddWithValue("@DOB", "");
                    cmd.Parameters.AddWithValue("@EmailID", "");
                    cmd.Parameters.AddWithValue("@Path", "");
                    cmd.Parameters.AddWithValue("@filename", "");
                    cmd.Parameters.AddWithValue("@Query", 3);
                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(ds);
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        crudModel cobj = new crudModel();
                        cobj.CustomerID = Convert.ToInt32(ds.Tables[0].Rows[i]["CustomerID"].ToString());
                        cobj.Name = ds.Tables[0].Rows[i]["Name"].ToString();
                        cobj.Birthdate = Convert.ToDateTime(ds.Tables[0].Rows[i]["Birthdate"].ToString());
                        cobj.DOB = cobj.Birthdate.ToShortDateString();
                        cobj.EmailID = ds.Tables[0].Rows[i]["EmailID"].ToString();
                        cobj.FilePath = ds.Tables[0].Rows[i]["Filedesignation"].ToString();
                        cobj.Filename = ds.Tables[0].Rows[i]["FileName"].ToString();
                        dataItem.Add(cobj);
                    }
                    con.Close();
                }
            }
            return dataItem;
        }

        public String UpdateCrudOperationDetails(crudModel Model)
        {
            string result = "";
            DateTime dt = Convert.ToDateTime(Model.Birthdate);
            string updatedDate = dt.ToString("yyyy-MM-dd HH:mm:ss.fff");
            var conn = ConfigurationManager.ConnectionStrings["conn"].ConnectionString.ToString();
            if (conn != null)
            {
                using (SqlConnection con = new SqlConnection(conn))
                {
                    SqlCommand cmd = new SqlCommand("File_CrudOperation_Query", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 3000;

                    cmd.Parameters.AddWithValue("@CustomerID", Model.CustomerID);
                    cmd.Parameters.AddWithValue("@Name", Model.Name);
                    cmd.Parameters.AddWithValue("@DOB", updatedDate);
                    cmd.Parameters.AddWithValue("@EmailID", Model.EmailID);
                    cmd.Parameters.AddWithValue("@Path", Model.FilePath);
                    cmd.Parameters.AddWithValue("@filename", Model.Filename);
                    cmd.Parameters.AddWithValue("@Query", 4);
                    con.Open();
                    result = cmd.ExecuteScalar().ToString();
                    con.Close();
                }
            }
            return result;
        }

        public String DeleteCrudOperationDetails(int UserId)
        {
            string result = "";
            var conn = ConfigurationManager.ConnectionStrings["conn"].ConnectionString.ToString();
            if (conn != null)
            {
                using (SqlConnection con = new SqlConnection(conn))
                {
                    SqlCommand cmd = new SqlCommand("File_CrudOperation_Query", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 3000;

                    cmd.Parameters.AddWithValue("@CustomerID", UserId);
                    cmd.Parameters.AddWithValue("@Name", "");
                    cmd.Parameters.AddWithValue("@DOB", "");
                    cmd.Parameters.AddWithValue("@EmailID", "");
                    cmd.Parameters.AddWithValue("@Path", "");
                    cmd.Parameters.AddWithValue("@filename", "");
                    cmd.Parameters.AddWithValue("@Query", 5);
                    con.Open();
                    result = cmd.ExecuteScalar().ToString();
                    con.Close();
                }
            }
            return result;
        }
    }
}