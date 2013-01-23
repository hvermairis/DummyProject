using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;

public partial class Congratulation : System.Web.UI.Page
{
    string strMsg = "";
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Request.QueryString["IsResult"] != null)
        {
            if (Request.QueryString["IsResult"].ToString()=="1")
            {
                strMsg = "You have successfully uploaded recce data .";
                if (Request.QueryString["IsApp"] != null)
                {
                    if (Request.QueryString["IsApp"].ToString() == "Y")
                    {
                        strMsg = "You have successfully approved recce data .";
                    }
                    else if (Request.QueryString["IsApp"].ToString() == "N")
                    {
                        strMsg = "You have successfully rejected recce data .";
                    }

                }

                lblMassage.Text = strMsg;
                lblMassage.ForeColor = Color.Green;
            }
            
        }
    }
}