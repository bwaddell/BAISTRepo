using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Events : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        CustomPrincipal cp = HttpContext.Current.User as CustomPrincipal;
        CSS Director = new CSS();
        List<Event> EventList;

        EventList = Director.GetFacilitatorEvents(Convert.ToInt32(cp.Identity));





    }
}