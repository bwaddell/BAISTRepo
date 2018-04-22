using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using DotNet.Highcharts;
using DotNet.Highcharts.Options;
using DotNet.Highcharts.Helpers;
using DotNet.Highcharts.Enums;
using System.Xml.Serialization;

/// <summary>
/// This webservice will allow you to gather all the event data from the server
/// </summary>
[WebService(Namespace = "http://ContinUI.com", Name = "ContinUI", Description = "Web service for ContinUI")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]


public class EventWebService : System.Web.Services.WebService
{
    public EventWebService()
    {
        //Uncomment the following line if using designed components 
        //InitializeComponent();
    }

    //[WebMethod]
    //public string HelloWorld()
    //{
    //    return "Hello World";
    //}

    [WebMethod]
    public Event GetEvent(string eventID)
    {
        //this method gets all the event data using the CSS director
        Random rand = new Random();
        CSS Director = new CSS();
        Event ActiveEvent = new Event();

        ActiveEvent.EventID = eventID;

        ActiveEvent = Director.GetEvent(ActiveEvent);

        return ActiveEvent;
    }

}
