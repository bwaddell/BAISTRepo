using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

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

            newFac.Salt = RequestDirector.CreateSalt(5);
            newFac.Password = RequestDirector.CreatePasswordHash(PasswordTxt.Text, newFac.Salt);
            newFac.Roles = "Facilitator|";

            Confirmation = RequestDirector.CreateFacilitator(newFac);

            if (Confirmation)
            {

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