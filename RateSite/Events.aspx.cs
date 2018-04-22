using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Events : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        CustomPrincipal cp = HttpContext.Current.User as CustomPrincipal;
        CSS Director = new CSS();


        Facilitator activeFac = new Facilitator();
        activeFac.FacilitatorID = Convert.ToInt32(cp.Identity.Name);
        activeFac = Director.GetFacilitator(activeFac.FacilitatorID);

        List<Event> EventList;
        EventList = Director.GetFacilitatorEvents(activeFac.FacilitatorID);

        foreach(Event eve in EventList)
        {
            TableRow tRow = new TableRow();

            TableCell tCell = new TableCell();
            tCell.Text = eve.Date.ToLongDateString();
            tRow.Cells.Add(tCell);

            tCell = new TableCell();
            tCell.Text = eve.Location;
            tRow.Cells.Add(tCell);

            tCell = new TableCell();
            tCell.Text = eve.Performer;
            tRow.Cells.Add(tCell);

            tCell = new TableCell();
            tCell.Text = eve.Description;
            tRow.Cells.Add(tCell);

            tCell = new TableCell();
            tCell.Text = eve.Evaluators.Count.ToString();
            tRow.Cells.Add(tCell);

            tCell = new TableCell();
            tCell.CssClass = "btn-group";
            Button btn = new Button();
            btn.Text = "View Event";
            btn.ID = String.Format("EventView{0}", eve.EventID);
            btn.Click += new EventHandler(ViewEvent_Click);
            btn.CssClass = "btn btn-default";
            tCell.Controls.Add(btn);
            tRow.Cells.Add(tCell);

            //Future delete event button
            //btn = new Button();
            //btn.Text = "Delete";
            //btn.ID = String.Format("EventDelete{0}", eve.EventID);
            //btn.Click += new EventHandler(DeleteEvent_Click);
            //btn.CssClass = "btn btn-light";
            //tCell.Controls.Add(btn);
            //tRow.Cells.Add(tCell);

            //tRow.Cells.Add(tCell);

            tblEventList.Rows.Add(tRow);

        }



    }

    protected void ViewEvent_Click(object sender, EventArgs e)
    {
        CSS RequestDirector = new CSS();
        string EventID = ((Button)sender).ID;

        EventID = EventID.Replace("EventView", "");

        //get selected event info
        Event selectedEvent = new Event();
        selectedEvent.EventID = EventID;
        selectedEvent = RequestDirector.GetEvent(selectedEvent);

        //save event to session, redirect to view event page
        if (selectedEvent.Date != default(DateTime))
        {
            Session["Event"] = selectedEvent;
            Response.Redirect("ViewEvent.aspx");
        }
    }

    protected void DeleteEvent_Click(object sender, EventArgs e)
    {

    }
}