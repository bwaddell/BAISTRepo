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
        AddParameter.ParameterName = "@TimeOfData";
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


        //try
        //{
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
        //}
        //catch (Exception ex)
        //{
        //    Success = false;
        //}
        //finally
        //{
        DataBaseCon.Close();
        //}

        return Success;
    }

    public List<Evaluation> GetCurrentEventData(Event currentEvent)
    {
        List<Evaluation> evals = new List<Evaluation>();

        ConnectionStringSettings webSettings = ConfigurationManager.ConnectionStrings["localdb"];
        SqlConnection DataBaseCon = new SqlConnection(webSettings.ConnectionString);

        SqlCommand CommandGet = new SqlCommand();
        CommandGet.Connection = DataBaseCon;
        CommandGet.CommandType = CommandType.StoredProcedure;
        CommandGet.CommandText = "GetMostRecentEvaluativeData";

        SqlParameter AddParameter = new SqlParameter();
        AddParameter.ParameterName = "@EventID";
        AddParameter.SqlDbType = SqlDbType.Int;
        AddParameter.Direction = ParameterDirection.Input;
        AddParameter.Value = currentEvent.EventID;
        CommandGet.Parameters.Add(AddParameter);

        DataBaseCon.Open();
        SqlDataReader eventReader = CommandGet.ExecuteReader();

        if (eventReader.HasRows)
        {
            Evaluation eval;

            while (eventReader.Read())
            {
                eval = new Evaluation();

                eval.EvaluatorID = (int)eventReader["EvaluatorID"];
                eval.EventID = currentEvent.EventID;
                eval.Rating = (int)eventReader["Rating"];
                eval.TimeStamp = (DateTime)eventReader["TimeOfData"];

                evals.Add(eval);
            }
        }
        eventReader.Close();
        DataBaseCon.Close();

        return evals;
    }

    public List<Evaluation> GetAllEventData(int EventID)
    {
        List<Evaluation> eventData = new List<Evaluation>();

        ConnectionStringSettings webSettings = ConfigurationManager.ConnectionStrings["localdb"];
        SqlConnection DataBaseCon = new SqlConnection(webSettings.ConnectionString);

        SqlCommand CommandGet = new SqlCommand();
        CommandGet.Connection = DataBaseCon;
        CommandGet.CommandType = CommandType.StoredProcedure;
        CommandGet.CommandText = "GetAllEventData";

        SqlParameter AddParameter = new SqlParameter();
        AddParameter.ParameterName = "@EventID";
        AddParameter.SqlDbType = SqlDbType.Int;
        AddParameter.Direction = ParameterDirection.Input;
        AddParameter.Value = EventID;
        CommandGet.Parameters.Add(AddParameter);

        DataBaseCon.Open();
        SqlDataReader eventReader = CommandGet.ExecuteReader();

        if (eventReader.HasRows)
        {
            Evaluation eval;

            while (eventReader.Read())
            {
                eval = new Evaluation();

                eval.EvaluatorID = (int)eventReader["EvaluatorID"];
                eval.EventID = EventID;
                eval.Rating = (int)eventReader["Rating"];
                eval.TimeStamp = (DateTime)eventReader["TimeOfData"];

                eventData.Add(eval);
            }
        }
        eventReader.Close();
        DataBaseCon.Close();

        return eventData;
    }


    public List<Evaluation> GetEvaluationsForEventEvaluator(int EventID, int EvaluatorID)
    {
        //Evaluator EvaluData = new Evaluator();

        //list of Evaluations
        List<Evaluation> liOfEvaluations = new List<Evaluation>();


        ConnectionStringSettings webSettings = ConfigurationManager.ConnectionStrings["localdb"];
        SqlConnection DataBaseCon = new SqlConnection(webSettings.ConnectionString);

        SqlCommand CommandGet = new SqlCommand();
        CommandGet.Connection = DataBaseCon;
        CommandGet.CommandType = CommandType.StoredProcedure;
        CommandGet.CommandText = "GetEvaluatorEventData";

        SqlParameter AddParameter = new SqlParameter();
        AddParameter.ParameterName = "@EventID";
        AddParameter.SqlDbType = SqlDbType.Int;
        AddParameter.Direction = ParameterDirection.Input;
        AddParameter.Value = EventID;
        CommandGet.Parameters.Add(AddParameter);

        AddParameter = new SqlParameter();
        AddParameter.ParameterName = "@EvaluatorID";
        AddParameter.SqlDbType = SqlDbType.Int;
        AddParameter.Direction = ParameterDirection.Input;
        AddParameter.Value = EvaluatorID;
        CommandGet.Parameters.Add(AddParameter);

        DataBaseCon.Open();

        DataSet myDataSet = new DataSet();
        myDataSet.DataSetName = "GetEvaluations";
        myDataSet.Tables.Add("Evaluations");

        SqlDataAdapter myDataAdapter = new SqlDataAdapter();
        myDataAdapter.SelectCommand = CommandGet;
        myDataAdapter.Fill(myDataSet, "Evaluations");

        DataBaseCon.Close();

        DataTable myDataTable = new DataTable();
        myDataTable = myDataSet.Tables["Evaluations"];


        foreach (DataRow dataRow in myDataTable.Rows)
        {
            Evaluation evaluation = new Evaluation();

            evaluation.EventID = Convert.ToInt32(dataRow["EventID"]);
            evaluation.EvaluatorID = Convert.ToInt32(dataRow["EvaluatorID"]);
            evaluation.TimeStamp = DateTime.Parse(dataRow["TimeOfData"].ToString());
            evaluation.Rating = Convert.ToInt32(dataRow["Rating"]);
            liOfEvaluations.Add(evaluation);

        }

        return liOfEvaluations;
    }

    public bool DeleteEvaluatorEventData(Event eve, Evaluator eva)
    {
        ConnectionStringSettings webSettings = ConfigurationManager.ConnectionStrings["localdb"];
        SqlConnection DataBaseCon = new SqlConnection(webSettings.ConnectionString);
        DataBaseCon.ConnectionString = webSettings.ConnectionString;

        bool success;
        try
        {
            DataBaseCon.Open();

            SqlCommand CommandAdd = new SqlCommand();
            CommandAdd.Connection = DataBaseCon;
            CommandAdd.CommandType = CommandType.StoredProcedure;
            CommandAdd.CommandText = "DeleteEvaluatorEventData";

            SqlParameter AddParamater = new SqlParameter();
            AddParamater.ParameterName = "@EventID";
            AddParamater.SqlDbType = SqlDbType.Int;
            AddParamater.Direction = ParameterDirection.Input;
            AddParamater.Value = eve.EventID;
            CommandAdd.Parameters.Add(AddParamater);

            AddParamater = new SqlParameter();
            AddParamater.ParameterName = "@EvaluatorID";
            AddParamater.SqlDbType = SqlDbType.Int;
            AddParamater.Direction = ParameterDirection.Input;
            AddParamater.Value = eva.EvaluatorID;
            CommandAdd.Parameters.Add(AddParamater);

            CommandAdd.ExecuteNonQuery();

            success = true;
        }
        catch (Exception)
        {
            success = false;
        }
        finally
        {
            DataBaseCon.Close();
        }
        return success;
    }

    public bool DeleteEventData(Event eve)
    {
        ConnectionStringSettings webSettings = ConfigurationManager.ConnectionStrings["localdb"];
        SqlConnection DataBaseCon = new SqlConnection(webSettings.ConnectionString);
        DataBaseCon.ConnectionString = webSettings.ConnectionString;

        bool success;
        try
        {
            DataBaseCon.Open();

            SqlCommand CommandAdd = new SqlCommand();
            CommandAdd.Connection = DataBaseCon;
            CommandAdd.CommandType = CommandType.StoredProcedure;
            CommandAdd.CommandText = "DeleteEventData";

            SqlParameter AddParamater = new SqlParameter();
            AddParamater.ParameterName = "@EventID";
            AddParamater.SqlDbType = SqlDbType.Int;
            AddParamater.Direction = ParameterDirection.Input;
            AddParamater.Value = eve.EventID;
            CommandAdd.Parameters.Add(AddParamater);

            CommandAdd.ExecuteNonQuery();

            success = true;
        }
        catch (Exception)
        {
            success = false;
        }
        finally
        {
            DataBaseCon.Close();
        }
        return success;
    }
}