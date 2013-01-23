using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI.HtmlControls;

public partial class OutletReview : System.Web.UI.Page
{
    string strReceeId = "0";
    bool IsCustomRecce = false;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["ReceeId"] != null)
        {
            HiperLExcle.HRef = "OutletReview.aspx?ReceeId=" + Request.QueryString["ReceeId"].ToString()+"&DL=1";
            strReceeId = clsCryptography.Decrypt(Request.QueryString["ReceeId"].ToString());

            if (Request.QueryString["CustomRecce"] != null)
            {
                IsCustomRecce = true;
                HiperLExcle.HRef = "OutletReview.aspx?ReceeId=" + Request.QueryString["ReceeId"].ToString() + "&DL=1" + "&CustomRecce=1";
            }

            setCounterInfo();

           
        }

        hyperCustom.NavigateUrl = "OutletReview.aspx?ReceeId=" + Request.QueryString["ReceeId"].ToString() + "&CustomRecce=1";
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
        Response.AddHeader("Content-Disposition", "attachment; filename=" + lblOutletName.Text.Replace(" ","-") + "_" + DatetimeStr + ".xls");
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

            lblRecceStatus.Text = dataRecce.Rows[0]["ReceeStatus"].ToString();
           
            lblDealerName.Text = dataRecce.Rows[0]["DealerName"].ToString();

            lblOutletType.Text = dataRecce.Rows[0]["OutletType"].ToString();
            lblDealerMobilePhone.Text = dataRecce.Rows[0]["DealerMobilePhoneNo"].ToString();

          //  lblPowerSupplyDetails.Text = dataRecce.Rows[0]["DealerMobilePhoneNo"].ToString();

            lblNoOfPointsRequired.Text = dataRecce.Rows[0]["PSDNoOfPointRequired"].ToString();
            lblNoofPointsAvailable.Text = dataRecce.Rows[0]["PSDNoOfPointAvailable"].ToString();
            lblExtensionsCable.Text = dataRecce.Rows[0]["IsPSDExtensionsCableWorkRequired"].ToString();
            lblFloorlevelingDetails.Text = dataRecce.Rows[0]["FloorLevelingDetails"].ToString();

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



            lblDealerRemarks.Text = dataRecce.Rows[0]["SpecialRemarks"].ToString();

            lblCity.Text = dataRecce.Rows[0]["City"].ToString().ToUpper();
            lblState.Text = dataRecce.Rows[0]["State"].ToString().ToUpper();
            lblRegion.Text = dataRecce.Rows[0]["Region"].ToString().ToUpper();
            lblAdminComments.Text = dataRecce.Rows[0]["AdminRemarks"].ToString();




            DateTime dtInserted = Convert.ToDateTime(dataRecce.Rows[0]["InsertedOn"].ToString());
            string strFolderName = getTodayFolderName(dtInserted);
            string strImageSuffix = dataRecce.Rows[0]["Imagesuffix"].ToString();

            try
            {
                populateImages(strImageSuffix, strFolderName);

            }
            catch (Exception ex)
            {

            }
        }


        addElementReview();

     

    }


    private void addElementReview()
    {
        //tbElement,strReceeId

        string steQuery = @"";

     

        if (IsCustomRecce)
        {
            steQuery = @"select EM.ElementId ,EM.ElementName+ (case when SRD.ElementNo!=0 then ' '+CONVERT(varchar(500),SRD.ElementNo) else '' end) as [ElementName],EM.IsDepth,EM.ShortElementName,SRD.ElementNo
                            ,SRD.IsAvailable,SRD.Height,SRD.Width,SRD.Depth,SRD.ElementReview,SRD.Surface

                            from SonyReceeDetails SRD inner join dbo.ElementMaster EM
                            on SRD.RefElementID=EM.ElementId
                            and ActualElementId!=0

                                where RefReceeID=@ReceeID";
        }
        else
        {
            steQuery = @"select EM.ElementId ,EM.ElementName+ (case when SRD.ElementNo!=0 then ' '+CONVERT(varchar(500),SRD.ElementNo) else '' end) as [ElementName],EM.IsDepth,EM.ShortElementName,SRD.ElementNo
                            ,SRD.IsAvailable,SRD.Height,SRD.Width,SRD.Depth,SRD.ElementReview,SRD.Surface

                            from SonyReceeDetails SRD inner join dbo.ElementMaster EM
                            on SRD.RefElementID=EM.ElementId
                            and CustomElementId!=0

                                where RefReceeID=@ReceeID";
        }

        SqlParameter[] arrayparam = new SqlParameter[1];

        arrayparam[0] = new SqlParameter("@ReceeID", Convert.ToInt32(strReceeId));

        DataSet dataElementValue = DBLayer.getDataSetFromFinalDB(steQuery, arrayparam);

        DataTable datElement = null;
        if (dataElementValue != null && dataElementValue.Tables.Count > 0)
        {
            datElement = dataElementValue.Tables[0];
         
           

            for (int count = 0; count < datElement.Rows.Count; count++)
            {
                HtmlTableRow rowHeaing = new HtmlTableRow();
                HtmlTableCell cellHeadingElement = new HtmlTableCell();
                Control elementSourcecontrol = LoadControl("~/UserControl/RecceElementLabel.ascx");
                UserControl_RecceElementLabel elementCon = (UserControl_RecceElementLabel)elementSourcecontrol;

                elementCon.strElementID = datElement.Rows[count]["ElementId"].ToString();
                elementCon.strElementName = datElement.Rows[count]["ElementName"].ToString();
                elementCon.strIsDepth = datElement.Rows[count]["IsDepth"].ToString();
                elementCon.strSurface = datElement.Rows[count]["Surface"].ToString();

                elementCon.strIsAvalability = datElement.Rows[count]["IsAvailable"].ToString();
                elementCon.strHeight = datElement.Rows[count]["Height"].ToString();
                elementCon.strWidth = datElement.Rows[count]["Width"].ToString();
                elementCon.strDepth = datElement.Rows[count]["Depth"].ToString();
                elementCon.strRemarks = datElement.Rows[count]["ElementReview"].ToString();


                cellHeadingElement.Controls.Add(elementCon);
                rowHeaing.Controls.Add(cellHeadingElement);

                tbElement.Controls.Add(rowHeaing);

            }

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

        string steQuery = @"select UM.Name as [ReceePerson],isnull(OM.OutletName,'-NA-') as [OutletName],isnull(OM.OutletAddress,'-NA-') as [OutletAddress],OM.City,OM.State,OM.Region,isnull(convert(varchar(50),RetailerContactNo),'-NA-') as [RetailerContactNo],ReceePersonComments,RefOutletID,RefReceeUserID,RSM.ReceeStatus,ReceeStartTime,ReceeEndTime,Imagesuffix,ReceeElementString,ReceeElementReviewString,SpecialRemarks,F.InsertedOn,F.InsertedBy,isnull(AdminRemarks,'-NA-') as [AdminRemarks],StateUser_Comments,RegionalUser_Comments,StateUser_IsApproved,RegionalUser_IsApproved,PSDNoOfPointRequired,PSDNoOfPointAvailable,(case when IsPSDExtensionsCableWorkRequired=1 then 'Yes' when IsPSDExtensionsCableWorkRequired=0 then 'No' end) as [IsPSDExtensionsCableWorkRequired],FloorLevelingDetails,(case when IsDeliveryTruckReach=1 then 'Yes' when IsDeliveryTruckReach=0 then 'No' end) as [IsDeliveryTruckReach],DistanceFromTruckToShop,ShopTimings,MarketWeeklyHoliday,ShopOwnersPreferredTimeForInstallation,SOSpaceAvailabilityByDate,SurveyorName,SurveyorMobilePhoneNo,DealerName,DealerMobilePhoneNo,SonyAreaMngName,SonyAreaMngPhone,F.OutletType";
        steQuery += @" from SonyFinalRecee F inner join OutletMaster OM
                      on F.RefOutletID=OM.OutletID 
                      inner join UserMaster UM 
                      on UM.UserID=F.RefReceeUserID
                      inner join ReceeStatusMaster RSM on RSM.ReceeStatusID=F.ReceeStatusId where ReceeID=@ReceeID";

        SqlParameter[] arrayparam = new SqlParameter[1];

        arrayparam[0] = new SqlParameter("@ReceeID", Convert.ToInt32(strReceeId));

        DataSet dataDs = DBLayer.getDataSetFromFinalDB(steQuery, arrayparam);

        if (dataDs != null && dataDs.Tables.Count > 0)
        {
            dataTb = dataDs.Tables[0];
        }

        return dataTb;


    }

    private void populateImages(string strImageSuffix, string strFolderName)
    {
        string AuditPic = "SonyReceeUpload/ReceeImage/" + strFolderName + "/";
        string ThumbnailImg = "SonyReceeUpload/ReceeThumbnailImage/" + strFolderName + "/";
        string picAuditPic = Server.MapPath(AuditPic);

        DirectoryInfo sysdir = new DirectoryInfo(picAuditPic);
        FileInfo[] arrFiles1 = sysdir.GetFiles("*" + strImageSuffix + "*");
        string strImagePath = "";
        string strThumbnailImagePath = "";

        string finalImageTbRows = " <table cellpadding='2' cellspacing='5' border='0' ><tr>";
        finalImageTbRows += @"";
        for (int count = 0; count < arrFiles1.Length; count++)
        {

            strImagePath = AuditPic + arrFiles1[count].Name;
            strThumbnailImagePath = ThumbnailImg + arrFiles1[count].Name;
            finalImageTbRows += "<td>";
            finalImageTbRows += getImageTable(strImagePath, strThumbnailImagePath);
            finalImageTbRows += "</td>";
            if (((count + 1) % 4) == 0)
            {
                finalImageTbRows += "</tr><tr>";
            }
        }

        finalImageTbRows += "</tr></table>";

        CellImg.InnerHtml = finalImageTbRows;

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


    public override void VerifyRenderingInServerForm(System.Web.UI.Control control)
    {
    }
}