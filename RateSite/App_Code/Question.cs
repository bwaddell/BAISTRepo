using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Question
/// </summary>
public class Question
{
    public Question()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    private int QIDValue;
    private int EventIDValue;
    private int EvaluatorIDValue;
    private string QuestionTextValue;
    private string ResponseTextValue;

    public int QID
    {
        get
        {
            return QIDValue;
        }

        set
        {
            QIDValue = value;
        }
    }

    public int EventID
    {
        get
        {
            return EventIDValue;
        }

        set
        {
            EventIDValue = value;
        }
    }

    public int EvaluatorID
    {
        get
        {
            return EvaluatorIDValue;
        }

        set
        {
            EvaluatorIDValue = value;
        }
    }

    public string QuestionText
    {
        get
        {
            return QuestionTextValue;
        }

        set
        {
            QuestionTextValue = value;
        }
    }

    public string ResponseText
    {
        get
        {
            return ResponseTextValue;
        }

        set
        {
            ResponseTextValue = value;
        }
    }
}