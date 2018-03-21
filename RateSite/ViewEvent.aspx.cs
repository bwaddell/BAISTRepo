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

public partial class ViewEvent : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {



        CSS Director = new CSS();
        List<Evaluation> currentEvals = new List<Evaluation>();

        Event test = new Event();
        test.EventID = "ABCD";

        //currentEvals = RequestDirector.GetCurrentEventData((Event)Session["Event"]);
        currentEvals = Director.GetCurrentEventData(test);

        //object[] oblist = currentEvals.Cast<object>().ToArray();



        Point[] oblist = new Point[currentEvals.Count];
        //object[,] datapoint;

        for (int i = 0; i < currentEvals.Count; i++)
        {
            Point myPoint = new Point()
            {
                X = currentEvals[i].TimeStamp.Minute,
                Y = currentEvals[i].Rating
            };


            //datapoint = new object[currentEvals[i].EvaluatorID, currentEvals[i].Rating];
            //oblist[i] = new object[i, i];

            oblist[i] = myPoint;
        }



        //one H line on graph
        Series mySeries = new Series
        {

            Name = "DateTime",
            Type = ChartTypes.Line,
            Data = new Data(oblist),
            Color = System.Drawing.Color.FromName("'#4CB7A5'")

        };

        


        Highcharts chart = new Highcharts("chart")
                        //{
                        //    Type = ChartTypes.Spline,
                        //    BackgroundColor = new BackColorOrGradient(System.Drawing.Color.FromName("'#f1f2f7'")),
                        //    ZoomType = ZoomTypes.X
                        //};
                        ;


        chart.SetTitle(new Title
        {
            Text = "myHighChart"
        });

        chart.SetLegend(new Legend
        {
            Enabled = true
            //BackgroundColor = new BackColorOrGradient(System.Drawing.Color.FromName("'#ffffff'"))
        });

        chart.SetTooltip(new Tooltip
        {
            Shared = true,
            Shadow = true
        });
        chart.SetXAxis(new XAxis
        {
            Type = AxisTypes.Datetime,
            DateTimeLabelFormats = new DateTimeLabel
            {

                Month = "%e. %b",
                Year = "%b"
            },
            Labels = new XAxisLabels
            {
                StaggerLines = 2
            }
        });
        chart.SetYAxis(new YAxis
        {
            Title = new YAxisTitle
            {
                Text = "Rating"
            }
        });
        chart.SetSeries( new[] { mySeries });

        //litChart.text = chart.ToHtmlString();
        ltrChart.Text = chart.ToHtmlString();






    }
}