using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using System.Web.UI.DataVisualization.Charting;
using DotNet.Highcharts;
using DotNet.Highcharts.Options;
using DotNet.Highcharts.Helpers;
using DotNet.Highcharts.Enums;


public partial class AnalyzeEvent : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lbStartTime.Text = "<br /> Page Loaded at: " + DateTime.Now.ToLocalTime().ToString();

            //tbEventID.Text = ((Event)Session["Event"]).EventID;
            //tbEventID.Text = "AAAA";

            ////ScriptManager.GetCurrent(this).RegisterPostBackControl(TimerForGraphRefresh);
            //Event ActiveEvent = new Event();
            //ActiveEvent.EventID = "AAAA";

            //Highcharts chart = createChart(ActiveEvent);
            //chart.SetSeries(updateChart(ActiveEvent).ToArray());
            //ltrChart.Text = chart.ToHtmlString();

            Event ActiveEvent = new Event();

            //get Event ID from....?
            ActiveEvent.EventID = "AAAA";

            //build and display chart
            CSS Director = new CSS();
            ActiveEvent = Director.GetEvent(ActiveEvent);
            DrawChart(ActiveEvent);
        }

    }



    protected void btnTable_Click(object sender, EventArgs e)
    {
        lbUpdateTime.Text = "Update Time: " + DateTime.Now.ToLocalTime().ToString();

        CSS RequestDirector = new CSS();

        List<Evaluation> currentEvals = new List<Evaluation>();

        Event test = new Event();
        test.EventID = "AAAA";

        //currentEvals = RequestDirector.GetCurrentEventData((Event)Session["Event"]);
        currentEvals = RequestDirector.GetCurrentEventData(test);


        foreach (Evaluation ev in currentEvals)
        {
            TableRow tRow = new TableRow();
            TableCell tCell = new TableCell();

            tCell.Text = ev.EvaluatorID.ToString();
            tRow.Cells.Add(tCell);

            tCell = new TableCell();
            tCell.Text = ev.Rating.ToString();
            tRow.Cells.Add(tCell);

            tCell = new TableCell();
            tCell.Text = ev.TimeStamp.ToString();
            tRow.Cells.Add(tCell);

            Table1.Rows.Add(tRow);
        }

        Ratinglbl.Text = currentEvals.Average(x => (double)x.Rating).ToString("#.##");



        lbChartUpdateTime.Text = "Update Time: " + DateTime.Now.ToLocalTime().ToString();



    }



    protected void btnChart_Click(object sender, EventArgs e)
    {
        lbChartUpdateTime.Text = "Update Time: " + DateTime.Now.ToLocalTime().ToString();

        CSS Director = new CSS();
        Event ActiveEvent = new Event();

        //build and display chart
        ActiveEvent.EventID = "AAAA";
        ActiveEvent = Director.GetEvent(ActiveEvent);
        DrawChart(ActiveEvent);

    }





    private void DrawChart(Event ActiveEvent)
    {
        List<Series> liOfSeries = new List<Series>();
        List<object> points = new List<object>();
        Random rand = new Random();

        foreach (Evaluator evalu in ActiveEvent.Evaluators)
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
        chart.SetSeries(liOfSeries.ToArray());


        //write the chart to the div(literal) on web page
        //the rest is automatic
        ltrChart.Text = chart.ToHtmlString();
    }


}