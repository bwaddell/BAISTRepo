using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PostEvent : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        CSS RequestDirector = new CSS();
        DateTime defaultTime = Convert.ToDateTime("1800-01-01 12:00:00 PM");

        Event ActiveEvent = new Event();
        ActiveEvent.EventID = ((Event)Session["Event"]).EventID;
        ActiveEvent = RequestDirector.GetEvent(ActiveEvent);

        //Event ActiveEvent = new Event();
        //ActiveEvent.EventID = 4;
        //ActiveEvent = RequestDirector.GetEvent(ActiveEvent);

        if (ActiveEvent.CloseMsg != null && ActiveEvent.CloseMsg.Length > 0)
        {
            PanelMSG.Visible = true;
            tbCloseMessage.Text = ActiveEvent.CloseMsg;
        }
    }
}