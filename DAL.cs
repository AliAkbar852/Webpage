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
        public bool UpdateUserInfoDAL(User user)
        {
            var updates = new List<string>();
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Email", user.Email)
            };

            if (!string.IsNullOrEmpty(user.Name))
            {
                updates.Add("Name = @Name");
                parameters.Add(new SqlParameter("@Name", user.Name));
            }
            if (!string.IsNullOrEmpty(user.Age))
            {
                updates.Add("Age = @Age");
                parameters.Add(new SqlParameter("@Age", user.Age));
            }
            if (!string.IsNullOrEmpty(user.Gender))
            {
                updates.Add("Gender = @Gender");
                parameters.Add(new SqlParameter("@Gender", user.Gender));
            }
            if (!string.IsNullOrEmpty(user.Status))
            {
                updates.Add("Status = @Status");
                parameters.Add(new SqlParameter("@Status", user.Status));
            }

            if (updates.Count == 0)
            {
                return false; // No fields to update
            }

            string query = $"UPDATE UserInfo SET {string.Join(", ", updates)} WHERE Email = @Email";
            return new DbCon().UDI(query, parameters);
        }
        public bool UpdateUserPasswordDAL(string email, string newPassword)
        {
            string query = "UPDATE UserInfo SET Password = @Password WHERE Email = @Email";
            List<SqlParameter> parameters = new List<SqlParameter>()
            {
                new SqlParameter("@Password", newPassword),
                new SqlParameter("@Email", email)
            };
            return new DbCon().UDI(query, parameters);
        }
        public bool InsertOrUpdateProfilePicture(string email, string profilePictureUrl)
        {
            string query = @"
            IF EXISTS (SELECT 1 FROM UserProfilePictures WHERE Email = @Email)
                UPDATE UserProfilePictures SET ProfilePictureUrl = @ProfilePictureUrl, UploadDate = GETDATE() WHERE Email = @Email
            ELSE
                INSERT INTO UserProfilePictures (Email, ProfilePictureUrl) VALUES (@Email, @ProfilePictureUrl)";
            List<SqlParameter> parameters = new List<SqlParameter>()
        {
            new SqlParameter("@Email", email),
            new SqlParameter("@ProfilePictureUrl", profilePictureUrl)
        };
            return new DbCon().UDI(query, parameters);
        }

        public string GetProfilePictureUrl(string email)
        {
            string query = "SELECT ProfilePictureUrl FROM UserProfilePictures WHERE Email = @Email";
            List<SqlParameter> parameters = new List<SqlParameter>()
        {
            new SqlParameter("@Email", email)
        };
            DataTable dt = new DbCon().Search(query, parameters);
            if (dt.Rows.Count > 0)
            {
                return dt.Rows[0]["ProfilePictureUrl"].ToString();
            }
            return null;
        }
    }
}