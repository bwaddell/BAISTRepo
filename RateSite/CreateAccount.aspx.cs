using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

public partial class CreateAccount : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

    protected void ButtonSubmit_Click(object sender, EventArgs e)
    {
        Facilitator newFac = new Facilitator();
        CSS RequestDirector = new CSS();
        bool Confirmation;

        //if getfacilitator returns default facilitator values, that email has not been used
        if (RequestDirector.GetFacilitatorByEmail(EmailTxt.Text).Email == default(string))
        {
            newFac.FirstName = FirstNameTxt.Text;
            newFac.LastName = LastNameTxt.Text;
            newFac.Title = TitleTxt.Text;
            newFac.Email = EmailTxt.Text;
            newFac.Organization = OrgTxt.Text;
            newFac.Location = LocTxt.Text;

            //generate password hash
            newFac.Salt = RequestDirector.CreateSalt(5);
            newFac.Password = RequestDirector.CreatePasswordHash(PasswordTxt.Text, newFac.Salt);

            newFac.Roles = "Facilitator|";

            //attempt to create an account
            Confirmation = RequestDirector.CreateFacilitator(newFac);

            //if account creation successful, log in and redirect to home
            if (Confirmation)
            {
                if (RequestDirector.IsAuthenticated(EmailTxt.Text, PasswordTxt.Text))
                {
                    Facilitator pullFacilitator = RequestDirector.GetFacilitatorByEmail(EmailTxt.Text);

                    string roles = pullFacilitator.Roles;

                    FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(1, RequestDirector.GetFacilitatorByEmail(EmailTxt.Text).FacilitatorID.ToString(), DateTime.Now,
                                    DateTime.Now.AddMinutes(60), false, roles);

                    string encryptedTicket = FormsAuthentication.Encrypt(authTicket);

                    HttpCookie authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);

                    Response.Cookies.Add(authCookie);

                    Response.Redirect("Default.aspx");
                }
                else
                {
                    MsgLbl.Text = "Your email or password is incorrect";
                }
            }
            else
            {
                MsgLbl.Text = "Error creating account.";
            }
        }
        else
        {
            MsgLbl.Text = "This email is already associated with an account.";
        }    
    }
}