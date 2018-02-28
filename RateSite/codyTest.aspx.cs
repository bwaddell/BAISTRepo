using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.DataVisualization.Charting;

public partial class codyTest : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        DateTime[] x = new DateTime[10] { DateTime.Now.AddMinutes(1),
            DateTime.Now.AddMinutes(2), DateTime.Now.AddMinutes(3),
            DateTime.Now.AddMinutes(4),DateTime.Now.AddMinutes(5),
            DateTime.Now.AddMinutes(6),DateTime.Now.AddMinutes(7),
            DateTime.Now.AddMinutes(8),DateTime.Now.AddMinutes(9),
            DateTime.Now.AddMinutes(10) };
        int[] y = new int[10] { 0, 1, 2, 3, 4, 4, 5, 5, 8, 9 };
        //for (int i = 0; i < 10; i++)
        //{
        //    x[i] = dt.Rows[i][0].ToString();
        //    y[i] = Convert.ToInt32(dt.Rows[i][1]);
        //}

        Chart1.Series[0].Points.DataBindXY(x, y);
        Chart1.Series[0].ChartType = SeriesChartType.Line;

        //Chart1.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;

        Chart1.Legends[0].Enabled = true;
        

    }

    protected void TimerForNumRefresh_Tick(object sender, EventArgs e)
    {

    }
}