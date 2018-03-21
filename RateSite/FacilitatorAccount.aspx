<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="FacilitatorAccount.aspx.cs" Inherits="FacilitatorAccount" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainBody" Runat="Server">
    <div class="row">
        <div>
            <asp:Label ID="Namelbl" runat="server" Text=""></asp:Label>
        </div>       
    </div>
    <div class="row">
        <div class="col-md-6 text-right">
            Organization:
        </div>
        <div class="col-md-6">
            <asp:TextBox ID="Orgtxt" runat="server"></asp:TextBox>
        </div>
        <div class="col-md-6 text-right">
            Location:
        </div>
        <div class="col-md-6">
            <asp:TextBox ID="Loctxt" runat="server"></asp:TextBox>
        </div>
        <div class="col-md-6 text-right">
            Email:
        </div>
        <div class="col-md-6">
            <asp:TextBox ID="Emailtxt" runat="server"></asp:TextBox>
        </div>
    </div>
    <div class="row">
        <div>
            <asp:ListBox ID="EventListBox" runat="server"></asp:ListBox>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" Runat="Server">
</asp:Content>

