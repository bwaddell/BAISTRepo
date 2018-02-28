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
        lbCurrentTime.Text += "page load";




    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        EvalDirector Director = new EvalDirector();
        Evaluation eval = new Evaluation(100, 111, 222);
        lbCurrentTime.Text = Director.AddEvaulation(eval).ToString();
    }


    protected void TimerForGraphRefresh_Tick(object sender, EventArgs e)
    {
        lbCurrentTime.Text = "refereshed at " + DateTime.Now.ToString();

    }
}