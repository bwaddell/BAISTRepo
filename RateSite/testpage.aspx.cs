using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

public partial class testpage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Write("<br />Environment.UserName: " + Environment.UserName);
        Response.Write("<br />Environment.Machine Name: " + Environment.MachineName);



        ConnectionStringSettings webSettings = ConfigurationManager.ConnectionStrings["localdb"];
        SqlConnection DataBaseCon = new SqlConnection(webSettings.ConnectionString);

        DataBaseCon.Open();
        DataBaseCon.Close();

        Response.Write("no error");


    }
}