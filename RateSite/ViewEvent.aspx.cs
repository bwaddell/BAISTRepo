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

            theEvent.CustomQuestions = Director.GetQuestions(theEvent.EventID);

            if (theEvent.EventKey == "AAAA")
            {
                upTable.Visible = false;
                PanelButtons.Visible = false;
                PanelPreLabel.Visible = true;
                PanelPostLabel.Visible = false;
                tbEventID.Text = "Generate Key";
                tbPerformer.ReadOnly = false;
                tbDesc.ReadOnly = false;
                tbLocation.ReadOnly = false;
                tbOpen.ReadOnly = false;
                tbClose.ReadOnly = false;
                allCritLB.Enabled = true;
                allQsLB.Enabled = true;
                newQTB.Visible = true;
                critTxt.Visible = true;
                AddCritBTN.Enabled = true;
                AddCritBTN.Visible = true;
                RemoveCritBRN.Enabled = true;
                RemoveCritBRN.Visible = true;
                AddQBTN.Enabled = true;
                AddQBTN.Visible = true;
                RemoveQBTN.Enabled = true;
                RemoveQBTN.Visible = true;
                TimerForTableRefresh.Enabled = false;
                PanelCharts.Visible = false;
            }
            else 
            {
                if (theEvent.EventKey == "ZZZZ")
                {
                    tbEventID.Text = "Key Expired";
                }
                else
                {
                    tbEventID.Text = theEvent.EventKey;
                }
                upTable.Visible = true;
                PanelButtons.Visible = true;
                PanelPreLabel.Visible = false;
                PanelPostLabel.Visible = true;
                tbPerformer.ReadOnly = true;
                tbDesc.ReadOnly = true;
                tbLocation.ReadOnly = true;
                tbOpen.ReadOnly = true;
                tbClose.ReadOnly = true;
                allCritLB.Enabled = false;
                allQsLB.Enabled = false;
                newQTB.Visible = false;
                critTxt.Visible = false;
                AddCritBTN.Enabled = false;
                AddCritBTN.Visible = false;
                RemoveCritBRN.Enabled = false;
                RemoveCritBRN.Visible = false;
                AddQBTN.Enabled = false;
                AddQBTN.Visible = false;
                RemoveQBTN.Enabled = false;
                RemoveQBTN.Visible = false;
                GenKeyBtn.Enabled = false;
                GenKeyBtn.Visible = false;
                UpdateBtn.Enabled = false;
                UpdateBtn.Visible = false;
            }

            

            tbPerformer.Text = theEvent.Performer;
            tbLocation.Text = theEvent.Location;
            tbDate.Text = theEvent.Date.ToLongDateString();
            tbDesc.Text = theEvent.Description;
            tbOpen.Text = theEvent.OpenMsg;
            tbClose.Text = theEvent.CloseMsg;
            
            foreach (Question q in theEvent.CustomQuestions)
            {
                allQsLB.Items.Add(new ListItem(q.QuestionText,q.QuestionText));
            }

            string[] crits = theEvent.VotingCrit.Split('|');

            DropDownListCrit.Items.Add(new ListItem("All Evaluations", "All Evaluations"));

            foreach (string s in crits)
            {             
                if (s.Length > 0)
                {
                    allCritLB.Items.Add(new ListItem(s, s));
                    DropDownListCrit.Items.Add(new ListItem(s, s));
                }
            }

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
                BuildTable();
                PanelCharts.Visible = true;

                string chartCrit;
                string chartMath;

                if (Session["criteria"] == null)
                    chartCrit = "All Evaluations";
                else
                {
                    chartCrit = Session["criteria"].ToString();
                    DropDownListCrit.SelectedValue = chartCrit;
                }

                if (Session["math"] == null)
                    chartMath = "Mean";
                else
                {
                    chartMath = Session["math"].ToString();
                    DropDownListMath.SelectedValue = chartMath;
                }


                BuildCharts(chartCrit, chartMath);

            }
            else
            {
                TimerForTableRefresh.Enabled = true;
                //if event has not begun
                if (theEvent.EventStart == defaultTime)
                {
                    tbStart.Text = "--:--:--";
                    tbEnd.Text = "--:--:--";
                    ButtonStart.Enabled = true;
                    ButtonEnd.Enabled = false;
                    Export.Enabled = false;
                    RepeatBtn.Enabled = false;
                    //TimerForTableRefresh.Enabled = true;
                }
                //if event is still active
                else
                {
                    tbStart.Text = theEvent.EventStart.ToLocalTime().ToLongTimeString();
                    tbEnd.Text = "--:--:--";
                    ButtonStart.Enabled = false;
                    //TimerForTableRefresh.Enabled = true;
                    Export.Enabled = false;
                    ButtonEnd.Enabled = true;
                    RepeatBtn.Enabled = false;
                }
            }
        }
        else
        {
            //BuildTable();
            //BuildCharts();
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
        DateTime addEval;
        DateTime defaultTime = Convert.ToDateTime("1800-01-01 12:00:00 PM");

        //Set the Event.ID of our empty event, and use said event to pull event information from DB
        theEvent.EventID = ((Event)Session["Event"]).EventID;
        theEvent = Director.GetEvent(theEvent);
        theEvent.Evaluators = Director.GetEvaluatorsForEvent(theEvent.EventID);

        foreach (Evaluator eva in theEvent.Evaluators)
        {
            eva.EvaluatorEvaluations.RemoveAll(x => x.Rating == 999);
        }

        List<Question> questions = new List<Question>();
        questions = Director.GetQuestions(theEvent.EventID);

        List<Question> answers = new List<Question>();
        Question que;

        foreach (Question q in questions)
        {
            foreach (Evaluator ev in theEvent.Evaluators)
            {
                que = new Question();

                que.QID = q.QID;
                que.EvaluatorID = ev.EvaluatorID;
                que = Director.GetResponse(que);

                ev.Responses.Add(que);
            }
        }

        Facilitator = Director.GetFacilitator(1);

        //Creates the first section of data in the CSV, regarding the Facilitators info. Data is formatted in two lines, the first line being the data description, the second line being the respective data.
        csvcontent.AppendLine("Event Creator,Organization");
        csvcontent.AppendLine(Facilitator.FirstName + " " + Facilitator.LastName + "," + Facilitator.Organization);
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
        string tsLine = "ID #,Name,";

        foreach (Question q in questions)
        {
            tsLine += q.QuestionText + ",";
        }

        tsLine += "Voting Criteria,Time of First Rating, Time of Last Rating, TimeStamp (HH:MM:SS.mmm):,";

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
        string insert;

        foreach (Evaluator eval in theEvent.Evaluators)
        {
            insert = eval.EvaluatorID.ToString() + "," + eval.Name + "," ;

            //add q answers
            foreach (Question r in eval.Responses)
            {
                insert += r.ResponseText + ",";
            }

            insert += eval.Criteria + ",";

            if (eval.EvaluatorEvaluations.Count > 0)
            {
                insert += (eval.EvaluatorEvaluations.First().TimeStamp.ToLocalTime() - theEvent.EventStart).ToString() + ",";
                insert += (eval.EvaluatorEvaluations.Last().TimeStamp.ToLocalTime() - theEvent.EventStart).ToString() + ",";
            }
            else
            {
                insert += "--:--:--,--:--:--,,";
            }

            

            for (DateTime i = eventStart; i <= eventEnd; i = i.AddSeconds(secsBetweenPoints))
            {
                if (eval.EvaluatorEvaluations.Count > 0)
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
        //Event updateMe = (Event)Session["Event"];
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
        updateMe.EventKey = "ZZZZ";
        confirmation = Manager.UpdateEventStatus(updateMe);

        //generate table and charts 
        if (confirmation)
        {
            //Reload page
            Session["criteria"] = "All Evaluations";
            Session["math"] = "Mean";
            Response.Redirect("ViewEvent.aspx", true);
        }
    }


    public void BuildCharts(string criteria, string math)
    {
        CSS Director = new CSS();


        //get event info
        Event theEvent = new Event();
        theEvent.EventID = ((Event)Session["Event"]).EventID;
        theEvent = Director.GetEvent(theEvent);
        theEvent.Evaluators = Director.GetEvaluatorsForEvent(theEvent.EventID);
        DateTime defaultTime = Convert.ToDateTime("1800-01-01 12:00:00 PM");


        if (criteria != "All Evaluations")
            theEvent.Evaluators.RemoveAll(o => o.Criteria != criteria);

        foreach (Evaluator ev in theEvent.Evaluators)
        {
            ev.EvaluatorEvaluations.RemoveAll(x => x.Rating == 999);
        }

        //if event has evaluator data, construct the chart
        if (theEvent.Evaluators.Count > 0)
        {
            Highcharts chart = Director.CreateChart(theEvent);
            ltrChart.Text = chart.ToHtmlString();

            //generate chart with mean/mode/median
            Highcharts mathChart = Director.MakeMathChart(theEvent, math);
            meanChart.Text = mathChart.ToHtmlString();

        }
    }

    //build table with evaluator info
    public void BuildTable()
    { 
        DateTime defaultTime = Convert.ToDateTime("1800-01-01 12:00:00 PM");

        //lbUpdateTime.Text = DateTime.Now.ToLocalTime().ToString();
        CSS RequestDirector = new CSS();

        //get event evaluation data
        List<Evaluation> currentEvals = new List<Evaluation>();
        Event activeEvent = new Event();
        activeEvent.EventID = ((Event)Session["Event"]).EventID;

        activeEvent = RequestDirector.GetEvent(activeEvent);
        activeEvent.Evaluators = RequestDirector.GetEvaluatorsForEvent(activeEvent.EventID);

        //get most recent evaluation from each evaluator
        currentEvals = RequestDirector.GetCurrentEventData(activeEvent);

        Button btn;

        foreach (Evaluator ev in activeEvent.Evaluators)
        {
            

            TableRow tRow = new TableRow();
            TableCell tCell = new TableCell();

            //name
            if (ev.Name.ToString().Equals("Default"))
                tCell.Text = "ID:" + ev.EvaluatorID.ToString();
            else
                tCell.Text = ev.Name;
   
            tRow.Cells.Add(tCell);

            //what criteria user is rating on
            tCell = new TableCell();
            tCell.Text = ev.Criteria;
            tRow.Cells.Add(tCell);

            if (ev.EvaluatorEvaluations.Last().Rating == 999)
            {
                //first Rating time
                tCell = new TableCell();
                tCell.Text = "--:--:--";
                tRow.Cells.Add(tCell);

                //last rating time
                tCell = new TableCell();
                tCell.Text = "--:--:--";
                tRow.Cells.Add(tCell);

                //last rating
                tCell = new TableCell();
                tCell.Text = "-";
                tRow.Cells.Add(tCell);
            }
            else
            {
                ev.EvaluatorEvaluations.RemoveAll(x => x.Rating == 999);

                //first Rating time
                tCell = new TableCell();
                tCell.Text = (ev.EvaluatorEvaluations.First().TimeStamp.ToLocalTime() - activeEvent.EventStart).ToString();
                tRow.Cells.Add(tCell);

                //last rating time
                tCell = new TableCell();
                tCell.Text = (ev.EvaluatorEvaluations.Last().TimeStamp.ToLocalTime() - activeEvent.EventStart).ToString();
                tRow.Cells.Add(tCell);

                //last rating
                tCell = new TableCell();
                tCell.Text = ev.EvaluatorEvaluations.Last().Rating.ToString();
                tRow.Cells.Add(tCell);
            }

            

            //delete button
            tCell = new TableCell();
            btn = new Button();
            btn.Text = "Remove";
            btn.ID = String.Format("Remove{0}", ev.EvaluatorID.ToString());
            btn.Click += new EventHandler(RemoveEvaluator_Click);
            btn.OnClientClick = "return confirm('Are you sure you want to remove this evaluator?');";
            btn.CssClass = "btn btn-light";
            tCell.Controls.Add(btn);
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

                lbTotalEvalsNum.Text = activeEvent.Evaluators.Count.ToString();

                //create list of all evals for event then average
                foreach (Evaluator ev in activeEvent.Evaluators)
                {
                    ev.EvaluatorEvaluations.RemoveAll(x => x.Rating == 999);
                    allEvaluations.AddRange(ev.EvaluatorEvaluations);
                }

                if (allEvaluations.Count > 0)
                {
                    totalAverage = allEvaluations.Average(o => o.Rating);
                    Ratinglbl.Text = totalAverage.ToString("#.##");
                }


                
            }
        }
        else
        {
            if (currentEvals.Count > 0)
            {
                lbTotalEvalsNum.Text = activeEvent.Evaluators.Count.ToString();
                currentEvals.RemoveAll(x => x.Rating == 999);

                if (currentEvals.Count > 0)
                {
                    Ratinglbl.Text = currentEvals.Average(x => (double)x.Rating).ToString("#.##");
                    
                }

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

        BuildTable();
    }

    protected void RepeatBtn_Click(object sender, EventArgs e)
    {
        CSS RequestDirector = new CSS();
        Event currentEvent = new Event();
        currentEvent.EventID = ((Event)Session["Event"]).EventID;

        currentEvent = RequestDirector.GetEvent(currentEvent);
        currentEvent.CustomQuestions = RequestDirector.GetQuestions(currentEvent.EventID);

        Event repeatEvent = new Event();

        //default value for event start and end times
        DateTime defaultTime = Convert.ToDateTime("1800-01-01 12:00:00 PM");

        //get facilitator info and event info input
        CustomPrincipal cp = HttpContext.Current.User as CustomPrincipal;
        repeatEvent.EventKey = "AAAA";
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

    protected void RemoveQBTN_Click(object sender, EventArgs e)
    {
        allQsLB.Items.Remove(allQsLB.SelectedItem);
    }

    protected void AddQBTN_Click(object sender, EventArgs e)
    {
        ListItem li = new ListItem();

        li.Text = newQTB.Text;
        li.Value = (allQsLB.Items.Count + 1).ToString();

        allQsLB.Items.Add(li);

        newQTB.Text = "";
    }

    protected void AddCritBTN_Click(object sender, EventArgs e)
    {
        ListItem li = new ListItem();

        li.Text = critTxt.Text;
        li.Value = (allCritLB.Items.Count + 1).ToString();

        allCritLB.Items.Add(li);

        critTxt.Text = "";
    }

    protected void RemoveCritBRN_Click(object sender, EventArgs e)
    {
        allCritLB.Items.Remove(allCritLB.SelectedItem);
    }

    protected void GenKeyBtn_Click(object sender, EventArgs e)
    {
        CSS RequestDirector = new CSS();
        Event currentEvent = new Event();
        currentEvent.EventID = ((Event)Session["Event"]).EventID;
        bool success;

        currentEvent = RequestDirector.GetEvent(currentEvent);

        currentEvent.EventKey = RequestDirector.GenKey(3);
        currentEvent.Date = DateTime.Today;

        success = RequestDirector.UpdateEventInfo(currentEvent);

        if (success)
        {
            upTable.Visible = true;
            PanelButtons.Visible = true;
            PanelPreLabel.Visible = false;
            PanelPostLabel.Visible = true;
            tbEventID.Text = currentEvent.EventKey;
            tbPerformer.ReadOnly = true;
            tbDesc.ReadOnly = true;
            tbLocation.ReadOnly = true;
            tbOpen.ReadOnly = true;
            tbClose.ReadOnly = true;
            allCritLB.Enabled = false;
            allQsLB.Enabled = false;
            newQTB.Visible = false;
            critTxt.Visible = false;
            AddCritBTN.Enabled = false;
            AddCritBTN.Visible = false;
            RemoveCritBRN.Enabled = false;
            RemoveCritBRN.Visible = false;
            AddQBTN.Enabled = false;
            AddQBTN.Visible = false;
            RemoveQBTN.Enabled = false;
            RemoveQBTN.Visible = false;
            GenKeyBtn.Enabled = false;
            GenKeyBtn.Visible = false;
            UpdateBtn.Enabled = false;
            UpdateBtn.Visible = false;
            TimerForTableRefresh.Enabled = true;
        }
        else
        {
            tbEventID.Text = "Error Generating Key";
        }
    }

    protected void UpdateBtn_Click(object sender, EventArgs e)
    {
        CSS RequestDirector = new CSS();
        Event currentEvent = new Event();
        currentEvent.EventID = ((Event)Session["Event"]).EventID;
        bool success;

        currentEvent = RequestDirector.GetEvent(currentEvent);

        currentEvent.Performer = tbPerformer.Text;
        currentEvent.Description = tbDesc.Text;
        currentEvent.Location = tbLocation.Text;
        currentEvent.OpenMsg = tbOpen.Text;
        currentEvent.CloseMsg = tbClose.Text;
        currentEvent.Date = DateTime.Today;

        string crit = "";

        if (allCritLB.Items.Count > 0)
        {
            foreach (ListItem i in allCritLB.Items)
            {
                crit += (i.Text + '|');
            }
            crit.TrimEnd('|');
        }
        else
        {
            crit = "Overall Quality";
        }

        currentEvent.VotingCrit = crit;

        success = RequestDirector.UpdateEventInfo(currentEvent);

        if (success)
        {
            List<Question> questions = new List<Question>();
            questions = RequestDirector.GetQuestions(currentEvent.EventID);

            foreach (Question q in questions)
            {
                RequestDirector.DeleteQuestion(q);
            }

            Question qu;

            foreach (ListItem li in allQsLB.Items)
            {
                qu = new Question();
                qu.EventID = currentEvent.EventID;
                qu.QuestionText = li.Text;

                RequestDirector.AddQuestion(qu);
            }

        }
    }

    protected void ChartCritBtn_Click(object sender, EventArgs e)
    {
        string criteria = DropDownListCrit.SelectedValue;

        Session["criteria"] = criteria;

        Response.Redirect("ViewEvent.aspx");
    }

    protected void ChartMathBtn_Click(object sender, EventArgs e)
    {
        string math = DropDownListMath.SelectedValue;

        Session["math"] = math;

        Response.Redirect("ViewEvent.aspx");
    }
}