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
            
            //for comparing default values of event start and end times.
            DateTime defaultTime = Convert.ToDateTime("01-01-1800 12:00:00");

            //Get evaluation data for chosen event
            Event theEvent = new Event();
            theEvent.EventID = ((Event)Session["Event"]).EventID;

            theEvent = Director.GetEvent(theEvent);

            tbEventID.Text = theEvent.EventID.ToString();
            tbPerformer.Text = theEvent.Performer;
            tbLocation.Text = theEvent.Location;
            tbDate.Text = theEvent.Date.ToLongDateString();
            tbDesc.Text = theEvent.Description;
            //int numOfEvaluators = theEvent.Evaluators.Count;

            //if the event has ended hide start/end buttons and generate charts and table
            if (theEvent.EventEnd != defaultTime)
            {
                tbStart.Text = theEvent.EventStart.ToLocalTime().ToLongTimeString();
                tbEnd.Text = theEvent.EventEnd.ToLocalTime().ToLongTimeString();
                ButtonStart.Visible = false;
                ButtonEnd.Visible = false;
                TimerForTableRefresh.Enabled = false;
                BuildTable();
                BuildCharts();
            }   
            else
            {
                //if event has not begun
                if (theEvent.EventStart == defaultTime)
                {
                    tbStart.Text = "The Event has not yet begun.";
                    tbEnd.Text = "The Event has not yet begun.";
                    ButtonStart.Visible = true;
                    TimerForTableRefresh.Enabled = false;
                }
                //if event is still active
                else
                {
                    tbStart.Text = theEvent.EventStart.ToLocalTime().ToLongTimeString();
                    tbEnd.Text = "The Event is active.";
                    ButtonStart.Visible = false;
                    TimerForTableRefresh.Enabled = true;
                    ButtonEnd.Visible = true;
                    ButtonEnd.Enabled = true;
                }
            }         
        }
        
    }

    //export event data to .csv for external use
    protected void Export_Click(object sender, EventArgs e)
    {
        CSS Director = new CSS();
        Event Event = new Event();
        Facilitator Facilitator = new Facilitator();
        List<Evaluation> Evaluations = new List<Evaluation>();
        StringBuilder csvcontent = new StringBuilder();

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


    //begin the event if start button clicked
    protected void ButtonStart_Click(object sender, EventArgs e)
    {
        CSS Manager = new CSS();
        bool confirmation = false;
        Event updateMe = new Event();

        //get event info
        updateMe.EventID = tbEventID.Text;
        updateMe = Manager.GetEvent(updateMe);

        //update event with start time
        updateMe.EventStart = DateTime.Now.ToUniversalTime();
        confirmation = Manager.UpdateEventStatus(updateMe);

        if (confirmation)
        {
            tbStart.Text = updateMe.EventStart.ToLocalTime().ToLongTimeString();
            tbEnd.Text = "The Event is ongoing.";
            ButtonStart.Visible = false;
        }
            
    }

    //end event when button clicked
    protected void ButtonEnd_Click(object sender, EventArgs e)
    {
        CSS Manager = new CSS();
        bool confirmation = false;

        //get event info
        Event updateMe = new Event();
        updateMe.EventID = tbEventID.Text;
        updateMe = Manager.GetEvent(updateMe);

        //update event with end time
        updateMe.EventEnd = DateTime.Now.ToUniversalTime();
        confirmation = Manager.UpdateEventStatus(updateMe);

        //generate table and charts 
        if (confirmation)
        {
            ButtonEnd.Visible = false;
            tbEnd.Text = updateMe.EventEnd.ToLocalTime().ToLongTimeString();
            BuildTable();
            BuildCharts();
        }             
    }

    protected void btnTable_Click(object sender, EventArgs e)
    {
        BuildTable();
    }

    public void BuildCharts()
    {
        CSS Director = new CSS();

        //get event info
        Event theEvent = new Event();
        theEvent.EventID = ((Event)Session["Event"]).EventID;
        theEvent = Director.GetEvent(theEvent);

        //if event has evaluator data, construct the chart
        if (theEvent.Evaluators.Count > 0)
        {
            Highcharts chart = Director.CreateChart(theEvent);

            //write the chart to the div(literal) on web page
            //the rest is automatic
            ltrChart.Text = chart.ToHtmlString();

            //generate chart with mean/mode/median
            Highcharts mChart = Director.MakeMathChart(theEvent);
            mathChart.Text = mChart.ToHtmlString();
        }
    }

    //build table with evaluator info
    public void BuildTable()
    {
        lbUpdateTime.Text = "Update Time: " + DateTime.Now.ToLocalTime().ToString();

        CSS RequestDirector = new CSS();

        //get event evaluation data
        List<Evaluation> currentEvals = new List<Evaluation>();
        Event activeEvent = new Event();
        activeEvent.EventID = ((Event)Session["Event"]).EventID;

        //get most recent evaluation from each evaluator
        currentEvals = RequestDirector.GetCurrentEventData(activeEvent);

        //build table with evaluation data
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
            tCell.Text = ev.TimeStamp.ToLocalTime().ToString();
            tRow.Cells.Add(tCell);

            Table1.Rows.Add(tRow);
        }

        //calculate current average rating
        Ratinglbl.Text = currentEvals.Average(x => (double)x.Rating).ToString("#.##");
    }
}