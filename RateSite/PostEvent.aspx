<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="PostEvent.aspx.cs" Inherits="PostEvent" Theme="ContinUI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainBody" runat="Server">

    <div class="row text-center">
            <h2>This Event has completed.  Thank you for participating.</h2>
    </div>
    <hr />
    <asp:Panel ID="PanelMSG" Visible="false" runat="server">
        <div class="row">
            <div class="form-group">
                <label>Please read this message from the event creator:</label>
                <asp:TextBox ID="tbCloseMessage" runat="server" CssClass="form-control" TextMode="MultiLine" ReadOnly="true"></asp:TextBox>
            </div>
        </div>

    </asp:Panel>
    <hr />
    <div class="row center-block">
        <div class="form-group text-center">
            <label><a href="Default.aspx">Click here</a> to join another event or create your own.</label>
        </div>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" runat="Server">
</asp:Content>

