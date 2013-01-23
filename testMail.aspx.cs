using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class testMail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string strFromEmailId = "";
        string name = "";
        string UserName = "";
        // string strToEmail = "utkarsh.deep@iris-worldwide.com,kush.srivastava@iris-worldwide.com,Shikha.Sharma@iris-worldwide.com";
        string strToEmail = "hvermamzn@gmail.com";
        string body = getMailBoby("", "", "Delhi", "100100");
        string strSubject = "City- Delhi - Outlet ID 100100 - New Recce Data Uploaded";
        string strCcEMailIs = "rajivarya@gmail.com";
        string strBCCEMailIs = "";

       
            BOLayer boLayer = new BOLayer();
        bool result=    boLayer.sendEmail(strFromEmailId, name, strToEmail, body, strSubject, strCcEMailIs, strBCCEMailIs);

        Response.Write("Result=" + result.ToString());
       
    }

    private string getMailBoby(string Name, string UserName, string City, string strOuletID)
    {

        string strDate = System.DateTime.Now.ToString("dd-Mmm-yyyy");
        string strDatetime = System.DateTime.Now.Hour.ToString() + ":" + System.DateTime.Now.Minute.ToString() + ":" + System.DateTime.Now.Second.ToString();

        string strMailBody = @" <table cellpadding='3' cellspacing='1' border='0' width='100%'>
        <tr>
            <td colspan='2'>
                Hi,
            </td>
        </tr>
        <tr>
            <td colspan='2'>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td colspan='2'>
               FOS from <strong>" + City + @"</strong> has uploaded recce data for the outlet  <strong>" + strOuletID + @"</strong> on <strong>" + strDate + "</strong> at <strong>" + strDatetime + @"</strong>.
            </td>
        </tr>
 <tr>
            <td colspan='2'>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td colspan='2'>
               Please login to the website for more information.
            </td>
        </tr>
<tr>
            <td colspan='2'>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td colspan='2'>
               <strong>Test URL</strong>
            </td>
        </tr>
           <tr>
            <td colspan='2'>
               http://d333581.u309.palcomweb.net/Login.aspx
            </td>
        </tr>
        </table>";


        return strMailBody;
    }

}