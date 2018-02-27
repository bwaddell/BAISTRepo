using System;
using System.Collections.Generic;

using System.Linq;
using System.Web;


/// <summary>
/// Summary description for EvalDirector
/// </summary>
public class EvalDirector
{
    public EvalDirector()
    {
        //
        // TODO: Add constructor logic here
        //
    }


    /// <summary>
    /// Function is called from front end to add evaluation
    /// data to SQL Server
    /// </summary>
    public bool AddEvaulation(Evaluation evaluation) //change name later??
    {
        bool Confirmation = false;
        CController Controller = new CController();

        Confirmation = Controller.CreateEvaluation(evaluation);

        return Confirmation;
    }


}