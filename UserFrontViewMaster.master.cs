using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserFrontViewMaster : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
         string strUserId = "";
    string strUserRoleId = "";

        if (HttpContext.Current.Request.Cookies["UserInfo"] != null)
        {
            lblUserName.Text = Convert.ToString(HttpContext.Current.Request.Cookies["UserInfo"]["Name"]);

             strUserId = Convert.ToString(HttpContext.Current.Request.Cookies["UserInfo"]["UserId"]);
            strUserRoleId = Convert.ToString(HttpContext.Current.Request.Cookies["UserInfo"]["UserRoleId"]);
        }

        if (strUserId == "183")
        {
            trAddCounter.Style.Add("display", "");
        }
        else
        {
            trAddCounter.Style.Add("display", "none");
        }

    }
    protected void linkButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("Login.aspx");
    }
}
