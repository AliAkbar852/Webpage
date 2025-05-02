using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Webpage
{
    public class BLL
    {
        public DataTable GetAllUserImages()
        {
            return new DAL().GetAllUserImagesDAL();
        }

        public bool UserExists(string email)
        {
            return new DAL().UserExists(email);
        }
        public bool InsertUser(User user)
        {
            return new DAL().InsertUserDAL(user);
        }
        public bool UpdateUser(User user)
        {
            return new DAL().UpdateUserDAl(user);
        }
        public bool DeleteUSer(User user)
        {
            return new DAL().DeleteUser(user);
        }
        public DataTable ValidateUser(User user)
        {
            return new DAL().ValidateUserDAL(user);
        }
        public DataTable GetUser(User user)
        {
            return new DAL().GetUserByID(user);
        }
        public DataTable GetUser()
        {
            return new DAL().GetUser();
        }
        public bool UpdateUserInfo(User user)
        {
            return new DAL().UpdateUserInfoDAL(user);
        }
        public bool UpdateUserPassword(string email, string newPassword)
        {
            return new DAL().UpdateUserPasswordDAL(email, newPassword);
        }
        public bool InsertOrUpdateProfilePicture(string email, string profilePictureUrl)
        {
            return new DAL().InsertOrUpdateProfilePicture(email, profilePictureUrl);
        }

        public string GetProfilePictureUrl(string email)
        {
            return new DAL().GetProfilePictureUrl(email);
        }
    }
}