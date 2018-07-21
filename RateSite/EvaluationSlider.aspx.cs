using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Windows.Forms;

public partial class EvaluationSlider : System.Web.UI.Page
{
    private int _ypos;
    private bool _dragging;
    
    


    protected void Page_Load(object sender, EventArgs e)
    {
        TimerVoteChange.Enabled = true;

        string sheight = HttpContext.Current.Request.Params["height"];

        if (sheight != null) heightlbl.Text = sheight;
        //if (!IsPostBack)
        //{
        //    CSS RequestDirector = new CSS();
        //    DateTime defaultTime = Convert.ToDateTime("1800-01-01 12:00:00 PM");

        //    Event ActiveEvent = new Event();
        //    ActiveEvent.EventID = ((Event)Session["Event"]).EventID;
        //    ActiveEvent = RequestDirector.GetEvent(ActiveEvent);


        //}
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

            }
        }
        else
        {
            Response.Redirect("Default.aspx");
        }


    }

    protected void TimerVoteChange_Tick(object sender, EventArgs e)
    {
        _ypos = Cursor.Position.Y;
        poslbl.Text = _ypos.ToString();

        

        System.Web.UI.HtmlControls.HtmlGenericControl masterBody = (System.Web.UI.HtmlControls.HtmlGenericControl)Master.FindControl("masterBody");
        masterBody.Attributes.Add("style", "background-color: black");
    }

    protected void TimerCheckEventStatus_Tick(object sender, EventArgs e)
    {

    }
}