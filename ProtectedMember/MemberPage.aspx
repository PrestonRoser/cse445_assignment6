<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MemberPage.aspx.cs" Inherits="WebApplication1_Assignment5.MemberPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1>Member Page</h1>
        </div>
        <div>
        <asp:Label ID="user_lb" runat="server" Font-Bold="True" Font-Size="Larger" Height="53px" Text="Welcome user!" Width="233px"></asp:Label>
        </div>
        <p>You can view all criminals checked in but you can't check in others, or remove them unless you're a staff member</p>
        <asp:Button ID="criminal_btn" runat="server" Text="Criminal Manager" OnClick="criminal_btn_Click" />
        <br />
        <p>
            <asp:Button ID="logout_btn" runat="server" OnClick="logout_btn_Click" Text="Logout" />
        </p>
     </form>
</body>
</html>
