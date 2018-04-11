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
            Facilitator fac = new Facilitator();


            fac = requester.GetFacilitator(Convert.ToInt32(cp.Identity.Name));

            lbAccount.Text = "Hi, " + fac.FirstName + fac.LastName;


            tbEventDate.Text = DateTime.Today.ToShortDateString();
        }       
    }

    protected void btnCreateEvent_Click(object sender, EventArgs e)
    {
        CSS requester = new CSS();
        Event cEvent = new Event();
        bool success;
        string EventKey;
        EventKey = requester.CreateEventKey(3);

        CustomPrincipal cp = HttpContext.Current.User as CustomPrincipal;

        //tbEventID.Text = EventKey;

        cEvent.EventID = EventKey;
        cEvent.FacilitatorID = Convert.ToInt32(cp.Identity.Name);
        cEvent.Performer = tbPerformer.Text;
        cEvent.Location = tbLocation.Text;
        cEvent.Description = tbNatureOfPerformance.Text;
        cEvent.Date = DateTime.Parse(tbEventDate.Text);

        
        success = requester.CreateEvent(cEvent);

        if (success)
        {
            Session["Event"] = cEvent;
            Server.Transfer("ViewEvent.aspx");
        }


    }
}