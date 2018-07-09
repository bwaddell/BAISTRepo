using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Event
/// </summary>
public class Event
{
    private int EventIDValue;
    private string EventKeyValue;
    private int FacilitatorIDValue;
    private List<Evaluator> EvaluatorsList;
    private string LocationValue;
    private string PerformerValue;
    private string DescriptionValue;
    private DateTime DateValue;
    private DateTime EventStartValue;
    private DateTime EventEndValue;
    private string OpenMsgValue;
    private string CloseMsgValue;
    private string VotingCritValue;
    private List<Question> CustomQuestionsList;

    public int EventID
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

    public string EventKey
    {
        get{ return EventKeyValue; }
        set{ EventKeyValue = value; }
    }

    public string OpenMsg
    {
        get{ return OpenMsgValue; }
        set { OpenMsgValue = value; }
    }

    public string CloseMsg
    {
        get
        {
            return CloseMsgValue;
        }

        set
        {
            CloseMsgValue = value;
        }
    }

    public List<Question> CustomQuestions
    {
        get
        {
            return CustomQuestionsList;
        }

        set
        {
            CustomQuestionsList = value;
        }
    }

    public string VotingCrit
    {
        get
        {
            return VotingCritValue;
        }

        set
        {
            VotingCritValue = value;
        }
    }
}