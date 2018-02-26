<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AnalyzeEvent.aspx.cs" Inherits="AnalyzeEvent" Theme="ContinUI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainBody" Runat="Server">


    <asp:UpdatePanel ID="UpdatePanel1" runat="server" > <%--attach to timer--%>

        <%--this is where the graph and stuff goes--%>





    </asp:UpdatePanel>
    <asp:Timer ID="Timer1" runat="server" Interval="1000" ></asp:Timer>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" Runat="Server">
</asp:Content>

