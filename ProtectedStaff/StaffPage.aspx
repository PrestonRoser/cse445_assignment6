<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StaffPage.aspx.cs" Inherits="WebApplication1_Assignment5.StaffPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Staff Page</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1> Staff Page </h1>
            <p> This page is to be implemented in Assignment 6</p>
             <% Response.Write("Hello " + Context.User.Identity.Name + ", "); %>
        </div>
    </form>
</body>
</html>
