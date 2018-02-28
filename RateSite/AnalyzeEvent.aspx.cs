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
        
        lbStartTime.Text = "<br /> page load at " + DateTime.Now.ToString();

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        CSS Director = new CSS();
        Evaluation eval = new Evaluation(100, 1, 1);

        lbCurrentTime.Text += "<br />" + Director.AddEvaluation(eval).ToString();
    }


    protected void TimerForGraphRefresh_Tick(object sender, EventArgs e)
    {
        lbCurrentTime.Text = "<br /> refereshed at " + DateTime.Now.ToString();
    }
}