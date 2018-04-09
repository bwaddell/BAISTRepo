<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ViewEvent.aspx.cs" Inherits="ViewEvent" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">


    <%--highchart scripts--%>
    <script src="Scripts/jquery-3.3.1.min.js"></script>
    <script src="Scripts/Highcharts-4.0.1/js/highcharts-all.js"></script>
    <%--<script src="Scripts/Highcharts-4.0.1/js/highcharts.js"></script>--%>
    <%--<script src="Scripts/Highcharts-4.0.1/js/modules/exporting.js"></script>--%>
<%--    <script src="Scripts/Highcharts-4.0.1/js/modules/data.js"></script>
    <script src="Scripts/Highcharts-4.0.1/js/modules/canvas-tools.js"></script>--%>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainBody" runat="Server">

    <asp:ScriptManager ID="ConScriptManager" runat="server"></asp:ScriptManager>
    <br />
    <br />
    <br />
    <label>Event ID:</label>
    <asp:TextBox ID="tbEventID" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
    <label>Performer:</label>
    <asp:TextBox ID="tbPerformer" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
    <label>Nature of Event:</label>
    <asp:TextBox ID="tbDesc" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
    <label>Location:</label>
    <asp:TextBox ID="tbLocation" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
    <label>Date:</label>
    <asp:TextBox ID="tbDate" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>

    <%--<label>Evaluator ID:</label>
    <asp:TextBox ID="tbEvaluatorID" runat="server" CssClass="form-control">1</asp:TextBox>--%>

    <asp:Label ID="lbStartTime" runat="server" Text="StartTime"></asp:Label>

    <div class="row">
        <asp:Button ID="ButtonStart" runat="server" Text="Begin Event" OnClick="ButtonStart_Click"/>
        <asp:Button ID="ButtonEnd" runat="server" Text="End Event" Enabled="false" OnClick="ButtonEnd_Click"/>
    </div>

    <div class="row ">
        <asp:Button ID="btnTable" runat="server" Text="Update Table"
            OnClick="btnTable_Click" />

        <asp:Button ID="btnChart" runat="server" Text="Update Chart"
            OnClick="btnChart_Click" />
    </div>

    <div class="row">
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
    </div>




    <%-- tables --%>

    <div class="col-lg border">

        <label>HighChart Graph of data</label>

        <div class="row">

            
            <asp:Literal ID="ltrChart" runat="server"></asp:Literal>
            <%--High chart literal, this is where the chart will be placed--%>
        </div>

        
        
    </div>
    <div class="col-lg border">
        <div class="row">
            <asp:Literal ID="mathChart" runat="server"></asp:Literal>
        </div>

    </div>
    <div class="row">
            <asp:Button ID="Export" runat="server" Text="Export Event Data" OnClick="Export_Click" />
        </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" runat="Server">
</asp:Content>

