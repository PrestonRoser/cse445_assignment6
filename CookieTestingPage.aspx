<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CookieTestingPage.aspx.cs" Inherits="WebApplication1_Assignment5.CookieTestingPageaspx" %>

<%@ Register Src="~/WebUserControl1.ascx" TagPrefix="uc1" TagName="WebUserControl1" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1> Cookie Testing Page for Staff </h1>
            <uc1:webusercontrol1 runat="server" id="WebUserControl1" />
            <div>
                <asp:Label ID="LabelUsername" runat="server" Text=""></asp:Label>
                <asp:Label ID="LabelPassword" runat="server" Text=""></asp:Label>
            </div>
        </div>
    </form>
</body>
</html>
