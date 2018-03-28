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
       // List<Evaluation> EventData = new List<Evaluation>();

        Event theEvent = new Event();

        theEvent.EventID = "aaaa";

        //Get ALL event Data!!!

        theEvent = Director.GetEvent(theEvent);
        int numOfEvaluators = theEvent.Evaluators.Count;


        List<Series> liOfSeries = new List<Series>();


        List<object> points = new List<object>();
        Random rand = new Random();
        //rand.Next(999);


        foreach (Evaluator evalu in theEvent.Evaluators)
        {
            points.Clear();

            foreach (Evaluation evaluation in evalu.EvaluatorEvaluations)
            {
                points.Add(new
                {
                    X = evaluation.TimeStamp,
                    Y = evaluation.Rating
                });
            }

            //add the points to the series
            Series ser = new Series();
            ser.Name = String.Format("Evaluator ({0})", evalu.EvaluatorID);
            ser.Type = ChartTypes.Line;
            ser.Data = new Data(points.ToArray());
            ser.Color = System.Drawing.Color.
                FromArgb(150, rand.Next(256), rand.Next(256), rand.Next(256));

            liOfSeries.Add(ser);

        }





        //-----------------------------------------------------------------------------
        Highcharts chart = new Highcharts("chart");
                        //{
                        //    Type = ChartTypes.Spline,
                        //    BackgroundColor = new BackColorOrGradient(System.Drawing.Color.FromName("'#f1f2f7'")),
                        //    ZoomType = ZoomTypes.X
                        //};
                        //;
        

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
            BackgroundColor = new BackColorOrGradient(System.Drawing.Color.FromName("'#aaaaff'"))
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
        chart.SetSeries( liOfSeries.ToArray() );

        

        //write the chart to the div(literal) on web page
        //the rest is automatic
        ltrChart.Text = chart.ToHtmlString();






    }
}