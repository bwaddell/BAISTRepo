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

        //tbEventID.Text = ((Event)Session["Event"]).EventID;
        tbEventID.Text = "ABCD";

        CSS Director = new CSS();
        Evaluation eval = new Evaluation();

    }



    protected void btnChart_Click(object sender, EventArgs e)
    {
        lbUpdateTime.Text = "Update Time: " + DateTime.Now.ToString();

        CSS RequestDirector = new CSS();

        List<Evaluation> currentEvals = new List<Evaluation>();

        Event test = new Event();
        test.EventID = "ABCD";

        //currentEvals = RequestDirector.GetCurrentEventData((Event)Session["Event"]);
        currentEvals = RequestDirector.GetCurrentEventData(test);


        foreach (Evaluation ev in currentEvals)
        {
            TableRow tRow = new TableRow();
            TableCell tCell = new TableCell();

            tCell.Text = ev.EvaluatorID.ToString();
            tRow.Cells.Add(tCell);

            tCell = new TableCell();
            tCell.Text = ev.Rating.ToString();
            tRow.Cells.Add(tCell);

            tCell = new TableCell();
            tCell.Text = ev.TimeStamp.ToString();
            tRow.Cells.Add(tCell);

            Table1.Rows.Add(tRow);
        }

        Ratinglbl.Text = currentEvals.Average(x => (double)x.Rating).ToString("#.##");




    }


    protected void btnGraph_Click(object sender, EventArgs e)
    {
        lbUpdateTimeInGraph.Text = "Update Time: " + DateTime.Now.ToString();

        //add code to update the graph


    }

}