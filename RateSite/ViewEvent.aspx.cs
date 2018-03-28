using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Text;
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

        Highcharts chart = Director.CreateChart(theEvent);

        //write the chart to the div(literal) on web page
        //the rest is automatic
        ltrChart.Text = chart.ToHtmlString();
    }






    }

}