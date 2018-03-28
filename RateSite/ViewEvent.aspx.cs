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

    protected void Export_Click(object sender, EventArgs e)
    {
        CSS Director = new CSS();
        Event Event = new Event();
        Facilitator Facilitator = new Facilitator();
        List<Evaluation> Evaluations = new List<Evaluation>();
        StringBuilder csvcontent = new StringBuilder();

        Event.EventID = "aaaa";
        Event = Director.GetEvent(Event);
        Facilitator = Director.GetFacilitator(1);

        csvcontent.AppendLine("First Name,Last Name,Title,Organization,Location,Email");
        csvcontent.AppendLine(Facilitator.FirstName + "," + Facilitator.LastName + "," + Facilitator.Title + "," + Facilitator.Organization + "," + Facilitator.Location + "," + Facilitator.Email);
        csvcontent.AppendLine("\n");

        csvcontent.AppendLine("Event,Performer,Location,Date of Event,Event Start Date,Event End Date");
        csvcontent.AppendLine(Event.Description + "," + Event.Performer + "," + Event.Location + "," + Event.Date.ToShortDateString() + "," + Event.EventStart.ToLongTimeString() + "," + Event.EventEnd.ToLongTimeString());
        csvcontent.AppendLine("\n");

        foreach (Evaluator User in Event.Evaluators)
        {
            Evaluations = Director.GetEvaluatorEvaluations(Event.EventID, User.EvaluatorID);

            string insert = " ,";

            for (int i = 0; i < Evaluations.Count; i++)
            {
                if (i == Evaluations.Count - 1)
                    insert += Evaluations[i].TimeStamp.ToLongTimeString();
                else
                    insert += Evaluations[i].TimeStamp.ToLongTimeString() + ",";
            }

            csvcontent.AppendLine(insert);

            insert = "ID: " + User.EvaluatorID.ToString() + ",";

            for (int i = 0; i < Evaluations.Count; i++)
            {
                if (i == Evaluations.Count - 1)
                    insert += Evaluations[i].Rating.ToString();
                else
                    insert += Evaluations[i].Rating.ToString() + ",";
            }

            csvcontent.AppendLine(insert);
            csvcontent.AppendLine("\n");
        }

        Response.Clear();
        Response.ContentType = "text/csv";
        Response.AddHeader("Content-Disposition", "attachment;filename=myfilename.csv");
        Response.Write(csvcontent.ToString());
        Response.End();
    }
}