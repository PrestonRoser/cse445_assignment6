<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="staff.aspx.cs" Inherits="Police_Forensics_CSE445.WebForm5" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
         <div>
     <h1>Staff Page</h1>
     
 </div>
 <div>
 <asp:Label ID="user_lb" runat="server" Font-Bold="True" Font-Size="Larger" Height="53px" Text="Welcome user!" Width="233px"></asp:Label>
 </div>
 <p>You can view all criminals checked in and modify the list of crminals</p>
 <br />
         <p>
             <asp:Button ID="criminalman_btn" runat="server" OnClick="criminalman_btn_Click" Text="Manage Criminals" />
         </p>
         <p>
             <asp:Button ID="file_btn" runat="server" OnClick="file_btn_Click" Text="Examine File" />
         </p>
         <p>
             <asp:Button ID="logout_btn" runat="server" OnClick="logout_btn_Click" Text="Logout" />
         </p>
    </form>
</body>
</html>
