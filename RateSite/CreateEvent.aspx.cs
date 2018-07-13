using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CreateEvent : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CustomPrincipal cp = HttpContext.Current.User as CustomPrincipal;
            CSS requester = new CSS();

            //get facilitator info
            Facilitator fac = new Facilitator();

            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                fac = requester.GetFacilitator(Convert.ToInt32(cp.Identity.Name));
                tbEventDate.Text = DateTime.Today.ToString();
            }

        }       
    }

    protected void btnCreateEvent_Click(object sender, EventArgs e)
    {
        CSS RequestDirector = new CSS();
        Event cEvent = new Event();
        bool success;

        //create key for event
        string EventKey;
        EventKey = RequestDirector.GenKey(3);


        //default value for event start and end times
        DateTime defaultTime = Convert.ToDateTime("1800-01-01 12:00:00 PM");

        //get facilitator info and event info input
        CustomPrincipal cp = HttpContext.Current.User as CustomPrincipal;
        cEvent.EventKey = EventKey;
        cEvent.FacilitatorID = Convert.ToInt32(cp.Identity.Name);
        cEvent.Performer = tbPerformer.Text;
        cEvent.Location = tbLocation.Text;
        cEvent.Description = tbNatureOfPerformance.Text;
        cEvent.Date = Convert.ToDateTime(tbEventDate.Text);
        cEvent.OpenMsg = OpenTxt.Text;
        cEvent.CloseMsg = CloseTxt.Text;
        
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

        cEvent.VotingCrit = crit;

        //attept event creation
        Event newEvent = RequestDirector.CreateEvent(cEvent);
        newEvent.Evaluators = new List<Evaluator>();

        //if successful, add event to session and redirect to view event
        if (newEvent.EventID != -1)
        {
            Question q;

            foreach (ListItem li in allQsLB.Items)
            {
                q = new Question();
                q.EventID = newEvent.EventID;
                q.QuestionText = li.Text;

                RequestDirector.AddQuestion(q);
            }

            

            Session["Event"] = newEvent;
            Response.Redirect("ViewEvent.aspx");
        }
        else
        {
            lbstatus.Text = "There was an Error creating your event";
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
}