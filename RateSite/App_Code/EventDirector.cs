﻿using System;
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