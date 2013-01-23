using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Data.SqlClient;
using System.Web.UI.HtmlControls;
using Ionic.Zip;

public partial class ZipFileDownload : System.Web.UI.Page
{
    string strRandomNumber = "";
    string strUpdateRoot = "SonyReceeUpload";
    string strZipFileFolder = "SonyReceeUpload/TempZipFiles";
    string strAssessment = "SonyRecce";
    string strFileName = "SonyRecceReport";
    string strAuditImagesSamsung_Audit = "SonyReceeUpload/ReceeImage";
    string strUserId = "";

    string strReceeId = "0";

    string strReceeIds = "0";

    bool IsMainRecce = true;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (HttpContext.Current.Request.Cookies["UserInfo"] != null)
        {
            strUserId = Convert.ToString(HttpContext.Current.Request.Cookies["UserInfo"]["UserId"]);

        }
        if (Request.QueryString["ReceeIds"] != null || Request.QueryString["MainReceeIds"] != null)
        {
           
            Random ram = new Random();

            string strFileName = strUserId + ram.Next().ToString();
            strRandomNumber = strFileName;

           // strReceeIds = clsCryptography.Decrypt(Request.QueryString["ReceeIds"].ToString());
            if (Request.QueryString["ReceeIds"] != null)
            {
                IsMainRecce = false;
                strReceeIds = clsCryptography.Decrypt(Request.QueryString["ReceeIds"].ToString());
            }
            else
            {
                IsMainRecce = true;
                strReceeIds = clsCryptography.Decrypt(Request.QueryString["MainReceeIds"].ToString());
            }


            string[] ArrRecceId = strReceeIds.Split(',');

            for (int count = 0; count < ArrRecceId.Length; count++)
            {
                strReceeId=ArrRecceId[count];
                setCounterInfo();
            }


            string strMapPathOfZipFile = Server.MapPath("~/" + strZipFileFolder + "/");

            string ZipRootFolder = "";
            ZipRootFolder = "" + strZipFileFolder + "/" + strRandomNumber + "/";

            string PZipRootFolder = Server.MapPath(ZipRootFolder);


            string zipfilePath = strMapPathOfZipFile + "\\" + strRandomNumber + ".zip";

            using (ZipFile zip = new ZipFile())
            {
                zip.AddItem(PZipRootFolder, strRandomNumber);

                // Response.Write(zipfilePath + ",PZipRootFolder=" + PZipRootFolder);

                zip.Save(zipfilePath);

            }

            try
            {
                RemoveDirectory(PZipRootFolder);
            }
            catch (Exception ex)
            {

            }
            zipfilePath = "~/" + strZipFileFolder + "/" + strRandomNumber + ".zip";


            Response.Redirect(zipfilePath);


        }
    }

    public void RemoveDirectory(string dir)
    {
        DirectoryInfo dirInfo = new DirectoryInfo(dir);
        RemoveDirFiles(dirInfo);

        foreach (DirectoryInfo subDir in dirInfo.GetDirectories("*.*"))
        {
            RemoveDirectory(subDir.FullName);

        }
        dirInfo.Delete();
    }

    public void RemoveDirFiles(DirectoryInfo dirInfo)
    {
        foreach (FileInfo file in dirInfo.GetFiles("*.*"))
        {
            file.Delete();
        }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (Request.QueryString["DL"] != null)
        {
            Export();
        }
    }

    private void Export()
    {
        //divExcle
        string DatetimeStr = System.DateTime.Now.ToString("dddd") + "_" + System.DateTime.Now.ToString("dd-MMMM-yyyy") + System.DateTime.Now.ToLongTimeString();

        DatetimeStr = DatetimeStr.Replace(' ', '_').Trim();
        Response.Clear();
        Response.Buffer = true;
        Response.ContentType = "application/vnd.ms-excel";
        Response.Charset = "";
        Response.AddHeader("Content-Disposition", "attachment; filename=" + lblOutletName.Text + "_" + DatetimeStr + ".xls");
        Page.EnableViewState = false;
        System.IO.StringWriter oStringWriter = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter oHTMLTextWriter = new System.Web.UI.HtmlTextWriter(oStringWriter);
        //Dim     abc As String = tdDataDiv.InnerHtml

        divExcel.RenderControl(oHTMLTextWriter);

        Response.Write(oStringWriter.ToString());
        Response.End();
    }



    private void setCounterInfo()
    {

        string strOutletName = "";
        string strOutletID = "";
        string strOutletAddress = "";
        string strSurveyName = "";
        string strSurveyPhone = "";

        string strSonyAreaMngName = "";
        string strSonyAreaMngPhone = "";
        string strRecceStatus = "";
        string strDealerName = "";
        string strDealerPhone = "";

        string strNoOfPointsRequired = "";
        string strNoOfPointsAvailable = "";
        string strExtensionsCable = "";//Yes/No
        string strShopTimings = "";
        string strMarketWeeklyHoliday = "";
        string strShopOwnersPreferred = "";
        string strSonySOToEnsure = "";
        string strDealerRemarks = "";

        DataTable dataRecce = getReceeData();
        string ZipFilAuditPic = "";

        if (dataRecce != null && dataRecce.Rows.Count > 0)
        {


            lblOutletName.Text = dataRecce.Rows[0]["OutletName"].ToString();
            lblOutletID.Text = dataRecce.Rows[0]["RefOutletID"].ToString();
            lblOutletAddress.Text = dataRecce.Rows[0]["OutletAddress"].ToString();
            lblShopOwnerContactNo.Text = dataRecce.Rows[0]["RetailerContactNo"].ToString();
            lblSurveyorName.Text = dataRecce.Rows[0]["SurveyorName"].ToString();

            lblSurveyorMobilePhone.Text = dataRecce.Rows[0]["SurveyorMobilePhoneNo"].ToString();
            lblSonyAreaManagerName.Text = dataRecce.Rows[0]["SonyAreaMngName"].ToString();
            lblSonyAreaManagerPhone.Text = dataRecce.Rows[0]["SonyAreaMngPhone"].ToString();

            lblRecceStatus.Text = dataRecce.Rows[0]["RetailerContactNo"].ToString();

            lblDealerName.Text = dataRecce.Rows[0]["DealerName"].ToString();

            lblOutletType.Text = dataRecce.Rows[0]["OutletType"].ToString();
            lblDealerMobilePhone.Text = dataRecce.Rows[0]["DealerMobilePhoneNo"].ToString();

            //  lblPowerSupplyDetails.Text = dataRecce.Rows[0]["DealerMobilePhoneNo"].ToString();

            lblNoOfPointsRequired.Text = dataRecce.Rows[0]["PSDNoOfPointRequired"].ToString();
            lblNoofPointsAvailable.Text = dataRecce.Rows[0]["PSDNoOfPointAvailable"].ToString();
            lblExtensionsCable.Text = dataRecce.Rows[0]["IsPSDExtensionsCableWorkRequired"].ToString();
            lblFloorlevelingDetails.Text = dataRecce.Rows[0]["FloorLevelingDetails"].ToString();

            strReceeId = dataRecce.Rows[0]["ReceeID"].ToString();

            

            if (dataRecce.Rows[0]["IsDeliveryTruckReach"].ToString() == "No")
            {
                divDeliveryAvailable.Style.Add("display", "");
                lblCanItReachThe.Text = dataRecce.Rows[0]["DistanceFromTruckToShop"].ToString();
            }
            else if (dataRecce.Rows[0]["IsDeliveryTruckReach"].ToString() == "Yes")
            {
                lblDeliveryTruckReach.Text = "Yes";
            }

            lblShopTimings.Text = dataRecce.Rows[0]["ShopTimings"].ToString();
            lblMarketWeekly.Text = dataRecce.Rows[0]["MarketWeeklyHoliday"].ToString();
            lblShopOwnersPreferred.Text = dataRecce.Rows[0]["ShopOwnersPreferredTimeForInstallation"].ToString();


            string strSOSpaceAvailabilityByDate = "";
            strSOSpaceAvailabilityByDate = Convert.ToDateTime(dataRecce.Rows[0]["SOSpaceAvailabilityByDate"]).ToString("dd-Mmm-yyyy");

            if (strSOSpaceAvailabilityByDate != "21-500-1940")
                lblSonySOto.Text = strSOSpaceAvailabilityByDate;
            else
                lblSonySOto.Text = "-NA-";



            lblDealerRemarks.Text = dataRecce.Rows[0]["AdminRemarks"].ToString();




            DateTime dtInserted = Convert.ToDateTime(dataRecce.Rows[0]["InsertedOn"].ToString());
            string strFolderName = getTodayFolderName(dtInserted);
            string strImageSuffix = dataRecce.Rows[0]["Imagesuffix"].ToString();
           

            try
            {
                string strOuletName = lblOutletID.Text + "-" + lblOutletName.Text.Replace(".", "").Replace("~", "").Replace("#", "").Replace("$", "").Replace(" ", "_");

                string MainZipRootFolder = "";
                MainZipRootFolder = "" + strZipFileFolder + "/" + strRandomNumber + "/";
                string PMainZipRootFolder = Server.MapPath(MainZipRootFolder);

                if (!Directory.Exists(Server.MapPath(MainZipRootFolder)))
                {
                    Directory.CreateDirectory(PMainZipRootFolder);
                }

                if (!Directory.Exists(Server.MapPath(MainZipRootFolder + strOuletName)))
                {
                    Directory.CreateDirectory(PMainZipRootFolder + strOuletName);
                }

                string strImageFolder = strOuletName + "-Images";


                ZipFilAuditPic = Server.MapPath(MainZipRootFolder + strOuletName) + "/" + strOuletName + ".xls";

                if (!Directory.Exists(Server.MapPath(MainZipRootFolder + strOuletName + "/" + strImageFolder)))
                {
                    Directory.CreateDirectory(PMainZipRootFolder + strOuletName + "/" + strImageFolder);
                }

                string ImagesZipFilAuditPic = Server.MapPath(MainZipRootFolder + strOuletName + "/" + strImageFolder);

                populateImages(strImageSuffix, strFolderName, ImagesZipFilAuditPic, strOuletName, strImageFolder);

            }
            catch (Exception ex)
            {

            }
        }


        addElementReview();

        if (ZipFilAuditPic != "")
        {
            using (StreamWriter sw = new StreamWriter(ZipFilAuditPic))
            {
                using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                {
                    excelContainer.RenderControl(hw);
                }
            }
        }

    }


    private void addElementReview()
    {
        //tbElement,strReceeId


        string steQuery = @"select EM.ElementId ,EM.ElementName+ (case when SRD.ElementNo!=0 then ' '+CONVERT(varchar(500),SRD.ElementNo) else '' end) as [ElementName],EM.IsDepth,EM.ShortElementName,SRD.ElementNo
                            ,(case when SRD.IsAvailable=1 then 'Yes' else 'No' end) as [IsAvailable],SRD.Height,SRD.Width,SRD.Depth,SRD.ElementReview,SRD.Surface

                            from SonyReceeDetails SRD inner join dbo.ElementMaster EM
                            on SRD.RefElementID=EM.ElementId


                                where RefReceeID=@ReceeID";

        SqlParameter[] arrayparam = new SqlParameter[1];

        arrayparam[0] = new SqlParameter("@ReceeID", Convert.ToInt32(strReceeId));

        DataSet dataElementValue = DBLayer.getDataSetFromFinalDB(steQuery, arrayparam);

        DataTable datElement = null;
        if (dataElementValue != null && dataElementValue.Tables.Count > 0)
        {
            datElement = dataElementValue.Tables[0];

            string strTbInnerHtml = @" ";

            for (int count = 0; count < datElement.Rows.Count; count++)
            {

                strTbInnerHtml += @"  <tr>
                                            <td width='25%' valign='top' align='left' class='BoldText'>
                                                " + datElement.Rows[count]["ElementName"].ToString() + @"
                                            </td>
                                            <td width='15%' valign='top' align='left' class='BoldText'>
                                                " + datElement.Rows[count]["IsAvailable"].ToString() + @"
                                            </td>
                                            <td width='15%' valign='top' align='left' class='BoldText'>
                                               " + datElement.Rows[count]["Height"].ToString() + @"
                                            </td>
                                            <td width='15%' valign='top' align='left' class='BoldText'>
                                                " + datElement.Rows[count]["Width"].ToString() + @"
                                            </td>
                                            <td width='15%' valign='top' align='left' class='BoldText'>
                                                " + datElement.Rows[count]["Depth"].ToString() + @"
                                            </td>
                                            <td width='15%' valign='top' align='left' class='BoldText'>
                                                " + datElement.Rows[count]["Surface"].ToString() + @"
                                            </td>
                                            <td width='15%' valign='top' align='left' class='BoldText'>
                                               " + datElement.Rows[count]["ElementReview"].ToString() + @"
                                            </td>
                                        </tr>";

          

            }

            strTbInnerHtml = "<table cellpadding='3' cellspacing='1' border='0' width='100%' >" + strTbInnerHtml + "</table>";

            divElement.InnerHtml = strTbInnerHtml;

        }



    }

    public string getTodayFolderName(DateTime dtInsertedOn)
    {
        string FolderPath = dtInsertedOn.ToString("yyyy") + dtInsertedOn.ToString("MM") + dtInsertedOn.ToString("dd");
        return FolderPath;
    }

    private DataTable getReceeData()
    {
        DataTable dataTb = null;

        string steQuery = @"select F.ReceeID,UM.Name as [ReceePerson],isnull(OM.OutletName,'-NA-') as [OutletName],isnull(OM.OutletAddress,'-NA-') as [OutletAddress],isnull(convert(varchar(50),RetailerContactNo),'-NA-') as [RetailerContactNo],ReceePersonComments,RefOutletID,RefReceeUserID,ReceeStatusId,ReceeStartTime,ReceeEndTime,Imagesuffix,ReceeElementString,ReceeElementReviewString,SpecialRemarks,F.InsertedOn,F.InsertedBy,isnull(AdminRemarks,'-NA-') as [AdminRemarks],StateUser_Comments,RegionalUser_Comments,StateUser_IsApproved,RegionalUser_IsApproved,PSDNoOfPointRequired,PSDNoOfPointAvailable,(case when IsPSDExtensionsCableWorkRequired=1 then 'Yes' when IsPSDExtensionsCableWorkRequired=0 then 'No' end) as [IsPSDExtensionsCableWorkRequired],FloorLevelingDetails,(case when IsDeliveryTruckReach=1 then 'Yes' when IsDeliveryTruckReach=0 then 'No' end) as [IsDeliveryTruckReach],DistanceFromTruckToShop,ShopTimings,MarketWeeklyHoliday,ShopOwnersPreferredTimeForInstallation,SOSpaceAvailabilityByDate,SurveyorName,SurveyorMobilePhoneNo,DealerName,DealerMobilePhoneNo,SonyAreaMngName,SonyAreaMngPhone,F.OutletType";
        steQuery += @" from SonyFinalRecee F inner join OutletMaster OM
                      on F.RefOutletID=OM.OutletID 
                      inner join UserMaster UM 
                      on UM.UserID=F.RefReceeUserID";

        if (IsMainRecce)
        {
            steQuery += " where F.MainReceeID=@ReceeID";
        }
        else
        {
            steQuery += " where ReceeID=@ReceeID";
        }

        SqlParameter[] arrayparam = new SqlParameter[1];

        arrayparam[0] = new SqlParameter("@ReceeID", Convert.ToInt32(strReceeId));

        DataSet dataDs = DBLayer.getDataSetFromFinalDB(steQuery, arrayparam);

        if (dataDs != null && dataDs.Tables.Count > 0)
        {
            dataTb = dataDs.Tables[0];
        }

        return dataTb;


    }

    private void populateImages(string strImageSuffix, string strFolderName, string ZipFilAuditPic, string strOuletName, string strLocationUrl)
    {
        string AuditPic = "SonyReceeUpload/ReceeImage/" + strFolderName + "/";
        string picAuditPic = Server.MapPath(AuditPic);

        DirectoryInfo sysdir = new DirectoryInfo(picAuditPic);
        FileInfo[] arrFiles1 = sysdir.GetFiles("*" + strImageSuffix + "*");
        string strImagePath = "";
      

   
       
        string fileName = "";  
        string destFile = "";
   


        for (int count = 0; count < arrFiles1.Length; count++)
        {

            strImagePath = AuditPic + arrFiles1[count].Name;
           
            fileName = System.IO.Path.GetFileName(arrFiles1[count].FullName);
            destFile = System.IO.Path.Combine(ZipFilAuditPic, fileName);
            System.IO.File.Copy(arrFiles1[count].FullName, destFile, false);
           
        }

      
      
        literCellImg.Text = "=HYPERLINK(\""+strLocationUrl+"\",\"Recce Images\")";

    }

    private string getImageTable(string imgURl, string ThumbnailImg)
    {
        string strTable = @"<table cellpadding='0' cellspacing='1' border='0'>
                              
                                <tr>
                                    <td align='center'>
<a onclick=""return hs.htmlExpand(this, { objectType: 'iframe', width: '800' } )""  href='RotateImage.aspx?URL=" + imgURl + @"'>
                                    <img src='" + ThumbnailImg + @"' class='thumbnailImg'   /></a>
                                    </td>
                                </tr>
                               </table>";

        return strTable;

    }
}