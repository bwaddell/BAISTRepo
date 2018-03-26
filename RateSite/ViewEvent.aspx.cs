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
        List<Evaluation> EventData = new List<Evaluation>();

        Event theEvent = new Event();

        theEvent.EventID = "ABCD";

        //Get ALL event Data!!!
        theEvent = Director.GetEvent(theEvent);

        
        //EventData = Director.GetEventData("abcd");


        //List<Evaluator> evaluators = new List<Evaluator>(); //list of evaluators for the event
        //List<Evaluation> evaluations = new List<Evaluation>(); //list of evaluations for each event
        List<object> points = new List<object>();

        List<object> evaluatorEvaluations = new List<object>();


        for (int i = 0; i < EventData.Count; i++)
        {
            Evaluator tempEval = new Evaluator();

            //if (evaluators.Contains( )      //check if the list of evaluators contains this one

            points.Add(new
            {
                X = EventData[i].TimeStamp,
                Y = EventData[i].Rating
            });

        }



        //one H line on graph
        Series mySeries = new Series
        {

            Name = "Evaluator",
            Type = ChartTypes.Line,
            Data = new Data(points.ToArray()),
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
            Text = "Evaluation Data"
        });
        chart.SetOptions(new GlobalOptions
        {
            Global = new Global { UseUTC = false }
        });
        chart.SetLegend(new Legend
        {
            Enabled = true,
            BackgroundColor = new BackColorOrGradient(System.Drawing.Color.FromName("'#ffffff'"))
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
                Minute = "%l%M<br>%p"
            },
            //{
                
            //    Month = "%e. %b",
            //    Year = "%b"
            //},
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

        

        //write the chart to the div(literal) on web page
        //the rest is automatic
        ltrChart.Text = chart.ToHtmlString();






    }
}