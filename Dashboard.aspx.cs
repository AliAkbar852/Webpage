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
    }
}