using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.IO;
using System.Drawing;

public partial class ReceeForm : System.Web.UI.Page
{
    int NoOfRows = 0;
    string strRetailerID = "";
    string strUCRecceElement = "UCRecceElement";
    string strUserId = "1";
    string strUserRoleId = "";
    bool IsReccePerson = false;
    string strReceeId = "0";
    bool isEnable = true;

    string strRemarksSparator = "®";
    string strRemarksIDSparator = "ª";

    int InReceeImgLimit = 30;

    string strOutletTypeForMsg = "";
    string strName = "";
    string strUserName = "";



    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
           

            this.Form.Enctype = "multipart/form-data";

            if (Request.QueryString["ReceeId"] != null)
            {
                strReceeId = clsCryptography.Decrypt(Request.QueryString["ReceeId"].ToString());
                isEnable = false;
            }


            if (HttpContext.Current.Request.Cookies["UserInfo"] != null)
            {
                strUserId = Convert.ToString(HttpContext.Current.Request.Cookies["UserInfo"]["UserId"]);
                strUserRoleId = Convert.ToString(HttpContext.Current.Request.Cookies["UserInfo"]["UserRoleId"]);
                strName = Convert.ToString(HttpContext.Current.Request.Cookies["UserInfo"]["Name"]);
                strUserName = Convert.ToString(HttpContext.Current.Request.Cookies["UserInfo"]["UserName"]);

                if (strUserRoleId == "6" || strUserRoleId == "9")
                {
                    IsReccePerson = true;
                }
            }
            else
            {
                Response.Redirect("Login.aspx");
            }



            if (strUserRoleId == "9")//Racee Person
            {
                drpReceePerson.SelectedValue = strUserId;
                drpReceePerson.Enabled = false;
            }





            txtReceeDate.Text = System.DateTime.Now.ToString("dd/MMM/yyyy");


            if (Request.QueryString["ReceeId"] == null)
            {
                if (!IsReccePerson)
                {
                    Response.Redirect("Login.aspx");
                }

            }

            if (isEnable)
            {

                if (txtOutletId.Text.Trim() != "" && txtOutletId.Text.Trim() != "0")
                {
                    strRetailerID = Convert.ToString(txtOutletId.Text.Trim().Split('-')[0].Trim());
                    DataTable dataTB = GetElementDetails();

                    if (dataTB != null && dataTB.Rows.Count > 0)
                    {
                        NoOfRows = dataTB.Rows.Count;
                        CreateControl(dataTB);

                    }

                }

            }

            if (strReceeId != "0" && strReceeId != "")
            {
                try
                {
                    DataTable dataRecce = getReceeData();
                    
                    if(!IsPostBack)
                     populateQCComments(dataRecce);
                    
                    bool IsEnabled = setUserVisibility();

                    if (!IsPostBack)
                     populateValue(IsEnabled, dataRecce);

                    if (IsPostBack)
                    {
                        if (txtOutletId.Text.Trim() != "" && txtOutletId.Text.Trim() != "0")
                        {
                            strRetailerID = Convert.ToString(txtOutletId.Text.Trim().Split('-')[0].Trim());
                            DataTable dataTB = GetElementDetails();
                            isEnable = true;

                            if (dataTB != null && dataTB.Rows.Count > 0)
                            {
                                NoOfRows = dataTB.Rows.Count;
                                CreateControl(dataTB);

                            }

                        }
                    }



                }
                catch (Exception ex)
                {

                }
            }


            if (drpReceeStatus.SelectedValue == "1")
            {
                divReceePersonRemarks.Style.Add("display", "none");
                tdReceeElement.Style.Add("display", "");
                divSpecialRemarks.Style.Add("display", "");
                cellFileUploder.Style.Add("display", "");

            }
            else if (drpReceeStatus.SelectedValue == "2")
            {
                divReceePersonRemarks.Style.Add("display", "");
                tdReceeElement.Style.Add("display", "none");
                divSpecialRemarks.Style.Add("display", "");
                cellFileUploder.Style.Add("display", "");
            }

            if (strReceeId != "0" && strReceeId != "")
            {
                cellFileUploder.Style.Add("display", "none");
                drpReceeStatus.Enabled = false;
                btnSearch.Enabled = false;
            }


            drpReceeStatus.Attributes.Add("OnChange", "setVisibilityRecee('" + drpReceeStatus.ClientID + "','" + divReceePersonRemarks.ClientID + "','" + tdReceeElement.ClientID + "','" + divSpecialRemarks.ClientID + "','" + cellFileUploder.ClientID + "')");
            txtPSDPointRequired.Attributes.Add("onKeyPress", "javascript:return checkNum(event)");
            txtPSDAvailable.Attributes.Add("onKeyPress", "javascript:return checkNum(event)");
            txtTruckDistance.Attributes.Add("onKeyPress", "javascript:return checkNum(event)");

            txtSurveyorPhoneNo.Attributes.Add("onKeyPress", "javascript:return checkNum(event)");
            txtDealerPhone.Attributes.Add("onKeyPress", "javascript:return checkNum(event)");
            txtSonyAreMngPhone.Attributes.Add("onKeyPress", "javascript:return checkNum(event)");

            drpInstallation.Attributes.Add("OnChange", "setInstallation(this)");
            drpDeliveryTruckReach.Attributes.Add("OnChange", "setDeliveryTruckReach(this,'" + txtTruckDistance .ClientID+ "')");



