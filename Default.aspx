<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebApplication1_Assignment5._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <main>
        <section class="row" aria-labelledby="aspnetTitle">
            <h1 id="aspnetTitle">Police and Forensics Web Application</h1>
            <p> This ASP.NET application is a secure police and forensics platform, providing staff and member access through a login page. 
                It currently offers services such as analyzing web URLs to detect dangerous or malicious content.
            </p>
        </section>
        <div>
            <h2>Buttons to navigate to pages</h2>
        </div>
        <div>

            <asp:Button CssClass="btn btn-primary btn-md" ID="staffbutton" runat="server" Text="Staff Page" Style="margin-right: 10px;" OnClick="staffbutton_Click"></asp:Button>
            <asp:Button CssClass="btn btn-primary btn-md" ID="memberButton" runat="server" Text="Member Page"  Style="margin-right: 10px;" OnClick="memberButton_Click"></asp:Button>
             <asp:Button CssClass="btn btn-primary btn-md" ID="CookieTestingButton" runat="server" Text="CookieTestingPage" Style="margin-right: 10px;" OnClick="CookieTestingButton_Click" />
             <asp:Button CssClass="btn btn-primary btn-md" ID="LogoutBtn" runat="server" Text="Logout" Style="margin-right: 10px;" OnClick="LogoutButton_Click" />
            <asp:Button CssClass="btn btn-primary btn-md" ID="RegisterMemberBtn" runat="server" Text="Register Member" Style="margin-right: 10px;" OnClick="RegisterMemberButton_Click" />

        </div>
        <div>
            
            
           
        </div>
       <div>
           <asp:Table ID="Table1" runat="server" Style="margin-top: 50px;" GridLines="Both" CellSpacing="200" CellPadding="15">
                
            <asp:TableHeaderRow>
                <asp:TableHeaderCell>Provider Name</asp:TableHeaderCell>
                <asp:TableHeaderCell>ASPX Page/Component Type</asp:TableHeaderCell>
                <asp:TableHeaderCell>Description</asp:TableHeaderCell>
                <asp:TableHeaderCell>Resources/Methods</asp:TableHeaderCell>
                 <asp:TableHeaderCell>TryIt Page</asp:TableHeaderCell>
            </asp:TableHeaderRow>

               <asp:TableRow>
                 <asp:TableCell>Khushi</asp:TableCell>
                 <asp:TableCell>Safe URL Web Service Testing Page</asp:TableCell>
                 <asp:TableCell>Downloads contents of url and checks for unsafe keywords, returns boolean result.
                     <br />Webstrar Url: https://webstrarportal.fulton.asu.edu/sites/website117/Page1/ServiceTesting
                     <br />Unsafe url: https://httpbin.org/get?text=phishing
                     <br />Safe url: https://venus.sod.asu.edu/webhome/teaching/honors.html
        
                 </asp:TableCell>
                 <asp:TableCell>File: ServiceTestingPage.aspx  <br />
                     Method: UrlButton_Click 
                     <br />Webstrar Url: https://webstrarportal.fulton.asu.edu/sites/website117/Page1/ServiceTesting
                 </asp:TableCell>
                 <asp:TableCell><a href="ServiceTesting" target="_blank">Go to ServiceTestingPage</a></asp:TableCell>
             </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell>Khushi</asp:TableCell>
                    <asp:TableCell>Safe URL Web Service</asp:TableCell>
                    <asp:TableCell> Service implementation for evaluating if url is safe
                        <br />Service url: https://webstrarportal.fulton.asu.edu/sites/website117/Page1/Service1.svc
                       
                    </asp:TableCell>
                    <asp:TableCell>File: Service1.svc.cs  <br />
                        Method: isSafeUrl 
                    </asp:TableCell>
                    <asp:TableCell><a href="Service1.svc" target="_blank">Go to web service</a></asp:TableCell>
                </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell>Khushi</asp:TableCell>
                <asp:TableCell>Default Page</asp:TableCell>
                <asp:TableCell>Landing page with buttons to navigate to staff and member page, logout, register member and application summary table</asp:TableCell>
                <asp:TableCell> File: Default.aspx.cs. <br />
                    Methods: loginButton_Click, CookieTestingButton_Click, staffbutton_Click, memberButton_Click.</asp:TableCell>
                <asp:TableCell><a href="Default" target="_blank">Go to DefaultPage</a></asp:TableCell>
            </asp:TableRow>

                <asp:TableRow>
                     <asp:TableCell>Khushi</asp:TableCell>
                     <asp:TableCell>Cookies Session State</asp:TableCell>
                     <asp:TableCell> This page embeds the user login control to enable testing the user control. This page retrieves the user name and password from session cookies
                         and displays for testing purposes.
                     </asp:TableCell>
                     <asp:TableCell>File: CookieTestingPage.aspx.cs. <br /> Methods: Page_Load manages cookie sessions.</asp:TableCell>
                     <asp:TableCell><a href="CookieTestingPage" target="_blank">Go to CookieTestingPage</a></asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                      <asp:TableCell>Khushi</asp:TableCell>
                      <asp:TableCell>User Login Control</asp:TableCell>
                      <asp:TableCell>User control for login (session cookies) stores the user entered username and password in the cookie session. </asp:TableCell>
                      <asp:TableCell>File: WebUserControl1.ascx.cs <br /> Methods: Button1_Click manages user name and password in cookie sessions. </asp:TableCell>
                      <asp:TableCell><a href="CookieTestingPage" target="_blank">Go to CookieTestingPage</a></asp:TableCell>
                 </asp:TableRow>

               <asp:TableRow>
                      <asp:TableCell>Khushi</asp:TableCell>
                      <asp:TableCell>Staff Page Redirection</asp:TableCell>
                      <asp:TableCell>Click staff button which redirects to login page if staff not logged in. After login
                          redirects to staff page. Also a page to test login user control.  
                      </asp:TableCell>
                      <asp:TableCell>File: StaffPage.aspx.cs</asp:TableCell>
                      <asp:TableCell><a href="Default" target="_blank">Click staff button on Default.aspx to login and go to staff page</a></asp:TableCell>
                 </asp:TableRow>

               <asp:TableRow>
                   <asp:TableCell>Khushi</asp:TableCell>
                   <asp:TableCell>Member Page Redirection</asp:TableCell>
                   <asp:TableCell>Click member button which redirects to login page if member not logged in. After login
                       redirects to member page. Also a page to test login user control.
                   </asp:TableCell>
                   <asp:TableCell>File: MemberPage.aspx.cs</asp:TableCell>
                   <asp:TableCell><a href="Default" target="_blank">Click member button on Default.aspx to login and go to member page</a></asp:TableCell>
              </asp:TableRow>

                <asp:TableRow>
                         <asp:TableCell>Khushi</asp:TableCell>
                         <asp:TableCell>Member Registration</asp:TableCell>
                         <asp:TableCell>Click register member button which redirects to RegisterNewMember page. In this page, member can enter user id, password,
                             and register. This creates an entry in the App_Data/Member.xml file.
                         </asp:TableCell>
                         <asp:TableCell>File: RegisterNewMember.aspx.cs</asp:TableCell>
                         <asp:TableCell><a href="Default" target="_blank">Click register member button on Default.aspx to go to RegisterNewMember page</a></asp:TableCell>
                    </asp:TableRow>

           </asp:Table>
       </div>
    </main>

</asp:Content>
