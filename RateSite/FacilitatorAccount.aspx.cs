using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FacilitatorAccount : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        CustomPrincipal cp = HttpContext.Current.User as CustomPrincipal;
        CSS requestDirector = new CSS();

        Facilitator activeFac = new Facilitator(); 
            
        activeFac.FacilitatorID = Convert.ToInt32(cp.Identity.Name);

        activeFac = requestDirector.GetFacilitator(activeFac.FacilitatorID);

        Namelbl.Text = "Hello " + activeFac.Title + " " + activeFac.FirstName + " " + activeFac.LastName;

        FNametxt.Text = activeFac.FirstName;
        LNametxt.Text = activeFac.LastName;
        Titletxt.Text = activeFac.Title;
        Orgtxt.Text = activeFac.Organization;
        Loctxt.Text = activeFac.Location;
        Emailtxt.Text = activeFac.Email;

        List<Event> facEvents = new List<Event>();

        facEvents = requestDirector.GetFacilitatorEvents(activeFac.FacilitatorID);

        ListItem eventItem;
        EventListBox.Items.Clear();

        foreach  (Event ev in facEvents)
        {
            eventItem = new ListItem();

            eventItem.Text = ev.Date.ToShortDateString() + ": " + ev.Performer + " : " + ev.Description;
            eventItem.Value = ev.EventID;

            EventListBox.Items.Add(eventItem);
        }
    }

    protected void ViewEventbtn_Click(object sender, EventArgs e)
    {
        CSS RequestDirector = new CSS();

        Event selectedEvent = new Event();
        selectedEvent.EventID = EventListBox.SelectedValue;

        selectedEvent = RequestDirector.GetEvent(selectedEvent);

        if (selectedEvent.Date != default(DateTime))
        {
            Session["Event"] = selectedEvent;
            Server.Transfer("AnalyzeEvent.aspx");
        }
    }

    protected void UpdatePasswordBtn_Click(object sender, EventArgs e)
    {

    }

    protected void UpdateBtn_Click(object sender, EventArgs e)
    {

    }
}