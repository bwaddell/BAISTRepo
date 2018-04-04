using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

public partial class MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //checks to see if the evaluator exsits in the session before it writes to the page
        //if (Session["Evaluator"] != null)
        //{
        //    lbMsg.Text = "Your Evaluator ID is: " + ((Evaluator)Session["Evaluator"]).EvaluatorID.ToString();
        //    lbMsg.Text += "<br />Your Event ID is: " + ((Event)Session["Event"]).EventID + "<br />";


        //}

        CustomPrincipal cp = HttpContext.Current.User as CustomPrincipal;

        if (HttpContext.Current.User.Identity.IsAuthenticated)
        {
            ButtonSignOut.Visible = true;
            ButtonViewAccount.Visible = true;

            if (cp.IsInRole("Facilitator"))
            {
                
            }
            else
            {
                
            }
        }
        else
        {
            ButtonCreateAccount.Visible = true;
            ButtonLogIn.Visible = true;
        }


    }

    protected void ButtonViewAccount_Click(object sender, EventArgs e)
    {
        Server.Transfer("FacilitatorAccount.aspx");
    }

    protected void ButtonLogIn_Click(object sender, EventArgs e)
    {
        Server.Transfer("Logon.aspx");
    }

    protected void ButtonSignOut_Click(object sender, EventArgs e)
    {
        FormsAuthentication.SignOut();
        Response.Redirect("HomePage.aspx", true);
    }

    protected void ButtonCreateAccount_Click(object sender, EventArgs e)
    {
        Server.Transfer("CreateAccount.aspx");
    }
}
