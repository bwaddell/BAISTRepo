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

        //get start and end times for chart.  Convert to local time
        DateTime eventStart = theEvent.EventStart.ToLocalTime();
        DateTime eventEnd = theEvent.EventEnd.ToLocalTime();

        //get amount of seconds between points on chart.  have a minimum of one a second
        double diffInSeconds = (eventEnd - eventStart).TotalSeconds;

        int secsBetweenPoints;
        double numOfPoints = 50.0;

        if ((int)(diffInSeconds / numOfPoints) > 1)
            secsBetweenPoints = (int)(diffInSeconds / numOfPoints);
        else
            secsBetweenPoints = 1;

        List<object> pts;
        Series ser;
        Evaluator e = new Evaluator();
        Evaluation ev = new Evaluation();

        //create a series of points for each evaluator in event
        for (int j = 0; j < theEvent.Evaluators.Count; j++)
        {
            ser = new Series();
            pts = new List<object>();

            liOfSeries.Add(ser);
            points.Add(pts);
        }

        //add a point for each evaluator at specified time interval
        for (DateTime i = eventStart; i < eventEnd; i = i.AddSeconds(secsBetweenPoints))
        {
            for (int j = 0; j < theEvent.Evaluators.Count; j++)
            {
                e = theEvent.Evaluators[j];

                if (e.EvaluatorEvaluations.Count > 0)
                {
                    //don't make a point if he evaluator hasn't changed his rating yet
                    if (e.EvaluatorEvaluations[0].TimeStamp.ToLocalTime() <= i)
                    {
                        int evalCount = 0;
                        while (evalCount < e.EvaluatorEvaluations.Count)
                        {
                            //get the last evaluation before time i
                            if (e.EvaluatorEvaluations[evalCount].TimeStamp.ToLocalTime() <= i)
                            {
                                ev = e.EvaluatorEvaluations[evalCount];
                                evalCount++;
                            }
                            else
                            {
                                break;
                            }
                        }
                        //get time from the start of the event
                        double timestamp = (i - eventStart).TotalMilliseconds;

                        points[j].Add(new
                        {
                            X = timestamp,
                            Y = ev.Rating
                        });
                    }
                }
                
            }
        }
        //give data to each series
        for (int k = 0; k < liOfSeries.Count; k++)
        {
            liOfSeries[k].Data = new Data(points[k].ToArray());
            liOfSeries[k].Color = System.Drawing.Color.
                     FromArgb(255, rand.Next(50, 200), rand.Next(50, 200), rand.Next(50, 200));
            //liOfSeries[k].Name = String.Format("{1} ({0})", theEvent.Evaluators[k].EvaluatorID, theEvent.Evaluators[k].Name);

            if (theEvent.Evaluators[k].Name != "Default")
                liOfSeries[k].Name = String.Format("{0}", theEvent.Evaluators[k].Name);
            else
                liOfSeries[k].Name = String.Format("ID:{0}", theEvent.Evaluators[k].EvaluatorID);
        }

        //return chart data
        Highcharts chart = makeChartInfo(theEvent, liOfSeries, ChartTypes.Spline);

        return chart;
    }
   
    private Highcharts makeChartInfo(Event theEvent, List<Series> liOfSeries, ChartTypes chartType)
    {
        //get start and end times, convert to device localtime
        DateTime eventStart = theEvent.EventStart.ToLocalTime();
        DateTime eventEnd = theEvent.EventEnd.ToLocalTime();

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
        });
        chart.SetPlotOptions(new PlotOptions
        {
        });
        chart.SetExporting(new Exporting
        {
            Enabled = true
        });
        chart.SetLegend(new Legend
        {
            Enabled = true,
            BackgroundColor = new BackColorOrGradient(System.Drawing.Color.FromName("'#f1f2f7'"))
        });
        chart.SetNavigation(new Navigation
        {
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
            Type = AxisTypes.Datetime,
            DateTimeLabelFormats = new DateTimeLabel
            {
            },
            Labels = new XAxisLabels
            {
                StaggerLines = 2
            },
            Min = 0
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
        });
        return chart;
    }

    //make chart was calculations for mode, median, mean
    public Highcharts MakeMathChart(Event theEvent, string math)
    {
        //make a series and points for each calculation type
        List<Series> liOfSeries = new List<Series>();
        List<object> points = new List<object>();
        Random rand = new Random();

        Series serMean = new Series();
        List<object> meanPoints = new List<object>();

        switch (math)
        {
            case "Mean":
                serMean.Name = "Mean";
                break;
            case "Median":
                serMean.Name = "Median";
                break;            
            case "Mode":
                serMean.Name = "Mode";
                break;
            default:
                break;
        }


        //make a list of every evaluation for the event
        List<Evaluation> allEvaluations = new List<Evaluation>();
        foreach (Evaluator e in theEvent.Evaluators)
        {
            allEvaluations.AddRange(e.EvaluatorEvaluations);
        }

        //get start and end times for event, convert to device local time
        DateTime eventStart = theEvent.EventStart.ToLocalTime();
        DateTime eventEnd = theEvent.EventEnd.ToLocalTime();

        //calculate the time between points depending on length of event
        double diffInSeconds = (eventEnd - eventStart).TotalSeconds;

        int secsBetweenPoints;
        double numOfPoints = 50.0;      //maximum 50 points on chart

        //minimum 1 point a second
        if ((int)(diffInSeconds / numOfPoints) > 1)
            secsBetweenPoints = (int)(diffInSeconds / numOfPoints);
        else
            secsBetweenPoints = 1;

        List<int> ratings;

        //calculate at each interval on chart
        for (DateTime i = eventStart; i < eventEnd; i = i.AddSeconds(secsBetweenPoints))
        {
            ratings = new List<int>();

            //get the most current rating for each evaluator at timestamp
            ratings = getRatingsAtTimestamp(i.ToUniversalTime(), theEvent);

            //if there are any ratings at time i, put them on the chart
            if (ratings.Count > 0)
            {
                //time since event start
                double timestamp = (i - eventStart).TotalMilliseconds;

                switch (math)
                {
                    case "Mean":
                        //calc mean
                        meanPoints.Add(new
                        {
                            X = timestamp,
                            Y = ratings.Average(x => x)
                        });
                        break;
                    case "Median":
                        //calc median
                        meanPoints.Add(new
                        {
                            X = timestamp,
                            Y = GetMedian(ratings)
                        });
                        break;
                    case "Mode":
                        //calc mode
                        meanPoints.Add(new
                        {
                            X = timestamp,
                            Y = ratings.GroupBy(v => v)
                                    .OrderByDescending(g => g.Count())
                                    .First()
                                    .Key
                        });
                        break;
                    default:
                        break;
                }           
            }
        }

        switch (math)
        {
            case "Mean":
                serMean.Data = new Data(meanPoints.ToArray());
                break;
            case "Median":
                serMean.Data = new Data(meanPoints.ToArray());
                break;
            case "Mode":
                serMean.Data = new Data(meanPoints.ToArray());
                break;
            default:
                break;
        }

        //add points to series
        
        
        

        switch (math)
        {
            case "Mean":
                serMean.Name = "Mean";
                serMean.Color = System.Drawing.Color.
                FromArgb(150, rand.Next(150, 256), rand.Next(25), rand.Next(25));
                break;
            case "Median":
                serMean.Name = "Median";
                serMean.Color = System.Drawing.Color.
               FromArgb(150, rand.Next(25), rand.Next(25), rand.Next(150, 256));
                break;
            case "Mode":
                serMean.Name = "Mode";
                serMean.Color = System.Drawing.Color.
                FromArgb(150, rand.Next(25), rand.Next(150, 256), rand.Next(25));
                break;
            default:
                break;
        }

        //set series colour
        
        
       

        

        liOfSeries.Add(serMean);

        

        //add data to chart
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
            Global = new Global { UseUTC = true }
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
            BackgroundColor = new BackColorOrGradient(System.Drawing.Color.FromName("'#f1f2f7'"))
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
            //Type = AxisTypes.Linear,
            Type = AxisTypes.Datetime,
            DateTimeLabelFormats = new DateTimeLabel
            {

            },
            Labels = new XAxisLabels
            {

                //Format = string.Format("{0}:{1}:{2}", this.Value, this.)
                StaggerLines = 2
            },
            Min = 0
        });
        chart.SetYAxis(new YAxis
        {
            Title = new YAxisTitle
            {
                Text = "Rating"
            },
            Max = 10,
            Min = 0
        });
        chart.SetSeries(liOfSeries.ToArray());
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

            if (e.EvaluatorEvaluations.Count > 0)
            {
                //if the first rating was before the timestamp don' add anything to list
                if (e.EvaluatorEvaluations[count].TimeStamp < Timestamp)
                {
                    //find the last rating before the timestamp, add to list
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
        }

        return ratings;
    }
    //calculate the median of a list of ints
    public double GetMedian(List<int> ints)
    {
        double median;

        ints.Sort();

        //calculate depending on even or odd number of ints
        if (ints.Count % 2 != 0)
            median = ints[ints.Count / 2];
        else
            median = (((double)ints[ints.Count / 2 - 1] + (double)ints[ints.Count / 2]) / 2.0);


        return Math.Round(median);
    }
    public double GetMode(List<int> ints)
    {
        double Mode;

        var groups = ints.GroupBy(x => x);
        int highCount = groups.Max(o => groups.Count());
        List<int> winners = new List<int>();

        foreach (var i in groups)
        {
            if (i.Count() == highCount)
                winners.Add(i.Key);
        }

        Mode = winners.Average();

        return Mode;
    }
}