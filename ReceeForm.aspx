<%@ Page Title="" Language="C#" MasterPageFile="~/UserFrontViewMaster.master" AutoEventWireup="true"
    CodeFile="ReceeForm.aspx.cs" Inherits="ReceeForm" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="tk" %>
<%@ Register Src="~/UserControl/RecceElementControl.ascx" TagName="RecceElementControl"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <%--  <script src="js/jquery-1.3.2.js" type="text/javascript"></script>
    <script src="js/jquery-1.3.2-vsdoc2.js" type="text/javascript"></script>
        <script src="js/jquery.MultiFile.js" type="text/javascript"></script>

    --%>
    <script type="text/javascript" src="highslide/highslide-with-html.js"></script>
    <link rel="stylesheet" href="css/highslide/highslideCss.css" type="text/css" media="screen, projection" />
    <script type="text/javascript">
        hs.graphicsDir = 'highslide/graphics/';
        hs.outlineType = 'rounded-white';
        hs.outlineWhileAnimating = true;
        function comingsoon() {
            alert("Visi Score Deck coming Soon");
        }

        window.history.forward(1);
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="divuploading" style="width: 100%; left: 550px; text-align: center; top: 300px;
        position: absolute;">
        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="updatedFirst"
            OnInit="UpdateProgress1_PreRender">
            <ProgressTemplate>
                <div style="width: 200px; padding: 60px; text-align: center; background-color: White;
                    border: solid 2px #1F64B6;">
                    <img src="images/processing.gif" alt="Processing..." />
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>
    <div style="width: 100%; left: 550px; text-align: center; top: 1000px; position: absolute;"
        id="divuploading23">
        <div style="display: none;" id="UpdateProgress3">
            <div style="width: 200px; padding: 60px; text-align: center; background-color: White;
                border: solid 2px #1F64B6;">
                <img alt="Processing..." src="images/processing.gif" />
            </div>
        </div>
    </div>
    <table cellpadding="3" cellspacing="1" border="0" width="100%">
        <tr>
            <td>
                <table cellpadding="3" cellspacing="1" border="0" width="100%">
                    <tr>
                        <td align="left">
                            <table cellpadding="3" cellspacing="1" border="0">
                                <tr>
                                    <td>
                                        Enter Outlet ID
                                    </td>
                                    <td>
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <asp:TextBox ID="txtOutletId" runat="server" CssClass="textbox"></asp:TextBox>
                                                <tk:TextBoxWatermarkExtender ID="txtOutletId_TextBoxWatermarkExtender" WatermarkText="Enter Outlet Id or Name"
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
                                    <td>
                                        <asp:ImageButton ID="btnSearch" runat="server" Text="Search" ImageUrl="~/images/main-content/search-btn.jpg"
                                            OnClick="btnSearch_Click" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td align="right" style="padding-right: 5px;">
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
        <tr>
            <td valign="top">
                <div id="divCounterBody" runat="server" style="display: none;">
                    <table cellpadding="3" cellspacing="1" border="0" width="100%">
                        <tr>
                            <td class="heading-blue">
                                <asp:Literal ID="lblScript" runat="server"></asp:Literal>
                                Retailer Information
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div>
                                    <table cellpadding="3" cellspacing="1" border="0">
                                        <tr>
                                            <td colspan="3">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="leftCellCss">
                                                Outlet ID
                                            </td>
                                            <td class="CenterCellCss">
                                                :
                                            </td>
                                            <td class="rightCellCss">
                                                <asp:Label ID="lblOutletId" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="leftCellCss">
                                                Retailer ID
                                            </td>
                                            <td class="CenterCellCss">
                                                :
                                            </td>
                                            <td class="rightCellCss">
                                                <asp:Label ID="lblRetailerID" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="leftCellCss">
                                                Retailer Name
                                            </td>
                                            <td class="CenterCellCss">
                                                :
                                            </td>
                                            <td class="rightCellCss">
                                                <asp:Label ID="lblRetailerName" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="leftCellCss">
                                                Retailer Outlet
                                            </td>
                                            <td class="CenterCellCss">
                                                :
                                            </td>
                                            <td class="rightCellCss">
                                                <asp:Label ID="lblRetailerOutletName" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="leftCellCss">
                                                Outlet Address
                                            </td>
                                            <td class="CenterCellCss">
                                                :
                                            </td>
                                            <td class="rightCellCss">
                                                <asp:Label ID="lblOutletAddress" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="leftCellCss">
                                                City
                                            </td>
                                            <td class="CenterCellCss">
                                                :
                                            </td>
                                            <td class="rightCellCss">
                                                <asp:Label ID="lblCity" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="leftCellCss">
                                                State
                                            </td>
                                            <td class="CenterCellCss">
                                                :
                                            </td>
                                            <td class="rightCellCss">
                                                <asp:Label ID="lblState" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="leftCellCss">
                                                Region
                                            </td>
                                            <td class="CenterCellCss">
                                                :
                                            </td>
                                            <td class="rightCellCss">
                                                <asp:Label ID="lblRegion" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                           <tr>
                                            <td class="leftCellCss">
                                                Outlet Type
                                            </td>
                                            <td class="CenterCellCss">
                                                :
                                            </td>
                                            <td class="rightCellCss">
                                                <asp:Label ID="lblOuletType" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr style="display: none;" runat="server" id="trReceeDate">
                                            <td class="leftCellCss">
                                                Recce Date
                                            </td>
                                            <td class="CenterCellCss">
                                                :
                                            </td>
                                            <td class="rightCellCss">
                                                <asp:Label ID="lblReceeDate" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr style="display: none;" runat="server" id="trReceePerson">
                                            <td class="leftCellCss">
                                                Recce User
                                            </td>
                                            <td class="CenterCellCss">
                                                :
                                            </td>
                                            <td class="rightCellCss">
                                                <asp:Label ID="lblReceePerson" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="leftCellCss">
                                                Email
                                            </td>
                                            <td class="CenterCellCss">
                                                :
                                            </td>
                                            <td class="rightCellCss">
                                                <asp:Label ID="lblEmailId" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="leftCellCss">
                                                Contact No
                                            </td>
                                            <td class="CenterCellCss">
                                                :
                                            </td>
                                            <td class="rightCellCss">
                                                <asp:Label ID="lblContactNo" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3">
                                                &nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <img src="images/spacer.gif" width="2" height="5" alt="" />
                            </td>
                        </tr>
                        <tr>
                            <td class="contenct-seperator">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td class="heading-blue">
                                Recce Information
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div class="">
                                    <table cellpadding="1" cellspacing="1" border="0" width="100%">
                                        <tr>
                                            <td colspan="2" class="BigItalicfont">
                                                <asp:HiddenField ID="hdnOutTypeID" runat="server" />
                                                <asp:Label ID="lblMassage" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="60%" valign="top">
                                                <div id="divDate" runat="server">
                                                    <table cellpadding="3" cellspacing="1" border="0" width="100%">
                                                        <tr>
                                                            <td class="leftCellCss cellWidth">
                                                                Recce Date :
                                                            </td>
                                                            <td class="leftCellCss">
                                                                <table cellpadding="0" cellspacing="0" border="0">
                                                                    <tr>
                                                                        <td>
                                                                            <asp:TextBox ID="txtReceeDate" Enabled="false" Width="85" runat="server"></asp:TextBox>
                                                                        </td>
                                                                        <td>
                                                                            <asp:ImageButton runat="Server" ID="imgFrom" ImageUrl="~/images/calendaricon.jpg"
                                                                                AlternateText="Click here to display calendar" />
                                                                            <tk:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtReceeDate"
                                                                                Format="dd/MMM/yyyy" PopupButtonID="imgFrom" />
                                                                            <span id="spantxtReceeDate" class="mandentoryfieldColor" style="display: none">*</span>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </td>
                                            <td width="40%" class="leftCellCss" valign="top" align="left" rowspan="6">
                                                <table cellpadding="3" cellspacing="1" border="0" width="100%">
                                                    <tr>
                                                        <td>
                                                            <div id="cellFileUploder" runat="server" style="display: none;">
                                                                <table cellpadding="1" cellspacing="1" border="0" width="100%">
                                                                    <tr>
                                                                        <td class="leftCellCss">
                                                                            Please select Recce Images (jpg,gif,bmp,jpeg,png,tif)
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="padding-left: 10px;">
                                                                            <div id="divFile">
                                                                                <input type="file" onchange="makeFileList(this);" multiple="" id="fileUImage_1" name="fileUImage_1" />
                                                                                <span id="spanfileUImage_1" class="mandentoryfieldColor" style="display: none">*</span>
                                                                            </div>
                                                                            <ui id="fileList">
                                                                         
                                                                            </ui>
                                                                        </td>
                                                                        <td id="hyperAdd" style="display: none;">
                                                                            <input type="hidden" id="hdnImgCount" value="1" />
                                                                            <a href='javascript:addImg();'>Add More Image</a>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td id="CellImg" runat="server">
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="50%" valign="top">
                                                <table cellpadding="3" cellspacing="1" border="0" width="100%">
                                                    <tr>
                                                        <td colspan="2">
                                                        <asp:HiddenField ID="hdnIsAddress" runat="server" Value="False" />
                                                            <div id="divOutletAddress" runat="server" style="display: none">
                                                                <table cellpadding="3" cellspacing="1" border="0" width="100%">
                                                                    <tr>
                                                                        <td class="leftCellCss cellWidth">
                                                                            Outlet Address :
                                                                        </td>
                                                                        <td class="leftCellCss">
                                                                            <asp:TextBox ID="txtOutletAddress" CssClass="textarea" runat="server" TextMode="MultiLine"
                                                                                MaxLength="500"></asp:TextBox>
                                                                            <span id="spanOutletAddress" class="mandentoryfieldColor" style="display: none">*</span>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="leftCellCss cellWidth">
                                                            Recce User :
                                                        </td>
                                                        <td class="leftCellCss">
                                                            <asp:DropDownList ID="drpReceePerson" Width="124" DataValueField="ID" DataTextField="Text"
                                                                runat="server">
                                                            </asp:DropDownList>
                                                            <span id="spanReceePerson" class="mandentoryfieldColor" style="display: none">*</span>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="leftCellCss cellWidth">
                                                            Surveyor Name :
                                                        </td>
                                                        <td class="leftCellCss">
                                                            <asp:TextBox ID="txtSurveyorName" runat="server" Text="" MaxLength="100" CssClass="mid-textbox"></asp:TextBox>
                                                            <span id="spanTxtSurveyorName" class="mandentoryfieldColor" style="display: none">*</span>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="leftCellCss cellWidth">
                                                            Surveyor Mobile Phone :
                                                        </td>
                                                        <td class="leftCellCss">
                                                            <asp:TextBox ID="txtSurveyorPhoneNo" runat="server" Text="" MaxLength="10" CssClass="mid-textbox"></asp:TextBox>
                                                            <span id="spanTxtSurveyorPhoneNo" class="mandentoryfieldColor" style="display: none">
                                                                *</span>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="leftCellCss cellWidth">
                                                            Sony Area Manager Name :
                                                        </td>
                                                        <td class="leftCellCss">
                                                            <asp:TextBox ID="txtSonyAreMngName" runat="server" Text="" MaxLength="100" CssClass="mid-textbox"></asp:TextBox>
                                                            <span id="span1" class="mandentoryfieldColor" style="display: none">*</span>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="leftCellCss cellWidth">
                                                            Sony Area Manager Phone :
                                                        </td>
                                                        <td class="leftCellCss">
                                                            <asp:TextBox ID="txtSonyAreMngPhone" runat="server" Text="" MaxLength="10" CssClass="mid-textbox"></asp:TextBox>
                                                            <span id="span2" class="mandentoryfieldColor" style="display: none">*</span>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="50%" valign="top">
                                                <table cellpadding="3" cellspacing="1" border="0" width="100%">
                                                    <tr>
                                                        <td class="leftCellCss cellWidth">
                                                            Recce Status :
                                                        </td>
                                                        <td class="leftCellCss">
                                                            <asp:DropDownList ID="drpReceeStatus" DataValueField="ID" Width="124" DataTextField="Text"
                                                                runat="server">
                                                            </asp:DropDownList>
                                                            <span id="spanDrpReceeStatus" class="mandentoryfieldColor" style="display: none">*</span>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <img src="images/spacer.gif" width="2" height="5" alt="" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="contenct-seperator-small">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="50%" id="tdReceeElement" runat="server" style="display: none;" valign="top">
                                                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                                    <tr>
                                                        <td valign="top" align="left">
                                                            <table cellpadding="3" cellspacing="0" border="0" width="100%">
                                                                <tr>
                                                                    <td class="leftCellCss cellWidth">
                                                                        Dealer Name :
                                                                    </td>
                                                                    <td class="leftCellCss">
                                                                        <asp:TextBox ID="txtDealerName" runat="server" Text="" MaxLength="100" CssClass="mid-textbox"></asp:TextBox>
                                                                        <span id="spanTxtDealerName" class="mandentoryfieldColor" style="display: none">*</span>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="leftCellCss cellWidth">
                                                                        Dealer Mobile Phone :
                                                                    </td>
                                                                    <td class="leftCellCss">
                                                                        <asp:TextBox ID="txtDealerPhone" runat="server" Text="" MaxLength="10" CssClass="mid-textbox"></asp:TextBox>
                                                                        <span id="spanTxtDealerPhone" class="mandentoryfieldColor" style="display: none">*</span>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td valign="top" align="left">
                                                            <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                                                <tr>
                                                                    <td>
                                                                        <table cellpadding="3" cellspacing="0" border="0" width="100%">
                                                                            <tr>
                                                                                <td colspan="2" class="leftCellCss cellWidth">
                                                                                    Power Supply Details
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="leftCellCss cellWidth">
                                                                                    No of Points Required:
                                                                                </td>
                                                                                <td class="leftCellCss">
                                                                                    <asp:TextBox ID="txtPSDPointRequired" runat="server" Text="" MaxLength="2" CssClass="small-textbox"></asp:TextBox>
                                                                                    <span id="spanNoOfPointsRequired" class="mandentoryfieldColor" style="display: none">
                                                                                        *</span>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="leftCellCss cellWidth">
                                                                                    No of Points Available:
                                                                                </td>
                                                                                <td class="leftCellCss">
                                                                                    <asp:TextBox ID="txtPSDAvailable" runat="server" Text="" MaxLength="2" CssClass="small-textbox"></asp:TextBox>
                                                                                    <span id="spanNoOfPointsAvailable" class="mandentoryfieldColor" style="display: none">
                                                                                        *</span>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="leftCellCss cellWidth">
                                                                                    Extensions/ Cable work required:
                                                                                </td>
                                                                                <td>
                                                                                    <asp:DropDownList ID="drpExtensions" runat="server">
                                                                                        <asp:ListItem Value="-1" Text="-Select-" Selected="True"> </asp:ListItem>
                                                                                        <asp:ListItem Value="1" Text="Yes"> </asp:ListItem>
                                                                                        <asp:ListItem Value="0" Text="No"> </asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                    <span id="spanExtensions" class="mandentoryfieldColor" style="display: none">*</span>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="2">
                                                                                    <img src="images/spacer.gif" width="2" height="5" alt="" />
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="contenct-seperator-small" colspan="2">
                                                                                    &nbsp;
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <table cellpadding="3" cellspacing="0" border="0" width="100%">
                                                                            <tr>
                                                                                <td class="leftCellCss cellWidth">
                                                                                    Floor leveling Details
                                                                                </td>
                                                                                <td>
                                                                                    <div id="divFloor" runat="server">
                                                                                        <asp:DropDownList ID="drpFloorLevelingDetails" runat="server">
                                                                                            <asp:ListItem Value="0" Text="-Select-" Selected="True"> </asp:ListItem>
                                                                                            <asp:ListItem Value="Flat" Text="Flat"> </asp:ListItem>
                                                                                            <asp:ListItem Value="Uneven" Text="Uneven"> </asp:ListItem>
                                                                                        </asp:DropDownList>
                                                                                        <span id="spanFloorLevelingDetails" class="mandentoryfieldColor" style="display: none">
                                                                                            *</span>
                                                                                    </div>
                                                                                    <asp:Label ID="lblFloorLevelingDetails" runat="server"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="2">
                                                                                    <img src="images/spacer.gif" width="2" height="5" alt="" />
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="contenct-seperator-small" colspan="2">
                                                                                    &nbsp;
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <table cellpadding="3" cellspacing="0" border="0" width="100%">
                                                                            <tr>
                                                                                <td colspan="2" class="leftCellCss cellWidth">
                                                                                    Delivery Truck Reach
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="2">
                                                                                    <div id="divTruck" runat="server">
                                                                                        <table cellpadding="0" cellspacing="0" border="0">
                                                                                            <tr>
                                                                                                <td class="InnerCellWidth">
                                                                                                    Can it reach the shop door step:
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:DropDownList ID="drpDeliveryTruckReach" runat="server">
                                                                                                        <asp:ListItem Value="-1" Text="-Select-" Selected="True"> </asp:ListItem>
                                                                                                        <asp:ListItem Value="1" Text="Yes"> </asp:ListItem>
                                                                                                        <asp:ListItem Value="0" Text="No"> </asp:ListItem>
                                                                                                    </asp:DropDownList>
                                                                                                    <span id="spanDrpDeliveryTruckReach" class="mandentoryfieldColor" style="display: none">
                                                                                                        *</span>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </div>
                                                                                    <asp:Label ID="lblTruckLevel" runat="server" Text=""></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr style="display: none" id="trTruckDistance">
                                                                                <td class="leftCellCss cellWidth">
                                                                                    Distance from Truck to shop:
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtTruckDistance" runat="server" Text="" MaxLength="4" CssClass="small-textbox"></asp:TextBox>
                                                                                    <span id="spanTxtTruckDistance" class="mandentoryfieldColor" style="display: none">*</span>&nbsp;(In
                                                                                    Meters) 1000 Meters = 1KM
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="2">
                                                                                    <img src="images/spacer.gif" width="2" height="5" alt="" />
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="contenct-seperator-small" colspan="2">
                                                                                    &nbsp;
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <table cellpadding="3" cellspacing="0" border="0" width="100%">
                                                                            <tr>
                                                                                <td class="leftCellCss cellWidth">
                                                                                    Shop Timings (Opening / Closing):
                                                                                </td>
                                                                                <td>
                                                                                    <div id="divShopTiming" runat="server">
                                                                                        <table cellpadding="0" cellspacing="0" border="0">
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <asp:DropDownList ID="drpShopTimingsFromHours" runat="server">
                                                                                                        <asp:ListItem Value="-1" Text="From Hour" Selected="True"> </asp:ListItem>
                                                                                                        <asp:ListItem Value="00" Text="00"> </asp:ListItem>
                                                                                                        <asp:ListItem Value="01" Text="01"> </asp:ListItem>
                                                                                                        <asp:ListItem Value="02" Text="02"> </asp:ListItem>
                                                                                                        <asp:ListItem Value="03" Text="03"> </asp:ListItem>
                                                                                                        <asp:ListItem Value="04" Text="04"> </asp:ListItem>
                                                                                                        <asp:ListItem Value="05" Text="05"> </asp:ListItem>
                                                                                                        <asp:ListItem Value="06" Text="06"> </asp:ListItem>
                                                                                                        <asp:ListItem Value="07" Text="07"> </asp:ListItem>
                                                                                                        <asp:ListItem Value="08" Text="08"> </asp:ListItem>
                                                                                                        <asp:ListItem Value="09" Text="09"> </asp:ListItem>
                                                                                                        <asp:ListItem Value="10" Text="10"> </asp:ListItem>
                                                                                                        <asp:ListItem Value="11" Text="11"> </asp:ListItem>
                                                                                                        <asp:ListItem Value="12" Text="12"> </asp:ListItem>
                                                                                                        <asp:ListItem Value="13" Text="13"> </asp:ListItem>
                                                                                                        <asp:ListItem Value="14" Text="14"> </asp:ListItem>
                                                                                                        <asp:ListItem Value="15" Text="15"> </asp:ListItem>
                                                                                                        <asp:ListItem Value="16" Text="16"> </asp:ListItem>
                                                                                                        <asp:ListItem Value="14" Text="14"> </asp:ListItem>
                                                                                                        <asp:ListItem Value="15" Text="15"> </asp:ListItem>
                                                                                                        <asp:ListItem Value="16" Text="16"> </asp:ListItem>
                                                                                                        <asp:ListItem Value="17" Text="17"> </asp:ListItem>
                                                                                                        <asp:ListItem Value="18" Text="18"> </asp:ListItem>
                                                                                                        <asp:ListItem Value="19" Text="19"> </asp:ListItem>
                                                                                                        <asp:ListItem Value="20" Text="20"> </asp:ListItem>
                                                                                                        <asp:ListItem Value="21" Text="21"> </asp:ListItem>
                                                                                                        <asp:ListItem Value="22" Text="22"> </asp:ListItem>
                                                                                                        <asp:ListItem Value="23" Text="23"> </asp:ListItem>
                                                                                                    </asp:DropDownList>
                                                                                                    <span id="spanDrpShopTimingsFromHours" class="mandentoryfieldColor" style="display: none">
                                                                                                        *</span>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:DropDownList ID="drpShopTimingsFromMin" runat="server">
                                                                                                        <asp:ListItem Value="00" Text="00" Selected="True"> </asp:ListItem>
                                                                                                        <asp:ListItem Value="10" Text="10"> </asp:ListItem>
                                                                                                        <asp:ListItem Value="20" Text="20"> </asp:ListItem>
                                                                                                        <asp:ListItem Value="30" Text="30"> </asp:ListItem>
                                                                                                        <asp:ListItem Value="40" Text="40"> </asp:ListItem>
                                                                                                        <asp:ListItem Value="50" Text="50"> </asp:ListItem>
                                                                                                    </asp:DropDownList>
                                                                                                </td>
                                                                                                <td>
                                                                                                    &nbsp;
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:DropDownList ID="drpShopTimingsToHours" runat="server">
                                                                                                        <asp:ListItem Value="-1" Text="To Hour" Selected="True"> </asp:ListItem>
                                                                                                        <asp:ListItem Value="00" Text="00"> </asp:ListItem>
                                                                                                        <asp:ListItem Value="01" Text="01"> </asp:ListItem>
                                                                                                        <asp:ListItem Value="02" Text="02"> </asp:ListItem>
                                                                                                        <asp:ListItem Value="03" Text="03"> </asp:ListItem>
                                                                                                        <asp:ListItem Value="04" Text="04"> </asp:ListItem>
                                                                                                        <asp:ListItem Value="05" Text="05"> </asp:ListItem>
                                                                                                        <asp:ListItem Value="06" Text="06"> </asp:ListItem>
                                                                                                        <asp:ListItem Value="07" Text="07"> </asp:ListItem>
                                                                                                        <asp:ListItem Value="08" Text="08"> </asp:ListItem>
                                                                                                        <asp:ListItem Value="09" Text="09"> </asp:ListItem>
                                                                                                        <asp:ListItem Value="10" Text="10"> </asp:ListItem>
                                                                                                        <asp:ListItem Value="11" Text="11"> </asp:ListItem>
                                                                                                        <asp:ListItem Value="12" Text="12"> </asp:ListItem>
                                                                                                        <asp:ListItem Value="13" Text="13"> </asp:ListItem>
                                                                                                        <asp:ListItem Value="14" Text="14"> </asp:ListItem>
                                                                                                        <asp:ListItem Value="15" Text="15"> </asp:ListItem>
                                                                                                        <asp:ListItem Value="16" Text="16"> </asp:ListItem>
                                                                                                        <asp:ListItem Value="17" Text="17"> </asp:ListItem>
                                                                                                        <asp:ListItem Value="18" Text="18"> </asp:ListItem>
                                                                                                        <asp:ListItem Value="19" Text="19"> </asp:ListItem>
                                                                                                        <asp:ListItem Value="20" Text="20"> </asp:ListItem>
                                                                                                        <asp:ListItem Value="21" Text="21"> </asp:ListItem>
                                                                                                        <asp:ListItem Value="22" Text="22"> </asp:ListItem>
                                                                                                        <asp:ListItem Value="23" Text="23"> </asp:ListItem>
                                                                                                    </asp:DropDownList>
                                                                                                    <span id="spanDrpShopTimingsToHours" class="mandentoryfieldColor" style="display: none">
                                                                                                        *</span>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:DropDownList ID="drpShopTimingsToMin" runat="server">
                                                                                                        <asp:ListItem Value="00" Text="00" Selected="True"> </asp:ListItem>
                                                                                                        <asp:ListItem Value="10" Text="10"> </asp:ListItem>
                                                                                                        <asp:ListItem Value="20" Text="20"> </asp:ListItem>
                                                                                                        <asp:ListItem Value="30" Text="30"> </asp:ListItem>
                                                                                                        <asp:ListItem Value="40" Text="40"> </asp:ListItem>
                                                                                                        <asp:ListItem Value="50" Text="50"> </asp:ListItem>
                                                                                                    </asp:DropDownList>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </div>
                                                                                    <asp:Label ID="lblShopTiming" runat="server"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="2">
                                                                                    <img src="images/spacer.gif" width="2" height="5" alt="" />
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="contenct-seperator-small" colspan="2">
                                                                                    &nbsp;
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <table cellpadding="3" cellspacing="0" border="0" width="100%">
                                                                            <tr>
                                                                                <td class="leftCellCss cellWidth">
                                                                                    Market Weekly Holiday:
                                                                                </td>
                                                                                <td valign="top">
                                                                                    <table cellpadding="1" cellspacing="1" border="0">
                                                                                        <tr>
                                                                                            <td valign="top" align="left">
                                                                                                <asp:RadioButtonList ID="rdbMWH" runat="server">
                                                                                                    <asp:ListItem Value="7 Days Open" Text="7 Days Open"></asp:ListItem>
                                                                                                    <asp:ListItem Value="Sunday" Text="Sunday"></asp:ListItem>
                                                                                                    <asp:ListItem Value="Monday" Text="Monday"></asp:ListItem>
                                                                                                    <asp:ListItem Value="Tuesday" Text="Tuesday"></asp:ListItem>
                                                                                                    <asp:ListItem Value="Wednesday" Text="Wednesday"></asp:ListItem>
                                                                                                    <asp:ListItem Value="Thursday" Text="Thursday"></asp:ListItem>
                                                                                                    <asp:ListItem Value="Friday" Text="Friday"></asp:ListItem>
                                                                                                    <asp:ListItem Value="Saturday" Text="Saturday"></asp:ListItem>
                                                                                                </asp:RadioButtonList>
                                                                                            </td>
                                                                                            <td valign="top" align="left">
                                                                                                <span id="spanRdbMWH" class="mandentoryfieldColor" style="display: none">*</span>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="2">
                                                                                    <img src="images/spacer.gif" width="2" height="5" alt="" />
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="contenct-seperator-small" colspan="2">
                                                                                    &nbsp;
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <table cellpadding="3" cellspacing="0" border="0" width="100%">
                                                                            <tr>
                                                                                <td class="leftCellCss cellWidth">
                                                                                    Shop Owners Preferred time for Installation:
                                                                                </td>
                                                                                <td>
                                                                                    <div id="divShowInstallation" runat="server">
                                                                                        <asp:DropDownList ID="drpInstallation" runat="server">
                                                                                            <asp:ListItem Value="0" Text="-Select-" Selected="True"> </asp:ListItem>
                                                                                            <asp:ListItem Value="Any Time" Text="Any Time"> </asp:ListItem>
                                                                                            <asp:ListItem Value="Timing" Text="Specify Time"> </asp:ListItem>
                                                                                        </asp:DropDownList>
                                                                                        <span id="spanDrpInstallation" class="mandentoryfieldColor" style="display: none">*</span>
                                                                                    </div>
                                                                                    <asp:Label ID="lblShowInstallation" runat="server"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="2">
                                                                                    <div id="divInstallation" style="display: none">
                                                                                        <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                                                                            <tr>
                                                                                                <td class="leftCellCss cellWidth">
                                                                                                    Timing:
                                                                                                </td>
                                                                                                <td>
                                                                                                    <table cellpadding="0" cellspacing="0" border="0">
                                                                                                        <tr>
                                                                                                            <td>
                                                                                                                <asp:DropDownList ID="drpFromHourInstallation" runat="server">
                                                                                                                    <asp:ListItem Value="-1" Text="From Hour" Selected="True"> </asp:ListItem>
                                                                                                                    <asp:ListItem Value="00" Text="00"> </asp:ListItem>
                                                                                                                    <asp:ListItem Value="01" Text="01"> </asp:ListItem>
                                                                                                                    <asp:ListItem Value="02" Text="02"> </asp:ListItem>
                                                                                                                    <asp:ListItem Value="03" Text="03"> </asp:ListItem>
                                                                                                                    <asp:ListItem Value="04" Text="04"> </asp:ListItem>
                                                                                                                    <asp:ListItem Value="05" Text="05"> </asp:ListItem>
                                                                                                                    <asp:ListItem Value="06" Text="06"> </asp:ListItem>
                                                                                                                    <asp:ListItem Value="07" Text="07"> </asp:ListItem>
                                                                                                                    <asp:ListItem Value="08" Text="08"> </asp:ListItem>
                                                                                                                    <asp:ListItem Value="09" Text="09"> </asp:ListItem>
                                                                                                                    <asp:ListItem Value="10" Text="10"> </asp:ListItem>
                                                                                                                    <asp:ListItem Value="11" Text="11"> </asp:ListItem>
                                                                                                                    <asp:ListItem Value="12" Text="12"> </asp:ListItem>
                                                                                                                    <asp:ListItem Value="13" Text="13"> </asp:ListItem>
                                                                                                                    <asp:ListItem Value="14" Text="14"> </asp:ListItem>
                                                                                                                    <asp:ListItem Value="15" Text="15"> </asp:ListItem>
                                                                                                                    <asp:ListItem Value="16" Text="16"> </asp:ListItem>
                                                                                                                    <asp:ListItem Value="14" Text="14"> </asp:ListItem>
                                                                                                                    <asp:ListItem Value="15" Text="15"> </asp:ListItem>
                                                                                                                    <asp:ListItem Value="16" Text="16"> </asp:ListItem>
                                                                                                                    <asp:ListItem Value="17" Text="17"> </asp:ListItem>
                                                                                                                    <asp:ListItem Value="18" Text="18"> </asp:ListItem>
                                                                                                                    <asp:ListItem Value="19" Text="19"> </asp:ListItem>
                                                                                                                    <asp:ListItem Value="20" Text="20"> </asp:ListItem>
                                                                                                                    <asp:ListItem Value="21" Text="21"> </asp:ListItem>
                                                                                                                    <asp:ListItem Value="22" Text="22"> </asp:ListItem>
                                                                                                                    <asp:ListItem Value="23" Text="23"> </asp:ListItem>
                                                                                                                </asp:DropDownList>
                                                                                                                <span id="spanDrpFromHourInstallation" class="mandentoryfieldColor" style="display: none">
                                                                                                                    *</span>
                                                                                                            </td>
                                                                                                            <td>
                                                                                                                <asp:DropDownList ID="drpFromMinInstallation" runat="server">
                                                                                                                    <asp:ListItem Value="00" Text="00" Selected="True"> </asp:ListItem>
                                                                                                                    <asp:ListItem Value="10" Text="10"> </asp:ListItem>
                                                                                                                    <asp:ListItem Value="20" Text="20"> </asp:ListItem>
                                                                                                                    <asp:ListItem Value="30" Text="30"> </asp:ListItem>
                                                                                                                    <asp:ListItem Value="40" Text="40"> </asp:ListItem>
                                                                                                                    <asp:ListItem Value="50" Text="50"> </asp:ListItem>
                                                                                                                </asp:DropDownList>
                                                                                                            </td>
                                                                                                            <td>
                                                                                                                &nbsp;
                                                                                                            </td>
                                                                                                            <td>
                                                                                                                <asp:DropDownList ID="drpToHoursInstallation" runat="server">
                                                                                                                    <asp:ListItem Value="-1" Text="To Hour" Selected="True"> </asp:ListItem>
                                                                                                                    <asp:ListItem Value="00" Text="00"> </asp:ListItem>
                                                                                                                    <asp:ListItem Value="01" Text="01"> </asp:ListItem>
                                                                                                                    <asp:ListItem Value="02" Text="02"> </asp:ListItem>
                                                                                                                    <asp:ListItem Value="03" Text="03"> </asp:ListItem>
                                                                                                                    <asp:ListItem Value="04" Text="04"> </asp:ListItem>
                                                                                                                    <asp:ListItem Value="05" Text="05"> </asp:ListItem>
                                                                                                                    <asp:ListItem Value="06" Text="06"> </asp:ListItem>
                                                                                                                    <asp:ListItem Value="07" Text="07"> </asp:ListItem>
                                                                                                                    <asp:ListItem Value="08" Text="08"> </asp:ListItem>
                                                                                                                    <asp:ListItem Value="09" Text="09"> </asp:ListItem>
                                                                                                                    <asp:ListItem Value="10" Text="10"> </asp:ListItem>
                                                                                                                    <asp:ListItem Value="11" Text="11"> </asp:ListItem>
                                                                                                                    <asp:ListItem Value="12" Text="12"> </asp:ListItem>
                                                                                                                    <asp:ListItem Value="13" Text="13"> </asp:ListItem>
                                                                                                                    <asp:ListItem Value="14" Text="14"> </asp:ListItem>
                                                                                                                    <asp:ListItem Value="15" Text="15"> </asp:ListItem>
                                                                                                                    <asp:ListItem Value="16" Text="16"> </asp:ListItem>
                                                                                                                    <asp:ListItem Value="17" Text="17"> </asp:ListItem>
                                                                                                                    <asp:ListItem Value="18" Text="18"> </asp:ListItem>
                                                                                                                    <asp:ListItem Value="19" Text="19"> </asp:ListItem>
                                                                                                                    <asp:ListItem Value="20" Text="20"> </asp:ListItem>
                                                                                                                    <asp:ListItem Value="21" Text="21"> </asp:ListItem>
                                                                                                                    <asp:ListItem Value="22" Text="22"> </asp:ListItem>
                                                                                                                    <asp:ListItem Value="23" Text="23"> </asp:ListItem>
                                                                                                                </asp:DropDownList>
                                                                                                                <span id="spanDrpToHoursInstallation" class="mandentoryfieldColor" style="display: none">
                                                                                                                    *</span>
                                                                                                            </td>
                                                                                                            <td>
                                                                                                                <asp:DropDownList ID="drpToMinsInstallation" runat="server">
                                                                                                                    <asp:ListItem Value="00" Text="00" Selected="True"> </asp:ListItem>
                                                                                                                    <asp:ListItem Value="10" Text="10"> </asp:ListItem>
                                                                                                                    <asp:ListItem Value="20" Text="20"> </asp:ListItem>
                                                                                                                    <asp:ListItem Value="30" Text="30"> </asp:ListItem>
                                                                                                                    <asp:ListItem Value="40" Text="40"> </asp:ListItem>
                                                                                                                    <asp:ListItem Value="50" Text="50"> </asp:ListItem>
                                                                                                                </asp:DropDownList>
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                    </table>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </div>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="2">
                                                                                    <img src="images/spacer.gif" width="2" height="5" alt="" />
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="contenct-seperator-small" colspan="2">
                                                                                    &nbsp;
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <table cellpadding="3" cellspacing="0" border="0" width="100%">
                                                                            <tr>
                                                                                <td class="leftCellCss cellWidth">
                                                                                    Sony SO to ensure Space Availability by (Date):
                                                                                </td>
                                                                                <td>
                                                                                    <table cellpadding="0" cellspacing="0" border="0">
                                                                                        <tr>
                                                                                            <td>
                                                                                                <asp:TextBox ID="txtSpaceAvailDate" Enabled="false" Width="85" runat="server"></asp:TextBox>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:ImageButton runat="Server" ID="imgSpaceAvailDate" ImageUrl="~/images/calendaricon.jpg"
                                                                                                    AlternateText="Click here to display calendar" />
                                                                                                <tk:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtSpaceAvailDate"
                                                                                                    Format="dd/MMM/yyyy" PopupButtonID="imgSpaceAvailDate" />
                                                                                                &nbsp;<span class="leftCellCss " style="vertical-align: top;">(Optional)</span>
                                                                                                <span id="spanSpaceAvailDate" class="mandentoryfieldColor" style="display: none">*</span>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="2">
                                                                                    <img src="images/spacer.gif" width="2" height="5" alt="" />
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="contenct-seperator-small" colspan="2">
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
                                                        <td valign="top" align="left">
                                                            <asp:UpdatePanel ID="updatedFirst" runat="server" UpdateMode="Conditional">
                                                                <ContentTemplate>
                                                                    <table cellpadding="3" cellspacing="1" border="0" runat="server" id="tbRecceeControl">
                                                                    </table>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table cellpadding="3" cellspacing="1" border="0" width="100%">
                                    <tr>
                                        <td valign="top">
                                            <div id="divSpecialRemarks" runat="server" style="display: none;">
                                                <table cellpadding="1" cellspacing="1" border="0" width="100%">
                                                    <tr>
                                                        <td class="leftCellCss">
                                                            Dealer Remarks
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:TextBox ID="txtSpecialRemarks" runat="server" CssClass="textarea" TextMode="MultiLine"
                                                                MaxLength="500"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </td>
                                        <td>
                                            <div id="divReceePersonRemarks" runat="server" style="display: none;">
                                                <table cellpadding="1" cellspacing="1" border="0" width="100%">
                                                    <tr>
                                                        <td class="leftCellCss">
                                                            Recee Remarks
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td valign="top">
                                                            <asp:TextBox ID="txtReceePersonRemarks" runat="server" CssClass="textarea" TextMode="MultiLine"
                                                                MaxLength="500"></asp:TextBox>
                                                            <span id="spanReceePersonRemarks" class="mandentoryfieldColor" style="display: none">
                                                                *</span>
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
                            <td>
                                <table cellpadding="3" cellspacing="1" border="0" width="100%">
                                    <tr>
                                        <td valign="top">
                                            <div id="divAdminComments" runat="server" style="display: none;">
                                                <table cellpadding="1" cellspacing="1" border="0" width="100%">
                                                    <tr>
                                                        <td class="leftCellCss">
                                                            Admin Comments
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:TextBox ID="txtAdminComments" CssClass="textarea" runat="server" TextMode="MultiLine"
                                                                MaxLength="500"></asp:TextBox>
                                                            <span id="spanAdminComments" class="mandentoryfieldColor" style="display: none">*</span>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </td>
                                        <td>
                                            <div id="divStateManager" runat="server" style="display: none;">
                                                <table cellpadding="1" cellspacing="1" border="0" width="100%">
                                                    <tr>
                                                        <td class="leftCellCss">
                                                            State Manager Comments
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td valign="top">
                                                            <asp:TextBox ID="txtStateMng" runat="server" CssClass="textarea" TextMode="MultiLine"
                                                                MaxLength="500"></asp:TextBox>
                                                            <span id="spanStateMng" class="mandentoryfieldColor" style="display: none">*</span>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="leftCellCss">
                                                            Approved/Reject
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td valign="top">
                                                            <asp:DropDownList ID="drpStateMngApproved" runat="server">
                                                                <asp:ListItem Value="0" Text="-Select-" Selected="True"> </asp:ListItem>
                                                                <asp:ListItem Value="Y" Text="Approved"> </asp:ListItem>
                                                                <asp:ListItem Value="N" Text="Reject"> </asp:ListItem>
                                                            </asp:DropDownList>
                                                            <span id="spanDrpStateMngApproved" class="mandentoryfieldColor" style="display: none">
                                                                *</span>
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
                            <td>
                                <table cellpadding="3" cellspacing="1" border="0" width="100%">
                                    <tr>
                                        <td valign="top">
                                            <div id="divRegionalMngCom" runat="server" style="display: none;">
                                                <table cellpadding="1" cellspacing="1" border="0" width="100%">
                                                    <tr>
                                                        <td class="leftCellCss">
                                                            Regional Manager Comments
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:TextBox ID="txtRegionalMngCom" CssClass="textarea" runat="server" TextMode="MultiLine"
                                                                MaxLength="500"></asp:TextBox>
                                                            <span id="spanRegionalMngCom" class="mandentoryfieldColor" style="display: none">*</span>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="leftCellCss">
                                                            Approved/Reject
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td valign="top">
                                                            <asp:DropDownList ID="drpRegionalmng" runat="server">
                                                                <asp:ListItem Value="0" Text="-Select-" Selected="True"> </asp:ListItem>
                                                                <asp:ListItem Value="Y" Text="Approved"> </asp:ListItem>
                                                                <asp:ListItem Value="N" Text="Reject"> </asp:ListItem>
                                                            </asp:DropDownList>
                                                            <span id="spanDrpRegionalmng" class="mandentoryfieldColor" style="display: none">*</span>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </td>
                                        <td>
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
                        <tr>
                            <td align="left" style="padding-left: 10px;">
                                <asp:ImageButton ID="btnSave" runat="server" Text="Submit" OnClick="btnSave_Click"
                                    ImageUrl="~/images/submit-btn.jpg" />
                                <asp:ImageButton ID="btnStateMng" Visible="false" runat="server" Text="Submit" ImageUrl="~/images/submit-btn.jpg"
                                OnClick="btnStateMng_Click"    /> 
                                <asp:ImageButton ID="btnRegionalMng" Visible="false" runat="server" Text="Submit"
                                    ImageUrl="~/images/submit-btn.jpg" OnClick="btnRegionalMng_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
    </table>
    <script type="text/javascript">

        $(window).scroll(function () {



            var divObj = document.getElementById('divuploading');


            var scrol = (document.all) ? document.body.scrollTop : window.pageYOffset;

            divObj.style.top = scrol + 200 + 'px';

            var divuploading23Obj = document.getElementById('divuploading23');

            divuploading23Obj.style.top = scrol + 200 + 'px';


        });

      
    </script>
</asp:Content>
