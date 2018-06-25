using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

public partial class Privacy : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void delCook_Click(object sender, EventArgs e)
    {
        var consentCookie = Request.Cookies["ConsentCookie"];

        if (consentCookie != null)
        {
            HttpCookie eatCookie = new HttpCookie("ConsentCookie", "true");

            //set cookie to expire in 100 days
            eatCookie.Expires = DateTime.UtcNow.AddDays(-1);

            Response.Cookies.Add(eatCookie);
        }


        HttpContext.Current.Request.Cookies.Clear();
        FormsAuthentication.SignOut();
        Response.Redirect("Privacy.aspx", true);



    }
}