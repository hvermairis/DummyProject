using System;
using System.Collections.Generic;
using System.Web;
using SonyIris;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for AbstractBaseClass
/// </summary>
public abstract class AbstractBaseClass : System.Web.UI.Page
{

	public AbstractBaseClass()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public abstract void setParamenter(SelectedValuesClass selectObj);

    public abstract void SetPanelSelectedValues(SelectedValuesClass selectObj);
    public abstract SelectedValuesClass GetPanelSelectedValues();

   

    public SelectedValuesClass getValuesObjFromQueryString()
    {
        SelectedValuesClass p_Obj = new SelectedValuesClass();

        p_Obj.setValues();

        return p_Obj;

    }

    public string getProductImgUrl(string productId)
    {
        string strImgUrl = "~/images/Refrigerator.png";

        switch (productId)   
        {
            case "1":
                strImgUrl = "~/images/Refrigerator.png";
                break;
            case "2":
                strImgUrl = "~/images/washingmachine.png";
                break;
            case "3":
                strImgUrl = "~/images/ac.png";
                break;
            case "4":
                strImgUrl = "~/images/MicrowaveOven.png";
                break;
            case "5":
                strImgUrl = "~/images/DigitalImaging.png";
                break;

        }

        return strImgUrl;

        //return strImgUrl;
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




        string strProc = "select top " + count + " OutletID,OutletName from OutletMaster where @p_Para!='%' and (OutletID like @p_Para OR OutletName like @p_Para) and IsActive=1";
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


    public override void VerifyRenderingInServerForm(System.Web.UI.Control control)
    {
    }

}
