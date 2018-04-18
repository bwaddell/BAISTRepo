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
        List<List<object>> points = new List<List<object>>();

        DateTime eventStart = theEvent.EventStart.ToUniversalTime();
        DateTime eventEnd = theEvent.EventEnd.ToUniversalTime();

        double diffInSeconds = (eventEnd - eventStart).TotalSeconds;

        int secsBetweenPoints;
        double numOfPoints = 50.0;

        if ((int)(diffInSeconds / numOfPoints) > 1)
            secsBetweenPoints = (int)(diffInSeconds / numOfPoints);
        else
            secsBetweenPoints = 1;

        List<int> ratings;
        List<object> pts;
        Series ser;
        Evaluator e = new Evaluator();
        Evaluation ev = new Evaluation();

        for (int j = 0; j < theEvent.Evaluators.Count; j++)
        {
            ser = new Series();
            pts = new List<object>();         

            liOfSeries.Add(ser);
            points.Add(pts);
        }

        for (DateTime i = eventStart; i < eventEnd; i = i.AddSeconds(secsBetweenPoints))
        {
            for (int j = 0; j < theEvent.Evaluators.Count; j++)
            {
                e = theEvent.Evaluators[j];

                if (e.EvaluatorEvaluations[0].TimeStamp <= i)
                {
                    int evalCount = 0;
                    while (evalCount < e.EvaluatorEvaluations.Count)
                    {
                        if (e.EvaluatorEvaluations[evalCount].TimeStamp <= i)
                        {
                            ev = e.EvaluatorEvaluations[evalCount];
                            evalCount++;
                        }
                        else
                        {
                            break;
                        }                
                    }
                    double timestamp = (i - eventStart).TotalSeconds;

                    points[j].Add(new
                    {
                        X = timestamp,
                        Y = ev.Rating
                    });
                }
            }     

        }

        for (int k = 0; k < liOfSeries.Count; k++)
        {
            liOfSeries[k].Data = new Data(points[k].ToArray());
            liOfSeries[k].Color = System.Drawing.Color.
                    FromArgb(150, rand.Next(150, 256), rand.Next(25), rand.Next(25));
            liOfSeries[k].Name = String.Format("{1} ({0})", theEvent.Evaluators[k].EvaluatorID, theEvent.Evaluators[k].Name);
        }


        ///////////////////////////////////////////////////////////////////////////////////
        //foreach evaluator in event foreach evaluation in evaluator add point. [TimeStamp,Rating]
        //foreach (Evaluator evalu in theEvent.Evaluators)
        //{
        //    points.Clear();
        //    foreach (Evaluation evaluation in evalu.EvaluatorEvaluations)
        //    {
        //        points.Add(new
        //        {
        //            X = (evaluation.TimeStamp.ToUniversalTime() - theEvent.EventStart).TotalSeconds,
        //            //X = evaluation.TimeStamp.ToUniversalTime(),
        //            Y = evaluation.Rating
        //        });
        //    }
        //    //add the points to the series
        //    Series ser = new Series();
        //    ser.Name = String.Format("{1} ({0})", evalu.EvaluatorID, evalu.Name);
        //    //ser.Type = ChartTypes.Spline;
        //    ser.Data = new Data(points.ToArray());
        //    ser.Color = System.Drawing.Color.
        //        FromArgb(200, rand.Next(50, 200), rand.Next(50, 200), rand.Next(50, 200));
        //    liOfSeries.Add(ser);
        //}



        //-----------------------------------------------------------------------------
        Highcharts chart = makeChartInfo(theEvent, liOfSeries, ChartTypes.Spline);

        return chart;
    }


    


    private Highcharts makeChartInfo(Event theEvent, List<Series> liOfSeries, ChartTypes chartType)
    {
        DateTime eventStart = theEvent.EventStart.ToUniversalTime();
        DateTime eventEnd = theEvent.EventEnd.ToUniversalTime();

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
            BackgroundColor = new BackColorOrGradient(System.Drawing.Color.FromName("'#cccccc'"))
        });

        chart.SetNavigation(new Navigation
        {
            //ButtonOptions = new NavigationButtonOptions { SymbolStroke = System.Drawing.Color.FromArgb(5, 40, 200) }

        });
        chart.SetTooltip(new Tooltip
        {
            Shared = true,
            Shadow = true
        });
        chart.SetXAxis(new XAxis
        {
            Title = new XAxisTitle
            {
                Text = "Time Stamp"
            },
            Type = AxisTypes.Linear,
            //DateTimeLabelFormats = new DateTimeLabel
            //{
            //    Minute = "%l:%M %p"
            //},            
            Labels = new XAxisLabels
            {
                StaggerLines = 2
            }
            //Max = 
        });
        chart.SetYAxis(new YAxis
        {
            Title = new YAxisTitle
            {
                Text = "Rating"
            },
            Max = 10,
            Min = 0,
        });
        chart.SetSeries(liOfSeries.ToArray());
        chart.SetLoading(new Loading
        {
            //ShowDuration = 0,
            //HideDuration = 0          //idk if these did anything?
        });
        return chart;
    }

    public Highcharts MakeMathChart(Event theEvent)
    {
        List<Series> liOfSeries = new List<Series>();
        List<object> points = new List<object>();
        Random rand = new Random();

        Series serMean = new Series();
        serMean.Name = "Mean";
        List<object> meanPoints = new List<object>();

        Series serMode = new Series();
        serMode.Name = "Mode";
        List<object> modePoints = new List<object>();

        Series serMedian = new Series();
        serMedian.Name = "Median";
        List<object> medianPoints = new List<object>();

        List<Evaluation> allEvaluations = new List<Evaluation>();
        foreach (Evaluator e in theEvent.Evaluators)
        {
            allEvaluations.AddRange(e.EvaluatorEvaluations);
        }

        //get times of first and last rating
        //DateTime firstRating = allEvaluations.OrderBy(x => x.TimeStamp).FirstOrDefault().TimeStamp;
        //DateTime lastRating = allEvaluations.OrderByDescending(x => x.TimeStamp).FirstOrDefault().TimeStamp;

        DateTime eventStart = theEvent.EventStart.ToUniversalTime();
        DateTime eventEnd = theEvent.EventEnd.ToUniversalTime();

        double diffInSeconds = (eventEnd - eventStart).TotalSeconds;

        int secsBetweenPoints = (int)(diffInSeconds / 50.0);

        List<int> ratings;

        for (DateTime i = eventStart; i < eventEnd; i = i.AddSeconds(secsBetweenPoints))
        {
            ratings = new List<int>();
            ratings = getRatingsAtTimestamp(i, theEvent);

            if (ratings.Count > 0)
            {
                double timestamp = (i - eventStart).TotalSeconds;

                meanPoints.Add(new
                {
                    X = timestamp,
                    Y = ratings.Average(x => x)
                });

                modePoints.Add(new
                {
                    X = timestamp,
                    Y = ratings.GroupBy(v => v)
                            .OrderByDescending(g => g.Count())
                            .First()
                            .Key
                });

                medianPoints.Add(new
                {
                    X = timestamp,
                    Y = GetMedian(ratings)
                });
            }
        }

        serMean.Data = new Data(meanPoints.ToArray());
        serMode.Data = new Data(modePoints.ToArray());
        serMedian.Data = new Data(medianPoints.ToArray());

        serMean.Color = System.Drawing.Color.
                FromArgb(150, rand.Next(150,256), rand.Next(25), rand.Next(25));
        serMode.Color = System.Drawing.Color.
                FromArgb(150, rand.Next(25), rand.Next(150, 256), rand.Next(25));
        serMedian.Color = System.Drawing.Color.
                FromArgb(150, rand.Next(25), rand.Next(25), rand.Next(150, 256));

        liOfSeries.Add(serMean);
        liOfSeries.Add(serMode);
        liOfSeries.Add(serMedian);




        //-----------------------------------------------------------------------------
        Highcharts chart = new Highcharts("mChart").InitChart(new Chart
        {
            Type = ChartTypes.Spline,
            BackgroundColor = new BackColorOrGradient(System.Drawing.Color.FromName("'#f1f2f7'")),
            ZoomType = ZoomTypes.X,
            ClassName = "char"
        });
        chart.SetTitle(new Title
        {
            Text = "Calculated Data"
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
            BackgroundColor = new BackColorOrGradient(System.Drawing.Color.FromName("'#cccccc'"))
        });

        chart.SetNavigation(new Navigation
        {
            //ButtonOptions = new NavigationButtonOptions { SymbolStroke = System.Drawing.Color.FromArgb(5, 40, 200) }

        });
        chart.SetTooltip(new Tooltip
        {
            Shared = true,
            Shadow = true
        });
        chart.SetXAxis(new XAxis
        {
            Title = new XAxisTitle
            {
                Text = "Time Stamp"
            },
            Type = AxisTypes.Linear,
            //DateTimeLabelFormats = new DateTimeLabel
            //{
            //    Minute = "%l:%M %p"
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
            },
            Max = 10

        });
        chart.SetSeries(liOfSeries.ToArray());

        //chart.SetSeries(

        chart.SetLoading(new Loading
        {
            //ShowDuration = 0,
            //HideDuration = 0          //idk if these did anything?
        });
        return chart;
    }
    public List<int> getRatingsAtTimestamp(DateTime Timestamp, Event theEvent)
    {
        List<int> ratings = new List<int>();
        int count;

        foreach (Evaluator e in theEvent.Evaluators)
        {
            count = 0;
            if (e.EvaluatorEvaluations[count].TimeStamp < Timestamp)
            {
                while (count < e.EvaluatorEvaluations.Count - 1)
                {
                    if (e.EvaluatorEvaluations[count + 1].TimeStamp < Timestamp)
                    {
                        count++;
                    }
                    else
                    {
                        break;
                    }
                }
                ratings.Add(e.EvaluatorEvaluations[count].Rating);
            }
        }

        return ratings;
    }
    public double GetMedian(List<int> ints)
    {
        double median;

        ints.Sort();

        if (ints.Count % 2 != 0)
            median = ints[ints.Count / 2];
        else
            median = (((double)ints[ints.Count / 2 - 1] + (double)ints[ints.Count / 2]) / 2.0);


        return Math.Round(median);
    }
}