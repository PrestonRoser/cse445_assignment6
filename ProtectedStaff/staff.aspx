<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="staff.aspx.cs"
    Inherits="Police_Forensics_CSE445.WebForm5" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1>Staff Page</h1>
            <% Response.Write("Hello " + Context.User.Identity.Name + ", "); %>

        </div>
        <div>
        </div>
        
        
        <br />
        <p>
            <asp:Button ID="criminalman_btn" runat="server"
                OnClick="criminalman_btn_Click" Text="Manage Criminals" />
        </p>
        <p>
            <asp:Button ID="file_btn" runat="server"
                OnClick="file_btn_Click" Text="Examine File" />
        </p>
        <p>
            <asp:Button ID="account_btn" runat="server"
                OnClick="account_btn_Click" Text="Account Management" />
        </p>
        <p>
            <asp:Button ID="logout_btn" runat="server"
                OnClick="logout_btn_Click" Text="Logout" />
        </p>
    </form>
</body>
</html>