using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace Webpage
{
    public partial class SignUp : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
                // Page initialization if needed
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            // Manual server-side validation
            bool isValid = true;
            string errorMessage = "";

            // Check required fields
            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                isValid = false;
                errorMessage += "Email is required.<br />";
            }
            else if (!Regex.IsMatch(txtEmail.Text, @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$"))
            {
                isValid = false;
                errorMessage += "Invalid email format.<br />";
            }

            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                isValid = false;
                errorMessage += "Password is required.<br />";
            }

            if (string.IsNullOrWhiteSpace(txtConfirmPassword.Text))
            {
                isValid = false;
                errorMessage += "Confirm Password is required.<br />";
            }
            else if (txtPassword.Text != txtConfirmPassword.Text)
            {
                isValid = false;
                errorMessage += "Passwords do not match.<br />";
            }

            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                isValid = false;
                errorMessage += "Name is required.<br />";
            }

            if (string.IsNullOrWhiteSpace(txtAge.Text))
            {
                isValid = false;
                errorMessage += "Age is required.<br />";
            }
            else if (!int.TryParse(txtAge.Text, out int age) || age < 18 || age > 100)
            {
                isValid = false;
                errorMessage += "Age must be between 18 and 100.<br />";
            }

            if (string.IsNullOrWhiteSpace(ddlGender.SelectedValue))
            {
                isValid = false;
                errorMessage += "Gender is required.<br />";
            }

            if (string.IsNullOrWhiteSpace(ddlStatus.SelectedValue))
            {
                isValid = false;
                errorMessage += "Status is required.<br />";
            }

            if (!isValid)
            {
                lblMessage.Text = errorMessage;
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Visible = true;
                return;
            }

            try
            {
                BLL logic = new BLL();

                if (logic.UserExists(txtEmail.Text.Trim()))
                {
                    lblMessage.Text = "User with this email already exists.";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    lblMessage.Visible = true;
                    return;
                }

                // Proceed to insert if not exists
                User user = new User()
                {
                    Name = txtName.Text.Trim(),
                    Email = txtEmail.Text.Trim(),
                    Password = txtPassword.Text.Trim(), // Consider hashing
                    Gender = ddlGender.SelectedValue,
                    Status = ddlStatus.SelectedValue,
                    Age = txtAge.Text.Trim()
                };

                bool isInserted = logic.InsertUser(user);

                if (isInserted)
                {
                    lblMessage.Text = "Registration successful!";
                    lblMessage.ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    lblMessage.Text = "Registration failed. Please try again.";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                }
                lblMessage.Visible = true;

                // Clear form
                txtEmail.Text = "";
                txtPassword.Text = "";
                txtConfirmPassword.Text = "";
                txtName.Text = "";
                txtAge.Text = "";
                ddlGender.SelectedIndex = 0;
                ddlStatus.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                lblMessage.Text = "An error occurred: " + ex.Message;
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Visible = true;
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            Response.Redirect("LoginForm.aspx");
        }
    }
}