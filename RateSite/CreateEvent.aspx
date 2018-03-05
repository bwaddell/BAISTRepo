<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="CreateEvent.aspx.cs" Inherits="CreateEvent" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainBody" runat="Server">



    <div class="form-group border">


        <label>Event ID: </label>
        <asp:TextBox ID="tbEventID" runat="server" CssClass="form-control">1</asp:TextBox>

        <label>Facilitator ID:</label>
        <asp:TextBox ID="tbFacilitatorID" runat="server" CssClass="form-control">1</asp:TextBox>

        <label>Location:</label>
        <asp:TextBox ID="tbLocation" runat="server" CssClass="form-control"></asp:TextBox>

        <label>Performer:</label>
        <asp:TextBox ID="tbPerformer" runat="server" CssClass="form-control"></asp:TextBox>

        <label>Nature Of Performance:</label>
        <asp:TextBox ID="tbNatureOfPerformance" runat="server" CssClass="form-control"></asp:TextBox>

        <label>Event Date:</label>
        <asp:TextBox ID="tbEventDate" runat="server" CssClass="form-control"></asp:TextBox>

        <label>Event Begin:</label>
        <asp:TextBox ID="tbEventBegin" runat="server" CssClass="form-control"></asp:TextBox>

        <label>Event End:</label>
        <asp:TextBox ID="tbEventEnd" runat="server" CssClass="form-control"></asp:TextBox>

    </div>




    <asp:Button ID="btnCreateEvent" runat="server" Text="Create Event" OnClick="btnCreateEvent_Click" />






</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" runat="Server">
</asp:Content>

