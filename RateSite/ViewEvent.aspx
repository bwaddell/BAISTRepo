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


    <div class="col-lg border">



        <label>HighChart Graph of data</label>

        <div>
            <asp:Literal ID="ltrChart" runat="server"></asp:Literal>
            <%--High chart literal, this is where the chart will be placed--%>
        </div>
        <div>
            <asp:Button ID="Export" runat="server" Text="Export Event Data" OnClick="Export_Click" />
        </div>
    </div>


</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" runat="Server">
</asp:Content>

