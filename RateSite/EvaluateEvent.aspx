<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="EvaluateEvent.aspx.cs" Inherits="EvaluateEvent" Theme="ContinUI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no">

    <!-- Global Site Tag (gtag.js) - Google Analytics -->
    <%--replace 'GA_TRACKING_ID' with your tracking ID code--%>
    <script async src="https://www.googletagmanager.com/gtag/js?id=GA_TRACKING_ID"></script>
    <script>
        window.dataLayer = window.dataLayer || [];
        function gtag(){dataLayer.push(arguments);}
        gtag('js', new Date());       
        gtag('config', 'GA_TRACKING_ID');
    </script>
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
            <asp:AsyncPostBackTrigger ControlID="ButtonUp" />
            <asp:AsyncPostBackTrigger ControlID="ButtonDown" />
            <%--<asp:AsyncPostBackTrigger ControlID="TimerForNumRefresh" EventName="Tick" />--%>
        </Triggers>
    </asp:UpdatePanel>



    <div class="row">
        <div class="btnClass">
            <asp:ImageButton ID="ButtonDown" class="btn btn-block img-responsive btnImage" ImageUrl="Images/arrowdown.png" runat="server" OnClick="ButtonDown_Click" />
        </div>
    </div>

    <div class="modal fade" tabindex="-1" role="dialog" id="myModal">
        <div class="modal-dialog modal-dialog-centered" role="document">
            <div class="modal-content">
                <asp:Label ID="Issue" runat="server" Text="Error" CssClass="text-center"></asp:Label>
            </div>
            <div class="modal-body">
                 <asp:Label ID="Status" runat="server" Text="Event is currently not running." CssClass="center-block"></asp:Label>
            </div>
        </div>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" runat="Server">
</asp:Content>

