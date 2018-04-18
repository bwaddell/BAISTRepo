<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" 
    AutoEventWireup="true" CodeFile="HomePage.aspx.cs" Inherits="HomePage" Theme="ContinUI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainBody" Runat="Server">
    <div class="jumbotron">
        <h1>ContinUI</h1>
        <p>The Continuous Rating Site</p>
    </div>

    <div class="row">
        <div class="col-sm-4 col-sm-offset-4 text-center">
            <asp:Button ID="CreateButton" class="btn btn-primary btn-block" runat="server" Text="Create Event" OnClick="CreateButton_Click" />
        </div>
    </div>
    <br />
    <div class="row">
        <div class=" col-sm-4 col-sm-offset-4">
            <asp:TextBox ID="tbEventKey" class="" runat="server" MaxLength="4"  placeholder="enter the event key"></asp:TextBox>
        </div>
    </div>
    <br />
    <div class="row">
                <div class=" btn-block col-sm-4 col-sm-offset-4 text-center">

            <asp:Button ID="JoinButton" class="btn btn-success btn-block" runat="server" Text="Join Event" OnClick="JoinButton_Click" />
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

