<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AnalyzeEvent.aspx.cs" Inherits="AnalyzeEvent" Theme="ContinUI" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <%--highchart scripts--%>
    <script src="Scripts/jquery-3.3.1.min.js"></script>
    <script src="Scripts/Highcharts-4.0.1/js/highcharts.js"></script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainBody" runat="Server">
    <asp:ScriptManager ID="ConScriptManager" runat="server" EnablePageMethods="true"></asp:ScriptManager>


    <label>Event ID:</label>
    <asp:TextBox ID="tbEventID" runat="server" CssClass="form-control">1</asp:TextBox>

    <label>Evaluator ID:</label>
    <asp:TextBox ID="tbEvaluatorID" runat="server" CssClass="form-control">1</asp:TextBox>

    <asp:Label ID="lbStartTime" runat="server" Text="StartTime"></asp:Label>

    <div class="row ">
        <asp:Button ID="btnTable" runat="server" Text="Update Table"
            OnClick="btnTable_Click" />

        <asp:Button ID="btnChart" runat="server" Text="Update Chart"
            OnClick="btnChart_Click" />

        <asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click" />

    </div>




    <div class="border row">
        <div class="col-lg">
            <asp:UpdatePanel ID="upTable" runat="server" UpdateMode="Conditional">
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
                    <%--<asp:AsyncPostBackTrigger ControlID="TimerForTableRefresh"
                        EventName="Tick" />--%>
                </Triggers>
            </asp:UpdatePanel>
        </div>



        <div class="col-lg border">
            <asp:UpdatePanel ID="upChart" runat="server" UpdateMode="Conditional">
                <ContentTemplate>


                    <label>Highcharts Chart data</label>
                    <div>
                        <asp:Literal ID="ltrChart" runat="server"></asp:Literal>
                        <%--High chart literal, this is where the chart will be placed--%>
                    </div>


                    <asp:Label ID="lbChartUpdateTime" runat="server" Text="Update Time: "></asp:Label>
                    <button onclick="myFunc()">myFunc</button>

                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnChart" />
                    <%--<asp:AsyncPostBackTrigger ControlID="TimerForChartRefresh"/>--%>
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>





    <%--    <asp:Timer ID="TimerForTableRefresh" runat="server"
        Interval="1000" OnTick="btnTable_Click">
    </asp:Timer>--%>
    <%--<asp:Timer ID="TimerForChartRefresh" runat="server"
        Interval="2000" OnTick="btnChart_Click">
    </asp:Timer>--%>
</asp:Content>







<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" runat="Server">
    <script type="text/javascript">

        function myFunc() {
            var series = chart.series[0],
                shift = series.data.length > 40, // shift if the series is longer than 20
                point = [Date.parse('03/28/2018 17:00:00'), 3];
            // add the point
            //chart.series[0].clear();
            chart.series[0].addPoint(point, true, shift);
            //chart.series[0].addPoint([Date.parse('03/28/2018 16:00:00'), 1], true);
        }


    </script>

</asp:Content>

