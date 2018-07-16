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
                    CssClass="form-control"></asp:TextBox>
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
                    CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidatorDesc" runat="server" ErrorMessage="This Field is Required" Display="Dynamic" ControlToValidate="tbNatureOfPerformance"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidatorDescLength" runat="server" ErrorMessage="Maximum 100 characters allowed." Display="Dynamic"
                    ValidationExpression = "^[\s\S]{0,100}$" ControlToValidate="tbNatureOfPerformance"></asp:RegularExpressionValidator>
            </div>
<%--            <div class="form-group">
                <label>Event Date:</label>
                <asp:TextBox ID="tbEventDate" runat="server"
                    CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidatorDate" runat="server" ErrorMessage="Date is Required" Display="Dynamic" ControlToValidate="tbEventDate"></asp:RequiredFieldValidator>
            </div>--%>

            <div class="form-group">
                <label>Opening Message:</label>
                <p>(Optional) Leave a message that will be viewed by all evaluators after they join the event.</p>
                <asp:TextBox ID="OpenTxt" runat="server" MaxLength="4000" TextMode="MultiLine"
                    CssClass="form-control"></asp:TextBox>
                <asp:RegularExpressionValidator ID="RegularExpressionValidatorOpen" runat="server" ErrorMessage="Maximum 4000 characters allowed." Display="Dynamic"
                    ValidationExpression = "^[\s\S]{0,4000}$" ControlToValidate="OpenTxt"></asp:RegularExpressionValidator>
            </div>


            <div class="form-group">
                <label>Closing Message:</label>
                <p>(Optional) Leave a message that will be viewed by all evaluators after the event has ended.  For example, where to find more information about your organization or link a survey.</p>
                <asp:TextBox ID="CloseTxt" runat="server" MaxLength="4000" TextMode="MultiLine"
                    CssClass="form-control"></asp:TextBox>
                <asp:RegularExpressionValidator ID="RegularExpressionValidatorClose" runat="server" ErrorMessage="Maximum 4000 characters allowed." Display="Dynamic"
                    ValidationExpression = "^[\s\S]{0,4000}$" ControlToValidate="CloseTxt"></asp:RegularExpressionValidator>
            </div>

            <div class="form-group">
                <label>Voting Criteria:</label>
                <p>(Optional) If you wish to give voter the choice of what the criteria they will be evaluating, enter each option here.</p>
                <asp:TextBox ID="critTxt" runat="server" MaxLength="256"
                    CssClass="form-control"></asp:TextBox>
                <asp:ListBox ID="allCritLB" runat="server" CssClass="form-control"></asp:ListBox>
                <asp:Button ID="AddCritBTN" CausesValidation="false" runat="server" Text="Add" CssClass="btn btn-dark" OnClick="AddCritBTN_Click" />
                <asp:Button ID="RemoveCritBRN" CausesValidation="false" runat="server" Text="Remove" CssClass="btn btn-dark" OnClick="RemoveCritBRN_Click" />
            </div>

            <div class="form-group">
                <label>Custom Questions:</label>
                <p>(Optional) Add questions that you wish all evaluators to answer before they can join the event.</p>
                <asp:TextBox ID="newQTB" runat="server" MaxLength="1024"
                    CssClass="form-control"></asp:TextBox>
                <asp:ListBox ID="allQsLB" runat="server" CssClass="form-control"></asp:ListBox>
                <asp:Button ID="AddQBTN" CausesValidation="false" runat="server" Text="Add" CssClass="btn btn-dark"  OnClick="AddQBTN_Click" />
                <asp:Button ID="RemoveQBTN" CausesValidation="false" runat="server" Text="Remove" CssClass="btn btn-dark"  OnClick="RemoveQBTN_Click" />
            </div>

            <div class="form-group">
                <label>Voting Method:</label>
                <p>Choose which voting method you would like your evaluator to use.</p>
                <asp:RadioButtonList ID="RadioButtonList1" runat="server">
                    <asp:ListItem Value="Arrows" Selected="True">Up/Downvote Arrows</asp:ListItem>
                    <asp:ListItem Value="Slider" Enabled="false">Sliding Scale (coming soon)</asp:ListItem>
                </asp:RadioButtonList>
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

