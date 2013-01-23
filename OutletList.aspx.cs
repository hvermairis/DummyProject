using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SonyIris;
using System.Data.SqlClient;
using System.Data;

public partial class OutletList : AbstractBaseClass
{

    #region AbtractorClass Section

    public SelectedValuesClass SelectValuesObj = new SelectedValuesClass();
    public SelectedValuesClass TempSelectValuesObj = new SelectedValuesClass();

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
        if (Request.QueryString["DL"] != null && Request.QueryString["DL"].ToString() == "1")
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
    string strOutletId = "";

    protected void Page_Load(object sender, EventArgs e)
    {

        if (Request.QueryString["OutletId"] != null)
        {
            strOutletId = clsCryptography.Decrypt(Request.QueryString["OutletId"].ToString());
            
        }

        if (Request.QueryString["BackUrl"] != null)
        {

            brandCramas.Visible = false;
            hyperBackUrl.Visible = true;
            hyperBackUrl.NavigateUrl = clsCryptography.Decrypt(Request.QueryString["BackUrl"].ToString());

        }


        if (HttpContext.Current.Request.Cookies["UserInfo"] != null)
        {
            strUserId = Convert.ToString(HttpContext.Current.Request.Cookies["UserInfo"]["UserId"]);
            strUserRoleId = Convert.ToString(HttpContext.Current.Request.Cookies["UserInfo"]["UserRoleId"]);

        }
        else
        {
            Response.Redirect("Login.aspx");
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

            if (Request.QueryString["DL"] != null)
            {
                DownloadExcel();
            }
            else
            {
                BindGrid();
            }

            HiperLExcle.HRef = "OutletList.aspx" + SelectValuesObj.getQueryString()+"&DL=1";

        }
        catch (Exception ex)
        {

        }
    }


    private void DownloadExcel()
    {
        string strExcelHtml = "";

        GridView grid = new GridView();
        grid.Width = Unit.Percentage(100);
        grid.CellPadding = 4;
        grid.BorderWidth = Unit.Pixel(1);
        grid.GridLines = GridLines.Both;

        grid.ID = "grdRecceListDown";

        try
        {
            DataTable dataTB = getGridSource();
            grid.DataSource = dataTB;
            grid.DataBind();
            strExcelHtml = general.gwtGridHtml(grid);
        }
        catch (Exception ex)
        {

        }
       

        Export(strExcelHtml);

    }

    private void Export(string strExcelHtml)
    {
        //divExcle
        string DatetimeStr = System.DateTime.Now.ToString("dddd") + "_" + System.DateTime.Now.ToString("dd-MMMM-yyyy") + System.DateTime.Now.ToLongTimeString();

        DatetimeStr = DatetimeStr.Replace(' ', '_').Trim();
        Response.Clear();
        Response.Buffer = true;
        Response.ContentType = "application/vnd.ms-excel";
        Response.Charset = "";
        Response.AddHeader("Content-Disposition", "attachment; filename=Recce_" + DatetimeStr + ".xls");
        Page.EnableViewState = false;
        System.IO.StringWriter oStringWriter = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter oHTMLTextWriter = new System.Web.UI.HtmlTextWriter(oStringWriter);
        //Dim     abc As String = tdDataDiv.InnerHtml


        Response.Write(strExcelHtml);
        Response.End();
    }
       

    private void BindGrid()
    {

        try
        {
            DataTable dataTB = getGridSource();
            gridAuditDetail.DataSource = dataTB;
            gridAuditDetail.DataBind();
        }
        catch (Exception ex)
        {

        }
    }

    private DataTable getGridSource()
    {
        string strQuery = @"  select  
                    F.ReceeID,
                    RefOutletID as [OutletID],
                    OM.OutletName as [Outlet Name],
                    OM.OutletAddress as [Address],
                    RM.RegionName as [Region],
                    SM.StateName as [State],
                    CM.CityName as [City],
OTM.OutletType,

                    UM.Name as [Recce Person],
                    RSM.ReceeStatus as [Recce Status],
                    CONVERT(varchar(15),F.ReceeEndTime,105) as [Recce Date] ,
                    F.Mno
                 
                    
                     from SonyFinalRecee F inner join OutletMaster OM
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
                    on OM.RefCityID=CM.CityID 
                    inner join OutletTypeMaster OTM
                    on F.RefOutletTypeId=OTM.OutletTypeID 
                    and F.StateUser_IsApproved='Y'
                    and F.RegionalUser_IsApproved='Y'";

        SelectValuesObj = GetPanelSelectedValues();

        SelectValuesObj.OutletId = strOutletId;

        if (SelectValuesObj.Region != "0" && SelectValuesObj.Region != "")
        {
            strQuery = strQuery + @" And ( @p_RegionID ='0' or  OM.RefRegionID in (select * from Split(@p_RegionID,',')) )";

        }
        if (SelectValuesObj.State != "0" && SelectValuesObj.State != "")
        {
            strQuery = strQuery + @" And ( @p_StateID ='0' or  OM.RefStateID in (select * from Split(@p_StateID,',')) )";
        }
        if (SelectValuesObj.City != "0" && SelectValuesObj.City != "")
        {
            strQuery = strQuery + @" And ( @p_CityID ='0' or  OM.RefCityID in (select * from Split(@p_CityID,',')) )";

        }

        strQuery = strQuery + @" And (  @p_OutletTypeId ='0' or   F.RefOutletTypeId in (select * from Split(@p_OutletTypeId,',')) )";
        strQuery += @" And ( ( @p_OutletId ='0' or  @p_OutletId ='' ) or   F.RefOutletID in (select * from Split(@p_OutletId,',')) )";


        strQuery += @" ORDER BY  F.ReceeEndTime desc";


        SqlParameter[] arrayparam = new SqlParameter[5];

        arrayparam[0] = new SqlParameter("@p_RegionID", SelectValuesObj.Region);
        arrayparam[1] = new SqlParameter("@p_StateID", SelectValuesObj.State);
        arrayparam[2] = new SqlParameter("@p_CityID", SelectValuesObj.City);
        arrayparam[3] = new SqlParameter("@p_OutletTypeId", SelectValuesObj.OutletTypeId);
        arrayparam[4] = new SqlParameter("@p_OutletId", SelectValuesObj.OutletId);
        
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

    protected void gridAuditDetail_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gridAuditDetail.PageIndex = e.NewPageIndex;
            BindGrid();
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


            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                e.Row.Cells[1].Text = @"<a href='#' onclick=""javascript:setIframeUrl('OutletReview.aspx?ReceeId=" + clsCryptography.Encrypt(e.Row.Cells[0].Text) + @"');""  >" + e.Row.Cells[1].Text + "</a>";
               // e.Row.Cells[1].Text = @" <a onclick=""return hs.htmlExpand(this, { objectType: 'iframe', width: '1300' } )""  href='OutletReview.aspx?ReceeId=" + clsCryptography.Encrypt(e.Row.Cells[0].Text) + @"'>" + e.Row.Cells[1].Text + "</a>";
            }
        }
    }
  
}