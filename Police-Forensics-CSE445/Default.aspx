<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Police_Forensics_CSE445.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body style="height: 736px">
    <form id="form1" runat="server">
        <h1>Police Forensics application</h1>
        <p>Welcome to our Police Forensics application, this application is a portal for civilians to use basic services our police force uses.

           </p>
        <p>
            This is also an application that police officers, and detectives can use to access information about cases, and run applications through a malware detection system. 
        </p>
        <p>Provided services: Virustotal, criminal database</p>
        <p>Users can sign up for a free account by creating a username and password</p>
            <asp:ListBox ID="Criminaldb_lb" runat="server" Height="172px" Width="393px"
                DataTextField="criminal_name"
                DataValueField="criminal_name"
                AutoPostBack="true"
                OnSelectedIndexChanged="selected_criminal">
            </asp:ListBox>
        
        <p>&nbsp;</p>
        <p>
            <asp:Label ID="Criminalinfo_label" runat="server" Text="Criminal History"></asp:Label>
        </p>
        <p>
            <asp:Button ID="criminalman_btn" runat="server" OnClick="criminalman_btn_Click" Text="Manage Criminals" Width="182px" />
        </p>
        <p>
            <asp:Button ID="file_btn" runat="server" OnClick="file_btn_Click" Text="Examine File" />
        </p>
        <p>
            &nbsp;</p>
        <p>
            &nbsp;</p>
        <p>
            <asp:Label ID="session_lb" runat="server" Text="Label"></asp:Label>
        </p>
    </form>
</body>
</html>
