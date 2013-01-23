using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using SonyIris;

public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            HttpContext.Current.Response.Cookies["UserInfo"].Expires = System.DateTime.Now.AddDays(-1);
            HttpContext.Current.Response.Cookies["UserInfo"]["UserId"] = "";
            HttpContext.Current.Response.Cookies["UserInfo"]["UserRoleId"] = "";
            HttpContext.Current.Response.Cookies["UserInfo"]["Name"] = "";
            HttpContext.Current.Response.Cookies["UserInfo"]["UserName"] = "";


        }
        btnSubmit.Attributes.Add("OnClick", "return ValidateLogin('" + txtUserName.ClientID + "','" + txtPassword.ClientID + "','" + lblErrorMsg .ClientID+ "')");

    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {

        string strErrorMsg = "";
        try
        {
            string strUserName = general.ClearInput(txtUserName.Text);
            string strPassword = general.ClearInput(txtPassword.Text);

            string strLoginId = "";
            string strName = "";
            string strUserRoleID = "";
         
            string strIsPassword = "0";

            string strQuery = "select UserID,UserName,Password,Name,UserRoleId,IsPassword from UserMaster where UserName=@UserName and Password=@Password and IsActive=1";

            SqlParameter[] arrayparam = new SqlParameter[2];

            arrayparam[0] = new SqlParameter("@UserName", strUserName);
            arrayparam[1] = new SqlParameter("@Password", strPassword);
            try
            {
                DataSet dataDs = DBLayer.getDataSetFromFinalDB(strQuery, arrayparam);

                if (dataDs != null && dataDs.Tables.Count > 0)
                {

                    DataTable dataTB = dataDs.Tables[0];
                    if (dataTB != null && dataTB.Rows.Count > 0)
                    {
                        strLoginId = dataTB.Rows[0]["UserID"].ToString();
                        strName = dataTB.Rows[0]["Name"].ToString();
                        strUserRoleID = dataTB.Rows[0]["UserRoleId"].ToString();
                        strIsPassword = dataTB.Rows[0]["IsPassword"].ToString();

                        HttpCookie aCookie = new HttpCookie("UserInfo");
                        aCookie.Values["UserId"] = strLoginId;
                        aCookie.Values["UserRoleId"] = strUserRoleID;
                        aCookie.Values["Name"] = strName;
                        aCookie.Values["UserName"] = strUserName;
                        

                        HttpContext.Current.Session["IsNew"] = "1";
                        HttpContext.Current.Response.Cookies.Add(aCookie);

                        string strIPAddress = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();

                        string strLogQuery = "insert into [UserLoginLog]([UserID],IpAddress)values(@UserId,@IPAddress)";

                        SqlParameter[] arrayInsertparam = new SqlParameter[2];

                        arrayInsertparam[0] = new SqlParameter("@UserId", strLoginId);
                        arrayInsertparam[1] = new SqlParameter("@IPAddress", strIPAddress);

                       int insertResult= DBLayer.ExcuteNonQueryInFinalDB(strLogQuery, arrayInsertparam);
                       if (insertResult > 0)
                       {
                           if (strIsPassword == "0")
                           {
                               Response.Redirect("ChangePassword.aspx");
                           }

                           goToDashBoard();
                       }
                       else
                       {
                           strErrorMsg = "Failed to Login!";
                       }
                    }
                    else
                    {
                        strErrorMsg = "Failed to Login!";
                    }



                }
                else
                {
                    strErrorMsg = "Failed to Login!";
                }

            }
            catch (Exception ex)
            {

            }


        }
        catch(Exception ex)
        {

        }
        lblErrorMsg.Text = strErrorMsg;

    }

    private void goToDashBoard()
    {
        int userId = 0;
        if (HttpContext.Current.Request.Cookies["UserInfo"] != null)
        {
            userId = Convert.ToInt32(HttpContext.Current.Request.Cookies["UserInfo"]["UserId"]);
        }

        string UserRoleId = "0";
        if (HttpContext.Current.Request.Cookies["UserInfo"] != null)
        {
            UserRoleId = HttpContext.Current.Request.Cookies["UserInfo"]["UserRoleId"].ToString();
        }

        if (UserRoleId == "6" || UserRoleId == "9")
        {
            Response.Redirect("ReceeForm.aspx");
        }
        else if (UserRoleId == "7" || UserRoleId == "8" || UserRoleId == "2")// For Recce Review
        {
            Response.Redirect("ReceeList.aspx");
        }
        else if (UserRoleId == "10" || UserRoleId == "2")//All India,All India Admin
        {
            Response.Redirect("RecceDashboard.aspx?PageType=" + clsCryptography.Encrypt(ReportTypeLevel.IndiaLevel.ToString()));
        }
        else if (UserRoleId == "11" || UserRoleId == "8")//Regional Director Sale,Reginal Manager
        {
            Response.Redirect("RecceDashboard.aspx?PageType=" + clsCryptography.Encrypt(ReportTypeLevel.RegionLevel.ToString()));
        }
        else if (UserRoleId == "12" || UserRoleId == "7")//State User ,State Manager
        {
            Response.Redirect("RecceDashboard.aspx?PageType=" + clsCryptography.Encrypt(ReportTypeLevel.StateLevel.ToString()));
        }
        else if (UserRoleId == "13")//City User
        {
            Response.Redirect("RecceDashboard.aspx?PageType=" + clsCryptography.Encrypt(ReportTypeLevel.CityLevel.ToString()));
        }
        else if (UserRoleId == "1")//City User
        {
            Response.Redirect("RecceDashboard.aspx?PageType=" + clsCryptography.Encrypt(ReportTypeLevel.OutletListLevel.ToString()));
        }
    }
}