using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class EvaluateEvent : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void ButtonUp_Click(object sender, EventArgs e)
    {
        int Rating = int.Parse(LabelRating.Text);
        Rating = (Rating + 1 > 10)  ?  Rating = 10 : Rating + 1;

        LabelRating.Text = Rating.ToString();



        Evaluation eval = new Evaluation(Rating,1,1);   // test evaluator and event IDs

        CSS RequestDirector = new CSS();

        bool Success = RequestDirector.AddEvaluation(eval);
        //now send eval to the database with xxx class?
    }

    protected void ButtonDown_Click(object sender, EventArgs e)
    {
        int Rating = int.Parse(LabelRating.Text);
        Rating = (Rating - 1 < 1) ? Rating = 1 : Rating - 1;

        LabelRating.Text = Rating.ToString();

        Evaluation eval = new Evaluation(Rating,1,1);   // test evaluator and event IDs

        CSS RequestDirector = new CSS();

        bool Success = RequestDirector.AddEvaluation(eval);
    }


}