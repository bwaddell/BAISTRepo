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
        var consentCookie = Request.Cookies["ConsentCookie"];

        //if cookie doesn't exist, user has not accepted cookies
        if (consentCookie == null)
        {
            cookieBanner.Visible = true;
        }
        else
        {
            if (consentCookie.Value == "true")
            {
                cookieBanner.Visible = false;
            }
        }


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

    protected void acceptCookie_Click(object sender, EventArgs e)
    {
        CSS requestManager = new CSS();

        //create cookie stating that user has accepted cookie use
        HttpCookie consentCookie = new HttpCookie("ConsentCookie", "true");

        //set cookie to expire in 100 days
        consentCookie.Expires = DateTime.UtcNow.AddDays(100);

        Response.Cookies.Add(consentCookie);

        Page.Response.Redirect(Page.Request.Url.ToString(), true);
    }
}
