using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace Webpage
{
    public class DAL
    {
        //public bool InsertUserDAL(User user)
        //{
        //    return new DbCon().UDI("Insert into UserInfo values(Name,Email,Password,Gender,Status,Age");
        //}
        public DataTable GetAllUserImagesDAL()
        {
            string query = "SELECT Email, ImagePath FROM UserImages";
            return new DbCon().Search(query);
        }

        public bool UpdateUserDAl(User user)
        {
            return new DbCon().UDI("UPDATE UserInfo where id ='" + user.Id + "'");
        }
        public bool DeleteUser(User user)
        {
            return new DbCon().UDI("Delete UserInfo wher id='" + user.Id + "'");
        }
        public DataTable ValidateUserDAL(User user)
        {
            string query = "SELECT * FROM UserInfo WHERE Email = @Email AND Password = @Password";
            List<SqlParameter> parameters = new List<SqlParameter>()
            {
               new SqlParameter("@Email", user.Email),
                 new SqlParameter("@Password", user.Password)
             };

            return new DbCon().Search(query, parameters);
        }
        public bool InsertUserDAL(User user)
        {
            string query = "INSERT INTO UserInfo (Name, Email, Password, Gender, Status, Age) " +
                           "VALUES (@Name, @Email, @Password, @Gender, @Status, @Age)";

            List<SqlParameter> parameters = new List<SqlParameter>()
    {
        new SqlParameter("@Name", user.Name),
        new SqlParameter("@Email", user.Email),
        new SqlParameter("@Password", user.Password), // Ideally hashed
        new SqlParameter("@Gender", user.Gender),
        new SqlParameter("@Status", user.Status),
        new SqlParameter("@Age", user.Age)
    };

            return new DbCon().UDI(query, parameters);
        }
        public bool UserExists(string email)
        {
            string query = "SELECT COUNT(*) FROM UserInfo WHERE Email = @Email";
            List<SqlParameter> parameters = new List<SqlParameter>()
    {
        new SqlParameter("@Email", email)
    };

            DataTable dt = new DbCon().Search(query, parameters);
            int count = Convert.ToInt32(dt.Rows[0][0]);
            return count > 0;
        }

        public DataTable GetUserByID(User user)
        {
            return new DbCon().Search("Select * From UserInfo where id='" + user.Id + "'");
        }
        public DataTable GetUser()
        {
            string qry = "Select * from UserInfo";
            return new DbCon().Search(qry);
        }
    }
}