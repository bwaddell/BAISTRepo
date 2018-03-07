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
        tbEventID.Enabled = false;
        tbEventDate.Text = DateTime.Today.ToShortDateString();
    }

    protected void btnCreateEvent_Click(object sender, EventArgs e)
    {
        CSS thing = new CSS();
        Event cEvent = new Event();
        bool success;
        string EventKey;
        EventKey = thing.CreateEventKey(3);

        tbEventID.Text = EventKey;

        cEvent.EventID = EventKey;
        cEvent.FacilitatorID = Convert.ToInt32(tbFacilitatorID.Text);
        cEvent.Performer = tbPerformer.Text;
        cEvent.Location = tbLocation.Text;
        cEvent.Description = tbNatureOfPerformance.Text;

        
        success = thing.CreateEvent(cEvent);

        if (success)
        {
            Server.Transfer("AnalyzeEvent.aspx");
        }
        //else
        //{
        //    lbstatus.Text = "Error Generating Event.  Please Try Again.";
        //}


    }
}