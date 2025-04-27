using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace Webpage
{
    public class DbCon
    {
        private SqlConnection con;
        public DbCon()
        {
            string ConnectionString = "Data Source=DESKTOP-6BJDJ1A;Initial Catalog=Projects;Integrated Security=True;TrustServerCertificate=True";
            con = new SqlConnection(ConnectionString);
        }
        public bool UDI(string qry)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand(qry, con);
            bool result = cmd.ExecuteNonQuery() > 0;
            con.Close();
            return result;
        }
        public DataTable Search(string query, List<SqlParameter> parameters)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand(query, con);

            if (parameters != null)
            {
                cmd.Parameters.AddRange(parameters.ToArray());
            }

            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            con.Close();
            return dt;
        }
        public bool UDI(string query, List<SqlParameter> parameters)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand(query, con);

            if (parameters != null)
            {
                cmd.Parameters.AddRange(parameters.ToArray());
            }

            bool result = cmd.ExecuteNonQuery() > 0;
            con.Close();
            return result;
        }

        public DataTable Search(string qry)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand(qry, con);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            con.Close();
            return dt;
        }
    }
}