<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="EvaluateEvent.aspx.cs" Inherits="EvaluateEvent" Theme="ContinUI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainBody" Runat="Server">

        <div class="row btnClass">
            <asp:ImageButton ID="ButtonUp" class="btn btnClass" ImageUrl="Images/arrowup.png" runat="server" OnClick="ButtonUp_Click" />
        </div>

    <%--wrap in UpdatePanel?--%>
        <div class="row RatingClass">
            <asp:Label ID="LabelRating" class="RatingClass" runat="server" Text="5"></asp:Label>
        </div>

        <div class="row btnClass">
            <asp:ImageButton ID="ButtonDown" class="btn btnClass" ImageUrl="Images/arrowdown.png" runat="server" OnClick="ButtonDown_Click" />
        </div>


    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" Runat="Server">
</asp:Content>

