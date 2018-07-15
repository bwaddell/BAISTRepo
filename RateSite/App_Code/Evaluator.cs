using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Evaluator
/// </summary>
public class Evaluator
{
    private int EvaluatorIDValue;
    private string NameValue;
    private string CriteriaValue;
    private List<Evaluation> EvaluatorDataValue = new List<Evaluation>();
    private List<Question> ResponsesList = new List<Question>();

    public int EvaluatorID
    {
        get { return EvaluatorIDValue; }
        set { EvaluatorIDValue = value; }
    }
    public string Name
    {
        get { return NameValue; }
        set { NameValue = value; }
    }

    public string Criteria
    {
        get { return CriteriaValue; }
        set { CriteriaValue = value; }
    }

    public List<Evaluation> EvaluatorEvaluations
    {
        get
        {
            return EvaluatorDataValue;
        }

        set
        {
            EvaluatorDataValue = value;
        }
    }

    public List<Question> Responses
    {
        get
        {
            return ResponsesList;
        }

        set
        {
            ResponsesList = value;
        }
    }
}