using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;


/// <summary>
/// Summary description for EventDirector
/// </summary>
public class EventDirector
{
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

    public bool UpdateEvent(Event updatedEvent)
    {

        bool Success = false;
        int numRows = 0;
        ConnectionStringSettings webSettings = ConfigurationManager.ConnectionStrings["localdb"];
        SqlConnection DataBaseCon = new SqlConnection(webSettings.ConnectionString);

        //add code for sql stored proc for adding eval data to sql server


        SqlCommand CommandAdd = new SqlCommand();
        CommandAdd.Connection = DataBaseCon;
        CommandAdd.CommandType = CommandType.StoredProcedure;
        CommandAdd.CommandText = "UpdateEventStatus";

        SqlParameter AddParameter = new SqlParameter();
        AddParameter.ParameterName = "@EventID";
        AddParameter.SqlDbType = SqlDbType.VarChar;
        AddParameter.Direction = ParameterDirection.Input;
        AddParameter.Value = updatedEvent.EventID;
        CommandAdd.Parameters.Add(AddParameter);

        //AddParameter = new SqlParameter();
        //AddParameter.ParameterName = "@FacilitatorID";
        //AddParameter.SqlDbType = SqlDbType.Int;
        //AddParameter.Direction = ParameterDirection.Input;
        //AddParameter.Value = updatedEvent.FacilitatorID;
        //CommandAdd.Parameters.Add(AddParameter);

        //AddParameter = new SqlParameter();
        //AddParameter.ParameterName = "@Location";
        //AddParameter.SqlDbType = SqlDbType.VarChar;
        //AddParameter.Direction = ParameterDirection.Input;
        //AddParameter.Value = updatedEvent.Location;
        //CommandAdd.Parameters.Add(AddParameter);

        //AddParameter = new SqlParameter();
        //AddParameter.ParameterName = "@Performer";
        //AddParameter.SqlDbType = SqlDbType.VarChar;
        //AddParameter.Direction = ParameterDirection.Input;
        //AddParameter.Value = updatedEvent.Performer;
        //CommandAdd.Parameters.Add(AddParameter);

        //AddParameter = new SqlParameter();
        //AddParameter.ParameterName = "@NatureOfPerformance";
        //AddParameter.SqlDbType = SqlDbType.VarChar;
        //AddParameter.Direction = ParameterDirection.Input;
        //AddParameter.Value = updatedEvent.Description;
        //CommandAdd.Parameters.Add(AddParameter);

        //AddParameter = new SqlParameter();
        //AddParameter.ParameterName = "@EventDate";
        //AddParameter.SqlDbType = SqlDbType.Date;
        //AddParameter.Direction = ParameterDirection.Input;
        //AddParameter.Value = DateTime.Today;           //help
        //CommandAdd.Parameters.Add(AddParameter);

        AddParameter = new SqlParameter();
        AddParameter.ParameterName = "@EventStart";
        AddParameter.SqlDbType = SqlDbType.Date;
        AddParameter.Direction = ParameterDirection.Input;
        AddParameter.Value = updatedEvent.EventStart;           //help
        CommandAdd.Parameters.Add(AddParameter);

        AddParameter = new SqlParameter();
        AddParameter.ParameterName = "@EventFinish";
        AddParameter.SqlDbType = SqlDbType.Date;
        AddParameter.Direction = ParameterDirection.Input;
        AddParameter.Value = updatedEvent.EventEnd;           //help
        CommandAdd.Parameters.Add(AddParameter);



        //try
        //{
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


    public Event GetEvent(string eventID)
    {
        Event foundEvent;
        foundEvent = new Event();
        int numRows = 0;

        ConnectionStringSettings webSettings = ConfigurationManager.ConnectionStrings["localdb"];
        SqlConnection DataBaseCon = new SqlConnection(webSettings.ConnectionString);

        SqlCommand CommandGet = new SqlCommand();
        CommandGet.Connection = DataBaseCon;
        CommandGet.CommandType = CommandType.StoredProcedure;
        CommandGet.CommandText = "GetEvent";

        SqlParameter GetParameter = new SqlParameter();
        GetParameter.ParameterName = "@EventKey";
        GetParameter.SqlDbType = SqlDbType.NVarChar;
        GetParameter.Direction = ParameterDirection.Input;
        GetParameter.Value = eventID;
        CommandGet.Parameters.Add(GetParameter);

        //try
        //{
        DataBaseCon.Open();

        SqlDataReader eventReader = CommandGet.ExecuteReader();

        if (eventReader.HasRows)
        {
            eventReader.Read();
            
            foundEvent.EventID = eventID;
            foundEvent.Date = (DateTime)eventReader["EventDate"];
            foundEvent.Description = eventReader["NatureOfEvent"].ToString();
            foundEvent.FacilitatorID = Convert.ToInt32(eventReader["FacilitatorID"]);
            foundEvent.Performer = eventReader["Performer"].ToString();

            if (eventReader["EventBegin"] != DBNull.Value)
            {
                foundEvent.EventStart = (DateTime)eventReader["EventBegin"];
            }

            if (eventReader["EventEnd"] != DBNull.Value)
            {
                foundEvent.EventEnd = (DateTime)eventReader["EventEnd"];
            }


            //call get evaluators for event

            foundEvent.Evaluators = GetEvaluatorsForEvent(foundEvent.EventID);



            //foreach evaluator in Event -> get Evaluation data
            foreach (Evaluator evaluators in foundEvent.Evaluators)
            {
                //GetEvaluationForEventEvaluator(EventID,EvaluatorID);
            }





        }

        eventReader.Close();
        //}
        //catch (Exception ex)
        //{

        //}
        //finally
        //{
        DataBaseCon.Close();
        //}

        return foundEvent;
    }


    public List<Evaluator> GetEvaluatorsForEvent(string eventID)              //Evaluator Director???
    {
        List<Evaluator> listOfEvaluators = new List<Evaluator>();
        //foundEvent.EventID = eventID;
        int numRows = 0;
        EvaluationDirector evalD = new EvaluationDirector();


        ConnectionStringSettings webSettings = ConfigurationManager.ConnectionStrings["localdb"];
        SqlConnection DataBaseCon = new SqlConnection(webSettings.ConnectionString);

        SqlCommand CommandGet = new SqlCommand();
        CommandGet.Connection = DataBaseCon;
        CommandGet.CommandType = CommandType.StoredProcedure;
        CommandGet.CommandText = "GetEventEvaluators";

        SqlParameter GetParameter = new SqlParameter();
        GetParameter.ParameterName = "@EventKey";
        GetParameter.SqlDbType = SqlDbType.NVarChar;
        GetParameter.Direction = ParameterDirection.Input;
        GetParameter.Value = eventID;
        CommandGet.Parameters.Add(GetParameter);


        DataBaseCon.Open();

        DataSet myDataSet = new DataSet();
        myDataSet.DataSetName = "GetEvaluators";
        myDataSet.Tables.Add("Evaluators");

        SqlDataAdapter myDataAdapter = new SqlDataAdapter();
        myDataAdapter.SelectCommand = CommandGet;
        myDataAdapter.Fill(myDataSet, "Evaluators");

        DataBaseCon.Close();
         
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
            if (dataRow["DateOfBirth"] != DBNull.Value)
            {
                evaluator.DateOfBirth = DateTime.Parse(dataRow["DateOfBirth"].ToString());
            }
            if (dataRow["Sex"] != DBNull.Value)
            {
                evaluator.Sex = dataRow["Sex"].ToString();
            }
            if (dataRow["SchoolOrOrganization"] != DBNull.Value)
            {
                evaluator.School = dataRow["SchoolOrOrganization"].ToString();
            }
            if (dataRow["City"] != DBNull.Value)
            {
                evaluator.City = dataRow["City"].ToString();
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

        return listOfEvaluators;

    }


    

    public Evaluator CreateEvaluator()
    {
        Evaluator newEvaluator = new Evaluator();

        ConnectionStringSettings webSettings = ConfigurationManager.ConnectionStrings["localdb"];
        SqlConnection DataBaseCon = new SqlConnection(webSettings.ConnectionString);

        SqlCommand CommandAdd = new SqlCommand();
        CommandAdd.Connection = DataBaseCon;
        CommandAdd.CommandType = CommandType.StoredProcedure;
        CommandAdd.CommandText = "CreateEvaluator";

        SqlParameter AddParameter = new SqlParameter();
        AddParameter.ParameterName = "@EvaluatorID";
        AddParameter.SqlDbType = SqlDbType.Int;
        AddParameter.Direction = ParameterDirection.Output;
        CommandAdd.Parameters.Add(AddParameter);

        //try
        //{
        DataBaseCon.Open();

        int numRows = CommandAdd.ExecuteNonQuery();

        if (numRows >= 1)
        {
            newEvaluator.EvaluatorID = Convert.ToInt32(CommandAdd.Parameters["@EvaluatorID"].Value);
        }
        //}
        //catch (Exception)
        //{

        //}
        //finally
        //{
        DataBaseCon.Close();
        //}

        return newEvaluator;
    }



}