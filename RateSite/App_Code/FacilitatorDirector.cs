using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data;
using System.Data.SqlClient;
using System.Configuration;


/// <summary>
/// Summary description for FacilitatorDirector
/// </summary>
public class FacilitatorDirector
{
    public FacilitatorDirector()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public Facilitator GetFacilitatorByEmail(string email)
    {
        Facilitator pullFacilitator = new Facilitator();

        try
        {
            ConnectionStringSettings webSettings = ConfigurationManager.ConnectionStrings["localdb"];
            SqlConnection ClubBAISTData = new SqlConnection();
            ClubBAISTData.ConnectionString = webSettings.ConnectionString;
            ClubBAISTData.Open();

            SqlCommand CommandGet = new SqlCommand
            {
                Connection = ClubBAISTData,
                CommandType = CommandType.StoredProcedure,
                CommandText = "GetFacilitatorInfo"
            };

            SqlParameter AddParamater = new SqlParameter
            {
                ParameterName = "@email",
                SqlDbType = SqlDbType.NVarChar,
                Direction = ParameterDirection.Input,
                Value = email
            };
            CommandGet.Parameters.Add(AddParamater);

            SqlDataReader facilitatorDataReader = CommandGet.ExecuteReader();

            if (facilitatorDataReader.HasRows)
            {
                facilitatorDataReader.Read();

                pullFacilitator.Email = email;
                pullFacilitator.FacilitatorID = Convert.ToInt32(facilitatorDataReader["FacilitatorID"]);
                pullFacilitator.Password = facilitatorDataReader["Password"].ToString();
                pullFacilitator.Salt = facilitatorDataReader["Salt"].ToString();
                pullFacilitator.FirstName = facilitatorDataReader["FirstName"].ToString();
                pullFacilitator.LastName = facilitatorDataReader["LastName"].ToString();
                pullFacilitator.Title = facilitatorDataReader["Title"].ToString();
                pullFacilitator.Organization = facilitatorDataReader["Organization"].ToString();
                pullFacilitator.Location = facilitatorDataReader["City"].ToString();
                pullFacilitator.Roles = facilitatorDataReader["Roles"].ToString();
            }

            facilitatorDataReader.Close();
            ClubBAISTData.Close();
        }
        catch (Exception) { }

        return pullFacilitator;
    }

    public bool CreateFacilitator(Facilitator newFacilitator)
    {
        bool Success = false;
        int numRows = 0;
        ConnectionStringSettings webSettings = ConfigurationManager.ConnectionStrings["localdb"];
        SqlConnection DataBaseCon = new SqlConnection(webSettings.ConnectionString);

        SqlCommand CommandAdd = new SqlCommand();
        CommandAdd.Connection = DataBaseCon;
        CommandAdd.CommandType = CommandType.StoredProcedure;
        CommandAdd.CommandText = "CreateFacilitator";

        SqlParameter AddParameter = new SqlParameter();
        AddParameter.ParameterName = "@FirstName";
        AddParameter.SqlDbType = SqlDbType.NVarChar;
        AddParameter.Direction = ParameterDirection.Input;
        AddParameter.Value = newFacilitator.FirstName;
        CommandAdd.Parameters.Add(AddParameter);

        AddParameter = new SqlParameter();
        AddParameter.ParameterName = "@LastName";
        AddParameter.SqlDbType = SqlDbType.NVarChar;
        AddParameter.Direction = ParameterDirection.Input;
        AddParameter.Value = newFacilitator.LastName;
        CommandAdd.Parameters.Add(AddParameter);

        AddParameter = new SqlParameter();
        AddParameter.ParameterName = "@EMail";
        AddParameter.SqlDbType = SqlDbType.NVarChar;
        AddParameter.Direction = ParameterDirection.Input;
        AddParameter.Value = newFacilitator.Email;
        CommandAdd.Parameters.Add(AddParameter);

        AddParameter = new SqlParameter();
        AddParameter.ParameterName = "@Role";
        AddParameter.SqlDbType = SqlDbType.NVarChar;
        AddParameter.Direction = ParameterDirection.Input;
        AddParameter.Value = newFacilitator.Roles;
        CommandAdd.Parameters.Add(AddParameter);

        AddParameter = new SqlParameter();
        AddParameter.ParameterName = "@Password";
        AddParameter.SqlDbType = SqlDbType.NVarChar;
        AddParameter.Direction = ParameterDirection.Input;
        AddParameter.Value = newFacilitator.Password;
        CommandAdd.Parameters.Add(AddParameter);

        AddParameter = new SqlParameter();
        AddParameter.ParameterName = "@Salt";
        AddParameter.SqlDbType = SqlDbType.NVarChar;
        AddParameter.Direction = ParameterDirection.Input;
        AddParameter.Value = newFacilitator.Salt;
        CommandAdd.Parameters.Add(AddParameter);

        AddParameter = new SqlParameter();
        AddParameter.ParameterName = "@Title";
        AddParameter.SqlDbType = SqlDbType.NVarChar;
        AddParameter.Direction = ParameterDirection.Input;
        AddParameter.Value = newFacilitator.Title;
        CommandAdd.Parameters.Add(AddParameter);

        AddParameter = new SqlParameter();
        AddParameter.ParameterName = "@Organization";
        AddParameter.SqlDbType = SqlDbType.NVarChar;
        AddParameter.Direction = ParameterDirection.Input;
        AddParameter.Value = newFacilitator.Organization;
        CommandAdd.Parameters.Add(AddParameter);

        AddParameter = new SqlParameter();
        AddParameter.ParameterName = "@City";
        AddParameter.SqlDbType = SqlDbType.NVarChar;
        AddParameter.Direction = ParameterDirection.Input;
        AddParameter.Value = newFacilitator.Location;
        CommandAdd.Parameters.Add(AddParameter);

        //try
        //{
        DataBaseCon.Open();
        numRows = CommandAdd.ExecuteNonQuery();

        if (numRows == 1)
        {
            Success = true;
        }
        else
        {
            Success = false; 
        }

        DataBaseCon.Close();


        return Success;
    }

