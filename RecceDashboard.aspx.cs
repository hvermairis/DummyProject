using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SonyIris;
using System.Data.SqlClient;
using System.Data;

public partial class RecceDashboard : AbstractBaseClass
{

    #region AbtractorClass Section

    public SelectedValuesClass SelectValuesObj = new SelectedValuesClass();
    public SelectedValuesClass TempSelectValuesObj = new SelectedValuesClass();

    string RecceId = "0";
    string localRecceId = "0";

    int ColorIndex = 1;
    string strHeaderColor = "#4E81BD";
    string strFirstRowBGColor = "#DBE5F1";
    string strSecondRowBGColor = "#EFDCDB";

    public override void setParamenter(SelectedValuesClass selectObj)
    {
        SelectValuesObj = selectObj;
        SelectValuesObj.setValuesInObj(SelectValuesObj);

    }


    public override void SetPanelSelectedValues(SelectedValuesClass SelectValues)
    {
        SiteFrontMasterPage masterPage = (SiteFrontMasterPage)Page.Master;
        masterPage.SetPanelSelectedValues(SelectValues);
    }

    public override SelectedValuesClass GetPanelSelectedValues()
    {

        SelectedValuesClass selectValues = new SelectedValuesClass();
        if ((Request.QueryString["DL"] != null && Request.QueryString["DL"].ToString() == "1") || (Request.QueryString["RDL"] != null && Request.QueryString["RDL"].ToString() == "1") || (Request.QueryString["RDLWIP"] != null && Request.QueryString["RDLWIP"].ToString() == "1") || (Request.QueryString["ReSynopsis"] != null && Request.QueryString["ReSynopsis"].ToString() == "1"))
        {

            selectValues = getValuesObjFromQueryString();
            TempSelectValuesObj = getValuesObjFromQueryString();
        }
        else
        {

            SiteFrontMasterPage masterPage = (SiteFrontMasterPage)Page.Master;
            selectValues = masterPage.GetPanelSelectedValues();
            TempSelectValuesObj = masterPage.GetPanelSelectedValues();
        }

        return selectValues;
    }

    #endregion

    private ReportTypeLevel _IsUseInControl = ReportTypeLevel.IndiaLevel;
    public ReportTypeLevel IsUseInControl
    {
        get
        {
            return _IsUseInControl;

        }
        set
        {

            _IsUseInControl = value;
        }



    }

    ReportTypeLevel userType = ReportTypeLevel.IndiaLevel;

    string strUserId = "";

    string strUserRoleId = "";
    string strReportTypeId = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (HttpContext.Current.Request.Cookies["UserInfo"] != null)
        {
            strUserId = Convert.ToString(HttpContext.Current.Request.Cookies["UserInfo"]["UserId"]);
            strUserRoleId = Convert.ToString(HttpContext.Current.Request.Cookies["UserInfo"]["UserRoleId"]);
           
        }
        else
        {
            Response.Redirect("Login.aspx");
        }


        //divPending

