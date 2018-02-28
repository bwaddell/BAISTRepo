<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AnalyzeEvent.aspx.cs" Inherits="AnalyzeEvent" Theme="ContinUI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainBody" runat="Server">

    <asp:ScriptManager ID="ScriptManagerForGraph" runat="server"></asp:ScriptManager>
    <asp:Timer ID="TimerForGraphRefresh" runat="server" Interval="10000" OnTick="TimerForGraphRefresh_Tick"></asp:Timer>

    <asp:UpdatePanel ID="UpdatePanelGraph" runat="server" UpdateMode="Conditional">
        <%--INSIDE PANEL--%>
        <%--this is where the graph and stuff goes--%>
        <ContentTemplate>

            <asp:Label ID="lbCurrentTime" runat="server" Text="Time"></asp:Label>
            <asp:Calendar ID="Calendar1" runat="server"></asp:Calendar>
        </ContentTemplate>





        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="Button1" />
            <asp:AsyncPostBackTrigger ControlID="TimerForGraphRefresh" EventName="Tick" />
        </Triggers>
    </asp:UpdatePanel>
    END of panel


    <asp:Calendar ID="Calendar2" runat="server"></asp:Calendar>
    <%--so this button can sit outside of the panel but its trigger is inside the panel^^--%>
    <asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click" />


</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" runat="Server">
</asp:Content>

