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

            //get data for event facilitator
            Facilitator activeFac = new Facilitator();
            activeFac.FacilitatorID = Convert.ToInt32(cp.Identity.Name);
            activeFac = requestDirector.GetFacilitator(activeFac.FacilitatorID);

            FNametxt.Text = activeFac.FirstName;
            LNametxt.Text = activeFac.LastName;
            Titletxt.Text = activeFac.Title;
            Orgtxt.Text = activeFac.Organization;
            Loctxt.Text = activeFac.Location;
            Emailtxt.Text = activeFac.Email;

            //get any active or past events beloning to facilitator
            List<Event> facEvents = new List<Event>();
            facEvents = requestDirector.GetFacilitatorEvents(activeFac.FacilitatorID);

            //add events to list
            ListItem eventItem;
            EventListBox.Items.Clear();

            //if user has no events, hide the listbox and button
            if (facEvents.Count > 0)
            {
                EventListBox.Visible = true;
                ViewEventbtn.Visible = true;

                //populate event list
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

    //redirect to view event page for selected event
    protected void ViewEventbtn_Click(object sender, EventArgs e)
    {
        CSS RequestDirector = new CSS();

        //get selected event info
        Event selectedEvent = new Event();
        selectedEvent.EventID = EventListBox.SelectedValue;
        selectedEvent = RequestDirector.GetEvent(selectedEvent);

        //save event to session, redirect to view event page
        if (selectedEvent.Date != default(DateTime))
        {
            Session["Event"] = selectedEvent;
            Server.Transfer("ViewEvent.aspx");
        }
    }

    //update facilitator password
    protected void UpdatePasswordBtn_Click(object sender, EventArgs e)
    {
        CustomPrincipal cp = HttpContext.Current.User as CustomPrincipal;
        CSS requestDirector = new CSS();

        //get facilitator info
        Facilitator activeFac = new Facilitator();
        activeFac.FacilitatorID = Convert.ToInt32(cp.Identity.Name);
        activeFac = requestDirector.GetFacilitator(activeFac.FacilitatorID);

        //if valid password, update facilitator account with new hash
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

    //update facilitator account info
    protected void UpdateBtn_Click(object sender, EventArgs e)
    {
        CustomPrincipal cp = HttpContext.Current.User as CustomPrincipal;
        CSS requestDirector = new CSS();

        //get facilitator info
        Facilitator activeFac = new Facilitator();
        activeFac.FacilitatorID = Convert.ToInt32(cp.Identity.Name);
        activeFac = requestDirector.GetFacilitator(activeFac.FacilitatorID);

        //check if facilitator changed email
        if (Emailtxt.Text != activeFac.Email)
        {
            //if new email, check if email already in use
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