using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for EvaluationDirector
/// </summary>
public class EvaluationDirector
{
    public EvaluationDirector()
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
        int numRows = 0;
        ConnectionStringSettings webSettings = ConfigurationManager.ConnectionStrings["localdb"];
        SqlConnection DataBaseCon = new SqlConnection(webSettings.ConnectionString);

        //add code for sql stored proc for adding eval data to sql server


        SqlCommand CommandAdd = new SqlCommand();
        CommandAdd.Connection = DataBaseCon;
        CommandAdd.CommandType = CommandType.StoredProcedure;
        CommandAdd.CommandText = "AddEvaluationDataPoint";

        SqlParameter AddParameter = new SqlParameter();
        AddParameter.ParameterName = "@Event";
        AddParameter.SqlDbType = SqlDbType.Int;
        AddParameter.Direction = ParameterDirection.Input;
        AddParameter.Value = eval.EventID;
        CommandAdd.Parameters.Add(AddParameter);

        AddParameter = new SqlParameter();
        AddParameter.ParameterName = "@Evaluator";
        AddParameter.SqlDbType = SqlDbType.Int;
        AddParameter.Direction = ParameterDirection.Input;
        AddParameter.Value = eval.EvaluatorID;
        CommandAdd.Parameters.Add(AddParameter);

        AddParameter = new SqlParameter();
        AddParameter.ParameterName = "@DataTime";
        AddParameter.SqlDbType = SqlDbType.DateTime;
        AddParameter.Direction = ParameterDirection.Input;
        AddParameter.Value = eval.TimeStamp;
        CommandAdd.Parameters.Add(AddParameter);

        AddParameter = new SqlParameter();
        AddParameter.ParameterName = "@Rating";
        AddParameter.SqlDbType = SqlDbType.Int;
        AddParameter.Direction = ParameterDirection.Input;
        AddParameter.Value = eval.Rating;
        CommandAdd.Parameters.Add(AddParameter);


        try
        {
            DataBaseCon.Open();
            //execute quary
            numRows = CommandAdd.ExecuteNonQuery();
            if (numRows == 1)
            {
                //Number of rows affected is 1, all is good
                Success = true;
            }
            else
            {
                //not good
                Success = false;
            }
        }
        catch (Exception ex)
        {
            Success = false;
        }
        finally
        {
            DataBaseCon.Close();


        }


        return Success;

       
    }
}