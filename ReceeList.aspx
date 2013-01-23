<%@ Page Title="" Language="C#" MasterPageFile="~/UserFrontViewMaster.master" AutoEventWireup="true"  EnableEventValidation="false"
    CodeFile="ReceeList.aspx.cs" Inherits="ReceeList" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="tk" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" href="css/gridCss.css" type="text/css" />
    <script language="javascript" type="text/javascript">
        function loadPage(hrefId, urlName) {
            alert(urlName);
            var urlObject = document.getElementById(hrefId);
            urlObject.setAttribute('href', urlName);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table cellpadding="3" cellspacing="1" border="0" width="100%">
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <table cellpadding="2" width="100%" cellspacing="0" border="0">
                    <tr>
                        <td class="headingColor">
                            Recce List
                        </td>
                        <td align="right" style="padding-right: 5px;">
                            <table cellpadding="2" cellspacing="0" border="0">
                                <tr>
                                    <td>
                                        <a href="" id="aOutletDump" style="display:none" runat="server">Download Recce Outlet Dump</a>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        <a href="" id="aRecceDetails" runat="server">Download Recce Details</a>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="right" style="padding-right: 10px;">
                <div id="divDownloadExcel" runat="server">
                    <table cellpadding="2" cellspacing="0" border="0">
                        <tr>
                            <td align="left" valign="bottom">
                                <table cellpadding="3" cellspacing="1" border="0">
                                    <tr>
                                        <td>
                                            Enter Outlet ID
                                        </td>
                                        <td valign="bottom">
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <asp:TextBox ID="txtOutletId" runat="server" CssClass="textbox"></asp:TextBox>
                                                    <tk:TextBoxWatermarkExtender ID="txtOutletId_TextBoxWatermarkExtender" WatermarkText="Enter Counter Id or Name"
                                                        runat="server" Enabled="True" TargetControlID="txtOutletId">
                                                    </tk:TextBoxWatermarkExtender>
                                                    &nbsp;<span id="processing" style="visibility: hidden;"></span>
                                                    <tk:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" MinimumPrefixLength="1"
                                                        CompletionInterval="1000" EnableCaching="true" CompletionSetCount="20" ServiceMethod="GetList"
                                                        CompletionListCssClass="completionList" CompletionListHighlightedItemCssClass="itemHighlighted"
                                                        CompletionListItemCssClass="listItem" OnClientPopulating="ShowIcon" OnClientPopulated="ShowIcon"
                                                        TargetControlID="txtOutletId" BehaviorID="AutoCompleteEx" OnClientItemSelected="getCode">
                                                        <Animations>
                                                                    <OnShow>
                                                                        <Sequence>
                                                                            <%-- Make the completion list transparent and then show it --%>
                                                                            <OpacityAction Opacity="0" />
                                                                            <HideAction Visible="true" />
                                                                            
                                                                            <%--Cache the original size of the completion list the first time
                                                                                the animation is played and then set it to zero --%>
                                                                            
                                                                            
                                                                            
                                                                            <ScriptAction Script="
                                                                                // Cache the size and setup the initial size
                                                                                var behavior = $find('AutoCompleteEx');
                                                                                if (!behavior._height) {
                                                                                    var target = behavior.get_completionList();
                                                                                    behavior._height = target.offsetHeight - 2;
                                                                                    target.style.height = '0px';
                                                                                }" />
                                                                            
                                                                            <%-- Expand from 0px to the appropriate size while fading in --%>
                                                                            
                                                                            
                                                                            
                                                                            <Parallel Duration=".4">
                                                                                <FadeIn />
                                                                                <Length PropertyKey="height" StartValue="0" EndValueScript="$find('AutoCompleteEx')._height" />
                                                                            </Parallel>
                                                                        </Sequence>
                                                                    </OnShow>
                                                                    <OnHide>
                                                                        <%-- Collapse down to 0px and fade out --%>
                                                                        
                                                                        
                                                                        <Parallel Duration=".4">
                                                                            <FadeOut />
                                                                            <Length PropertyKey="height" StartValueScript="$find('AutoCompleteEx')._height" EndValue="0" />
                                                                        </Parallel>
                                                                    </OnHide>
                                                        </Animations>
                                                    </tk:AutoCompleteExtender>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                        <td valign="bottom">
                                            <asp:ImageButton ID="btnSearch" runat="server" Text="Search" ImageUrl="~/images/main-content/search-btn.jpg"
                                                OnClick="btnSearch_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td align="right" style="padding-right: 5px;">
                            </td>
                            <td valign="bottom">
                                <asp:Button ID="btnCityWise" runat="server" Text="Recce Status at a Glance" OnClick="btnCityWise_Click" />
                            </td>
                            <td valign="bottom">
                                <asp:ImageButton ID="btnDownload" runat="server" ImageUrl="~/images/download-excel.png"
                                    OnClick="btnDownload_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <asp:UpdatePanel ID="upReceeList" runat="server">
                    <ContentTemplate>
                        <table cellpadding="3" cellspacing="1" border="0" width="100%">
                            <tr>
                                <td>
                                    <div id="divCounterBody" class="divSize" runat="server">
                                        <table cellpadding="3" cellspacing="1" border="0" width="100%">
                                            <tr>
                                                <td valign="top" align="left" class="areaWidth">
                                                    <div id="divRegion" runat="server">
                                                        <table cellpadding="3" cellspacing="1" border="0" width="100%">
                                                            <tr>
                                                                <td class="filterHeader">
                                                                    Select Region
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:CheckBoxList ID="chkRegion" CssClass="cssChkBox" Width="140" AutoPostBack="true"
                                                                        DataValueField="RegionID" DataTextField="Region" RepeatColumns="1" RepeatDirection="Vertical"
                                                                        runat="server" OnSelectedIndexChanged="chkRegion_SelectedIndexChanged">
                                                                    </asp:CheckBoxList>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </td>
                                                <td valign="top" align="left" class="areaWidth">
                                                    <div id="divState" runat="server">
                                                        <table cellpadding="3" cellspacing="1" border="0" width="100%">
                                                            <tr>
                                                                <td class="filterHeader">
                                                                    Select State
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:CheckBoxList ID="chkState" CssClass="cssChkBox" Width="140" AutoPostBack="true"
                                                                        DataValueField="StateID" DataTextField="State" RepeatColumns="1" RepeatDirection="Vertical"
                                                                        runat="server" OnSelectedIndexChanged="chkState_SelectedIndexChanged">
                                                                    </asp:CheckBoxList>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </td>
                                                <td valign="top" align="left" class="areaWidth">
                                                    <div id="divCity" runat="server">
                                                        <table cellpadding="3" cellspacing="1" border="0" width="100%">
                                                            <tr>
                                                                <td class="filterHeader">
                                                                    Select City
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:CheckBoxList ID="chkCity" CssClass="cssChkBox" Width="140" AutoPostBack="false"
                                                                        DataValueField="CityID" DataTextField="City" RepeatColumns="1" RepeatDirection="Vertical"
                                                                        runat="server">
                                                                    </asp:CheckBoxList>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </td>
                                                   <td valign="top" align="left" class="areaWidth">
                                                    <div id="div3" runat="server">
                                                        <table cellpadding="3" cellspacing="1" border="0" width="100%">
                                                            <tr>
                                                                <td class="filterHeader">
                                                                 Recce Status
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:DropDownList ID="drpReceeStatus" runat="server">
                                                                        <asp:ListItem Value="0" Text="-Select-" Selected="True"> </asp:ListItem>
                                                                        <asp:ListItem Value="1" Text="Yes"> </asp:ListItem>
                                                                        <asp:ListItem Value="2" Text="No"> </asp:ListItem>
                                                                       
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </td>
                                                <td valign="top" align="left" class="areaWidth">
                                                    <div id="div1" runat="server">
                                                        <table cellpadding="3" cellspacing="1" border="0" width="100%">
                                                            <tr>
                                                                <td class="filterHeader">
                                                                    State Approval
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:DropDownList ID="drpStateMngApproved" runat="server">
                                                                        <asp:ListItem Value="0" Text="-Select-" Selected="True"> </asp:ListItem>
                                                                        <asp:ListItem Value="Y" Text="Approved"> </asp:ListItem>
                                                                        <asp:ListItem Value="N" Text="Reject"> </asp:ListItem>
                                                                        <asp:ListItem Value="P" Text="Pending"> </asp:ListItem>
                                                                        <asp:ListItem Value="NR" Text="A/P"> </asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </td>
                                                <td valign="top" align="left" class="areaWidth">
                                                    <div id="div2" runat="server">
                                                        <table cellpadding="3" cellspacing="1" border="0" width="100%">
                                                            <tr>
                                                                <td class="filterHeader">
                                                                    Regional Approval
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:DropDownList ID="drpRegionMngApproved" runat="server">
                                                                        <asp:ListItem Value="0" Text="-Select-" Selected="True"> </asp:ListItem>
                                                                        <asp:ListItem Value="Y" Text="Approved"> </asp:ListItem>
                                                                        <asp:ListItem Value="N" Text="Reject"> </asp:ListItem>
                                                                        <asp:ListItem Value="P" Text="Pending"> </asp:ListItem>
                                                                        <asp:ListItem Value="NR" Text="A/P"> </asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </td>
                                                <td valign="top" align="left" width="150">
                                                    <div id="divDate" runat="server">
                                                        <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                                            <tr>
                                                                <td class="filterHeader">
                                                                    Date Range
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    &nbsp;
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <table cellpadding="3" cellspacing="0" border="0" width="100%">
                                                                        <tr>
                                                                            <td class="csspaddingLeft">
                                                                                From:<br />
                                                                                <table cellpadding="0" cellspacing="0" border="0">
                                                                                    <tr>
                                                                                        <td>
                                                                                            <asp:TextBox ID="calDateFrom" Enabled="false" Width="85" runat="server"></asp:TextBox>
                                                                                            <cc1:TextBoxWatermarkExtender ID="calDateFrom_TextBoxWatermarkExtender" runat="server"
                                                                                                Enabled="True" TargetControlID="calDateFrom" WatermarkText="From Date">
                                                                                            </cc1:TextBoxWatermarkExtender>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:ImageButton runat="Server" ID="imgFrom" ImageUrl="~/images/calendaricon.jpg"
                                                                                                AlternateText="Click here to display calendar" />
                                                                                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="calDateFrom"
                                                                                                Format="dd/MMM/yyyy" PopupButtonID="imgFrom" />
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="csspaddingLeft">
                                                                                To:<br />
                                                                                <table cellpadding="0" cellspacing="0" border="0">
                                                                                    <tr>
                                                                                        <td>
                                                                                            <asp:TextBox ID="calDateTo" Enabled="false" Width="85" runat="server"></asp:TextBox>
                                                                                            <cc1:TextBoxWatermarkExtender ID="calDateTo_TextBoxWatermarkExtender" runat="server"
                                                                                                Enabled="True" TargetControlID="calDateTo" WatermarkText="To Date">
                                                                                            </cc1:TextBoxWatermarkExtender>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:ImageButton runat="Server" ID="imgToDate" ImageUrl="~/images/calendaricon.jpg"
                                                                                                AlternateText="Click here to display calendar" />
                                                                                            <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="calDateTo"
                                                                                                Format="dd/MMM/yyyy" PopupButtonID="imgToDate" />
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td valign="top" style="padding-top: 100;" align="left">
                                                                                <asp:ImageButton ID="imgBtn" runat="server" ImageUrl="~/images/view-report.jpg" OnClick="imgBtn_Click" />
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
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <img src='images/spacer.gif' width='2' height='5' alt='' />
                                </td>
                            </tr>
                            <tr>
                                <td class='contenct-seperator'>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <a href="" id="aRecceZip" runat="server">Download Recce Zip File</a>
                                </td>
                            </tr>
                            <tr>
                                <td class='contenct-seperator'>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <div class="" style="text-align: center; padding-left: 31px;">
                                        <div style="width: 100%; overflow: auto;">
                                            <table cellpadding="3" cellspacing="1" border="0" width="100%">
                                                <tr>
                                                    <td align="left" align="center">
                                                        <asp:GridView ID="gridAuditDetail" CellPadding="4" Width="100%" AutoGenerateColumns="true"
                                                            runat="server" CssClass="gridCssNew" GridLines="None" EmptyDataText="No Recee Found"
                                                            OnRowDataBound="gridAuditDetail_RowDataBound">
                                                            <FooterStyle CssClass="gridHeaderCssNew" />
                                                            <RowStyle CssClass="gridRowStypeNew" />
                                                            <PagerStyle CssClass="gridPagerStyleNew" />
                                                            <SelectedRowStyle CssClass="gridItemCssNew" />
                                                            <HeaderStyle CssClass="gridHeaderCssNew" />
                                                            <AlternatingRowStyle CssClass="gridAlternateItemCssNew" />
                                                        </asp:GridView>
                                                    </td>
                                                </tr>
                                                <tr class="gridHeaderCssNew">
                                                    <td align="center">
                                                        <table cellpadding="1" cellspacing="1" border="0">
                                                            <tr>
                                                                <td>
                                                                    <asp:HiddenField ID="hdnStartPage" runat="server" Value="1" />
                                                                    <asp:HiddenField ID="hdnEndPage" runat="server" Value="1" />
                                                                    <asp:LinkButton ID="linkPreVious" runat="server" Text="<< Pre" OnClick="linkPreVious_Click"></asp:LinkButton>
                                                                </td>
                                                                <td>
                                                                    <table cellpadding="1" cellspacing="3" border="0" id="tbTableImg" runat="server">
                                                                    </table>
                                                                </td>
                                                                <td>
                                                                    <asp:LinkButton ID="linkNext" runat="server" Text="Next >>" OnClick="linkNext_Click"></asp:LinkButton>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
</asp:Content>
