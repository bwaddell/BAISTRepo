using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DotNet.Highcharts;
using DotNet.Highcharts.Options;
using DotNet.Highcharts.Helpers;
using DotNet.Highcharts.Enums;

public partial class LiveChart : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //Label1.Text = "!IsPostBack";

            Event ActiveEvent = new Event();

            //get Event ID from....?
            ActiveEvent.EventID = "AAAA";

            //build and display chart
            CSS Director = new CSS();
            ActiveEvent = Director.GetEvent(ActiveEvent);
            //Highcharts chart = Director.CreateChart(ActiveEvent);
            //List<Series> listOfSeries = Director.ConvertEventToSeriesList(ActiveEvent);


            //ScriptManager.RegisterStartupScript(ltrChart, Page.GetType(), "chartKey", chart.ToHtmlString(), false);
            ScriptManager.RegisterClientScriptBlock(ltrChart, Page.GetType(), "chartKey", chart.ToHtmlString(), false);

            //ltrChart.Text = chart.ToHtmlString();
        }
        else
        {
            //Label1.Text = "IsPostBack";
        }
    }


    //private void DrawChart(Event ActiveEvent)
    //{

    //    Random rand = new Random();
    //    //ltrChart.Text = "";



    //    Highcharts chart = new Highcharts("liveChart");
    //    //{
    //    //    Type = ChartTypes.Spline,
    //    //    BackgroundColor = new BackColorOrGradient(System.Drawing.Color.FromName("'#f1f2f7'")),
    //    //    ZoomType = ZoomTypes.X
    //    //};
    //    //;

        

    //    //chart.AddJavascripFunction("ChartEventsLoad",
    //    //                              @"// set up the updating of the chart each second
    //    //                               var series = this.series[0];
    //    //                               setInterval(function() {
    //    //                                  var x = (new Date()).getTime(), // current time
    //    //                                     y = Math.random();
    //    //                                  series.addPoint([x, y], true, true);
    //    //                               }, 1000);");

    //    chart.SetTitle(new Title
    //    {
    //        Text = "Evaluation Data"
    //    });
    //    chart.SetOptions(new GlobalOptions
    //    {
    //        Global = new Global { UseUTC = false }
    //    });
    //    chart.SetLegend(new Legend
    //    {
    //        Enabled = true,
    //        BackgroundColor = new BackColorOrGradient(System.Drawing.Color.FromName("'#aaaaff'"))
    //    });

    //    chart.SetTooltip(new Tooltip
    //    {
    //        Shared = true,
    //        Shadow = true
    //    });
    //    chart.SetXAxis(new XAxis
    //    {
    //        Type = AxisTypes.Datetime,
    //        DateTimeLabelFormats = new DateTimeLabel
    //        {
    //            Minute = "%l%M<br>%p"
    //        },
    //        //{

    //        //    Month = "%e. %b",
    //        //    Year = "%b"
    //        //},
    //        Labels = new XAxisLabels
    //        {
    //            StaggerLines = 2
    //        }
    //    });
    //    chart.SetYAxis(new YAxis
    //    {
    //        Title = new YAxisTitle
    //        {
    //            Text = "Rating"
    //        }
    //    });
    //    chart.SetSeries(liOfSeries.ToArray());


    //    //write the chart to the div(literal) on web page
    //    //the rest is automatic
    //    ltrChart.Text = chart.ToHtmlString();
    //}
}