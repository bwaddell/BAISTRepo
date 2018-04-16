using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

public partial class FacilitatorAccount : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            CustomPrincipal cp = HttpContext.Current.User as CustomPrincipal;
            CSS requestDirector = new CSS();

            Facilitator activeFac = new Facilitator();

            activeFac.FacilitatorID = Convert.ToInt32(cp.Identity.Name);

            activeFac = requestDirector.GetFacilitator(activeFac.FacilitatorID);

            FNametxt.Text = activeFac.FirstName;
            LNametxt.Text = activeFac.LastName;
            Titletxt.Text = activeFac.Title;
            Orgtxt.Text = activeFac.Organization;
            Loctxt.Text = activeFac.Location;
            Emailtxt.Text = activeFac.Email;

            List<Event> facEvents = new List<Event>();

            facEvents = requestDirector.GetFacilitatorEvents(activeFac.FacilitatorID);

            ListItem eventItem;
            EventListBox.Items.Clear();

            if (facEvents.Count > 0)
            {
                EventListBox.Visible = true;
                ViewEventbtn.Visible = true;

                foreach (Event ev in facEvents)
                {
                    eventItem = new ListItem();

                    eventItem.Text = ev.Date.ToShortDateString() + ": " + ev.Performer + " : " + ev.Description;
                    eventItem.Value = ev.EventID;

                    EventListBox.Items.Add(eventItem);
                }
            }
            else
            {
                EventListBox.Visible = false;
                ViewEventbtn.Visible = false;
            }

           
        }
        
    }

    protected void ViewEventbtn_Click(object sender, EventArgs e)
    {
        CSS RequestDirector = new CSS();

        Event selectedEvent = new Event();
        selectedEvent.EventID = EventListBox.SelectedValue;

        selectedEvent = RequestDirector.GetEvent(selectedEvent);

        if (selectedEvent.Date != default(DateTime))
        {
            Session["Event"] = selectedEvent;
            Server.Transfer("ViewEvent.aspx");
        }
    }

    protected void UpdatePasswordBtn_Click(object sender, EventArgs e)
    {
        CustomPrincipal cp = HttpContext.Current.User as CustomPrincipal;
        CSS requestDirector = new CSS();

        Facilitator activeFac = new Facilitator();

        activeFac.FacilitatorID = Convert.ToInt32(cp.Identity.Name);

        activeFac = requestDirector.GetFacilitator(activeFac.FacilitatorID);

        if (activeFac.Password == requestDirector.CreatePasswordHash(oldPasswordtxt.Text, activeFac.Salt))
        {
            activeFac.Password = requestDirector.CreatePasswordHash(Passwordtxt.Text, activeFac.Salt);

            if (requestDirector.UpdateFacilitator(activeFac))
            {
                Pswdlbl.Text = "Account Password Updated";
            }
            else
            {
                Pswdlbl.Text = "Account Password Update Failed";
            }
        }
    }

    protected void UpdateBtn_Click(object sender, EventArgs e)
    {
        CustomPrincipal cp = HttpContext.Current.User as CustomPrincipal;
        CSS requestDirector = new CSS();

        Facilitator activeFac = new Facilitator();

        activeFac.FacilitatorID = Convert.ToInt32(cp.Identity.Name);

        activeFac = requestDirector.GetFacilitator(activeFac.FacilitatorID);

        if (Emailtxt.Text != activeFac.Email)
        {
            if (requestDirector.GetFacilitatorByEmail(Emailtxt.Text).Email == default(string))
            {
                activeFac.Email = Emailtxt.Text;
                activeFac.FirstName = FNametxt.Text;
                activeFac.LastName = LNametxt.Text;
                activeFac.Title = Titletxt.Text;
                activeFac.Organization = Orgtxt.Text;
                activeFac.Location = Loctxt.Text;

                if (requestDirector.UpdateFacilitator(activeFac))
                {
                    Msglbl.Text = "Account Information Updated";
                }
                else
                {
                    Msglbl.Text = "Account Information Update Failed";
                }
            }
            else
            {
                Msglbl.Text = "That email is used by another account";
            }
        }
        else
        {
            activeFac.Email = Emailtxt.Text;
            activeFac.FirstName = FNametxt.Text;
            activeFac.LastName = LNametxt.Text;
            activeFac.Title = Titletxt.Text;
            activeFac.Organization = Orgtxt.Text;
            activeFac.Location = Loctxt.Text;

            if (requestDirector.UpdateFacilitator(activeFac))
            {
                Msglbl.Text = "Account Information Updated";
            }
            else
            {
                Msglbl.Text = "Account Information Update Failed";
            }
        }       
    }

}