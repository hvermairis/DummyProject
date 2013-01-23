<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ZipFileDownload.aspx.cs"
    Inherits="ZipFileDownload" %>

<%@ Register Src="~/UserControl/RecceElementLabel.ascx" TagName="RecceElementLabel"
    TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div id="excelContainer" runat="server">
            <table cellpadding="0" cellspacing="0" border="0" width="100%">
                <tr>
                    <td>
                        &nbsp;
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
                                                                Outlet Type
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblOutletType" runat="server" Text=""></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td width='49'>
                                                                5
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
                                                                6
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
                                                                7
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
                                                                8
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
                                                                9
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
                                                                10
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
                                                                11
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
                                                                12
                                                            </td>
                                                            <td>
                                                                Dealer Mobile Phone
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblDealerMobilePhone" runat="server" Text=""></asp:Label>
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
                                                                13
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
                                                                14
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
                                                                15
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
                                                                16
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
                                                                17
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
                                                                                18
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
                                                                19
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
                                                                20
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
                                                                21
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
                                                                22
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
                                                                23
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
                                                                24
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
                                                                        <div id="divElement" runat="server"></div>
                                                                    
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
                                                            <td colspan='2' width="40%" valign="top" align="left" style="font-weight: bold; background-color: #36F;
                                                                color: #FFF;">
                                                                <asp:Literal ID="literCellImg" runat="server">Images</asp:Literal>
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
        </div>
    </div>
    </form>
</body>
</html>
