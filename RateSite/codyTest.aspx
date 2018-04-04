<%@ Page Language="C#" AutoEventWireup="true" CodeFile="codyTest.aspx.cs"
    Inherits="codyTest" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
        <script src="Scripts/jquery-3.3.1.min.js"></script>
    <script src="Scripts/Highcharts-4.0.1/js/highcharts.js"></script>



    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>

            <%--<asp:ScriptManager ID="ScriptManagerForGraph" runat="server"></asp:ScriptManager>--%>

            <button onclick="myFunc()">myFunc</button>


                    <label>Highcharts Chart data</label>

            <div id="myChart">
                <asp:Literal ID="ltrChart" runat="server"></asp:Literal>
                <%--High chart literal, this is where the chart will be placed--%>
            </div>
            <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>




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


            </script>




        </div>
    </form>
</body>
</html>
