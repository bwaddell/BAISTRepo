<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" 
    CodeFile="CreateEvent.aspx.cs" Inherits="CreateEvent" Theme="ContinUI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <!-- Global Site Tag (gtag.js) - Google Analytics -->
    <%--replace 'GA_TRACKING_ID' with your tracking ID code--%>
    <script async src="https://www.googletagmanager.com/gtag/js?id=UA-8088578-2"></script>
    <script>
        window.dataLayer = window.dataLayer || [];
        function gtag(){dataLayer.push(arguments);}
        gtag('js', new Date());       
        gtag('config', 'UA-8088578-2');
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainBody" runat="Server">

    <h2>Create Event</h2>

    <div class="row">
        <div class="col-lg">
            <div class="form-group">
                <asp:Label ID="lbAccount" runat="server" Text=""></asp:Label>
            </div>

            <div class="form-group">
                <label>Location of Event:</label>
                <asp:TextBox ID="tbLocation" runat="server"
                    CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidatorLoc" runat="server" ErrorMessage="Location Field is Required" Display="Dynamic" ControlToValidate="tbLocation"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidatorLocLength" runat="server" ErrorMessage="Maximum 100 characters allowed." Display="Dynamic"
                    ValidationExpression = "^[\s\S]{0,100}$" ControlToValidate="tbLocation"></asp:RegularExpressionValidator>
            </div>
            <div class="form-group">
                <label>Performer:</label>
                <asp:TextBox ID="tbPerformer" runat="server"
                    CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidatorPerf" runat="server" ErrorMessage="Performer Field is Required" Display="Dynamic" ControlToValidate="tbPerformer"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidatorPerfLength" runat="server" ErrorMessage="Maximum 100 characters allowed." Display="Dynamic"
                    ValidationExpression = "^[\s\S]{0,100}$" ControlToValidate="tbPerformer"></asp:RegularExpressionValidator>
            </div>
            <div class="form-group">
                <label>Nature Of Performance:</label>
                <asp:TextBox ID="tbNatureOfPerformance" runat="server"
                    CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidatorDesc" runat="server" ErrorMessage="This Field is Required" Display="Dynamic" ControlToValidate="tbNatureOfPerformance"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidatorDescLength" runat="server" ErrorMessage="Maximum 100 characters allowed." Display="Dynamic"
                    ValidationExpression = "^[\s\S]{0,100}$" ControlToValidate="tbNatureOfPerformance"></asp:RegularExpressionValidator>
            </div>
            <div class="form-group">
                <label>Event Date:</label>
                <asp:TextBox ID="tbEventDate" runat="server"
                    CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidatorDate" runat="server" ErrorMessage="Date is Required" Display="Dynamic" ControlToValidate="tbEventDate"></asp:RequiredFieldValidator>
            </div>

            <div class="form-group">
                <asp:Button ID="btnCreateEvent" runat="server"
                    Text="Create Event" OnClick="btnCreateEvent_Click"
                    CssClass="btn btn-dark" />
                <asp:Label ID="lbstatus" runat="server" Text=""></asp:Label>
            </div>
        </div>
    </div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" runat="Server">
</asp:Content>

