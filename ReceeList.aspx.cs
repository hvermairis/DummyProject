using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.HtmlControls;
using SonyIris;

public partial class ReceeList : System.Web.UI.Page
{
    string strUserId = "";

    string strUserRoleId = "";

    bool IsReccePerson = false;

    int PageSize = 20;
    int startIndex = 0;
    string strSelectedPageColor = "#FFFFFF";
    string strUnSelectedPageColor = "#0000FF";

    string Region = "";
    string State = "";
    string City = "";
    string FromDate = "";
    string ToDate = "";

    string RecceId = "0";
    string localRecceId = "0";

    int ColorIndex = 1;
    string strHeaderColor = "#4E81BD";
    string strFirstRowBGColor = "#DBE5F1";
    string strSecondRowBGColor = "#EFDCDB";
    string strMainRecceIds = "";

    protected void Page_Load(object sender, EventArgs e)
    {

       
        if (HttpContext.Current.Request.Cookies["UserInfo"] != null)
        {
            strUserId = Convert.ToString(HttpContext.Current.Request.Cookies["UserInfo"]["UserId"]);
            strUserRoleId = Convert.ToString(HttpContext.Current.Request.Cookies["UserInfo"]["UserRoleId"]);
            if (strUserRoleId == "6" || strUserRoleId == "9") // Data Entry
            {
                IsReccePerson = true;
            }
        }
        else
        {
            Response.Redirect("Login.aspx");
        }

        if (strUserId == "183")//adminsony
        {
            aOutletDump.Style.Add("display", "");
        }
        else
        {
            aOutletDump.Style.Add("display", "none");
        }

        if (!IsPostBack)
        {
           
            try
            {

                if (Request.QueryString["DL"] == null && Request.QueryString["OutDL"] == null)
                {
                    BindControl();

                    if (strUserRoleId == "7") // State Mng
                    {
                        drpStateMngApproved.SelectedValue = "NR";
                        drpRegionMngApproved.SelectedValue = "NR";
                    }

                    populateFilterPara();
                    PopulateAuditGrid();
                }
                else
                {
                    if (Request.QueryString["DL"] != null)
                    {
                        DownloadRecceDetailsExcel();
                    }
                    else if (Request.QueryString["OutDL"] != null)
                    {
                        DownloadRecceOutletDumpExcel();
                    }
                }
            }
            catch(Exception ex)
            {

            }

        }

        populateFilterPara();
        setDownloadRecceDetailsQueryString();

        CreateCustomPaging();

        attachJavascriptFunction();

      

    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (drpStateMngApproved.SelectedValue == "Y" && drpRegionMngApproved.SelectedValue == "Y")
        {
            aRecceZip.HRef = "ZipFileDownload.aspx?MainReceeIds=" + clsCryptography.Encrypt(strMainRecceIds);
           
        }
        else
        {
            
            aRecceZip.HRef = "javascript:alert('Please select State Manager and Regional Manager Approved.');";
        }


    }

    private void setDownloadRecceDetailsQueryString()
    {
        SelectedValuesClass selectedObj = new SelectedValuesClass();

        selectedObj.Region = Region;
        selectedObj.State = State;
        selectedObj.City = City;
        selectedObj.RegionMngApproved = drpRegionMngApproved.SelectedValue;
        selectedObj.StateMngApproved = drpStateMngApproved.SelectedValue;
        selectedObj.FromDate = calDateFrom.Text;
        selectedObj.ToDate = calDateTo.Text;

        string strquerystr ="ReceeList.aspx"+selectedObj.getQueryString()+"&DL=1";
        aRecceDetails.HRef = strquerystr;

        strquerystr =  "ReceeList.aspx?OutDL=1";
        aOutletDump.HRef = strquerystr;
        
    }

    private void attachJavascriptFunction()
    {
        try
        {
            chkRegion.Items[0].Attributes.Add("OnClick", "SelectAll('" + chkRegion.ClientID + "')");
            chkRegion.Items[0].Attributes.Add("class", "checkbox");
            
            setJavascriptFun(chkRegion);

            chkState.Items[0].Attributes.Add("OnClick", "SelectAll('" + chkState.ClientID + "')");
            chkState.Items[0].Attributes.Add("class", "checkbox");
            setJavascriptFun(chkState);

            chkCity.Items[0].Attributes.Add("OnClick", "SelectAll('" + chkCity.ClientID + "')");
            chkCity.Items[0].Attributes.Add("class", "checkbox");
            setJavascriptFun(chkCity);

            imgBtn.Attributes.Add("OnClick", "return checkUncheckAllCheckBox('" + chkRegion.ClientID + "'," + chkRegion.Items.Count + ",,'" + chkState.ClientID + "'," + chkState.Items.Count + ",,'" + chkCity.ClientID + "'," + chkCity.Items.Count + ")");
        
        }
        catch (Exception ex)
        {

        }


    }


    private void setJavascriptFun(CheckBoxList checkBox)
    {

        for (int count = 1; count < checkBox.Items.Count; count++)
        {
            checkBox.Items[count].Attributes.Add("class", "checkbox");
            checkBox.Items[count].Attributes.Add("OnClick", "checkUncheckCheckBox('" + checkBox.ClientID + "'," + checkBox.Items.Count + ")");

        }


    }


    #region Control Bind Region

    private void BindControl()
    {
        string strProc = "exec urd_Region @userId";
        SqlParameter[] arrayparam = new SqlParameter[1];
        arrayparam[0] = new SqlParameter("@userId", strUserId);
       
        DataSet dataSet = DBLayer.getDataSetFromFinalDB(strProc, arrayparam);

        if (dataSet != null && dataSet.Tables.Count > 0)
        {
            DataTable regionTb = dataSet.Tables[0];
            DataRow rowRegion = regionTb.NewRow();

            rowRegion[0] = "0";
            rowRegion[1] = "Select All";
            regionTb.Rows.InsertAt(rowRegion, 0);
            regionTb.AcceptChanges();

            chkRegion.DataTextField = "Region";
            chkRegion.DataValueField = "RegionID";

            chkRegion.DataSource = regionTb;
            chkRegion.DataBind();
            chkRegion.SelectedIndex = 0;

            selectAll(chkRegion);

            populateStates();

            selectAll(chkState);



            populateCities();
            selectAll(chkCity);


        }
    }

