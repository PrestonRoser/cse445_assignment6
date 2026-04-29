<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="member.aspx.cs" Inherits="Police_Forensics_CSE445.WebForm4" %>

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
        <br />
        <div>
         <asp:ListBox ID="Criminaldb_lb" runat="server" Height="172px" Width="393px"
            DataTextField="criminal_name"
            DataValueField="criminal_name"
            AutoPostBack="true"
            OnSelectedIndexChanged="selected_criminal">
         </asp:ListBox>
        </div>
        <asp:Label ID="Criminalinfo_label" runat="server" Text="Label"></asp:Label>
        <p>
            <asp:Button ID="logout_btn" runat="server" OnClick="logout_btn_Click" Text="Logout" />
        </p>
     </form>
</body>
</html>
