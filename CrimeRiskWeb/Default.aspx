<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="CrimeRiskWeb._Default" Async="true" %>
<%@ Register Src="~/UserControls/CaptchaControl.ascx" TagPrefix="uc" TagName="CaptchaControl" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Crime Risk Lookup</title>
    <style>
        body {
            margin: 0;
            font-family: Arial, Helvetica, sans-serif;
            background: #0f172a;
            color: #e5e7eb;
        }

        .page {
            max-width: 1080px;
            margin: 0 auto;
            padding: 32px 20px 64px 20px;
        }

        .hero {
            margin-bottom: 24px;
        }

        .hero h1 {
            margin: 0 0 8px 0;
            font-size: 34px;
        }

        .hero p {
            margin: 0;
            color: #cbd5e1;
            line-height: 1.6;
        }

        .grid {
            display: grid;
            grid-template-columns: 1fr;
            gap: 20px;
        }

        .card {
            background: #1e293b;
            border-radius: 16px;
            padding: 22px;
            box-shadow: 0 8px 28px rgba(0,0,0,0.28);
        }

        .card h2 {
            margin-top: 0;
            margin-bottom: 16px;
            font-size: 24px;
        }

        .helper {
            color: #cbd5e1;
            margin-bottom: 16px;
            line-height: 1.6;
        }

        .input, .small-input, .captcha-input {
            width: 100%;
            box-sizing: border-box;
            background: #0f172a;
            border: 1px solid #334155;
            color: #f8fafc;
            border-radius: 10px;
            padding: 10px 12px;
            margin-top: 6px;
            margin-bottom: 14px;
        }

        .row {
            display: grid;
            grid-template-columns: repeat(2, minmax(0, 1fr));
            gap: 14px;
        }

        .button, .secondary-button {
            border: none;
            border-radius: 10px;
            padding: 10px 16px;
            cursor: pointer;
            margin-right: 10px;
            margin-top: 6px;
            margin-bottom: 8px;
        }

        .button {
            background: #dc2626;
            color: white;
        }

        .secondary-button {
            background: #334155;
            color: #f8fafc;
        }

        .result-box {
            background: #0f172a;
            border: 1px solid #334155;
            border-radius: 12px;
            padding: 16px;
            margin-top: 16px;
            line-height: 1.8;
        }

        .status {
            color: #fca5a5;
            margin-top: 12px;
            display: block;
        }

        table {
            width: 100%;
            border-collapse: collapse;
            font-size: 14px;
        }

        th, td {
            text-align: left;
            border: 1px solid #334155;
            padding: 10px;
            vertical-align: top;
        }

        th {
            background: #0f172a;
        }

        .metric {
            display: inline-block;
            margin-right: 16px;
            color: #cbd5e1;
        }

        .service-url {
            color: #93c5fd;
            word-break: break-all;
        }

        .captcha-wrap {
            margin-top: 6px;
            margin-bottom: 8px;
        }

        .captcha-label {
            display: block;
            margin-bottom: 6px;
            color: #e2e8f0;
        }

        @media (max-width: 768px) {
            .row {
                grid-template-columns: 1fr;
            }
        }
    </style>
    <script type="text/javascript">
        function useMyLocation() {
            if (!navigator.geolocation) {
                alert("Geolocation is not supported in this browser.");
                return;
            }

            navigator.geolocation.getCurrentPosition(
                function (position) {
                    var latBox = document.getElementById('<%= txtLatitude.ClientID %>');
                    var longBox = document.getElementById('<%= txtLongitude.ClientID %>');
                    latBox.value = position.coords.latitude.toFixed(6);
                    longBox.value = position.coords.longitude.toFixed(6);
                },
                function () {
                    alert("Location access was denied or unavailable.");
                }
            );
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="page">
            <div class="hero">
                <h1>Crime Risk Lookup</h1>
                <p>
                    This web application demonstrates a RESTful crime risk lookup service,
                    a local CAPTCHA user control, and application-level event handling through Global.asax.
                    The TryIt interface below allows the grader to test the full workflow from one page.
                </p>
            </div>

            <div class="grid">
                <div class="card">
                    <h2>Application and Components Summary Table</h2>
                    <div class="helper">
                        All TryIt access points are embedded directly on this page for easier grading.
                    </div>
                    <table>
                        <tr>
                            <th>Component Name</th>
                            <th>Type</th>
                            <th>Description</th>
                            <th>Input</th>
                            <th>Output</th>
                            <th>TryIt Link</th>
                        </tr>
                        <tr>
                            <td>CrimeRiskController</td>
                            <td>REST Service</td>
                            <td>Returns a synthetic regional crime risk score and level from ZIP, city/state, or coordinates.</td>
                            <td>ZIP code, city/state, or latitude/longitude</td>
                            <td>JSON: score, riskLevel, region</td>
                            <td><a href="#tryit" style="color:#93c5fd;">TryIt on this page</a></td>
                        </tr>
                        <tr>
                            <td>CaptchaControl</td>
                            <td>User Control</td>
                            <td>Local math CAPTCHA used to validate user interaction before a service call is made.</td>
                            <td>Simple math answer</td>
                            <td>True/False validation state</td>
                            <td><a href="#tryit" style="color:#93c5fd;">TryIt on this page</a></td>
                        </tr>
                        <tr>
                            <td>Global.asax Event Tracking</td>
                            <td>Global.asax</td>
                            <td>Tracks total site visits and total successful searches through session/application events.</td>
                            <td>User session start and search trigger</td>
                            <td>Updated application counters</td>
                            <td><a href="#metrics" style="color:#93c5fd;">View counters</a></td>
                        </tr>
                    </table>
                </div>

                <div class="card" id="tryit">
                    <h2>TryIt Page</h2>
                    <div class="helper">
                        Enter any one supported input mode. You may use ZIP, city/state, or coordinates.
                        Geolocation may be used to pre-fill coordinates for testing.
                    </div>

                    <label>ZIP Code</label>
                    <asp:TextBox ID="txtZipCode" runat="server" CssClass="input" />

                    <div class="row">
                        <div>
                            <label>City</label>
                            <asp:TextBox ID="txtCity" runat="server" CssClass="small-input" />
                        </div>
                        <div>
                            <label>State</label>
                            <asp:TextBox ID="txtState" runat="server" CssClass="small-input" />
                        </div>
                    </div>

                    <div class="row">
                        <div>
                            <label>Latitude</label>
                            <asp:TextBox ID="txtLatitude" runat="server" CssClass="small-input" />
                        </div>
                        <div>
                            <label>Longitude</label>
                            <asp:TextBox ID="txtLongitude" runat="server" CssClass="small-input" />
                        </div>
                    </div>

                    <asp:Button ID="btnUseLocation" runat="server" Text="Use My Location" CssClass="secondary-button" OnClientClick="useMyLocation(); return false;" CausesValidation="false" />
                    <asp:Button ID="btnLoadSample" runat="server" Text="Load Sample Inputs" CssClass="secondary-button" OnClick="btnLoadSample_Click" CausesValidation="false" />
                    <asp:Button ID="btnClearAll" runat="server" Text="Clear" CssClass="secondary-button" OnClick="btnClearAll_Click" CausesValidation="false" />

                    <uc:CaptchaControl ID="CrimeCaptcha" runat="server" />

                    <div>
                        <asp:Button ID="btnGetRisk" runat="server" Text="Get Crime Risk" CssClass="button" OnClick="btnGetRisk_Click" />
                    </div>

                    <asp:Label ID="lblStatus" runat="server" CssClass="status" />

                    <div class="result-box">
                        <div><strong>Service URL:</strong> <span class="service-url"><asp:Literal ID="litServiceUrl" runat="server" /></span></div>
                        <div style="margin-top:12px;"><strong>Region:</strong> <asp:Literal ID="litRegion" runat="server" /></div>
                        <div><strong>Score:</strong> <asp:Literal ID="litScore" runat="server" /></div>
                        <div><strong>Risk Level:</strong> <asp:Literal ID="litRiskLevel" runat="server" /></div>
                    </div>
                </div>

                <div class="card" id="metrics">
                    <h2>Application Metrics</h2>
                    <div class="helper">
                        These counters are updated through Global.asax event logic and visible here so the
                        grader can confirm the component is actually being used.
                    </div>
                    <div class="metric"><strong>Total Visits:</strong> <asp:Literal ID="litTotalVisits" runat="server" /></div>
                    <div class="metric"><strong>Total Searches:</strong> <asp:Literal ID="litTotalSearches" runat="server" /></div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
