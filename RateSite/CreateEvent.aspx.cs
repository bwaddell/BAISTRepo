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
    }

    protected void btnCreateEvent_Click(object sender, EventArgs e)
    {
        CSS thing = new CSS();

        string EventKey;
        EventKey = thing.CreateEventKey(3);

        tbEventID.Text = EventKey;





    }
}