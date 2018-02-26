using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System.Threading.Tasks;

public class RateHub : Hub
{

    public void Hello()
    {        
        
    }

    
    public void NewMessageToPage(string name, string message)
    {

        var theHubContext = GlobalHost.ConnectionManager.GetHubContext<RateHub>();
        if (theHubContext != null)
        {
            // addNewMessgeToPage is the Javascript function name on the client side
            theHubContext.Clients.All.addNewMessageToPage(name, message);
        }


        //Clients.All.addNewMessageToPage(name, message);
    }

    public Task JoinGroup(string groupName)
    {
        return Groups.Add(Context.ConnectionId, groupName);
    }

    public Task LeaveGroup(string groupName)
    {
        return Groups.Remove(Context.ConnectionId, groupName);
    }



}


//public interface iClient
//{
//    void addNewMessageToPage(string name, string message);
//}
