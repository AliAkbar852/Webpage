using Azure.Storage.Blobs;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
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

        protected void btnChangeProfilePic_Click(object sender, EventArgs e)
        {

        }
    }
}
