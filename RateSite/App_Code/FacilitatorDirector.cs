using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Globalization;


/// <summary>
/// Summary description for FacilitatorDirector
/// </summary>
public class FacilitatorDirector
{

    CultureInfo culture = new CultureInfo("fr-FR");  // , culture


    public FacilitatorDirector()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public Facilitator GetFacilitatorByEmail(string email)
    {
        Facilitator pullFacilitator = new Facilitator();

        ConnectionStringSettings webSettings = ConfigurationManager.ConnectionStrings["localdb"];
        SqlConnection DataBaseCon = new SqlConnection();
        DataBaseCon.ConnectionString = webSettings.ConnectionString;
        SqlDataReader facilitatorDataReader = null;

        try
        {

            DataBaseCon.Open();

            SqlCommand CommandGet = new SqlCommand
            {
                Connection = DataBaseCon,
                CommandType = CommandType.StoredProcedure,
                CommandText = "GetFacilitatorInfo"
            };

            SqlParameter AddParamater = new SqlParameter
            {
                ParameterName = "@Email",
                SqlDbType = SqlDbType.NVarChar,
                Direction = ParameterDirection.Input,
                Value = email
            };
            CommandGet.Parameters.Add(AddParamater);

            facilitatorDataReader = CommandGet.ExecuteReader();

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
            DataBaseCon.Close();
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
        finally
        {
            if (facilitatorDataReader != null)
                facilitatorDataReader.Close();
            DataBaseCon.Close();
        }

        return pullFacilitator;
    }

    public bool CreateFacilitator(Facilitator newFacilitator)
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

            numRows = CommandAdd.ExecuteNonQuery();

            if (numRows == 1)
            {
                Success = true;
            }
            else
            {
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

    public Facilitator GetFacilitator(int id)
    {
        Facilitator pullFacilitator = new Facilitator();

        ConnectionStringSettings webSettings = ConfigurationManager.ConnectionStrings["localdb"];
        SqlConnection DataBaseCon = new SqlConnection(webSettings.ConnectionString);
        DataBaseCon.ConnectionString = webSettings.ConnectionString;

        try
        {
            DataBaseCon.Open();

            SqlCommand CommandGet = new SqlCommand
            {
                Connection = DataBaseCon,
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

                pullFacilitator.Email = facilitatorDataReader["Email"].ToString();
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
        }
        catch (Exception)
        {
        }
        finally
        {
            DataBaseCon.Close();
        }

        return pullFacilitator;
    }

    public List<Event> GetFacilitatorEvents(int id)
    {
        List<Event> FacEvents = new List<Event>();

        ConnectionStringSettings webSettings = ConfigurationManager.ConnectionStrings["localdb"];
        SqlConnection DataBaseCon = new SqlConnection(webSettings.ConnectionString);
        DataBaseCon.ConnectionString = webSettings.ConnectionString;

        try
        {
            DataBaseCon.Open();

            SqlCommand CommandGet = new SqlCommand
            {
                Connection = DataBaseCon,
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

                    facEvent.EventID = Convert.ToInt32(facilitatorDataReader["EventID"]);
                    facEvent.FacilitatorID = id;
                    facEvent.Location = facilitatorDataReader["Location"].ToString();
                    facEvent.Performer = facilitatorDataReader["Performer"].ToString();
                    facEvent.Description = facilitatorDataReader["NatureOfEvent"].ToString();
                    facEvent.Date = Convert.ToDateTime(facilitatorDataReader["EventDate"], culture);
                    facEvent.EventStart = Convert.ToDateTime(facilitatorDataReader["EventBegin"], culture);
                    facEvent.EventEnd = Convert.ToDateTime(facilitatorDataReader["EventEnd"], culture);

                    FacEvents.Add(facEvent);
                }
            }

            facilitatorDataReader.Close();
        }
        catch (Exception)
        {
        }
        finally
        {
            DataBaseCon.Close();
        }

        return FacEvents;
    }

    public bool UpdateFacilitator(Facilitator newFacilitator)
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
            AddParameter.ParameterName = "@Email";
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

            numRows = CommandAdd.ExecuteNonQuery();

            if (numRows == 1)
            {
                Success = true;
            }
            else
            {
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

    public bool DeleteFacilitator(Facilitator fac)
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
            CommandAdd.CommandText = "DeleteFacilitator";

            SqlParameter AddParamater = new SqlParameter();
            AddParamater.ParameterName = "@FacID";
            AddParamater.SqlDbType = SqlDbType.Int;
            AddParamater.Direction = ParameterDirection.Input;
            AddParamater.Value = fac.FacilitatorID;
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

ConnectionStringSettings webSettings = ConfigurationManager.ConnectionStrings["localdb"];
SqlConnection DataBaseCon = new SqlConnection(webSettings.ConnectionString);
DataBaseCon.ConnectionString = webSettings.ConnectionString;

try
{
    DataBaseCon.Open();   


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