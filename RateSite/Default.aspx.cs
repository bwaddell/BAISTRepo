﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (HttpContext.Current.User.Identity.IsAuthenticated)
            //CreateButton.Visible = true;
        //else
        //    CreateButton.Visible = false;
    }

    protected void CreateButton_Click(object sender, EventArgs e)
    {
        if (HttpContext.Current.User.Identity.IsAuthenticated)
        {
            Response.Redirect("CreateEvent.aspx");
        }
        else
        {
            //not logged in or cant find user redirect to create account page.
            Response.Redirect("Logon.aspx");
        }
    }

    protected void JoinButton_Click(object sender, EventArgs e)
    {
        //check if key is right length
        if(tbEventKey.Text.Length == 4 && tbEventKey.Text != "AAAA" && tbEventKey.Text != "ZZZZ" )
        {
            statuslbl.Text = "";

            //default value for event start/end time
            DateTime defaultTime = Convert.ToDateTime("01-01-1800 12:00:00");
        
            CSS RequestDirector = new CSS();

            //get event info for key input
            Event findEvent = new Event();
            findEvent.EventKey = tbEventKey.Text;

            findEvent = RequestDirector.GetEventFromKey(findEvent.EventKey);

            //check if event key exists
            if (findEvent.EventKey != default(string))
            {
                //if event end time is not default value, event is over.  Can not join
                if (findEvent.EventEnd == defaultTime)
                {
                    Session["Event"] = findEvent;
                    Response.Redirect("JoinEvent.aspx");

                }
                else
                {
                    tbEventKey.Text = "";
                    statuslbl.Text = "Invalid Key";
                }
            }
            else
            {
                tbEventKey.Text = "";
                statuslbl.Text = "Invalid Key";
            }
        }
        else
        {
            tbEventKey.Text = "";
            statuslbl.Text = "Invalid Key";
        }
    }
}