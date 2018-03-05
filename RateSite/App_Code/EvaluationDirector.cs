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
            numRows = CommandAdd.ExecuteNonQuery(); //Number of rows affected

            if (numRows == 1)
            {
                Success = true;//if 1, all is good
            }
            else
            {
                Success = false; //otherwise, not good
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

    public bool CreateEvent(Event Created)
    {
        bool Success = false;
        int numRows = 0;
        ConnectionStringSettings webSettings = ConfigurationManager.ConnectionStrings["localdb"];
        SqlConnection DataBaseCon = new SqlConnection(webSettings.ConnectionString);

        SqlCommand CommandAdd = new SqlCommand();
        CommandAdd.Connection = DataBaseCon;
        CommandAdd.CommandType = CommandType.StoredProcedure;
        CommandAdd.CommandText = "CreateEvent";

        SqlParameter AddParameter = new SqlParameter();
        AddParameter.ParameterName = "@EventKey";
        AddParameter.SqlDbType = SqlDbType.NVarChar;
        AddParameter.Direction = ParameterDirection.Input;
        AddParameter.Value = Created.EventID;
        CommandAdd.Parameters.Add(AddParameter);

        AddParameter = new SqlParameter();
        AddParameter.ParameterName = "@Facilitator";
        AddParameter.SqlDbType = SqlDbType.Int;
        AddParameter.Direction = ParameterDirection.Input;
        AddParameter.Value = Created.FacilitatorID;
        CommandAdd.Parameters.Add(AddParameter);

        AddParameter = new SqlParameter();
        AddParameter.ParameterName = "@Location";
        AddParameter.SqlDbType = SqlDbType.NVarChar;
        AddParameter.Direction = ParameterDirection.Input;
        AddParameter.Value = Created.Location;
        CommandAdd.Parameters.Add(AddParameter);

        AddParameter = new SqlParameter();
        AddParameter.ParameterName = "@Performer";
        AddParameter.SqlDbType = SqlDbType.NVarChar;
        AddParameter.Direction = ParameterDirection.Input;
        AddParameter.Value = Created.Performer;
        CommandAdd.Parameters.Add(AddParameter);

        AddParameter = new SqlParameter();
        AddParameter.ParameterName = "@NatureOfEvent";
        AddParameter.SqlDbType = SqlDbType.NVarChar;
        AddParameter.Direction = ParameterDirection.Input;
        AddParameter.Value = Created.Description;
        CommandAdd.Parameters.Add(AddParameter);

        AddParameter = new SqlParameter();
        AddParameter.ParameterName = "@EventDate";
        AddParameter.SqlDbType = SqlDbType.Date;
        AddParameter.Direction = ParameterDirection.Input;
        AddParameter.Value = Created.Date;
        CommandAdd.Parameters.Add(AddParameter);

        try
        {
            DataBaseCon.Open();
            numRows = CommandAdd.ExecuteNonQuery(); //Number of rows affected

            if (numRows == 1)
            {                
                Success = true;//if 1, all is good
            }
            else
            {              
                Success = false; //otherwise, not good
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