    public Facilitator GetFacilitator(int id)
    {
        Facilitator pullFacilitator = new Facilitator();

        try
        {
            ConnectionStringSettings webSettings = ConfigurationManager.ConnectionStrings["localdb"];
            SqlConnection ClubBAISTData = new SqlConnection();
            ClubBAISTData.ConnectionString = webSettings.ConnectionString;
            ClubBAISTData.Open();

            SqlCommand CommandGet = new SqlCommand
            {
                Connection = ClubBAISTData,
                CommandType = CommandType.StoredProcedure,
                CommandText = "GetFacilitator"
            };

            SqlParameter AddParamater = new SqlParameter
            {
                ParameterName = "@facilitatorID",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Input,
                Value = id
            };
            CommandGet.Parameters.Add(AddParamater);

            SqlDataReader facilitatorDataReader = CommandGet.ExecuteReader();

            if (facilitatorDataReader.HasRows)
            {
                facilitatorDataReader.Read();

                pullFacilitator.Email = facilitatorDataReader["EMail"].ToString();
                pullFacilitator.FacilitatorID = id;
                pullFacilitator.Password = facilitatorDataReader["Password"].ToString();
                pullFacilitator.Salt = facilitatorDataReader["Salt"].ToString();
                pullFacilitator.FirstName = facilitatorDataReader["FirstName"].ToString();
                pullFacilitator.LastName = facilitatorDataReader["LastName"].ToString();
                pullFacilitator.Title = facilitatorDataReader["Title"].ToString();
                pullFacilitator.Organization = facilitatorDataReader["Organization"].ToString();
                pullFacilitator.Location = facilitatorDataReader["City"].ToString();
                pullFacilitator.Roles = facilitatorDataReader["Roles"].ToString();
            }

            facilitatorDataReader.Close();
            ClubBAISTData.Close();
        }
        catch (Exception) { }

        return pullFacilitator;
    }

    public List<Event> GetFacilitatorEvents(int id)
    {
        List<Event> FacEvents = new List<Event>();

        try
        {
            ConnectionStringSettings webSettings = ConfigurationManager.ConnectionStrings["localdb"];
            SqlConnection ClubBAISTData = new SqlConnection();
            ClubBAISTData.ConnectionString = webSettings.ConnectionString;
            ClubBAISTData.Open();

            SqlCommand CommandGet = new SqlCommand
            {
                Connection = ClubBAISTData,
                CommandType = CommandType.StoredProcedure,
                CommandText = "GetFacilitatorEvents"
            };

            SqlParameter AddParamater = new SqlParameter
            {
                ParameterName = "@facilitatorID",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Input,
                Value = id
            };
            CommandGet.Parameters.Add(AddParamater);

            SqlDataReader facilitatorDataReader = CommandGet.ExecuteReader();

            if (facilitatorDataReader.HasRows)
            {
                Event facEvent;

                while (facilitatorDataReader.Read())
                {
                    facEvent = new Event();

                    facEvent.EventID = facilitatorDataReader["EventKey"].ToString();
                    facEvent.FacilitatorID = id;
                    facEvent.Location = facilitatorDataReader["Location"].ToString();
                    facEvent.Performer = facilitatorDataReader["Performer"].ToString();
                    facEvent.Description = facilitatorDataReader["NatureOfEvent"].ToString();
                    facEvent.Date = (DateTime)(facilitatorDataReader["EventDate"]);

                    if (facilitatorDataReader["EventBegin"] != DBNull.Value)
                        facEvent.EventStart = (DateTime)(facilitatorDataReader["EventBegin"]);


                    if (facilitatorDataReader["EventEnd"] != DBNull.Value)
                        facEvent.EventEnd = (DateTime)(facilitatorDataReader["EventEnd"]);


                    FacEvents.Add(facEvent);
                }
            }

            facilitatorDataReader.Close();
            ClubBAISTData.Close();
        }
        catch (Exception) { }

        return FacEvents;
    }

