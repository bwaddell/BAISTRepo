using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.DataVisualization.Charting;


public partial class AnalyzeEvent : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            lbStartTime.Text = "<br /> Page Loaded at: " + DateTime.Now.ToString();

        tbEventID.Text = ((Event)Session["Event"]).EventID;

        CSS Director = new CSS();
        Evaluation eval = new Evaluation();


        //tbEventID.Text
        //Director.GetEvaluationData()

        Random rnd = new Random();

        DateTime[] x = new DateTime[20];
        int[] y = new int[20]; //{ 0, 1, 2, 3, 4, 4, 5, 5, 8, 9 };

        for (int i = 0; i < 20; i++)
        {
            x[i] = DateTime.UtcNow.AddDays(i);
            y[i] = rnd.Next(1, 10);
        }


        Chart1.Series[0].Points.DataBindXY(x, y);
        Chart1.Series[0].ChartType = SeriesChartType.Line;
        

        //Chart1.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;

        //Chart1.Legends[0].Enabled = true;

    }

    protected void Button1_Click(object sender, EventArgs e)
    {

    }


    protected void TimerForGraphRefresh_Tick(object sender, EventArgs e)
    {
        lbUpdateTime.Text = "Update Time: " + DateTime.Now.ToString();
    }
}