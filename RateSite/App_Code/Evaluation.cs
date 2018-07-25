using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//using Microsoft.AspNet.SignalR;
//using Microsoft.AspNet.SignalR.Hubs;


/// <summary>
/// Summary description for Evaluation
/// </summary>
public class Evaluation
{
    private DateTime TimeStampValue;
    private int RatingValue;
    private int EvaluatorIDValue;
    private int EventIDValue;

    public Evaluation()
    {
    }

    public Evaluation(DateTime dt, int rating, int evaluatorID, int eventID)
    {
        //  method sets the timestamp to NOW and all other 
        //  variables are supplied
        TimeStampValue = dt;
        RatingValue = rating;
        EvaluatorIDValue = evaluatorID;
        EventIDValue = eventID;
    }

    public DateTime TimeStamp
    {
        get { return TimeStampValue; }
        set { TimeStampValue = value; }
    }
    public int Rating
    {
        get { return RatingValue; }
        set { RatingValue = value; }
    }
    public int EvaluatorID
    {
        get { return EvaluatorIDValue; }
        set { EvaluatorIDValue = value; }
    }
    public int EventID
    {
        get { return EventIDValue; }
        set { EventIDValue = value; }
    }

//    private readonly static Lazy<Evaluation> _instance = new Lazy<Evaluation>(() => 
//    new Evaluation(GlobalHost.ConnectionManager.GetHubContext<RateHub>().Clients));
}