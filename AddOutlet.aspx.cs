using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;

public partial class AddOutlet : System.Web.UI.Page
{
    string strUserId = "";
    string strUserRoleId = "";
    
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


        if (!IsPostBack && Request.QueryString["Msg"] != null)
        {
            lblMsg.Text =clsCryptography.Decrypt( Request.QueryString["Msg"]).ToString();
            lblMsg.ForeColor = Color.Green;
            
        }
        else
        {
            lblMsg.Text = "";
        }



        if (!IsPostBack)
        {
            populateRegionAndOutletType();
           
        }

        if (strUserId != "183")
        {
            Response.Redirect("Login.aspx");
        }
       

        lblMsgCityName.Text="";

        txtRetailerContactNo.Attributes.Add("onKeyPress", "javascript:return checkNum(event)");
        

        linkCityAdd.Attributes.Add("OnClick", "return ValidateCity('" + txtCityName.ClientID + "')");
        btnSearch.Attributes.Add("OnClick", "return ValidateOutletName('" + txtOutletName.ClientID + "')");
        btnSave.Attributes.Add("OnClick", "return ValidateOutletDetails(this,'" + txtNewOutletName.ClientID + "','" + drpOutletType.ClientID + "','" + txtOutletAddress.ClientID + "')");
    }
    protected void drpRegion_SelectedIndexChanged(object sender, EventArgs e)
    {
        divCity.Style.Add("display", "none");
        if (drpRegion.SelectedValue != "0")
        {
            divState.Style.Add("display", "");
            
            populateState();
            populateCity();
        }
        else
        {
            divState.Style.Add("display", "none");
           
        }

    }

    private void populateRegionAndOutletType()
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
            rowRegion[1] = "Select";
            regionTb.Rows.InsertAt(rowRegion, 0);
            regionTb.AcceptChanges();

            drpRegion.DataTextField = "Region";
            drpRegion.DataValueField = "RegionID";

            drpRegion.DataSource = regionTb;
            drpRegion.DataBind();
            drpRegion.SelectedIndex = 0;



            DataTable OutletTypeTb = dataSet.Tables[1];
            DataRow rowOutletType = OutletTypeTb.NewRow();

            rowOutletType[0] = "0";
            rowOutletType[1] = "Select";
            OutletTypeTb.Rows.InsertAt(rowOutletType, 0);
            OutletTypeTb.AcceptChanges();

            drpOutletType.DataTextField = "OutletType";
            drpOutletType.DataValueField = "OutletTypeID";

            drpOutletType.DataSource = OutletTypeTb;
            drpOutletType.DataBind();
            drpOutletType.SelectedIndex = 0;


        }
    }

    private void populateState()
    {

        DataTable StateSource = null;

        string whereCondition = drpRegion.SelectedValue;


        if (whereCondition == "0")
        {
            whereCondition = "-1";
        }

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


            drpState.DataTextField = "State";
            drpState.DataValueField = "StateID";

            drpState.DataSource = StateSource;
            drpState.DataBind();
        }
    }

    private void populateCity()
    {
        string whereCondition = drpState.SelectedValue;

        if (whereCondition == "0")
        {
            whereCondition = "-1";
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
        rowCity[1] = "Insert City";
        CitySource.Rows.InsertAt(rowCity, 0);
        CitySource.AcceptChanges();

        rbListCity.DataTextField = "City";
        rbListCity.DataValueField = "CityID";

        rbListCity.DataSource = CitySource;
        rbListCity.DataBind();
    }



    private void populateGrid(string strOutltName)
    {
        
      
        string strOutletQuery = @" select  
                   
                    OM.OutletID as [OutletID],
                    OM.OutletName as [Outlet Name],
                    OM.OutletAddress as [Address],
                    RM.RegionName as [Region],
                    SM.StateName as [State],
                    CM.CityName as [City],
OTM.OutletType
from OutletMaster Om   inner join RegionMaster RM
                    on OM.RefRegionID=RM.RegionID
                    inner join StateMaster SM
                    on OM.RefStateID=SM.StateId
                    inner join CityMaster CM
                    on OM.RefCityID=CM.CityID 
                    inner join OutletTypeMaster OTM
                    on OM.RefOutletTypeId=OTM.OutletTypeID
                     and OM.OutletName like @p_OutletName
                    and OM.RefRegionID=@p_RegionID
                     and OM.RefStateID=@p_StateID
                     and OM.RefCityID=@p_CityID

                     order by OM.OutletID desc";

        SqlParameter[] arrayparam = new SqlParameter[4];

        arrayparam[0] = new SqlParameter("@p_OutletName", strOutltName);
        arrayparam[1] = new SqlParameter("@p_RegionID", drpRegion.SelectedValue);
        arrayparam[2] = new SqlParameter("@p_StateID", drpState.SelectedValue);
        arrayparam[3] = new SqlParameter("@p_CityID", rbListCity.SelectedValue);


        DataSet dataSet = DBLayer.getDataSetFromFinalDB(strOutletQuery, arrayparam);

        gridOutletDetail.DataSource = dataSet;
        gridOutletDetail.DataBind();

        //gridAuditDetail%
    }

    protected void drpState_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (drpState.SelectedValue != "0")
        {
            divCity.Style.Add("display", "");
            populateCity();
        }
        else
        {
            divCity.Style.Add("display", "none");
        }
    }

   



    protected void linkCityAdd_Click(object sender, EventArgs e)
    {
        string strCityName = general.ClearInput(txtCityName.Text);
        string strStateId = drpState.SelectedValue;
        string strRegionId = drpRegion.SelectedValue;

        string strCityID = "0";
        //Code For Add City

        DataTable datacity = null;

        string whereCondition = drpState.SelectedValue;
        string strProc = "exec urd_InsertedCity @p_UserID,@p_RegionID,@p_StateID,@p_CityName";
        
        SqlParameter[] arrayparam = new SqlParameter[4];

        arrayparam[0] = new SqlParameter("@p_RegionID", strRegionId);
        arrayparam[1] = new SqlParameter("@p_StateID", strStateId);
        arrayparam[2] = new SqlParameter("@p_CityName", strCityName);
        arrayparam[3] = new SqlParameter("@p_UserID", strUserId);

        DataSet dataSet = DBLayer.getDataSetFromFinalDB(strProc, arrayparam);
       
        if (dataSet != null && dataSet.Tables.Count > 0)
        {
            datacity = dataSet.Tables[0];
        }


        if (datacity != null && datacity.Rows.Count > 0)
        {
            strCityID = datacity.Rows[0][0].ToString();

            if (strCityID != "-1")
            {
                populateCity();

                rbListCity.SelectedValue = strCityID;
                txtCityName.Text = "";
                lblMsgCityName.Text = "City has been inserted";
                lblMsgCityName.ForeColor = Color.Green;

                if (rbListCity.SelectedValue == "0" || rbListCity.SelectedValue == "-1")
                {
                    btnInsertCity.Visible = false;
                    divAddCity.Style.Add("display", "");

                }
                else
                {
                    btnInsertCity.Visible = true;
                    divAddCity.Style.Add("display", "none");
                }
            }
            else
            {
                lblMsgCityName.Text = strCityName+" city is already exists into our database";
                lblMsgCityName.ForeColor = Color.Red;
            }


        }
        else
        {
            lblMsgCityName.Text = "City is not inserted";
            lblMsgCityName.ForeColor = Color.Red;
        }

    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        divCityGrid.Style.Add("display","");
        try
        {
            string strOutltName = general.ClearInput(txtOutletName.Text) + "%";
            populateGrid(strOutltName);
        }
        catch (Exception ex)
        {

        }
    }

    
    protected void btnSave_Click(object sender, EventArgs e)
    {
       // btnSave.Enabled = false;

        string strOutletName = general.ClearInput(txtNewOutletName.Text);
        string strOutletAddress = general.ClearInput(txtOutletAddress.Text);
        string strOutletType = drpOutletType.SelectedValue;

        string strRetailerName = general.ClearInput(txtRetailerName.Text);
        string strRetailerContactNo = general.ClearInput(txtRetailerContactNo.Text);
        
        int result=0;

        try
        {
            DataTable dataOutlet = null;

            string whereCondition = drpState.SelectedValue;
            string strProc = "exec urd_InsertedOutlet @p_UserID,@p_RetailerName,@p_RetailerContactNo,@p_RegionID,@p_StateID,@p_CityID,@p_OutletTypeId,@p_RegionName,@p_StateName,@p_CityName,@p_OutletType,@p_OutletName,@p_OutletAddress";

            SqlParameter[] arrayparam = new SqlParameter[13];

            arrayparam[0] = new SqlParameter("@p_RegionID", drpRegion.SelectedValue);
            arrayparam[1] = new SqlParameter("@p_StateID", drpState.SelectedValue);
            arrayparam[2] = new SqlParameter("@p_CityID", rbListCity.SelectedValue);
            arrayparam[3] = new SqlParameter("@p_OutletTypeId", strOutletType);

            arrayparam[4] = new SqlParameter("@p_RegionName", drpRegion.SelectedItem.Text);
            arrayparam[5] = new SqlParameter("@p_StateName", drpState.SelectedItem.Text);
            arrayparam[6] = new SqlParameter("@p_CityName", rbListCity.SelectedItem.Text);
            arrayparam[7] = new SqlParameter("@p_OutletType", drpOutletType.SelectedItem.Text);

            arrayparam[8] = new SqlParameter("@p_OutletName", strOutletName);
            arrayparam[9] = new SqlParameter("@p_OutletAddress", strOutletAddress);
            arrayparam[10] = new SqlParameter("@p_UserID", strUserId);
            arrayparam[11] = new SqlParameter("@p_RetailerName", strRetailerName);
            arrayparam[12] = new SqlParameter("@p_RetailerContactNo", strRetailerContactNo);
           

            DataSet dataSet = DBLayer.getDataSetFromFinalDB(strProc, arrayparam);

            if (dataSet != null && dataSet.Tables.Count > 0)
            {
                dataOutlet = dataSet.Tables[0];

                string strOutltForGridName = general.ClearInput(strOutletName) + "%";
                populateGrid(strOutltForGridName);

                lblOutletInsertMng.Text = "Your Insert Outlet ID is " + dataOutlet.Rows[0][0].ToString();
                lblOutletInsertMng.ForeColor = Color.Green;

                string strFromEmailId = "";
                string name = "";
                string strToEmail = "utkarsh.deep@iris-worldwide.com,kush.srivastava@iris-worldwide.com,iris.rajat@gmail.com";
                string body = getMailBoby(name, strOutletName, rbListCity.SelectedItem.Text, drpState.SelectedItem.Text, drpRegion.SelectedItem.Text, dataOutlet.Rows[0][0].ToString());

                string strSubject = "New Outlet  - " + drpRegion.SelectedItem.Text + " - " + drpState.SelectedItem.Text + ": " + dataOutlet.Rows[0][0].ToString() + " " + strOutletName + " ";
                string strCcEMailIs = "";
                string strBCCEMailIs = "hvermamzn@gmail.com,abhidang@gmail.com";

                try
                {
                    BOLayer boLayer = new BOLayer();
                    boLayer.sendEmail(strFromEmailId, name, strToEmail, body, strSubject, strCcEMailIs, strBCCEMailIs);
                }
                catch (Exception)
                {

                }


                txtNewOutletName.Text="";
                txtOutletAddress.Text="";
                txtRetailerName.Text="";
                txtRetailerContactNo.Text="";
                drpOutletType.SelectedIndex=0;

                Response.Redirect("AddOutlet.aspx?Msg=" + clsCryptography.Encrypt(lblOutletInsertMng.Text));

            }
            else
            {

                lblOutletInsertMng.Text = "Outlet is not inserted";
                lblOutletInsertMng.ForeColor = Color.Red;
            }
        }
        catch (Exception ex)
        {

        }

    }

    private string getMailBoby(string Name, string strOuletName, string City, string State, string Region, string strOuletID)
    {

        string strDate = System.DateTime.Now.ToString("dd-MMM-yyyy");
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
                New Outlet  <strong>" + strOuletID + @" " + strOuletName + @" </strong>has been created in  " + City + @"  under " + State + " - " + Region + @" Region.
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
        <tr>
            <td colspan='2'>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td colspan='2'>
            Thanks,
            </td>
        </tr>
        <tr>
            <td colspan='2'>
                &nbsp;Iris Tech Support Team.
            </td>
        </tr>
        </table>";


        return strMailBody;
    }


   
    protected void btnInsertCity_Click(object sender, EventArgs e)
    {
        divInsertCity.Style.Add("display", "");
        divCityArea.Style.Add("display", "none");



    }
    protected void rbListCity_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbListCity.SelectedValue == "0" || rbListCity.SelectedValue == "-1")
        {
            btnInsertCity.Visible = false;
            divAddCity.Style.Add("display","");

        }
        else
        {
            btnInsertCity.Visible = true;
            divAddCity.Style.Add("display", "none");
        }
    }

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




        string strProc = "select top " + count + " OutletID,OutletName from OutletMaster where @p_Para!='%' and (OutletName like @p_Para) and IsActive=1";
        if (IsReccePerson)
        {
            strProc += " and IrisRPID=@IrisRPID";
        }

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

                string suggestion = (dataTable.Rows[count1]["OutletName"]).ToString();
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