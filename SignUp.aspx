<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SignUp.aspx.cs" Inherits="Webpage.SignUp" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Sign Up</title>
    <style type="text/css">
        body {
            font-family: Arial, sans-serif;
            background-color: #f4f4f4;
            display: flex;
            justify-content: center;
            align-items: center;
            height: 100vh;
            margin: 0;
        }

        .signup-container {
            background-color: white;
            padding: 20px;
            border-radius: 8px;
            box-shadow: 0 0 10px rgba(0,0,0,0.1);
            width: 400px;
        }

        .signup-container h2 {
            text-align: center;
            margin-bottom: 20px;
        }

        .form-group {
            margin-bottom: 15px;
        }

        .form-group label {
            display: block;
            margin-bottom: 5px;
        }

        .form-control {
            width: 100%;
            padding: 8px;
            border: 1px solid #ccc;
            border-radius: 4px;
            box-sizing: border-box;
        }

        .btn-submit {
            width: 100%;
            padding: 10px;
            background-color: #28a745;
            color: white;
            border: none;
            border-radius: 4px;
            cursor: pointer;
        }

        .btn-submit:hover {
            background-color: #218838;
        }

        .error {
            color: red;
            font-size: 12px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="signup-container">
            <h2>Sign Up</h2>
            <asp:Label ID="lblMessage" runat="server" ForeColor="Red" Visible="false"></asp:Label>
            
            <div class="form-group">
                <label for="txtEmail">Email:</label>
                <asp:TextBox ID="txtEmail" runat="server" TextMode="Email" CssClass="form-control"></asp:TextBox>
            </div>

            <div class="form-group">
                <label for="txtPassword">Password:</label>
                <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control"></asp:TextBox>
            </div>

            <div class="form-group">
                <label for="txtConfirmPassword">Confirm Password:</label>
                <asp:TextBox ID="txtConfirmPassword" runat="server" TextMode="Password" CssClass="form-control"></asp:TextBox>
            </div>

            <div class="form-group">
                <label for="txtName">Name:</label>
                <asp:TextBox ID="txtName" runat="server" CssClass="form-control"></asp:TextBox>
            </div>

            <div class="form-group">
                <label for="txtAge">Age:</label>
                <asp:TextBox ID="txtAge" runat="server" TextMode="Number" CssClass="form-control"></asp:TextBox>
            </div>

            <div class="form-group">
                <label for="ddlGender">Gender:</label>
                <asp:DropDownList ID="ddlGender" runat="server" CssClass="form-control">
                    <asp:ListItem Value="" Text="Select Gender"></asp:ListItem>
                    <asp:ListItem Value="Male" Text="Male"></asp:ListItem>
                    <asp:ListItem Value="Female" Text="Female"></asp:ListItem>
                </asp:DropDownList>
            </div>

            <div class="form-group">
                <label for="ddlStatus">Status:</label>
                <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control">
                    <asp:ListItem Value="" Text="Select Status"></asp:ListItem>
                    <asp:ListItem Value="Single" Text="Single"></asp:ListItem>
                    <asp:ListItem Value="Married" Text="Married"></asp:ListItem>
                    <asp:ListItem Value="Divorced" Text="Divorced"></asp:ListItem>
                    <asp:ListItem Value="Widowed" Text="Widowed"></asp:ListItem>
                </asp:DropDownList>
            </div>

            <div class="form-group">
                <asp:Button ID="btnSubmit" runat="server" Text="Sign Up" CssClass="btn-submit" OnClick="btnSubmit_Click" />
            </div>
            <div>
                <asp:Button ID="btnLogin" runat="server" Text="Login" CssClass="btn-submit" OnClick="btnLogin_Click" />
            </div>
        </div>
    </form>
</body>
</html>