        if (strUserRoleId == "2" || strUserRoleId == "6" || strUserRoleId == "7" || strUserRoleId == "8" || strUserRoleId == "9" || strUserRoleId == "14")
        {
            divPending.Style.Add("display","");
        }

    }

    protected void Page_PreRender(object sender, EventArgs e)
    {

        try
        {
           


            #region PreRender
            SelectValuesObj = GetPanelSelectedValues();

            #endregion

            #region Set BrandCram

            brandCramas.CurrentPage = SelectValuesObj.PageType;
            #endregion
            HiperLExcle.HRef = "RecceDashboard.aspx" + SelectValuesObj.getQueryString()+"&DL=1";

            string strquerystr = SelectValuesObj.getQueryString() + "&RDL=1";

            aRecceDetails.HRef = strquerystr;
           
            
            strquerystr = SelectValuesObj.getQueryString() + "&RDLWIP=1";
            aRecceWIP.HRef = strquerystr;

            strquerystr = SelectValuesObj.getQueryString() + "&ReSynopsis=1";
            aRecceSynopsis.HRef = strquerystr;

            ReportTypeLevel PageType = SelectValuesObj.PageType;
            setLevel(PageType);


            lblAsPerToday.Text = System.DateTime.Now.ToString("dddd") + "  " + System.DateTime.Now.ToString("dd-MMMM-yyyy");// + " , " + System.DateTime.Now.ToLongTimeString();
            setDataLevel(SelectValuesObj);

            if (SelectValuesObj.PageType == ReportTypeLevel.CityLevel)
            {
                divOutletListButton.Style.Add("display","");
            }

            DesignPage();

        }
        catch(Exception ex)
        {
          //  Response.Redirect("Login.aspx");
        }

    }

    private void setDataLevel(SelectedValuesClass SelectValuesObj)
    {
        string strFilterConditions = "";

        string strQuery = @"select  count( (case when  StateUser_IsApproved='Y' and RegionalUser_IsApproved='Y'  then  RefOutletID end) ) as [DataCount] from SonyFinalRecee  F  inner join OutletMaster OM on F.RefOutletID=OM.OutletID";

        if (SelectValuesObj.Region != "0" && SelectValuesObj.Region != "")
        {
            strFilterConditions = strFilterConditions + @" And (@p_RegionID='0' or OM.RefRegionID in (select * from Split(@p_RegionID,',')) )";

        }
        if (SelectValuesObj.State != "0" && SelectValuesObj.State != "")
        {
            strFilterConditions = strFilterConditions + @" And (@p_StateID='0' or OM.RefStateID in (select * from Split(@p_StateID,',')) )";

        }
        if (SelectValuesObj.City != "0" && SelectValuesObj.City != "")
        {
            strFilterConditions = strFilterConditions + @" And (@p_CityID='0' or   OM.RefCityID in (select * from Split(@p_CityID,',')) )";

        }
        if (SelectValuesObj.OutletTypeId != "0" && SelectValuesObj.OutletTypeId != "")
        {
            strFilterConditions = strFilterConditions + @" And  (@p_OutletTypeId='0' or  OM.RefOutletTypeId in (select * from Split(@p_OutletTypeId,',')) )";

        }

        strQuery += strFilterConditions;

        strQuery += @";   select count(distinct (case when ( isnull(StateUser_IsApproved,'C') !='N' and isnull(RegionalUser_IsApproved,'C')!='N') then  RefOutletID end)) as [TotalDataUploadedCount],count( (case when ( StateUser_IsApproved is null or RegionalUser_IsApproved is null)  then   RefOutletID end) ) as [Pending] from NonQcSonyTempRecee F  inner join OutletMaster OM on F.RefOutletID=OM.OutletID ";
        
        strQuery += strFilterConditions;

        SqlParameter[] arrayparam = new SqlParameter[4];
        arrayparam[0] = new SqlParameter("@p_RegionId", SelectValuesObj.Region);
        arrayparam[1] = new SqlParameter("@p_StateId", SelectValuesObj.State);
        arrayparam[2] = new SqlParameter("@p_CityId", SelectValuesObj.City);
        arrayparam[3] = new SqlParameter("@p_OutletTypeId", SelectValuesObj.OutletTypeId);

        DataSet dsData = DBLayer.getDataSetFromFinalDB(strQuery, arrayparam);

        if (dsData != null && dsData.Tables.Count > 1)
        {
            lblApprovedRecce.Text = dsData.Tables[0].Rows[0][0].ToString();
            lblRecceDataUpload.Text = dsData.Tables[1].Rows[0][0].ToString();
            lblPending.Text = dsData.Tables[1].Rows[0][1].ToString();

        }


        lblOutletType.Text = SelectValuesObj.OutletType;
    }

    private void DesignPage()
    {
        SelectValuesObj = GetPanelSelectedValues();

        DataTable dataTb = getElementMaster(SelectValuesObj.OutletTypeId);

        string strElementId = "";
        string strElementName = "";
        string strIsRepeat = "";
        string strIsDepth = "";

        string strMainTableHtml = "<table cellpadding='3' cellspacing='1' border='0' width='100%'>";

        bool IsDownload = true;

        if (Request.QueryString["DL"] == null && Request.QueryString["RDL"] == null && Request.QueryString["RDLWIP"] == null && Request.QueryString["ReSynopsis"] == null)//
        {

            IsDownload = false;
        }

        if (dataTb != null && dataTb.Rows.Count>0)
        {
            if (IsDownload == false)
            {
                strMainTableHtml += "<tr>";
            }
            DataTable dataGridSource = null;



            for (int count = 0; count < dataTb.Rows.Count; count++)
            {
               
                strElementId=dataTb.Rows[count]["ElementId"].ToString();
                strIsRepeat = dataTb.Rows[count]["IsRepeat"].ToString();
                strIsDepth = dataTb.Rows[count]["IsDepth"].ToString();
                strElementName = dataTb.Rows[count]["ShortElementName"].ToString();

                dataGridSource = getElementGrid(strElementId,strElementName,strIsRepeat, strIsDepth);

                if (IsDownload == false)
                {
                    strMainTableHtml += "<td width='50%'  valign='top' align='center'>" + getMainElementHtml(strElementName, dataGridSource) + "</td>";

                    if ((count + 1) % 2 == 0)
                    {

                        strMainTableHtml += "</tr><tr><td colspan='2'>&nbsp;</td></tr><tr>";
                    }

                }
                else
                {
                    strMainTableHtml += "<tr><td width='100%'  valign='top' align='center'>" + getMainElementHtml(strElementName, dataGridSource) + "</td></tr><tr><td>&nbsp;</td></tr>";

                }



            }

            if (IsDownload == false)
            {
                strMainTableHtml += "</tr>";
            }
        }

        strMainTableHtml += "</table>";

        if (Request.QueryString["DL"] == null && Request.QueryString["RDL"] == null && Request.QueryString["RDLWIP"] == null && Request.QueryString["ReSynopsis"] == null)//
        {
            lblMainElementHTml.Text = strMainTableHtml;
        }
        else
        {
            if (Request.QueryString["DL"] != null)
            {
                Export(strMainTableHtml, "Recce_Summary");
            }
            else if (Request.QueryString["RDL"] != null)
            {
                DownloadRecceDetailsExcel();
            }
            else if (Request.QueryString["RDLWIP"] != null)
            {
                DownloadRecceDetailsWIPExcel();
            }
            else if (Request.QueryString["ReSynopsis"] != null)
            {
                DownloadRecceSynopsisExcel();
            }



        }

    }

    #region Recce details



    private void DownloadRecceSynopsisExcel()
    {
        string strExcelHtml = "";

        GridView gridWIP = new GridView();
        gridWIP.Width = Unit.Percentage(100);
        gridWIP.CellPadding = 4;
        gridWIP.BorderWidth = Unit.Pixel(1);
        gridWIP.GridLines = GridLines.Both;

        gridWIP.ID = "grdRecceListDetailsSynopsisWIP";

        try
        {

            DataTable dataTB = getRecceSynopsis();
            gridWIP.DataSource = dataTB;
            gridWIP.DataBind();
            strExcelHtml = general.gwtGridHtml(gridWIP);
        }
        catch (Exception ex)
        {

        }


        Export(strExcelHtml, "Recce_Synopsis");

    }

    private void DownloadRecceDetailsWIPExcel()
    {
        string strExcelHtml = "";

        GridView gridWIP = new GridView();
        gridWIP.Width = Unit.Percentage(100);
        gridWIP.CellPadding = 4;
        gridWIP.BorderWidth = Unit.Pixel(1);
        gridWIP.GridLines = GridLines.Both;

        gridWIP.ID = "grdRecceListDetailsDowngridWIP";

        try
        {

            DataTable dataTB = getGridSourceRecceDetails();
            gridWIP.RowDataBound += new GridViewRowEventHandler(gridExcel_RowDataBound);
            gridWIP.DataSource = dataTB;
            gridWIP.DataBind();
            strExcelHtml = general.gwtGridHtml(gridWIP);
        }
        catch (Exception ex)
        {

        }


        Export(strExcelHtml, "Recce_WIP");

    }


    private void DownloadRecceDetailsExcel()
    {
        string strExcelHtml = "";

        GridView grid = new GridView();
        grid.Width = Unit.Percentage(100);
        grid.CellPadding = 4;
        grid.BorderWidth = Unit.Pixel(1);
        grid.GridLines = GridLines.Both;

        grid.ID = "grdRecceListDetailsDown";

        try
        {

            DataTable dataTB = getGridSourceRecceDetails();
            grid.RowDataBound += new GridViewRowEventHandler(gridExcel_RowDataBound);
            grid.DataSource = dataTB;
            grid.DataBind();
            strExcelHtml = general.gwtGridHtml(grid);
        }
        catch (Exception ex)
        {

        }


        Export(strExcelHtml, "Recce_Details");

    }

    string strCurrentBgColor = "";

    protected void gridExcel_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        GridView sourceGrid = (GridView)sender;

        e.Row.Cells[0].Visible = false;

        if (e.Row.RowType == DataControlRowType.Header)
        {
            for (int count = 0; count < e.Row.Cells.Count; count++)
            {
               // e.Row.Cells[count].Attributes.Add("bgcolor", strHeaderColor);
               // e.Row.Cells[count].HorizontalAlign = HorizontalAlign.Left;
            }

        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
           
                localRecceId = e.Row.Cells[0].Text;
                if (RecceId != localRecceId)
                {
                    RecceId = localRecceId;
                    if (ColorIndex % 2 == 1)
                    {
                        strCurrentBgColor = strFirstRowBGColor;
                    }
                    else
                    {
                        strCurrentBgColor = strSecondRowBGColor;
                    }

                    for (int count = 0; count < e.Row.Cells.Count; count++)
                    {
                     //   e.Row.Cells[count].Attributes.Add("bgcolor", strCurrentBgColor);
                     //   e.Row.Cells[count].HorizontalAlign = HorizontalAlign.Left;
                    }

                    ColorIndex = ColorIndex + 1;
                }
                else
                {

                    if (sourceGrid.ID != "grdRecceListDetailsDowngridWIP")
                    {
                        for (int count = 0; count < 8; count++)
                        {
                            e.Row.Cells[count].Text = "";
                        }
                    }

                    for (int count = 8; count < 15; count++)
                    {
                      //  e.Row.Cells[count].Attributes.Add("bgcolor", strCurrentBgColor);
                      //  e.Row.Cells[count].HorizontalAlign = HorizontalAlign.Left;
                    }

                    if (sourceGrid.ID != "grdRecceListDetailsDowngridWIP")
                    {
                        for (int count = 15; count < e.Row.Cells.Count; count++)
                        {
                            e.Row.Cells[count].Text = "";
                        }
                    }
                }


            
        }
    }


    private DataTable getRecceSynopsis()
    {
        string strQuery = @"  select
                            upper(OM.Region) as [Region],
                            upper(OM.State) as [State],
                            upper(OM.City) as [City]
                            ,count(distinct Ref_Oulet_ID) as [Outlet Count]
                            ,count(case when RefElementID=1 then 1 end) as [SIS Unit Wall (Back Wall Unit)]
                            ,count(case when RefElementID=3 then 1 end) as [Counter with SIS]
                            ,count(case when RefElementID=4 then 1 end) as [Brand Main Signage (Glow Sign Board - GSB)]
                            ,count(case when RefElementID=15 then 1 end) as [Customize - GSB]
                            ,count(case when RefElementID=5 then 1 end) as [Staircase Branding]
                            ,count(case when RefElementID=6 then 1 end) as [Flange (side view)]
                            ,count(case when RefElementID=7 then 1 end) as [Flange (hanging)]
                            ,count(case when RefElementID=8 then 1 end) as [Pillar Branding]
                            ,count(case when RefElementID=9 then 1 end) as [One Way Vinyl]
                            ,count(case when RefElementID=10 then 1 end) as [Made to share Translite (Wall panel)]
                            ,count(case when RefElementID=11 then 1 end) as [Inshop Branding]
                            ,count(case when RefElementID=12 then 1 end) as [Any Other]
                            ,count(case when RefElementID=13 then 1 end) as [Entrance Specification]
                            ,count(case when RefElementID=14 then 1 end) as [Table top Brand Glorifier ]



                            from  OutletMaster OM
                            inner join SonyReceeDetails TSRD
                            on TSRD.Ref_Oulet_ID=OM.OutletID
                            and IsAvailable=1
                            and Height!=0 and Width!=0 ";

        SelectedValuesClass SelectedValueObj = new SelectedValuesClass();
        SelectedValueObj.setValues();

        string strRegion = SelectedValueObj.Region;
        string strState = SelectedValueObj.State;
        string strCity = SelectedValueObj.City;
        string strFromDate = SelectedValueObj.FromDate;
        string strToDate = SelectedValueObj.ToDate;

        if (strUserRoleId == "6" || strUserRoleId == "9") // Data Entry
        {
            //strQuery += @" and F.InsertedBy=@p_ReceeUserId";
        }
        else if (strUserRoleId == "7") // State Manager
        {
            strQuery += @" and OM.IrisSMID=@p_ReceeUserId";
        }
        else if (strUserRoleId == "8") // Reginal Manager
        {
            strQuery += @" and OM.IrisRMID=@p_ReceeUserId";
        }




        strQuery += @"    AND  ((@p_ToDate!='' AND @p_FromDate!='' AND (convert(datetime,convert(varchar,TSRD.ReceeEndTime,101)) <=convert(datetime,@p_ToDate) AND convert(datetime,convert(varchar,TSRD.ReceeEndTime,101)) >=convert(datetime,@p_FromDate)))
                 OR (@p_ToDate!='' AND @p_FromDate='' AND (convert(datetime,convert(varchar,TSRD.ReceeEndTime,101)) <=convert(datetime,@p_ToDate)))
                 OR (@p_FromDate!='' AND @p_ToDate='' AND (convert(datetime,convert(varchar,TSRD.ReceeEndTime,101)) >=convert(datetime,@p_FromDate)))
                 OR (@p_ToDate='' AND @p_FromDate=''))";

        if (strRegion != "0" && strRegion != "")
        {
            strQuery = strQuery + @" And (@p_RegionID='0' or  OM.RefRegionID in (select * from Split(@p_RegionID,',')) )";
        }

        if (strState != "0" && strState != "")
        {
            strQuery = strQuery + @" And ( @p_StateID ='0' or  OM.RefStateID in (select * from Split(@p_StateID,',')) )";
        }
        if (strCity != "0" && strCity != "")
        {
            strQuery = strQuery + @" And ( @p_CityID ='0' or  OM.RefCityID in (select * from Split(@p_CityID,',')) )";
        }


        strQuery += @" group by OM.Region,
                OM.State,
                OM.City
                order by OM.Region,
                OM.State,
                OM.City";


        SqlParameter[] arrayparam = new SqlParameter[7];

        arrayparam[0] = new SqlParameter("@p_RegionID", strRegion);
        arrayparam[1] = new SqlParameter("@p_StateID", strState);
        arrayparam[2] = new SqlParameter("@p_CityID", strCity);
        arrayparam[3] = new SqlParameter("@p_FromDate", SelectedValueObj.FromDate);
        arrayparam[4] = new SqlParameter("@p_ToDate", SelectedValueObj.ToDate);
        arrayparam[5] = new SqlParameter("@p_ReceeUserId", strUserId);
        arrayparam[6] = new SqlParameter("@RegionalUser_IsApproved", SelectedValueObj.RegionMngApproved);



        DataTable dataTB = null;

        try
        {
            DataSet dataSetRe = DBLayer.getDataSetFromFinalDB(strQuery, arrayparam);
            if (dataSetRe != null && dataSetRe.Tables.Count > 0)
            {
                dataTB = dataSetRe.Tables[0];
            }


        }
        catch (Exception ex)
        {

        }

        return dataTB;
    }


    private DataTable getGridSourceRecceDetails()
    {
        string strQuery = @"  select 
                                F.ReceeID,
                                OM.OutletID,
                                OM.OutletName,
                                OM.OutletAddress,
                                OM.City,
                                OM.State,
                                OM.Region,
                                OM.OutletType,
                                (case when TSRD.IsAvailable=1 then EM.ElementName +' '+CONVERT(varchar(10),TSRD.ElementNo) else EM.ElementName end) as [ElementName],
                                (case when TSRD.IsAvailable=1 then 'Yes' else 'No' end) as [IsAvailable],
                                (case when TSRD.Height=0.00 then '-NA-' else convert(varchar(15),TSRD.Height) end) as [Height],
                                (case when TSRD.Width=0.00 then '-NA-' else convert(varchar(15),TSRD.Width) end) as [Width],
                                (case when TSRD.Depth=0.00 then '-NA-' else convert(varchar(15),TSRD.Depth) end) as [Depth],
                                (case when (TSRD.Surface is null or TSRD.Surface='') then '-NA-' else TSRD.Surface end) as [Surface],
                                TSRD.ElementReview,
                                (case when F.ReceeStatusId=1 then 'Yes' else 'No' end) as [Recee Status],
                                TSRD.ReceeEndTime as [Recce DateTime],
                                isnull(F.AdminRemarks,'-NA-') as [Dealer Comments],
                                isnull(F.StateUser_Comments,'-NA-') as [StateUser Comments],
                                isnull(F.RegionalUser_Comments,'-NA-') as [RegionalUser Comments],
                                isnull(F.SpecialRemarks,'-NA-') as [SpecialRemarks],
                                F.PSDNoOfPointRequired as [No of Points Required],
                                F.PSDNoOfPointAvailable as [No of Points Available],
                                (case when F.IsPSDExtensionsCableWorkRequired=1 then 'Yes' else 'No' end) as [Extensions/ Cable work required],
                                F.FloorLevelingDetails as [Floor leveling Details],
                                (case when F.IsDeliveryTruckReach=1 then 'Yes' else 'No' end) as [IsAvailable],
                                (case when F.DistanceFromTruckToShop is null then convert(varchar(10),F.DistanceFromTruckToShop) else '-NA-' end) as [Distance From Truck To Shop],
                                
                                F.ShopTimings as [Shop Timings (Opening / Closing)],
                                F.MarketWeeklyHoliday as [Market Weekly Holiday],
                                F.ShopOwnersPreferredTimeForInstallation as [Shop Owners Preferred time for Installation],
                                (case when F.SOSpaceAvailabilityByDate='1940-05-21 00:00:00.000' then '-NA-' else CONVERT(varchar(15),F.SOSpaceAvailabilityByDate,105) end) as [Sony SO to ensure Space Availability by (Date)],
                                F.SurveyorName,
                                F.SurveyorMobilePhoneNo,
                                F.DealerName,
                                F.DealerMobilePhoneNo
                                
                                from SonyFinalRecee F inner join OutletMaster OM
                                on F.RefOutletID=OM.OutletID inner join SonyReceeDetails TSRD
                                on F.ReceeID=TSRD.RefReceeID inner join dbo.ElementMaster EM
                                on EM.ElementId=TSRD.RefElementID ";

        SelectedValuesClass SelectedValueObj = new SelectedValuesClass();
        SelectedValueObj.setValues();

        string strRegion = SelectedValueObj.Region;
        string strState = SelectedValueObj.State;
        string strCity = SelectedValueObj.City;
        string strFromDate = SelectedValueObj.FromDate;
        string strToDate = SelectedValueObj.ToDate;

        if (strUserRoleId == "6" || strUserRoleId == "9") // Data Entry
        {
            strQuery += @" and F.InsertedBy=@p_ReceeUserId";
        }
        else if (strUserRoleId == "7") // State Manager
        {
            strQuery += @" and OM.IrisSMID=@p_ReceeUserId";
        }
        else if (strUserRoleId == "8") // Reginal Manager
        {
            strQuery += @" and OM.IrisRMID=@p_ReceeUserId";
        }

      

        strQuery += @"    AND  ((@p_ToDate!='' AND @p_FromDate!='' AND (convert(datetime,convert(varchar,F.ReceeEndTime,101)) <=convert(datetime,@p_ToDate) AND convert(datetime,convert(varchar,F.ReceeEndTime,101)) >=convert(datetime,@p_FromDate)))
                 OR (@p_ToDate!='' AND @p_FromDate='' AND (convert(datetime,convert(varchar,F.ReceeEndTime,101)) <=convert(datetime,@p_ToDate)))
                 OR (@p_FromDate!='' AND @p_ToDate='' AND (convert(datetime,convert(varchar,F.ReceeEndTime,101)) >=convert(datetime,@p_FromDate)))
                 OR (@p_ToDate='' AND @p_FromDate=''))";

        if (strRegion != "0" && strRegion != "")
        {
            strQuery = strQuery + @" And (@p_RegionID='0' or  OM.RefRegionID in (select * from Split(@p_RegionID,',')) )";
        }

        if (strState != "0" && strState != "")
        {
            strQuery = strQuery + @" And ( @p_StateID ='0' or  OM.RefStateID in (select * from Split(@p_StateID,',')) )";
        }
        if (strCity != "0" && strCity != "")
        {
            strQuery = strQuery + @" And ( @p_CityID ='0' or  OM.RefCityID in (select * from Split(@p_CityID,',')) )";
        }


        strQuery += @" ORDER BY   F.ReceeID,OM.OutletID,F.ReceeEndTime desc,EM.SortBy,TSRD.ElementNo";


        SqlParameter[] arrayparam = new SqlParameter[7];

        arrayparam[0] = new SqlParameter("@p_RegionID", strRegion);
        arrayparam[1] = new SqlParameter("@p_StateID", strState);
        arrayparam[2] = new SqlParameter("@p_CityID", strCity);
        arrayparam[3] = new SqlParameter("@p_FromDate", SelectedValueObj.FromDate);
        arrayparam[4] = new SqlParameter("@p_ToDate", SelectedValueObj.ToDate);
        arrayparam[5] = new SqlParameter("@p_ReceeUserId", strUserId);
        arrayparam[6] = new SqlParameter("@RegionalUser_IsApproved", SelectedValueObj.RegionMngApproved);



        DataTable dataTB = null;

        try
        {
            DataSet dataSetRe = DBLayer.getDataSetFromFinalDB(strQuery, arrayparam);
            if (dataSetRe != null && dataSetRe.Tables.Count > 0)
            {
                dataTB = dataSetRe.Tables[0];
            }


        }
        catch (Exception ex)
        {

        }

        return dataTB;
    }

   


    #endregion


    private void Export(string strExcelHtml)
    {
        Export(strExcelHtml, "Recce");
    }

    private void Export(string strExcelHtml,string strExcelName)
    {
        //divExcle
        string DatetimeStr = System.DateTime.Now.ToString("dddd") + "_" + System.DateTime.Now.ToString("dd-MMMM-yyyy") + System.DateTime.Now.ToLongTimeString();

        DatetimeStr = DatetimeStr.Replace(' ', '_').Trim();
        Response.Clear();
        Response.Buffer = true;
        Response.ContentType = "application/vnd.ms-excel";
        Response.Charset = "";
        Response.AddHeader("Content-Disposition", "attachment; filename=" + strExcelName + "_" + DatetimeStr + ".xls");
        Page.EnableViewState = false;
        System.IO.StringWriter oStringWriter = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter oHTMLTextWriter = new System.Web.UI.HtmlTextWriter(oStringWriter);
        //Dim     abc As String = tdDataDiv.InnerHtml


        Response.Write(strExcelHtml);
        Response.End();
    }
       


    private string getMainElementHtml(string strElementName,DataTable dataGridSource)
    {
        string strMainElementStr =   "<table cellpadding='3' cellspacing='1' border='0' width='100%'>";;
        strMainElementStr += "<tr><td class='heading-blue'>" + strElementName + "</td></tr>";


        string strGridHtml = getGridElementHtml(strElementName,dataGridSource);

        strMainElementStr += "<tr><td>" + strGridHtml + "</td></tr>";
        strMainElementStr += "</table>";
        strMainElementStr = "<div style='width:500px;height:250px;overflow:auto;'>" + strMainElementStr + "</div>";
        return strMainElementStr;
    }

    private string getGridElementHtml(string strElementName, DataTable dataGridSource)
    {
        string strMainElementStr = "";
        GridView grid = new GridView();
        grid.Width = Unit.Percentage(100);
        grid.CellPadding = 4;
        grid.BorderWidth = Unit.Pixel(1);
        grid.GridLines = GridLines.Both;
        grid.CssClass = "gridCssNew";
        grid.HeaderStyle.CssClass = "gridHeaderCssNew";
        grid.AlternatingRowStyle.CssClass = "gridAlternateItemCssNew";
        grid.RowStyle.CssClass = "gridItemCssNew";
        grid.PagerStyle.CssClass = "gridPagerStyleNew";
        
        grid.ID = "grd" + strElementName;
        grid.RowCreated += new GridViewRowEventHandler(gridElement_RowCreatedTrue);

        grid.RowDataBound += new GridViewRowEventHandler(grid_RowDataBound);
        grid.DataSource = dataGridSource;
        grid.DataBind();

        strMainElementStr = general.gwtGridHtml(grid);

        return strMainElementStr;
    }

    protected void grid_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        GridView gridSource=(GridView)sender;
        DataTable dataSou = (DataTable)gridSource.DataSource;

        e.Row.Cells[0].Style.Add("text-align","left !important");

        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Attributes.Add("nowrap", "nowrap");
            for (int count = 1; count < e.Row.Cells.Count; count++)
            {
                e.Row.Cells[count].Text = e.Row.Cells[count].Text.Replace("x", "<br>x<br>");
            }
        }
        else
        {
            for (int count = 0; count < e.Row.Cells.Count; count++)
            {
                e.Row.Cells[count].Attributes.Add("nowrap", "nowrap");
            }
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.RowIndex < (dataSou.Rows.Count - 1))
                e.Row.Cells[0].CssClass = "gridFirstHeaderRow";
            else
                e.Row.CssClass = "gridHeaderCssNew";
        }
    }


    protected void gridElement_RowCreatedTrue(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView oGridView = (GridView)sender;
            general.setGridHeaderRow(oGridView, e);
        }
    }

    private string getStatesFromCitiesID(string strCitiesIDs)
    {

        string strRefStateIds = "";
        string strQuery = "select distinct RefStateId from CityMaster where IsActive=1 and CityId in(select * from Split(@p_CityIds,','))";

        SqlParameter[] arrayparam = new SqlParameter[1];

        arrayparam[0] = new SqlParameter("@p_CityIds", strCitiesIDs);

        DataTable dataTB = DBLayer.getDataSetFromFinalDB(strQuery, arrayparam).Tables[0];

        if (dataTB != null)
        {
            for (int count = 0; count < dataTB.Rows.Count; count++)
            {
                strRefStateIds = strRefStateIds + dataTB.Rows[count]["RefStateId"].ToString() + ",";
            }
        }

        if (strRefStateIds != "")
        {
            strRefStateIds = strRefStateIds.Substring(0, strRefStateIds.Length - 1);
        }
        else
        {
            strRefStateIds = "0";
        }

        return strRefStateIds;
    }

    private DataTable getElementGrid(string strElementId,string strElementName,string strIsRepeat, string strIsDepth)
    {
        DataTable dataTb = null;
        string strElementMaster = "exec urd_ElementValue @ReportTypeId,@v_UserId,@v_UserRoleId,@ElementId,@ElementName,@IsRepeat,@IsDepth,@p_RegionId,@p_StateId,@p_CityId,@p_OutletTypeId";

        SqlParameter[] arrayparam = new SqlParameter[11];

        arrayparam[0] = new SqlParameter("@ReportTypeId", strReportTypeId);
        arrayparam[1] = new SqlParameter("@v_UserId", strUserId);
        arrayparam[2] = new SqlParameter("@v_UserRoleId", strUserRoleId);

        arrayparam[3] = new SqlParameter("@ElementId", strElementId);
        arrayparam[4] = new SqlParameter("@ElementName", strElementName);
        arrayparam[5] = new SqlParameter("@IsRepeat", strIsRepeat);
        arrayparam[6] = new SqlParameter("@IsDepth", strIsDepth);
      

        arrayparam[7] = new SqlParameter("@p_RegionId", SelectValuesObj.Region);
        arrayparam[8] = new SqlParameter("@p_StateId", SelectValuesObj.State);
        arrayparam[9] = new SqlParameter("@p_CityId", SelectValuesObj.City);
        arrayparam[10] = new SqlParameter("@p_OutletTypeId", SelectValuesObj.OutletTypeId);

        DataSet dataDs = DBLayer.getDataSetFromFinalDB(strElementMaster, arrayparam);

        if (dataDs != null && dataDs.Tables.Count > 0)
        {
          

            DataTable dtSamCompetorTB = dataDs.Tables[0];
            DataView dtSamCompetorDV = new DataView(dtSamCompetorTB);
            dtSamCompetorDV.Sort = "SortBy ASC";

            dataTb = dtSamCompetorDV.ToTable();
            dataTb.Columns.RemoveAt(0);
        }

         



        return dataTb;


    }

    private DataTable getElementMaster(string OutletTypeId)
    {
        DataTable dataTb = null;
        string strElementMaster = "exec urd_ElementList @OutletTypeId";

        SqlParameter[] arrayparam = new SqlParameter[1];

        arrayparam[0] = new SqlParameter("@OutletTypeId", OutletTypeId);

        DataSet dataDs = DBLayer.getDataSetFromFinalDB(strElementMaster, arrayparam);

        if (dataDs != null && dataDs.Tables.Count > 0)
        {
            dataTb = dataDs.Tables[0];
        }

        return dataTb;


    }

    private void setLevel(ReportTypeLevel PageType)
    {

        if (PageType == ReportTypeLevel.IndiaLevel)
        {
            lblPageHeading.Text = "All India";
            strReportTypeId = "1";
        }
        else if (PageType == ReportTypeLevel.RegionLevel)
        {
            lblPageHeading.Text = "Region";
            strReportTypeId = "1";
        }
        else if (PageType == ReportTypeLevel.StateLevel)
        {
            lblPageHeading.Text = SelectValuesObj.RegionName;
            strReportTypeId = "2";
        }
        else if (PageType == ReportTypeLevel.CityLevel)
        {
            lblPageHeading.Text = SelectValuesObj.StateName;
            strReportTypeId = "3";
        }
    }
    protected void btnOutListTypeID_Click(object sender, EventArgs e)
    {
           SelectValuesObj = GetPanelSelectedValues();
           SelectValuesObj.PageType = ReportTypeLevel.OutletListLevel;
           SelectValuesObj.State = getStatesFromCitiesID(SelectValuesObj.City);
           string strQuery = SelectValuesObj.getQueryString();
           Response.Redirect("OutletList.aspx" + strQuery);
    }
}