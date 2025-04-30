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
            background-image: url('https://media-hosting.imagekit.io/5e2ed990d1c145e1/image.jpg?Expires=1840518897&Key-Pair-Id=K2ZIVPTIP2VGHC&Signature=V9FUUGTikfkOoTfTp~T7JhoH1tg2p8H3gQ21cNk8Xs~UJT1MI~gvoSsNbw~ocUSCqqEjfY3~F8muA7d88UMJOp6HE6hzgECIlKYv1gtPNOByB10Q4O3~X40t5CuQnsZX9YE~5IS8ndKK5pMiCM-sTDjFEAcWpqt-iZ7uwvgmqFWD1WxF-48RZnlDpZgSKvbBVBS04sAL0HyCB3ebFOkD5bjxSwaej1MO6FCoUKYCtgxwOZ5X7AzfzI~9~i14z2-7kAVDyVtDXA0X62gsfhmn7flHz7--qgRejxcpmjdNfpDg-e6sCJMQIxNtDoJos9AnD2HK58EdEAKX-7LwsM1rIQ__'); 
            background-size: cover;
            background-position: center;
            background-repeat: no-repeat;
        }

        #form1 {
            background-color: rgba(255, 255, 255, 0.9); 
            padding: 30px;
            border-radius: 10px;
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.2);
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
    <!-- Bootstrap 5 CSS -->
 <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet"/>

</head>
<body>
    <div class="container d-flex justify-content-center align-items-center vh-100">
        <form id="form1" runat="server" class="card p-4 shadow w-100" style="max-width: 400px; background-color: rgba(255, 255, 255, 0.9);">
            <div class="mb-3">
                <asp:Label ID="lbl" Text="Enter Email" runat="server" CssClass="form-label"></asp:Label>
                <asp:TextBox ID="TxtEmail" runat="server" TextMode="Email" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="mb-3">
                <asp:Label Text="Enter Password" runat="server" CssClass="form-label"></asp:Label>
                <asp:TextBox ID="TxtPassword" runat="server" TextMode="Password" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="mb-3">
                <asp:Label ID="LblError" runat="server" CssClass="text-danger"></asp:Label>
            </div>
            <div class="d-flex gap-2">
                <asp:Button ID="BtnLogin" runat="server" Text="Login" CssClass="btn btn-primary w-100" OnClick="BtnLogin_Click" />
                <asp:Button ID="BtnSingUp" Text="Sign Up" runat="server" CssClass="btn btn-secondary w-100" OnClick="BtnSingUp_Click1" />
            </div>
      </form>
    </div>
    <!-- Bootstrap 5 JS -->
<script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>

</body>
</html>