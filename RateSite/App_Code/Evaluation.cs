﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Evaluation
/// </summary>
public class Evaluation
{
    private DateTime TimeStampValue;
    private int RatingValue;
    private int EvaluatorIDValue;
    private int EventIDValue;

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
}