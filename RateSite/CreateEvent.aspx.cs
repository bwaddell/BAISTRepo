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
        CSS requester = new CSS();
        Event cEvent = new Event();
        bool success;

        //create key for event
        string EventKey;
        EventKey = requester.CreateEventKey(3);

        //default value for event start and end times
        DateTime defaultTime = Convert.ToDateTime("1/1/1800 12:00:00 PM");

        //get facilitator info and event info input
        CustomPrincipal cp = HttpContext.Current.User as CustomPrincipal;
        cEvent.EventID = EventKey;
        cEvent.FacilitatorID = Convert.ToInt32(cp.Identity.Name);
        cEvent.Performer = tbPerformer.Text;
        cEvent.Location = tbLocation.Text;
        cEvent.Description = tbNatureOfPerformance.Text;
        cEvent.Date = Convert.ToDateTime(tbEventDate.Text);

        //attept event creation
        success = requester.CreateEvent(cEvent);

        //if successful, add event to session and redirect to view event
        if (success)
        {
            Session["Event"] = cEvent;
            Response.Redirect("ViewEvent.aspx");
        }
    }
}