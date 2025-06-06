﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="Webpage.Dashboard" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Dashboard</title>
    <style>
        body {
            font-family: 'Arial', sans-serif;
            background-color: #f4f4f9;
            margin: 0;
            padding: 20px;
        }

         .profile-pic {
            position: fixed;
            top: 10px;
            left: 10px;
            z-index: 1000;
        }

        .profile-pic img {
            width: 50px;
            height: 50px;
            border-radius: 50%;
            object-fit: cover;
            border: 2px solid #fff;
            box-shadow: 0 2px 5px rgba(0, 0, 0, 0.2);
        }

        /* Navbar Styles */
        .navbar {
             background-color: #2c3e50;
            border-radius: 8px;
            box-shadow: 0 2px 5px rgba(0, 0, 0, 0.2);
            margin-bottom: 20px;
            display: flex;
            flex-wrap: wrap;
            justify-content: space-between;
            align-items: center;
            padding: 10px;
        }

         .navbar a, .navbar .logout {
            color: white;
            text-decoration: none;
            font-size: 16px;
            padding: 10px;
            margin: 5px;
            display: block;
            text-align: center;
            background-color: transparent;
            border-radius: 5px;
            transition: background-color 0.3s;
        }

        .navbar a:hover, .navbar .logout:hover {
            background-color: #34495e;
        }

        .navbar a.active {
            background-color: #1abc9c;
        }

        /* Form Styles */
        #form1 {
           max-width: 1000px;
            margin: 0 auto;
            background: white;
            padding: 20px;
            border-radius: 8px;
            box-shadow: 0 2px 10px rgba(0, 0, 0, 0.1);
        }

        .upload-section {
           margin-bottom: 20px;
            display: flex;
            flex-wrap: wrap;
            gap: 10px;
            align-items: center;
        }

        input[type="file"] {
            padding: 10px;
        }

        input[type="submit"], .logout-btn {
             background-color: #1abc9c;
            color: white;
            border: none;
            padding: 10px 20px;
            border-radius: 5px;
            cursor: pointer;
            font-size: 14px;
            transition: background-color 0.3s;
        }

        input[type="submit"]:hover, .logout-btn:hover {
            background-color: #16a085;
        }

        .status-label {
            color: #e74c3c;
            margin: 10px 0;
            display: block;
        }

        hr {
            border: 0;
            border-top: 1px solid #ddd;
            margin: 20px 0;
        }

        /* Repeater Image Gallery */
        .image-gallery {
            display: grid;
            grid-template-columns: repeat(auto-fit, minmax(150px, 1fr));
            gap: 20px;
        }

        .image-gallery > div {
             background: white;
            padding: 10px;
            border-radius: 8px;
            box-shadow: 0 2px 5px rgba(0, 0, 0, 0.1);
            text-align: center;
            transition: transform 0.3s;
        }

        .image-gallery > div:hover {
            transform: translateY(-5px);
        }

        .image-gallery img {
           border-radius: 5px;
            object-fit: cover;
            width: 100%;
            height: auto;
            max-height: 150px;
        }

        .image-gallery p {
           margin: 10px 0 0;
            color: #333;
            font-size: 14px;
            word-wrap: break-word;
        }
         @media (max-width: 600px) {
            .upload-section {
                flex-direction: column;
                align-items: stretch;
            }

            .navbar {
                flex-direction: column;
            }
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
         <div class="profile-pic">
            <asp:Image ID="imgProfilePic" runat="server" AlternateText="Profile Picture" />
        </div>

        <!-- Navbar -->
        <div class="navbar">
            <a href="#" class="active">Dashboard</a>
            <a href="#"><asp:Button ID="btnProfile" Text="Profile" runat="server" OnClick="btnProfile_Click" /> </a>
            <a href="#"><asp:Button ID="btnSetting" Text="Settings" runat="server" /> </a>
            <a href="#" class="logout">
                <asp:Button ID="btnLogout" runat="server" Text="Logout" OnClick="btnLogout_Click" CssClass="logout-btn" />
            </a>
        </div>

        <!-- Upload Section -->
        <div class="upload-section">
            <asp:FileUpload ID="FileUpload1" runat="server" />
            <asp:Button ID="btnUpload" runat="server" Text="Upload Image" OnClick="btnUpload_Click" />
            <asp:Label ID="lblUploadStatus" runat="server" CssClass="status-label" />
        </div>

        <hr />

        <!-- Image Gallery -->
        <div class="image-gallery">
            <asp:Repeater ID="rptImages" runat="server">
                <ItemTemplate>
                    <div>
                        <img src='<%# Eval("ImagePath") %>' width="150" height="150" />
                        <p><%# Eval("UserEmail") %></p>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </form>
</body>
</html>
