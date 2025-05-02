using Azure.Storage.Blobs;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Webpage
{
	public partial class Profile : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
            if(!IsPostBack)
            {
                LoadProfilePicture();
            }
            if (Session["userEmail"] == null)
            {
                Response.Redirect("LoginForm.aspx");
            }
		}
        private void LoadProfilePicture()
        {
            string email = Session["userEmail"].ToString();
            BLL logic = new BLL();
            string profilePicUrl = logic.GetProfilePictureUrl(email);
            if (!string.IsNullOrEmpty(profilePicUrl))
            {
                imgProfilePic.ImageUrl = profilePicUrl;
            }
            else
            {
                imgProfilePic.ImageUrl = "https://media-hosting.imagekit.io/5e2ed990d1c145e1/image.jpg?Expires=1840518897&Key-Pair-Id=K2ZIVPTIP2VGHC&Signature=V9FUUGTikfkOoTfTp~T7JhoH1tg2p8H3gQ21cNk8Xs~UJT1MI~gvoSsNbw~ocUSCqqEjfY3~F8muA7d88UMJOp6HE6hzgECIlKYv1gtPNOByB10Q4O3~X40t5CuQnsZX9YE~5IS8ndKK5pMiCM-sTDjFEAcWpqt-iZ7uwvgmqFWD1WxF-48RZnlDpZgSKvbBVBS04sAL0HyCB3ebFOkD5bjxSwaej1MO6FCoUKYCtgxwOZ5X7AzfzI~9~i14z2-7kAVDyVtDXA0X62gsfhmn7flHz7--qgRejxcpmjdNfpDg-e6sCJMQIxNtDoJos9AnD2HK58EdEAKX-7LwsM1rIQ__";
            }
        }
        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            Response.Redirect("LoginForm.aspx");
        }

        protected void btnUploadProfilePic_Click(object sender, EventArgs e)
        {
            if (fuProfilePicture.HasFile)
            {
                try
                {
                    string filename = Guid.NewGuid().ToString() + Path.GetExtension(fuProfilePicture.FileName);
                    string blobContainerName = "profilepictures"; // Ensure this container exists in Azure
                    string email = Session["userEmail"].ToString();

                    // Azure Blob Storage setup
                    string connectionString = ConfigurationManager.AppSettings["AzureBlobStorageConnectionString"];
                    BlobContainerClient container = new BlobContainerClient(connectionString, blobContainerName);
                    container.CreateIfNotExists(Azure.Storage.Blobs.Models.PublicAccessType.Blob);

                    BlobClient blob = container.GetBlobClient(filename);
                    using (Stream stream = fuProfilePicture.PostedFile.InputStream)
                    {
                        blob.Upload(stream, overwrite: true);
                    }

                    string blobUrl = blob.Uri.ToString();

                    // Save URL to database
                    BLL logic = new BLL();
                    bool isSaved = logic.InsertOrUpdateProfilePicture(email, blobUrl);

                    if (isSaved)
                    {
                        lblStatus.Text = "Profile picture updated successfully!";
                        lblStatus.ForeColor = System.Drawing.Color.Green;
                    }
                    else
                    {
                        lblStatus.Text = "Failed to update profile picture.";
                        lblStatus.ForeColor = System.Drawing.Color.Red;
                    }
                }
                catch (Exception ex)
                {
                    lblStatus.Text = "Error: " + ex.Message;
                    lblStatus.ForeColor = System.Drawing.Color.Red;
                }
            }
            else
            {
                lblStatus.Text = "Please select an image.";
                lblStatus.ForeColor = System.Drawing.Color.Red;
            }
        }
        protected void btnChangePassword_Click(object sender, EventArgs e)
        {
            try
            {
                string email = Session["userEmail"]?.ToString();
                if (string.IsNullOrEmpty(email))
                {
                    lblChangePassword.Text = "User not logged in.";
                    lblChangePassword.ForeColor = System.Drawing.Color.Red;
                    return;
                }

                string oldPassword = TxtOldPassword.Text.Trim();
                string newPassword = TxtNewPassword.Text.Trim();
                string confirmPassword = ConfirmPassword.Text.Trim();

                // Validate new and confirm passwords match
                if (newPassword != confirmPassword)
                {
                    lblChangePassword.Text = "New password and confirm password do not match.";
                    lblChangePassword.ForeColor = System.Drawing.Color.Red;
                    return;
                }

                // Validate new password requirements (e.g., minimum length)
                if (newPassword.Length < 4)
                {
                    lblChangePassword.Text = "New password must be at least 4 characters long.";
                    lblChangePassword.ForeColor = System.Drawing.Color.Red;
                    return;
                }

                // Verify old password
                BLL logic = new BLL();
                User user = new User { Email = email, Password = oldPassword };
                DataTable dt = logic.ValidateUser(user);
                if (dt.Rows.Count == 0)
                {
                    lblChangePassword.Text = "Incorrect old password.";
                    lblChangePassword.ForeColor = System.Drawing.Color.Red;
                    return;
                }

                // Update password
                bool isUpdated = logic.UpdateUserPassword(email, newPassword);
                if (isUpdated)
                {
                    lblChangePassword.Text = "Password changed successfully.";
                    lblChangePassword.ForeColor = System.Drawing.Color.Green;
                    // Clear password fields
                    TxtOldPassword.Text = string.Empty;
                    TxtNewPassword.Text = string.Empty;
                    ConfirmPassword.Text = string.Empty;
                }
                else
                {
                    lblChangePassword.Text = "Failed to update password.";
                    lblChangePassword.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (Exception ex)
            {
                lblChangePassword.Text = $"Error changing password: {ex.Message}";
                lblChangePassword.ForeColor = System.Drawing.Color.Red;
            }
        }


        protected void btnChangeProfilePic_Click(object sender, EventArgs e)
        {

        }

        protected void btnUpdateInfo_Click(object sender, EventArgs e)
        {
            try
            {
                string email = Session["userEmail"]?.ToString();
                if (string.IsNullOrEmpty(email))
                {
                    lblStatus.Text = "User not logged in.";
                    lblStatus.ForeColor = System.Drawing.Color.Red;
                    return;
                }

                // Check if at least one field is filled
                if (string.IsNullOrWhiteSpace(TxtName.Text) &&
                    string.IsNullOrWhiteSpace(TxtEmail.Text) &&
                    string.IsNullOrWhiteSpace(TxtAge.Text) &&
                    string.IsNullOrWhiteSpace(ddlGender.SelectedValue) &&
                    string.IsNullOrWhiteSpace(ddlStatus.SelectedValue))
                {
                    lblUpdateStatus.Text = "Please fill at least one field to update.";
                    lblUpdateStatus.ForeColor = System.Drawing.Color.Red;
                    return;
                }

                // Validate inputs
                string errorMessage = "";
                if (!string.IsNullOrWhiteSpace(TxtEmail.Text) &&
                    !Regex.IsMatch(TxtEmail.Text.Trim(), @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$"))
                {
                    errorMessage += "Invalid email format.<br />";
                }

                if (!string.IsNullOrWhiteSpace(TxtAge.Text) &&
                    (!int.TryParse(TxtAge.Text.Trim(), out int age) || age < 18 || age > 100))
                {
                    errorMessage += "Age must be between 18 and 100.<br />";
                }

                if (!string.IsNullOrEmpty(errorMessage))
                {
                    lblUpdateStatus.Text = errorMessage;
                    lblUpdateStatus.ForeColor = System.Drawing.Color.Red;
                    return;
                }

                // Prepare user object with only filled fields
                User user = new User
                {
                    Email = email, // Used to identify the user
                    Name = string.IsNullOrWhiteSpace(TxtName.Text) ? null : TxtName.Text.Trim(),
                    Age = string.IsNullOrWhiteSpace(TxtAge.Text) ? null : TxtAge.Text.Trim(),
                    Gender = string.IsNullOrWhiteSpace(ddlGender.SelectedValue) ? null : ddlGender.SelectedValue,
                    Status = string.IsNullOrWhiteSpace(ddlStatus.SelectedValue) ? null : ddlStatus.SelectedValue
                };

                // Update user info
                BLL logic = new BLL();
                bool isUpdated = logic.UpdateUserInfo(user);
                if (isUpdated)
                {
                    lblUpdateStatus.Text = "Profile updated successfully.";
                    lblUpdateStatus.ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    lblUpdateStatus.Text = "Failed to update profile.";
                    lblUpdateStatus.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (Exception ex)
            {
                lblUpdateStatus.Text = $"Error updating profile: {ex.Message}";
                lblUpdateStatus.ForeColor = System.Drawing.Color.Red;
            }
        }
    }
}
