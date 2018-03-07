<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AnalyzeEvent.aspx.cs" Inherits="AnalyzeEvent" Theme="ContinUI" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainBody" runat="Server">

    <asp:timer id="TimerForGraphRefresh" runat="server" interval="1000" ontick="TimerForGraphRefresh_Tick"></asp:timer>

    <label>Event ID:</label>
    <asp:textbox id="tbEventID" runat="server" cssclass="form-control">1</asp:textbox>

    <label>Evaluator ID:</label>
    <asp:textbox id="tbEvaluatorID" runat="server" cssclass="form-control">1</asp:textbox>

    <asp:label id="lbStartTime" runat="server" text="StartTime"></asp:label>

    <div class="border">
        START of panel
    <asp:updatepanel id="UpdatePanelGraph" runat="server" updatemode="Conditional">
        <%--INSIDE PANEL--%>
        <%--this is where the graph and stuff goes--%>
        <ContentTemplate>

            <label>Current Average Rating:</label>
            <asp:Label ID="Ratinglbl" runat="server" Text="0"></asp:Label>
            <br/>

            <%--CHART--%>
            <%--<asp:Chart ID="Chart1" runat="server" Height="500px" Width="500px">
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
            </asp:Chart>--%>

            <asp:Label ID="lbUpdateTime" runat="server" Text="Update Time: "></asp:Label>

        </ContentTemplate>


        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="Button1" />
            <asp:AsyncPostBackTrigger ControlID="TimerForGraphRefresh" EventName="Tick" />
        </Triggers>
    </asp:updatepanel>
        END of panel
    </div>
    OUTSIDE panel

    <%--so this button can sit outside of the panel but its trigger is inside the panel^^--%>
    <asp:button id="Button1" runat="server" text="Button" onclick="Button1_Click" />



</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" runat="Server">
</asp:Content>

