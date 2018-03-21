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
            lbStartTime.Text = "<br /> Page Loaded at: " + DateTime.Now.ToLocalTime().ToString();

        //tbEventID.Text = ((Event)Session["Event"]).EventID;
        tbEventID.Text = "ABCD";

        CSS Director = new CSS();
        Evaluation eval = new Evaluation();

        Highcharts chart = new Highcharts("chart")
        //{
        //    Type = ChartTypes.Spline,
        //    BackgroundColor = new BackColorOrGradient(System.Drawing.Color.FromName("'#f1f2f7'")),
        //    ZoomType = ZoomTypes.X
        //};

        

                        .SetTitle(new Title
                        {
                            Text = "myHighChart"
                        })
                        
                        .SetLegend(new Legend
                        {
                            Enabled = true
                    //BackgroundColor = new BackColorOrGradient(System.Drawing.Color.FromName("'#ffffff'"))
                        })
                        .SetXAxis(new XAxis
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
                        })
                        .SetTooltip(new Tooltip
                        {
                            Shared = true,
                            Shadow = true
                        })
                        .SetYAxis(new YAxis
                        {
                            Title = new YAxisTitle
                            {
                                Text = ""
                            }
                        })

                        .SetSeries(new[]
                            {
                        new Series{
                            Name = "Parameter1",
                            Type = ChartTypes.Line,
                            Data = new Data(new object[] { "3200", "1300", "1400", "1600", "1800", "2000", "2200", "2300", "2400", "2500", "2600", "3200" }),
                            Color = System.Drawing.Color.FromName("'#4CB7A5'")
                        },

                        new Series{
                            Name = "high",
                            Type = ChartTypes.Line,
                            Data = new Data(new object[] { 1100, 1500, 1200, 1800, 2000, 2100, 2300, 2500,2600, 2700, 2800, 2900 }),
                            Color = System.Drawing.Color.FromName("'#ff6c60'")
                        },
                            });


        //litChart.text = chart.ToHtmlString();
        ltrChart.Text = chart.ToHtmlString();   

}



    protected void btnTable_Click(object sender, EventArgs e)
    {
        lbUpdateTime.Text = "Update Time: " + DateTime.Now.ToLocalTime().ToString();

        CSS RequestDirector = new CSS();

        List<Evaluation> currentEvals = new List<Evaluation>();

        Event test = new Event();
        test.EventID = "ABCD";

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




    }




    protected void btnHighChart_Click(object sender, EventArgs e)
    {
        lbHighChartUpdateTime.Text = "Update Time: " + DateTime.Now.ToLocalTime().ToString();



        //DotNet.Highcharts.Highcharts chart = new DotNet.Highcharts.Highcharts("chart");
        //chart.SetXAxis(new DotNet.Highcharts.Options.XAxis
        //{
        //    Categories = new[] { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" }
        //});

        //chart.SetSeries(new DotNet.Highcharts.Options.Series
        //{
        //    Data = new DotNet.Highcharts.Helpers.Data(new object[] { 29.9, 71.5, 106.4, 129.2, 144.0, 176.0, 135.6, 148.5, 216.4, 194.1, 95.6, 54.4 })
        //});

        //chart.SetTitle(new DotNet.Highcharts.Options.Title { Text = "myTitle" });





        //ltrChart.Text = chart.ToHtmlString();
    }
}