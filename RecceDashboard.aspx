<%@ Page Title="" Language="C#" MasterPageFile="~/SiteFrontMasterPage.master" AutoEventWireup="true"
    CodeFile="RecceDashboard.aspx.cs" Inherits="RecceDashboard" %>

<%@ Register Src="UserControl/BrandCramas.ascx" TagName="BrandCramas" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="100%" border="0" align="left" cellpadding="0" cellspacing="0">
        <tr>
            <td valign="top">
            </td>
        </tr>
        <tr>
            <td valign="top" align="left" class="leftCellCss">
                <uc1:BrandCramas ID="brandCramas" runat="server" />
            </td>
        </tr>
        <tr>
            <td valign="top">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td valign="top" align="left" class="heading-blue" nowrap="nowrap">
                Recce Dashboard&nbsp;-&nbsp;<asp:Label ID="lblPageHeading" runat="server" Text="All India"> </asp:Label>
            </td>
        </tr>
        <tr>
            <td valign="top" style="height: 50px;" align="right">
                <table cellpadding="2" cellspacing="0" border="0">
                    <tr>
                     <td class="headingColor" align="right" style="padding-right: 3px;">
                            <a href="" id="aRecceSynopsis" runat="server">Download Recce Synopsis</a>
                        </td>
                        <td>
                        |
                        </td>
                        <td class="headingColor" align="right" style="padding-right: 3px;">
                            <a href="" id="aRecceWIP" runat="server">Download Recce WIP</a>
                        </td>
                        <td>
                        |
                        </td>
                        <td class="headingColor" align="right" style="padding-right: 3px;">
                            <a href="" id="aRecceDetails" runat="server">Download Recce Details</a>
                        </td>
                       
                        <td align="right" >
                            <div id="divOutletListButton" runat="server" style="display: none; padding-right: 5px;">
                                <asp:ImageButton ID="btnOutListTypeID" ImageUrl="~/images/outlet-list.png" runat="server"
                                    Text="OutletList" OnClick="btnOutListTypeID_Click" />
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td valign="top" align="left">
                <table width="100%" border="0" align="left" cellpadding="0" cellspacing="0">
                    <tr>
                        <td valign="top" align="left" width="60%">
                            <table width="1000%" border="0" align="left" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td class="leftCellCss dashlblWidth BoldText">
                                        Status as of today :
                                    </td>
                                    <td class="leftCellCss">
                                        <asp:Label ID="lblAsPerToday" runat="server" Text="2000"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" align="left" valign="top">
                                        <table cellspacing="1" cellpadding="1" border="0">
                                            <tr>
                                                <td class="leftCellCss dashlblWidth">
                                                    No. of recce data approved :
                                                </td>
                                                <td class="leftCellCss BoldText">
                                                    <asp:Label ID="lblApprovedRecce" runat="server" Text="2000"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" align="left" valign="top">
                                        <table cellspacing="1" cellpadding="1" border="0">
                                            <tr>
                                                <td class="leftCellCss dashlblWidth">
                                                    No. of recce data upload :
                                                </td>
                                                <td class="leftCellCss BoldText">
                                                    <asp:Label ID="lblRecceDataUpload" runat="server" Text="3000"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" align="left" valign="top">
                                        <div id="divPending" runat="server" style="display: none;">
                                            <table cellspacing="1" cellpadding="1" border="0">
                                                <tr>
                                                    <td class="leftCellCss dashlblWidth">
                                                        No. of Recee data is pending for review :
                                                    </td>
                                                    <td class="leftCellCss BoldText">
                                                        <asp:HyperLink ID="lblPending" runat="server" Text="3000" NavigateUrl="~/ReceeList.aspx"></asp:HyperLink>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" align="left" valign="top">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" align="left" valign="top">
                                        <table cellspacing="1" cellpadding="1" border="0">
                                            <tr>
                                                <td class="leftCellCss dashlblWidth">
                                                    Outlet Type :
                                                </td>
                                                <td class="leftCellCss">
                                                    <asp:Label ID="lblOutletType" runat="server" Text="3000"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td valign="top" align="right">
                            <div id="divDownloadExcel" runat="server">
                                <table cellpadding="2" cellspacing="0" border="0">
                                    <tr>
                                        <td valign="bottom">
                                            <a href="HAAssessmentReport.aspx?DL=1" runat="server" id="HiperLExcle" style="line-height: 8px;">
                                                <img src="images/download-excel.png" alt="Excel Download" style="text-decoration: none;" /></a>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td valign="top" style="height: 50px;">
            </td>
        </tr>
        <tr>
            <td valign="top">
                <asp:Literal ID="lblMainElementHTml" runat="server"></asp:Literal>
            </td>
        </tr>
    </table>
</asp:Content>
