using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Web.Security;

public partial class Logon : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void ButtonLogin_Click(object sender, EventArgs e)
    {

        CSS RequestManager = new CSS();

        if (RequestManager.IsAuthenticated(EmailTxt.Text, PasswordTxt.Text))
        {
            Facilitator pullFacilitator = RequestManager.GetFacilitatorByEmail(EmailTxt.Text);

            string roles = pullFacilitator.Roles;

            FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(1, RequestManager.GetFacilitatorByEmail(EmailTxt.Text).FacilitatorID.ToString(), DateTime.Now,
                            DateTime.Now.AddMinutes(60), RememberChk.Checked, roles);

            string encryptedTicket = FormsAuthentication.Encrypt(authTicket);

            HttpCookie authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);

            Response.Cookies.Add(authCookie);

            Response.Redirect("HomePage.aspx");
        }
        else
        {
            MsgLbl.Text = "Your email or password is incorrect";
        }
    }
}