    private void populateCities()
    {
        string whereCondition = "";

        for (int count = 1; count < chkState.Items.Count; count++)
        {
            if (chkState.Items[count].Selected)
            {
                if (whereCondition == "")
                {
                    whereCondition += chkState.Items[count].Value;
                }
                else
                {
                    whereCondition += "," + chkState.Items[count].Value;

                }

            }
        }
        if (whereCondition == "")
        {
            whereCondition = "0";
        }



        string strProc = "exec urd_City @userId,@StateId";
        SqlParameter[] arrayparam = new SqlParameter[2];
        arrayparam[0] = new SqlParameter("@userId", strUserId);
        arrayparam[1] = new SqlParameter("@StateId", whereCondition);

        DataSet dataSet = DBLayer.getDataSetFromFinalDB(strProc, arrayparam);

        DataTable CitySource = null;
        if (dataSet != null && dataSet.Tables.Count > 0)
        {
            CitySource = dataSet.Tables[0];
        }




        DataRow rowCity = CitySource.NewRow();
        rowCity[0] = "0";
        rowCity[1] = "Select All";
        CitySource.Rows.InsertAt(rowCity, 0);
        CitySource.AcceptChanges();



        chkCity.DataSource = CitySource;
        chkCity.DataBind();
    }


    private void populateStates()
    {
        string whereCondition = "";

        for (int count = 1; count < chkRegion.Items.Count; count++)
        {
            if (chkRegion.Items[count].Selected)
            {
                if (whereCondition == "")
                {
                    whereCondition += chkRegion.Items[count].Value;
                }
                else
                {
                    whereCondition += "," + chkRegion.Items[count].Value;

                }

            }
        }
        if (whereCondition == "")
        {
            whereCondition = "0";
        }


        DataTable StateSource = null;


        string strProc = "exec urd_State @userId,@RegionId";
        SqlParameter[] arrayparam = new SqlParameter[2];
        arrayparam[0] = new SqlParameter("@userId", strUserId);
        arrayparam[1] = new SqlParameter("@RegionId", whereCondition);

        DataSet dataSet = DBLayer.getDataSetFromFinalDB(strProc, arrayparam);


       
        if (dataSet != null && dataSet.Tables.Count > 0)
        {
            StateSource = dataSet.Tables[0];
        }

        if (StateSource != null)
        {
            DataRow rowState = StateSource.NewRow();
            rowState[0] = "0";
            rowState[1] = "Select All";
            StateSource.Rows.InsertAt(rowState, 0);
            StateSource.AcceptChanges();



            chkState.DataSource = StateSource;
            chkState.DataBind();
        }
    }


    private void selectAll(CheckBoxList chkList)
    {
        for (int count = 0; count < chkList.Items.Count; count++)
        {
            chkList.Items[count].Selected = true;
        }


    }


    #endregion




    protected void chkRegion_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            populateStates();

            selectAll(chkState);



