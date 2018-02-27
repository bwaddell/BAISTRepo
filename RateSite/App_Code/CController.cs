using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for CController
/// </summary>
public class CController
{
    public CController()
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

        ConnectionStringSettings webSettings = ConfigurationManager.ConnectionStrings["localdb"];
        SqlConnection DataBase = new SqlConnection(webSettings.ConnectionString);
        //DataBase.Open();

        //add code for sql stored proc for adding eval data to sql server




        //DataBase.Close();
        return Success;


    }
}