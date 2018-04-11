<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="EvaluateEvent.aspx.cs" Inherits="EvaluateEvent" Theme="ContinUI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainBody" runat="Server">
    <asp:ScriptManager ID="ConScriptManager" runat="server"></asp:ScriptManager>
    <div class="row">
        <div class="btnClass">
            <asp:ImageButton ID="ButtonUp" class="btn btn-block img-responsive btnImage" ImageUrl="Images/arrowup.png" runat="server" OnClick="ButtonUp_Click" />
        </div>
    </div>

    <%--wrap in UpdatePanel?--%>

    <asp:UpdatePanel ID="UpdatePanelGraph" runat="server" UpdateMode="Conditional">
        <%--INSIDE PANEL--%>
        <%--this is where the graph and stuff goes--%>
        <ContentTemplate>
            <div class="row RatingClass">
                <asp:Label ID="LabelRating" class="ratingLabel" runat="server" Text="5"></asp:Label>
            </div>
        </ContentTemplate>

        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="ButtonUp"/>
            <asp:AsyncPostBackTrigger ControlID="ButtonDown" />
            <%--<asp:AsyncPostBackTrigger ControlID="TimerForNumRefresh" EventName="Tick" />--%>
        </Triggers>
    </asp:UpdatePanel>



    <div class="row">
        <div class="btnClass">
            <asp:ImageButton ID="ButtonDown" class="btn btn-block img-responsive btnImage" ImageUrl="Images/arrowdown.png" runat="server" OnClick="ButtonDown_Click" />
        </div>
    </div>



</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" runat="Server">
</asp:Content>

