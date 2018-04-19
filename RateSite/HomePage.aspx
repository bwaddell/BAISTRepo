<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master"
    AutoEventWireup="true" CodeFile="HomePage.aspx.cs" Inherits="HomePage" Theme="ContinUI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainBody" runat="Server">
    <div class="jumbotron">
        <h1>ContinUI</h1>
        <p>The Continuous Rating Site</p>
    </div>

    <div class="col-lg-4 col-md-8 col-lg-offset-4 col-md-offset-2 text-center">

        <div class="form-group row">
            <asp:TextBox ID="tbEventKey" class="form-control" runat="server" MaxLength="4"
                placeholder="enter the event key"></asp:TextBox>
        </div>
        <div class="form-group row">
            <asp:Button ID="JoinButton" class="btn btn-default btn-block"
                runat="server" Text="Join Existing Event" OnClick="JoinButton_Click" />
        <h4>Or</h4>
            <asp:Button ID="CreateButton" class="btn btn-default btn-block"
                runat="server" Text="Create New Event" OnClick="CreateButton_Click" />
        </div>
        <div class="row">
            <div class="homepageText">
                <asp:Label ID="statuslbl" class="homepageText" runat="server"
                    Text=""></asp:Label>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" runat="Server">
</asp:Content>

