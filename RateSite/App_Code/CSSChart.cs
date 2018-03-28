using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DotNet.Highcharts;
using DotNet.Highcharts.Options;
using DotNet.Highcharts.Helpers;
using DotNet.Highcharts.Enums;

/// <summary>
/// Summary description for CSSChart
/// </summary>
public class CSSChart
{
    private Random rand;

    public CSSChart()
    {
        //
        // TODO: Add constructor logic here
        //
        rand = new Random();
    }



    public Highcharts MakeChart(Event theEvent)
    {
        List<Series> liOfSeries = new List<Series>();
        List<object> points = new List<object>();

        //foreach evaluator in event foreach evaluation in evaluator add point. [TimeStamp,Rating]
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
            ser.Name = String.Format("{1} ({0})", evalu.EvaluatorID, evalu.Name);
            //ser.Type = ChartTypes.Spline;
            ser.Data = new Data(points.ToArray());
            ser.Color = System.Drawing.Color.
                FromArgb(150, rand.Next(256), rand.Next(256), rand.Next(256));
            liOfSeries.Add(ser);
        }



        //-----------------------------------------------------------------------------
        Highcharts chart = makeChartInfo(liOfSeries, ChartTypes.Scatter);

        return chart;
    }




    private Highcharts makeChartInfo(List<Series> liOfSeries, ChartTypes chartType)
    {
        Highcharts chart = new Highcharts("chart").InitChart(new Chart
        {
            Type = chartType,
            BackgroundColor = new BackColorOrGradient(System.Drawing.Color.FromName("'#f1f2f7'")),
            ZoomType = ZoomTypes.X,
            ClassName = "char"
        });
        chart.SetTitle(new Title
        {
            Text = "Evaluation Data"
        });
        chart.SetOptions(new GlobalOptions
        {
            Global = new Global { UseUTC = false }
        });
        chart.SetPlotOptions(new PlotOptions
        {
            //Column = new PlotOptionsColumn { Animation = new Animation(false) },
            //Line = new PlotOptionsLine { Animation = new Animation(false) }

        });
        chart.SetExporting(new Exporting
        {
            Enabled = true
        });
        chart.SetLegend(new Legend
        {
            Enabled = true,
            BackgroundColor = new BackColorOrGradient(System.Drawing.Color.
                FromArgb(150, rand.Next(256), rand.Next(256), rand.Next(256)))
        });

        chart.SetNavigation(new Navigation
        {
            ButtonOptions = new NavigationButtonOptions { SymbolStroke = System.Drawing.Color.FromArgb(5, 40, 200) }

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
        chart.SetLoading(new Loading
        {
            //ShowDuration = 0,
            //HideDuration = 0          //idk if these did anything?
        });
        return chart;
    }
}