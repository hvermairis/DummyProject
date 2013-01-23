using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SonyIris;
using System.Data.SqlClient;
using System.Data;

public partial class SiteFrontMasterPage : System.Web.UI.MasterPage
{
    private ReportTypeLevel TabControlType = ReportTypeLevel.IndiaLevel;

    private ReportTypeLevel _ControlType = ReportTypeLevel.IndiaLevel;


    public ReportTypeLevel ControlType
    {

        get
        {
            return _ControlType;
        }
        set
        {
            _ControlType = value;
        }

    }
    protected void Page_Load(object sender, EventArgs e)
    {

        if (HttpContext.Current.Request.Cookies["UserInfo"] != null)
        {
            lblUserName.Text = Convert.ToString(HttpContext.Current.Request.Cookies["UserInfo"]["Name"]);


        }
    }
    protected void linkButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("Login.aspx");
    }

    public void SetPanelSelectedValues(SelectedValuesClass SelectValues)
    {
        leftPanel.setIsPostBackFlow(SelectValues);
    }

    public SelectedValuesClass GetPanelSelectedValues()
    {

        SelectedValuesClass selectValues = leftPanel.GetPanelSelectedValues();

        return selectValues;
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (txtOutletId.Text.Trim() != "" && txtOutletId.Text.Trim() != "0")
        {
          
           string strOutletID = Convert.ToString(txtOutletId.Text.Trim().Split('-')[0].Trim());

           string strUrl = "OutletList.aspx?OutletId=" + clsCryptography.Encrypt(strOutletID) + "&BackUrl=" + clsCryptography.Encrypt(Request.Url.ToString());

           Response.Redirect(strUrl);
        }
    }


}
