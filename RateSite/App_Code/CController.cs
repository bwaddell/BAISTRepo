using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for CController
/// </summary>
public class CController
{
    public CController()
    {
        //
        // TODO: Add constructor logic here
        //
    }





    /// <summary>
    /// Function is called by the Director to store
    /// data to SQL Server
    /// </summary>
    public bool CreateEvaluation(Evaluation eval)
    {
        bool Success = false;

        ConnectionStringSettings webSettings = ConfigurationManager.ConnectionStrings["localdb"];
        SqlConnection DataBaseCon = new SqlConnection(webSettings.ConnectionString);

        //add code for sql stored proc for adding eval data to sql server


        SqlCommand CommandGet = new SqlCommand();
        CommandGet.Connection = DataBaseCon;
        CommandGet.CommandType = CommandType.StoredProcedure;
        CommandGet.CommandText = "AddEvaluationDataPoint";

        SqlParameter AddParameter = new SqlParameter();
        AddParameter.ParameterName = "@Event";
        AddParameter.SqlDbType = SqlDbType.Int;
        AddParameter.Direction = ParameterDirection.Input;
        AddParameter.Value = eval.EventID;
        CommandGet.Parameters.Add(AddParameter);

        AddParameter = new SqlParameter();
        AddParameter.ParameterName = "@Evaluator";
        AddParameter.SqlDbType = SqlDbType.Int;
        AddParameter.Direction = ParameterDirection.Input;
        AddParameter.Value = eval.EvaluatorID;
        CommandGet.Parameters.Add(AddParameter);

        SqlParameter AddParameter = new SqlParameter();
        AddParameter.ParameterName = "@DataTime";
        AddParameter.SqlDbType = SqlDbType.DateTime;
        AddParameter.Direction = ParameterDirection.Input;
        AddParameter.Value = eval.TimeStamp;
        CommandGet.Parameters.Add(AddParameter);

        SqlParameter AddParameter = new SqlParameter();
        AddParameter.ParameterName = "@Rating";
        AddParameter.SqlDbType = SqlDbType.Int;
        AddParameter.Direction = ParameterDirection.Input;
        AddParameter.Value = eval.Rating;
        CommandGet.Parameters.Add(AddParameter);



        DataBaseCon.Open();

        //execute quary

        DataBaseCon.Close();
        return Success;


    }
}