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
            ConnectionStringSettings webSettings = ConfigurationManager.ConnectionStrings["ClubBAISTConnect"];
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
                pullFacilitator.City = facilitatorDataReader["City"].ToString();
            }

            facilitatorDataReader.Close();
            ClubBAISTData.Close();
        }
        catch (Exception) { }

        return pullFacilitator;
    }
}