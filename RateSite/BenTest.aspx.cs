using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class BenTest : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        CSS RequestDirector = new CSS();

        stuffLbl.Text = RequestDirector.CreateEventKey(4);
    }
}