            populateCities();
            selectAll(chkCity);
        }
        catch(Exception ex)
        {
        }
    }

    protected void chkState_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            populateCities();
            selectAll(chkCity);
        }
        catch(Exception ex)
        {
        }
    }
    protected void imgBtn_Click(object sender, ImageClickEventArgs e)
    {
        string errormsg = "";
        bool isFocus = true;

        if ((calDateFrom.Text != "" && calDateTo.Text != "") && (general.IsDate(calDateFrom.Text) && general.IsDate(calDateTo.Text)) && Convert.ToDateTime(calDateFrom.Text) > Convert.ToDateTime(calDateTo.Text))
        {
            if (errormsg != "")
            {
                errormsg += " , ";

            }
            errormsg += "From Date should be less than To Date";

        }

        if (errormsg != "")
        {
            isFocus = false;
        }


        try
        {
            if (isFocus)
            {
                populateFilterPara();
                tbTableImg.Controls.Clear();
                CreateCustomPaging();
                PopulateAuditGrid();


                //string strHref = "";
                //string IsHref = "True";

                //if (drpStateMngApproved.SelectedValue == "Y" && drpRegionMngApproved.SelectedValue == "Y")
                //{
                //    strHref = "ZipFileDownload.aspx?MainReceeIds=" + clsCryptography.Encrypt(strMainRecceIds);
                //    IsHref = "True";
                //}
                //else
                //{
                //    IsHref = "False";
                //    strHref = "javascript:alert('Please select State Manager and Regional Manager Approved.');";
                //}


                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "loadPage" + IsHref.ToString(), "loadPage('" + aRecceZip.ClientID + "','" + strHref + "');", true);
     

            }
            else
            {
                if (errormsg != "")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ErrorMsgAlert", "ErrorMsgAlert('" + errormsg + "');", true);

                }
            }

        }
        catch (Exception ex)
        {

        }



    }

    private void PopulateAuditGrid()
    {
        PopulateAuditGrid("");
    }
    private void PopulateAuditGrid(string strOuletID)
    {

        int startIndex = 1;
        if (hdnStartPage.Value != "")
        {
            startIndex = Convert.ToInt32(hdnStartPage.Value);
        }

        // startIndex = startIndex - 1;
        linkNext.Style.Add("color", strUnSelectedPageColor);
        linkPreVious.Style.Add("color", strUnSelectedPageColor);

        PopulateAuditGrid(startIndex, strOuletID);
    }

    private void PopulateAuditGrid(int startIndex)
    {
        PopulateAuditGrid(startIndex,"");
    }
    private void PopulateAuditGrid(int startIndex, string strOuletID)
    {

        DataTable ReceeDetailTb = getGridSource(startIndex, PageSize, strOuletID);
        setMainRecceIds(ReceeDetailTb);

        gridAuditDetail.DataSource = ReceeDetailTb;
        gridAuditDetail.DataBind();
    }

    

    private void setMainRecceIds(DataTable ReceeDetailTb)
    {
        if (ReceeDetailTb != null)
        {
            strMainRecceIds = "";
            for (int count = 0; count < ReceeDetailTb.Rows.Count; count++)
            {
                strMainRecceIds += ReceeDetailTb.Rows[count]["ReceeID"].ToString() + ",";
            }

            if (strMainRecceIds != "")
            {
                strMainRecceIds = strMainRecceIds.Substring(0, strMainRecceIds.Length-1);
            }
        }

    }



    private DataTable getGridSource(int startIndex, int PageSize, string strOuletID)
    {


        DataTable dataTb = null;
        try
        {
           int inStartIndex = ((startIndex - 1) * PageSize);
           int inPageSize = (startIndex * PageSize);
           dataTb = getReceeData(inStartIndex, inPageSize, strOuletID);

        }
        catch (Exception ex)
        {

        }

        return dataTb;


    }

    private void populateFilterPara()
    {


        string selectValue = "";

        selectValue = getSelectedValue(chkRegion);
        if (selectValue == "")
        {
            selectValue = "0";
        }

        Region = selectValue;


        selectValue = "";
        selectValue = getSelectedValue(chkState);
        if (selectValue == "")
        {
            selectValue = "0";
        }

        State = selectValue;


        selectValue = "";
        selectValue = getSelectedValue(chkCity);
        if (selectValue == "")
        {
            selectValue = "0";
        }

        City = selectValue;

        FromDate = general.ClearInput(calDateFrom.Text);
        ToDate = general.ClearInput(calDateTo.Text);

    }



   private string getSelectedValue(CheckBoxList chkList)
   {
       string strSelectedValue = "";
       string strSelected = "";
       for (int count = 0; count < chkList.Items.Count; count++)
       {
           if (chkList.Items[count].Selected)
           {
               strSelected = chkList.Items[count].Value;

               if (strSelectedValue == "")
               {
                   strSelectedValue = strSelected;

               }
               else
               {
                   strSelectedValue += "," + strSelected;
               }

           }
       }
       if (strSelectedValue == "")
       {
           strSelectedValue = "-1";
       }

       return strSelectedValue;

   }

   private DataTable getReceeData(int inStartIndex, int inEndIndex, string strOuletID)
    {
      
        string strQuery = @" select * from (  
                                          select  ROW_NUMBER() OVER (ORDER BY  F.ReceeEndTime desc,F.ReceeID desc ) as Rownumber,
                    F.ReceeID,
                    RefOutletID as [OutletID],
                    OM.OutletName as [Outlet Name],
                    OM.OutletAddress as [Address],
                    RM.RegionName as [Region],
                    SM.StateName as [State],
                    CM.CityName as [City],

                    (case when F.StateUser_IsApproved='Y' then 'Approved' when F.StateUser_IsApproved='N' then 'Reject' else 'Pending' end ) as [State Approval] ,
                    (case when F.RegionalUser_IsApproved='Y' then 'Approved' when F.RegionalUser_IsApproved='N' then 'Reject' else 'Pending' end ) as [Regional Approval] ,


                    UM.Name as [Recce Person],
                    RSM.ReceeStatus as [Recce Status],
                    CONVERT(varchar(15),F.ReceeEndTime,105) as [Recce Date] ,
                    F.Mno
                 
                    
                     from NonQcSonyTempRecee F inner join OutletMaster OM
                    on F.RefOutletID=OM.OutletID
                    inner join UserMaster UM
                    on F.RefReceeUserID=UM.UserID
                    inner join ReceeStatusMaster RSM
                    on RSM.ReceeStatusID=F.ReceeStatusId
                    inner join RegionMaster RM
                    on OM.RefRegionID=RM.RegionID
                    inner join StateMaster SM
                    on OM.RefStateID=SM.StateId
                    inner join CityMaster CM
                    on OM.RefCityID=CM.CityID ";
        strQuery += @" and (@OutletId='' or (F.RefOutletID=@OutletId)) ";
        string strRegion = Region;
        string strState = State;
        string strCity = City;
        string strFromDate = FromDate;
        string strToDate = ToDate;

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

        if (drpReceeStatus.SelectedValue == "1")// Yes
        {
            strQuery += @" and  F.ReceeStatusId=1";
        }
        else if (drpReceeStatus.SelectedValue == "2") //No
        {
            strQuery += @" and  F.ReceeStatusId=2";
        }

        if (strOuletID == "")
        {

            if (drpRegionMngApproved.SelectedValue != "0")
            {
                if (drpRegionMngApproved.SelectedValue == "NR")
                {
                    strQuery += @" and F.RegionalUser_IsApproved<>'N'";

                }
                else if (drpRegionMngApproved.SelectedValue == "P")
                {
                    strQuery += @" and F.RegionalUser_IsApproved is null";

                }
                else
                {
                    strQuery += @" and F.RegionalUser_IsApproved=@RegionalUser_IsApproved";
                }
            }

            if (drpStateMngApproved.SelectedValue != "0")
            {
                if (drpStateMngApproved.SelectedValue == "NR")
                {
                    strQuery += @" and F.StateUser_IsApproved<>'N'";

                }
                else if (drpStateMngApproved.SelectedValue == "P")
                {
                    strQuery += @" and F.StateUser_IsApproved is null";

                }
                else
                {
                    strQuery += @" and F.StateUser_IsApproved=@StateUser_IsApproved";
                }
            }

        }

        if (drpReceeStatus.SelectedValue == "1")// Yes
        {
            strQuery += @" and  F.ReceeStatusId=1";
        }
        else if (drpReceeStatus.SelectedValue == "2") //No
        {
            strQuery += @" and  F.ReceeStatusId=2";
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


        strQuery = strQuery + @" ";

             strQuery = strQuery + @"   ) as ReceeData";

             strQuery = strQuery + @" where ReceeData.Rownumber >" + inStartIndex.ToString() + @" and ReceeData.Rownumber<=" + inEndIndex.ToString();


        SqlParameter[] arrayparam = new SqlParameter[9];

        arrayparam[0] = new SqlParameter("@p_RegionID", strRegion);
        arrayparam[1] = new SqlParameter("@p_StateID", strState);
        arrayparam[2] = new SqlParameter("@p_CityID", strCity);
        arrayparam[3] = new SqlParameter("@p_FromDate", strFromDate);
        arrayparam[4] = new SqlParameter("@p_ToDate", strToDate);
        arrayparam[5] = new SqlParameter("@p_ReceeUserId", strUserId);
        arrayparam[6] = new SqlParameter("@RegionalUser_IsApproved", drpRegionMngApproved.SelectedValue);
        arrayparam[7] = new SqlParameter("@StateUser_IsApproved", drpStateMngApproved.SelectedValue);
        arrayparam[8] = new SqlParameter("@OutletId", strOuletID);

        DataTable dataTB = null;

        try
        {
            DataSet dataSetRe = DBLayer.getDataSetFromFinalDB(strQuery, arrayparam);
            if (dataSetRe != null && dataSetRe.Tables.Count > 0)
            {
                dataTB = dataSetRe.Tables[0];
            }
        }
        catch(Exception ex)
        {

        }


       

        return dataTB;

    }


    public override void VerifyRenderingInServerForm(System.Web.UI.Control control)
    {
    }

    private void CreateCustomPaging()
    {
        CreateCustomPaging("");
    }

    private void CreateCustomPaging(string strOuletID)
    {


        int _totalRecord = PageCount(strOuletID);

        int stratPageIndex = 1;
        if (hdnStartPage.Value != "")
        {
            stratPageIndex = Convert.ToInt32(hdnStartPage.Value);
        }

        //stratPageIndex=stratPageIndex-1;

        int maxIndex = _totalRecord / PageSize;

        if (_totalRecord % PageSize > 0)
        {

            maxIndex += 1;

        }


        int lastLimitofPaging = PageSize;

        if ((stratPageIndex + PageSize) <= maxIndex)
        {

            hdnEndPage.Value = (stratPageIndex + (PageSize - 1)).ToString();
        }
        else
        {
            hdnEndPage.Value = maxIndex.ToString();
        }

        lastLimitofPaging = Convert.ToInt32(hdnEndPage.Value);

        HtmlTableRow trPageNo = new HtmlTableRow();
        trPageNo.Style.Add("height", "15px");
        trPageNo.Style.Add("font-size", "15px");
        trPageNo.Style.Add("margin", "2px");
        trPageNo.ID = "trPageNo";

        tbTableImg.Controls.Add(trPageNo);

        for (int count = stratPageIndex; count <= (lastLimitofPaging); count++)
        {
            LinkButton linkPageNo = new LinkButton();

            linkPageNo.ID = "linkPageNo_" + count;
            linkPageNo.Text = count.ToString();
            linkPageNo.Click += new EventHandler(linkPageNo_Click);

            HtmlTableCell cellLink = new HtmlTableCell();

            linkPageNo.Style.Add("color", strUnSelectedPageColor);

            if (count == stratPageIndex)
            {
                linkPageNo.Style.Add("color", strSelectedPageColor);
            }

            cellLink.Controls.Add(linkPageNo);

            trPageNo.Controls.Add(cellLink);

        }


        if (maxIndex > int.Parse(hdnEndPage.Value))
        {

            linkNext.Visible = true;
        }
        else
        {
            linkNext.Visible = false;
        }

        if (maxIndex  != 0 && int.Parse(hdnStartPage.Value) >= 2)
        {
            linkPreVious.Visible = true;
        }
        else
        {
            linkPreVious.Visible = false;
        }

    }

    protected void linkPageNo_Click(object sender, EventArgs e)
    {
        LinkButton linkPageNo = (LinkButton)sender;

        try
        {
            int stratPageIndex = 1;
            if (hdnStartPage.Value != "")
            {
                stratPageIndex = Convert.ToInt32(hdnStartPage.Value);
            }


            string strPageNo = "linkPageNo_";
            string strtempPageNo = "";


            HtmlTableRow trPageNo = (HtmlTableRow)tbTableImg.FindControl("trPageNo");

            for (int count = stratPageIndex; count < (stratPageIndex + PageSize); count++)
            {
                strtempPageNo = strPageNo + count.ToString();

                LinkButton linkOtherPageNo = (LinkButton)trPageNo.FindControl(strtempPageNo);
                if (linkOtherPageNo != null)
                {

                    linkOtherPageNo.Style.Add("color", strUnSelectedPageColor);
                }
            }

            linkPageNo.Style.Add("color", strSelectedPageColor);

            int currentPageIndex = 1;

            currentPageIndex = Convert.ToInt32(linkPageNo.ID.Split('_')[1]);
            PopulateAuditGrid(currentPageIndex);
        }
        catch (Exception ex)
        {

        }
    }



    protected void linkPreVious_Click(object sender, EventArgs e)
    {
        try
        {
            int stratPageIndex = 1;
            if (hdnStartPage.Value != "")
            {
                stratPageIndex = Convert.ToInt32(hdnStartPage.Value);
            }

            if (stratPageIndex != 1)
            {
                stratPageIndex = stratPageIndex - PageSize;
            }

            hdnStartPage.Value = stratPageIndex.ToString();

            tbTableImg.Controls.Clear();

            CreateCustomPaging();
            PopulateAuditGrid();
        }
        catch (Exception ex)
        {

        }
    }

    private int PageCount()
    {
        return PageCount("");
    }

    private int PageCount(string strOuletID)
    {
        int AssessmentCount = 0;
        try
        {
            DataTable dataTb = getPageCountDataTable(strOuletID);


          

          


            if (dataTb != null && dataTb.Rows.Count > 0)
            {
                AssessmentCount = Convert.ToInt32(dataTb.Rows[0][0]);
            }

        }
        catch(Exception ex)
        {

        }
        return AssessmentCount;
    }



    private DataTable getPageCountDataTable(string strOuletID)
    {

        string strQuery = @"   select  count(1) as [ReceeCount]
                 
                    
                     from NonQcSonyTempRecee F inner join OutletMaster OM
                    on F.RefOutletID=OM.OutletID";

        strQuery += @" and (@OutletId='' or (F.RefOutletID=@OutletId)) ";
        string strRegion = Region;
        string strState = State;
        string strCity = City;
        string strFromDate = FromDate;
        string strToDate = ToDate;

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

        if (drpReceeStatus.SelectedValue == "1")// Yes
        {
            strQuery += @" and  F.ReceeStatusId=1";
        }
        else if (drpReceeStatus.SelectedValue == "2") //No
        {
            strQuery += @" and  F.ReceeStatusId=2";
        }

        if (strOuletID == "")
        {

            if (drpRegionMngApproved.SelectedValue != "0")
            {
                if (drpRegionMngApproved.SelectedValue == "NR")
                {
                    strQuery += @" and F.RegionalUser_IsApproved<>'N'";

                }
                else if (drpRegionMngApproved.SelectedValue == "P")
                {
                    strQuery += @" and F.RegionalUser_IsApproved is null";

                }
                else
                {
                    strQuery += @" and F.RegionalUser_IsApproved=@RegionalUser_IsApproved";
                }
            }

            if (drpStateMngApproved.SelectedValue != "0")
            {
                if (drpStateMngApproved.SelectedValue == "NR")
                {
                    strQuery += @" and F.StateUser_IsApproved<>'N'";

                }
                else if (drpStateMngApproved.SelectedValue == "P")
                {
                    strQuery += @" and F.StateUser_IsApproved is null";

                }
                else
                {
                    strQuery += @" and F.StateUser_IsApproved=@StateUser_IsApproved";
                }
            }
        }

        strQuery += @"    AND  ((@p_ToDate!='' AND @p_FromDate!='' AND (convert(datetime,convert(varchar,F.ReceeEndTime,101)) <=convert(datetime,@p_ToDate) AND convert(datetime,convert(varchar,F.ReceeEndTime,101)) >=convert(datetime,@p_FromDate)))
                 OR (@p_ToDate!='' AND @p_FromDate='' AND (convert(datetime,convert(varchar,F.ReceeEndTime,101)) <=convert(datetime,@p_ToDate)))
                 OR (@p_FromDate!='' AND @p_ToDate='' AND (convert(datetime,convert(varchar,F.ReceeEndTime,101)) >=convert(datetime,@p_FromDate)))
                 OR (@p_ToDate='' AND @p_FromDate=''))";

        if (strRegion != "0" && strRegion != "")
        {
            strQuery = strQuery + @" And  OM.RefRegionID in (select * from Split(@p_RegionID,',')) ";

        }
        if (strState != "0" && strState != "")
        {
            strQuery = strQuery + @" And  OM.RefStateID in (select * from Split(@p_StateID,',')) ";

        }
        if (strCity != "0" && strCity != "")
        {
            strQuery = strQuery + @" And  OM.RefCityID in (select * from Split(@p_CityID,','))";

        }
        strQuery = strQuery + @" ";



        SqlParameter[] arrayparam = new SqlParameter[9];

        arrayparam[0] = new SqlParameter("@p_RegionID", strRegion);
        arrayparam[1] = new SqlParameter("@p_StateID", strState);
        arrayparam[2] = new SqlParameter("@p_CityID", strCity);
        arrayparam[3] = new SqlParameter("@p_FromDate", strFromDate);
        arrayparam[4] = new SqlParameter("@p_ToDate", strToDate);
        arrayparam[5] = new SqlParameter("@p_ReceeUserId", strUserId);
        arrayparam[6] = new SqlParameter("@RegionalUser_IsApproved", drpRegionMngApproved.SelectedValue);
        arrayparam[7] = new SqlParameter("@StateUser_IsApproved", drpStateMngApproved.SelectedValue);
        arrayparam[8] = new SqlParameter("@OutletId",strOuletID);

        //@OutletId
   
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




    protected void linkNext_Click(object sender, EventArgs e)
    {
        try
        {
            int stratPageIndex = 1;
            if (hdnStartPage.Value != "")
            {
                stratPageIndex = Convert.ToInt32(hdnStartPage.Value);
            }
            int MaxRecordCount = PageCount();

            //int MaxPageCount = 1;
            int MaxPageCount = MaxRecordCount / PageSize;

            if (MaxRecordCount % PageSize > 0)
            {

                MaxPageCount += 1;

            }

            if ((stratPageIndex + PageSize) <= MaxPageCount)
            {
                stratPageIndex = stratPageIndex + PageSize;
                hdnEndPage.Value = (stratPageIndex + (PageSize - 1)).ToString();
            }
            else
            {
                hdnEndPage.Value = MaxPageCount.ToString();
            }

            // hdnEndPage
            hdnStartPage.Value = stratPageIndex.ToString();

            tbTableImg.Controls.Clear();
            CreateCustomPaging();
            PopulateAuditGrid();

        }
        catch (Exception ex)
        {

        }
    }




    protected void gridAuditDetail_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        
        if (e.Row.Cells.Count > 2)
        {
            e.Row.Cells[0].Visible = false;
            e.Row.Cells[1].Visible = false;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[2].Text = "<a  href='ReceeForm.aspx?ReceeId=" + clsCryptography.Encrypt(e.Row.Cells[1].Text) + "'>" + e.Row.Cells[2].Text + "</a>";

            }
        }
    }

    protected void btnDownload_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            DataTable dataTempTB = getDatatable();
            string DatetimeStr = System.DateTime.Now.ToString("dddd") + "_" + System.DateTime.Now.ToString("dd-MMMM-yyyy") + System.DateTime.Now.ToLongTimeString();

            if (dataTempTB != null)
            {
                RKLib.ExportData.Export objExport = new RKLib.ExportData.Export("Web");
                objExport.ExportDetails(dataTempTB, RKLib.ExportData.Export.ExportFormat.Excel, "RecceList_" + DatetimeStr+".xls");
            }
        }
        catch (Exception ex)
        {

        }
    }


    private DataTable getDatatable()
    {
        DataTable dataTb = new DataTable();


        populateFilterPara();
        string strQuery = @" select
                    ReceeID,
                    RefOutletID as [OutletID],
                    OM.OutletName as [Outlet Name],
                    OM.OutletAddress as [Address],
                    RM.RegionName as [Region],
                    SM.StateName as [State],
                    CM.CityName as [City],
                    OM.OutletType,

                    (case when F.StateUser_IsApproved='Y' then 'Approved' when F.StateUser_IsApproved='N' then 'Reject' else 'Pending' end ) as [State Approval] ,
                    (case when F.RegionalUser_IsApproved='Y' then 'Approved' when F.RegionalUser_IsApproved='N' then 'Reject' else 'Pending' end ) as [Regional Approval] ,


                    UM.Name as [Recce Person],
                    RSM.ReceeStatus as [Recce Status],
                    CONVERT(varchar(15),F.ReceeEndTime,105) as [Recce Date] ,
                    F.Mno
                 
                    
                     from NonQcSonyTempRecee F inner join OutletMaster OM
                    on F.RefOutletID=OM.OutletID
                    inner join UserMaster UM
                    on F.RefReceeUserID=UM.UserID
                    inner join ReceeStatusMaster RSM
                    on RSM.ReceeStatusID=F.ReceeStatusId
                    inner join RegionMaster RM
                    on OM.RefRegionID=RM.RegionID
                    inner join StateMaster SM
                    on OM.RefStateID=SM.StateId
                    inner join CityMaster CM
                    on OM.RefCityID=CM.CityID ";

        string strRegion = Region;
        string strState = State;
        string strCity = City;
        string strFromDate = FromDate;
        string strToDate = ToDate;

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

        if (drpReceeStatus.SelectedValue == "1")// Yes
        {
            strQuery += @" and  F.ReceeStatusId=1";
        }
        else if (drpReceeStatus.SelectedValue == "2") //No
        {
            strQuery += @" and  F.ReceeStatusId=2";
        }

        if (drpRegionMngApproved.SelectedValue != "0")
        {
            if (drpRegionMngApproved.SelectedValue == "NR")
            {
                strQuery += @" and F.RegionalUser_IsApproved<>'N'";

            }
            else if (drpRegionMngApproved.SelectedValue == "P")
            {
                strQuery += @" and F.RegionalUser_IsApproved is null";

            }
            else
            {
                strQuery += @" and F.RegionalUser_IsApproved=@RegionalUser_IsApproved";
            }
        }

        if (drpStateMngApproved.SelectedValue != "0")
        {
            if (drpStateMngApproved.SelectedValue == "NR")
            {
                strQuery += @" and F.StateUser_IsApproved<>'N'";

            }
            else if (drpStateMngApproved.SelectedValue == "P")
            {
                strQuery += @" and F.StateUser_IsApproved is null";

            }
            else
            {
                strQuery += @" and F.StateUser_IsApproved=@StateUser_IsApproved";
            }
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

        strQuery = strQuery + @" ORDER BY  F.ReceeEndTime desc,F.ReceeID desc";

        SqlParameter[] arrayparam = new SqlParameter[8];

        arrayparam[0] = new SqlParameter("@p_RegionID", strRegion);
        arrayparam[1] = new SqlParameter("@p_StateID", strState);
        arrayparam[2] = new SqlParameter("@p_CityID", strCity);
        arrayparam[3] = new SqlParameter("@p_FromDate", strFromDate);
        arrayparam[4] = new SqlParameter("@p_ToDate", strToDate);
        arrayparam[5] = new SqlParameter("@p_ReceeUserId", strUserId);
        arrayparam[6] = new SqlParameter("@RegionalUser_IsApproved", drpRegionMngApproved.SelectedValue);
        arrayparam[7] = new SqlParameter("@StateUser_IsApproved", drpStateMngApproved.SelectedValue);

        //dataTb

        DataSet dataDs = DBLayer.getDataSetFromFinalDB(strQuery, arrayparam);

        if (dataDs != null && dataDs.Tables.Count > 0 && dataDs.Tables[0]!=null)
        {
            dataTb = dataDs.Tables[0];
        }

        return dataTb;
    }



    



    protected void btnCityWise_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dataTempTB = getCityRecceStatus();
            string DatetimeStr = System.DateTime.Now.ToString("dddd") + "_" + System.DateTime.Now.ToString("dd-MMMM-yyyy") + System.DateTime.Now.ToLongTimeString();

            if (dataTempTB != null)
            {
                RKLib.ExportData.Export objExport = new RKLib.ExportData.Export("Web");
                objExport.ExportDetails(dataTempTB, RKLib.ExportData.Export.ExportFormat.Excel, "RecceStatus_" + DatetimeStr + ".xls");
            }
        }
        catch (Exception ex)
        {

        }

    }

    private DataTable getCityRecceStatus()
    {
        DataTable dataTb = new DataTable();


        populateFilterPara();
        string strQuery = @" SELECT MAX(Cm.SortBy),
                            (CASE when GROUPING(Cm.CityName)=1 then 'All City Total' else Cm.CityName end) as [City],(CASE WHEN GROUPING(Cm.CityName)!=1 THEN MAX(Sm.StateName) ELSE '' END) as [State],(CASE WHEN GROUPING(Cm.CityName)!=1 THEN MAX(Rm.RegionName) ELSE '' END) as [Region],
                            COUNT(1) as [Total Uploaded Recce],
                            COUNT(case when F.ReceeStatusId=1 then 1 end) as [Total Uploaded Recce Allow],
                            COUNT(case when F.ReceeStatusId!=1 then 1 end) as [Total Uploaded Recce Is Not Allow],
                            COUNT(case when (F.StateUser_IsApproved='Y' ) then 1 end) as [Total State Manager Approval],
                            COUNT(case when (F.RegionalUser_IsApproved='Y') then 1 end) as [Total Regional Manager Approval],
                            COUNT(case when (F.RegionalUser_IsApproved='N') then 1 end) as [Total Regional Manager Rejection],
                            COUNT(case when (F.StateUser_IsApproved='Y' and F.RegionalUser_IsApproved='Y') then 1 end) as [Total Recce is Approved],
                            (COUNT(RefOutletID)-COUNT(distinct RefOutletID)) as [Total Duplicate Recce],
                            (COUNT(case when (F.RegionalUser_IsApproved='Y') then RefOutletID end)-COUNT(distinct case when (F.RegionalUser_IsApproved='Y') then RefOutletID end)) as [Total Duplicate Rm Approved Recce]
                    
                     from NonQcSonyTempRecee F inner join OutletMaster OM
                    on F.RefOutletID=OM.OutletID
                    inner join UserMaster UM
                    on F.RefReceeUserID=UM.UserID
                    inner join ReceeStatusMaster RSM
                    on RSM.ReceeStatusID=F.ReceeStatusId
                    inner join RegionMaster RM
                    on OM.RefRegionID=RM.RegionID
                    inner join StateMaster SM
                    on OM.RefStateID=SM.StateId
                    inner join CityMaster CM
                    on OM.RefCityID=CM.CityID ";

        string strRegion = Region;
        string strState = State;
        string strCity = City;
        string strFromDate = FromDate;
        string strToDate = ToDate;

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

        strQuery = strQuery + @"   group by RollUp(Cm.CityName)";

        SqlParameter[] arrayparam = new SqlParameter[8];

        arrayparam[0] = new SqlParameter("@p_RegionID", strRegion);
        arrayparam[1] = new SqlParameter("@p_StateID", strState);
        arrayparam[2] = new SqlParameter("@p_CityID", strCity);
        arrayparam[3] = new SqlParameter("@p_FromDate", strFromDate);
        arrayparam[4] = new SqlParameter("@p_ToDate", strToDate);
        arrayparam[5] = new SqlParameter("@p_ReceeUserId", strUserId);
        arrayparam[6] = new SqlParameter("@RegionalUser_IsApproved", drpRegionMngApproved.SelectedValue);
        arrayparam[7] = new SqlParameter("@StateUser_IsApproved", drpStateMngApproved.SelectedValue);

        //dataTb

        DataSet dataDs = DBLayer.getDataSetFromFinalDB(strQuery, arrayparam);

        if (dataDs != null && dataDs.Tables.Count > 0 && dataDs.Tables[0] != null)
        {
            dataTb = dataDs.Tables[0];
            dataTb.Columns.RemoveAt(0);
            dataTb.AcceptChanges();
        }


        return dataTb;
   }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
       


        try
        {
            if (txtOutletId.Text.Trim() != "" && txtOutletId.Text.Trim() != "0")
            {

                string strOutletId = Convert.ToString(general.ClearInput(txtOutletId.Text).Trim().Split('-')[0].Trim());

                populateFilterPara();
                tbTableImg.Controls.Clear();
                CreateCustomPaging(strOutletId);
                PopulateAuditGrid(strOutletId);

            }
        }
        catch (Exception ex)
        {

        }


    }


    #region Recce details 

    private void DownloadRecceOutletDumpExcel()
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

            DataTable dataTB = getGridSourceRecceOutletMasterDump();
            grid.DataSource = dataTB;
            grid.DataBind();
            strExcelHtml = general.gwtGridHtml(grid);
        }
        catch (Exception ex)
        {

        }


        Export(strExcelHtml,"OutletMasterDump");

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
            grid.RowDataBound += new GridViewRowEventHandler(grid_RowDataBound);
            grid.DataSource = dataTB;
            grid.DataBind();
            strExcelHtml = general.gwtGridHtml(grid);
        }
        catch (Exception ex)
        {

        }


        Export(strExcelHtml);

    }

    string strCurrentBgColor = "";
    
   protected void grid_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        e.Row.Cells[0].Visible = false;

        if (e.Row.RowType == DataControlRowType.Header)
        {
            for (int count = 0; count < e.Row.Cells.Count; count++)
            {
                if (count == 8)
                {

                    e.Row.Cells[count].Style.Add("background-color", " #F00");
                }
                else
                {

                    e.Row.Cells[count].Style.Add("background-color", strHeaderColor);
                    e.Row.Cells[count].HorizontalAlign = HorizontalAlign.Left;
                }
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
                    e.Row.Cells[count].Style.Add("background-color", strCurrentBgColor);
                    e.Row.Cells[count].HorizontalAlign = HorizontalAlign.Left;
                }

                ColorIndex = ColorIndex + 1;
            }
            else
            {


                for (int count = 0; count < 8; count++)
                {
                    e.Row.Cells[count].Text = "";
                }

                for (int count = 9; count <= 15; count++)
                {
                    e.Row.Cells[count].Style.Add("background-color", strCurrentBgColor);
                    e.Row.Cells[count].HorizontalAlign = HorizontalAlign.Left;
                }

                for (int count = 16; count < e.Row.Cells.Count; count++)
                {
                    e.Row.Cells[count].Text = "";
                }
            }

            e.Row.Cells[8].Style.Add("background-color", " #FBB");

        }
    }
   private DataTable getGridSourceRecceOutletMasterDump()
   {
       string strQuery = @" select OutletID,RetailerID,	REPLACE(REPLACE(REPLACE(OutletName, CHAR(10), ' '), CHAR(13), ' '), CHAR(9), ' ') as [OutletName],RetailerName,		REPLACE(REPLACE(REPLACE(OutletAddress, CHAR(10), ' '), CHAR(13), ' '), CHAR(9), ' ') as [OutletAddress],REPLACE(REPLACE(REPLACE(City, CHAR(10), ' '), CHAR(13), ' '), CHAR(9), ' ') as [City], REPLACE(REPLACE(REPLACE(State, CHAR(10), ' '), CHAR(13), ' '), CHAR(9), ' ') as [State], REPLACE(REPLACE(REPLACE(Region, CHAR(10), ' '), CHAR(13), ' '), CHAR(9), ' ') as [Region],REPLACE(REPLACE(REPLACE(OutletType, CHAR(10), ' '), CHAR(13), ' '), CHAR(9), ' ') as [OutletType] from OutletMaster order by OutletID,City,State,Region desc ;";

       DataTable dataTB = null;

       try
       {
           DataSet dataSetRe = DBLayer.getDataSetFromFinalDB(strQuery);
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
                                '' as [Mantion Error Remarks],
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
                                
                                from NonQcSonyTempRecee F inner join OutletMaster OM
                                on F.RefOutletID=OM.OutletID inner join TempSonyReceeDetails TSRD
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

        if (SelectedValueObj.RegionMngApproved != "0")
        {
            if (drpRegionMngApproved.SelectedValue == "NR")
            {
                strQuery += @" and F.RegionalUser_IsApproved<>'N'";

            }
            else if (SelectedValueObj.RegionMngApproved == "P")
            {
                strQuery += @" and F.RegionalUser_IsApproved is null";

            }
            else
            {
                strQuery += @" and F.RegionalUser_IsApproved=@RegionalUser_IsApproved";
            }
        }

        if (drpStateMngApproved.SelectedValue != "0")
        {
            if (drpStateMngApproved.SelectedValue == "NR")
            {
                strQuery += @" and F.StateUser_IsApproved<>'N'";

            }
            else if (drpStateMngApproved.SelectedValue == "P")
            {
                strQuery += @" and F.StateUser_IsApproved is null";

            }
            else
            {
                strQuery += @" and F.StateUser_IsApproved=@StateUser_IsApproved";
            }
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


        strQuery += @" ORDER BY  F.ReceeID,OM.OutletID,F.ReceeEndTime desc,EM.SortBy,TSRD.ElementNo";


        SqlParameter[] arrayparam = new SqlParameter[8];

        arrayparam[0] = new SqlParameter("@p_RegionID", strRegion);
        arrayparam[1] = new SqlParameter("@p_StateID", strState);
        arrayparam[2] = new SqlParameter("@p_CityID", strCity);
        arrayparam[3] = new SqlParameter("@p_FromDate", SelectedValueObj.FromDate);
        arrayparam[4] = new SqlParameter("@p_ToDate", SelectedValueObj.ToDate);
        arrayparam[5] = new SqlParameter("@p_ReceeUserId", strUserId);
        arrayparam[6] = new SqlParameter("@RegionalUser_IsApproved", SelectedValueObj.RegionMngApproved);
        arrayparam[7] = new SqlParameter("@StateUser_IsApproved", SelectedValueObj.StateMngApproved);

        //@StateUser_IsApproved



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


    private void Export(string strExcelHtml)
    {
        Export(strExcelHtml, "RecceDetails");
    }
    private void Export(string strExcelHtml,string strExcleName)
    {
        //divExcle
        string DatetimeStr = System.DateTime.Now.ToString("dddd") + "_" + System.DateTime.Now.ToString("dd-MMMM-yyyy") + System.DateTime.Now.ToLongTimeString();

        DatetimeStr = DatetimeStr.Replace(' ', '_').Trim();
        Response.Clear();
        Response.Buffer = true;
        Response.ContentType = "application/vnd.ms-excel";
        Response.Charset = "";
        Response.AddHeader("Content-Disposition", "attachment; filename=" + strExcleName + "_" + DatetimeStr + ".xls");
        Page.EnableViewState = false;
        System.IO.StringWriter oStringWriter = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter oHTMLTextWriter = new System.Web.UI.HtmlTextWriter(oStringWriter);
        //Dim     abc As String = tdDataDiv.InnerHtml


        Response.Write(strExcelHtml);
        Response.End();
    }


   

    #endregion

    [System.Web.Services.WebMethod()]
    public static string[] GetList(String prefixText, int count)
    {
        List<String> suggetions = GetOutletDetails(prefixText, count);
        return suggetions.ToArray();
    }

    public static List<String> GetOutletDetails(string str, int count)
    {

        string strUserId = "";
        string strUserRoleId = "";

        bool IsReccePerson = false;

        if (HttpContext.Current.Request.Cookies["UserInfo"] != null)
        {
            strUserId = Convert.ToString(HttpContext.Current.Request.Cookies["UserInfo"]["UserId"]);
            strUserRoleId = Convert.ToString(HttpContext.Current.Request.Cookies["UserInfo"]["UserRoleId"]);
            if (strUserRoleId == "6" || strUserRoleId == "9")
            {
                IsReccePerson = true;
            }
        }

        List<String> suggestions = new List<string>();

        str = general.ClearInput(str);
        str = "" + str + "%";
        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@p_Para", str);
        param[1] = new SqlParameter("@IrisRPID", strUserId);




        string strProc = "select top " + count + " OutletID,OutletName from OutletMaster where @p_Para!='%' and (OutletID like @p_Para OR OutletName like @p_Para) and IsActive=1";
       

        //string strProc = "GetCounterDetials|" + str + "*" + count.ToString() + "^SamsungRace";
        DataSet dataSet = DBLayer.getDataSetFromFinalDB(strProc, param);


        DataTable dataTable = null;

        if (dataSet != null)
        {
            dataTable = dataSet.Tables[0];
        }

        if (dataTable != null && dataTable.Rows.Count > 0)
        {
            for (int count1 = 0; count1 < dataTable.Rows.Count; count1++)
            {

                string suggestion = (dataTable.Rows[count1]["OutletID"] + "-" + dataTable.Rows[count1]["OutletName"]).ToString();
                suggestions.Add(suggestion);
            }
        }
        else
        {
            suggestions.Add("No Data Found");
        }


        return suggestions;
    }

   
  
}