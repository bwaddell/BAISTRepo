<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="EvaluationSlider.aspx.cs" Inherits="EvaluationSlider" Theme="ContinUI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <%--<meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no">--%>

    <!-- Global Site Tag (gtag.js) - Google Analytics -->
    <%--replace 'GA_TRACKING_ID' with your tracking ID code--%>
    <%--<script async src="https://www.googletagmanager.com/gtag/js?id=UA-8088578-2"></script>
    <script>
        window.dataLayer = window.dataLayer || [];
        function gtag() { dataLayer.push(arguments); }
        gtag('js', new Date());
        gtag('config', 'UA-8088578-2');
    </script>--%>

    
    <script type="text/javascript">
            $(document).ready(function() {
                $("#height").val($(window).height());

            });
    </script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainBody" runat="Server">
    <asp:ScriptManager ID="ConScriptManager" runat="server"></asp:ScriptManager>
    <asp:HiddenField ID="height" Value="" runat="server" />
    <div class="row form-group">
        <div class="col-lg">
            <asp:Button ID="LeaveBtn" runat="server" Text="Leave Event" CssClass="btn btn-default form-control" OnClick="LeaveBtn_Click" OnClientClick="return confirm('Are you sure you want to leave?');" />
        </div>
    </div>
    <div class="row form-group">
                <div class="col-lg">
                      <asp:Label ID="heightlbl" Text="nope" runat="server"></asp:Label>
                </div>
        </div>

    <asp:UpdatePanel ID="UpdatePanelGraph" runat="server" UpdateMode="Conditional">
        <%--INSIDE PANEL--%>
        <%--this is where the graph and stuff goes--%>
        <ContentTemplate>
            <div class="row form-group">
                <div class="col-lg">
                    <asp:Label ID="poslbl" Text="" runat="server"></asp:Label>
                    <hr />
                  
                </div>
            </div>
        </ContentTemplate>

        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="TimerVoteChange" EventName="Tick" />
        </Triggers>
    </asp:UpdatePanel>

    <asp:Timer ID="TimerVoteChange" OnTick="TimerVoteChange_Tick" Interval="500" Enabled="false" runat="server"></asp:Timer>
    <asp:Timer ID="TimerCheckEventStatus" OnTick="TimerCheckEventStatus_Tick" Interval="5000" Enabled="false" runat="server"></asp:Timer>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" runat="Server">
</asp:Content>

