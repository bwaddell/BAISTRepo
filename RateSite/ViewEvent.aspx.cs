﻿using System;
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
    DateTime defaultTime = Convert.ToDateTime("1/1/1800 12:00:00 PM");


    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
        scriptManager.RegisterPostBackControl(this.Export);

        if (!IsPostBack)
        {
            CSS Director = new CSS();

            //for comparing default values of event start and end times.

            //Get evaluation data for chosen event
            Event theEvent = new Event();
            theEvent.EventID = ((Event)Session["Event"]).EventID;

            theEvent = Director.GetEvent(theEvent);

            tbEventID.Text = theEvent.EventID.ToString();
            tbPerformer.Text = theEvent.Performer;
            tbLocation.Text = theEvent.Location;
            tbDate.Text = theEvent.Date.ToShortDateString();
            tbDesc.Text = theEvent.Description;
            //int numOfEvaluators = theEvent.Evaluators.Count;

            //if the event has ended disable start/end buttons and generate charts and table
            if (theEvent.EventEnd != defaultTime)
            {
                tbStart.Text = theEvent.EventStart.ToLocalTime().ToLongTimeString();
                tbEnd.Text = theEvent.EventEnd.ToLocalTime().ToLongTimeString();
                ButtonStart.Enabled = false;
                ButtonEnd.Enabled = false;
                Export.Enabled = true;
                TimerForTableRefresh.Enabled = false;
                BuildTable();
                BuildCharts();
            }
            else
            {
                //if event has not begun
                if (theEvent.EventStart == defaultTime)
                {
                    tbStart.Text = "The Event has not begun.";
                    tbEnd.Text = "The Event has not begun.";
                    ButtonStart.Enabled = true;
                    ButtonEnd.Enabled = true;
                    Export.Enabled = false;
                    TimerForTableRefresh.Enabled = false;
                }
                //if event is still active
                else
                {
                    tbStart.Text = theEvent.EventStart.ToLocalTime().ToLongTimeString();
                    tbEnd.Text = "The Event is active.";
                    ButtonStart.Enabled = false;
                    TimerForTableRefresh.Enabled = true;
                    Export.Enabled = false;
                    ButtonEnd.Enabled = true;
                }
            }
        }

    }

    //export event data to .csv for external use
    protected void Export_Click(object sender, EventArgs e)
    {
        //Initialize instances of all neccessary classes and director
        CSS Director = new CSS();
        Event Event = new Event();
        Facilitator Facilitator = new Facilitator();
        List<Evaluation> Evaluations = new List<Evaluation>();
        StringBuilder csvcontent = new StringBuilder();

        //Set the Event.ID of our empty event, and use said event to pull event information from DB
        Event.EventID = ((Event)Session["Event"]).EventID;
        Event = Director.GetEvent(Event);
        Facilitator = Director.GetFacilitator(1);

        //Creates the first section of data in the CSV, regarding the Facilitators info. Data is formatted in two lines, the first line being the data description, the second line being the respective data.
        csvcontent.AppendLine("First Name,Last Name,Title,Organization,Location,Email");
        csvcontent.AppendLine(Facilitator.FirstName + "," + Facilitator.LastName + "," + Facilitator.Title + "," + Facilitator.Organization + "," + Facilitator.Location + "," + Facilitator.Email);
        csvcontent.AppendLine("\n");

        //Creates the second line of data in the CSV, regarding the Events info. Formatted as above.
        csvcontent.AppendLine("Event,Performer,Location,Date of Event,Event Start Date,Event End Date");
        csvcontent.AppendLine(Event.Description + "," + Event.Performer + "," + Event.Location + "," + Event.Date.ToShortDateString() + "," + Event.EventStart.ToLongTimeString() + "," + Event.EventEnd.ToLongTimeString());
        csvcontent.AppendLine("\n");

        //Creates a section of data for each evaluator, formatted with each data point underneath its respective timestamp
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

        //Clear the response and re package it as a downloadable CSV file
        Response.Clear();
        Response.ContentType = "text/csv";
        Response.AddHeader("Content-Disposition", "attachment;filename=EventData.csv");
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
            tbEnd.Text = "The Event has started.";
            ButtonStart.Enabled = false;
            ButtonEnd.Enabled = true;

            TimerForTableRefresh.Enabled = true;

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
            //Reload page
            Response.Redirect("ViewEvent.aspx", true);
        }
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
        lbUpdateTime.Text = DateTime.Now.ToLocalTime().ToString();
        //DateTime defaultTime = Convert.ToDateTime("1/1/1800 12:00:00 PM");
        CSS RequestDirector = new CSS();

        //get event evaluation data
        List<Evaluation> currentEvals = new List<Evaluation>();
        Event activeEvent = new Event();
        activeEvent.EventID = ((Event)Session["Event"]).EventID;

        activeEvent = RequestDirector.GetEvent(activeEvent);

        //get most recent evaluation from each evaluator
        currentEvals = RequestDirector.GetCurrentEventData(activeEvent);


        foreach (Evaluator ev in activeEvent.Evaluators)
        {
            TableRow tRow = new TableRow();
            TableCell tCell = new TableCell();

            tCell.Text = ev.EvaluatorID.ToString();
            tRow.Cells.Add(tCell);

            tCell = new TableCell();
            tCell.Text = ev.EvaluatorEvaluations.Last().Rating.ToString();
            tRow.Cells.Add(tCell);

            tCell = new TableCell();            //add the avg rating of the eval
            tCell.Text = (ev.EvaluatorEvaluations.Average(x => x.Rating)).ToString("#.##");
            tRow.Cells.Add(tCell);

            tCell = new TableCell();
            tCell.Text = ev.EvaluatorEvaluations.Last().TimeStamp.ToString();
            tRow.Cells.Add(tCell);

            Table1.Rows.Add(tRow);
        }

        //if event is over calculate the average rating for the whole event.
        //else calculate the current average if the event is currently active
        if (activeEvent.EventEnd != defaultTime)
        {
            if (currentEvals.Count != 0)
            {
                double totalAverage;
                List<Evaluation> allEvaluations = new List<Evaluation>();

                //change the label
                RatingTitle.Text = "Total Average Rating:";

                //create list of all evals for event then average
                foreach (Evaluator ev in activeEvent.Evaluators)
                {
                    allEvaluations.AddRange(ev.EvaluatorEvaluations);
                }
                totalAverage = allEvaluations.Average(o => o.Rating);
                Ratinglbl.Text = totalAverage.ToString("#.##");

                lbTotalEvalsNum.Text = activeEvent.Evaluators.Count.ToString();
            }
        }
        else
        {
            if (currentEvals.Count != 0)
            {
                Ratinglbl.Text = currentEvals.Average(x => (double)x.Rating).ToString("#.##");
                lbTotalEvalsNum.Text = activeEvent.Evaluators.Count.ToString();
            }
        }

        
    }

    protected void TimerForTableRefresh_Tick(object sender, EventArgs e)
    {
        BuildTable();
    }
}