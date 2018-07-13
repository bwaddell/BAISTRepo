using System;
using System.Collections.Generic;
using DotNet.Highcharts;
using DotNet.Highcharts.Options;
using DotNet.Highcharts.Helpers;
using DotNet.Highcharts.Enums;
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

    public string GenKey(int size)
    {
        string EventKey = "";
        EventDirector Controller = new EventDirector();

        EventKey = Controller.GenKey(size);
        return EventKey;
    }

    public string CreateEventKey(int size)
    {
        string EventKey = "";
        EventDirector Controller = new EventDirector();

        EventKey = Controller.CreateEventKey(size);
        return EventKey;
    }

    public Event CreateEvent(Event CEvent)
    {
        Event newEvent = new Event();
        EventDirector Controller = new EventDirector();

        newEvent = Controller.CreateEvent(CEvent);

        return newEvent;
    }
    public Event GetEvent(Event currentEvent)               //this one
    {
        //redo this method to return the WHOLE Event
        Event fEvent = new Event();
        EventDirector Controller = new EventDirector();

        fEvent = Controller.GetEvent(currentEvent.EventID);
        return fEvent;
    }

    public Event GetEventFromKey(string key)
    {
        Event fEvent = new Event();
        EventDirector Controller = new EventDirector();

        fEvent = Controller.GetEventFromKey(key);
        return fEvent;
    }

    public Evaluator CreateEvaluator(Evaluator eval)
    { 
        Evaluator newEvaluator = new Evaluator();
        EventDirector Controller = new EventDirector();

        newEvaluator = Controller.CreateEvaluator(eval);

        return newEvaluator;
    }
    public List<Evaluation> GetCurrentEventData(Event currentEvent)
    {
        List<Evaluation> evals = new List<Evaluation>();
        EvaluationDirector Controller = new EvaluationDirector();

        evals = Controller.GetCurrentEventData(currentEvent);
        return evals;
    }

    public List<Evaluation> GetEventData(int EventID)
    {
        //List<Evaluator> evaluators = new List<Evaluator>();
        List<Evaluation> evaluations = new List<Evaluation>();
        EvaluationDirector Controller = new EvaluationDirector();

        evaluations = Controller.GetAllEventData(EventID);          

        return evaluations;
    }

    public Facilitator GetFacilitatorByEmail(string email)
    {
        Facilitator pullFacilitator = new Facilitator();
        FacilitatorDirector Controller = new FacilitatorDirector();

        pullFacilitator = Controller.GetFacilitatorByEmail(email);

        return pullFacilitator;
    }

    public bool IsAuthenticated(string email, string password)
    {
        bool Confirmation;
        SecurityManager Controller = new SecurityManager();

        Confirmation = Controller.IsAuthenticated(email, password);

        return Confirmation;
    }

    public bool CreateFacilitator(Facilitator newFacilitator)
    {
        bool confirmation;
        FacilitatorDirector Controller = new FacilitatorDirector();

        confirmation = Controller.CreateFacilitator(newFacilitator);
        return confirmation;
    }
    public string CreateSalt(int size)
    {
        string Salt;
        SecurityManager Controller = new SecurityManager();

        Salt = Controller.CreateSalt(size);

        return Salt;
    }
    public string CreatePasswordHash(string pwd, string salt)
    {
        string pwdHash = "";
        SecurityManager Controller = new SecurityManager();

        pwdHash = Controller.CreatePasswordHash(pwd, salt);

        return pwdHash;
    }
    public string GetHashSHA245(string inputString)
    {
        string hash = "";
        SecurityManager Controller = new SecurityManager();

        hash = Controller.GetHashSHA245(inputString);

        return hash;
    }

    public Facilitator GetFacilitator(int id)
    {
        Facilitator newFac = new Facilitator();
        FacilitatorDirector Controller = new FacilitatorDirector();

        newFac = Controller.GetFacilitator(id);
        return newFac;
    }

    public List<Event> GetFacilitatorEvents(int id)
    {
        List<Event> facEvents = new List<Event>();
        FacilitatorDirector FacController = new FacilitatorDirector();
        EventDirector EveController = new EventDirector();

        facEvents = FacController.GetFacilitatorEvents(id);


        foreach (Event even in facEvents)
            even.Evaluators = EveController.GetEvaluatorsForEvent(even.EventID);

        return facEvents;
    }

    public List<Evaluation> GetEvaluatorEvaluations(int EventID, int EvaluatorID)
    {
        List<Evaluation> EvaluatorEvents = new List<Evaluation>();
        EvaluationDirector Controller = new EvaluationDirector();

        EvaluatorEvents = Controller.GetEvaluationsForEventEvaluator(EventID, EvaluatorID);
        return EvaluatorEvents;
    }

    public bool UpdateFacilitator(Facilitator newFacilitator)
    {
        bool Confirmation;
        FacilitatorDirector Controller = new FacilitatorDirector();

        Confirmation = Controller.UpdateFacilitator(newFacilitator);
        return Confirmation;
    }

    public bool UpdateEventStatus(Event evnt)
    {
        bool Confirmation;
        EventDirector Controller = new EventDirector();

        Confirmation = Controller.UpdateEvent(evnt);
        return Confirmation;
    }


    public Highcharts CreateChart(Event evnt)
    {
        CSSChart chartmaker = new CSSChart();
        Highcharts chart = chartmaker.MakeChart(evnt);

        return chart;

    }
    public Highcharts MakeMathChart(Event theEvent, string mathType)
    {
        CSSChart chartmaker = new CSSChart();
        Highcharts chart = chartmaker.MakeMathChart(theEvent, mathType);

        return chart;
    }

    public bool DeleteFacilitator(Facilitator fac)
    {
        bool Confirmation;
        FacilitatorDirector Controller = new FacilitatorDirector();

        Confirmation = Controller.DeleteFacilitator(fac);
        return Confirmation;
    }

    public bool DeleteEvent(Event ev)
    {
        bool Confirmation;
        EventDirector Controller = new EventDirector();

        Confirmation = Controller.DeleteEvent(ev);
        return Confirmation;
    }

    public bool DeleteEvaluatorEventData(Event eve, Evaluator eva)
    {
        bool Confirmation;
        EvaluationDirector Controller = new EvaluationDirector();

        Confirmation = Controller.DeleteEvaluatorEventData(eve, eva);
        return Confirmation;
    }

    public bool DeleteEventData(Event eve)
    {
        bool Confirmation;
        EvaluationDirector Controller = new EvaluationDirector();

        Confirmation = Controller.DeleteEventData(eve);
        return Confirmation;
    }

    public bool AddQuestion(Question q)
    {
        bool Confirmation;
        EventDirector Controller = new EventDirector();

        Confirmation = Controller.AddQuestion(q);
        return Confirmation;
    }

    public List<Question> GetQuestions(int eventID)
    {
        List<Question> questions = new List<Question>();
        EventDirector Controller = new EventDirector();

        questions = Controller.GetQuestions(eventID);
        return questions;
    }

    public bool AddResponse(Question r)
    {
        bool Confirmation;
        EventDirector Controller = new EventDirector();

        Confirmation = Controller.AddResponse(r);
        return Confirmation;
    }

    public Question GetResponse(Question q)
    {
        Question qr;
        EventDirector Controller = new EventDirector();

        qr = Controller.GetResponse(q);
        return qr;
    }

    public List<string> GetEventKeys()
    {
        List<string> keys = new List<string>();
        EventDirector Controller = new EventDirector();

        keys = Controller.GetEventKeys();

        return keys;
    }

    public bool DeleteQuestion(Question q)
    {
        bool Success;
        EventDirector Controller = new EventDirector();

        Success = Controller.DeleteQuestion(q);
        return Success;
    }

    //public List<Series> ConvertEventToSeriesList(Event theEvent)
    //{
    //    CSSChart chartmaker = new CSSChart();
    //    List<Series> liOfSeries = chartmaker.ConvertEventToSeriesList(theEvent);

    //    return liOfSeries;
    //}
}