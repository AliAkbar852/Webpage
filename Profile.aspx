<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="Webpage.Profile" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Profile</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <style>
        body {
            font-family: 'Arial', sans-serif;
            background-color: #f4f4f9;
            margin: 0;
            padding: 20px;
            position: relative;
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

        input[type="submit"], .btn, .logout-btn {
            background-color: #1abc9c;
            color: white;
            border: none;
            padding: 10px 20px;
            border-radius: 5px;
            cursor: pointer;
            font-size: 14px;
            transition: background-color 0.3s;
        }

        input[type="submit"]:hover, .btn:hover, .logout-btn:hover {
            background-color: #16a085;
        }

        .status-label {
            color: #e74c3c;
            margin: 10px 0;
            display: block;
        }

        /* Form Elements */
        input[type="text"], input[type="email"], input[type="password"], select {
            width: 100%;
            max-width: 300px;
            padding: 8px;
            margin: 5px 0;
            border: 1px solid #ccc;
            border-radius: 4px;
            box-sizing: border-box;
        }

        label {
            display: block;
            margin: 10px 0 5px;
        }

        /* Modal Styles */
        .modal {
            display: none;
            position: fixed;
            z-index: 1001;
            left: 0;
            top: 0;
            width: 100%;
            height: 100%;
            overflow: auto;
            background-color: rgba(0,0,0,0.4);
        }

        .modal-content {
            background-color: #fefefe;
            margin: 15% auto;
            padding: 20px;
            border: 1px solid #888;
            width: 80%;
            max-width: 500px;
            border-radius: 8px;
            box-shadow: 0 2px 10px rgba(0, 0, 0, 0.1);
        }

        .close {
            color: #aaa;
            float: right;
            font-size: 28px;
            font-weight: bold;
        }

        .close:hover,
        .close:focus {
            color: black;
            text-decoration: none;
            cursor: pointer;
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
        <div class="navbar">
            <a href="Dashboard.aspx">Dashboard</a>
            <a href="Profile.aspx" class="active">Profile</a>
            <a href="Settings.aspx">Settings</a>
            <a href="#" class="logout">
                <asp:Button ID="btnLogout" runat="server" Text="Logout" OnClick="btnLogout_Click" CssClass="logout-btn" />
            </a>
        </div>
        <div>
            <asp:Label Text="Name" runat="server" />
            <asp:TextBox ID="TxtName" runat="server" />
        </div>
        <div>
            <asp:Label Text="Email" runat="server" />
            <asp:TextBox ID="TxtEmail" runat="server" TextMode="Email" />
        </div>
        <div>
            <asp:Label Text="Age" runat="server" />
            <asp:TextBox ID="TxtAge" runat="server" />
        </div>
        <div>
            <label for="ddlGender">Gender:</label>
            <asp:DropDownList ID="ddlGender" runat="server">
                <asp:ListItem Value="" Text="Select Gender" />
                <asp:ListItem Value="Male" Text="Male" />
                <asp:ListItem Value="Female" Text="Female" />
            </asp:DropDownList>
        </div>
        <div>
            <label for="ddlStatus">Status:</label>
            <asp:DropDownList ID="ddlStatus" runat="server">
                <asp:ListItem Value="" Text="Select Status" />
                <asp:ListItem Value="Single" Text="Single" />
                <asp:ListItem Value="Married" Text="Married" />
                <asp:ListItem Value="Divorced" Text="Divorced" />
                <asp:ListItem Value="Widowed" Text="Widowed" />
            </asp:DropDownList>
        </div>
        <div>
            <asp:Button ID="btnUpdateInfo" Text="Update Info" runat="server" CssClass="btn" OnClick="btnUpdateInfo_Click" />"
            <asp:Label ID="lblUpdateStatus" Text="" runat="server" CssClass="status-label" />
        </div>
        <div class="upload-section">
            <h3>Profile Picture</h3>
            <asp:Button ID="btnChangeProfilePic" runat="server" Text="Change Profile Pic" CssClass="btn" OnClientClick="openModal('profilePicModal'); return false;" />
            <asp:Label ID="lblStatus" runat="server" CssClass="status-label" />
        </div>
        <div class="upload-section">
            <h3>Password</h3>
            <asp:Button ID="btnOpenChangePassword" runat="server" Text="Change Password" CssClass="btn" OnClientClick="openModal('PasswordChangeModal'); return false;" />
        </div>
        <div>
            <asp:Label ID="lblChangePassword" Text="" runat="server" CssClass="status-label" />
        </div>
        <!-- Modal for Profile Picture -->
        <div id="profilePicModal" class="modal">
            <div class="modal-content">
                <span class="close" onclick="closeModal('profilePicModal')">&times;</span>
                <h3>Upload Profile Picture</h3>
                <asp:FileUpload ID="fuProfilePicture" runat="server" />
                <br />
                <asp:Button ID="btnUploadProfilePic" runat="server" Text="Save" CssClass="btn" OnClick="btnUploadProfilePic_Click" />
            </div>
        </div>

        <!-- Modal for Password Change -->
        <div id="PasswordChangeModal" class="modal">
            <div class="modal-content">
                <span class="close" onclick="closeModal('PasswordChangeModal')">&times;</span>
                <h3>Change Password</h3>
                <div>
                    <asp:Label Text="Old Password" runat="server" />
                    <asp:TextBox ID="TxtOldPassword" runat="server" TextMode="Password" />
                </div>
                <div>
                    <asp:Label Text="New Password" runat="server" />
                    <asp:TextBox ID="TxtNewPassword" runat="server" TextMode="Password" />
                </div>
                <div>
                    <asp:Label Text="Confirm Password" runat="server" />
                    <asp:TextBox ID="ConfirmPassword" runat="server" TextMode="Password" />
                </div>
                <asp:Button ID="btnChangePassword" runat="server" Text="Save" CssClass="btn" OnClick="btnChangePassword_Click" />
            </div>
        </div>
    </form>

    <script>
        function openModal(modalId) {
            document.getElementById(modalId).style.display = 'block';
        }

        function closeModal(modalId) {
            document.getElementById(modalId).style.display = 'none';
        }

        window.onclick = function (event) {
            var modals = ['profilePicModal', 'PasswordChangeModal'];
            modals.forEach(function (modalId) {
                var modal = document.getElementById(modalId);
                if (event.target == modal) {
                    modal.style.display = 'none';
                }
            });
        }
    </script>
</body>
</html>