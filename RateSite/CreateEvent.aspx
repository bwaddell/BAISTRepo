<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="CreateEvent.aspx.cs" Inherits="CreateEvent" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainBody" runat="Server">

    <div class="form-group border">
        <div class="row">
            <div class="col-xs-4">
                <label>Event ID: </label>
            </div>
            <div class="col-xs-8">
                <asp:TextBox ID="tbEventID" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
            </div>
        </div>
        <div class="row">
            <div class="col-xs-4">
                <label>Facilitator ID:</label>
            </div>
            <div class="col-xs-8">
                <asp:TextBox ID="tbFacilitatorID" runat="server" CssClass="form-control">1</asp:TextBox>
            </div>
        </div>
        <div class="row">
            <div class="col-xs-4">
                <label>Location:</label>
            </div>
            <div class="col-xs-8">
                <asp:TextBox ID="tbLocation" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
        </div>
        <div class="row">
            <div class="col-xs-4">
                <label>Performer:</label>
            </div>
            <div class="col-xs-8">
                <asp:TextBox ID="tbPerformer" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
        </div>
        <div class="row">
            <div class="col-xs-4">
                <label>Nature Of Performance:</label>
            </div>
            <div class="col-xs-8">
                <asp:TextBox ID="tbNatureOfPerformance" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
        </div>
        <div class="row">
            <div class="col-xs-4">
                <label>Event Date:</label>
            </div>
            <div class="col-xs-8">
                <asp:TextBox ID="tbEventDate" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <%--<div class='col-xs-8'>

                <input type='text' class="form-control" id='datetimepicker' />


            </div>
            <script type="text/javascript">
                $(function () {
                    $('#datetimepicker').datetimepicker();
                });
            </script>--%>
        </div>

        <div class="row">
            <div class="col-xs-4">
                <label>Event Begin:</label>
            </div>
            <div class="col-xs-8">
                <asp:TextBox ID="tbEventBegin" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
        </div>
        <div class="row">
            <div class="col-xs-4">
                <label>Event End:</label>
            </div>
            <div class="col-xs-8">
                <asp:TextBox ID="tbEventEnd" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
        </div
        <div class="row">
            <div>
                <asp:Button ID="btnCreateEvent" runat="server" Text="Create Event" OnClick="btnCreateEvent_Click" />
            </div>
            
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Scripts" runat="Server">
</asp:Content>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" runat="Server">
</asp:Content>

