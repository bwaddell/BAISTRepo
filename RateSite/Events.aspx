<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" 
    CodeFile="Events.aspx.cs" Inherits="Events" Theme="ContinUI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainBody" Runat="Server">

    <h2>Event List</h2>

    <div class="row">
        <div class="col-md-12">
            <div class="form-group">

                <asp:Table ID="tblEventList" runat="server" CssClass="table">
                    <asp:TableHeaderRow>
                        <asp:TableHeaderCell>Date</asp:TableHeaderCell>
                        <asp:TableHeaderCell>Location</asp:TableHeaderCell>
                        <asp:TableHeaderCell>Performer</asp:TableHeaderCell>
                        <asp:TableHeaderCell>Nature Of Event</asp:TableHeaderCell>
                        <asp:TableHeaderCell># of Evaluators</asp:TableHeaderCell>
                    </asp:TableHeaderRow>
                    <asp:TableRow>
                    </asp:TableRow>
                </asp:Table>
            </div>
        </div>
    </div>


</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" Runat="Server">
</asp:Content>

