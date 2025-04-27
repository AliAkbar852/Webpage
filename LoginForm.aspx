<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginForm.aspx.cs" Inherits="Webpage.LoginForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login Page</title>
    <style>
        body {
            margin: 0;
            padding: 0;
            font-family: Arial, sans-serif;
            background-image: url('/image.jpg'); 
            background-size: cover;
            background-position: center;
            background-repeat: no-repeat;
            height: 100vh;
            display: flex;
            justify-content: center;
            align-items: center;
        }

        #form1 {
            background-color: rgba(255, 255, 255, 0.9); /* Semi-transparent white background */
            padding: 30px;
            border-radius: 10px;
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.2);
            width: 100%;
            max-width: 400px;
            text-align: center;
        }

        .form-group {
            margin-bottom: 20px;
            text-align: left;
        }

        .form-group label {
            display: block;
            margin-bottom: 5px;
            font-weight: bold;
            color: #333;
        }

        .form-group input[type="text"],
        .form-group input[type="password"],
        .form-group input[type="email"] {
            width: 100%;
            padding: 12px 15px; /* Increased padding for better appearance */
            border: 1px solid #d1d1d1; /* Lighter, cleaner border */
            border-radius: 8px; /* More pronounced rounded corners */
            font-size: 16px;
            box-sizing: border-box;
            background-color: #f9f9f9; /* Subtle background color */
            box-shadow: inset 0 1px 3px rgba(0, 0, 0, 0.1); /* Inner shadow for depth */
            transition: all 0.3s ease; /* Smooth transition for hover/focus */
        }

        .form-group input[type="text"]:focus,
        .form-group input[type="password"]:focus,
        .form-group input[type="email"]:focus {
            border-color: #007bff;
            background-color: #fff; /* White background on focus */
            box-shadow: 0 0 8px rgba(0, 123, 255, 0.3); /* Glow effect on focus */
            outline: none;
        }

        #LblError {
            color: red;
            font-size: 14px;
            margin-bottom: 15px;
            display: block;
        }

        .button-group {
            display: flex;
            justify-content: space-between;
            gap: 10px;
        }

        input[type="submit"],
        input[type="button"] {
            background-color: #007bff;
            color: white;
            padding: 10px 20px;
            border: none;
            border-radius: 5px;
            cursor: pointer;
            font-size: 16px;
            width: 100%;
        }

        input[type="submit"]:hover,
        input[type="button"]:hover {
            background-color: #0056b3;
        }

        #ShowLoadGridView {
            margin-top: 20px;
            width: 100%;
            border-collapse: collapse;
        }

        #ShowLoadGridView th,
        #ShowLoadGridView td {
            border: 1px solid #ddd;
            padding: 8px;
            text-align: left;
        }

        #ShowLoadGridView th {
            background-color: #f2f2f2;
            font-weight: bold;
        }

        #ShowLoadGridView tr:nth-child(even) {
            background-color: #f9f9f9;
        }

        #ShowLoadGridView tr:hover {
            background-color: #f1f1f1;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="form-group">
            <asp:Label ID="lbl" Text="Enter Email" runat="server"></asp:Label>
            <asp:TextBox ID="TxtEmail" runat="server" TextMode="Email"></asp:TextBox>
        </div>
        <div class="form-group">
            <asp:Label Text="Enter Password" runat="server"></asp:Label>
            <asp:TextBox ID="TxtPassword" runat="server" TextMode="Password"></asp:TextBox>
        </div>
        <div>
            <asp:Label ID="LblError" runat="server"></asp:Label>
        </div>
        <div class="button-group">
            <asp:Button ID="BtnLogin" runat="server" Text="Login" OnClick="BtnLogin_Click" />
            <asp:Button ID="BtnSingUp" Text="Sign Up" runat="server" OnClick="BtnSingUp_Click1" />
        </div>
       
    </form>
</body>
</html>