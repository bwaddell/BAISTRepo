using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class HomePage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (HttpContext.Current.User.Identity.IsAuthenticated)
            CreateButton.Visible = true;
        else
            CreateButton.Visible = false;
    }

    protected void CreateButton_Click(object sender, EventArgs e)
    {
        Server.Transfer("CreateEvent.aspx");
    }

    protected void JoinButton_Click(object sender, EventArgs e)
    {
        //check if key is right length
        if(tbEventKey.Text.Length == 4)
        {
            statuslbl.Text = "";

            //default value for event start/end time
            DateTime defaultTime = Convert.ToDateTime("01-01-1800 12:00:00");
        
            CSS RequestDirector = new CSS();

            //get event info for key input
            Event currentEvent = new Event();
            currentEvent.EventID = tbEventKey.Text.ToUpper();
            currentEvent = RequestDirector.GetEvent(currentEvent);

            //check if event key exists
            if (currentEvent.EventID != default(string))
            {
                //if event end time is not default value, event is over.  Can not join
                if (currentEvent.EventEnd == defaultTime)
                {
                    //create new evaluator
                    Evaluator activeEvaluator = new Evaluator();                 
                    activeEvaluator = RequestDirector.CreateEvaluator();

                    //redirect to evaluate page if evaluator is created
                    if (activeEvaluator.EvaluatorID != default(int))
                    {
                        Session["Event"] = currentEvent;
                        Session["Evaluator"] = activeEvaluator;
                        Server.Transfer("EvaluateEvent.aspx");
                    }
                    else
                    {
                        tbEventKey.Text = "";
                        statuslbl.Text = "Error Joining Event";
                    }                             
                }
                else
                {
                    tbEventKey.Text = "";
                    statuslbl.Text = "Event has ended";
                }
            }
            else
            {
                tbEventKey.Text = "";
                statuslbl.Text = "Event Key Does Not Exist";
            }
        }
        else
        {
            tbEventKey.Text = "";
            statuslbl.Text = "Invalid Event ID";
        }
    }
}