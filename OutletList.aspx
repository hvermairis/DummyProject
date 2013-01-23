<%@ Page Title="" Language="C#" MasterPageFile="~/SiteFrontMasterPage.master" AutoEventWireup="true"
    CodeFile="OutletList.aspx.cs" Inherits="OutletList" %>

<%@ Register Src="UserControl/BrandCramas.ascx" TagName="BrandCramas" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script language="javascript" type="text/javascript">
        function setIframeUrl(pageUrl) {

            //alert('pageUrl=' + pageUrl);
            var divObj = document.getElementById('lightbox');

            // alert('divObj=' + divObj);

            if (pageUrl != "") {
                // alert('Show');
                document.getElementById('IFrameName').src = pageUrl;
                divObj.style.display = "";

            }
            else {

                document.getElementById('IFrameName').src = "";
                divObj.style.display = "none";

            }


        }



    </script>
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
                <asp:HyperLink ID="hyperBackUrl" runat="server" Text="<< Back" Visible="false"></asp:HyperLink>
            </td>
        </tr>
        <tr>
            <td valign="top" align="right" style="padding-right: 5px;">
                <div id="divDownloadExcel" runat="server">
                    <table cellpadding="2" cellspacing="0" border="0">
                        <tr>
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
            <td valign="top" align="left" class="heading-blue">
                Recce Outlet List
            </td>
        </tr>
        <tr>
            <td valign="top" style="height: 25px;" align="right">
            </td>
        </tr>
        <tr>
            <td valign="top">
                <div>
                    <asp:GridView ID="gridAuditDetail" CellPadding="4" Width="100%" AutoGenerateColumns="true"
                        runat="server" CssClass="gridCssNew" GridLines="None" EmptyDataText="No Recee Found"
                        AllowPaging="true" OnRowDataBound="gridAuditDetail_RowDataBound" OnPageIndexChanging="gridAuditDetail_PageIndexChanging">
                        <FooterStyle CssClass="gridHeaderCssNew" />
                        <RowStyle CssClass="gridRowStypeNew" />
                        <PagerStyle CssClass="gridPagerStyleNew" />
                        <SelectedRowStyle CssClass="gridItemCssNew" />
                        <HeaderStyle CssClass="gridHeaderCssNew" />
                        <AlternatingRowStyle CssClass="gridAlternateItemCssNew" />
                    </asp:GridView>
                </div>
                <div id="lightbox" style="display: none">
                    <iframe id="IFrameName" style="width: 100%; height: 100%;" frameborder="0" src="">
                    </iframe>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