            if (isEnable)
            {

                btnSave.Attributes.Add("OnClick", "return Validate(true);");
            }
            else
            {
                if (strUserRoleId != "2")
                {

                    btnSave.Visible = false;
                    divAdminComments.Visible = false;
                }

                if (strUserRoleId == "7" || strUserRoleId == "8")
                {
                    divAdminComments.Visible = true;
                }
                btnSave.Attributes.Add("OnClick", "return ValidateAdminComents();");
            }

        }
        catch (Exception ex)
        {

        }
    }


    private void populateQCComments(DataTable dataRecce)
    {
        string strAdminRemarks = dataRecce.Rows[0]["AdminRemarks"].ToString();

        txtAdminComments.Text = strAdminRemarks;
        if (strAdminRemarks != "")
        {
            btnSave.Visible = false;
            txtAdminComments.Enabled = false;
        }

        txtStateMng.Text = dataRecce.Rows[0]["StateUser_Comments"].ToString();
        txtRegionalMngCom.Text = dataRecce.Rows[0]["RegionalUser_Comments"].ToString();

        if (dataRecce.Rows[0]["StateUser_IsApproved"] != null)
            drpStateMngApproved.SelectedValue = dataRecce.Rows[0]["StateUser_IsApproved"].ToString();

        if (dataRecce.Rows[0]["RegionalUser_IsApproved"] != null)
            drpRegionalmng.SelectedValue = dataRecce.Rows[0]["RegionalUser_IsApproved"].ToString();


    }

    private bool setUserVisibility()
    {

        //strUserRoleId
        bool IsEnabled = true;

        if (strUserRoleId == "7")// State Manager
        {
            divStateManager.Style.Add("display","");
            divAdminComments.Style.Add("display", "");
            divRegionalMngCom.Style.Add("display", "");

            txtAdminComments.Enabled = false;
            txtStateMng.Enabled = true;
            txtRegionalMngCom.Enabled = false;
            btnSave.Visible = false;
            btnStateMng.Visible = true;

            drpRegionalmng.Enabled = false;

            if (drpStateMngApproved.SelectedValue != "0")
            {
                btnStateMng.Enabled = false;
                drpStateMngApproved.Enabled = false;
                txtStateMng.Enabled = false;
                setEnable(false);
                IsEnabled = false;

            }

           

        
        }
        else if (strUserRoleId == "8")// Regional Manager
        {
            divStateManager.Style.Add("display", "");
            divAdminComments.Style.Add("display", "");
            divRegionalMngCom.Style.Add("display", "");

            txtAdminComments.Enabled = false;
            txtStateMng.Enabled = false;
            txtRegionalMngCom.Enabled = true;
            btnSave.Visible = false;
            btnStateMng.Visible = false;
            btnRegionalMng.Visible = true;

            drpStateMngApproved.Enabled = false;


            btnRegionalMng.Enabled = false;
            drpRegionalmng.Enabled = false;
            txtRegionalMngCom.Enabled = false;

            if (drpStateMngApproved.SelectedValue == "Y" && drpRegionalmng.SelectedValue == "0")
            {
                btnRegionalMng.Enabled = true;
                drpRegionalmng.Enabled = true;
                txtRegionalMngCom.Enabled = true;


            }
            else if (drpRegionalmng.SelectedValue != "0")
            {
                setEnable(false);
                IsEnabled = false;
            }
        }
        else if (strUserRoleId == "6" || strUserRoleId == "9" || strUserRoleId == "2")// Data Entry,Racee Person// Admin Sony
        {
            setEnable(false);
            IsEnabled = false;

        }

        btnStateMng.Attributes.Add("OnClick", "return clickStateMng()");
        btnRegionalMng.Attributes.Add("OnClick", "return clickRegionalMng()");

        return IsEnabled;
    }



    private DataTable getReceeData()
    {
        DataTable dataTb= null;

        string steQuery = @"select UM.Name as [ReceePerson],OM.OutletName,ReceePersonComments,RefOutletID,RefReceeUserID,ReceeStatusId,ReceeStartTime,convert(varchar(15),ReceeEndTime,105) as [ReceeEndTime],Imagesuffix,ReceeElementString,ReceeElementReviewString,SpecialRemarks,F.InsertedOn,F.InsertedBy,AdminRemarks,StateUser_Comments,RegionalUser_Comments,StateUser_IsApproved,RegionalUser_IsApproved,PSDNoOfPointRequired,PSDNoOfPointAvailable,IsPSDExtensionsCableWorkRequired,FloorLevelingDetails,IsDeliveryTruckReach,DistanceFromTruckToShop,ShopTimings,MarketWeeklyHoliday,ShopOwnersPreferredTimeForInstallation,SOSpaceAvailabilityByDate,SurveyorName,SurveyorMobilePhoneNo,DealerName,DealerMobilePhoneNo,SonyAreaMngName,SonyAreaMngPhone";
         steQuery+=@" from NonQcSonyTempRecee F inner join OutletMaster OM
                      on F.RefOutletID=OM.OutletID 
                      inner join UserMaster UM 
                      on UM.UserID=F.RefReceeUserID where ReceeID=@ReceeID";
        
        SqlParameter[] arrayparam = new SqlParameter[1];

        arrayparam[0] = new SqlParameter("@ReceeID", Convert.ToInt32(strReceeId));
      
        DataSet dataDs= DBLayer.getDataSetFromFinalDB(steQuery,arrayparam);
      
        if(dataDs!=null && dataDs.Tables.Count>0)
        {
            dataTb=dataDs.Tables[0];
        }

        return dataTb;


    }

    private void populateImages(string strImageSuffix,string strFolderName)
    {
        string AuditPic = "SonyReceeUpload/ReceeImage/" + strFolderName+"/";
        string ThumbnailImg = "SonyReceeUpload/ReceeThumbnailImage/" + strFolderName + "/";
       string  picAuditPic = Server.MapPath(AuditPic);

       DirectoryInfo sysdir = new DirectoryInfo(picAuditPic);
        FileInfo[] arrFiles1 = sysdir.GetFiles("*" + strImageSuffix + "*");
        string strImagePath = "";
        string strThumbnailImagePath = "";
        
        string finalImageTbRows = " <table cellpadding='2' cellspacing='5' border='0' ><tr>";
        finalImageTbRows += @"<tr><td colspan='3'> Recee Images</td></tr>";
        for (int count = 0; count < arrFiles1.Length; count++)
        {
           
            strImagePath = AuditPic + arrFiles1[count].Name;
            strThumbnailImagePath =  ThumbnailImg + arrFiles1[count].Name;
            finalImageTbRows += "<td>";
            finalImageTbRows += getImageTable(strImagePath, strThumbnailImagePath);
            finalImageTbRows += "</td>";
            if (((count + 1) % 4) == 0)
            {
                finalImageTbRows += "</tr><tr>";
            }
        }

        finalImageTbRows += "</tr></table>";

        CellImg.InnerHtml = finalImageTbRows;

    }

    private string getImageTable(string imgURl, string ThumbnailImg)
    {
        string strTable = @"<table cellpadding='0' cellspacing='1' border='0'>
                              
                                <tr>
                                    <td align='center'>
<a onclick=""return hs.htmlExpand(this, { objectType: 'iframe', width: '745' } )""  href='RotateImage.aspx?URL=" + imgURl + @"'>
                                    <img src='" + ThumbnailImg + @"' class='thumbnailImg'   /></a>
                                    </td>
                                </tr>
                               </table>";

        return strTable;

    }


    private void setEnable(bool IsEnabled)
    {
        txtOutletId.Enabled = IsEnabled;
        txtSpecialRemarks.Enabled = IsEnabled;
        drpReceePerson.Enabled = IsEnabled;
        txtSurveyorName.Enabled = IsEnabled;
        txtSurveyorPhoneNo.Enabled = IsEnabled;
        txtDealerName.Enabled = IsEnabled;
        txtDealerPhone.Enabled = IsEnabled;
        txtPSDPointRequired.Enabled = IsEnabled;
        drpDeliveryTruckReach.Enabled = IsEnabled;
        txtPSDAvailable.Enabled = IsEnabled;
        drpExtensions.Enabled = IsEnabled;
        txtTruckDistance.Enabled = IsEnabled;
        rdbMWH.Enabled = IsEnabled;
        txtSpaceAvailDate.Enabled = IsEnabled;
        txtOutletId.Enabled = IsEnabled;
        txtSpecialRemarks.Enabled = IsEnabled;
        txtReceePersonRemarks.Enabled = IsEnabled;


    }


    private void populateValue(bool IsEnabled, DataTable dataRecce)
    {


       

        string strOutletID = "";
        string strOutletName = "";



        string strImageSuffix = "";
        DateTime dtInserted = System.DateTime.Now;

        string strSpecialRemarks = "";

        string strReceePersonDetails = "";
        string strReccePersonName = "";

        string strRecceDateTime = "";
        string strReceeStatusId = "";

        string strFolderName = "";

        string strAdminRemarks = "";


        string strTempElementString = "";
        string strElementString = "";

        string strTempElementReviewString = "";
        string strElementReviewString = "";



        string strIsAvalability = "-1"; string strElementStr = ""; string strQuantity = "0"; string strRemarks = "";


        if (dataRecce != null && dataRecce.Rows.Count > 0)
        {
            strOutletID = dataRecce.Rows[0]["RefOutletID"].ToString();//
            strOutletName = dataRecce.Rows[0]["OutletName"].ToString();
            strOutletName = dataRecce.Rows[0]["OutletName"].ToString();
            dtInserted = Convert.ToDateTime(dataRecce.Rows[0]["InsertedOn"].ToString());
            strFolderName = getTodayFolderName(dtInserted);
            strImageSuffix = dataRecce.Rows[0]["Imagesuffix"].ToString();

            strSpecialRemarks = dataRecce.Rows[0]["SpecialRemarks"].ToString();
            strReceePersonDetails = dataRecce.Rows[0]["ReceePersonComments"].ToString();
            strReccePersonName = dataRecce.Rows[0]["ReceePerson"].ToString();
            strRecceDateTime = dataRecce.Rows[0]["ReceeEndTime"].ToString();
            strReceeStatusId = dataRecce.Rows[0]["ReceeStatusId"].ToString();
            strAdminRemarks = dataRecce.Rows[0]["AdminRemarks"].ToString();

             lblReceeDate.Text = strRecceDateTime;
            lblReceePerson.Text = strReccePersonName;
           

            //+++++++++++++++++++++++++++++Start New Field Populate ++++++++++++++++++++++++++++++++++++++++++++++++

          
        
           

            //drpDeliveryTruckReach

            
           
            lblFloorLevelingDetails.Text = dataRecce.Rows[0]["FloorLevelingDetails"].ToString();
            divFloor.Visible = false;

           
          

            lblShopTiming.Text = dataRecce.Rows[0]["ShopTimings"].ToString();
            divShopTiming.Visible = false;

           

            lblShowInstallation.Text = dataRecce.Rows[0]["ShopOwnersPreferredTimeForInstallation"].ToString();
            divShowInstallation.Visible = false;

            string strSOSpaceAvailabilityByDate = "";
            strSOSpaceAvailabilityByDate = Convert.ToDateTime(dataRecce.Rows[0]["SOSpaceAvailabilityByDate"]).ToString("dd/MMM/yyyy");


            if (!IsPostBack)
            {
                drpExtensions.SelectedValue = dataRecce.Rows[0]["IsPSDExtensionsCableWorkRequired"].ToString();


                txtPSDAvailable.Text = dataRecce.Rows[0]["PSDNoOfPointAvailable"].ToString();

                txtSurveyorName.Text = dataRecce.Rows[0]["SurveyorName"].ToString();

                txtSurveyorPhoneNo.Text = dataRecce.Rows[0]["SurveyorMobilePhoneNo"].ToString();

                txtDealerName.Text = dataRecce.Rows[0]["DealerName"].ToString();

                txtDealerPhone.Text = dataRecce.Rows[0]["DealerMobilePhoneNo"].ToString();

                txtPSDPointRequired.Text = dataRecce.Rows[0]["PSDNoOfPointRequired"].ToString();
                txtSpecialRemarks.Text = strSpecialRemarks;
                txtReceePersonRemarks.Text = strReceePersonDetails;

                txtSonyAreMngName.Text = dataRecce.Rows[0]["SonyAreaMngName"].ToString();
                txtSonyAreMngPhone.Text = dataRecce.Rows[0]["SonyAreaMngPhone"].ToString();
                drpReceePerson.SelectedValue = dataRecce.Rows[0]["RefReceeUserID"].ToString();


                drpReceeStatus.SelectedValue = strReceeStatusId;
                drpDeliveryTruckReach.SelectedValue = dataRecce.Rows[0]["IsDeliveryTruckReach"].ToString();
                rdbMWH.SelectedValue = dataRecce.Rows[0]["MarketWeeklyHoliday"].ToString();


                if (strSOSpaceAvailabilityByDate != "21/May/1940")
                    txtSpaceAvailDate.Text = strSOSpaceAvailabilityByDate;

                if (drpExtensions.SelectedValue == "0")
                {
                    txtTruckDistance.Text = dataRecce.Rows[0]["DistanceFromTruckToShop"].ToString();

                }

                if (drpDeliveryTruckReach.SelectedValue == "0")
                {
                    lblTruckLevel.Text = "Distance from Truck to shop: " + dataRecce.Rows[0]["DistanceFromTruckToShop"].ToString() + " (In Meters)";
                    divTruck.Visible = false;
                }




            }








            //+++++++++++++++++++++++++++++End New Field Populate ++++++++++++++++++++++++++++++++++++++++++++++++

            try
            {
                populateImages(strImageSuffix, strFolderName);
            }
            catch (Exception ex)
            {

            }


            //  btnSave.Enabled = false;

            trReceeDate.Style.Add("display", "");
            trReceePerson.Style.Add("display", "");
            divSpecialRemarks.Style.Add("display", "");
            divCounterBody.Style.Add("display", "");

            cellFileUploder.Style.Add("display", "none");
            divDate.Style.Add("display", "none");

            if (strReceeStatusId == "1")
            {
                tdReceeElement.Style.Add("display", "");

                strElementString = dataRecce.Rows[0]["ReceeElementString"].ToString();
                strElementReviewString = dataRecce.Rows[0]["ReceeElementReviewString"].ToString();



            }
            else
            {
                divReceePersonRemarks.Style.Add("display", "");
                tdReceeElement.Style.Add("display", "none");

            }


        }


        txtOutletId.Text = strOutletID + "-" + strOutletName;
        txtOutletId.Enabled = false;

        strRetailerID = Convert.ToString(txtOutletId.Text.Trim().Split('-')[0].Trim());
        populateCounterInfo();

     




        DataTable dataTb = GetElementDetails();

        string strStringElementId = "0";

        if (dataTb != null && dataTb.Rows.Count > 0)
        {
            NoOfRows = dataTb.Rows.Count;
            CreateControl(dataTb);

            string strElementId = "";
            string strcontrolId = "";

            string[] strElementStringArr = strElementString.Split('$');
            string[] strElementReviewStringArr = strElementReviewString.Split(Convert.ToChar(strRemarksSparator));
            //1#1@2|44,55,,55,66,

            for (int count = 0; count < dataTb.Rows.Count; count++)
            {
                strElementId = dataTb.Rows[count]["ElementId"].ToString();
                strcontrolId = strUCRecceElement + "_" + strElementId.ToString();
                UserControl_RecceElementControl elementCon = (UserControl_RecceElementControl)tbRecceeControl.FindControl(strcontrolId);
                if (elementCon != null)
                {
                    for (int count1 = 0; count1 < strElementStringArr.Length; count1++)
                    {
                        if (strElementStringArr[count1].Contains("#"))
                        {
                            strStringElementId = strElementStringArr[count1].Substring(0, strElementStringArr[count1].IndexOf('#'));

                            if (strStringElementId == strElementId)
                            {
                                strIsAvalability = strElementStringArr[count1].Substring((strElementStringArr[count1].IndexOf('#') + 1), (strElementStringArr[count1].IndexOf('@') - (strElementStringArr[count1].IndexOf('#') + 1)));

                                strQuantity = strElementStringArr[count1].Substring((strElementStringArr[count1].IndexOf('@') + 1), (strElementStringArr[count1].IndexOf('*') - (strElementStringArr[count1].IndexOf('@') + 1)));
                                strElementStr = strElementStringArr[count1].Substring((strElementStringArr[count1].IndexOf('*') + 1), (strElementStringArr[count1].Length - (strElementStringArr[count1].IndexOf('*') + 1)));

                                string[] strArrElementRemarks = strElementReviewStringArr[count1].Split(Convert.ToChar(strRemarksIDSparator));

                                if (strArrElementRemarks.Length > 1 && strArrElementRemarks[0] == strElementId)
                                {
                                    strRemarks = strArrElementRemarks[1];
                                }

                                if (strQuantity == "")
                                {
                                    strQuantity = "0";
                                }


                                elementCon.IsAvailability = strIsAvalability;
                                elementCon.Quantity = Convert.ToInt32(strQuantity);
                                elementCon.SelectedValue = strElementStr;
                                elementCon.ElementRemarks = strRemarks;

                                elementCon.Enabled = IsEnabled;

                                break;

                            }
                        }
                    }



                }
            }



        }

    }

    private void setJSScript(DataTable dataTB)
    {
        string strElementID = "";
        string strJsScript = "<script language='javascript'  type='text/javascript'>";


         strJsScript =strJsScript+ @"
      var bit_focus=false;

                    function clickStateMng()
                    {
                         bit_focus=false;
                         var result=true;
                         var strErrorMsg=''; 
                            
                         var txtStateMngComObj=document.getElementById('" + txtStateMng.ClientID + @"');
                         var spanStateMngObj=document.getElementById('spanStateMng');
                             
                         var drpStateMngApprovedObj=document.getElementById('" + drpStateMngApproved.ClientID + @"');
                         var spanDrpStateMngApprovedObj=document.getElementById('spanDrpStateMngApproved');
                             
                         spanStateMngObj.style.display='none';

                        if(trim(txtStateMngComObj.value)=='')
                        {
                                spanStateMngObj.style.display='';
                                                         
                                if(bit_focus==false)
                                {
                                  txtStateMngComObj.focus();
                                  bit_focus=true;
                                }  
                            result=false;
                                                       
                        }   

                        spanDrpStateMngApprovedObj.style.display='none';

                        if(trim(drpStateMngApprovedObj.value)=='0')
                        {
                                spanDrpStateMngApprovedObj.style.display='';
                                                         
                                if(bit_focus==false)
                                {
                                  drpStateMngApprovedObj.focus();
                                  bit_focus=true;
                                }  

                            result=false;
                                                       
                        }    
                                               


                           var preResult=result;       

                       
                            result=Validate(false);   

                          //  alert('result='+result+' ,preResult='+preResult);
                            
                            if(result==true && preResult==false)
                            {

                                alert('Please fill all the fields marked with *');
                            }

                           if(result)
                            {
                                result=preResult;
                            }


                           if(result)
                            {
                               result= confirm('Do you want to save this recce.'); 
                            }


                         if(result)
                            {
                                    setvisibleloading();

                            }

                         return result;

                    }


                   function clickRegionalMng()
                    {
                         bit_focus=false;
                         var result=true;
                         var strErrorMsg=''; 
                            
                         var txtRegionalMngComObj=document.getElementById('" + txtRegionalMngCom.ClientID + @"');
                         var spanRegionalMngComObj=document.getElementById('spanRegionalMngCom');
                             
                         var drpRegionalmngObj=document.getElementById('" + drpRegionalmng.ClientID + @"');
                         var spanDrpRegionalmngObj=document.getElementById('spanDrpRegionalmng');
                             
                         spanRegionalMngComObj.style.display='none';

                        if(trim(txtRegionalMngComObj.value)=='')
                        {
                                spanRegionalMngComObj.style.display='';
                                                         
                                if(bit_focus==false)
                                {
                                  txtRegionalMngComObj.focus();
                                  bit_focus=true;
                                }  
                            result=false;
                                                       
                        }   

                        spanDrpRegionalmngObj.style.display='none';

                        if(trim(drpRegionalmngObj.value)=='0')
                        {
                                spanDrpRegionalmngObj.style.display='';
                                                         
                                if(bit_focus==false)
                                {
                                  drpRegionalmngObj.focus();
                                  bit_focus=true;
                                }  
                            result=false;
                                                       
                        }    
                                               


                             var preResult=result;       

                       
                            result=Validate(false);   

                            if(result==true && preResult==false)
                            {
                               alert('Please fill all the fields marked with *');
                            }

                           


                           if(result)
                            {
                               result= confirm('Do you want to save this recce.'); 
                            }

                           if(result)
                            {
                                    setvisibleloading();

                            }

                         return result;
                    }



                 function ValidateAdminComents()
                    {
                        bit_focus=false;
                         var result=true;
                         var strErrorMsg=''; 
                                
                             var txtAdminCommentsObj=document.getElementById('" + txtAdminComments.ClientID + @"');
                             var spanAdminCommentsObj=document.getElementById('spanAdminComments');
                                            
                                                 spanAdminCommentsObj.style.display='none';

                                                    if(trim(txtAdminCommentsObj.value)=='')
                                                    {
                                                         spanAdminCommentsObj.style.display='';
                                                         
                                                         if(bit_focus==false)
                                                         {
                                                            txtAdminCommentsObj.focus();
                                                            bit_focus=true;
                                                         }  
                                                        result=false;
                                                       
                                                    }    
                                               


                                      

                                         if(bit_focus)
                                        {
                                           alert('Please fill all the fields marked with *');
                                        }


                            if(result)
                            {
                                setvisibleloading();

                            }
               
                    return result; 


                    }

                function setvisibleloading()
                {
                 
                   document.getElementById('UpdateProgress3').style.display='';
                }


            var UploadImgLimit =" + InReceeImgLimit.ToString()+ @";
                         function Validate(IsFileUplloaderCheck)
                                {
                                    bit_focus=false;
                                   var result=true;
                                   var strErrorMsg=''; 
                            
                                    ";
         strJsScript = strJsScript + @"



                      // Strat OuletAddress
                            
                           var txtOutletAddressObj=document.getElementById('" + txtOutletAddress.ClientID + @"');
                           var hdnIsAddressObj=document.getElementById('" + hdnIsAddress.ClientID + @"');

                                        if(txtOutletAddressObj!=null && hdnIsAddressObj!=null && hdnIsAddressObj.value=='True')
                                        {

                                                 if(trim(txtOutletAddressObj.value)=='')
                                                    {
                                                      
                                                         result=false;
                                                         document.getElementById('spanOutletAddress').style.display='';
                                                         
                                                         if(bit_focus==false)
                                                         {
                                                            txtOutletAddressObj.focus();
                                                            bit_focus=true;
                                                         }  
                                                       
                                                    } 
                                                    else
                                                    {
                                                          document.getElementById('spanOutletAddress').style.display='none';
                                                    }   

                                        }

                      // End OuletAddress


                            var txtReceeDateObj=document.getElementById('" + txtReceeDate.ClientID + @"');
                            
                                           
                                                
                                                    if(trim(txtReceeDateObj.value)=='')
                                                    {
                                                        //strErrorMsg='Please select Recee Date';
                                                        result=false;
                                                       document.getElementById('spantxtReceeDate').style.display='';
                                                         
                                                         if(bit_focus==false)
                                                         {
                                                            txtReceeDateObj.focus();
                                                            bit_focus=true;
                                                         }  
                                                       
                                                    } 
                                                    else
                                                    {
                                                          document.getElementById('spantxtReceeDate').style.display='none';
                                                    }   

                                        var drpReceePersonObj=document.getElementById('" + drpReceePerson.ClientID + @"');
                                        if(drpReceePersonObj!=null)
                                        {

                                                 if(trim(drpReceePersonObj.value)=='0')
                                                    {
                                                      
                                                        result=false;
                                                         document.getElementById('spanReceePerson').style.display='';
                                                         
                                                         if(bit_focus==false)
                                                         {
                                                            drpReceePersonObj.focus();
                                                            bit_focus=true;
                                                         }  
                                                       
                                                    } 
                                                    else
                                                    {
                                                          document.getElementById('spanReceePerson').style.display='none';
                                                    }   

                                        }

                      // Strat Surveyor
                            
                           var txtSurveyorNameObj=document.getElementById('" + txtSurveyorName.ClientID + @"');
                                        if(txtSurveyorNameObj!=null)
                                        {

                                                 if(trim(txtSurveyorNameObj.value)=='')
                                                    {
                                                      
                                                        result=false;
                                                         document.getElementById('spanTxtSurveyorName').style.display='';
                                                         
                                                         if(bit_focus==false)
                                                         {
                                                            txtSurveyorNameObj.focus();
                                                            bit_focus=true;
                                                         }  
                                                       
                                                    } 
                                                    else
                                                    {
                                                          document.getElementById('spanTxtSurveyorName').style.display='none';
                                                    }   

                                        }

                      // End Surveyor

                    // Strat Surveyor Mobile No
                            
                           var txtSurveyorPhoneNoObj=document.getElementById('" + txtSurveyorPhoneNo.ClientID + @"');
                                        if(txtSurveyorPhoneNoObj!=null)
                                        {

                                                 if(trim(txtSurveyorPhoneNoObj.value)=='')
                                                    {
                                                      
                                                        result=false;
                                                         document.getElementById('spanTxtSurveyorPhoneNo').style.display='';
                                                         
                                                         if(bit_focus==false)
                                                         {
                                                            txtSurveyorPhoneNoObj.focus();
                                                            bit_focus=true;
                                                         }  
                                                       
                                                    } 
                                                    else
                                                    {
                                                          document.getElementById('spanTxtSurveyorPhoneNo').style.display='none';
                                                    }   

                                        }

                      // End Surveyor Mobile No

                   
                                               

                    var drpReceeStatusObj=document.getElementById('" + drpReceeStatus.ClientID + @"');
                     if(drpReceeStatusObj.value=='1')
                      {



                           // Strat Dealer Name
                            
                           var txtDealerNameObj=document.getElementById('" + txtDealerName.ClientID + @"');
                                        if(txtDealerNameObj!=null)
                                        {

                                                 if(trim(txtDealerNameObj.value)=='')
                                                    {
                                                      
                                                        result=false;
                                                         document.getElementById('spanTxtDealerName').style.display='';
                                                         
                                                         if(bit_focus==false)
                                                         {
                                                            txtDealerNameObj.focus();
                                                            bit_focus=true;
                                                         }  
                                                       
                                                    } 
                                                    else
                                                    {
                                                          document.getElementById('spanTxtDealerName').style.display='none';
                                                    }   

                                        }

                      // End  Dealer Name

                     // Strat Dealer Mobile No
                            
                           var txtDealerPhoneObj=document.getElementById('" + txtDealerPhone.ClientID + @"');
                                        if(txtDealerPhoneObj!=null)
                                        {

                                                 if(trim(txtDealerPhoneObj.value)=='')
                                                    {
                                                      
                                                        result=false;
                                                         document.getElementById('spanTxtDealerPhone').style.display='';
                                                         
                                                         if(bit_focus==false)
                                                         {
                                                            txtDealerPhoneObj.focus();
                                                            bit_focus=true;
                                                         }  
                                                       
                                                    } 
                                                    else
                                                    {
                                                          document.getElementById('spanTxtDealerPhone').style.display='none';
                                                    }   

                                        }

                      // End  Dealer Mobile No

                          // Start  PSDPointRequired 
                            var txtPSDPointRequiredObj=document.getElementById('" + txtPSDPointRequired.ClientID + @"');
                            
                                           
                                                
                                                    if(trim(txtPSDPointRequiredObj.value)=='')
                                                    {
                                                        //strErrorMsg='Please select Recee Date';
                                                        result=false;
                                                       document.getElementById('spanNoOfPointsRequired').style.display='';
                                                         
                                                         if(bit_focus==false)
                                                         {
                                                            txtPSDPointRequiredObj.focus();
                                                            bit_focus=true;
                                                         }  
                                                       
                                                    } 
                                                    else
                                                    {
                                                          document.getElementById('spanNoOfPointsRequired').style.display='none';
                                                    }   

                            // End  PSDPointRequired 

                            // Start  PSDPointAvailable 
                          
                            var txtPSDAvailableObj=document.getElementById('" + txtPSDAvailable.ClientID + @"');
                            
                                           
                                                
                                                    if(trim(txtPSDAvailableObj.value)=='')
                                                    {
                                                        //strErrorMsg='Please select Recee Date';
                                                        result=false;
                                                       document.getElementById('spanNoOfPointsAvailable').style.display='';
                                                         
                                                         if(bit_focus==false)
                                                         {
                                                            txtPSDAvailableObj.focus();
                                                            bit_focus=true;
                                                         }  
                                                       
                                                    } 
                                                    else
                                                    {
                                                          document.getElementById('spanNoOfPointsAvailable').style.display='none';
                                                    }   

                            // End  PSDPointAvailable 

                            // Start  drpExtensions 
                          
                            var drpExtensionsObj=document.getElementById('" + drpExtensions.ClientID + @"');
                            
                                           
                                                
                                                    if(drpExtensionsObj!=null && trim(drpExtensionsObj.value)=='-1')
                                                    {
                                                   
                                                        result=false;
                                                        document.getElementById('spanExtensions').style.display='';
                                                         
                                                         if(bit_focus==false)
                                                         {
                                                            drpExtensionsObj.focus();
                                                            bit_focus=true;
                                                         }  
                                                       
                                                    } 
                                                    else if( document.getElementById('spanExtensions')!=null)
                                                    {
                                                          document.getElementById('spanExtensions').style.display='none';
                                                    }   

                            // End  drpExtensions 


                            // Start  drpFloorLevelingDetails 
                          
                            var drpFloorLevelingDetailsObj=document.getElementById('" + drpFloorLevelingDetails.ClientID + @"');
                            
                                           
                                                
                                                    if(drpFloorLevelingDetailsObj!=null && trim(drpFloorLevelingDetailsObj.value)=='0')
                                                    {
                                                   
                                                        result=false;
                                                       document.getElementById('spanFloorLevelingDetails').style.display='';
                                                         
                                                         if(bit_focus==false)
                                                         {
                                                            drpFloorLevelingDetailsObj.focus();
                                                            bit_focus=true;
                                                         }  
                                                       
                                                    } 
                                                    else if(document.getElementById('spanFloorLevelingDetails')!=null)
                                                    {
                                                          document.getElementById('spanFloorLevelingDetails').style.display='none';
                                                    }   

                            // End  drpFloorLevelingDetails 

                             // Start  drpDeliveryTruckReach 
                          
                            var drpDeliveryTruckReachObj=document.getElementById('" + drpDeliveryTruckReach.ClientID + @"');
                            
                                           
                                                
                                                    if(drpDeliveryTruckReachObj!=null && trim(drpDeliveryTruckReachObj.value)=='-1')
                                                    {
                                                   
                                                        result=false;
                                                       document.getElementById('spanDrpDeliveryTruckReach').style.display='';
                                                         
                                                         if(bit_focus==false)
                                                         {
                                                            drpDeliveryTruckReachObj.focus();
                                                            bit_focus=true;
                                                         }  
                                                       
                                                    } 
                                                    else if(document.getElementById('spanDrpDeliveryTruckReach')!=null)
                                                    {
                                                          document.getElementById('spanDrpDeliveryTruckReach').style.display='none';
                                                          document.getElementById('spanTxtTruckDistance').style.display='none';
                                                   
                                                            if(trim(drpDeliveryTruckReachObj.value)=='0')
                                                            {
                                                                   var txtTruckDistanceObj=document.getElementById('" + txtTruckDistance.ClientID + @"');

                                                                   if(trim(txtTruckDistanceObj.value)=='')
                                                                    {
    
                                                                          result=false;
                                                                           document.getElementById('spanTxtTruckDistance').style.display='';
                                                         
                                                                            if(bit_focus==false)
                                                                            {
                                                                                txtTruckDistanceObj.focus();
                                                                                 bit_focus=true;
                                                                            }  


                                                                    }

                                                            }
                                                          
                                                    }   

                            // End  drpDeliveryTruckReach 

                             // Start  drpShopTimingsFromHours 
                          
                            var drpShopTimingsFromHoursObj=document.getElementById('" + drpShopTimingsFromHours.ClientID + @"');
                            
                                           
                                                
                                                    if(drpShopTimingsFromHoursObj!=null && trim(drpShopTimingsFromHoursObj.value)=='-1')
                                                    {
                                                   
                                                        result=false;
                                                        document.getElementById('spanDrpShopTimingsFromHours').style.display='';
                                                         
                                                         if(bit_focus==false)
                                                         {
                                                            drpShopTimingsFromHoursObj.focus();
                                                            bit_focus=true;
                                                         }  
                                                       
                                                    } 
                                                    else if(document.getElementById('spanDrpShopTimingsFromHours')!=null)
                                                    {
                                                          document.getElementById('spanDrpShopTimingsFromHours').style.display='none';
                                                    }   

                            // End  drpShopTimingsFromHours 

                             // Start  drpShopTimingsToHours 
                          
                             var drpShopTimingsToHoursObj=document.getElementById('" + drpShopTimingsToHours.ClientID + @"');
                            
                                           
                                                
                                                    if(drpShopTimingsToHoursObj!=null && trim(drpShopTimingsToHoursObj.value)=='-1')
                                                    {
                                                   
                                                        result=false;
                                                        document.getElementById('spanDrpShopTimingsToHours').style.display='';
                                                         
                                                         if(bit_focus==false)
                                                         {
                                                            drpShopTimingsToHoursObj.focus();
                                                            bit_focus=true;
                                                         }  
                                                       
                                                    } 
                                                    else if(document.getElementById('spanDrpShopTimingsToHours')!=null)
                                                    {
                                                          document.getElementById('spanDrpShopTimingsToHours').style.display='none';
                                                    }   

                            // End  drpShopTimingsToHours 


 // Start  Market Weekly Holiday:  
                          
                              var rdbMWHObj=document.getElementById('" + rdbMWH.ClientID + @"');
                              var rdbMWHID=rdbMWHObj.id;
                            
                              var IsMWH=false;
                              var currentRBId='';
                              var RdObj=null;

                            for(var count=0;count<=7;count++)
                            {
                                    currentRBId=rdbMWHID+'_'+count.toString();
                                    RdObj=document.getElementById(currentRBId);
                                    
                                    if(RdObj!=null)
                                    {
                                      if(RdObj.checked)
                                        {
                                            IsMWH=true;
                                            break;
                                        }

                                    }

                            }

                      if(IsMWH==false)
                        {

                            result=false;
                            document.getElementById('spanRdbMWH').style.display='';
                                                         
                                if(bit_focus==false)
                                {
                                   
                                    bit_focus=true;
                                }  
                                                       
                        } 
                        else
                        {
                                document.getElementById('spanRdbMWH').style.display='none';
                        }   
                            
                                                

  // End  Market Weekly Holiday:  


 // Start  Shop Owners Preferred time for Installation:  
                          
                            var drpInstallationObj=document.getElementById('" + drpInstallation.ClientID + @"');
                            
                                                   if( document.getElementById('spanDrpInstallation')!=null)
                                                    {
                                                          document.getElementById('spanDrpInstallation').style.display='none';
                                                    }

                                                    if(drpInstallationObj!=null && trim(drpInstallationObj.value)=='0')
                                                    {
                                                   
                                                        result=false;
                                                        document.getElementById('spanDrpInstallation').style.display='';
                                                         
                                                         if(bit_focus==false)
                                                         {
                                                            drpInstallationObj.focus();
                                                            bit_focus=true;
                                                         }  
                                                       
                                                    } 
                                                    else  if(drpInstallationObj!=null && trim(drpInstallationObj.value)=='Timing')
                                                    {
                                                      
                                                          document.getElementById('spanDrpFromHourInstallation').style.display='none';
                                                         document.getElementById('spanDrpToHoursInstallation').style.display='none';
                                                   
                                                               // Start  drpShopTimingsFromHours 
                          
                                                            var drpFromHourInstallationObj=document.getElementById('" + drpFromHourInstallation.ClientID + @"');
                                                            
                                                            
                                                            
                                                            if(trim(drpFromHourInstallationObj.value)=='-1')
                                                            {
                                                            
                                                                result=false;
                                                                document.getElementById('spanDrpFromHourInstallation').style.display='';
                                                                 
                                                                 if(bit_focus==false)
                                                                 {
                                                                    drpFromHourInstallationObj.focus();
                                                                    bit_focus=true;
                                                                 }  
                                                               
                                                            } 
                                                            else
                                                            {
                                                                  document.getElementById('spanDrpFromHourInstallation').style.display='none';
                                                            }   

                          					          // End  drpShopTimingsFromHours 

                            					 // Start  drpShopTimingsToHours 
                          
                                                         var drpToHoursInstallationObj=document.getElementById('" + drpToHoursInstallation.ClientID + @"');
                                
                                               
                                                    
                                                        if(trim(drpToHoursInstallationObj.value)=='-1')
                                                        {
                                                       
                                                            result=false;
                                                            document.getElementById('spanDrpToHoursInstallation').style.display='';
                                                             
                                                             if(bit_focus==false)
                                                             {
                                                                drpToHoursInstallationObj.focus();
                                                                bit_focus=true;
                                                             }  
                                                           
                                                        } 
                                                        else
                                                        {
                                                              document.getElementById('spanDrpToHoursInstallation').style.display='none';
                                                        }   

                            						// End  drpShopTimingsToHours 

                                                          
 
 													}
                                                    
                                                    
     // Start  Shop Owners Preferred time for Installation:  


                        // Start  Sony SO to ensure Space Availability by (Date):  
                          
//                            var txtSpaceAvailDateObj=document.getElementById('" + txtSpaceAvailDate.ClientID + @"');
//                            
//                                           
//                                                
//                                                    if(trim(txtSpaceAvailDateObj.value)=='')
//                                                    {
//                                                        //strErrorMsg='Please select Recee Date';
//                                                        result=false;
//                                                       document.getElementById('spanSpaceAvailDate').style.display='';
//                                                         
//                                                         if(bit_focus==false)
//                                                         {
//                                                            txtSpaceAvailDateObj.focus();
//                                                            bit_focus=true;
//                                                         }  
//                                                       
//                                                    } 
//                                                    else
//                                                    {
//                                                          document.getElementById('spanSpaceAvailDate').style.display='none';
//                                                    }  



                            // End  Sony SO to ensure Space Availability by (Date):  








                ";
        for (int count = 0; count < dataTB.Rows.Count; count++)
        {
            strElementID = dataTB.Rows[count]["ElementId"].ToString();
            strJsScript = strJsScript + @"
                                            
                                                  //  alert('Validate" + strUCRecceElement + "_" + strElementID.ToString() + @"()');
                                                    bit_focus=Validate" + strUCRecceElement + "_" + strElementID.ToString() + @"();
                                                    //alert('bit_focus_" + strUCRecceElement + "_" + strElementID.ToString() + @"='+bit_focus);
                                                    if(bit_focus)
                                                    {
                                                            result=false;
                                                    }
                                             ";
        }

        strJsScript = strJsScript + @" }
else if(parseInt(drpReceeStatusObj.value)>1)
{
                      var  txtReceePersonRemarksObj=document.getElementById('" + txtReceePersonRemarks.ClientID + @"');
  
                                                    if(trim(txtReceePersonRemarksObj.value)=='')
                                                    {
                                                       // strErrorMsg='Please enter Recee Person Remarks';
                                                       
                                                       result=false;
                                                       document.getElementById('spanReceePersonRemarks').style.display='';
                                                         
                                                         if(bit_focus==false)
                                                         {
                                                            txtReceePersonRemarksObj.focus();
                                                            bit_focus=true;
                                                         }  

                                                       
                                                    }  
                                                    else
                                                    {
                                                        document.getElementById('spanReceePersonRemarks').style.display='none';
                                                    }  
                                               
}
else if(parseInt(drpReceeStatusObj.value)==0)
{
                       
                                           
                                                
                                                         result=false;
                                                       document.getElementById('spanDrpReceeStatus').style.display='';
                                                         
                                                         if(bit_focus==false)
                                                         {
                                                            drpReceeStatusObj.focus();
                                                            bit_focus=true;
                                                         }  

                                                




                   
}
else
{
   document.getElementById('spanDrpReceeStatus').style.display='none';
}
  
";

        strJsScript = strJsScript + @"  
                                 
                               if(IsFileUplloaderCheck)
                                {
                                               var  fileUImage_1Obj=document.getElementById('fileUImage_1');
                        
                       

                                                
                                                    if(trim(fileUImage_1Obj.value)=='')
                                                    {
                                                         document.getElementById('spanfileUImage_1').style.display='';
                                                         result=false;
                                                         if(bit_focus==false)
                                                         {
                                                            fileUImage_1Obj.focus();
                                                            bit_focus=true;
                                                         }  

                                                     
                                                    }    
                                                    else
                                                    {
                                                         document.getElementById('spanfileUImage_1').style.display='none';
                                                    }
                                           


                                           
                                               
                                                    if(UploadImgCount>UploadImgLimit)
                                                    {
                                                        alert('Please select Recee Images less than '+UploadImgLimit.toString()+' .');
                                                        result=false;
                                                        document.getElementById('spanfileUImage_1').style.display='';
                                                         
                                                          if(bit_focus==false)
                                                         {
                                                            fileUImage_1Obj.focus();
                                                            bit_focus=true;
                                                         }  
                                                    }    
                                               
                                        }
                             
                                        //alert('bit_focus='+bit_focus);
                                       // alert('result='+result);
                                        if(bit_focus)
                                        {
                                           alert('Please fill all the fields marked with *');
                                       
                                        }


                                  
                            if(result && IsFileUplloaderCheck)
                            {
                                    setvisibleloading();

                            }
                  
                   return result;
                
";






        strJsScript = strJsScript+@"     }
                                    ";

        strJsScript = strJsScript + "</script>";
        lblScript.Text = strJsScript;
    }

    private DataTable GetElementDetails()
    {
        DataTable dataTb = null;


        string strQuery = "select ElementId,ElementName,(case when IsRepeat=1 then 'True' else 'False' end) as [IsRepeat],(case when IsDepth=1 then 'True' else 'False' end) as IsDepth,Height,Width,Depth from ElementMaster where IsActive=1 and IsReccePortal=1";

       
        switch (hdnOutTypeID.Value)
        {
            case "1":
                strQuery += " and IsMBOs=1";
                break;
            case "2":
                strQuery += " and IsNKRCs=1";
                break;
            case "3":
                strQuery += " and IsRKRCs=1";
                break;
            case "4":
                strQuery += " and IsSEES=1";
                break;

        }

        strQuery += " order by SortBy";
        DataSet dataDS = DBLayer.getDataSetFromFinalDB(strQuery);

        if (dataDS != null && dataDS.Tables.Count > 0)
        {
            dataTb = dataDS.Tables[0];
        }

        return dataTb;
    }

    private void CreateControl(DataTable elementTB)
    {
        string strElement = "";
        for (int count = 0; count < elementTB.Rows.Count; count++)
        {

            strElement = elementTB.Rows[count]["ElementId"].ToString();
           
            //--------------------------------------------------Start Add Control Row ---------------------------------------
            HtmlTableRow rowHeaing = new HtmlTableRow();

            HtmlTableCell cellHeadingElement = new HtmlTableCell();

           
            Control elementSourcecontrol = LoadControl("~/UserControl/RecceElementControl.ascx");

            UserControl_RecceElementControl elementCon = (UserControl_RecceElementControl)elementSourcecontrol;
            elementCon.ID = strUCRecceElement + "_" + strElement.ToString();
            elementCon.ControlName = elementTB.Rows[count]["ElementName"].ToString();
            elementCon.ElementID = elementTB.Rows[count]["ElementId"].ToString();
            elementCon.IsRepeat = Convert.ToBoolean(elementTB.Rows[count]["IsRepeat"]);
            elementCon.IsDepth = Convert.ToBoolean(elementTB.Rows[count]["IsDepth"]);

            elementCon.Height = elementTB.Rows[count]["Height"].ToString();
            elementCon.Width = elementTB.Rows[count]["Width"].ToString();
            elementCon.Depth = elementTB.Rows[count]["Depth"].ToString();

            elementCon.Enabled = isEnable;
           
            cellHeadingElement.Controls.Add(elementSourcecontrol);
            rowHeaing.Controls.Add(cellHeadingElement);

            tbRecceeControl.Controls.Add(rowHeaing);

            //--------------------------------------------------Start Add Control Row ---------------------------------------

            HtmlTableRow rowBlank = new HtmlTableRow();

            HtmlTableCell cellBlankElement = new HtmlTableCell();
            cellBlankElement.InnerHtml = @" <table cellpadding='0' cellspacing='0' border='0' width='100%'>
             <tr>
                            <td>
                                <img src='images/spacer.gif' width='2' height='5' alt='' />
                            </td>
                        </tr>
                        <tr>
                            <td class='contenct-seperator-small'>
                                &nbsp;
                            </td>
                        </tr>
                        </table>

                ";

            rowBlank.Controls.Add(cellBlankElement);

            tbRecceeControl.Controls.Add(rowBlank);

            //--------------------------------------------------End Add Blank Space Row ---------------------------------------

        

        }

        setJSScript(elementTB);

    }

    private void populateCounterInfo()
    {
        DataTable dataTb = null;
        string strQuery = @" select  OM.OutletID,(case when OM.RetailerID=0 then '-NA-' else convert(varchar(50),OM.RetailerID) end) as [RetailerID],isnull(OutletName,'-NA-') as [OutletName],isnull(RetailerName,'-NA-') as [RetailerName],isnull(OutletAddress,'-NA-') as [OutletAddress],isnull(convert(varchar(50),RetailerContactNo),'-NA-') as [RetailerContactNo] ,isnull(UM.EmailId,'-NA-') as EmailId,RM.RegionName,
                            SM.StateName,CM.CityName,OM.RefOutletTypeID as [OutletTypeID],OM.OutletType

                            from OutletMaster OM inner join RegionMaster RM
                            on OM.RefRegionID=RM.RegionID
                            inner join StateMaster SM
                            on OM.RefStateID=SM.StateId
                            inner join CityMaster CM
                            on OM.RefCityID=CM.CityId
                            left outer join UserMaster UM
                            on UM.UserID=OM.RetailerUserID
                            where OM.OutletID=" + strRetailerID;
        strQuery += "; select ReceeStatusID as [ID],ReceeStatus as [Text] from ReceeStatusMaster order by ReceeStatusID;";
        strQuery += " select UserID as [ID],Name as [Text] from UserMaster where IsActive=1 and UserRoleId=9 order by Name";
        DataSet dataDS = DBLayer.getDataSetFromFinalDB(strQuery);

        DataTable dataReceeTb = null;
        DataTable dataReceeUserTb = null;
        if (dataDS != null && dataDS.Tables.Count > 0)
        {
            dataTb = dataDS.Tables[0];
            dataReceeTb= dataDS.Tables[1];
            dataReceeUserTb = dataDS.Tables[2];
        }

        if(dataReceeTb!=null)
        {
            DataRow rowRecee = dataReceeTb.NewRow();

            rowRecee[0] = "0";
            rowRecee[1] = "--Select--";
            dataReceeTb.Rows.InsertAt(rowRecee, 0);
            dataReceeTb.AcceptChanges();

            drpReceeStatus.DataSource=dataReceeTb;
            drpReceeStatus.DataBind();


        }

        if (dataReceeUserTb != null)
        {
            DataRow rowReceePer = dataReceeUserTb.NewRow();

            rowReceePer[0] = "0";
            rowReceePer[1] = "--Select--";
            dataReceeUserTb.Rows.InsertAt(rowReceePer, 0);
            dataReceeUserTb.AcceptChanges();

            drpReceePerson.DataSource = dataReceeUserTb;
            drpReceePerson.DataBind();


        }

        if(dataTb!=null && dataTb.Rows.Count>0)
        {
            if (dataTb.Rows[0]["OutletAddress"].ToString().Trim() != "")
            {
                lblOutletAddress.Text = dataTb.Rows[0]["OutletAddress"].ToString();
                divOutletAddress.Style.Add("display", "none");
                hdnIsAddress.Value = "False";
            }
            else
            {
                hdnIsAddress.Value = "True";
                divOutletAddress.Style.Add("display","");

            }
            hdnOutTypeID.Value = dataTb.Rows[0]["OutletTypeID"].ToString();

            strOutletTypeForMsg = dataTb.Rows[0]["OutletType"].ToString();

            lblRetailerID.Text = dataTb.Rows[0]["RetailerID"].ToString();
            lblRetailerName.Text = dataTb.Rows[0]["RetailerName"].ToString();
            lblRetailerOutletName.Text = dataTb.Rows[0]["OutletName"].ToString();

            lblOutletId.Text = dataTb.Rows[0]["OutletID"].ToString();
            lblCity.Text = dataTb.Rows[0]["CityName"].ToString();
            lblState.Text = dataTb.Rows[0]["StateName"].ToString();
            lblRegion.Text = dataTb.Rows[0]["RegionName"].ToString();
            lblOuletType.Text = dataTb.Rows[0]["OutletType"].ToString();
            lblEmailId.Text = dataTb.Rows[0]["EmailId"].ToString();
            lblContactNo.Text = dataTb.Rows[0]["RetailerContactNo"].ToString();
            lblRetailerID.Text = dataTb.Rows[0]["RetailerID"].ToString();

        }


    }
    

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtOutletId.Text.Trim() != "" && txtOutletId.Text.Trim() != "0")
            {
                tbRecceeControl.Controls.Clear();
                strRetailerID = Convert.ToString(txtOutletId.Text.Trim().Split('-')[0].Trim());

                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@p_Para", strRetailerID);
                param[1] = new SqlParameter("@IrisRPID", strUserId);

                string strProc = "select  OutletID,OutletName from OutletMaster where  OutletID=@p_Para and IsActive=1";
                if (IsReccePerson)
                {
                    strProc += " and IrisRPID=@IrisRPID";
                }

                //string strProc = "GetCounterDetials|" + str + "*" + count.ToString() + "^SamsungRace";
                DataSet dataSet = DBLayer.getDataSetFromFinalDB(strProc, param);
                if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
                {

                    populateCounterInfo();
                    DataTable dataTB = GetElementDetails();

                    if (dataTB != null && dataTB.Rows.Count > 0)
                    {
                        NoOfRows = dataTB.Rows.Count;
                        CreateControl(dataTB);
                        btnSave.Enabled = true;
                    }
                    else
                    {
                        string strOutletType = "";

                        btnSave.Enabled = false;
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alertElementIsNotFound", "alert('There is no element found for this outlet (" + strOutletTypeForMsg + "), so you are not able to do data entry for this oultlet. Please contact your manager.');", true);

                    }


                    divCounterBody.Style.Add("display", "");

                }
                else
                {
                    divCounterBody.Style.Add("display", "none");
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alertElementIsNotFound", "alert('Outlet is not found.');", true);

                }
            }
        }
        catch (Exception ex)
        {

        }
    }

    protected void btnStateMng_Click(object sender, EventArgs e)
    {
        string strErrorMsg = "";
        try
        {
            InsertRecee(true, drpStateMngApproved.SelectedValue,general.ClearInput(txtStateMng.Text));
        }
        catch (Exception ex)
        {
            strErrorMsg = "Sorry Your Comments is not save.";
        }

        if (strErrorMsg != "")
        {
            lblMassage.Text = strErrorMsg;
            lblMassage.ForeColor = Color.Red;

        }
    }


    private void InsertRecee(bool IsStateMng,string IsApproved,string strComments)
    {
        string strUpdateQuery = "";

        if (IsStateMng)
        {
            strUpdateQuery = "UPDATE NonQcSonyTempRecee set StateUser_ID=@RefUserID,StateUser_Name=@User_Name,StateUser_IsApproved=@IsApproved,StateUser_Comments=@Comments,StateUser_DateTime=getdate(),StateUser_IPAddress=@IPAddress,SpecialRemarks=@SpecialRemarks,ReceePersonComments=@ReceePersonRemarks,ReceeElementString=@ReceeElementString,ReceeElementReviewString=@ReceeElementReviewString,PSDNoOfPointRequired=@PSDNoOfPointRequired,PSDNoOfPointAvailable=@PSDNoOfPointAvailable,IsPSDExtensionsCableWorkRequired=@IsPSDExtensionsCableWorkRequired,IsDeliveryTruckReach=@IsDeliveryTruckReach,DistanceFromTruckToShop=@DistanceFromTruckToShop,MarketWeeklyHoliday=@MarketWeeklyHoliday,SOSpaceAvailabilityByDate=@SOSpaceAvailabilityByDate,SurveyorName=@SurveyorName,SurveyorMobilePhoneNo=@SurveyorPhoneNo,DealerName=@DealerName,DealerMobilePhoneNo=@DealerPhoneNo,SonyAreaMngName=@SonyAreaMngName,SonyAreaMngPhone=@SonyAreaMngPhone  where ReceeID=@ReceeID;";
        }
        else
        {
            strUpdateQuery = "UPDATE NonQcSonyTempRecee set RegionalUser_ID=@RefUserID,RegionalUser_Name=@User_Name,RegionalUser_IsApproved=@IsApproved,RegionalUser_Comments=@Comments,RegionalUser_DateTime=getdate(),RegionalUser_IPAddress=@IPAddress,SpecialRemarks=@SpecialRemarks,ReceePersonComments=@ReceePersonRemarks,ReceeElementString=@ReceeElementString,ReceeElementReviewString=@ReceeElementReviewString,PSDNoOfPointRequired=@PSDNoOfPointRequired,PSDNoOfPointAvailable=@PSDNoOfPointAvailable,IsPSDExtensionsCableWorkRequired=@IsPSDExtensionsCableWorkRequired,IsDeliveryTruckReach=@IsDeliveryTruckReach,DistanceFromTruckToShop=@DistanceFromTruckToShop,MarketWeeklyHoliday=@MarketWeeklyHoliday,SOSpaceAvailabilityByDate=@SOSpaceAvailabilityByDate,SurveyorName=@SurveyorName,SurveyorMobilePhoneNo=@SurveyorPhoneNo,DealerName=@DealerName,DealerMobilePhoneNo=@DealerPhoneNo,SonyAreaMngName=@SonyAreaMngName,SonyAreaMngPhone=@SonyAreaMngPhone  where ReceeID=@ReceeID;";
            if (IsApproved == "Y")
            {
                strUpdateQuery = strUpdateQuery + @" insert into SonyFinalRecee(RefOutletID,RefReceeUserID,ReceeStatusId,ReceeStartTime,ReceeEndTime,Imagesuffix,SpecialRemarks,ReceePersonComments,ReceeElementString,ReceeElementReviewString,MainReceeID,StateUser_ID,	StateUser_Name,	StateUser_DateTime,StateUser_IPAddress,	StateUser_Comments,	StateUser_IsApproved,	RegionalUser_ID,	RegionalUser_Name,	RegionalUser_DateTime,	RegionalUser_IPAddress,	RegionalUser_Comments,	RegionalUser_IsApproved,	InsertedBy,	InsertedOn,	AdminRemarks,RefIrisRMID,RefIrisSMID,RefIrisCMID,RefSonyRegionalUserId,RefSonyStateUserId,RefSonyCityUserId,PSDNoOfPointRequired,PSDNoOfPointAvailable,IsPSDExtensionsCableWorkRequired,FloorLevelingDetails,IsDeliveryTruckReach,DistanceFromTruckToShop,ShopTimings,MarketWeeklyHoliday,ShopOwnersPreferredTimeForInstallation,SOSpaceAvailabilityByDate,SurveyorName,SurveyorMobilePhoneNo,DealerName,DealerMobilePhoneNo,RefOutletTypeId,OutletType,SonyAreaMngName,SonyAreaMngPhone)
                                        select RefOutletID,RefReceeUserID,ReceeStatusId,ReceeStartTime,ReceeEndTime,Imagesuffix,SpecialRemarks,ReceePersonComments,ReceeElementString,ReceeElementReviewString,ReceeID,StateUser_ID,	StateUser_Name,	StateUser_DateTime,StateUser_IPAddress,	StateUser_Comments,	StateUser_IsApproved,	RegionalUser_ID,	RegionalUser_Name,	RegionalUser_DateTime,	RegionalUser_IPAddress,	RegionalUser_Comments,	RegionalUser_IsApproved,	InsertedBy,	InsertedOn,	AdminRemarks,RefIrisRMID,RefIrisSMID,RefIrisCMID,RefSonyRegionalUserId,RefSonyStateUserId,RefSonyCityUserId,PSDNoOfPointRequired,PSDNoOfPointAvailable,IsPSDExtensionsCableWorkRequired,FloorLevelingDetails,IsDeliveryTruckReach,DistanceFromTruckToShop,ShopTimings,MarketWeeklyHoliday,ShopOwnersPreferredTimeForInstallation,SOSpaceAvailabilityByDate,SurveyorName,SurveyorMobilePhoneNo,DealerName,DealerMobilePhoneNo,RefOutletTypeId,OutletType,SonyAreaMngName,SonyAreaMngPhone
                                        from NonQcSonyTempRecee where ReceeID=@ReceeID;";
            }
        }

       
        string strErrorMsg = "";
        int result = 0;

        try
        {

            SqlParameter[] arrayparam = getInsertReceeSqlPara(System.DateTime.Now, "",IsStateMng,IsApproved,strComments);
           
            result = DBLayer.ExcuteNonQueryInFinalDB(strUpdateQuery, arrayparam);


        }
        catch (Exception ex)
        {

        }

        if (result > 0)
        {
            string Role = "";
           
           
            if (IsStateMng)
                Role = "S";
            else
                Role = "R";



            Response.Redirect("Congratulation.aspx?IsResult=1&IsApp=" + IsApproved + "");
        }
        else
        {
            if (strErrorMsg == "")
            {
                strErrorMsg = "Sorry Your Comments is not save.";
            }

        }

        if (strErrorMsg != "")
        {
            lblMassage.Text = strErrorMsg;
            lblMassage.ForeColor = Color.Red;

        }
    }

    protected void btnRegionalMng_Click(object sender, EventArgs e)
    {
        string strErrorMsg = "";
        try
        {
            InsertRecee(false, drpRegionalmng.SelectedValue, general.ClearInput(txtRegionalMngCom.Text));
        }
        catch (Exception ex)
        {
            strErrorMsg = "Sorry Your Comments is not save.";
        }

        if (strErrorMsg != "")
        {
            lblMassage.Text = strErrorMsg;  
            lblMassage.ForeColor = Color.Red;

        }
    }


    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (isEnable)
        {

            //string strInsertQuery = "insert into NonQcSonyTempRecee(RefOutletID,RefReceeUserID,ReceeStatusId,ReceeStartTime,ReceeEndTime,Imagesuffix,SpecialRemarks,InsertedOn,InsertedBy,ReceePersonComments,ReceeElementString,ReceeElementReviewString,PSDNoOfPointRequired,PSDNoOfPointAvailable,IsPSDExtensionsCableWorkRequired,FloorLevelingDetails,IsDeliveryTruckReach,DistanceFromTruckToShop,ShopTimings,MarketWeeklyHoliday,ShopOwnersPreferredTimeForInstallation,SOSpaceAvailabilityByDate,SurveyorName,SurveyorMobilePhoneNo,DealerName,DealerMobilePhoneNo)";
            //strInsertQuery = strInsertQuery + "         values(@RefOutletID,@RefReceeUserID,@ReceeStatus,@ReceeDate,@ReceeDate,@Imagesuffix,@SpecialRemarks,@InsertedOn,@RefDataEntryUserID,@ReceePersonRemarks,@ReceeElementString,@ReceeElementReviewString,@PSDNoOfPointRequired,@PSDNoOfPointAvailable,@IsPSDExtensionsCableWorkRequired,@FloorLevelingDetails,@IsDeliveryTruckReach,@DistanceFromTruckToShop,@ShopTimings,@MarketWeeklyHoliday,@ShopOwnersPreferredTimeForInstallation,@SOSpaceAvailabilityByDate,@SurveyorName,@SurveyorPhoneNo,@DealerName,@DealerPhoneNo)";

            string strInsertQuery = " exec sp_InsertRecee @RefOutletID,@RefReceeUserID,@ReceeStatus,@ReceeDate,@Imagesuffix,@SpecialRemarks,@RefDataEntryUserID,@ReceePersonRemarks,@ReceeElementString,@ReceeElementReviewString,@PSDNoOfPointRequired,@PSDNoOfPointAvailable,@IsPSDExtensionsCableWorkRequired,@FloorLevelingDetails,@IsDeliveryTruckReach,@DistanceFromTruckToShop,@ShopTimings,@MarketWeeklyHoliday,@ShopOwnersPreferredTimeForInstallation,@SOSpaceAvailabilityByDate,@SurveyorName,@SurveyorPhoneNo,@DealerName,@DealerPhoneNo,@SonyAreaMngName,@SonyAreaMngPhone,@OutletAddress";


            //sp_InsertRecee
            string strErrorMsg = "";
            int result = 0;

            bool IsValidated = true;

            HttpFileCollection uploadFilCol = Request.Files;
            string fileExt = "";
            for (int count = 0; count < uploadFilCol.Count; count++)
            {
                 HttpPostedFile file = uploadFilCol[count];
                  fileExt = Path.GetExtension(file.FileName).ToLower();

                 if (fileExt == ".jpg" || fileExt == ".gif" || fileExt == ".bmp" || fileExt == ".jpeg" || fileExt == ".png" || fileExt == ".tif")
                 {
                     IsValidated = true;
                 }
                 else
                 {
                     IsValidated = false;
                     break;
                 }
            }

            if (IsValidated && uploadFilCol.Count <= InReceeImgLimit)
            {
                try
                {
                    string strImageSuffix = lblOutletId.Text + "_" + general.getImageSuffix();
                    DateTime dtInsertedOn = System.DateTime.Now;

                    if (ImageUpload(dtInsertedOn, strImageSuffix))
                    {

                        SqlParameter[] arrayparam = getInsertReceeSqlPara(dtInsertedOn, strImageSuffix);
                        result = DBLayer.ExcuteNonQueryInFinalDB(strInsertQuery, arrayparam);

                    }
                    else
                    {
                        strErrorMsg = "Sorry your data is not able to upload, Please try again.";
                        result = 0;
                    }
                   
                }
                catch (Exception ex)
                {

                }
            }
            else
            {
                if (IsValidated == false)
                {
                    strErrorMsg = "Please upload valid recce images such as jpg,gif,bmp,jpeg,png,tif.";
                    result = 0;
                }
                else if (strErrorMsg == "")
                {
                    strErrorMsg = "Please upload images less than equal to 25";
                    result = 0;
                }

            }

            if (result > 0 && IsValidated)
            {
                string strFromEmailId="";
                string name=strName;
                string UserName = strUserName;
                string strToEmail = "utkarsh.deep@iris-worldwide.com,kush.srivastava@iris-worldwide.com,iris.rajat@gmail.com,ranadip.boruah@iris-worldwide.com";
               // string strToEmail = "hvermamzn@gmail.com";
                string body = getMailBoby(name, UserName, lblCity.Text, lblOutletId.Text);
                string strSubject = lblRegion.Text + " - " + lblState.Text + " - " +lblCity.Text + " - Outlet ID " + lblOutletId.Text + " - New Recce Data Uploaded";
                string strCcEMailIs="";
                string strBCCEMailIs = "hvermamzn@gmail.com,abhidang@gmail.com";

                try
                {
                    BOLayer boLayer = new BOLayer();
                    boLayer.sendEmail(strFromEmailId, name, strToEmail, body, strSubject, strCcEMailIs, strBCCEMailIs);
                }
                catch (Exception)
                {

                }
                Response.Redirect("Congratulation.aspx?IsResult=1");
            }
            else
            {
                if (strErrorMsg == "")
                {
                    strErrorMsg = "Sorry Your recee is not uploaded.";
                }

            }

            if (strErrorMsg != "")
            {
                lblMassage.Text = strErrorMsg;
                lblMassage.ForeColor = Color.Red;

            }


        }
        else
        {
            string strUpdateQuery = "UPDATE NonQcSonyTempRecee set AdminRemarks=@AdminRemarks  where ReceeID=@ReceeID";
           
            string strErrorMsg = "";
            int result = 0;

            try
            {
                string strAdminComment=general.ClearInput(txtAdminComments.Text);
                SqlParameter[] arrayparam = new SqlParameter[2];
                arrayparam[0] = new SqlParameter("@AdminRemarks", strAdminComment);
                arrayparam[1] = new SqlParameter("@ReceeID", strReceeId);

               result = DBLayer.ExcuteNonQueryInFinalDB(strUpdateQuery, arrayparam);


            }
            catch (Exception ex)
            {

            }

            if (result > 0)
            {
                Response.Redirect("Congratulation.aspx?IsResult=1");
            }
            else
            {
                if (strErrorMsg == "")
                {
                    strErrorMsg = "Sorry Your Comments is not save.";
                }

            }

            if (strErrorMsg != "")
            {
                lblMassage.Text = strErrorMsg;
                lblMassage.ForeColor = Color.Red;

            }
          

        }

    }


    private string getMailBoby(string Name, string UserName,string City,string strOuletID)
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
               FOS from <strong>" + City + @"</strong> has uploaded recce data for the outlet  <strong>" + strOuletID + @"</strong> on <strong>" + strDate + "</strong> at <strong>" + strDatetime + @"</strong>.
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

    private bool ImageUpload(DateTime dtInsertedOn, string strImageSuffix)
    {
        bool IsUploaded = true;
        try
        {
            //  fileUploadImg.PostedFile
            string strUpdateRoot = "SonyReceeUpload";
            if (!Directory.Exists(System.Web.Hosting.HostingEnvironment.MapPath("~/" + strUpdateRoot + "/")))
            {
                Directory.CreateDirectory(System.Web.Hosting.HostingEnvironment.MapPath("~/" + strUpdateRoot + "/"));
            }

            string strImgRootFolder = "";

            string strThumbnailImgRootFolder = "";

            strImgRootFolder = strUpdateRoot + "/ReceeImage";
            strThumbnailImgRootFolder=strUpdateRoot + "/ReceeThumbnailImage";

            

            if (!Directory.Exists(System.Web.Hosting.HostingEnvironment.MapPath("~/" + strImgRootFolder + "/")))
            {
                Directory.CreateDirectory(System.Web.Hosting.HostingEnvironment.MapPath("~/" + strImgRootFolder + "/"));
            }

            if (!Directory.Exists(System.Web.Hosting.HostingEnvironment.MapPath("~/" + strThumbnailImgRootFolder + "/")))
            {
                Directory.CreateDirectory(System.Web.Hosting.HostingEnvironment.MapPath("~/" + strThumbnailImgRootFolder + "/"));
            }


            string strFolderName = getTodayFolderName(dtInsertedOn);

            strImgRootFolder = strImgRootFolder + "/" + strFolderName;

          

            if (!Directory.Exists(System.Web.Hosting.HostingEnvironment.MapPath("~/" + strImgRootFolder + "/")))
            {
                Directory.CreateDirectory(System.Web.Hosting.HostingEnvironment.MapPath("~/" + strImgRootFolder + "/"));
            }

            strThumbnailImgRootFolder = strThumbnailImgRootFolder + "/" + strFolderName;


            if (!Directory.Exists(System.Web.Hosting.HostingEnvironment.MapPath("~/" + strThumbnailImgRootFolder + "/")))
            {
                Directory.CreateDirectory(System.Web.Hosting.HostingEnvironment.MapPath("~/" + strThumbnailImgRootFolder + "/"));
            }

            HttpFileCollection uploadFilCol = Request.Files;
         

            for (int i = 0; i < uploadFilCol.Count; i++)
            {
               
                HttpPostedFile file = uploadFilCol[i];
               
                 
                string fileExt = Path.GetExtension(file.FileName).ToLower();
                string fileName = Path.GetFileName(file.FileName);
                if (fileName != string.Empty)
                {

                    fileName = strImageSuffix +"_"+ (i + 1).ToString() + fileExt;

                    if (fileExt == ".jpg" || fileExt == ".gif" || fileExt == ".bmp" || fileExt == ".jpeg" || fileExt == ".png" || fileExt == ".tif")
                    {
                        file.SaveAs(System.Web.Hosting.HostingEnvironment.MapPath("~/" + strImgRootFolder + "/") + fileName);

                        System.Drawing.Image img = System.Drawing.Image.FromFile(Server.MapPath("~/" + strImgRootFolder + "/") + fileName);

                        System.Drawing.Image thumb = img.GetThumbnailImage(80, 80, null, IntPtr.Zero);

                        thumb.Save(Server.MapPath("~/" + strThumbnailImgRootFolder + "/") + fileName);

                    }

                }

            }

        }
        catch (Exception ex)
        {
            IsUploaded = false;
        }


        return IsUploaded;
    }

    public string getTodayFolderName(DateTime dtInsertedOn)
    {
        string FolderPath = dtInsertedOn.ToString("yyyy") + dtInsertedOn.ToString("MM") + dtInsertedOn.ToString("dd");
        return FolderPath;
    }

    private SqlParameter[] getInsertReceeSqlPara(DateTime dtInsertedOn, string strImageSuffix)
    {
        SqlParameter[] arrayparam = getInsertReceeSqlPara(dtInsertedOn, strImageSuffix,false,"N","");

        return arrayparam;

    }
    private SqlParameter[] getInsertReceeSqlPara(DateTime dtInsertedOn, string strImageSuffix, bool IsStateMng, string IsApproved, string strComments)
    {

        string strIsAvalability = "-1"; string strString = ""; int inElementCount = 0; string strRemarks = "";
        
        
        string strTempElementString = "";
        string strElementString = "";

        string strTempElementReviewString = "";
        string strElementReviewString = "";


        string strSpecialRemarks = general.ClearInput(txtSpecialRemarks.Text);
        string strReceePersonRemarks = general.ClearInput(txtReceePersonRemarks.Text);
        string strReceedate = general.ClearInput(txtReceeDate.Text);
        string strReceeStatus = drpReceeStatus.SelectedValue;

    

        DataTable dataTb = GetElementDetails();

        if (dataTb != null && dataTb.Rows.Count > 0)
        {
            string strElementId = "";
            string strcontrolId = "";

            for (int count = 0; count < dataTb.Rows.Count; count++)
            {
                strElementId = dataTb.Rows[count]["ElementId"].ToString();
                strcontrolId = strUCRecceElement + "_" + strElementId.ToString();
                UserControl_RecceElementControl elementCon = (UserControl_RecceElementControl)tbRecceeControl.FindControl(strcontrolId);
                if (elementCon != null)
                {
                    strRemarks = "";
                    strTempElementString = "";

                    strString = elementCon.SelectedValue;
                    inElementCount = elementCon.Quantity;
                    strIsAvalability = elementCon.IsAvailability;
                    strRemarks = elementCon.ElementRemarks;

                    strTempElementString=strElementId+"#"+strIsAvalability+"@"+inElementCount+"*"+strString;

                    strElementString = strElementString + strTempElementString + "$";

                  
                    strTempElementReviewString = strElementId + strRemarksIDSparator + strRemarks;
                   

              

                    strElementReviewString += strTempElementReviewString + strRemarksSparator;

                }
            }


            if (strElementString != "")
            {
                strElementString = strElementString.Substring(0, strElementString.Length - 1);
            }

            if (strElementReviewString != "")
            {
                strElementReviewString = strElementReviewString.Substring(0, strElementReviewString.Length - 1);
            }
        }

        string PSDNoOfPointRequired = general.ClearInput(txtPSDPointRequired.Text);
        string PSDNoOfPointAvailable = general.ClearInput(txtPSDAvailable.Text);
        string IsPSDExtensionsCableWorkRequired = drpExtensions.SelectedValue;
        string FloorLevelingDetails = drpFloorLevelingDetails.SelectedValue;
        string IsDeliveryTruckReach = drpDeliveryTruckReach.SelectedValue;
        string DistanceFromTruckToShop = general.ClearInput(txtTruckDistance.Text);
        string ShopTimings = drpShopTimingsFromHours.SelectedValue + ":" + drpShopTimingsFromMin.SelectedValue + " to " + drpShopTimingsToHours.SelectedValue + ":" + drpShopTimingsToMin.SelectedValue;
        string MarketWeeklyHoliday = rdbMWH.SelectedValue;
       
        string ShopOwnersPreferredTimeForInstallation = "";

        if (drpInstallation.SelectedValue == "Timing")
        {
            ShopOwnersPreferredTimeForInstallation = drpFromHourInstallation.SelectedValue + ":" + drpFromMinInstallation.SelectedValue + " to " + drpToHoursInstallation.SelectedValue + ":" + drpToMinsInstallation.SelectedValue;
        }
        else
        {
            ShopOwnersPreferredTimeForInstallation = drpInstallation.SelectedValue;
        }

        string SOSpaceAvailabilityByDate = general.ClearInput(txtSpaceAvailDate.Text);

        string SurveyorName = general.ClearInput(txtSurveyorName.Text); ;
        string SurveyorPhoneNo = general.ClearInput(txtSurveyorPhoneNo.Text); ;
        string DealerName = general.ClearInput(txtDealerName.Text);
        string DealerPhoneNo = general.ClearInput(txtDealerPhone.Text);

        string SonyAreaMngName = general.ClearInput(txtSonyAreMngName.Text);
        string SonyAreaMngPhone = general.ClearInput(txtSonyAreMngPhone.Text);

        SqlParameter[] arrayparam=new SqlParameter[34];

        arrayparam[0] = new SqlParameter("@RefOutletID", lblOutletId.Text);
        arrayparam[1] = new SqlParameter("@RefDataEntryUserID", strUserId);
        arrayparam[2] = new SqlParameter("@Imagesuffix", strImageSuffix);
        arrayparam[3] = new SqlParameter("@ReceeElementString", strElementString);
        arrayparam[4] = new SqlParameter("@ReceeElementReviewString", strElementReviewString);


        arrayparam[5] = new SqlParameter("@SpecialRemarks", strSpecialRemarks);

        DateTime dtSOSpaceAvailabilityByDate = Convert.ToDateTime("21-May-1940");

        if (SOSpaceAvailabilityByDate!="")
           dtSOSpaceAvailabilityByDate = Convert.ToDateTime(SOSpaceAvailabilityByDate);
        
        arrayparam[6] = new SqlParameter("@InsertedOn", dtInsertedOn);
        arrayparam[7] = new SqlParameter("@ReceeDate", Convert.ToDateTime(strReceedate));
        arrayparam[8] = new SqlParameter("@ReceePersonRemarks", strReceePersonRemarks);
        arrayparam[9] = new SqlParameter("@ReceeStatus", strReceeStatus);
        arrayparam[10] = new SqlParameter("@RefReceeUserID", drpReceePerson.SelectedValue);

        arrayparam[11] = new SqlParameter("@PSDNoOfPointRequired", PSDNoOfPointRequired);
        arrayparam[12] = new SqlParameter("@PSDNoOfPointAvailable", PSDNoOfPointAvailable);
        arrayparam[13] = new SqlParameter("@IsPSDExtensionsCableWorkRequired", IsPSDExtensionsCableWorkRequired);
        arrayparam[14] = new SqlParameter("@FloorLevelingDetails", FloorLevelingDetails);
        arrayparam[15] = new SqlParameter("@IsDeliveryTruckReach", IsDeliveryTruckReach);
        arrayparam[16] = new SqlParameter("@DistanceFromTruckToShop", DistanceFromTruckToShop);
        arrayparam[17] = new SqlParameter("@ShopTimings", ShopTimings);
        arrayparam[18] = new SqlParameter("@MarketWeeklyHoliday", MarketWeeklyHoliday);
        arrayparam[19] = new SqlParameter("@ShopOwnersPreferredTimeForInstallation", ShopOwnersPreferredTimeForInstallation);
        arrayparam[20] = new SqlParameter("@SOSpaceAvailabilityByDate", dtSOSpaceAvailabilityByDate);

        arrayparam[21] = new SqlParameter("@SurveyorName", SurveyorName);
        arrayparam[22] = new SqlParameter("@SurveyorPhoneNo", SurveyorPhoneNo);
        arrayparam[23] = new SqlParameter("@DealerName", DealerName);
        arrayparam[24] = new SqlParameter("@DealerPhoneNo", DealerPhoneNo);

        arrayparam[25] = new SqlParameter("@SonyAreaMngName", SonyAreaMngName);
        arrayparam[26] = new SqlParameter("@SonyAreaMngPhone", SonyAreaMngPhone);
        arrayparam[27] = new SqlParameter("@OutletAddress", general.ClearInput(txtOutletAddress.Text));


        string User_Name = "";
        string IPAddress = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();

        if (HttpContext.Current.Request.Cookies["UserInfo"] != null)
        {
            User_Name = Convert.ToString(HttpContext.Current.Request.Cookies["UserInfo"]["Name"]);

        }


        arrayparam[28] = new SqlParameter("@Comments", strComments);
        arrayparam[29] = new SqlParameter("@ReceeID", strReceeId);
        arrayparam[30] = new SqlParameter("@User_Name", User_Name);
        arrayparam[31] = new SqlParameter("@IsApproved", IsApproved);
        arrayparam[32] = new SqlParameter("@IPAddress", IPAddress);
        arrayparam[33] = new SqlParameter("@RefUserID", strUserId);

        return arrayparam;
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

    protected void UpdateProgress1_PreRender(object sender, EventArgs e)
    {
       // ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ChangeDivdemin", "ChangeDivdemin('" + divuploading.ClientID + "');", true);

    }
   
}