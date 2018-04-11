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
        if (!IsPostBack)
        {
            CSS Director = new CSS();
            // List<Evaluation> EventData = new List<Evaluation>();
            DateTime defaultTime = Convert.ToDateTime("01-01-1800 12:00:00");
            Event theEvent = new Event();

            //theEvent.EventID = "aaaa";
            theEvent.EventID = ((Event)Session["Event"]).EventID;


            //Get ALL event Data!!!

            theEvent = Director.GetEvent(theEvent);

            tbEventID.Text = theEvent.EventID.ToString();
            tbPerformer.Text = theEvent.Performer;
            tbLocation.Text = theEvent.Location;
            tbDate.Text = theEvent.Date.ToLongDateString();
            tbDesc.Text = theEvent.Description;
            //int numOfEvaluators = theEvent.Evaluators.Count;

            if (theEvent.EventEnd != defaultTime)
            {
                tbStart.Text = theEvent.EventStart.ToLongTimeString();
                tbEnd.Text = theEvent.EventEnd.ToLongTimeString();
                ButtonStart.Visible = false;
                ButtonEnd.Visible = false;
                TimerForTableRefresh.Enabled = false;
                BuildTable();
                BuildCharts();
            }
            else
            {
                if (theEvent.EventStart == defaultTime)
                {
                    tbStart.Text = "The Event has not yet begun.";
                    tbEnd.Text = "The Event has not yet begun.";
                    ButtonStart.Visible = true;
                    TimerForTableRefresh.Enabled = false;
                }
                else
                {
                    tbStart.Text = theEvent.EventStart.ToLongTimeString();
                    tbEnd.Text = "The Event is active.";
                    ButtonStart.Visible = false;
                    TimerForTableRefresh.Enabled = true;
                    ButtonEnd.Visible = true;
                    ButtonEnd.Enabled = true;
                }
            }         
        }
        
    }


    protected void Export_Click(object sender, EventArgs e)
    {
        CSS Director = new CSS();
        Event Event = new Event();
        Facilitator Facilitator = new Facilitator();
        List<Evaluation> Evaluations = new List<Evaluation>();
        StringBuilder csvcontent = new StringBuilder();

        //Event.EventID = "aaaa";
        Event.EventID = ((Event)Session["Event"]).EventID;
        Event = Director.GetEvent(Event);
        Facilitator = Director.GetFacilitator(1);
       

        csvcontent.AppendLine("First Name,Last Name,Title,Organization,Location,Email");
        csvcontent.AppendLine(Facilitator.FirstName + "," + Facilitator.LastName + "," + Facilitator.Title + "," + Facilitator.Organization + "," + Facilitator.Location + "," + Facilitator.Email);
        csvcontent.AppendLine("\n");

        
        if(Event.EventStart == null)
        {
            csvcontent.AppendLine("Event,Performer,Location,Date of Event,Event Start Date,Event End Date");
            csvcontent.AppendLine(Event.Description + "," + Event.Performer + "," + Event.Location + "," + Event.Date.ToShortDateString() + ",Not Set," + Event.EventEnd.ToLongTimeString());
            csvcontent.AppendLine("\n");
        }
        else if(Event.EventEnd == null)
        {
            csvcontent.AppendLine("Event,Performer,Location,Date of Event,Event Start Date,Event End Date");
            csvcontent.AppendLine(Event.Description + "," + Event.Performer + "," + Event.Location + "," + Event.Date.ToShortDateString() + "," + Event.EventStart.ToLongTimeString() + "," + Event.EventEnd.ToLongTimeString());
            csvcontent.AppendLine("\n");
        }
        else
        {
            csvcontent.AppendLine("Event,Performer,Location,Date of Event,Event Start Date,Event End Date");
            csvcontent.AppendLine(Event.Description + "," + Event.Performer + "," + Event.Location + "," + Event.Date.ToShortDateString() + "," + Event.EventStart.ToLongTimeString() + ",Not Set");
            csvcontent.AppendLine("\n");
        }

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

    protected void btnTable_Click(object sender, EventArgs e)
    {
        BuildTable();
    }

    //protected void btnChart_Click(object sender, EventArgs e)
    //{
    //    BuildCharts();    
    //}

    protected void ButtonStart_Click(object sender, EventArgs e)
    {
        CSS Manager = new CSS();
        bool confirmation = false;
        Event updateMe = new Event();

        updateMe.EventID = tbEventID.Text;

        updateMe = Manager.GetEvent(updateMe);

        updateMe.EventStart = DateTime.Now;
        confirmation = Manager.UpdateEventStatus(updateMe);

        if (confirmation)
        {
            tbStart.Text = updateMe.EventStart.ToLongTimeString();
            tbEnd.Text = "The Event is ongoing.";
            ButtonStart.Visible = false;
        }
            
    }

    protected void ButtonEnd_Click(object sender, EventArgs e)
    {
        CSS Manager = new CSS();
        bool confirmation = false;
        Event updateMe = new Event();

        updateMe.EventID = tbEventID.Text;

        updateMe = Manager.GetEvent(updateMe);

        updateMe.EventEnd = DateTime.Now;
        confirmation = Manager.UpdateEventStatus(updateMe);

        if (confirmation)
        {
            ButtonEnd.Visible = false;
            tbEnd.Text = updateMe.EventEnd.ToLongTimeString();
            BuildTable();
            BuildCharts();
        }             
    }
    public void BuildCharts()
    {
        CSS Director = new CSS();
        // List<Evaluation> EventData = new List<Evaluation>();

        Event theEvent = new Event();

        //theEvent.EventID = "aaaa";
        theEvent.EventID = ((Event)Session["Event"]).EventID;

        //Get ALL event Data!!!

        theEvent = Director.GetEvent(theEvent);
        //int numOfEvaluators = theEvent.Evaluators.Count;

        if (theEvent.Evaluators.Count > 0)
        {
            Highcharts chart = Director.CreateChart(theEvent);

            //write the chart to the div(literal) on web page
            //the rest is automatic
            ltrChart.Text = chart.ToHtmlString();

            Highcharts mChart = Director.MakeMathChart(theEvent);
            mathChart.Text = mChart.ToHtmlString();
        }
    }
    public void BuildTable()
    {
        lbUpdateTime.Text = "Update Time: " + DateTime.Now.ToLocalTime().ToString();

        CSS RequestDirector = new CSS();

        List<Evaluation> currentEvals = new List<Evaluation>();

        Event test = new Event();
        //test.EventID = "AAAA";
        test.EventID = ((Event)Session["Event"]).EventID;

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
}