    public bool UpdateFacilitator(Facilitator newFacilitator)
    {
        bool Success = false;
        int numRows = 0;
        ConnectionStringSettings webSettings = ConfigurationManager.ConnectionStrings["localdb"];
        SqlConnection DataBaseCon = new SqlConnection(webSettings.ConnectionString);

        SqlCommand CommandAdd = new SqlCommand();
        CommandAdd.Connection = DataBaseCon;
        CommandAdd.CommandType = CommandType.StoredProcedure;
        CommandAdd.CommandText = "UpdateFacilitatorInfo";

        SqlParameter AddParameter = new SqlParameter();
        AddParameter.ParameterName = "@FirstName";
        AddParameter.SqlDbType = SqlDbType.NVarChar;
        AddParameter.Direction = ParameterDirection.Input;
        AddParameter.Value = newFacilitator.FirstName;
        CommandAdd.Parameters.Add(AddParameter);

        AddParameter = new SqlParameter();
        AddParameter.ParameterName = "@ID";
        AddParameter.SqlDbType = SqlDbType.Int;
        AddParameter.Direction = ParameterDirection.Input;
        AddParameter.Value = newFacilitator.FacilitatorID;
        CommandAdd.Parameters.Add(AddParameter);

        AddParameter = new SqlParameter();
        AddParameter.ParameterName = "@LastName";
        AddParameter.SqlDbType = SqlDbType.NVarChar;
        AddParameter.Direction = ParameterDirection.Input;
        AddParameter.Value = newFacilitator.LastName;
        CommandAdd.Parameters.Add(AddParameter);

        AddParameter = new SqlParameter();
        AddParameter.ParameterName = "@Mail";
        AddParameter.SqlDbType = SqlDbType.NVarChar;
        AddParameter.Direction = ParameterDirection.Input;
        AddParameter.Value = newFacilitator.Email;
        CommandAdd.Parameters.Add(AddParameter);

        AddParameter = new SqlParameter();
        AddParameter.ParameterName = "@Roles";
        AddParameter.SqlDbType = SqlDbType.NVarChar;
        AddParameter.Direction = ParameterDirection.Input;
        AddParameter.Value = newFacilitator.Roles;
        CommandAdd.Parameters.Add(AddParameter);

        AddParameter = new SqlParameter();
        AddParameter.ParameterName = "@Password";
        AddParameter.SqlDbType = SqlDbType.NVarChar;
        AddParameter.Direction = ParameterDirection.Input;
        AddParameter.Value = newFacilitator.Password;
        CommandAdd.Parameters.Add(AddParameter);

        AddParameter = new SqlParameter();
        AddParameter.ParameterName = "@Salt";
        AddParameter.SqlDbType = SqlDbType.NVarChar;
        AddParameter.Direction = ParameterDirection.Input;
        AddParameter.Value = newFacilitator.Salt;
        CommandAdd.Parameters.Add(AddParameter);

        AddParameter = new SqlParameter();
        AddParameter.ParameterName = "@Title";
        AddParameter.SqlDbType = SqlDbType.NVarChar;
        AddParameter.Direction = ParameterDirection.Input;
        AddParameter.Value = newFacilitator.Title;
        CommandAdd.Parameters.Add(AddParameter);

        AddParameter = new SqlParameter();
        AddParameter.ParameterName = "@Organization";
        AddParameter.SqlDbType = SqlDbType.NVarChar;
        AddParameter.Direction = ParameterDirection.Input;
        AddParameter.Value = newFacilitator.Organization;
        CommandAdd.Parameters.Add(AddParameter);

        AddParameter = new SqlParameter();
        AddParameter.ParameterName = "@City";
        AddParameter.SqlDbType = SqlDbType.NVarChar;
        AddParameter.Direction = ParameterDirection.Input;
        AddParameter.Value = newFacilitator.Location;
        CommandAdd.Parameters.Add(AddParameter);

        //try
        //{
        DataBaseCon.Open();
        numRows = CommandAdd.ExecuteNonQuery();

        if (numRows == 1)
        {
            Success = true;
        }
        else
        {
            Success = false;
        }

        DataBaseCon.Close();


        return Success;
    }
}
