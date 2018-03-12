<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainBody" runat="Server">
    <div class="container-fluid">
        
        <div class="row">
            <label>Default page</label>
        </div>

        <div class="row">
            <a href="HomePage.aspx">HomePage.aspx</a>
        </div>

        <div class="row">
            <a href="CreateEvent.aspx">CreateEvent.aspx</a>
        </div>

        <div class="row">
            <a href="AnalyzeEvent.aspx">AnalyzeEvent.aspx</a>
        </div>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" runat="Server">
</asp:Content>

