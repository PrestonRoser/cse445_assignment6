<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ServiceTesting.aspx.cs" Inherits="WebApplication1_Assignment5.WebForm2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>  </title>
</head>

<body>
    <form id="form1" runat="server">
        <div>
            <h1>
                URL Safety Checker
            </h1>
            <asp:TextBox ID="UrlTextBox" runat="server" placeholder="Enter url" Width="600px"> </asp:TextBox>
            <asp:Button ID="UrlButton" runat="server" Text="Check url" OnClick="UrlButton_Click" />
            <asp:Label ID="SafeLabel" runat="server" Text=""></asp:Label>
        </div>
    </form>
</body>
</html>
