<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="CreateEvent.aspx.cs" Inherits="CreateEvent" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainBody" runat="Server">

    <h3 class="mt-5">Create Event Page</h3>
    <p>This page is for Facilitators to Create a new Event</p>

    <div class="row">
        <div class="col-lg">


            <div class="form-group">
                <asp:Label ID="lbAccount" runat="server" Text=""></asp:Label>
            </div>
            <%--            <div class="form-group">
                <label>Event ID: </label>
                <asp:TextBox ID="tbEventID" runat="server"
                    CssClass="form-control" ReadOnly="true"></asp:TextBox>
            </div>--%>

            <%--            <div class="form-group">
                <label>Facilitator ID:</label>
                <asp:TextBox ID="tbFacilitatorID"  runat="server" 
                    CssClass="form-control" TextMode="Number">1</asp:TextBox>
            </div>--%>
            <div class="form-group">
                <label>Location of Event:</label>
                <asp:TextBox ID="tbLocation" runat="server"
                    CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
            </div>
            <div class="form-group">
                <label>Performer:</label>
                <asp:TextBox ID="tbPerformer" runat="server"
                    CssClass="form-control"></asp:TextBox>
            </div>
            <div class="form-group">
                <label>Nature Of Performance:</label>
                <asp:TextBox ID="tbNatureOfPerformance" runat="server"
                    CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
            </div>
            <div class="form-group">
                <label>Event Date:</label>
                <asp:TextBox ID="tbEventDate" runat="server"
                    CssClass="form-control" TextMode="Date"></asp:TextBox>
            </div>

            <div class="form-group">
                <asp:Button ID="btnCreateEvent" runat="server"
                    Text="Create Event" OnClick="btnCreateEvent_Click"
                    CssClass="btn btn-dark" />
                <asp:Label ID="lbstatus" runat="server" Text="Status: "></asp:Label>
            </div>
        </div>
    </div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" runat="Server">
</asp:Content>

