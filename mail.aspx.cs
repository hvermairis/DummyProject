using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;

public partial class mail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lblMsg.Text = setMail();
        }
    }


    private string setMail()
    {
        string strMail = "";

        string strFromEmailId = "sixhats_films@yahoo.co.in";//harsh.verma@iris-worldwide.com,sixhats_films@yahoo.co.in
        string strMass = "6 Hats";

        string strToEmail = "utkarsh.deep@iris-worldwide.com";//abhishek.dangwal@iris-worldwide.com,hvermamzn@gmail.com,utkarsh.deep@iris-worldwide.com


        string name = "sir";

        string strSubject = "Cost of product films";

        string strCcEMailIs = "";
        string strBCCEMailIs = "utkarsh.deep.iris@gmail.com,abhishek.dangwal@iris-worldwide.com,hvermamzn@gmail.com,harsh.verma@iris-worldwide.com";
    //   string strBCCEMailIs = "";

       // string strAttachFiles = "test.xls";
        string strAttachFiles = "";

        string strBody=@"";

      
       


       string body = getMailBoby(name,strBody);

      //  string body = "Test";
       

        string strIPAddress = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();

        try
        {

            //For Test
            //strMail = "Mail has been sent";
            //lblMsg.ForeColor = Color.Green;

            //body = general.ClearTextInput(body);
            //name = general.ClearTextInput(name);
            //strSubject = general.ClearTextInput(strSubject);

            //string strInsertQuery = "insert into DemoMailDetails(Name,FromEmailId,ToEmailId,BccEmailId,Subject,Body,FromIPAddress)values('" + name + @"','" + strFromEmailId + @"','" + strToEmail + @"','" + strBCCEMailIs + @"','" + strSubject + @"','" + body + "','" + strIPAddress + @"')";


            //int inResult = DBLayer.ExcuteNonQueryInFinalDB(strInsertQuery);
            ////For Test

            

            BOLayer boLayer = new BOLayer();
            bool mailResult = boLayer.sendEmailWithAttachFile(strFromEmailId, name, strToEmail, body, strSubject, strCcEMailIs, strBCCEMailIs, System.Net.Mail.MailPriority.Normal, strAttachFiles, strMass);

            lblMsg.ForeColor = Color.Red;

            if(mailResult)
            {

                strMail = "Mail has been sent";
                lblMsg.ForeColor = Color.Green;

                body = general.ClearTextInput(body);
                name = general.ClearTextInput(name);
                strSubject = general.ClearTextInput(strSubject);

                string strInsertQuery = "insert into DemoMailDetails(Name,FromEmailId,ToEmailId,BccEmailId,Subject,Body,FromIPAddress)values('" + name + @"','" + strFromEmailId + @"','" + strToEmail + @"','" + strBCCEMailIs + @"','" + strSubject + @"','" + body + "','" + strIPAddress + @"')";


                int inResult = DBLayer.ExcuteNonQueryInFinalDB(strInsertQuery);

                if(inResult>0)
                {

                    strMail+=" and insert into databese";
                    lblMsg.ForeColor = Color.Green;

                }
                else
                {
                    strMail+=" and It was not inserted into databese";

                }
            }
            else
            {
                 strMail="Mail is not sent";
            }
        }
        catch (Exception ex)
        {
             strMail="Mail is not sent";
        }

        return strMail;
    }

    private string getMailBoby(string Name, string strBody)
    {


        string strMailBody = @"<html><head>
<meta http-equiv='Content-Type' content='text/html; charset=us-ascii'></head><body style='word-wrap: break-word; -webkit-nbsp-mode: space; -webkit-line-break: after-white-space; color: rgb(0, 0, 0); font-size: 14px; '><div style='font-family: Calibri, sans-serif; '><span class='Apple-style-span' style='font-family: Calibri; '>Dear "+Name+@",</span></div><span id='OLK_SRC_BODY_SECTION'><div><div style='word-wrap: break-word; -webkit-nbsp-mode: space; -webkit-line-break: after-white-space; '><div style='font-family: Calibri, sans-serif; '><font class='Apple-style-span' face='Calibri'><br></font></div><div style='font-family: Calibri, sans-serif; '><font class='Apple-style-span' face='Calibri'>Further to our discussion, we are please to quote for the SONY product films. Post of two films will cost 8,40,000 &amp; includes the following:</font></div><div style='font-family: Calibri, sans-serif; '><ol><li><span class='Apple-style-span' style='font-family: Calibri; '>1 day shoot&nbsp;</span></li><li><span class='Apple-style-span' style='font-family: Calibri; '>VO (English only)</span></li><li><span class='Apple-style-span' style='font-family: Calibri; '>Animation and Graphics</span></li><li><span class='Apple-style-span' style='font-family: Calibri; '>Editing&nbsp;</span></li><li><span class='Apple-style-span' style='font-family: Calibri; '>Stock Music</span></li><li><span class='Apple-style-span' style='font-family: Calibri; '>Basic color correction</span></li><li><span class='Apple-style-span' style='font-family: Calibri; '>Director's fee</span></li></ol></div><div style='font-family: Calibri, sans-serif; '><font class='Apple-style-span' face='Calibri'>This cost would double for 4 films. WIll share a formal quote at the time of job allocation.</font></div><div style='font-family: Calibri, sans-serif; '><font class='Apple-style-span' face='Calibri'><br></font></div><div style='font-family: Calibri, sans-serif; '><font class='Apple-style-span' face='Calibri'><br></font></div><div style='font-family: Calibri, sans-serif; '><font class='Apple-style-span' face='Calibri'>Regards,</font></div><div style='font-family: Calibri, sans-serif; '><font class='Apple-style-span' face='Calibri'>Chandrachur</font></div><div style='font-family: Calibri, sans-serif; '><font class='Apple-style-span' face='Calibri'><br></font><div><div><div apple-content-edited='true'><span class='Apple-style-span' style='border-collapse: separate; font-style: normal; font-variant: normal; letter-spacing: normal; line-height: normal; orphans: 2; text-align: -webkit-auto; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-border-horizontal-spacing: 0px; -webkit-border-vertical-spacing: 0px; -webkit-text-decorations-in-effect: none; -webkit-text-size-adjust: auto; -webkit-text-stroke-width: 0px; '><span class='Apple-style-span' style='border-collapse: separate; font-style: normal; font-variant: normal; letter-spacing: normal; line-height: normal; orphans: 2; text-align: -webkit-auto; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-border-horizontal-spacing: 0px; -webkit-border-vertical-spacing: 0px; -webkit-text-decorations-in-effect: none; -webkit-text-size-adjust: auto; -webkit-text-stroke-width: 0px; '><span class='Apple-style-span' style='border-collapse: separate; font-style: normal; font-variant: normal; letter-spacing: normal; line-height: normal; orphans: 2; text-align: -webkit-auto; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-border-horizontal-spacing: 0px; -webkit-border-vertical-spacing: 0px; -webkit-text-decorations-in-effect: none; -webkit-text-size-adjust: auto; -webkit-text-stroke-width: 0px; '><div><div><font class='Apple-style-span' color='#0678a1' face='Arial,sans-serif'><b><br></b></font></div></div></span></span></span></div></div></div></div></div></div></span></body></html>
";


        return strMailBody;
    }

    

}