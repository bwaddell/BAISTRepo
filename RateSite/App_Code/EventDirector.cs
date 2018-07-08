using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;

/// <summary>
/// Summary description for EventDirector
/// </summary>
public class EventDirector
{

    CultureInfo culture = new CultureInfo("fr-FR");  // , culture

    public EventDirector()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public string CreateEventKey(int size)
    {
        byte[] KeyArr = new Byte[size];
        RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
        string key;
        do
        {
            rng.GetBytes(KeyArr);
            key = Convert.ToBase64String(KeyArr).ToUpper();
            key = key.Replace("=", "");
        } while (key.Contains('/') || key.Contains('+'));

        return key;
    }

    public bool CreateEvent(Event Created)
    {
        bool Success = false;
        int numRows = 0;

        ConnectionStringSettings webSettings = ConfigurationManager.ConnectionStrings["localdb"];
        SqlConnection DataBaseCon = new SqlConnection(webSettings.ConnectionString);
        DataBaseCon.ConnectionString = webSettings.ConnectionString;

        try
        {
            DataBaseCon.Open();

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
            AddParameter.SqlDbType = SqlDbType.NVarChar;
            AddParameter.Direction = ParameterDirection.Input;
            AddParameter.Value = Created.Date.ToShortDateString();
            CommandAdd.Parameters.Add(AddParameter);

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
        catch (Exception)
        {
            Success = false;
        }
        finally
        {
            DataBaseCon.Close();
        }
        return Success;
    }

    public bool UpdateEvent(Event updatedEvent)
    {
        bool Success = false;
        int numRows = 0;

        ConnectionStringSettings webSettings = ConfigurationManager.ConnectionStrings["localdb"];
        SqlConnection DataBaseCon = new SqlConnection(webSettings.ConnectionString);
        DataBaseCon.ConnectionString = webSettings.ConnectionString;

        try
        {
            DataBaseCon.Open();

            SqlCommand CommandAdd = new SqlCommand();
            CommandAdd.Connection = DataBaseCon;
            CommandAdd.CommandType = CommandType.StoredProcedure;
            CommandAdd.CommandText = "UpdateEventStatus";

            SqlParameter AddParameter = new SqlParameter();
            AddParameter.ParameterName = "@EventID";
            AddParameter.SqlDbType = SqlDbType.Int;
            AddParameter.Direction = ParameterDirection.Input;
            AddParameter.Value = updatedEvent.EventID;
            CommandAdd.Parameters.Add(AddParameter);


            AddParameter = new SqlParameter();
            AddParameter.ParameterName = "@EventStart";
            AddParameter.SqlDbType = SqlDbType.NVarChar;
            AddParameter.Direction = ParameterDirection.Input;
            AddParameter.Value = updatedEvent.EventStart.ToUniversalTime().ToString();     
            CommandAdd.Parameters.Add(AddParameter);

            AddParameter = new SqlParameter();
            AddParameter.ParameterName = "@EventFinish";
            AddParameter.SqlDbType = SqlDbType.NVarChar;
            AddParameter.Direction = ParameterDirection.Input;
            if (updatedEvent.EventEnd == Convert.ToDateTime("1/1/1800 12:00:00 PM"))
                AddParameter.Value = Convert.ToDateTime("1/1/1800 12:00:00 PM");
            else
                AddParameter.Value = updatedEvent.EventEnd.ToUniversalTime().ToString();         
            CommandAdd.Parameters.Add(AddParameter);
         
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
        catch (Exception)
        {
            Success = false;
        }
        finally
        {
            DataBaseCon.Close();
        }
        return Success;
    }

    public Event GetEvent(int eventID)
    {
        Event foundEvent;
        foundEvent = new Event();

        ConnectionStringSettings webSettings = ConfigurationManager.ConnectionStrings["localdb"];
        SqlConnection DataBaseCon = new SqlConnection(webSettings.ConnectionString);
        DataBaseCon.ConnectionString = webSettings.ConnectionString;

        try
        {
            DataBaseCon.Open();

            SqlCommand CommandGet = new SqlCommand();
            CommandGet.Connection = DataBaseCon;
            CommandGet.CommandType = CommandType.StoredProcedure;
            CommandGet.CommandText = "GetEvent";

            SqlParameter GetParameter = new SqlParameter();
            GetParameter.ParameterName = "@EventID";
            GetParameter.SqlDbType = SqlDbType.Int;
            GetParameter.Direction = ParameterDirection.Input;
            GetParameter.Value = eventID;
            CommandGet.Parameters.Add(GetParameter);

            SqlDataReader eventReader = CommandGet.ExecuteReader();

            if (eventReader.HasRows)
            {
                eventReader.Read();
         
                foundEvent.EventID = eventID;
                foundEvent.EventKey = eventReader["EventKey"].ToString();
                foundEvent.Date = Convert.ToDateTime(eventReader["EventDate"], culture);
                foundEvent.Description = eventReader["NatureOfEvent"].ToString();
                foundEvent.FacilitatorID = Convert.ToInt32(eventReader["FacilitatorID"]);
                foundEvent.Performer = eventReader["Performer"].ToString();
                foundEvent.Location = eventReader["Location"].ToString();


                if (eventReader["EventBegin"].ToString() == "1/1/1800 12:00:00 PM")
                    foundEvent.EventStart = Convert.ToDateTime("1/1/1800 12:00:00 PM", culture);
                else
                    foundEvent.EventStart = Convert.ToDateTime(eventReader["EventBegin"], culture).ToLocalTime();

                if (eventReader["EventEnd"].ToString() == "1/1/1800 12:00:00 PM")
                    foundEvent.EventEnd = Convert.ToDateTime("1/1/1800 12:00:00 PM", culture);
                else
                    foundEvent.EventEnd = Convert.ToDateTime(eventReader["EventEnd"], culture).ToLocalTime();

                //call get evaluators for event

                foundEvent.Evaluators = GetEvaluatorsForEvent(foundEvent.EventID);



                //foreach evaluator in Event -> get Evaluation data
                foreach (Evaluator evaluators in foundEvent.Evaluators)
                {
                    //GetEvaluationForEventEvaluator(EventID,EvaluatorID);
                }
            }
        }
        catch (Exception)
        {
        }
        finally
        {
            DataBaseCon.Close();
        }

        return foundEvent;
    }

    public List<Evaluator> GetEvaluatorsForEvent(int eventID)         
    {
        List<Evaluator> listOfEvaluators = new List<Evaluator>();
        EvaluationDirector evalD = new EvaluationDirector();

        ConnectionStringSettings webSettings = ConfigurationManager.ConnectionStrings["localdb"];
        SqlConnection DataBaseCon = new SqlConnection(webSettings.ConnectionString);
        DataBaseCon.ConnectionString = webSettings.ConnectionString;

        try
        {
            DataBaseCon.Open();

            SqlCommand CommandGet = new SqlCommand();
            CommandGet.Connection = DataBaseCon;
            CommandGet.CommandType = CommandType.StoredProcedure;
            CommandGet.CommandText = "GetEventEvaluators";

            SqlParameter GetParameter = new SqlParameter();
            GetParameter.ParameterName = "@EventID";
            GetParameter.SqlDbType = SqlDbType.Int;
            GetParameter.Direction = ParameterDirection.Input;
            GetParameter.Value = eventID;
            CommandGet.Parameters.Add(GetParameter);

            DataSet myDataSet = new DataSet();
            myDataSet.DataSetName = "GetEvaluators";
            myDataSet.Tables.Add("Evaluators");

            SqlDataAdapter myDataAdapter = new SqlDataAdapter();
            myDataAdapter.SelectCommand = CommandGet;
            myDataAdapter.Fill(myDataSet, "Evaluators");


            DataTable myDataTable = new DataTable();
            myDataTable = myDataSet.Tables["Evaluators"];

            int numrows = myDataTable.Rows.Count;


            foreach (DataRow dataRow in myDataTable.Rows)
            {
                Evaluator evaluator = new Evaluator();

                evaluator.EvaluatorID = Convert.ToInt32(dataRow["EvaluatorID"]);

                if (dataRow["Name"] != DBNull.Value)
                {
                    evaluator.Name = dataRow["Name"].ToString();
                }
                if (dataRow["VotingCriteria"] != DBNull.Value)
                {
                    evaluator.Criteria = dataRow["VotingCriteria"].ToString();
                }

                listOfEvaluators.Add(evaluator);

                //call GetEvaluationsForEventEvaluator
                evaluator.EvaluatorEvaluations =
                    evalD.GetEvaluationsForEventEvaluator(eventID, evaluator.EvaluatorID);

            }
        }
        catch (Exception)
        {

        }
        finally
        {
            DataBaseCon.Close();
        }
        return listOfEvaluators;
    }

    public Evaluator CreateEvaluator(Evaluator eval)
    {
        Evaluator newEvaluator = new Evaluator();

        ConnectionStringSettings webSettings = ConfigurationManager.ConnectionStrings["localdb"];
        SqlConnection DataBaseCon = new SqlConnection(webSettings.ConnectionString);
        DataBaseCon.ConnectionString = webSettings.ConnectionString;

        bool success;
        try
        {
            SqlCommand CommandAdd = new SqlCommand();
            CommandAdd.Connection = DataBaseCon;
            CommandAdd.CommandType = CommandType.StoredProcedure;
            CommandAdd.CommandText = "CreateEvaluator";

            SqlParameter AddParameter = new SqlParameter();
            AddParameter.ParameterName = "@EvaluatorID";
            AddParameter.SqlDbType = SqlDbType.Int;
            AddParameter.Direction = ParameterDirection.Output;
            CommandAdd.Parameters.Add(AddParameter);

            AddParameter = new SqlParameter();
            AddParameter.ParameterName = "@Name";
            AddParameter.SqlDbType = SqlDbType.NVarChar;
            AddParameter.Direction = ParameterDirection.Input;
            AddParameter.Value = eval.Name;
            CommandAdd.Parameters.Add(AddParameter);

            AddParameter = new SqlParameter();
            AddParameter.ParameterName = "@Criteria";
            AddParameter.SqlDbType = SqlDbType.NVarChar;
            AddParameter.Direction = ParameterDirection.Input;
            AddParameter.Value = eval.Criteria;
            CommandAdd.Parameters.Add(AddParameter);

            DataBaseCon.Open();
       
            int numRows = CommandAdd.ExecuteNonQuery();

            if (numRows >= 1)
                newEvaluator.EvaluatorID = Convert.ToInt32(CommandAdd.Parameters["@EvaluatorID"].Value);

            success = true;
        }
        catch (Exception)
        {

        }
        finally
        {
            DataBaseCon.Close();
        }
        return newEvaluator;




    }

    public bool DeleteEvent(Event ev)
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
            CommandAdd.CommandText = "DeleteEvent";

            SqlParameter AddParamater = new SqlParameter();
            AddParamater.ParameterName = "@EventID";
            AddParamater.SqlDbType = SqlDbType.Int;
            AddParamater.Direction = ParameterDirection.Input;
            AddParamater.Value = ev.EventID;
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