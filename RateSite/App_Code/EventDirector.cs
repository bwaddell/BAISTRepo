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

    public string GenKey(int size)
    {
        List<string> usedKeys = GetEventKeys();
        string key;

        //regenerate key if it matches any currently used keys
        //ZZZZ is default for closed events
        do
        {
            key = CreateEventKey(size);
        }
        while (usedKeys.Contains(key) || key == "ZZZZ");

        return key;
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

    public Event CreateEvent(Event Created)
    {
        Event newEvent = new Event();
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
            AddParameter.ParameterName = "@EventID";
            AddParameter.SqlDbType = SqlDbType.Int;
            AddParameter.Direction = ParameterDirection.Output;
            CommandAdd.Parameters.Add(AddParameter);

            AddParameter = new SqlParameter();
            AddParameter.ParameterName = "@EventKey";
            AddParameter.SqlDbType = SqlDbType.NVarChar;
            AddParameter.Direction = ParameterDirection.Input;
            AddParameter.Value = Created.EventKey;
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

            AddParameter = new SqlParameter();
            AddParameter.ParameterName = "@OpenMsg";
            AddParameter.SqlDbType = SqlDbType.NVarChar;
            AddParameter.Direction = ParameterDirection.Input;
            AddParameter.Value = Created.OpenMsg;
            CommandAdd.Parameters.Add(AddParameter);

            AddParameter = new SqlParameter();
            AddParameter.ParameterName = "@CloseMsg";
            AddParameter.SqlDbType = SqlDbType.NVarChar;
            AddParameter.Direction = ParameterDirection.Input;
            AddParameter.Value = Created.CloseMsg;
            CommandAdd.Parameters.Add(AddParameter);

            AddParameter = new SqlParameter();
            AddParameter.ParameterName = "@VotingCrit";
            AddParameter.SqlDbType = SqlDbType.NVarChar;
            AddParameter.Direction = ParameterDirection.Input;
            AddParameter.Value = Created.VotingCrit;
            CommandAdd.Parameters.Add(AddParameter);

            numRows = CommandAdd.ExecuteNonQuery(); //Number of rows affected

            if (numRows == 1)
            {
                newEvent.EventID = Convert.ToInt32(CommandAdd.Parameters["@EventID"].Value);
            }
            else
            {
                newEvent.EventID = -1;
            }
        }
        catch (Exception)
        {
            newEvent.EventID = -1;
        }
        finally
        {
            DataBaseCon.Close();
        }

        return newEvent;
    }

    public bool UpdateEventInfo(Event updated)
    {
        bool success;
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
            CommandAdd.CommandText = "UpdateEvent";

            SqlParameter AddParameter = new SqlParameter();
            AddParameter.ParameterName = "@EventID";
            AddParameter.SqlDbType = SqlDbType.Int;
            AddParameter.Direction = ParameterDirection.Input;
            AddParameter.Value = updated.EventID;
            CommandAdd.Parameters.Add(AddParameter);

            AddParameter = new SqlParameter();
            AddParameter.ParameterName = "@EventKey";
            AddParameter.SqlDbType = SqlDbType.NVarChar;
            AddParameter.Direction = ParameterDirection.Input;
            AddParameter.Value = updated.EventKey;
            CommandAdd.Parameters.Add(AddParameter);

            AddParameter = new SqlParameter();
            AddParameter.ParameterName = "@Facilitator";
            AddParameter.SqlDbType = SqlDbType.Int;
            AddParameter.Direction = ParameterDirection.Input;
            AddParameter.Value = updated.FacilitatorID;
            CommandAdd.Parameters.Add(AddParameter);

            AddParameter = new SqlParameter();
            AddParameter.ParameterName = "@Location";
            AddParameter.SqlDbType = SqlDbType.NVarChar;
            AddParameter.Direction = ParameterDirection.Input;
            AddParameter.Value = updated.Location;
            CommandAdd.Parameters.Add(AddParameter);

            AddParameter = new SqlParameter();
            AddParameter.ParameterName = "@Performer";
            AddParameter.SqlDbType = SqlDbType.NVarChar;
            AddParameter.Direction = ParameterDirection.Input;
            AddParameter.Value = updated.Performer;
            CommandAdd.Parameters.Add(AddParameter);

            AddParameter = new SqlParameter();
            AddParameter.ParameterName = "@NatureOfEvent";
            AddParameter.SqlDbType = SqlDbType.NVarChar;
            AddParameter.Direction = ParameterDirection.Input;
            AddParameter.Value = updated.Description;
            CommandAdd.Parameters.Add(AddParameter);

            AddParameter = new SqlParameter();
            AddParameter.ParameterName = "@EventDate";
            AddParameter.SqlDbType = SqlDbType.NVarChar;
            AddParameter.Direction = ParameterDirection.Input;
            AddParameter.Value = updated.Date.ToShortDateString();
            CommandAdd.Parameters.Add(AddParameter);

            AddParameter = new SqlParameter();
            AddParameter.ParameterName = "@OpenMsg";
            AddParameter.SqlDbType = SqlDbType.NVarChar;
            AddParameter.Direction = ParameterDirection.Input;
            AddParameter.Value = updated.OpenMsg;
            CommandAdd.Parameters.Add(AddParameter);

            AddParameter = new SqlParameter();
            AddParameter.ParameterName = "@CloseMsg";
            AddParameter.SqlDbType = SqlDbType.NVarChar;
            AddParameter.Direction = ParameterDirection.Input;
            AddParameter.Value = updated.CloseMsg;
            CommandAdd.Parameters.Add(AddParameter);

            AddParameter = new SqlParameter();
            AddParameter.ParameterName = "@VotingCrit";
            AddParameter.SqlDbType = SqlDbType.NVarChar;
            AddParameter.Direction = ParameterDirection.Input;
            AddParameter.Value = updated.VotingCrit;
            CommandAdd.Parameters.Add(AddParameter);

            CommandAdd.ExecuteNonQuery(); //Number of rows affected

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
            if (updatedEvent.EventEnd == Convert.ToDateTime("1800-01-01 12:00:00 PM"))
                AddParameter.Value = Convert.ToDateTime("1800-01-01 12:00:00 PM");
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
        //EvaluationDirector evalD = new EvaluationDirector();
        ConnectionStringSettings webSettings = ConfigurationManager.ConnectionStrings["localdb"];
        SqlConnection DataBaseCon = new SqlConnection(webSettings.ConnectionString);
        DataBaseCon.ConnectionString = webSettings.ConnectionString;
        SqlDataReader eventReader = null;
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

            eventReader = CommandGet.ExecuteReader();

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


                if (eventReader["EventBegin"].ToString() == "1800-01-01 12:00:00 PM")
                    foundEvent.EventStart = Convert.ToDateTime("1800-01-01 12:00:00 PM", culture);
                else
                    foundEvent.EventStart = Convert.ToDateTime(eventReader["EventBegin"], culture).ToLocalTime();

                if (eventReader["EventEnd"].ToString() == "1800-01-01 12:00:00 PM")
                    foundEvent.EventEnd = Convert.ToDateTime("1800-01-01 12:00:00 PM", culture);
                else
                    foundEvent.EventEnd = Convert.ToDateTime(eventReader["EventEnd"], culture).ToLocalTime();

                //call get evaluators for event

                foundEvent.OpenMsg = eventReader["OpeningMessage"].ToString();
                foundEvent.CloseMsg = eventReader["ClosingMessage"].ToString();
                foundEvent.VotingCrit = eventReader["VotingCrit"].ToString();

                //foundEvent.Evaluators = GetEvaluatorsForEvent(foundEvent.EventID);
            }
        }
        catch (Exception)
        {
        }
        finally
        {
            if (eventReader != null)
                eventReader.Close();
            DataBaseCon.Close();
        }

        return foundEvent;
    }

    public Event GetEventFromKey(string key)
    {
        Event foundEvent;
        foundEvent = new Event();

        ConnectionStringSettings webSettings = ConfigurationManager.ConnectionStrings["localdb"];
        SqlConnection DataBaseCon = new SqlConnection(webSettings.ConnectionString);
        DataBaseCon.ConnectionString = webSettings.ConnectionString;
        SqlDataReader eventReader = null;
        try
        {
            DataBaseCon.Open();

            SqlCommand CommandGet = new SqlCommand();
            CommandGet.Connection = DataBaseCon;
            CommandGet.CommandType = CommandType.StoredProcedure;
            CommandGet.CommandText = "GetEventFromKey";

            SqlParameter GetParameter = new SqlParameter();
            GetParameter.ParameterName = "@EventKey";
            GetParameter.SqlDbType = SqlDbType.NVarChar;
            GetParameter.Direction = ParameterDirection.Input;
            GetParameter.Value = key;
            CommandGet.Parameters.Add(GetParameter);

            eventReader = CommandGet.ExecuteReader();

            if (eventReader.HasRows)
            {
                eventReader.Read();

                foundEvent.EventID = Convert.ToInt32(eventReader["EventID"]);
                foundEvent.EventKey = key;
                foundEvent.Date = Convert.ToDateTime(eventReader["EventDate"], culture);
                foundEvent.Description = eventReader["NatureOfEvent"].ToString();
                foundEvent.FacilitatorID = Convert.ToInt32(eventReader["FacilitatorID"]);
                foundEvent.Performer = eventReader["Performer"].ToString();
                foundEvent.Location = eventReader["Location"].ToString();


                if (eventReader["EventBegin"].ToString() == "1800-01-01 12:00:00 PM")
                    foundEvent.EventStart = Convert.ToDateTime("1800-01-01 12:00:00 PM", culture);
                else
                    foundEvent.EventStart = Convert.ToDateTime(eventReader["EventBegin"], culture).ToLocalTime();

                if (eventReader["EventEnd"].ToString() == "1800-01-01 12:00:00 PM")
                    foundEvent.EventEnd = Convert.ToDateTime("1800-01-01 12:00:00 PM", culture);
                else
                    foundEvent.EventEnd = Convert.ToDateTime(eventReader["EventEnd"], culture).ToLocalTime();

                //call get evaluators for event

                foundEvent.OpenMsg = eventReader["OpeningMessage"].ToString();
                foundEvent.CloseMsg = eventReader["ClosingMessage"].ToString();
                foundEvent.VotingCrit = eventReader["VotingCrit"].ToString();
            }
        }
        catch (Exception)
        {
        }
        finally
        {
            if (eventReader != null)
                eventReader.Close();
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

        try
        {
            DataBaseCon.Open();

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
       
            int numRows = CommandAdd.ExecuteNonQuery();

            if (numRows >= 1)
                newEvaluator.EvaluatorID = Convert.ToInt32(CommandAdd.Parameters["@EvaluatorID"].Value);
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

    public bool AddQuestion(Question q)
    {
        ConnectionStringSettings webSettings = ConfigurationManager.ConnectionStrings["localdb"];
        SqlConnection DataBaseCon = new SqlConnection(webSettings.ConnectionString);
        DataBaseCon.ConnectionString = webSettings.ConnectionString;

        bool Success;

        try
        {
            DataBaseCon.Open();

            SqlCommand CommandAdd = new SqlCommand();
            CommandAdd.Connection = DataBaseCon;
            CommandAdd.CommandType = CommandType.StoredProcedure;
            CommandAdd.CommandText = "CreateQuestion";

            SqlParameter AddParamater = new SqlParameter();
            AddParamater.ParameterName = "@EventID";
            AddParamater.SqlDbType = SqlDbType.Int;
            AddParamater.Direction = ParameterDirection.Input;
            AddParamater.Value = q.EventID;
            CommandAdd.Parameters.Add(AddParamater);

            AddParamater = new SqlParameter();
            AddParamater.ParameterName = "@Question";
            AddParamater.SqlDbType = SqlDbType.NVarChar;
            AddParamater.Direction = ParameterDirection.Input;
            AddParamater.Value = q.QuestionText;
            CommandAdd.Parameters.Add(AddParamater);

            CommandAdd.ExecuteNonQuery();

            Success = true;
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

    public List<Question> GetQuestions(int eventID)
    {
        ConnectionStringSettings webSettings = ConfigurationManager.ConnectionStrings["localdb"];
        SqlConnection DataBaseCon = new SqlConnection(webSettings.ConnectionString);
        DataBaseCon.ConnectionString = webSettings.ConnectionString;
        SqlDataReader qReader = null;
        List<Question> questions = new List<Question>();

        try
        {
            DataBaseCon.Open();

            SqlCommand CommandGet = new SqlCommand();
            CommandGet.Connection = DataBaseCon;
            CommandGet.CommandType = CommandType.StoredProcedure;
            CommandGet.CommandText = "GetQuestions";

            SqlParameter AddParamater = new SqlParameter();
            AddParamater.ParameterName = "@EventID";
            AddParamater.SqlDbType = SqlDbType.Int;
            AddParamater.Direction = ParameterDirection.Input;
            AddParamater.Value = eventID;
            CommandGet.Parameters.Add(AddParamater);

            qReader = CommandGet.ExecuteReader();

            if (qReader.HasRows)
            {
                Question q;

                while (qReader.Read())
                {
                    q = new Question();

                    q.EventID = eventID;
                    q.QID = Convert.ToInt32(qReader["QID"]);
                    q.QuestionText = qReader["Question"].ToString();

                    questions.Add(q);
                }
            }
        }
        catch (Exception)
        {
        }
        finally
        {
            if (qReader != null)
                qReader.Close();
            DataBaseCon.Close();
        }
        return questions;
    }

    public bool AddResponse(Question r)
    {
        ConnectionStringSettings webSettings = ConfigurationManager.ConnectionStrings["localdb"];
        SqlConnection DataBaseCon = new SqlConnection(webSettings.ConnectionString);
        DataBaseCon.ConnectionString = webSettings.ConnectionString;

        bool Success;

        try
        {
            DataBaseCon.Open();

            SqlCommand CommandAdd = new SqlCommand();
            CommandAdd.Connection = DataBaseCon;
            CommandAdd.CommandType = CommandType.StoredProcedure;
            CommandAdd.CommandText = "AnswerQuestion";

            SqlParameter AddParamater = new SqlParameter();
            AddParamater.ParameterName = "@QID";
            AddParamater.SqlDbType = SqlDbType.Int;
            AddParamater.Direction = ParameterDirection.Input;
            AddParamater.Value = r.QID;
            CommandAdd.Parameters.Add(AddParamater);

            AddParamater = new SqlParameter();
            AddParamater.ParameterName = "@EvaluatorID";
            AddParamater.SqlDbType = SqlDbType.Int;
            AddParamater.Direction = ParameterDirection.Input;
            AddParamater.Value =  r.EvaluatorID;
            CommandAdd.Parameters.Add(AddParamater);

            AddParamater = new SqlParameter();
            AddParamater.ParameterName = "@Response";
            AddParamater.SqlDbType = SqlDbType.NVarChar;
            AddParamater.Direction = ParameterDirection.Input;
            AddParamater.Value = r.ResponseText;
            CommandAdd.Parameters.Add(AddParamater);

            CommandAdd.ExecuteNonQuery();

            Success = true;
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

    public Question GetResponse(Question q)
    {
        ConnectionStringSettings webSettings = ConfigurationManager.ConnectionStrings["localdb"];
        SqlConnection DataBaseCon = new SqlConnection(webSettings.ConnectionString);
        DataBaseCon.ConnectionString = webSettings.ConnectionString;
        SqlDataReader qReader = null;
        Question question = new Question();

        try
        {
            DataBaseCon.Open();

            SqlCommand CommandGet = new SqlCommand();
            CommandGet.Connection = DataBaseCon;
            CommandGet.CommandType = CommandType.StoredProcedure;
            CommandGet.CommandText = "GetResponse";

            SqlParameter AddParamater = new SqlParameter();
            AddParamater.ParameterName = "@QID";
            AddParamater.SqlDbType = SqlDbType.Int;
            AddParamater.Direction = ParameterDirection.Input;
            AddParamater.Value = q.QID;
            CommandGet.Parameters.Add(AddParamater);

            AddParamater = new SqlParameter();
            AddParamater.ParameterName = "@EvaluatorID";
            AddParamater.SqlDbType = SqlDbType.Int;
            AddParamater.Direction = ParameterDirection.Input;
            AddParamater.Value = q.EvaluatorID;
            CommandGet.Parameters.Add(AddParamater);

            qReader = CommandGet.ExecuteReader();

            if (qReader.HasRows)
            {
                qReader.Read();

                question.QID = q.QID;
                question.EvaluatorID = q.EvaluatorID;
                question.ResponseText = qReader["Response"].ToString();

            }
        }
        catch (Exception)
        {
        }
        finally
        {
            if (qReader != null)
                qReader.Close();
            DataBaseCon.Close();
        }
        return question;
    }

    public List<string> GetEventKeys()
    {
        ConnectionStringSettings webSettings = ConfigurationManager.ConnectionStrings["localdb"];
        SqlConnection DataBaseCon = new SqlConnection(webSettings.ConnectionString);
        DataBaseCon.ConnectionString = webSettings.ConnectionString;

        SqlDataReader qReader = null;

        List<string> keys = new List<string>();

        try
        {
            DataBaseCon.Open();

            SqlCommand CommandGet = new SqlCommand();
            CommandGet.Connection = DataBaseCon;
            CommandGet.CommandType = CommandType.StoredProcedure;
            CommandGet.CommandText = "GetEventKeys";

            qReader = CommandGet.ExecuteReader();

            if (qReader.HasRows)
            {
                string k;

                while (qReader.Read())
                {
                    k = qReader["EventKey"].ToString();

                    keys.Add(k);
                }
            }
        }
        catch (Exception)
        {
        }
        finally
        {
            if (qReader != null)
                qReader.Close();
            DataBaseCon.Close();
        }
        return keys;
    }

    public bool DeleteQuestion(Question q)
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
            CommandAdd.CommandText = "DeleteQuestion";

            SqlParameter AddParamater = new SqlParameter();
            AddParamater.ParameterName = "@QID";
            AddParamater.SqlDbType = SqlDbType.Int;
            AddParamater.Direction = ParameterDirection.Input;
            AddParamater.Value = q.QID;
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
