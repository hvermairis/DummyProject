using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using SonyIris;
using System.Collections.Generic;
using System.Data.SqlClient;

public partial class BrandCramas : System.Web.UI.UserControl
{
    public SelectedValuesClass SelectValues = null;

    private ReportTypeLevel _CurrentPage = ReportTypeLevel.IndiaLevel;
    private ReportTypeLevel TabControlType = ReportTypeLevel.IndiaLevel;

 

    public ReportTypeLevel CurrentPage
    {

        get
        {
            return _CurrentPage;
        }
        set
        {
            _CurrentPage = value;
        }

    }

 
    protected void Page_PreRender(object sender, EventArgs e)
    {
        AbstractBaseClass page = (AbstractBaseClass)Page;
        if (SelectValues == null)
        {
            SelectValues = page.GetPanelSelectedValues();
        }

        if (SelectValues != null)
        {
            List<string> levelCollec = new List<string>();

            TabControlType = BOLayer.getUserType();



            if (TabControlType == ReportTypeLevel.IndiaLevel && CurrentPage == ReportTypeLevel.IndiaLevel)
            {

                levelCollec.Add("India Report");
            }

            if ((TabControlType == ReportTypeLevel.IndiaLevel || TabControlType == ReportTypeLevel.RegionLevel) && CurrentPage == ReportTypeLevel.RegionLevel)
            {
                if (TabControlType == ReportTypeLevel.IndiaLevel)
                {
                    levelCollec.Add("India Report");
                }
                levelCollec.Add("Region Report");
            }

            if ((TabControlType == ReportTypeLevel.IndiaLevel || TabControlType == ReportTypeLevel.RegionLevel || TabControlType == ReportTypeLevel.StateLevel) && CurrentPage == ReportTypeLevel.StateLevel)
            {

                if (TabControlType == ReportTypeLevel.IndiaLevel)
                {
                    levelCollec.Add("India Report");
                }

                if (TabControlType == ReportTypeLevel.IndiaLevel || TabControlType == ReportTypeLevel.RegionLevel)
                {
                    levelCollec.Add("Region Report");
                }

                if (TabControlType == ReportTypeLevel.IndiaLevel || TabControlType == ReportTypeLevel.RegionLevel || TabControlType == ReportTypeLevel.StateLevel)
                {
                    levelCollec.Add("State Report");
                }
                
            }

            if ((TabControlType == ReportTypeLevel.IndiaLevel || TabControlType == ReportTypeLevel.RegionLevel || TabControlType == ReportTypeLevel.CityLevel) && CurrentPage == ReportTypeLevel.CityLevel)
            {

                if (TabControlType == ReportTypeLevel.IndiaLevel)
                {
                    levelCollec.Add("India Report");
                }

                if (TabControlType == ReportTypeLevel.IndiaLevel || TabControlType == ReportTypeLevel.RegionLevel)
                {
                    levelCollec.Add("Region Report");
                }

                if (TabControlType == ReportTypeLevel.IndiaLevel || TabControlType == ReportTypeLevel.RegionLevel || TabControlType == ReportTypeLevel.StateLevel)
                {
                    levelCollec.Add("State Report");
                }
                levelCollec.Add("City Report");
            }

            if (CurrentPage == ReportTypeLevel.OutletListLevel)
            {

                if (TabControlType == ReportTypeLevel.IndiaLevel)
                {
                    levelCollec.Add("India Report");
                }

                if (TabControlType == ReportTypeLevel.IndiaLevel || TabControlType == ReportTypeLevel.RegionLevel)
                {
                    levelCollec.Add("Region Report");
                }

                if (TabControlType == ReportTypeLevel.IndiaLevel || TabControlType == ReportTypeLevel.RegionLevel || TabControlType == ReportTypeLevel.StateLevel)
                {
                    levelCollec.Add("State Report");
                }

                if (TabControlType == ReportTypeLevel.IndiaLevel || TabControlType == ReportTypeLevel.RegionLevel || TabControlType == ReportTypeLevel.StateLevel || TabControlType == ReportTypeLevel.CityLevel)
                {
                    levelCollec.Add("City Report");
                }


                levelCollec.Add("Outlet List");

            }

           
            string LinkTable = @"<table cellpadding=3 cellspacing='0' border='0'><tr>";

            for (int count = 0; count < levelCollec.Count; count++)
            {
                LinkTable += "<td>";
                if (count == 0)
                {
                    LinkTable += getLinkCell(levelCollec[count]);
                }
                else
                {

                    LinkTable += " » " + getLinkCell(levelCollec[count]);
                }


                LinkTable += "</td>";
            }

            LinkTable += "</tr></table>";

            divBrand.InnerHtml = LinkTable;
        }
    }





