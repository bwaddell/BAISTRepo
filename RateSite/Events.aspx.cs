﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Events : System.Web.UI.Page
{
    DateTime defaultTime = Convert.ToDateTime("1/1/1800 12:00:00 PM");



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
            tCell.Text = eve.EventID;
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
                    tCell.Text = (eve.EventEnd - eve.EventStart).ToString(); //TotalMinutes.ToString("#.##");
                else tCell.Text = "Running";
            }
            else
                tCell.Text = "Waiting to start";
            tRow.Cells.Add(tCell);


            tCell = new TableCell();
            tCell.Text = eve.Evaluators.Count.ToString();
            tRow.Cells.Add(tCell);


            double totalAverage;
            List<Evaluation> allEvaluations = new List<Evaluation>();
            foreach (Evaluator ev in eve.Evaluators)
            {
                allEvaluations.AddRange(ev.EvaluatorEvaluations);
            }
            if (allEvaluations.Count != 0)
                totalAverage = allEvaluations.Average(o => o.Rating);
            else
                totalAverage = 0;
            tCell = new TableCell();
            tCell.Text = totalAverage.ToString("#.##");
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