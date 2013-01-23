using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;

public partial class UserControl_RecceElementControl : System.Web.UI.UserControl
{
    int NoOfRows = 0;

    public string ControlName { get; set; }
    public string ElementID { get; set; }

    public bool Enabled { get; set; }
    public bool IsDepth { get; set; }
    public bool IsRepeat { get; set; }

    public string Width { get; set; }
    public string Height { get; set; }
    public string Depth { get; set; }

    private string strtxtWidths = "";
    private string strtxtHeigths = "";

    public string strRemarks = "";

    

    string strRemarksSparator = "®";
    string strRemarksIDSparator = "ª";

    string InnerCellWidth = "InnerCellWidth";
    
    private int _Quantity = 0;

    public int Quantity
    {
        get
        {
            _Quantity = NoOfRows;
            return _Quantity;
        }
        set
        {
            _Quantity = value;
            txtQuantity.Text = Convert.ToString(_Quantity);
          //if (IsAvailability=="1")
          //   generateControlWhenQuantitychange();
        }
    }


    private string _IsAvailability = "-1";

    public string IsAvailability
    {
        get
        {
            _IsAvailability = drpAvaibility.SelectedValue;
            return _IsAvailability;
        }
        set
        {
            _IsAvailability = value;
            drpAvaibility.SelectedValue = _IsAvailability;

            if (drpAvaibility.SelectedValue == "1")
            {
                divRemarks.Style.Add("display", "");
            }
            else
            {
                divRemarks.Style.Add("display", "none");
            }
        }
    }


    private string _SelectedValue = "";

    public string SelectedValue
    {
        get
        {
            _SelectedValue=getValidate();
            return _SelectedValue;
        }
        set
        {
            _SelectedValue = value;
           
        }
    }


    private string _ElementRemarks = "";

