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
            DateTime defaultTime = Convert.ToDateTime("01-01-1800 12:00:00");

            Event ActiveEvent = new Event();
            ActiveEvent.EventID = ((Event)Session["Event"]).EventID;
            ActiveEvent = RequestDirector.GetEvent(ActiveEvent);

            if (ActiveEvent.EventStart != defaultTime)
            {

            }

        }
    }

    protected void ButtonUp_Click(object sender, EventArgs e)
    {
        CSS RequestDirector = new CSS();
        DateTime defaultTime = Convert.ToDateTime("01-01-1800 12:00:00");

        Event ActiveEvent = new Event();
        ActiveEvent.EventID = ((Event)Session["Event"]).EventID;
        ActiveEvent = RequestDirector.GetEvent(ActiveEvent);

        if (ActiveEvent.EventStart != defaultTime)
        {
            if (ActiveEvent.EventEnd == defaultTime)
            {
                int Rating = int.Parse(LabelRating.Text);
                Rating = (Rating + 1 > 10) ? Rating = 10 : Rating + 1;

                LabelRating.Text = Rating.ToString();

                Evaluation eval = new Evaluation(Rating, ((Evaluator)Session["Evaluator"]).EvaluatorID, ((Event)Session["Event"]).EventID);  

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
        DateTime defaultTime = Convert.ToDateTime("01-01-1800 12:00:00");

        Event ActiveEvent = new Event();
        ActiveEvent.EventID = ((Event)Session["Event"]).EventID;
        ActiveEvent = RequestDirector.GetEvent(ActiveEvent);

        if (ActiveEvent.EventStart != defaultTime)
        {
            if (ActiveEvent.EventEnd == defaultTime)
            {
                int Rating = int.Parse(LabelRating.Text);
                Rating = (Rating - 1 < 1) ? Rating = 1 : Rating - 1;

                LabelRating.Text = Rating.ToString();

                Evaluation eval = new Evaluation(Rating, ((Evaluator)Session["Evaluator"]).EvaluatorID, ((Event)Session["Event"]).EventID);

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


}