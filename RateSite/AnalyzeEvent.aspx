<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AnalyzeEvent.aspx.cs" Inherits="AnalyzeEvent" Theme="ContinUI" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainBody" runat="Server">

    <asp:Timer ID="TimerForGraphRefresh" runat="server" Interval="10000" OnTick="TimerForGraphRefresh_Tick"></asp:Timer>
    <br />
    Event ID:
    <asp:TextBox ID="tbEventID" runat="server">1</asp:TextBox>
    <br />
    Evaluator ID:
    <asp:TextBox ID="tbEvaluatorID" runat="server">1</asp:TextBox>

    <asp:Label ID="lbStartTime" runat="server" Text="StartTime"></asp:Label>

    <div class="border">
        START of panel
    <asp:UpdatePanel ID="UpdatePanelGraph" runat="server" UpdateMode="Conditional">
        <%--INSIDE PANEL--%>
        <%--this is where the graph and stuff goes--%>
        <ContentTemplate>

            <%--CHART--%>
            <asp:Chart ID="Chart1" runat="server" Height="500px" Width="500px">
                <Titles>
                    <asp:Title ShadowOffset="3" Name="Things" />
                </Titles>
                <Legends>
                    <asp:Legend Alignment="Center" Docking="Bottom"
                        IsTextAutoFit="false" Name="Default" LegendStyle="Row" />
                </Legends>
                <Series>
                    <asp:Series Name="Series1"></asp:Series>
                </Series>
                <ChartAreas>
                    <asp:ChartArea Name="ChartArea1"></asp:ChartArea>
                </ChartAreas>
            </asp:Chart>

            <asp:Label ID="lbUpdateTime" runat="server" Text="Update Time: "></asp:Label>

        </ContentTemplate>


        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="Button1" />
            <asp:AsyncPostBackTrigger ControlID="TimerForGraphRefresh" EventName="Tick" />
        </Triggers>
    </asp:UpdatePanel>
        END of panel
    </div>
    OUTSIDE panel

    <%--so this button can sit outside of the panel but its trigger is inside the panel^^--%>
    <asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click" />



</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" runat="Server">
</asp:Content>

