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

        //CustomPrincipal cp = HttpContext.Current.User as CustomPrincipal;

        if (HttpContext.Current.User.Identity.IsAuthenticated)
        {
            ButtonSignOut.Visible = true;
            ButtonViewAccount.Visible = true;
            ButtonEventList.Visible = true;

        }
        else
        {
            ButtonCreateAccount.Visible = true;
            ButtonLogIn.Visible = true;
            ButtonEventList.Visible = false;

        }


    }

    protected void ButtonViewAccount_Click(object sender, EventArgs e)
    {
        Response.Redirect("FacilitatorAccount.aspx");
    }

    protected void ButtonLogIn_Click(object sender, EventArgs e)
    {
        Response.Redirect("Logon.aspx");
    }

    protected void ButtonSignOut_Click(object sender, EventArgs e)
    {
        FormsAuthentication.SignOut();
        Response.Redirect("Default.aspx", true);
    }

    protected void ButtonCreateAccount_Click(object sender, EventArgs e)
    {
        Response.Redirect("CreateAccount.aspx");
    }

    protected void ButtonEventList_Click(object sender, EventArgs e)
    {
        Response.Redirect("Events.aspx");

    }
}
