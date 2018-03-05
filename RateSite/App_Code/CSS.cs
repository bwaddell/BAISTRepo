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
}