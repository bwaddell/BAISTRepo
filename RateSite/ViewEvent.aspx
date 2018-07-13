﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ViewEvent.aspx.cs" Inherits="ViewEvent" Theme="ContinUI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">


    <%--highchart scripts--%>
    <script src="Scripts/jquery-3.3.1.min.js"></script>
    <script src="Scripts/Highcharts-4.0.1/js/highcharts-all.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainBody" runat="Server">

    <asp:ScriptManager ID="ConScriptManager" runat="server"></asp:ScriptManager>
    <h2>View Event</h2>

    <div class="form-group row">
        <label class="col-md-2 col-form-label">Event Key:</label>
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
    <asp:UpdatePanel ID="upTable" runat="server" UpdateMode="Conditional">
        <%--INSIDE PANEL--%>
        <ContentTemplate>
            <div class="row">
                <div class="col-md-4">
                    <div class="form-group row">
                        <label class="col-md-5 col-form-label">Date:</label>
                        <div class="col-md-7">
                            <asp:TextBox ID="tbDate" runat="server" CssClass="form-control"
                                ReadOnly="true"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group row">
                        <label class="col-md-5 col-form-label">Start Time:</label>
                        <div class="col-md-7">
                            <asp:TextBox ID="tbStart" runat="server" CssClass="form-control"
                                ReadOnly="true"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group row">
                        <label class="col-md-5 col-form-label">End Time:</label>
                        <div class="col-md-7">
                            <asp:TextBox ID="tbEnd" runat="server" CssClass="form-control"
                                ReadOnly="true"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group row">
                        <div class="col-md-6">
                            <asp:Button ID="ButtonStart" CssClass="btn btn-default btn-block" runat="server"
                                Text="Begin Event" OnClick="ButtonStart_Click" />
                        </div>
                        <div class="col-md-6">
                            <asp:Button ID="ButtonEnd" CssClass="btn btn-default btn-block" runat="server"
                                Text="End Event" OnClick="ButtonEnd_Click" />
                        </div>
                    </div>
                    <div class="form-group row">
                        <div class="col-md-6">
                            <asp:Button ID="Export" runat="server" CssClass="btn btn-default btn-block"
                                Text="Export Event Data" OnClick="Export_Click" />
                        </div>
                        <div class="col-md-6">
                            <asp:Button ID="RepeatBtn" runat="server" CssClass="btn btn-default btn-block"
                                Text="Repeat this Event" OnClick="RepeatBtn_Click" />
                        </div>
                    </div>
                    <div class="form-group row">
                        <asp:Label ID="RatingTitle" CssClass="col-md-5 col-form-label" Font-Bold="true" runat="server" Text="Current Average Rating:"></asp:Label>
                        <asp:Label ID="Ratinglbl" CssClass="col-md-7 col-form-label" runat="server"
                            Text="0" Font-Size="XX-Large"></asp:Label>
                    </div>
                    <div class="form-group row">
                        <asp:Label ID="lbTotalEvals" CssClass="col-md-5 col-form-label" Font-Bold="true" runat="server" Text="Number of Evaluators:"></asp:Label>
                        <asp:Label ID="lbTotalEvalsNum" CssClass="col-md-7 col-form-label" runat="server"
                            Text="0" Font-Size="X-Large"></asp:Label>
                    </div>
                    <div class="form-group row">
                        <label class="col-md-5 col-form-label">Page Update Time:</label>
                        <asp:Label ID="lbUpdateTime" CssClass="col-md-7 col-form-label"
                            runat="server" Text=""></asp:Label>
                    </div>

                </div>


                <div class="col-md-8">
                    <div class="">
                        <asp:Panel ID="Panel1" runat="server" ScrollBars="Auto" Height="29em" 
                            CssClass="">

                            <asp:Table ID="Table1" runat="server" CssClass="table table-responsive">
                                <asp:TableHeaderRow CssClass="position-sticky">
                                    <asp:TableHeaderCell CssClass="">Evaluator</asp:TableHeaderCell>
                                    <asp:TableHeaderCell CssClass="">Time of First Rating</asp:TableHeaderCell>
                                    <asp:TableHeaderCell CssClass="">Time of Final Rating</asp:TableHeaderCell>
                                    <asp:TableHeaderCell CssClass="">Avg Rating</asp:TableHeaderCell>
                                </asp:TableHeaderRow>
                                <asp:TableRow>
                                    <%--Dynamic table Data will be inserted here--%>
                                </asp:TableRow>

                            </asp:Table>
                        </asp:Panel>

                    </div>
                </div>
            </div>





            <%-- Charts --%>
            <div class="row">
                <div class="col-lg">

                    <%--<h3>Graphs</h3>--%>
                    <div class="">
                        <asp:Literal ID="ltrChart" runat="server"  ></asp:Literal>
                        <%--High chart literal, this is where the chart will be placed--%>
                    </div>

                </div>
                <div class="col-lg">
                     <asp:Panel ID="PanelChartRadio" runat="server" Visible="false">
                         <asp:RadioButtonList ID="RBLmath" runat="server" AutoPostBack="true" OnSelectedIndexChanged="RBLmath_SelectedIndexChanged">
                             <asp:ListItem Value="Mean" Selected="True">Mean</asp:ListItem>
                             <asp:ListItem Value="Median">Median</asp:ListItem>
                             <asp:ListItem Value="Mode">Mode</asp:ListItem>
                         </asp:RadioButtonList>
                     </asp:Panel>
                </div>

                <div class="col-lg">

                    <div class="">
                        <asp:Literal ID="mathChart" runat="server"></asp:Literal>
                    </div>

                </div>
            </div>

        </ContentTemplate>
        <Triggers>
            <%--<asp:AsyncPostBackTrigger ControlID="btnTable" />--%>
            <asp:AsyncPostBackTrigger ControlID="TimerForTableRefresh"
                EventName="Tick" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:Timer ID="TimerForTableRefresh" runat="server"
        Interval="1000" OnTick="TimerForTableRefresh_Tick" Enabled="false">
    </asp:Timer>


</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" runat="Server">
</asp:Content>

