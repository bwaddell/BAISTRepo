<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AnalyzeEvent.aspx.cs" Inherits="AnalyzeEvent" Theme="ContinUI" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <!-- Latest compiled and minified plotly.js JavaScript -->
    <script src="https://cdn.plot.ly/plotly-latest.min.js"></script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainBody" runat="Server">


    <label>Event ID:</label>
    <asp:TextBox ID="tbEventID" runat="server" CssClass="form-control">1</asp:TextBox>

    <label>Evaluator ID:</label>
    <asp:TextBox ID="tbEvaluatorID" runat="server" CssClass="form-control">1</asp:TextBox>

    <asp:Label ID="lbStartTime" runat="server" Text="StartTime"></asp:Label>

    <div class="border row">
        <div class="col-lg">
            <asp:UpdatePanel ID="UpdatePanelTable" runat="server" UpdateMode="Conditional">
                <%--INSIDE PANEL--%>
                <%--this is where the graph and stuff goes--%>
                <ContentTemplate>


                    <label>Current Average Rating:</label>
                    <asp:Label ID="Ratinglbl" runat="server" Text="0"></asp:Label>
                    <br />
                    <asp:Table ID="Table1" runat="server" CssClass="table">
                        <asp:TableHeaderRow>
                            <asp:TableHeaderCell>EvaluatorID</asp:TableHeaderCell>
                            <asp:TableHeaderCell>Rating</asp:TableHeaderCell>
                            <asp:TableHeaderCell>TimeStamp</asp:TableHeaderCell>
                        </asp:TableHeaderRow>
                        <asp:TableRow>
                            <%--Dynamic table Data will be inserted here--%>
                        </asp:TableRow>
                    </asp:Table>
                    <asp:Label ID="lbUpdateTime" runat="server" Text="Update Time: "></asp:Label>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnTable" />
                    <asp:AsyncPostBackTrigger ControlID="TimerForTableRefresh" 
                        EventName="Tick" />
                </Triggers>
            </asp:UpdatePanel>
        </div>


        <div class="col-lg">
            <asp:UpdatePanel ID="UpdatePanelGraph" runat="server" UpdateMode="Conditional">
                <ContentTemplate>


                    <label>My Graph of data</label>
                    <div id="RatingGraph">
                        <!-- Plotly Graph will be drawn inside this DIV -->
                    </div>

                    <asp:Label ID="lbUpdateTimeInGraph" runat="server" Text="Update Time: "></asp:Label>

                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnGraph" />
<%--                    <asp:AsyncPostBackTrigger ControlID="TimerForGraphRefresh"
                        EventName="Tick" />--%>
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>


    <div class="row ">
        <asp:Button ID="btnTable" runat="server" Text="Update Table"
            OnClick="btnTable_Click" />
        <asp:Button ID="btnGraph" runat="server" Text="Update Graph"
            OnClick="btnGraph_Click" />
    </div>

    <asp:Timer ID="TimerForTableRefresh" runat="server"
        Interval="2000" OnTick="btnTable_Click">
    </asp:Timer>
<%--    <asp:Timer ID="TimerForGraphRefresh" runat="server"
        Interval="10000" OnTick="btnGraph_Click">
    </asp:Timer>--%>
</asp:Content>


<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" runat="Server">
    <script>
        var trace1 = {
            x: [1, 2, 3, 4, 5],
            y: [10, 15, 13, 17, 22],
            mode: 'markers'
        };

        var trace2 = {
            x: [2, 3, 4, 5, 6],
            y: [16, 5, 11, 9, 20],
            mode: 'lines'
        };

        var trace3 = {
            x: [1, 2, 3, 4, 5],
            y: [12, 9, 15, 12, 34],
            mode: 'lines+markers'
        };

        var data = [trace1, trace2, trace3];

        var layout = {
            title: 'Line and Scatter Plot'
        };

        Plotly.newPlot('RatingTable', data, layout);

    </script>
</asp:Content>

