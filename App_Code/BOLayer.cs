using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SonyIris;

/// <summary>
/// Summary description for BOLayer
/// </summary>
public class BOLayer
{
	public BOLayer()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public static ReportTypeLevel getUserType()
    {
        ReportTypeLevel UserType = ReportTypeLevel.IndiaLevel;
        string strUserId = "";
        string strUserRoleId = "";


        if (HttpContext.Current.Request.Cookies["UserInfo"] != null)
        {
            strUserId = Convert.ToString(HttpContext.Current.Request.Cookies["UserInfo"]["UserId"]);
            strUserRoleId = Convert.ToString(HttpContext.Current.Request.Cookies["UserInfo"]["UserRoleId"]);
          
            if (strUserRoleId == "2") // All India Admin
            {
                UserType = ReportTypeLevel.Admit;
            }
            else if (strUserRoleId == "7") // State Manager
            {
                UserType = ReportTypeLevel.StateLevel;
            }
            else if (strUserRoleId == "8") // Reginal Manager
            {
                UserType = ReportTypeLevel.RegionLevel;
            }
            else if (strUserRoleId == "10") // All India
            {
                UserType = ReportTypeLevel.IndiaLevel;
            }
            else if (strUserRoleId == "11") // Regional User
            {
                UserType = ReportTypeLevel.RegionLevel;
            }
            else if (strUserRoleId == "12") // State User
            {
                UserType = ReportTypeLevel.StateLevel;
            }
            else if (strUserRoleId == "13") //City User
            {
                UserType = ReportTypeLevel.CityLevel;
            }
            else if (strUserRoleId == "1") // Outlet Retailer
            {
                UserType = ReportTypeLevel.OutletListLevel;
            }
           
        }

        return UserType;
      

    }

    public bool sendEmail(string name, string email, string body)
    {

        return sendEmail("", name, email, body, "Login Details", "", "");
    }

    public bool sendEmail(string strFromEmailId, string name, string email, string body, string strSubject, string strCcEMailIs, string strBCCEMailIs)
    {
        bool isSuccess = false;
        sendEmail(strFromEmailId, name, email, body, strSubject, strCcEMailIs, strBCCEMailIs, System.Net.Mail.MailPriority.High);

        return isSuccess;
    }
    public bool sendEmail(string strFromEmailId, string name, string email, string body, string strSubject, string strCcEMailIs, string strBCCEMailIs, System.Net.Mail.MailPriority mPriority)
    {
        bool isSuccess = false;
        System.Net.Mail.MailMessage m = new System.Net.Mail.MailMessage();
        m.Body = body;
       
        //m.To = email;
        //m.BodyFormat = MailFormat.Html;

        m.To.Add(email);
        m.IsBodyHtml = true;// BodyFormat = MailFormat.Html;


        if (strCcEMailIs != "")
        {
            m.CC.Add(strCcEMailIs);
        }

       


        if (strBCCEMailIs != "")
        {
            m.Bcc.Add(strBCCEMailIs);// "newprogramer@gmail.com,gauravpandey@solutions-intg.com,harshverma@solutions-intg.com";
        }

        string strShowMail = "";

        if (strFromEmailId == "")
        {
            strFromEmailId = "hvermamzn@gmail.com";
            strShowMail = "harsh.verma@iris-worldwide.com";

        }
        else
        {
            strShowMail = strFromEmailId;
        }

        m.From = new System.Net.Mail.MailAddress(strFromEmailId, strShowMail);
       
     

        m.Subject = strSubject;
        m.Priority = mPriority;

      
        try
        {
        //124.247.226.43

            System.Net.Mail.SmtpClient smtpMail = new System.Net.Mail.SmtpClient("localhost", 25);
            smtpMail.UseDefaultCredentials = true;

            smtpMail.Send(m);
            isSuccess = true;

        }
        catch (Exception ex)
        {
            isSuccess = false;
        }
        return isSuccess;
    }

    public bool sendEmailWithAttachFile(string strFromEmailId, string name, string email, string body, string strSubject, string strCcEMailIs, string strBCCEMailIs, System.Net.Mail.MailPriority mPriority, string strFiles, string strMass)
    {
        bool isSuccess = false;
        System.Net.Mail.MailMessage m = new System.Net.Mail.MailMessage();
        m.Body = body;

        //m.To = email;
        //m.BodyFormat = MailFormat.Html;

        m.To.Add(email);
        m.IsBodyHtml = true;// BodyFormat = MailFormat.Html;


        if (strCcEMailIs != "")
        {
            m.CC.Add(strCcEMailIs);
        }




        if (strBCCEMailIs != "")
        {
            m.Bcc.Add(strBCCEMailIs);// "newprogramer@gmail.com,gauravpandey@solutions-intg.com,harshverma@solutions-intg.com";
        }

        string strShowMail = "";

        if (strFromEmailId == "")
        {
            strFromEmailId = "hvermamzn@gmail.com";
            strShowMail = "harsh.verma@iris-worldwide.com";

        }
        else
        {
            strShowMail = strMass;
        }

        strShowMail = strMass;

        m.From = new System.Net.Mail.MailAddress(strFromEmailId, strShowMail);



        m.Subject = strSubject;
        m.Priority = mPriority;



      
       
        if(strFiles!=null && strFiles !="")
        {
            string[] FileArr=strFiles.Split(',');

            for (int count = 0; count < FileArr.Length; count++)
			{
                if(FileArr[count]!="")
                {
			        string strPPath = HttpContext.Current.Server.MapPath("~/MailAttachFiles/" + FileArr[count]);
                    System.Net.Mail.Attachment attachment = new System.Net.Mail.Attachment(strPPath);//MailAttachFiles
                    m.Attachments.Add(attachment);
                }
			}
           

       
        }

        
        try
        {
            //124.247.226.43

            System.Net.Mail.SmtpClient smtpMail = new System.Net.Mail.SmtpClient("localhost", 25);
            smtpMail.UseDefaultCredentials = true;

            smtpMail.Send(m);
            isSuccess = true;

        }
        catch (Exception ex)
        {
            isSuccess = false;
        }
        return isSuccess;
    }


}