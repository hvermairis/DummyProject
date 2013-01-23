using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Security.Cryptography;
using System.IO;
using System.Text;

/// <summary>
/// Summary description for general
/// </summary>
public class general
{
	public general()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public static string getDate(DateTime date)
    {
        return date.ToString("yyyy-MM-dd");

    }

    public static string StringFormat(string inputStr)
    {
        inputStr = inputStr.Trim().Replace("'", "''");

        return inputStr;
    }


    public static string gwtGridHtml(GridView grid)
    {
        System.IO.StringWriter oStringWriter = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter oHTMLTextWriter = new System.Web.UI.HtmlTextWriter(oStringWriter);

        grid.RenderControl(oHTMLTextWriter);

        string strHtml = oStringWriter.ToString();

        return strHtml;
    }

    public static string ClearInput(string strInput)
    {
        strInput = strInput.Trim();
        //strInput = strInput.Replace("'","''");


        return strInput;
    }

    public static string ClearTextInput(string strInput)
    {
        strInput = strInput.Trim();
        strInput = strInput.Replace("'", "''");


        return strInput;
    }

    public static string getImageSuffix()
    {
        string MM = null;
        string DD = null;
        string HH = null;
        string mmm = null;
        string SS = null;


        if (System.DateTime.Now.Month.ToString().Length == 1)
        {
            MM = "0" + System.DateTime.Now.Month.ToString();
        }
        else
        {
            MM = System.DateTime.Now.Month.ToString();
        }

        if (System.DateTime.Now.Day.ToString().Length == 1)
        {
            DD = "0" + System.DateTime.Now.Day.ToString();
        }
        else
        {
            DD = System.DateTime.Now.Day.ToString();
        }

        if (System.DateTime.Now.Hour.ToString().Length == 1)
        {
            HH = "0" + System.DateTime.Now.Hour.ToString();
        }
        else
        {
            HH = System.DateTime.Now.Hour.ToString();
        }

        if (System.DateTime.Now.Minute.ToString().Length == 1)
        {
            mmm = "0" + System.DateTime.Now.Minute.ToString();
        }
        else
        {
            mmm = System.DateTime.Now.Minute.ToString();
        }

        if (System.DateTime.Now.Second.ToString().Length == 1)
        {
            SS = "0" + System.DateTime.Now.Second.ToString();
        }
        else
        {
            SS = System.DateTime.Now.Second.ToString();
        }

        return MM + DD + HH + mmm + SS;

    }


    public static bool IsDate(string strValue)
    {

        DateTime tmp;                        // Not sure why you'd want to ignore this but whatever...
        return DateTime.TryParse(strValue, out tmp);

    }


    public static void setGridHeaderRow(GridView oGridView, GridViewRowEventArgs e)
    {

        GridViewRow oGridViewRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
        oGridViewRow.Height = 15;
        oGridViewRow.Font.Bold = true;

        oGridViewRow.CssClass = "gridFirstHeaderRow";
        int countCount = e.Row.Cells.Count;
        int count = 0;
        int colcount = 0;

        string IsDepth = "0";

        TableCell oTableCell = new TableCell();//Backwall Unit_Region_1
        oTableCell.Attributes.Add("nowrap", "nowrap");
        
        string[] arrelement =e.Row.Cells[0].Text.Split('_');

        if (arrelement.Length > 2)
        {

            oTableCell.Text = arrelement[0];
            e.Row.Cells[0].Text = arrelement[1];
            IsDepth = arrelement[2];
           
        }

        oGridViewRow.Cells.Add(oTableCell);


        countCount = countCount - 1;
        colcount = count = 1;

        TableCell oTableSecondCell = new TableCell();
        oTableSecondCell.Attributes.Add("nowrap", "nowrap");
       
        oTableSecondCell.Style.Add("text-align", "left");
        oTableSecondCell.ColumnSpan = countCount;

        if (IsDepth == "1")
        {
            oTableSecondCell.Text = "Height x Width x Depth";
        }
        else
        {
            oTableSecondCell.Text = "Height x Width";
        }

        oGridViewRow.Cells.Add(oTableSecondCell);
        oGridView.Controls[0].Controls.AddAt(0, oGridViewRow);

    }


}