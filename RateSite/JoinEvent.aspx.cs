using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PreEvent : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CSS RequestDirector = new CSS();
            DateTime defaultTime = Convert.ToDateTime("1/1/1800 12:00:00 PM");

            Event ActiveEvent = new Event();
            ActiveEvent.EventID = ((Event)Session["Event"]).EventID;

            ActiveEvent = RequestDirector.GetEvent(ActiveEvent);

            ActiveEvent.CustomQuestions = RequestDirector.GetQuestions(ActiveEvent.EventID);

            tbLocation.Text = ActiveEvent.Location;
            tbDescription.Text = ActiveEvent.Description;
            tbPerformer.Text = ActiveEvent.Performer;
            
            if (ActiveEvent.OpenMsg.Length > 0)
            {
                PanelOpenMsg.Visible = true;
                TBOpenMsg.Text = ActiveEvent.OpenMsg;
            }

            if (ActiveEvent.VotingCrit.Length > 0)
            {
                PanelCrit.Visible = true;

                string[] crits = ActiveEvent.VotingCrit.Split('|');

                ListItem li;

                foreach (string s in crits)
                {
                    if (s.Length > 0)
                    {
                        li = new ListItem(s, s);
                        DDLCrit.Items.Add(li);
                    }
                }
            }

            if (ActiveEvent.CustomQuestions.Count > 0)
            {
                PanelQuestions.Visible = true;

                Label tbq;
                TextBox tba;

                foreach (Question q in ActiveEvent.CustomQuestions)
                {
                    tbq = new Label();
                    tbq.Text = q.QuestionText;
                    tbq.Font.Bold = true;

                    PanelQuestions.Controls.Add(tbq);

                    tba = new TextBox();
                    tba.ID = string.Format("tb{0}", q.QID);
                    tba.CssClass = "form-control";
                    PanelQuestions.Controls.Add(tba);

                }
            }
        }
    }

    protected void JoinBTN_Click(object sender, EventArgs e)
    {
        //only validate if user has agreed to terms
        if (consentCheck.Checked)
        {
            DateTime defaultTime = Convert.ToDateTime("01-01-1800 12:00:00");

            CSS RequestDirector = new CSS();

            //check all open events to match event Key
            //return Event ID

            //get event info for key input
            Event currentEvent = new Event();
            currentEvent.EventID = ((Event)Session["Event"]).EventID;
            currentEvent = RequestDirector.GetEvent(currentEvent);
            currentEvent.CustomQuestions = RequestDirector.GetQuestions(currentEvent.EventID);

            //check if event key exists
            if (currentEvent.EventKey != default(string))
            {
                //if event end time is not default value, event is over.  Can not join
                if (currentEvent.EventKey != "ZZZZ")
                {
                    //create new evaluator
                    Evaluator activeEvaluator = new Evaluator();

                    //get name if supplied
                    if (tbName.Text == "")
                    {
                        activeEvaluator.Name = "Default";
                    }
                    else
                    {
                        activeEvaluator.Name = tbName.Text;
                    }

                    //get criteria if selected
                    if (DDLCrit.Items.Count > 0)
                        activeEvaluator.Criteria = DDLCrit.SelectedValue;
                    else
                        activeEvaluator.Criteria = "Overall Quality";

                    activeEvaluator = RequestDirector.CreateEvaluator(activeEvaluator);

                    foreach (Question q in currentEvent.CustomQuestions)
                    {
                        q.EvaluatorID = activeEvaluator.EvaluatorID;

                        string responseTBID = string.Format("tb{0}", q.QID);

                        TextBox tb = (TextBox)(this.FindControl(responseTBID));

                        q.ResponseText = tb.Text;

                        RequestDirector.AddResponse(q);

                        //foreach (Control c in PanelQuestions.Controls)
                        //{
                        //    if (c.ID == responseTBID && c is TextBox)
                        //    {
                        //        q.ResponseText = ((TextBox)c).Text;

                        //        RequestDirector.AddResponse(q);
                        //    }
                        //}
                    }

                    //redirect to evaluate page if evaluator is created
                    if (activeEvaluator.EvaluatorID != default(int))
                    {
                        //create consent cookie if there isn't one
                        var consentCookie = Request.Cookies["ConsentCookie"];

                        if (consentCookie == null)
                        {
                            HttpCookie newConsent = new HttpCookie("ConsentCookie", "true");

                            //set cookie to expire in 100 days
                            newConsent.Expires = DateTime.UtcNow.AddDays(100);

                            Response.Cookies.Add(newConsent);
                        }

                        Session["Event"] = currentEvent;
                        Session["Evaluator"] = activeEvaluator;
                        Response.Redirect("EvaluateEvent.aspx");
                    }
                    else
                    {

                    }
                }
                else
                {

                }
            }
            else
            {

            }
        }
        else
        {
            consentCheck.ForeColor = System.Drawing.Color.Red;
        }
    }
}