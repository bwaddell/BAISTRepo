using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Event
/// </summary>
public class Event
{
    private string EventIDValue;
    private int FacilitatorIDValue;
    private List<Evaluator> EvaluatorsList;
    private string LocationValue;
    private string PerformerValue;
    private string DescriptionValue;
    private DateTime DateValue;
    private DateTime EventStartValue;
    private DateTime EventEndValue;

    public string EventID
    {
        get { return EventIDValue; }
        set { EventIDValue = value; }
    }
    public int FacilitatorID
    {
        get { return FacilitatorIDValue; }
        set { FacilitatorIDValue = value; }
    }
    public List<Evaluator> Evaluators
    {
        get { return EvaluatorsList; }
        set { EvaluatorsList = value; }
    }
    public string Location
    {
        get { return LocationValue; }
        set { LocationValue = value; }
    }
    public string Performer
    {
        get { return PerformerValue; }
        set { PerformerValue = value; }
    }
    public string Description
    {
        get { return DescriptionValue; }
        set { DescriptionValue = value; }
    }
    public DateTime Date
    {
        get { return DateValue; }
        set { DateValue = value; }
    }
    public DateTime EventStart
    {
        get { return EventStartValue; }
        set { EventStartValue = value; }
    }
    public DateTime EventEnd
    {
        get { return EventEndValue; }
        set { EventEndValue = value; }
    }
}