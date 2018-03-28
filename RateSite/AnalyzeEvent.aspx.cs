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
        tbEventID.Text = "aaaa";

        

}



    protected void btnTable_Click(object sender, EventArgs e)
    {
        lbUpdateTime.Text = "Update Time: " + DateTime.Now.ToLocalTime().ToString();

        CSS RequestDirector = new CSS();

        List<Evaluation> currentEvals = new List<Evaluation>();

        Event test = new Event();
        test.EventID = "aaaa";

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




    protected void btnChart_Click(object sender, EventArgs e)
    {
        lbChartUpdateTime.Text = "Update Time: " + DateTime.Now.ToLocalTime().ToString();



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