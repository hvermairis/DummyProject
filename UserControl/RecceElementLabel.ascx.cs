using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.HtmlControls;

public partial class UserControl_RecceElementLabel : System.Web.UI.UserControl
{
    int NoOfRows = 0;

    public string strElementName { get; set; }
    public string strElementID { get; set; }

    public string strElementNo { get; set; }




    public string strIsAvalability = "-1";
    public string strQuantity = "0";
    public string strIsDepth = "";
    
    public string strHeight = "";
    public string strWidth = "";
    public string strDepth = "";
    public string strSurface = "";
    public string strRemarks = "";


    private string _SelectedValue = "";

    public string SelectedValue
    {

        set
        {
            _SelectedValue = value;

        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CreateControl();
        }
    }

    private void CreateControl()
    {
        tbControl.Controls.Clear();

        HtmlTableRow rowHeaing = new HtmlTableRow();
        HtmlTableCell cellHeadingElement = new HtmlTableCell();
        cellHeadingElement.Width = "25%";

        HtmlTableCell cellHeadingAvailable = new HtmlTableCell();

        cellHeadingAvailable.Width = "15%";
        
        HtmlTableCell cellHeadingHeight = new HtmlTableCell();
        cellHeadingHeight.Width = "15%";

        HtmlTableCell cellHeadingWidth = new HtmlTableCell();
        cellHeadingWidth.Width = "15%";

        HtmlTableCell cellHeadingDepth = new HtmlTableCell();
        cellHeadingDepth.Width = "15%";

        HtmlTableCell cellHeadingsurface = new HtmlTableCell();
        cellHeadingsurface.Width = "15%";

        HtmlTableCell cellHeadingReview = new HtmlTableCell();
        cellHeadingReview.Width = "15%";

        if (strIsAvalability == "0")
        {
            cellHeadingElement.InnerHtml = strElementName;
            cellHeadingAvailable.InnerHtml = "No";

            cellHeadingHeight.InnerHtml = "&nbsp;";
            cellHeadingWidth.InnerHtml = "&nbsp;";
            cellHeadingDepth.InnerHtml = "&nbsp;";
            cellHeadingsurface.InnerHtml = "&nbsp;";
            cellHeadingReview.InnerHtml = "&nbsp;";
         

        }
        else if (strIsAvalability=="1")
        {
               
               
               

                cellHeadingElement.InnerHtml = strElementName;
                cellHeadingAvailable.InnerHtml = "Yes";
                cellHeadingHeight.InnerHtml = strHeight;
                cellHeadingWidth.InnerHtml = strWidth;

                if (strSurface != "")
                {
                    cellHeadingsurface.InnerHtml = strSurface;
                }
                else
                {
                    cellHeadingsurface.InnerHtml = "-NA-";
                }
                
               cellHeadingReview.InnerHtml = strRemarks;

               if (strIsDepth == "1")
                {
                    cellHeadingDepth.InnerHtml = strDepth;
                    
                }
                else
                {
                    cellHeadingDepth.InnerHtml = "-NA-";
                }
              

        }


        rowHeaing.Controls.Add(cellHeadingElement);
        rowHeaing.Controls.Add(cellHeadingAvailable);
        rowHeaing.Controls.Add(cellHeadingHeight);
        rowHeaing.Controls.Add(cellHeadingWidth);
        rowHeaing.Controls.Add(cellHeadingDepth);
        rowHeaing.Controls.Add(cellHeadingsurface);
        rowHeaing.Controls.Add(cellHeadingReview);

        tbControl.Controls.Add(rowHeaing);
    }




}

 

