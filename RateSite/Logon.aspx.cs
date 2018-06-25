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
        if (!IsPostBack)
        {
            ButtonLogin.Enabled = true;
            consentCheck.Checked = false;
        }
    }

    protected void ButtonLogin_Click(object sender, EventArgs e)
    {
        //only validate if user has agreed to terms
        if (consentCheck.Checked)
        {
            CSS RequestManager = new CSS();

            //validate user login info
            if (RequestManager.IsAuthenticated(EmailTxt.Text, PasswordTxt.Text))
            {
                //if


                //get info for email input
                Facilitator pullFacilitator = RequestManager.GetFacilitatorByEmail(EmailTxt.Text);

                string roles = pullFacilitator.Roles;

                //create authentication cookie
                FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(1, RequestManager.GetFacilitatorByEmail(EmailTxt.Text).FacilitatorID.ToString(), DateTime.Now,
                                DateTime.Now.AddHours(24), RememberChk.Checked, roles);

                string encryptedTicket = FormsAuthentication.Encrypt(authTicket);

                HttpCookie authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);

                Response.Cookies.Add(authCookie);

                //create consent cookie if there isn't one
                var consentCookie = Request.Cookies["ConsentCookie"];

                if (consentCookie == null)
                {
                    HttpCookie newConsent = new HttpCookie("ConsentCookie", "true");

                    //set cookie to expire in 100 days
                    newConsent.Expires = DateTime.UtcNow.AddDays(100);

                    Response.Cookies.Add(newConsent);
                }


                string Redirect;
                Redirect = Request["ReturnUrl"];
                if (Redirect == null)
                    Redirect = "Default.aspx";
                Response.Redirect(Redirect, true);
            }
            else
            {
                MsgLbl.Text = "Your email or password is incorrect";
            }
        }
        else
        {
            consentCheck.ForeColor = System.Drawing.Color.Red;
        }


        
    }

}