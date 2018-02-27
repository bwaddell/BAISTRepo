using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AnalyzeEvent : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //PAGE LOAD happens at each panel update 
        Label1.Text += "page load";




    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        EvalDirector Director = new EvalDirector();
        Evaluation eval = new Evaluation(100, 111, 222);
        Label1.Text = Director.AddEvaulation(eval).ToString();
    }

    protected void Timer1_Tick(object sender, EventArgs e)
    {
        Label1.Text = "refereshed at " + DateTime.Now.ToString();

    }
}