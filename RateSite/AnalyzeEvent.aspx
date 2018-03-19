<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AnalyzeEvent.aspx.cs" Inherits="AnalyzeEvent" Theme="ContinUI" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainBody" runat="Server">

    <asp:Timer ID="TimerForGraphRefresh" runat="server" Interval="1000" OnTick="TimerForGraphRefresh_Tick"></asp:Timer>

    <label>Event ID:</label>
    <asp:TextBox ID="tbEventID" runat="server" CssClass="form-control">1</asp:TextBox>

    <label>Evaluator ID:</label>
    <asp:TextBox ID="tbEvaluatorID" runat="server" CssClass="form-control">1</asp:TextBox>

    <asp:Label ID="lbStartTime" runat="server" Text="StartTime"></asp:Label>

    <div class="border">

        <asp:UpdatePanel ID="UpdatePanelGraph" runat="server" UpdateMode="Conditional">
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
                                    <%--Dynamic Data will be inserted here--%>
                                </asp:TableRow>

                            </asp:Table>


                <asp:Label ID="lbUpdateTime" runat="server" Text="Update Time: "></asp:Label>

            
            
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="Button1" />
                <asp:AsyncPostBackTrigger ControlID="TimerForGraphRefresh" EventName="Tick" />
            </Triggers>
        </asp:UpdatePanel>

    </div>


    <%--so this button can sit outside of the panel but its trigger is inside the panel^^--%>
    <asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click" />



</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" runat="Server">
</asp:Content>

