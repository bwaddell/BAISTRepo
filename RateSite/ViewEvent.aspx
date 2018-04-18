<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ViewEvent.aspx.cs" Inherits="ViewEvent" Theme="ContinUI" %>

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
    <%--    <div id="content" class="carousel slide" data-ride="carousel">
        <div class="carousel-inner">
            <div class="carousel-item active">
            </div>
        </div>
    </div>--%>
    <asp:ScriptManager ID="ConScriptManager" runat="server"></asp:ScriptManager>

    <asp:Panel ID="EventDetailsPanel" CssClass="row" runat="server">
        <div class="form-group row">
            <label class="col-md-2 col-form-label">Event ID:</label>
            <div class="col-md-4">
                <asp:TextBox ID="tbEventID" runat="server" CssClass="form-control"
                    ReadOnly="true"></asp:TextBox>
            </div>

            <label class="col-md-2 col-form-label">Performer:</label>
            <div class="col-md-4">
                <asp:TextBox ID="tbPerformer" runat="server" CssClass="form-control"
                    ReadOnly="true"></asp:TextBox>
            </div>
        </div>

        <div class="form-group row">
            <label class="col-md-2 col-form-label">Nature of Event:</label>
            <div class="col-md-10">
                <asp:TextBox ID="tbDesc" runat="server" CssClass="form-control"
                    ReadOnly="true"></asp:TextBox>
            </div>
        </div>

        <div class="form-group row">
            <label class="col-md-2 col-form-label">Location:</label>
            <div class="col-md-10">
                <asp:TextBox ID="tbLocation" runat="server" CssClass="form-control"
                    ReadOnly="true"></asp:TextBox>
            </div>
        </div>

        <div class="form-group row">
            <label class="col-md-2 col-form-label">Date:</label>
            <div class="col-md-10">
                <asp:TextBox ID="tbDate" runat="server" CssClass="form-control"
                    ReadOnly="true"></asp:TextBox>
            </div>
        </div>

        <div class="form-group row">
            <label class="col-md-2 col-form-label">Start Time:</label>
            <div class="col-md-3">
                <asp:TextBox ID="tbStart" runat="server" CssClass="form-control"
                    ReadOnly="true"></asp:TextBox>
            </div>
        </div>

        <div class="form-group row">
            <label class="col-md-2 col-form-label">End Time:</label>
            <div class="col-md-3">
                <asp:TextBox ID="tbEnd" runat="server" CssClass="form-control"
                    ReadOnly="true"></asp:TextBox>
            </div>
        </div>

    </asp:Panel>


    <%--<label>Evaluator ID:</label>
    <asp:TextBox ID="tbEvaluatorID" runat="server" CssClass="form-control">1</asp:TextBox>--%>
    <br />

    <div class="row">
        <asp:Button ID="ButtonStart" CssClass="startEnd" runat="server" Text="Begin Event" OnClick="ButtonStart_Click" />
        <asp:Button ID="ButtonEnd" CssClass="startEnd" runat="server" Text="End Event" Enabled="false" OnClick="ButtonEnd_Click" />
    </div>
    
    <br />

    <div class="row">
        <div class="col-lg">
            <asp:UpdatePanel ID="upTable" runat="server" UpdateMode="Conditional">
                <%--INSIDE PANEL--%>
                <ContentTemplate>

                    <div class="col-md-4">
                        <label>Current Average Rating:</label>
                        <asp:Label ID="Ratinglbl" CssClass="h1" 
                            runat="server" Text="0" ></asp:Label>
                    </div>
                    <div class="col-md-8">
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
                    </div>
                    <br />

                </ContentTemplate>
                <Triggers>
                    <%--<asp:AsyncPostBackTrigger ControlID="btnTable" />--%>
                    <asp:AsyncPostBackTrigger ControlID="TimerForTableRefresh"
                        EventName="Tick" />

                </Triggers>
            </asp:UpdatePanel>
            <asp:Timer ID="TimerForTableRefresh" runat="server"
                Interval="1000" OnTick="btnTable_Click" Enabled="false">
            </asp:Timer>
        </div>
    </div>




    <%-- tables --%>

    <div class="col-lg border">

        <h3>HighChart Graph of data</h3>

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

