<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AccountManagement.aspx.cs"
    Inherits="WebApplication1_Assignment5.AccountManagement" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Account Management</title>
</head>
<body>
    <form id="form1" runat="server">
        <h2>Account Management</h2>

        <p>
            <strong>Username:</strong>
            <asp:Label ID="UsernameLabel" runat="server" />
        </p>
        <p>
            <strong>Role:</strong>
            <asp:Label ID="RoleLabel" runat="server" />
        </p>

        <hr />
        <h3>Update Email</h3>
        <asp:Label ID="Label1" runat="server" Text="New Email:" />
        <asp:TextBox ID="EmailBox" runat="server" />
        <asp:Button ID="SaveEmailBtn" runat="server" Text="Update Email"
            OnClick="SaveEmailBtn_Click" />

        <hr />
        <h3>Update Password</h3>
        <asp:Label ID="Label2" runat="server" Text="Current Password:" />
        <asp:TextBox ID="OldPasswordBox" runat="server" TextMode="Password" />
        <br />
        <asp:Label ID="Label3" runat="server" Text="New Password:" />
        <asp:TextBox ID="NewPasswordBox" runat="server" TextMode="Password" />
        <br />
        <asp:Label ID="Label4" runat="server" Text="Confirm New Password:" />
        <asp:TextBox ID="ConfirmPasswordBox" runat="server" TextMode="Password" />
        <br />
        <asp:Button ID="SavePasswordBtn" runat="server" Text="Update Password"
            OnClick="SavePasswordBtn_Click" />

        <hr />
        <asp:Label ID="MessageLabel" runat="server" ForeColor="Red" />
        <br />
        <asp:Button ID="BackBtn" runat="server" Text="Back" OnClick="BackBtn_Click" />
    </form>
</body>
</html>