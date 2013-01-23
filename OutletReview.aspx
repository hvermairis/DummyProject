<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OutletReview.aspx.cs" Inherits="OutletReview" %>

<%@ Register Src="~/UserControl/RecceElementLabel.ascx" TagName="RecceElementLabel"
    TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Sony Recce</title>
    <link rel="stylesheet" href="css/devcss.css" type="text/css" />
    <link rel="stylesheet" href="css/style.css" type="text/css" />
    <link rel="stylesheet" href="css/gridCss.css" type="text/css" />
    <script language="javascript" src="js/jquery-1.7.1.min.js" type="text/javascript"></script>
    <script language="javascript" src="js/script.js" type="text/javascript"></script>
    <script type="text/javascript" src="highslide/highslide-with-html.js"></script>
    <link rel="stylesheet" href="css/highslide/highslideCss.css" type="text/css" media="screen, projection" />
    <script type="text/javascript">
        hs.graphicsDir = 'highslide/graphics/';
        hs.outlineType = 'rounded-white';
        hs.outlineWhileAnimating = true;
        function comingsoon() {
            alert("Visi Score Deck coming Soon");
        }

        // window.history.forward(1);
        function setIframeUrl(pageUrl) {

            // alert('pageUrl=' + pageUrl);
            var divObj = window.parent.document.getElementById('lightbox');
            // alert('divObj=' + divObj);
            // alert('IFrameName=' + document.getElementById('IFrameName'));

            if (pageUrl != "") {
                // alert('Show');
                window.parent.document.getElementById('IFrameName').src = pageUrl;
                divObj.style.display = "";

            }
            else {

                window.parent.document.getElementById('IFrameName').src = "";
                divObj.style.display = "none";

            }

        }



    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td>
                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td>
                            </td>
                            <td align="right" style="padding-right: 5px;">
                                <img src="images/Close.png" onclick="setIframeUrl('');" alt="Close" style="text-decoration: none;" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" align="right" style="padding-top: 8px;">
                                <table border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td class="lblHeaderUserName">
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td width="33">
                                &nbsp;
                            </td>
                            <td valign="top">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td width="98%">
                                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td class="main-content-top-left-bg">
                                                        &nbsp;
                                                    </td>
                                                    <td class="main-content-top-middle-bg">
                                                        <%--  <div id="menu-nav">
                                                                    <ul>
                                                                        <li><a href="ReceeList.aspx">Recce List</a></li>
                                                                        <li><a href="ReceeForm.aspx">New Recce</a></li>
                                                                    </ul>
                                                                </div>--%>
                                                    </td>
                                                    <td class="main-content-top-right-bg">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="main-content-middle-left-bg">
                                                        &nbsp;
                                                    </td>
                                                    <td class="main-content-bg-color">
                                                        <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                                            <tr>
                                                                <td>
                                                                    &nbsp;
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right" valign="top">
                                                                    <div id="divDownloadExcel" runat="server">
                                                                        <table cellpadding="2" cellspacing="0" border="0">
                                                                            <tr>
                                                                                <td valign="bottom">
                                                                                    <asp:HyperLink ID="hyperCustom" runat="server" Text="Custom Recce Detail"></asp:HyperLink>
                                                                                </td>
                                                                                <td valign="bottom">
                                                                                    <a href="HAAssessmentReport.aspx?DL=1" runat="server" id="HiperLExcle" style="line-height: 8px;"
                                                                                        target="_blank">
                                                                                        <img src="images/download-excel.png" alt="Excel Download" style="text-decoration: none;" /></a>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <div id="divExcel" runat="server">
                                                                        <table width='100%' border='1' cellspacing='1' cellpadding='3'>
                                                                            <tr>
                                                                                <td colspan='4' style='height: 50px; text-align: right; padding-right: 10px; vertical-align: middle;'
                                                                                    class="heading-blue">
                                                                                    Outlet Survey Form ( Sony Xperia Smart Phones SIS & Branding unit)
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="4">
                                                                                    <table width='100%' border='0' cellspacing='1' cellpadding='3'>
                                                                                        <tr>
                                                                                            <td width="60%">
                                                                                                <table width='100%' border='0' cellspacing='1' cellpadding='3'>
                                                                                                    <tr>
                                                                                                        <td class="BoldText">
                                                                                                            S.No
                                                                                                        </td>
                                                                                                        <td class="BoldText">
                                                                                                            Item
                                                                                                        </td>
                                                                                                        <td class="BoldText">
                                                                                                            Description
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td width="49">
                                                                                                            1
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            OutletName
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <asp:Label ID="lblOutletName" runat="server" Text=""></asp:Label>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td width='49'>
                                                                                                            2
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            Outlet ID
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <asp:Label ID="lblOutletID" runat="server" Text=""></asp:Label>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td width='49'>
                                                                                                            3
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            Outlet Address
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <asp:Label ID="lblOutletAddress" runat="server" Text=""></asp:Label>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td width='49'>
                                                                                                            4
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            City
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <asp:Label ID="lblCity" runat="server" Text=""></asp:Label>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td width='49'>
                                                                                                            5
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            State
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <asp:Label ID="lblState" runat="server" Text=""></asp:Label>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td width='49'>
                                                                                                            6
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            Region
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <asp:Label ID="lblRegion" runat="server" Text=""></asp:Label>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td width='49'>
                                                                                                            7
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            Outlet Type
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <asp:Label ID="lblOutletType" runat="server" Text=""></asp:Label>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td width='49'>
                                                                                                            8
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            Shop Owner ContactNo
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <asp:Label ID="lblShopOwnerContactNo" runat="server" Text=""></asp:Label>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td width='49'>
                                                                                                            9
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            Surveyor Name
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <asp:Label ID="lblSurveyorName" runat="server" Text=""></asp:Label>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td width='49'>
                                                                                                            10
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            Surveyor Mobile Phone
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <asp:Label ID="lblSurveyorMobilePhone" runat="server" Text=""></asp:Label>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td width='49'>
                                                                                                            11
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            Sony Area Manager Name
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <asp:Label ID="lblSonyAreaManagerName" runat="server" Text=""></asp:Label>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td width='49'>
                                                                                                            12
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            Sony Area Manager Phone
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <asp:Label ID="lblSonyAreaManagerPhone" runat="server" Text=""></asp:Label>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td width='49'>
                                                                                                            13
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            Recce Status
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <asp:Label ID="lblRecceStatus" runat="server" Text=""></asp:Label>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td width='49'>
                                                                                                            14
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            Dealer Name
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <asp:Label ID="lblDealerName" runat="server" Text=""></asp:Label>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td width='49'>
                                                                                                            15
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            Dealer Mobile Phone
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <asp:Label ID="lblDealerMobilePhone" runat="server" Text=""></asp:Label>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td width='49'>
                                                                                                            16
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            Admin Comments
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <asp:Label ID="lblAdminComments" runat="server" Text=""></asp:Label>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td colspan="3">
                                                                                                            &nbsp;
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td>
                                                                                                            &nbsp;
                                                                                                        </td>
                                                                                                        <td colspan="2" align="left" valign="top" style="font-weight: bold;">
                                                                                                            Power Supply Details
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td width='49'>
                                                                                                            17
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            No of Points Required
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <asp:Label ID="lblNoOfPointsRequired" runat="server" Text=""></asp:Label>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td width='49'>
                                                                                                            18
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            No of Points Available
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <asp:Label ID="lblNoofPointsAvailable" runat="server" Text=""></asp:Label>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td colspan="3">
                                                                                                            &nbsp;
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td width='49'>
                                                                                                            19
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            Extensions/ Cable work required
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <asp:Label ID="lblExtensionsCable" runat="server" Text=""></asp:Label>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td width='49'>
                                                                                                            20
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            Floor leveling Details
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <asp:Label ID="lblFloorlevelingDetails" runat="server" Text=""></asp:Label>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td>
                                                                                                            21
                                                                                                        </td>
                                                                                                        <td align="left" valign="top">
                                                                                                            Delivery Truck Reach
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <asp:Label ID="lblDeliveryTruckReach" runat="server" Text=""></asp:Label>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td>
                                                                                                            &nbsp
                                                                                                        </td>
                                                                                                        <td colspan="2">
                                                                                                            <div id="divDeliveryAvailable" runat="server" style="display: none;">
                                                                                                                <table cellpadding="3" cellspacing="1" width="100%">
                                                                                                                    <tr>
                                                                                                                        <td width='49'>
                                                                                                                            22
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            Can it reach the shop door step:
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <asp:Label ID="lblCanItReachThe" runat="server" Text=""></asp:Label>
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                </table>
                                                                                                            </div>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td width='49'>
                                                                                                            23
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            Shop Timings (Opening / Closing)
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <asp:Label ID="lblShopTimings" runat="server" Text=""></asp:Label>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td width='49'>
                                                                                                            24
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            Market Weekly Holiday
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <asp:Label ID="lblMarketWeekly" runat="server" Text=""></asp:Label>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td width='49'>
                                                                                                            25
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            Shop Owners Preferred time for Installation
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <asp:Label ID="lblShopOwnersPreferred" runat="server" Text=""></asp:Label>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td width='49'>
                                                                                                            26
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            Sony SO to ensure Space Availability by (Date)
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <asp:Label ID="lblSonySOto" runat="server" Text=""></asp:Label>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td width='49'>
                                                                                                            27
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            Dealer Remarks
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <asp:Label ID="lblDealerRemarks" runat="server" Text=""></asp:Label>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td colspan="3">
                                                                                                            &nbsp;
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td valign="top">
                                                                                                            28
                                                                                                        </td>
                                                                                                        <td colspan="2" align="left" valign="top">
                                                                                                            <table cellpadding="1" cellspacing="1" border="0" width="100%">
                                                                                                                <tr>
                                                                                                                    <td width="25%" valign="top" align="left" class="BoldText">
                                                                                                                        Elements
                                                                                                                    </td>
                                                                                                                    <td width="15%" valign="top" align="left" class="BoldText">
                                                                                                                        Available
                                                                                                                    </td>
                                                                                                                    <td width="15%" valign="top" align="left" class="BoldText">
                                                                                                                        Height
                                                                                                                    </td>
                                                                                                                    <td width="15%" valign="top" align="left" class="BoldText">
                                                                                                                        Width
                                                                                                                    </td>
                                                                                                                    <td width="15%" valign="top" align="left" class="BoldText">
                                                                                                                        Depth
                                                                                                                    </td>
                                                                                                                    <td width="15%" valign="top" align="left" class="BoldText">
                                                                                                                        Surface
                                                                                                                    </td>
                                                                                                                    <td width="15%" valign="top" align="left" class="BoldText">
                                                                                                                        Element Remarks
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                                <tr>
                                                                                                                    <td colspan="7" valign="top" align="left">
                                                                                                                        <table cellpadding="3" cellspacing="1" border="0" width="100%" id="tbElement" runat="server">
                                                                                                                        </table>
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                            </table>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </table>
                                                                                            </td>
                                                                                            <td width="40%" valign="top" align="left">
                                                                                                <table cellpadding="1" cellspacing="3" border="0" width="100%">
                                                                                                    <tr>
                                                                                                        <td colspan='3' class="BoldText">
                                                                                                            Recee Images
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td colspan='2' id="CellImg" runat="server" width="40%" valign="top" align="left">
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </table>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td class="main-content-middle-right-bg">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="main-content-bottom-left-bg">
                                                        &nbsp;
                                                    </td>
                                                    <td class="main-content-bottom-middle-bg">
                                                        &nbsp;
                                                    </td>
                                                    <td class="main-content-bottom-right-bg">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td width="2%">
                                            &nbsp;
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
