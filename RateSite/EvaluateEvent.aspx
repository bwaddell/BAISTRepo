<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="EvaluateEvent.aspx.cs" Inherits="EvaluateEvent" Theme="ContinUI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainBody" Runat="Server">

        <div class="btnClass">
            <asp:Button ID="ButtonUp" runat="server" Text="Up" OnClick="ButtonUp_Click"/>
        </div>


        <div class="RatingClass">
            <asp:Label ID="LabelRating" runat="server" Text="5"></asp:Label>
        </div>

        <div class="btnClass block">
            <asp:Button ID="ButtonDown" runat="server" Text="Down" OnClick="ButtonDown_Click" />
        </div>


    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" Runat="Server">
</asp:Content>

