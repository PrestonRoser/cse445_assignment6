<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="File-analysis.aspx.cs" Inherits="Police_Forensics_CSE445.WebForm3" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            Examine Files for malicious content</div>
        <asp:FileUpload ID="fileupload" runat="server" />
        <asp:Label ID="scancount_lb" runat="server" Text="Total files scanned:"></asp:Label>
        <p>
            <asp:Button ID="scan_btn" runat="server" OnClick="scan_btn_Click" Text="Scan file" />
        </p>
        <p>
            <asp:Label ID="results_lb" runat="server" Text="File Results"></asp:Label>
        </p>
        <p>
            <asp:Button ID="criminalman_btn" runat="server" OnClick="criminalman_btn_Click" Text="Manage Criminals" />
        </p>
        <p>
            <asp:Button ID="home_btn" runat="server" OnClick="home_btn_Click" Text="Home" />
        </p>
        <p>
            &nbsp;</p>
        <p>
            <asp:Label ID="session_lb" runat="server" Text="Label"></asp:Label>
        </p>
    </form>
</body>
</html>
