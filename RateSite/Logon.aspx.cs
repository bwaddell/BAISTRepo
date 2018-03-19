using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Logon : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void ButtonLogin_Click(object sender, EventArgs e)
    {

        BCS RequestManager = new BCS();

        if (RequestManager.IsAuthenticated(EmailTextBox.Text, PasswordTextBox.Text))
        {
            string roles = RequestManager.GetRoles(EmailTextBox.Text);

            FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(1, RequestManager.GetUserByEmail(EmailTextBox.Text).MemberID.ToString(), DateTime.Now,
                            DateTime.Now.AddMinutes(60), RememberCheckBox.Checked, roles);

            string encryptedTicket = FormsAuthentication.Encrypt(authTicket);

            HttpCookie authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);

            Response.Cookies.Add(authCookie);

            Response.Redirect(FormsAuthentication.GetRedirectUrl(EmailTextBox.Text, RememberCheckBox.Checked));
        }
        else
        {
            MsgLabel.Text = "Your email or password is incorrect";
        }
    }
}