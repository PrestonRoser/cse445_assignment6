<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebUserControl1.ascx.cs" Inherits="WebApplication1_Assignment5.WebUserControl1" %>
<div>
<asp:Label ID="Label1" runat="server" Text="Username"></asp:Label>
<asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
</div>
<div>
<asp:Label ID="Label2" runat="server" Text="Password"></asp:Label>
<asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
</div>

<asp:Button ID="Button1" runat="server" Text="Login" OnClick="Button1_Click" />