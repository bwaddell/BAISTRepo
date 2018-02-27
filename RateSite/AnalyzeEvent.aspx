<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AnalyzeEvent.aspx.cs" Inherits="AnalyzeEvent" Theme="ContinUI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainBody" runat="Server">

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server"> INSIDE PANEL
        <%--this is where the graph and stuff goes--%>
        <ContentTemplate>
            <asp:Timer ID="Timer1" runat="server" Interval="5000" OnTick="Timer1_Tick"></asp:Timer>
            <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>

        </ContentTemplate>





        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="Button1" />
        </Triggers>
    </asp:UpdatePanel> END of panel

    <%--so this button can sit outside of the panel but its trigger is inside the panel^^--%>
            <asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click" />

    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" runat="Server">
</asp:Content>

