using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using System.IO;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace Webpage
{
    public partial class Dashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["userEmail"] == null)
            {
                Response.Redirect("LoginForm.aspx");
            }
            if (!IsPostBack)
            {
                LoadImages();
                LoadProfilePicture();
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


        protected void btnUpload_Click(object sender, EventArgs e)
        {
            if (FileUpload1.HasFile)
            {
                string filename = Guid.NewGuid().ToString() + Path.GetExtension(FileUpload1.FileName);
                string blobContainerName = "userimages"; // make sure it's lowercase and already created in Azure
                string email = Session["userEmail"].ToString();

                // Azure Blob Storage setup
                string connectionString = ConfigurationManager.AppSettings["AzureBlobStorageConnectionString"];
                BlobContainerClient container = new BlobContainerClient(connectionString, blobContainerName);
                container.CreateIfNotExists(PublicAccessType.Blob);

                BlobClient blob = container.GetBlobClient(filename);

                using (Stream stream = FileUpload1.PostedFile.InputStream)
                {
                    blob.Upload(stream, overwrite: true);
                }

                string blobUrl = blob.Uri.ToString();

                // Save URL to DB
                string query = "INSERT INTO UserImages (UserEmail, ImagePath) VALUES (@Email, @Path)";
                List<SqlParameter> parameters = new List<SqlParameter>()
        {
            new SqlParameter("@Email", email),
            new SqlParameter("@Path", blobUrl)
        };

                new DbCon().UDI(query, parameters);

                lblUploadStatus.Text = "Image uploaded to Azure Blob Storage!";
                lblUploadStatus.ForeColor = Color.Green;

                LoadImages(); // refresh list
            }
            else
            {
                lblUploadStatus.Text = "Please select an image.";
                lblUploadStatus.ForeColor = Color.Red;
            }
            //if (FileUpload1.HasFile)
            //{
            //    string filename = Path.GetFileName(FileUpload1.FileName);
            //    string folderPath = "UserImages/";
            //    string filePath = folderPath + filename;

            //    // Save file
            //    FileUpload1.SaveAs(Server.MapPath("~/" + filePath));

            //    // Save path to DB
            //    string email = Session["userEmail"].ToString(); // assuming you store email on login
            //    string query = "INSERT INTO UserImages (UserEmail, ImagePath) VALUES (@Email, @Path)";
            //    List<SqlParameter> parameters = new List<SqlParameter>()
            //    {
            //        new SqlParameter("@Email", email),
            //        new SqlParameter("@Path", filePath)
            //    };

            //    new DbCon().UDI(query, parameters);

            //    lblUploadStatus.Text = "Image uploaded successfully!";
            //    lblUploadStatus.ForeColor = Color.Green;

            //    LoadImages(); // refresh image list
            //}
            //else
            //{
            //    lblUploadStatus.Text = "Please select an image.";
            //    lblUploadStatus.ForeColor = Color.Red;
            //}
        }

        private void LoadImages()
        {
            string query = "SELECT * FROM UserImages ORDER BY UploadDate DESC";
            DataTable dt = new DbCon().Search(query);
            rptImages.DataSource = dt;
            rptImages.DataBind();
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            Response.Redirect("LoginForm.aspx");
        }

        protected void btnProfile_Click(object sender, EventArgs e)
        {
            Response.Redirect("Profile.aspx");
        }
    }
}