<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LiveChart.aspx.cs"
    Inherits="LiveChart" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>HighCharts</title>

    <script src="Scripts/jquery-3.3.1.min.js"></script>
    <script src="Scripts/Highcharts-4.0.1/js/highcharts.js"></script>


</head>
<body>
    <form id="form1" runat="server">
        <div>

            <label>Highcharts Chart data</label>
            <asp:TextBox ID="evntID" runat="server">aaaa</asp:TextBox>

            <button onclick="myFunc()">myFunc</button>
            <%--<asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>--%>
              <button id="myButton">Get EVNT Lists</button> 


            <div id="myChart">
                <asp:Literal ID="ltrChart" runat="server"></asp:Literal>
                <%--High chart literal, this is where the chart will be placed--%>
            </div>

  <div id="contentHolder"></div>






        </div>
    </form>
</body>

</html>
<script type="text/javascript">

    function myFunc() {
        var series = chart.series[0],
            shift = series.data.length > 40, // shift if the series is longer than 20
            point = [Date.parse('03/28/2018 16:00:00'), 3];
        // add the point
        //chart.series[0].clear();
        chart.series[0].addPoint(point, true, shift);
        //chart.series[0].addPoint([Date.parse('03/28/2018 16:00:00'), 1], true);
    }


    $("#myButton").on("click", function (e) {
        e.preventDefault();
        var aData = [];
        aData[0] = $("#evntID").val();
        $("#contentHolder").empty();
        var jsonData = JSON.stringify({ aData: aData });
        $.ajax({
            type: "POST",
            //getListOfCars is my webmethod   
            url: "EventWebService.asmx/GetEventSeries",
            data: jsonData,
            contentType: "application/json; charset=utf-8",
            dataType: "json", // dataType is json format
            success: OnSuccess,
            error: OnErrorCall
        });

        function OnSuccess(response) {
            console.log(response.d)
        }
        function OnErrorCall(response) { console.log(error); }
    });

</script>
