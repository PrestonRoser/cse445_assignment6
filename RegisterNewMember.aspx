<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RegisterNewMember.aspx.cs" Inherits="WebApplication1_Assignment5.RegisterNewMember" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="Label1" runat="server" Text="Member username"></asp:Label>
            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
            <br />
            <asp:Label ID="Label2" runat="server" Text="Member password"></asp:Label>
            <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
            <br />
            <asp:Button ID="Button1" runat="server" Text="Register Member" OnClick="Button1_Click" />
            <br />
            <asp:Label ID="Output" runat="server" Text=""></asp:Label>
        </div>
    </form>
</body>
</html>
