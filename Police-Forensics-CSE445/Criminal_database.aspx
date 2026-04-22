<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Criminal_database.aspx.cs" Inherits="Police_Forensics_CSE445.WebForm2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:ListBox ID="Criminaldb_lb" runat="server" Height="172px" Width="393px"
                DataTextField="criminal_name"
                DataValueField="criminal_name"
                AutoPostBack="true"
                OnSelectedIndexChanged="selected_criminal">

            </asp:ListBox>
            <asp:Label ID="count_lb" runat="server" Text="Criminal Booking Count"></asp:Label>
        </div>
        <p>
            <asp:Label ID="Criminalinfo_label" runat="server" Text="Criminal History"></asp:Label>
        </p>
        <p>
            <asp:Button ID="release_btn" runat="server" OnClick="release_btn_Click" Text="Release inmate" />
        </p>
        <p>
            <asp:Label ID="Label3" runat="server" Text="Criminal first and last name:"></asp:Label>
            <asp:TextBox ID="name_tb" runat="server"></asp:TextBox>
        </p>
        <p>
            <asp:Label ID="Label2" runat="server" Text="Crime:"></asp:Label>
            <asp:TextBox ID="crime_tb" runat="server"></asp:TextBox>
        </p>
        <p>
            <asp:Label ID="Label1" runat="server" Text="State:"></asp:Label>
            <asp:TextBox ID="state_tb" runat="server" Height="25px" style="margin-top: 2px" Width="202px"></asp:TextBox>
        </p>
        <p>
            <asp:Button ID="addcriminal_btn" runat="server" OnClick="addcriminal_btn_Click" Text="Book Criminal" />
        </p>
        <p>
            &nbsp;</p>
        <p>
            &nbsp;</p>
        <p>
            <asp:Button ID="file_btn" runat="server" OnClick="file_btn_Click" Text="Examine File" />
        </p>
        <p>
        <asp:Button ID="home_btm" runat="server" OnClick="home_btm_Click" Text="Home" />
        </p>
        <p>
            &nbsp;</p>
        <p>
            <asp:Label ID="session_lb" runat="server" Text="Label"></asp:Label>
        </p>
        <p>
            &nbsp;</p>
        <p>
            &nbsp;</p>
        <p>
            &nbsp;</p>
        <p>
            &nbsp;</p>
        <p>
            &nbsp;</p>
        <p>
            &nbsp;</p>
        <p>
            &nbsp;</p>
        <p>
            &nbsp;</p>
        <p>
            &nbsp;</p>
        <p>
            &nbsp;</p>
        <p>
            &nbsp;</p>
        <p>
            &nbsp;</p>
        <p>
            &nbsp;</p>
        <p>
            &nbsp;</p>
    </form>
</body>
</html>
