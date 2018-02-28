<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="EvaluateEvent.aspx.cs" Inherits="EvaluateEvent" Theme="ContinUI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainBody" runat="Server">

    <div class="row">
        <div class="btnClass">
            <asp:ImageButton ID="ButtonUp" class="btn btn-block img-responsive btnImage" ImageUrl="Images/arrowup.png" runat="server" OnClick="ButtonUp_Click" />
        </div>
    </div>

    <%--wrap in UpdatePanel?--%>


    <div class="row">
        <div class="RatingClass">
            <asp:Label ID="LabelRating" runat="server" Text="5"></asp:Label>
        </div>
    </div>

    <div class="row">
        <div class="btnClass">
            <asp:ImageButton ID="ButtonDown" class="btn btn-block img-responsive btnImage" ImageUrl="Images/arrowdown.png" runat="server" OnClick="ButtonDown_Click" />
        </div>
    </div>



</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" runat="Server">

</asp:Content>

