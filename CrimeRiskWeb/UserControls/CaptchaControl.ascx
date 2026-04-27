<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CaptchaControl.ascx.cs" Inherits="CrimeRiskWeb.UserControls.CaptchaControl" %>

<div class="captcha-wrap">
    <asp:Label ID="lblPrompt" runat="server" CssClass="captcha-label" />
    <asp:TextBox ID="txtAnswer" runat="server" CssClass="captcha-input" />
    <asp:Button ID="btnRefreshCaptcha" runat="server" Text="New CAPTCHA" CssClass="secondary-button" OnClick="btnRefreshCaptcha_Click" CausesValidation="false" />
</div>