    public string ElementRemarks
    {
        get
        {
            _ElementRemarks=general.ClearInput(txtElementRemarks.Text);
            _ElementRemarks = _ElementRemarks.Replace(strRemarksSparator, "");
            _ElementRemarks = _ElementRemarks.Replace(strRemarksIDSparator, "");
            _ElementRemarks = _ElementRemarks.Trim();

            return _ElementRemarks;
        }
        set
        {
            _ElementRemarks = value;
            if (_ElementRemarks != "")
            {
                divRemarks.Style.Add("display", "");
                txtElementRemarks.Text = _ElementRemarks;
            }
            else
            {
                divRemarks.Style.Add("display", "none");
            }
        }
    }

   


    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {


            drpAvaibility.Attributes.Add("OnChange", "setQuantityText(this,'" + txtQuantity.ClientID + "','" + lblQuantity.ClientID + "');");

            txtQuantity.Attributes.Add("onKeyPress", "javascript:return checkNum(event,this)");

            txtQuantity.Enabled = Enabled;
            drpAvaibility.Enabled = Enabled;
            txtElementRemarks.Enabled = Enabled;

            setCss();
            if (txtQuantity.Text.Trim() != "" && txtQuantity.Text.Trim() != "0")
            {
                string strNoOFRows = txtQuantity.Text.Trim();
                if (strNoOFRows.Contains(','))
                {
                    strNoOFRows = strNoOFRows.Substring(0, strNoOFRows.Length - 1);
                }

                if (strNoOFRows != "")
                {
                    NoOfRows = Convert.ToInt32(strNoOFRows);
                }
                CreateControl();

                if (_SelectedValue != "" && IsAvailability == "1")
                {
                    if (txtQuantity.Text.Trim() != "")
                    {
                        NoOfRows = Convert.ToInt32(txtQuantity.Text);
                    }

                    try
                    {
                        setValues();
                    }
                    catch (Exception ex)
                    {

                    }

                }

            }





            lblElementName.Text = ControlName;

            if (txtQuantity.Text.Trim() == ",")
            {
                txtQuantity.Text = "";
            }

            if (drpAvaibility.SelectedIndex == 1)
            {
                txtQuantity.Style.Add("display", "");
                lblQuantity.Style.Add("display", "");
            }
            else
            {
                txtQuantity.Style.Add("display", "none");
                lblQuantity.Style.Add("display", "none");
            }

            setJSScript();

        }
        catch(Exception ex)
        {

        }
       
    }


    private void setCss()
    {
        InnerCellWidth = "InnerCellWidth";
        
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (ElementID == "12")
        {
            setOtherelementVisibility();
        }
    }


    private void setOtherelementVisibility()
    {
        for (int count = 1; count <= NoOfRows; count++)
        {


            DropDownList drpSurface = (DropDownList)tbControl.FindControl("drpSurface" + count.ToString());
            
            HtmlTableCell cellHeadingSurFaceText = (HtmlTableCell)tbControl.FindControl("cellHeadingSurFaceText" + count.ToString());
            
            if (drpSurface != null && cellHeadingSurFaceText != null)
            {
                if (drpSurface.SelectedValue == "8")
                {
                    cellHeadingSurFaceText.Style.Add("display", "");
                }
                else
                {
                    cellHeadingSurFaceText.Style.Add("display", "none");
                }
            }


        }

    }

    private void setHeightWidthId()
    {
        strtxtWidths = "";
        strtxtHeigths = "";

        string strdrpSurface = "";
        string strtxtSurFaceText = "";

        string strSpanHeight = "";
        string strSpanWidth = "";
        string strSpanDrpSurface = "";
        string strSpanTxtSurFaceText = "";

        string strTxtDepth = "";
        string strSpanDepth = "";


        for (int count = 1; count <= NoOfRows; count++)
        {
            TextBox txtWidth = (TextBox)FindControl("txtWidth" + count.ToString());
            TextBox txtHeight = (TextBox)FindControl("txtHeight" + count.ToString());

            DropDownList drpSurface = (DropDownList)FindControl("drpSurface" + count.ToString());
            TextBox txtSurFaceText = (TextBox)FindControl("txtSurFaceText" + count.ToString());

            HtmlGenericControl spanHeight = (HtmlGenericControl)FindControl("spanHeight" + count.ToString());
            HtmlGenericControl spanWidth = (HtmlGenericControl)FindControl("spanWidth" + count.ToString());
            HtmlGenericControl spanDrpSurface = (HtmlGenericControl)FindControl("spanDrpSurface" + count.ToString());
            HtmlGenericControl spanTxtSurFaceText = (HtmlGenericControl)FindControl("spanTxtSurFaceText" + count.ToString());


            TextBox txtDepth = (TextBox)FindControl("txtDepth" + count.ToString());
            HtmlGenericControl spanDepth = (HtmlGenericControl)FindControl("spanDepth" + count.ToString());

            if (txtWidth != null && txtHeight != null)
            {
                strtxtHeigths = strtxtHeigths + txtHeight.ClientID + ",";
                strtxtWidths = strtxtWidths + txtWidth.ClientID + ",";
                
            }

           

            if (spanHeight != null)
            {
                strSpanHeight = strSpanHeight + spanHeight.ClientID + ",";
            }

            if (spanWidth != null)
            {
                strSpanWidth = strSpanWidth + spanWidth.ClientID + ",";
            }

            if (txtDepth != null)
            {
                strTxtDepth = strTxtDepth + txtDepth.ClientID + ",";
            }

            if (spanDepth != null)
            {
                strSpanDepth = strSpanDepth + spanDepth.ClientID + ",";
            }

            if (spanDrpSurface != null)
            {
                strSpanDrpSurface = strSpanDrpSurface + spanDrpSurface.ClientID + ",";
            }

            if (spanTxtSurFaceText != null)
            {
                strSpanTxtSurFaceText = strSpanTxtSurFaceText + spanTxtSurFaceText.ClientID + ",";
            }

            if (drpSurface != null)
            {
                strdrpSurface = strdrpSurface + drpSurface.ClientID + ",";
            }

            if (txtSurFaceText != null)
            {
                strtxtSurFaceText = strtxtSurFaceText + txtSurFaceText.ClientID + ",";
            }
        }

        if (strtxtHeigths != "")
        {
            strtxtHeigths = strtxtHeigths.Substring(0, strtxtHeigths.Length-1);
            strtxtWidths = strtxtWidths.Substring(0, strtxtWidths.Length - 1);
        }

        if (strdrpSurface != "")
        {
            strdrpSurface = strdrpSurface.Substring(0, strdrpSurface.Length - 1);
        }

        if (strSpanHeight != "")
        {
            strSpanHeight = strSpanHeight.Substring(0, strSpanHeight.Length - 1);
        }

        if (strSpanWidth != "")
        {
            strSpanWidth = strSpanWidth.Substring(0, strSpanWidth.Length - 1);
        }

        if (strSpanDrpSurface != "")
        {
            strSpanDrpSurface = strSpanDrpSurface.Substring(0, strSpanDrpSurface.Length - 1);
        }

        if (strSpanTxtSurFaceText != "")
        {
            strSpanTxtSurFaceText = strSpanTxtSurFaceText.Substring(0, strSpanTxtSurFaceText.Length - 1);
        }


        if (strtxtSurFaceText != "")
        {
            strtxtSurFaceText = strtxtSurFaceText.Substring(0, strtxtSurFaceText.Length - 1);
        }

        if (strtxtHeigths != "")
        {
            Random rand = new Random((int)DateTime.Now.Ticks);
            int numIterations = 0;
            numIterations = rand.Next(1, 10000);


            hdntxtHeigths.Value = strtxtHeigths;
            hdntxtWidths.Value = strtxtWidths;
            hdnstrdrpSurface.Value = strdrpSurface;
            hdnstrtxtSurFaceText.Value = strtxtSurFaceText;
            hdnstrSpanHeight.Value = strSpanHeight;

            hdnstrSpanWidth.Value = strSpanWidth;
            hdnstrSpanDrpSurface.Value = strSpanDrpSurface;
            hdnstrSpanTxtSurFaceText.Value = strSpanTxtSurFaceText;
            hdnstrTxtDepth.Value = strTxtDepth;
            hdnstrSpanDepth.Value = strSpanDepth;

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "setHeightWidthId" + numIterations.ToString(), "setHeightWidthId" + this.ID + @"();", true);
        }

    }

    private void setJSScript()
    {
        if (ElementID == "13")
        {

        }
        setHeightWidthId();
        string strJs = @"<script language='javascript'  type='text/javascript'>
        var txtWidth" + this.ID + @"='';
        var spanWidth" + this.ID + @"='';
        
        var txtHeigth" + this.ID + @"='';
        var spanHeight" + this.ID + @"='';

         var txtDepth" + this.ID + @"='';
         var spanDepth" + this.ID + @"='';";

        if (ElementID == "12")
        {
            strJs = strJs + @"  var drpSurface" + this.ID + @"='';
                                var spanDrpSurface" + this.ID + @"='';
                                var txtSurFaceText" + this.ID + @"='';
                                var spanTxtSurFaceText" + this.ID + @"='';";
            
         
         }

        strJs = strJs + @"  function setHeightWidthId" + this.ID + @"()
        {
           
          var strtxtHeigths=document.getElementById('" + hdntxtHeigths .ClientID+ @"').value;
           var strtxtWidths=document.getElementById('" + hdntxtWidths.ClientID + @"').value;
           var strdrpSurface=document.getElementById('" + hdnstrdrpSurface.ClientID + @"').value;
          var  strtxtSurFaceText=document.getElementById('" + hdnstrtxtSurFaceText.ClientID + @"').value;
          var  strSpanHeight=document.getElementById('" + hdnstrSpanHeight.ClientID + @"').value;
          var  strSpanWidth=document.getElementById('" + hdnstrSpanWidth.ClientID + @"').value;
          var  strSpanDrpSurface=document.getElementById('" + hdnstrSpanDrpSurface.ClientID + @"').value;
           var strSpanTxtSurFaceText=document.getElementById('" + hdnstrSpanTxtSurFaceText.ClientID + @"').value;
          var  strtxtDepth=document.getElementById('" + hdnstrTxtDepth.ClientID + @"').value;
          var  strSpanDepth=document.getElementById('" + hdnstrSpanDepth.ClientID + @"').value;

            // alert(strtxtHeigths);

            txtHeigth" + this.ID + @"=strtxtHeigths;
            txtWidth" + this.ID + @"=strtxtWidths;

           
            spanWidth" + this.ID + @"=strSpanWidth;
            spanHeight" + this.ID + @"=strSpanHeight;





";

        if (IsDepth)
        {
            strJs = strJs + @"   txtDepth" + this.ID + @"=strtxtDepth; ";
            strJs = strJs + @"   spanDepth" + this.ID + @"=strSpanDepth;";
        }

            if (ElementID == "12")
            {

                strJs = strJs + @" 
                drpSurface" + this.ID + @"=strdrpSurface;
                txtSurFaceText" + this.ID + @"=strtxtSurFaceText;
                var spanDrpSurface" + this.ID + @"=strSpanDrpSurface;
                var spanTxtSurFaceText" + this.ID + @"=strSpanTxtSurFaceText;
                ";

        
            }

        strJs = strJs + @"  }

function onchnageValue" + this.ID + @"(isflag){";
        strJs = strJs + @" var txtHeightInLoop='txtHeight';";
        strJs = strJs + @" var txtWidthInLoop='txtWidth';
                           var txtSourceObj=null;
                           var txtHeightArr=txtHeigth" + this.ID + @".split(',');
                           var txtWidthArr=txtWidth" + this.ID + @".split(',');";
        if (IsDepth)
        {
            strJs = strJs + @"           var txtDepthArr=txtDepth" + this.ID + @".split(',');";
          
        }

        strJs = strJs + @"    var textBoxValue='';

                                      for (var count=0; count<txtHeightArr.length;count++)
                                            {
                                                //alert('Element'+count.toString()+'='+txtHeightArr[count]);
                                              //  alert('ObjElement'+count.toString()+'='+document.getElementById(txtHeightArr[count]));
                                                if(count==0)
                                                {
                                                    
                                                    if(isflag==1)
                                                    {
                                                         if(txtHeightArr[count]!='' && document.getElementById(txtHeightArr[count])!=null)
                                                           textBoxValue=document.getElementById(txtHeightArr[count]).value;
                                                    }
                                                    else  if(isflag==2)
                                                    {
                                                        if(txtWidthArr[count]!='' && document.getElementById(txtWidthArr[count])!=null)
                                                          textBoxValue=document.getElementById(txtWidthArr[count]).value;
                                                    } 
                                                    else
                                                    {
                                                        if(txtDepthArr[count]!='' && document.getElementById(txtDepthArr[count])!=null)
                                                          textBoxValue=document.getElementById(txtDepthArr[count]).value;

                                                    } 

                                                }
                                                else
                                                {

                                                    if(isflag==1)
                                                    {
                                                        if(txtHeightArr[count]!='' && document.getElementById(txtHeightArr[count])!=null)
                                                         txtSourceObj=document.getElementById(txtHeightArr[count]);
                                                    }
                                                    else  if(isflag==2)
                                                    {
                                                        if(txtWidthArr[count]!='' && document.getElementById(txtWidthArr[count])!=null)
                                                         txtSourceObj=document.getElementById(txtWidthArr[count]);
                                                    } 
                                                    else
                                                    {
                                                        if(txtDepthArr[count]!='' && document.getElementById(txtDepthArr[count])!=null)
                                                          txtSourceObj=document.getElementById(txtDepthArr[count]);

                                                    } 
                                                    
                                                     if(txtSourceObj!=null)
                                                        txtSourceObj.value= textBoxValue;
                                                }
 
                                            }";



        strJs = strJs + @" };


            ";
        strJs = strJs + @" function Validate"+this.ID+@"(){

                        setHeightWidthId" + this.ID + @"();



                        ";





        strJs = strJs + @" var drpAvaibility='" + drpAvaibility .ClientID+ "';";
        strJs = strJs + @" var txtQuantity='" + txtQuantity.ClientID + "';";
        strJs = strJs + @" var spanDrpAvaibility='" + spandrpAvaibility .ClientID+ "';";
        strJs = strJs + @" var spanTxtQuantity='" + spanTxtQuantity.ClientID + "';";

        strJs = strJs + @" var txtHeight='txtHeight';";
        strJs = strJs + @" var txtWidth='txtWidth';";
         strJs = strJs + @" var NoOfControl=0;";

        strJs = strJs + @" var txtHeightInLoop='txtHeight';";
        strJs = strJs + @" var txtWidthInLoop='txtWidth';";


        strJs = strJs + @" var spanHeightInLoop='txtHeight';";
        strJs = strJs + @" var spanWidthInLoop='txtWidth';";

        strJs = strJs + @" var drpAvaibilityObj=document.getElementById(drpAvaibility);";
        strJs = strJs + @" var txtQuantityObj=document.getElementById(txtQuantity);

         var spanDrpAvaibilityObj=document.getElementById(spanDrpAvaibility);
         var spanTxtQuantityObj=document.getElementById(spanTxtQuantity);

              
                           var txtHeightArr=txtHeigth" + this.ID + @".split(',');
                           var txtWidthArr=txtWidth" + this.ID + @".split(',');

                       
                      
                ";
        if (ElementID == "13")
        {
           // strJs = strJs + @"  alert('txtHeigthID='+txtHeigth" + this.ID + ");";


        }
        if (IsDepth)
        {
            strJs = strJs + @"           var txtDepthArr=txtDepth" + this.ID + @".split(',');";
            strJs = strJs + @"           var spanDepthArr=spanDepth" + this.ID + @".split(',');";

            strJs = strJs + @" var txtDepthInLoop=null;";
            strJs = strJs + @" var spanDepthInLoop=null;";

        }
        
        strJs = strJs + @" var ArrSpanHeight=spanHeight" + this.ID + @".split(',');";
        strJs = strJs + @" var ArrSpanWidthInLoop=spanWidth" + this.ID + @".split(',');";

        if (ElementID == "12")
        {

          strJs = strJs + @" 
                             var drpSurfaceArr=drpSurface" + this.ID + @".split(',');
                             var txtSurFaceTextArr=txtSurFaceText" + this.ID + @".split(',');

                             var spanSurfaceArr=spanDrpSurface" + this.ID + @".split(',');    
                             var spanSurFaceTextArr=spanTxtSurFaceText" + this.ID + @".split(','); ";




          strJs = strJs + @" var drpSurfaceInLoop=null;";
                strJs = strJs + @" var txtSurFaceTextInLoop=null;";

                strJs = strJs + @" var spanSurfaceInLoop=null;";
                strJs = strJs + @" var spanSurFaceTextInLoop=null;";

        }


        strJs = strJs + @"    var strerrorMsg='';
                        
                       
                        if(drpAvaibilityObj.value!='-1')
                        {
                             
                             if(drpAvaibilityObj.value=='1')
                                {
                                   if(txtQuantityObj.value!='')
                                    {    
 
                                        NoOfControl=parseInt(txtQuantityObj.value);
                                   
                                     
                                            for (var count=0; count<txtHeightArr.length;count++)
                                            {
                                                txtHeightInLoop=document.getElementById(txtHeightArr[count]);
                                                txtWidthInLoop=document.getElementById(txtWidthArr[count]);
  
                                                spanHeightInLoop=document.getElementById(ArrSpanHeight[count]);
                                                spanWidthInLoop=document.getElementById(ArrSpanWidthInLoop[count]);";


                                           

                                                if (ElementID == "12")
                                                {

                                                    strJs = strJs + @"     drpSurfaceInLoop=document.getElementById(drpSurfaceArr[count]);
                                                      txtSurFaceTextInLoop=document.getElementById(txtSurFaceTextArr[count]);";

                                                     strJs = strJs + @"     spanSurfaceInLoop=document.getElementById(spanSurfaceArr[count]);
                                                      spanSurFaceTextInLoop=document.getElementById(spanSurFaceTextArr[count]);";

                                                }

                                                if (IsDepth)
                                                {

                                                    strJs = strJs + @" var txtDepthInLoop=document.getElementById(txtDepthArr[count]);";
                                                    strJs = strJs + @" var spanDepthInLoop=document.getElementById(spanDepthArr[count]);";

                                                }

                                                if (ElementID == "13")
                                                {
                                                    strJs = strJs + @"  
                                                           // alert('txtHeightInLoop='+txtHeightInLoop);
                                                           // alert('txtWidthInLoop='+txtWidthInLoop);

                                                           // alert('txtHeightInLoopValue='+txtHeightInLoop.value);
                                                           // alert('txtWidthInLoopValue='+txtWidthInLoop.value);";
                                                }
                                                strJs = strJs + @"                   
                                                if(txtHeightInLoop!=null && (txtHeightInLoop.value=='' || txtHeightInLoop.value=='0' || isNaN(txtHeightInLoop.value)))
                                                 {
                                                    //strerrorMsg='Please enter " + ControlName + @" '+(count+1).toString()+' Height';
                                                    spanHeightInLoop.style.display='';
                                                    
                                                    if(bit_focus==false)
                                                     {
                                                        txtHeightInLoop.focus();
                                                        bit_focus=true;
                                                     }  
                                                 }
                                                else 
                                                 {  
                                                    if(spanHeightInLoop!=null)
                                                    {
                                                        spanHeightInLoop.style.display='none';
                                                    }
                                                 }

                                                if(txtWidthInLoop!=null && (strerrorMsg=='' && (txtWidthInLoop.value=='' || txtWidthInLoop.value=='0'  || isNaN(txtWidthInLoop.value))))
                                                 {
                                                    //strerrorMsg='Please enter " + ControlName + @" '+(count+1).toString()+' Width';
                                                    
                                                    spanWidthInLoop.style.display='';
                                                    
                                                  
                                                     if(bit_focus==false)
                                                     {
                                                        txtWidthInLoop.focus();
                                                        bit_focus=true;
                                                     }  
                                                 } 
                                                else 
                                                 {  
                                                    if(spanWidthInLoop!=null)
                                                    {
                                                        spanWidthInLoop.style.display='none';
                                                    }
                                                 }
                                               

";

        if (IsDepth)
        {


            strJs = strJs + @"                   if(txtDepthInLoop!=null && (txtDepthInLoop.value=='' || txtDepthInLoop.value=='0'  || isNaN(txtWidthInLoop.value)))
                                                 {
                                                    //strerrorMsg='Please enter " + ControlName + @" '+(count+1).toString()+' Height';
                                                    spanDepthInLoop.style.display='';
                                                    
                                                    if(bit_focus==false)
                                                     {
                                                        txtDepthInLoop.focus();
                                                        bit_focus=true;
                                                     }  
                                                 }
                                                else 
                                                 {  
                                                    if(spanDepthInLoop!=null)
                                                    {
                                                        spanDepthInLoop.style.display='none';
                                                    }
                                                 }
                                              ";

        }
        if (ElementID == "12")
        {
            strJs = strJs + @"
                                                  if(drpSurfaceInLoop!=null && drpSurfaceInLoop.value=='0' && spanSurfaceInLoop!=null)
                                                    {
                                                       //   strerrorMsg='Please select " + ControlName + @" '+(count+1).toString()+' Surface';
        
                                                         spanSurfaceInLoop.style.display='';
                                                    
                                                        
                                                        if(bit_focus==false)
                                                         {
                                                            drpSurfaceInLoop.focus();
                                                            bit_focus=true;
                                                         }  
                                                    }
                                                    else 
                                                    {  
                                                        if(spanSurfaceInLoop!=null)
                                                        {
                                                            spanSurfaceInLoop.style.display='none';
                                                        }
                                                    }
                                                 

                                                    if(drpSurfaceInLoop!=null && drpSurfaceInLoop.value=='8' && trim(txtSurFaceTextInLoop.value)=='')
                                                    {
                                                       //strerrorMsg='Please enter " + ControlName + @" '+(count+1).toString()+' Others Surface ';
                                                      
                                                       spanSurFaceTextInLoop.style.display='';
                                                    
                                                        if(bit_focus==false)
                                                         {
                                                            txtSurFaceTextInLoop.focus();
                                                            bit_focus=true;
                                                         }  
                                                       
                                                    }
                                                    else 
                                                    {  
                                                        if(spanSurFaceTextInLoop!=null)
                                                        {
                                                            spanSurFaceTextInLoop.style.display='none';
                                                        }
                                                    }
                                                   
                                        
                          ";

        }

        strJs = strJs + @"         //if(strerrorMsg!='')
                                        // {
                                        //   break;
                                        // }
                                        }

                                                if(spanTxtQuantityObj!=null)
                                                    {
                                                        spanTxtQuantityObj.style.display='none';
                                                    }
                                    
                                    }
                                    else 
                                    {
                                        if(spanTxtQuantityObj!=null)
                                        {
                                                spanTxtQuantityObj.style.display='';
                                                    //strerrorMsg='Please enter " + ControlName + @" Quantity';
                                            
                                            
                                                if(bit_focus==false)
                                                {
                                            
                                                    txtQuantityObj.focus();
                                                    bit_focus=true;
                                                }  

                                           
                                        }
                                    }

                                }

                               if(spanDrpAvaibilityObj!=null)
                                {
                                    spanDrpAvaibilityObj.style.display='none';
                                }
                           
                        }
                        else   if(spanDrpAvaibilityObj!=null)
                        {
                           spanDrpAvaibilityObj.style.display='';
                          // strerrorMsg='Please " + ControlName+ @" availability';
                          
                            if(bit_focus==false)
                            {

                                drpAvaibilityObj.focus();
                                bit_focus=true;
                            }  
                          
                        }
                

                


                    return bit_focus;
              ";

        strJs = strJs + "}";

        strJs = strJs + "</script>";

        lblScript.Text = strJs;
    }

    private void CreateHeadingRow()
    {
        HtmlTableRow rowHeaing = new HtmlTableRow();

        HtmlTableCell cellHeadingElement = new HtmlTableCell();
        cellHeadingElement.InnerText = "";
        cellHeadingElement.Attributes.Add("class", InnerCellWidth);
        rowHeaing.Controls.Add(cellHeadingElement);

        HtmlTableCell cellHeadingHeight = new HtmlTableCell();
        cellHeadingHeight.InnerText = "Height(MM)";
        cellHeadingHeight.Attributes.Add("class", "leftCellCss");
        rowHeaing.Controls.Add(cellHeadingHeight);


        HtmlTableCell cellHeadingWidth = new HtmlTableCell();
        cellHeadingWidth.InnerText = "Width(MM)";
        cellHeadingWidth.Attributes.Add("class", "leftCellCss");
        rowHeaing.Controls.Add(cellHeadingWidth);


        if (IsDepth)
        {
            HtmlTableCell cellHeadingDepth = new HtmlTableCell();
            cellHeadingDepth.InnerText = "Depth(MM)";
            cellHeadingDepth.Attributes.Add("class", "leftCellCss");
            rowHeaing.Controls.Add(cellHeadingDepth);

        }

        if (ElementID == "12")
        {
            HtmlTableCell cellHeadingSurface = new HtmlTableCell();
            cellHeadingSurface.InnerText = "Surface";
            cellHeadingSurface.Attributes.Add("class", "leftCellCss");
            rowHeaing.Controls.Add(cellHeadingSurface);

            HtmlTableCell cellHeadingOthersSurface = new HtmlTableCell();
            cellHeadingOthersSurface.InnerText = "";
            rowHeaing.Controls.Add(cellHeadingOthersSurface);

        }

        tbControl.Controls.Add(rowHeaing);


    }

    private void CreateControl()
    {
        setCss();
        if (NoOfRows>0)
          CreateHeadingRow();

        for (int count = 1; count <= NoOfRows; count++)
        {

            HtmlTableRow rowHeaing = new HtmlTableRow();

            HtmlTableCell cellHeadingElement = new HtmlTableCell();
           cellHeadingElement.Attributes.Add("class", "leftInnerCellWidthoutBoldCss");
            cellHeadingElement.InnerText = ControlName+" "+count.ToString();
            cellHeadingElement.Attributes.Add("class", InnerCellWidth);
       
            rowHeaing.Controls.Add(cellHeadingElement);

            HtmlTableCell cellHeadingHeight = new HtmlTableCell();
            cellHeadingHeight.Style.Add("width", "70px");

            TextBox txtHeight = new TextBox();
            txtHeight.ID = "txtHeight" + count.ToString();

            if (Height != "" && Convert.ToDecimal(Height)>1)
              txtHeight.Text=Height;
            
            txtHeight.MaxLength = 12;
            txtHeight.Attributes.Add("onKeyPress", "javascript:return checkDecimalNumber(event,this)");
            txtHeight.Enabled = Enabled;
            
            if (count==1 && IsRepeat)
                txtHeight.Attributes.Add("OnChange", "onchnageValue" + this.ID + "(1);");

            txtHeight.CssClass = "small-textbox";

            HtmlGenericControl spanHeight = new HtmlGenericControl("span");
            spanHeight.Style.Add("display","none");
            spanHeight.Attributes.Add("class", "mandentoryfieldColor");
            spanHeight.InnerHtml = "*";
            spanHeight.ID="spanHeight" + count.ToString();

            cellHeadingHeight.Controls.Add(txtHeight);
            cellHeadingHeight.Controls.Add(spanHeight);
            rowHeaing.Controls.Add(cellHeadingHeight);

            HtmlTableCell cellHeadingWidth = new HtmlTableCell();
            cellHeadingWidth.Style.Add("width", "70px");
            TextBox txtWidth = new TextBox();
            txtWidth.ID = "txtWidth" + count.ToString();

            if (Width != "" && Convert.ToDecimal(Width) > 1)
              txtWidth.Text = Width;
            
            txtWidth.MaxLength = 12;
            txtWidth.Attributes.Add("onKeyPress", "javascript:return checkDecimalNumber(event,this)");
            txtWidth.Enabled = Enabled;

            if (count == 1 && IsRepeat)
                txtWidth.Attributes.Add("OnChange", "onchnageValue" + this.ID + "(2);");
            
            txtWidth.CssClass = "small-textbox";
           
            HtmlGenericControl spanWidth = new HtmlGenericControl("span");
            spanWidth.Style.Add("display", "none");
        
            spanWidth.Attributes.Add("class", "mandentoryfieldColor");
            spanWidth.InnerHtml = "*";
            spanWidth.ID = "spanWidth" + count.ToString();

            cellHeadingWidth.Controls.Add(txtWidth);
            cellHeadingWidth.Controls.Add(spanWidth);
            rowHeaing.Controls.Add(cellHeadingWidth);


            if (IsDepth)
            {
                HtmlTableCell cellHeadingDepth = new HtmlTableCell();
                cellHeadingDepth.Style.Add("width", "70px");
                TextBox txtDepth = new TextBox();
                txtDepth.ID = "txtDepth" + count.ToString();

                if (Depth != "" && Convert.ToDecimal(Depth) > 1)
                  txtDepth.Text = Depth;

                txtDepth.MaxLength = 12;
                txtDepth.Attributes.Add("onKeyPress", "javascript:return checkDecimalNumber(event,this)");
                txtDepth.Enabled = Enabled;

                if (count == 1 && IsRepeat)
                    txtDepth.Attributes.Add("OnChange", "onchnageValue"+this.ID+"(3);");

                txtDepth.CssClass = "small-textbox";



                HtmlGenericControl spanDepth = new HtmlGenericControl("span");
                spanDepth.Style.Add("display", "none");

                spanDepth.Attributes.Add("class", "mandentoryfieldColor");
                spanDepth.InnerHtml = "*";
                spanDepth.ID = "spanDepth" + count.ToString();


                cellHeadingDepth.Controls.Add(txtDepth);
                cellHeadingDepth.Controls.Add(spanDepth);

                rowHeaing.Controls.Add(cellHeadingDepth);

            }
        


            DropDownList drpSurface = new DropDownList();
            HtmlTableCell cellHeadingSurFaceText = new HtmlTableCell();

            if (ElementID == "12")
            {
                HtmlTableCell cellHeadingSurFace = new HtmlTableCell();

                
                drpSurface.ID = "drpSurface" + count.ToString();
              
                drpSurface.Enabled = Enabled;
                drpSurface.Width = Unit.Pixel(80);

                drpSurface.DataValueField = "ID";
                drpSurface.DataTextField = "Text";

               DataTable dataTb= getSurface();
               if (dataTb != null)
               {
                   DataRow rowSurface = dataTb.NewRow();

                   rowSurface[0] = "0";
                   rowSurface[1] = "Select";
                   dataTb.Rows.InsertAt(rowSurface, 0);
                   dataTb.AcceptChanges();

                   drpSurface.DataSource = dataTb;
                   drpSurface.DataBind();
               }


                cellHeadingSurFace.Controls.Add(drpSurface);

                HtmlGenericControl spanDrpSurface = new HtmlGenericControl("span");
                spanDrpSurface.Style.Add("display", "none");
                spanDrpSurface.Attributes.Add("class", "mandentoryfieldColor");
                spanDrpSurface.InnerHtml = "*";
                spanDrpSurface.ID = "spanDrpSurface" + count.ToString();

                cellHeadingSurFace.Controls.Add(spanDrpSurface);
                
                rowHeaing.Controls.Add(cellHeadingSurFace);

                cellHeadingSurFaceText.ID = "cellHeadingSurFaceText" + count.ToString();

                cellHeadingSurFaceText.Style.Add("display","none");

                cellHeadingSurFaceText.ID = "cellHeadingSurFaceText" + count.ToString();

                TextBox txtSurFaceText = new TextBox();
                txtSurFaceText.ID = "txtSurFaceText" + count.ToString();
                txtSurFaceText.MaxLength = 200;
                txtSurFaceText.Enabled = Enabled;
                
                txtSurFaceText.CssClass = "small-textbox";

                cellHeadingSurFaceText.Controls.Add(txtSurFaceText);

                HtmlGenericControl spanTxtSurFaceText = new HtmlGenericControl("span");
                spanTxtSurFaceText.Style.Add("display", "none");
                spanTxtSurFaceText.Attributes.Add("class", "mandentoryfieldColor");
                spanTxtSurFaceText.InnerHtml = "*";
                spanTxtSurFaceText.ID = "spanTxtSurFaceText" + count.ToString();

                cellHeadingSurFaceText.Controls.Add(spanTxtSurFaceText);
          
                

                rowHeaing.Controls.Add(cellHeadingSurFaceText);
            }


            tbControl.Controls.Add(rowHeaing);

            drpSurface.Attributes.Add("OnChange", "setOthersVisible(this,'" + cellHeadingSurFaceText.ClientID + "')");

        }
    }


    private DataTable getSurface()
    {
        string strQuery = "select SurfaceId as [ID],Surface as [Text] from SurfaceMaster where IsActive=1 order by SortBy desc";
        DataTable dataTb = null;

        DataSet dataDs = DBLayer.getDataSetFromFinalDB(strQuery);
        if (dataDs != null && dataDs.Tables.Count > 0)
        {
            dataTb = dataDs.Tables[0];

        }

        return dataTb;
    }

    private string getValidate()
    {
        string strValue = "";
        string strSurface = "";

        string strDepath = "";
        for (int count = 1; count <= NoOfRows; count++)
        {
            strDepath = "";
            TextBox txtHeight = (TextBox)tbControl.FindControl("txtHeight" + count.ToString());
            TextBox txtWidth = (TextBox)tbControl.FindControl("txtWidth" + count.ToString());

            TextBox txtDepth = (TextBox)tbControl.FindControl("txtDepth" + count.ToString());

            DropDownList drpSurface = (DropDownList)tbControl.FindControl("drpSurface" + count.ToString());
            TextBox txtSurFaceText = (TextBox)tbControl.FindControl("txtSurFaceText" + count.ToString());
            
            if (ElementID == "12")
            {
                if (drpSurface != null )
                {
                    if (drpSurface.SelectedValue == "8")
                    {
                        strSurface = general.ClearInput(txtSurFaceText.Text);
                    }
                    else
                    {
                        strSurface = drpSurface.SelectedItem.Text;
                    }
                }
            }

            if (IsDepth && txtDepth!=null)
            {
                strDepath = general.ClearInput(txtDepth.Text);
                strDepath = strDepath.Replace("$", "");
                strDepath = strDepath.Replace("@", "");
                strDepath = strDepath.Replace("#", "");
                strDepath = strDepath.Replace("*", "");
            }

            if(txtHeight!=null && txtWidth!=null)
            {
                strValue = strValue + txtHeight.Text + "," + txtWidth.Text + "," + strDepath + "," + strSurface + ",";
            }
        }

        if (strValue != "")
        {
            strValue = strValue.Substring(0, strValue.Length-1);
        }

        return strValue;
    }

    private void setValues()
    {
        string[] arrValue = _SelectedValue.Split(',');

        int arrIndex = 0;
        string strSurface = "";

        for (int count = 1; count <= NoOfRows; count++, arrIndex=arrIndex + 4)
        {
            TextBox txtHeight = (TextBox)tbControl.FindControl("txtHeight" + count.ToString());
            TextBox txtWidth = (TextBox)tbControl.FindControl("txtWidth" + count.ToString());
            TextBox txtDepth = (TextBox)tbControl.FindControl("txtDepth" + count.ToString());

            DropDownList drpSurface = (DropDownList)tbControl.FindControl("drpSurface" + count.ToString());
            TextBox txtSurFaceText = (TextBox)tbControl.FindControl("txtSurFaceText" + count.ToString());

            HtmlTableCell cellHeadingSurFaceText = (HtmlTableCell)tbControl.FindControl("cellHeadingSurFaceText" + count.ToString());
            
            if (txtHeight != null)
            {
                txtHeight.Text = arrValue[arrIndex];
                txtWidth.Text = arrValue[arrIndex + 1];
                

              
               
                if (ElementID == "12")
                 {
                     strSurface = arrValue[arrIndex + 3];
                }
            }

            if (IsDepth && txtDepth != null)
            {
                txtDepth.Text = arrValue[arrIndex + 2];
            }

            if (ElementID == "12")
            {
                if (drpSurface != null)
                {
                    if (strSurface != "")
                    {

                        bool IsDropDownValue = false;
                        string strSelectId="0";


                        DataTable datatb = (DataTable)drpSurface.DataSource;

                        if (datatb != null)
                        {
                            for (int count2 = 1; count2 < datatb.Rows.Count; count2++)
                            {
                                if (datatb.Rows[count2]["Text"].ToString() == strSurface)
                                {
                                    strSelectId = datatb.Rows[count2]["ID"].ToString();
                                    IsDropDownValue = true;
                                    break;
                                }
                              
                            }
                        }

                        if (IsDropDownValue)
                        {
                            drpSurface.SelectedValue = strSelectId;
                        }
                        else
                        {
                            if (cellHeadingSurFaceText != null)
                            {
                                cellHeadingSurFaceText.Style.Add("display", "");
                            }
                            
                            drpSurface.SelectedValue = "8";

                            txtSurFaceText.Text = strSurface;
                        }
                    }
                }
            }
            
            
        }

    }



    protected void txtQuantity_TextChanged(object sender, EventArgs e)
    {
        generateControlWhenQuantitychange();
       
    }

    private void generateControlWhenQuantitychange()
    {

        string pcontrolId = this.ID;
        txtQuantity.Text = txtQuantity.Text.Replace(",", "");
        tbControl.Controls.Clear();
        if (txtQuantity.Text.Trim() != "" && txtQuantity.Text.Trim() != "0")
        {
            try
            {
                string strNoOFRows = txtQuantity.Text.Trim();
                if (strNoOFRows.Contains(','))
                {
                    strNoOFRows = strNoOFRows.Substring(0, strNoOFRows.Length - 1);
                }
                else if (strNoOFRows == "")
                {
                    strNoOFRows = "0";
                }

                if (strNoOFRows != "")
                {
                    NoOfRows = Convert.ToInt32(strNoOFRows);
                }

                CreateControl();

                if (drpAvaibility.SelectedIndex == 1)
                {
                    txtQuantity.Style.Add("display", "");
                    lblQuantity.Style.Add("display", "");
                }
                else
                {
                    txtQuantity.Style.Add("display", "none");
                    lblQuantity.Style.Add("display", "none");
                }



            }
            catch (Exception ex)
            {

            }
        }

        setJSScript();
    }



    protected void drpAvaibility_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (drpAvaibility.SelectedIndex == 1)
        {
            txtQuantity.Style.Add("display","");
            lblQuantity.Style.Add("display", "");
            divRemarks.Style.Add("display", "");
        }
        else
        {
            txtQuantity.Text = "";
            txtElementRemarks.Text = "";
            txtQuantity.Style.Add("display", "none");
            lblQuantity.Style.Add("display", "none");
            divRemarks.Style.Add("display", "none");
            tbControl.Controls.Clear();
        }
    }
}