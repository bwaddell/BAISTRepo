<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master"
    AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" Theme="ContinUI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <!-- Global Site Tag (gtag.js) - Google Analytics -->
    <%--replace 'GA_TRACKING_ID' with your tracking ID code--%>
    <script async src="https://www.googletagmanager.com/gtag/js?id=GA_TRACKING_ID"></script>
    <script>
        window.dataLayer = window.dataLayer || [];
        function gtag(){dataLayer.push(arguments);}
        gtag('js', new Date());       
        gtag('config', 'GA_TRACKING_ID');
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainBody" runat="Server">
    <div class="jumbotron">
        <h1>ContinUI</h1>
        <p>The Continuous Rating Site</p>
    </div>


    <div class="col-md-8 col-md-offset-2 text-center">

        <%--    <p>If you want to join an exsiting event enter the Event key below.</p>
    <p>If you want to create a new event click the Create New Event button below.</p>--%>

        <div class="form-group row">
            <asp:TextBox ID="tbEventKey" class="form-control text-center" runat="server" MaxLength="4"
                placeholder="Enter the event key"></asp:TextBox>
        </div>
        <div class="form-group row">
            <asp:Button ID="JoinButton" class="btn btn-default btn-block"
                runat="server" Text="Join Existing Event" OnClick="JoinButton_Click" />
            <h4>Or</h4>
            <asp:Button ID="CreateButton" class="btn btn-default btn-block"
                runat="server" Text="Create New Event"
                OnClick="CreateButton_Click" />
        </div>
        <div class="row">
            <div class="defaultText">
                <asp:Label ID="statuslbl" class="defaultText" runat="server"
                    Text=""></asp:Label>
            </div>
        </div>
        <div class="row">
            <a href="About.aspx">New to ContinUI?</a>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" runat="Server">
</asp:Content>

