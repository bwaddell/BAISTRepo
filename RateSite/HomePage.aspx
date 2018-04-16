<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="HomePage.aspx.cs" Inherits="HomePage" Theme="ContinUI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainBody" Runat="Server">
    <div class="jumbotron">
        <h1>ContinUI</h1>
        <p>The Continuous Rating Site</p>
    </div>
    <div class="row">
        <div class="btn homepageBtn">
            <asp:Button ID="CreateButton" class="homepageBtn" runat="server" Text="Create Event" OnClick="CreateButton_Click" />
        </div>
    </div>
    <br/>
    <div class="row">
        <div class="">
            <asp:TextBox ID="tbEventKey" class="homepageText" runat="server" MaxLength="4"  placeholder="enter the event key"></asp:TextBox>
        </div>
    </div>
    <div class="row">
        <div class="btn homepageBtn">
            <asp:Button ID="JoinButton" class="homepageBtn" runat="server" Text="Join Event" OnClick="JoinButton_Click" />
        </div>
    </div>
    <div class="row">
        <div class="homepageText">
            <asp:label id="statuslbl" class="homepageText" runat="server" text=""></asp:label>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" Runat="Server">
</asp:Content>