    private string getLinkCell(string linkName)
    {
        string linkHtml = "";
        string linkUrl = "";

        AbstractBaseClass page = (AbstractBaseClass)Page;

        SelectValues = page.GetPanelSelectedValues();

        if (linkName == "India Report")
        {

            SelectValues.PageType = ReportTypeLevel.IndiaLevel;
            SelectValues.Region = "0";
            string strQueryStr = SelectValues.getQueryString();
            linkUrl = "RecceDashboard.aspx" + strQueryStr;
        }
        else if (linkName == "Region Report")
        {
            SelectValues.PageType = ReportTypeLevel.RegionLevel;
            SelectValues.State = "0";
            SelectValues.City = "0";
            string strQueryStr = SelectValues.getQueryString();
            linkUrl = "RecceDashboard.aspx" + strQueryStr;
        }
        else if (linkName == "State Report")
        {
            SelectValues.PageType = ReportTypeLevel.StateLevel;
            SelectValues.Region = getRegionsFromStates(SelectValues.State);
            SelectValues.State = getStatesFromRegion(SelectValues.Region);
            SelectValues.City = "0";
            string strQueryStr = SelectValues.getQueryString();
            linkUrl = "RecceDashboard.aspx" + strQueryStr;
        }
        else if (linkName == "City Report")
        {
            SelectValues.PageType = ReportTypeLevel.CityLevel;
            SelectValues.State=getStatesFromCitiesID(SelectValues.City);
            SelectValues.City = getCitiesIDFromStates(SelectValues.State);
            string strQueryStr = SelectValues.getQueryString();
            linkUrl = "RecceDashboard.aspx" + strQueryStr;

        }
        else if (linkName == "Outlet List")
        {
            SelectValues.PageType = ReportTypeLevel.OutletListLevel;
            SelectValues.State = getStatesFromCitiesID(SelectValues.City);
            string strQueryStr = SelectValues.getQueryString();
            linkUrl = "OutletList.aspx" + strQueryStr;
        }


        linkHtml = "<a href='" + linkUrl + "'>" + linkName + "</a>";

        return linkHtml;
    }

    private string getStatesFromRegion(string strRegionIs)
    {

        string strRegionsIds = "";
        string strQuery = "select distinct StateId from StateMaster where IsActive=1 and RefRegionId in(select * from Split(@p_RegionIs,','))";

        SqlParameter[] arrayparam = new SqlParameter[1];

        arrayparam[0] = new SqlParameter("@p_RegionIs", strRegionIs);

        DataTable dataTB = DBLayer.getDataSetFromFinalDB(strQuery, arrayparam).Tables[0];

        if (dataTB != null)
        {
            for (int count = 0; count < dataTB.Rows.Count; count++)
            {
                strRegionsIds = strRegionsIds + dataTB.Rows[count]["StateId"].ToString() + ",";
            }
        }

        if (strRegionsIds != "")
        {
            strRegionsIds ="0,"+ strRegionsIds.Substring(0, strRegionsIds.Length - 1);
        }
        else
        {
            strRegionsIds = "0";
        }

        return strRegionsIds;
    }


    private string getRegionsFromStates(string strStateIs)
    {
        
        string strRegionsIds="";
        string strQuery = "select distinct RefRegionId from StateMaster where IsActive=1 and StateId in(select * from Split(@p_StateIs,','))";

        SqlParameter[] arrayparam = new SqlParameter[1];

        arrayparam[0] = new SqlParameter("@p_StateIs", strStateIs);

        DataTable dataTB = DBLayer.getDataSetFromFinalDB(strQuery, arrayparam).Tables[0];

        if (dataTB != null)
        {
            for (int count = 0; count < dataTB.Rows.Count; count++)
            {
                strRegionsIds = strRegionsIds + dataTB.Rows[count]["RefRegionId"].ToString() + ",";
            }
        }

        if (strRegionsIds != "")
        {
            strRegionsIds = strRegionsIds.Substring(0, strRegionsIds.Length-1);
        }
        else
        {
            strRegionsIds = "0";
        }

        return strRegionsIds;
    }


    private string getCitiesIDFromStates(string strStatesIDs)
    {

        string strRefStateIds = "";
        string strQuery = "select distinct CityId  from CityMaster where IsActive=1 and RefStateId in(select * from Split(@p_StateIds,','))";

        SqlParameter[] arrayparam = new SqlParameter[1];

        arrayparam[0] = new SqlParameter("@p_StateIds", strStatesIDs);

        DataTable dataTB = DBLayer.getDataSetFromFinalDB(strQuery, arrayparam).Tables[0];

        if (dataTB != null)
        {
            for (int count = 0; count < dataTB.Rows.Count; count++)
            {
                strRefStateIds = strRefStateIds + dataTB.Rows[count]["CityId"].ToString() + ",";
            }
        }

        if (strRefStateIds != "")
        {
            strRefStateIds = "0," + strRefStateIds.Substring(0, strRefStateIds.Length - 1);
        }
        else
        {
            strRefStateIds = "0";
        }

        return strRefStateIds;
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
}
