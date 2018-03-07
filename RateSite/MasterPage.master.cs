using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //checks to see if the evaluator exsits in the session before it writes to the page
        if (Session["Evaluator"] != null)
        {
            lbMsg.Text = "Your Evaluator ID is: " + ((Evaluator)Session["Evaluator"]).EvaluatorID.ToString();
            lbMsg.Text += "<br />Your Event ID is: " + ((Event)Session["Event"]).EventID;
        }



    }
}
