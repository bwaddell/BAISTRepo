<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="HomePage.aspx.cs" Inherits="HomePage" Theme="ContinUI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainBody" Runat="Server">
    <div class="row">
        <div class="btn">
            <asp:Button ID="CreateButton" runat="server" Text="Create Event" OnClick="CreateButton_Click" />
        </div>
    </div>
    <div class="row">
        <div>
            <asp:TextBox ID="tbEventKey" runat="server" MaxLength="4"  placeholder="enter the event key"></asp:TextBox>
        </div>
    </div>
    <div class="row">
        <div class="btn">
            <asp:Button ID="JoinButton" runat="server" Text="Join Event" OnClick="JoinButton_Click" />
        </div>
    </div>
    <div class="row">
        <div>
            <asp:label id="statuslbl" runat="server" text=""></asp:label>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" Runat="Server">
</asp:Content>

