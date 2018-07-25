using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Events : System.Web.UI.Page
{
    DateTime defaultTime = Convert.ToDateTime("1800-01-01 12:00:00 PM");



    protected void Page_Load(object sender, EventArgs e)
    {
        CustomPrincipal cp = HttpContext.Current.User as CustomPrincipal;
        CSS Director = new CSS();

        Facilitator activeFac = new Facilitator();
        activeFac.FacilitatorID = Convert.ToInt32(cp.Identity.Name);
        activeFac = Director.GetFacilitator(activeFac.FacilitatorID);

        List<Event> EventList;
        EventList = Director.GetFacilitatorEvents(activeFac.FacilitatorID);

        // Displays Rows in the table
        foreach(Event eve in EventList)
        {
            // Display the Row for each event
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
            if (eve.EventStart != defaultTime)
            {
                if (eve.EventEnd != defaultTime)
                    tCell.Text = "Completed";
                else tCell.Text = "Running";
            }
            else
                tCell.Text = "Waiting to start";
            tRow.Cells.Add(tCell);


            tCell = new TableCell();
            tCell.Text = eve.Evaluators.Count.ToString();
            tRow.Cells.Add(tCell);            


            tCell = new TableCell();
            tCell.CssClass = "btn-group";
            Button btn = new Button();
            btn.Text = "View Event";
            btn.ID = String.Format("EventView{0}", eve.EventID.ToString());
            btn.Click += new EventHandler(ViewEvent_Click);
            btn.CssClass = "btn btn-default";
            tCell.Controls.Add(btn);
            tRow.Cells.Add(tCell);

            btn = new Button();
            btn.Text = "Delete";
            btn.ID = String.Format("EventDelete{0}", eve.EventID.ToString());
            btn.Click += new EventHandler(DeleteEvent_Click);
            btn.OnClientClick = "return confirm('Are you sure you want to delete this event and all associated data?');";
            btn.CssClass = "btn btn-default";
            tCell.Controls.Add(btn);
            tRow.Cells.Add(tCell);

            tRow.Cells.Add(tCell);

            tblEventList.Rows.Add(tRow);

        }
    }

    protected void ViewEvent_Click(object sender, EventArgs e)
    {
        CSS RequestDirector = new CSS();
        string EventID = ((Button)sender).ID;

        EventID = EventID.Replace("EventView", "");

        int intID = Convert.ToInt32(EventID);

        //get selected event info
        Event selectedEvent = new Event();
        selectedEvent.EventID = intID;
        selectedEvent = RequestDirector.GetEvent(selectedEvent);

        //save event to session, redirect to view event page
        if (selectedEvent.Date != default(DateTime))
        {
            Session["Event"] = selectedEvent;
            Session["criteria"] = "All Evaluations";
            Session["math"] = "Mean";
            Response.Redirect("ViewEvent.aspx");
        }
    }

    protected void DeleteEvent_Click(object sender, EventArgs e)
    {
        CSS RequestDirector = new CSS();
        string EventID = ((Button)sender).ID;

        EventID = EventID.Replace("EventDelete", "");

        Event selectedEvent = new Event();
        selectedEvent.EventID = Convert.ToInt32(EventID);

        //Delete event data
        bool confirmation;

        List<Question> qs = new List<Question>();
        qs = RequestDirector.GetQuestions(selectedEvent.EventID);

        foreach (Question q in qs)
        {
            confirmation = RequestDirector.DeleteQuestion(q);
        }    
        
        confirmation = RequestDirector.DeleteEventData(selectedEvent);

        //Delete event
        confirmation = RequestDirector.DeleteEvent(selectedEvent);

        //refresh to remove from list
        Response.Redirect("Events.aspx");
    }
}