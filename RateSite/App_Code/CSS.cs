using System;
using System.Collections.Generic;

using System.Linq;
using System.Web;


/// <summary>
/// Summary description for CSS ContinUI Controller Services
/// </summary>
public class CSS
{
    public CSS()
    {
        //
        // TODO: Add constructor logic here
        //
    }


    /// <summary>
    /// Function is called from front end to add evaluation
    /// data to SQL Server
    /// </summary>
    public bool AddEvaluation(Evaluation evaluation) //change name later??
    {
        bool Confirmation = false;
        EvaluationDirector Controller = new EvaluationDirector();

        Confirmation = Controller.CreateEvaluation(evaluation);

        return Confirmation;
    }

    public string CreateEventKey(int size)
    {
        string EventKey = "";
        EventDirector Controller = new EventDirector();

        EventKey = Controller.CreateEventKey(size);
        return EventKey;
    }

    public bool CreateEvent(Event CEvent)
    {
        bool Confirmation = false;
        EventDirector Controller = new EventDirector();

        Confirmation = Controller.CreateEvent(CEvent);

        return Confirmation;

    }
    public Event GetEvent(Event currentEvent)
    {
        Event foundEvent = new Event();
        EventDirector Controller = new EventDirector();

        foundEvent = Controller.GetEvent(currentEvent);

        return foundEvent;
    }
    public Evaluator CreateEvaluator()
    { 
        Evaluator newEvaluator = new Evaluator();
        EventDirector Controller = new EventDirector();

        newEvaluator = Controller.CreateEvaluator();

        return newEvaluator;
    }
    public List<Evaluation> GetCurrentEventData(Event currentEvent)
    {
        List<Evaluation> evals = new List<Evaluation>();
        EvaluationDirector Controller = new EvaluationDirector();

        evals = Controller.GetCurrentEventData(currentEvent);
        return evals;
    }

    public Facilitator GetFacilitatorByEmail(string email)
    {
        Facilitator pullFacilitator = new Facilitator();
        FacilitatorDirector Controller = new FacilitatorDirector();

        pullFacilitator = Controller.GetFacilitatorByEmail(email);

        return pullFacilitator;
    }
}