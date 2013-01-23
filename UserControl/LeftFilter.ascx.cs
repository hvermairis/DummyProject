using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SonyIris;
using System.Data.SqlClient;
using System.Data;

public partial class UserControl_LeftFilter : System.Web.UI.UserControl
{
    SelectedValuesClass SelectValuesObj = new SelectedValuesClass();
    string strUserId = "0";
    string strUserRoleId = "0";
    ReportTypeLevel UserType = ReportTypeLevel.IndiaLevel;


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

    protected void Page_Load(object sender, EventArgs e)
    {
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
       

        if (hdnIsUsercontrol.Value != "")
        {
            if (hdnIsUsercontrol.Value == ReportTypeLevel.IndiaLevel.ToString())
            {
                IsUseInControl = ReportTypeLevel.IndiaLevel;
            }
            else if (hdnIsUsercontrol.Value == ReportTypeLevel.RegionLevel.ToString())
            {
                IsUseInControl = ReportTypeLevel.RegionLevel;
            }
            else if (hdnIsUsercontrol.Value == ReportTypeLevel.StateLevel.ToString())
            {
                IsUseInControl = ReportTypeLevel.StateLevel;
            }
            else if (hdnIsUsercontrol.Value == ReportTypeLevel.CityLevel.ToString())
            {
                IsUseInControl = ReportTypeLevel.CityLevel;
            }
            else if (hdnIsUsercontrol.Value == ReportTypeLevel.OutletListLevel.ToString())
            {
                IsUseInControl = ReportTypeLevel.OutletListLevel;
            }
        }
        else
        {
       
        }



        if (!IsPostBack)
        {

            SelectValuesObj.setValues();
            IsUseInControl = SelectValuesObj.PageType;
            hdnIsUsercontrol.Value = SelectValuesObj.PageType.ToString();
            setIsPostBackFlow(SelectValuesObj);
        }

       
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        hdnIsUsercontrol.Value = IsUseInControl.ToString();
  
        setVisibility();

        attachJavascriptFunction();
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

            checkBox.Items[count].Attributes.Add("OnClick", "checkUncheckCheckBox('" + checkBox.ClientID + "'," + checkBox.Items.Count + ")");

        }


    }


    public void setIsPostBackFlow(SelectedValuesClass TempSelectValuesObj)
    {
        SelectValuesObj = TempSelectValuesObj;

        BindControl();
       

        SelectControls();
    
    }

    private void SelectControls()
    {
        SelectedValuesClass SelectValues = SelectValuesObj;

        if (UserType == ReportTypeLevel.IndiaLevel && (IsUseInControl == ReportTypeLevel.RegionLevel || IsUseInControl == ReportTypeLevel.StateLevel))
           setSelectedValue(chkRegion, SelectValues.Region);
        
        populateStates();

        if ((UserType == ReportTypeLevel.IndiaLevel || UserType == ReportTypeLevel.RegionLevel) && (IsUseInControl == ReportTypeLevel.StateLevel || IsUseInControl == ReportTypeLevel.CityLevel || IsUseInControl == ReportTypeLevel.OutletListLevel))
            setSelectedValue(chkState, SelectValues.State);
        else
            selectAll(chkState);
        
        
        populateCities();

        if (IsUseInControl != ReportTypeLevel.RegionLevel)
        {
            if (SelectValues.City != "0" && SelectValues.City != "-1")
            {
                if ((UserType == ReportTypeLevel.IndiaLevel || UserType == ReportTypeLevel.RegionLevel || UserType == ReportTypeLevel.StateLevel) && (IsUseInControl == ReportTypeLevel.CityLevel || IsUseInControl == ReportTypeLevel.OutletListLevel))
                    setSelectedValue(chkCity, SelectValues.City);
                else
                {
                    allSetSelectedValue(chkCity);
                }
            }
            else
            {
                allSetSelectedValue(chkCity);

            }
        }
        else
        {
            allSetSelectedValue(chkCity);
        }


        rBlOutletType.SelectedValue = SelectValues.OutletTypeId;

    }

    protected void chkRegion_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            populateStates();

            selectAll(chkState);



            populateCities();
            selectAll(chkCity);
        }
        catch (Exception ex)
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
        catch (Exception ex)
        {
        }
    }

    public SelectedValuesClass GetPanelSelectedValues()
    {
       SelectValuesObj= populateSession();
        SelectedValuesClass selectValues = SelectValuesObj;
        return selectValues;


    }
    private SelectedValuesClass populateSession()
    {
        //SelectedValuesClass SelectValues = new SelectedValuesClass();
        string selectValue = "";

        selectValue = getSelectedValue(chkRegion);
        
        int  selectregionCount = getSelectedCount(chkRegion);


        if (selectValue == "")
        {
            selectValue = "0";
        }

        if (selectregionCount == 1)
        {
            SelectValuesObj.Region = hdnRegionIds.Value;
        }
        else
        {
            SelectValuesObj.Region = selectValue;
            hdnRegionIds.Value = selectValue;
        }
      

        selectValue = "";
        selectValue = getSelectedValue(chkState);
        if (selectValue == "")
        {
            selectValue = "0";
        }

        int selectStateCount = getSelectedCount(chkState);


        if (selectStateCount == 1)
        {
            SelectValuesObj.State = hdnStateIds.Value;
        }
        else
        {
            SelectValuesObj.State = selectValue;
            hdnStateIds.Value = selectValue;
        }
      
       


        selectValue = "";
        selectValue = getSelectedValue(chkCity);
        if (selectValue == "")
        {
            selectValue = "0";
        }

        SelectValuesObj.City = selectValue;
        setPageType();
        SelectValuesObj.PageType = getReportType();

        try
        {
            if (chkRegion.SelectedItem != null)
                SelectValuesObj.RegionName = chkRegion.SelectedItem.Text;

            if (chkCity.SelectedItem != null)
                SelectValuesObj.StateName = chkState.SelectedItem.Text;
        }
        catch (Exception ex)
        {

        }


        SelectValuesObj.OutletTypeId = rBlOutletType.SelectedValue;
        SelectValuesObj.OutletType = rBlOutletType.SelectedItem.Text;


        return SelectValuesObj;

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
    private void setSelectedValue(CheckBoxList chkList, string selectValues)
    {
        string[] arrSelectedValue = selectValues.Split(',');

        if (selectValues != "" && selectValues != "0")
        {
            for (int count = 0; count < chkList.Items.Count; count++)
            {
                chkList.Items[count].Selected = false;

            }

            for (int count = 0; count < chkList.Items.Count; count++)
            {
                for (int countSel = 0; countSel < arrSelectedValue.Length; countSel++)
                {
                    if (chkList.Items[count].Value == arrSelectedValue[countSel])
                    {
                        chkList.Items[count].Selected = true;

                    }
                }
            }

        }


    }


    private void allSetSelectedValue(CheckBoxList chkList)
    {

        for (int count = 0; count < chkList.Items.Count; count++)
        {
            chkList.Items[count].Selected = true;

        }

    }

    public void toRedirectToPage(SelectedValuesClass p_SelectValuesObj)
    {
        ReportTypeLevel resultPageType = getReportType();

        string strQueryStr = SelectValuesObj.getQueryString();

        AbstractBaseClass page = (AbstractBaseClass)Page;
        page.setParamenter(SelectValuesObj);

    }

    private ReportTypeLevel getReportType()
    {
        ReportTypeLevel ReportLevel = UserType;
        int selectCount = 0;


        if (IsUseInControl == ReportTypeLevel.IndiaLevel && UserType == ReportTypeLevel.IndiaLevel)
        {
             if (!chkRegion.Items[0].Selected || chkRegion.Items.Count == 2)
            {
                ReportLevel = ReportTypeLevel.RegionLevel;
            }
           
        }
        else if ((IsUseInControl == ReportTypeLevel.RegionLevel) && (UserType == ReportTypeLevel.RegionLevel || UserType == ReportTypeLevel.IndiaLevel))
        {
            selectCount = getSelectedCount(chkRegion);

            if (selectCount == 1 && selectCount < chkRegion.Items.Count)
            {
                ReportLevel = ReportTypeLevel.StateLevel;
            }
            else if (!chkRegion.Items[0].Selected || chkRegion.Items.Count == 2)
            {
                ReportLevel = ReportTypeLevel.RegionLevel;
            }

        }
        else if ((IsUseInControl == ReportTypeLevel.StateLevel) && (UserType == ReportTypeLevel.StateLevel || UserType == ReportTypeLevel.RegionLevel || UserType == ReportTypeLevel.IndiaLevel))
        {

            selectCount = getSelectedCount(chkState);

            if (selectCount == 1 && selectCount < chkState.Items.Count)
            {
                ReportLevel = ReportTypeLevel.CityLevel;
            }
            else  if (!chkState.Items[0].Selected || chkState.Items.Count == 2)
            {
                ReportLevel = ReportTypeLevel.StateLevel;
            }
         

        }
        else if ((IsUseInControl == ReportTypeLevel.CityLevel) && (UserType == ReportTypeLevel.CityLevel || UserType == ReportTypeLevel.StateLevel || UserType == ReportTypeLevel.RegionLevel || UserType == ReportTypeLevel.IndiaLevel))
        {
            ReportLevel = ReportTypeLevel.CityLevel;
        }
        else if ((IsUseInControl == ReportTypeLevel.OutletListLevel) && (UserType == ReportTypeLevel.OutletListLevel || UserType == ReportTypeLevel.CityLevel || UserType == ReportTypeLevel.RegionLevel || UserType == ReportTypeLevel.IndiaLevel))
        {
            ReportLevel = ReportTypeLevel.OutletListLevel;
        }


        if (ReportLevel == UserType)
        {
            ReportLevel = IsUseInControl;

        }
        else
        {
            IsUseInControl = ReportLevel;
        }


        return ReportLevel;
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

            DataTable OutletTypeTb = dataSet.Tables[1];
            DataRow rowOutletType = OutletTypeTb.NewRow();

            rowOutletType[0] = "0";
            rowOutletType[1] = "All";
            OutletTypeTb.Rows.InsertAt(rowOutletType, 0);
            OutletTypeTb.AcceptChanges();

            rBlOutletType.DataTextField = "OutletType";
            rBlOutletType.DataValueField = "OutletTypeID";

            rBlOutletType.DataSource = OutletTypeTb;
            rBlOutletType.DataBind();
            rBlOutletType.SelectedIndex = 0;



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

    private int getSelectedCount(CheckBoxList chkList)
    {
        int selectCount = 0;

        for (int count = 0; count < chkList.Items.Count; count++)
        {
            if (chkList.Items[count].Selected)
                selectCount++;
        }

        return selectCount;
    }


    #endregion
    private void setPageType()
    {
        if (Request.QueryString["PageType"] != null && !IsPostBack)
        {
            ReportTypeLevel PageType = ReportTypeLevel.IndiaLevel;
            string strPageType=clsCryptography.Decrypt(Request.QueryString["PageType"].ToString());

            if (strPageType == ReportTypeLevel.IndiaLevel.ToString())
            {
                PageType = ReportTypeLevel.IndiaLevel;
            }
            else if (strPageType == ReportTypeLevel.RegionLevel.ToString())
            {
                PageType = ReportTypeLevel.RegionLevel;
            }
            else if (strPageType == ReportTypeLevel.StateLevel.ToString())
            {
                PageType = ReportTypeLevel.StateLevel;
            }
            else if (strPageType == ReportTypeLevel.CityLevel.ToString())
            {
                PageType = ReportTypeLevel.CityLevel;
            }
            else
            {
                PageType = ReportTypeLevel.OutletListLevel;
            }

            IsUseInControl=PageType;

        }
        else
        {
            if (!IsPostBack)
            {
                if (SelectValuesObj.PageType!=ReportTypeLevel.IndiaLevel)
                  SelectValuesObj.setValues();


                IsUseInControl=SelectValuesObj.PageType;

            }

           
        }

    }

    private void setVisibility()
    {

        // India Level
        if (IsUseInControl == ReportTypeLevel.IndiaLevel) // City Wise Access
        {

            trState.Visible = false;
            trCity.Visible = false;

            trRegion.Visible = true;

        }

        //Region Level
        if (IsUseInControl == ReportTypeLevel.RegionLevel) // City Wise Access
        {
            trState.Visible = false;
            trCity.Visible = false;

            trRegion.Visible = true;
        }

        if (IsUseInControl == ReportTypeLevel.StateLevel) // City Wise Access
        {
            trRegion.Visible = false;
            trCity.Visible = false;

            trState.Visible = true;
        }

        if (IsUseInControl == ReportTypeLevel.CityLevel) // City Wise Access
        {
            trRegion.Visible = false;
            trState.Visible = false;

            trCity.Visible = true;
        }


        if (IsUseInControl == ReportTypeLevel.OutletListLevel)
        {
            trRegion.Visible = false;
            trState.Visible = false;

            trCity.Visible = true;
           
        }

    }

    protected void imgBtn_Click(object sender, ImageClickEventArgs e)
    {
      
        if (IsUseInControl == ReportTypeLevel.IndiaLevel)
        {
            populateStates();
            selectAll(chkState);
            populateCities();
            selectAll(chkCity);

        }
        else if (IsUseInControl == ReportTypeLevel.RegionLevel)
        {

            populateStates();
            selectAll(chkState);
            populateCities();
            selectAll(chkCity);

         

        }
        else if (IsUseInControl == ReportTypeLevel.StateLevel)
        {
            populateCities();
            selectAll(chkCity);
        }


      

       // setPageType();
        //setVisibility();


        toRedirectToPage(SelectValuesObj);
        populateSession();

                
    }
}