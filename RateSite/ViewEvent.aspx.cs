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
        ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
        scriptManager.RegisterPostBackControl(this.Export);

        DateTime defaultTime = Convert.ToDateTime("1800-01-01 12:00:00 PM");

        if (!IsPostBack)
        {
            CSS Director = new CSS();

            //for comparing default values of event start and end times.

            //Get evaluation data for chosen event
            Event theEvent = new Event();
            theEvent.EventID = ((Event)Session["Event"]).EventID;

            theEvent = Director.GetEvent(theEvent);


            if (theEvent.EventKey.ToString() == "ZZZZ")
                tbEventID.Text = "Key Expired";
            else
                tbEventID.Text = theEvent.EventKey.ToString();

            tbPerformer.Text = theEvent.Performer;
            tbLocation.Text = theEvent.Location;
            tbDate.Text = theEvent.Date.ToLongDateString();
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
                RepeatBtn.Enabled = true;
                PanelChartRadio.Visible = true;
                BuildTable();
                BuildCharts(RBLmath.SelectedValue);

            }
            else
            {
                //if event has not begun
                if (theEvent.EventStart == defaultTime)
                {
                    tbStart.Text = "The Event has not begun.";
                    tbEnd.Text = "The Event has not begun.";
                    ButtonStart.Enabled = true;
                    ButtonEnd.Enabled = false;
                    Export.Enabled = false;
                    RepeatBtn.Enabled = false;
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
                    RepeatBtn.Enabled = false;
                }
            }
        }
        else
        {
            BuildTable();
            BuildCharts(RBLmath.SelectedValue);
        }

    }

    //export event data to .csv for external use
    protected void Export_Click(object sender, EventArgs e)
    {
        //Initialize instances of all neccessary classes and director
        CSS Director = new CSS();
        Event theEvent = new Event();
        Facilitator Facilitator = new Facilitator();
        //List<Evaluation> Evaluations = new List<Evaluation>();
        StringBuilder csvcontent = new StringBuilder();
        string insert;
        DateTime addEval;

        //Set the Event.ID of our empty event, and use said event to pull event information from DB
        theEvent.EventID = ((Event)Session["Event"]).EventID;
        theEvent = Director.GetEvent(theEvent);
        Facilitator = Director.GetFacilitator(1);

        //Creates the first section of data in the CSV, regarding the Facilitators info. Data is formatted in two lines, the first line being the data description, the second line being the respective data.
        csvcontent.AppendLine("First Name,Last Name,Title,Organization,Location,Email");
        csvcontent.AppendLine(Facilitator.FirstName + "," + Facilitator.LastName + "," + Facilitator.Title + "," + Facilitator.Organization + "," + Facilitator.Location + "," + Facilitator.Email);
        csvcontent.AppendLine("\n");

        //Creates the second line of data in the CSV, regarding the Events info. Formatted as above.
        csvcontent.AppendLine("Event,Performer,Location,Date of Event,Start Time,End Time");
        csvcontent.AppendLine(theEvent.Description + "," + theEvent.Performer + "," + theEvent.Location + "," + theEvent.Date.ToShortDateString() + "," + theEvent.EventStart.ToLongTimeString() + "," + theEvent.EventEnd.ToLongTimeString());
        csvcontent.AppendLine("\n");

        //get start and end times for table.  Convert to local time
        DateTime eventStart = theEvent.EventStart.ToLocalTime();
        DateTime eventEnd = theEvent.EventEnd.ToLocalTime();
        double secsBetweenPoints = 0.5; //iterate every 0.5 second

        //display timestamp for every 0.5 s
        List<string> timestamps = new List<string>();

        for (DateTime i = eventStart; i <= eventEnd; i = i.AddSeconds(secsBetweenPoints))
        {
            timestamps.Add(String.Format("{0}", (i - eventStart).ToString(@"hh\:mm\:ss\:fff")));
        }
        string tsLine = "TimeStamp (HH:MM:SS.mmm):,";

        for (int k = 0; k < timestamps.Count; k++)
        {
            if (k == timestamps.Count - 1)
                tsLine += timestamps[k];
            else
                tsLine += (timestamps[k] + ",");
        }

        csvcontent.AppendLine(tsLine);


        //Add all evaluator evaluations
        Evaluator _evaluator = new Evaluator();
        Evaluation _evaluation = new Evaluation();
        int evalCount = 0;

        foreach (Evaluator eval in theEvent.Evaluators)
        {
            insert = "ID: " + eval.EvaluatorID.ToString() + ",";


            for (DateTime i = eventStart; i <= eventEnd; i = i.AddSeconds(secsBetweenPoints))
            {
                if (eval.EvaluatorEvaluations[0].TimeStamp.ToLocalTime() <= i)
                {
                    evalCount = 0;
                    while (evalCount < eval.EvaluatorEvaluations.Count - 1)
                    {
                        //get the last evaluation before time i
                        if (eval.EvaluatorEvaluations[evalCount].TimeStamp.ToLocalTime() <= i)
                        {
                            //ev = e.EvaluatorEvaluations[evalCount];
                            evalCount++;
                        }
                        else
                        {
                            break;
                        }
                    }
                    //get time from the start of the event
                    //double timestamp = (i - eventStart).TotalMilliseconds;

                    insert += eval.EvaluatorEvaluations[evalCount].Rating.ToString() + " ,";
                }
                else
                {
                    insert += " ,";
                }


            }
            csvcontent.AppendLine(insert);

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
        updateMe.EventID = Convert.ToInt32(((Event)Session["Event"]).EventID);
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
        updateMe.EventID = Convert.ToInt32(((Event)Session["Event"]).EventID);
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


    public void BuildCharts(string mathType)
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
            Highcharts mChart = Director.MakeMathChart(theEvent, mathType);
            mathChart.Text = mChart.ToHtmlString();
        }
    }

    //build table with evaluator info
    public void BuildTable()
    { 
        DateTime defaultTime = Convert.ToDateTime("1800-01-01 12:00:00 PM");

        lbUpdateTime.Text = DateTime.Now.ToLocalTime().ToString();
        CSS RequestDirector = new CSS();

        //get event evaluation data
        List<Evaluation> currentEvals = new List<Evaluation>();
        Event activeEvent = new Event();
        activeEvent.EventID = ((Event)Session["Event"]).EventID;

        activeEvent = RequestDirector.GetEvent(activeEvent);

        //get most recent evaluation from each evaluator
        currentEvals = RequestDirector.GetCurrentEventData(activeEvent);

        Button btn;

        foreach (Evaluator ev in activeEvent.Evaluators)
        {
            TableRow tRow = new TableRow();
            TableCell tCell = new TableCell();

            //name
            if (ev.EvaluatorID.ToString().Equals("voter"))
                tCell.Text = ev.EvaluatorID.ToString();
            else
                tCell.Text = ev.Name;
   
            tRow.Cells.Add(tCell);

            //first Rating
            tCell = new TableCell();
            tCell.Text = (ev.EvaluatorEvaluations.First().TimeStamp.ToLocalTime() - activeEvent.EventStart).ToString();
            tRow.Cells.Add(tCell);
            
            //last rating
            tCell = new TableCell();
            tCell.Text = (ev.EvaluatorEvaluations.Last().TimeStamp.ToLocalTime() - activeEvent.EventStart).ToString();
            tRow.Cells.Add(tCell);

            //avg rating
            tCell = new TableCell();
            tCell.Text = (ev.EvaluatorEvaluations.Average(x => x.Rating)).ToString("#.##");     
            tRow.Cells.Add(tCell);

            //delete button
            tCell = new TableCell();
            btn = new Button();
            btn.Text = "Remove";
            btn.ID = String.Format("Remove{0}", ev.EvaluatorID.ToString());
            btn.Click += new EventHandler(RemoveEvaluator_Click);
            btn.OnClientClick = "return confirm('Are you sure you want to remove this evaluator?');";
            btn.CssClass = "btn btn-light";
            tCell.Controls.Add(btn);
           

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

    protected void RemoveEvaluator_Click(object sender, EventArgs e)
    {
        CSS RequestDirector = new CSS();
        string EvaluatorID = ((Button)sender).ID;

        EvaluatorID = EvaluatorID.Replace("Remove", "");

        Evaluator ev = new Evaluator();
        ev.EvaluatorID = Convert.ToInt32(EvaluatorID);

        bool confirmation = RequestDirector.DeleteEvaluatorEventData(((Event)Session["Event"]), ev);
    }

    protected void RepeatBtn_Click(object sender, EventArgs e)
    {
        CSS RequestDirector = new CSS();
        Event currentEvent = new Event();
        currentEvent.EventID = ((Event)Session["Event"]).EventID;

        currentEvent = RequestDirector.GetEvent(currentEvent);
        currentEvent.CustomQuestions = RequestDirector.GetQuestions(currentEvent.EventID);

        Event repeatEvent = new Event();

        //create key for event
        string EventKey;
        EventKey = RequestDirector.GenKey(3);

        //default value for event start and end times
        DateTime defaultTime = Convert.ToDateTime("1800-01-01 12:00:00 PM");

        //get facilitator info and event info input
        CustomPrincipal cp = HttpContext.Current.User as CustomPrincipal;
        repeatEvent.EventKey = EventKey;
        repeatEvent.FacilitatorID = Convert.ToInt32(cp.Identity.Name);
        repeatEvent.Performer = currentEvent.Performer;
        repeatEvent.Location = currentEvent.Location;
        repeatEvent.Description = currentEvent.Description;
        repeatEvent.Date = DateTime.Today;
        repeatEvent.OpenMsg = currentEvent.OpenMsg;
        repeatEvent.CloseMsg = currentEvent.CloseMsg;
        repeatEvent.VotingCrit = currentEvent.VotingCrit;

        //attept event creation
        Event newEvent = RequestDirector.CreateEvent(repeatEvent);

        //if successful, add event to session and redirect to view event
        if (newEvent.EventID != -1)
        {
            Question q;

            foreach (Question que in currentEvent.CustomQuestions)
            {
                q = new Question();
                q.EventID = newEvent.EventID;
                q.QuestionText = que.QuestionText;

                RequestDirector.AddQuestion(q);
            }

            Session["Event"] = newEvent;
            Response.Redirect("ViewEvent.aspx");
        }
    }

    protected void RBLmath_SelectedIndexChanged(object sender, EventArgs e)
    {
        BuildTable();
        BuildCharts(RBLmath.SelectedValue);
    }
}