using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class EvaluateEvent : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            CSS RequestDirector = new CSS();
            DateTime defaultTime = Convert.ToDateTime("1800-01-01 12:00:00 PM");

            Event ActiveEvent = new Event();
            ActiveEvent.EventID = ((Event)Session["Event"]).EventID;
            ActiveEvent = RequestDirector.GetEvent(ActiveEvent);


        }
    }

    protected void ButtonUp_Click(object sender, EventArgs e)
    {
        CSS RequestDirector = new CSS();
        DateTime defaultTime = Convert.ToDateTime("1800-01-01 12:00:00 PM");  //default date for event start and end times

        //get event info
        Event ActiveEvent = new Event();
        ActiveEvent.EventID = ((Event)Session["Event"]).EventID;
        ActiveEvent = RequestDirector.GetEvent(ActiveEvent);

        //if event has started and has not ended
        if (ActiveEvent.EventStart != defaultTime)
        {
            if (ActiveEvent.EventEnd == defaultTime)
            {
                //limit rating between 1-10
                int Rating = int.Parse(LabelRating.Text);
                Rating = (Rating + 1 > 10) ? Rating = 10 : Rating + 1;

                LabelRating.Text = Rating.ToString();

                //create evaluation object and send to DB
                Evaluation eval = new Evaluation(DateTime.Now.ToUniversalTime(), Rating, ((Evaluator)Session["Evaluator"]).EvaluatorID, ((Event)Session["Event"]).EventID);

                bool Success = RequestDirector.AddEvaluation(eval);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('The Event has Ended')", true);
            }
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('The Event has not yet begun')", true);
        }

    }

    protected void ButtonDown_Click(object sender, EventArgs e)
    {
        CSS RequestDirector = new CSS();
        DateTime defaultTime = Convert.ToDateTime("1800-01-01 12:00:00 PM");  //default date for event start and end times

        //get event info
        Event ActiveEvent = new Event();
        ActiveEvent.EventID = ((Event)Session["Event"]).EventID;
        ActiveEvent = RequestDirector.GetEvent(ActiveEvent);

        //if event has started and not ended
        if (ActiveEvent.EventStart != defaultTime)
        {
            if (ActiveEvent.EventEnd == defaultTime)
            {
                //limit rating to 1-10
                int Rating = int.Parse(LabelRating.Text);
                Rating = (Rating - 1 < 1) ? Rating = 1 : Rating - 1;

                LabelRating.Text = Rating.ToString();

                //create evaluation and send to DB
                Evaluation eval = new Evaluation(DateTime.Now.ToUniversalTime(),Rating, ((Evaluator)Session["Evaluator"]).EvaluatorID, ((Event)Session["Event"]).EventID);

                bool Success = RequestDirector.AddEvaluation(eval);
            }
            else
            {
                Evaluator eval = new Evaluator();
                eval.EvaluatorID = ((Evaluator)Session["Evaluator"]).EvaluatorID;

                FinishEvent(ActiveEvent, eval);
            }
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('The Event has not yet begun')", true);
        }
    }

    public void FinishEvent(Event currentEvent, Evaluator activeEvaluator)
    {
        DateTime defaultTime = Convert.ToDateTime("01-01-1800 12:00:00");

        CSS RequestDirector = new CSS();

        //check all open events to match event Key
        //return Event ID

        //get event info for key input

        Session["Event"] = currentEvent;
        Session["Evaluator"] = activeEvaluator;
        Response.Redirect("PostEvent.aspx");

    }



    protected void LeaveBtn_Click(object sender, EventArgs e)
    {
        CSS RequestDirector = new CSS();
        Evaluator eval = new Evaluator();
        eval.EvaluatorID = ((Evaluator)Session["Evaluator"]).EvaluatorID;
        DateTime defaultTime = Convert.ToDateTime("1800-01-01 12:00:00 PM");
        Event eve = new Event();
        eve.EventID = ((Event)Session["Event"]).EventID;
        eve = RequestDirector.GetEvent(eve);

        if (eve.EventEnd == defaultTime)
        {
            bool success = RequestDirector.DeleteEvaluatorEventData(eve, eval);

            if (success)
            {
                Response.Redirect("Default.aspx");
            }
        }
        else
        {
            Response.Redirect("Default.aspx");
        }


    }

    protected void timerEnd_Tick(object sender, EventArgs e)
    {
        CSS RequestDirector = new CSS();


        DateTime defaultTime = Convert.ToDateTime("1800-01-01 12:00:00 PM");
        Event eve = new Event();
        eve.EventID = ((Event)Session["Event"]).EventID;
        eve = RequestDirector.GetEvent(eve);

        if (eve.EventEnd != defaultTime)
        {
            Evaluator eval = new Evaluator();
            eval.EvaluatorID = ((Evaluator)Session["Evaluator"]).EvaluatorID;

            FinishEvent(eve, eval);
        }
    }
}