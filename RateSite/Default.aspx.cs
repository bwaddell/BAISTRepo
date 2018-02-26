using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;


public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //RateHub chatHub = new RateHub();

        
        //chathub.Send("test", "String");

        //Context.GetOwinContext()
        //  chathub.NewMessageToPage("startupName", "startupMessage");



    }

    protected void btnBroadcast_Click(object sender, EventArgs e)
    {
        //    //this is the trigger event called when: btnBroadcast is clicked 
        //    //the method in the hub is called
        //    $('#MainBody_btnBroadcast').click(function() {
        //    console.log('Broadcast Button pressed');
        //    var formName = document.getElementById('MainBody_tbName').value;
        //    var formMessage = document.getElementById('MainBody_tbMessage').value;
        //    myRateHub.server.newMessageToPage(formName, formMessage);
        //});


        //var theHubContext = GlobalHost.ConnectionManager.GetHubContext<RateHub>();
        //if (theHubContext != null)
        //{
        //    // addNewMessgeToPage is the Javascript function name on the client side
        //    theHubContext.Clients.All.addNewMessageToPage(tbName.Text, tbMessage.Text);
        //}

        RateHub chatHub = new RateHub();
        chatHub.NewMessageToPage(tbName.Text, tbMessage.Text);

    }

    protected void btnJoinGroup_Click(object sender, EventArgs e)
    {
        RateHub chatHub = new RateHub();
        //chatHub.JoinGroup("hi");
        //chatHub.Groups.Add();
    }
}