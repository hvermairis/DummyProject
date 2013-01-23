using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public partial class ChangePassword : System.Web.UI.Page
{
    int userId = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (HttpContext.Current.Request.Cookies["UserInfo"] != null)
        {
            userId = Convert.ToInt32(HttpContext.Current.Request.Cookies["UserInfo"]["UserId"]);
        }
    }

    protected void btnsubmit_Click(object sender, EventArgs e)
    {
       
        string strOldPassword = general.ClearInput(txtoldpassword.Text);
        string strNewPassword = general.ClearInput(txtnewpassword.Text);


        if (strOldPassword == strNewPassword)
        {
            lbmessage.Text = "You cannot entered the same password. please create a new password.";
        }
        else
        {
            bool validate = getChangePassword(strOldPassword, strNewPassword);


            if (validate == true)
            {
               // dvContainer.Visible = false;
                lbmessage.Text = "<font color=#006400>Your password has been successfully updated</font>";
               // lnkHome.Visible = true;

            }
            else
            {
              //  dvContainer.Visible = true;
                lbmessage.Text = "Please check the password.";
            }
        }
    }

    private bool getChangePassword(string strOldPassword, string strNewPassword)
    {
        bool validate = false;
        //userId

        try
        {
            string strQuery = "update  UserMaster set Password=@Password,IsPassword=1 where UserID=@UserID ";
            SqlParameter[] arrayparam = new SqlParameter[2];

            arrayparam[0] = new SqlParameter("@Password", strNewPassword);
            arrayparam[1] = new SqlParameter("@UserID", userId);

            int result = DBLayer.ExcuteNonQueryInFinalDB(strQuery, arrayparam);

            if (result > 0)
            {
                validate = true;
            }
        }
        catch (Exception ex)
        {

        }

        return validate;
    }

    protected void lnkHome_Click(object sender, EventArgs e)
    {
        if (Request.UrlReferrer == null)
        {
            Response.Redirect("Login.aspx");
        }
        else
        {
            goToDashBoard();
        }
    }

    private void goToDashBoard()
    {
       

        string UserRoleId = "0";
        if (HttpContext.Current.Request.Cookies["UserInfo"] != null)
        {
            UserRoleId = HttpContext.Current.Request.Cookies["UserInfo"]["UserRoleId"].ToString();
        }

        if (UserRoleId == "6")
        {
            Response.Redirect("ReceeForm.aspx");
        }
        else
        {
            Response.Redirect("ReceeList.aspx");
        }
    }
}