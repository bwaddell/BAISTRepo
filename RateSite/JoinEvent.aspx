<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="JoinEvent.aspx.cs" Inherits="PreEvent" Theme="ContinUI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainBody" runat="Server">

    <h2>Join Event</h2>

    <div class="row">
        <div class="form-group">
            <label>Location of Event:</label>
            <asp:TextBox ID="tbLocation" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
        </div>
    </div>
    <div class="row">
        <div class="form-group">
            <label>Performer:</label>
            <asp:TextBox ID="tbPerformer" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
        </div>
    </div>
    <div class="row">
        <div class="form-group">
            <label>Description:</label>
            <asp:TextBox ID="tbDescription" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
        </div>
    </div>

    <div class="row">
        <asp:Panel ID="PanelOpenMsg" runat="server" Visible="false">
            <div class="form-group">
                <label>Message from Event Creator:</label>
                <asp:TextBox ID="TBOpenMsg" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
            </div>

        </asp:Panel>
    </div>

    <div class="row">
        <div class="form-group">
            <label>Name:</label>
            <p>Please enter a name you would like to be identified as.</p>
            <asp:TextBox ID="tbName" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
    </div>

    <div class="row">
        <asp:Panel ID="PanelQuestions" runat="server" Visible="false">
            <div class="form-group">
                <label>Pre-Event Questions:</label>
                <p>Please answer these short questions from the event creator.</p>
                <%-- add questions and answer textboxes here --%>
            </div>
        </asp:Panel>
    </div>
    <hr />

    <div class="row">
        <asp:Panel ID="PanelCrit" runat="server" Visible="false">
            <div class="form-group">
                <label>Criteria:</label>
                <p>Please select what criteria you will be evaluating the event on.</p>
                <asp:DropDownList ID="DDLCrit" CssClass="form-control" runat="server"></asp:DropDownList>
            </div>
        </asp:Panel>
    </div>

    <hr />
    <div class="form-group row">
        <p class="col-md-6">
            By joining this event, you consent to the use of Cookies to ensure you get the best experience during your visit.
                       <a href="http://www.continui.uk/Privacy.aspx">Read our Privacy Notice</a>
        </p>
        <div class="col-md-6">
            <asp:CheckBox ID="consentCheck" Checked="false" runat="server" Text="I agree (you must agree to join)" />
        </div>
    </div>

    <div class="row">
        <div class="form-group">
            <asp:Button ID="JoinBTN" runat="server" Text="Join" CssClass="btn btn-dark" OnClick="JoinBTN_Click" />
        </div>
    </div>

    <%-- Display event info, answer questions, add name, join event --%>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" runat="Server">
</asp:Content>

