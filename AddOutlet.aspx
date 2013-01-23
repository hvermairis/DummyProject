<%@ Page Title="" Language="C#" MasterPageFile="~/UserFrontViewMaster.master" AutoEventWireup="true"
    CodeFile="AddOutlet.aspx.cs" Inherits="AddOutlet" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="tk" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        window.history.forward(1);
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="100%" border="0" align="left" cellpadding="0" cellspacing="0">
        <tr>
            <td valign="top">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td valign="top" align="left" class="heading-blue">
                Add Sony Outlet
            </td>
        </tr>
        <tr>
            <td valign="top" style="height: 25px;" align="right">
            </td>
        </tr>
        <tr>
            <td valign="top" align="left">
                <div id="divCityArea" runat="server">
                    <table width="40%" border="0" align="left" cellpadding="0" cellspacing="0">
                        <tr>
                            <td valign="top">
                                <asp:Label ID="lblMsg" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
                                Region
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <asp:DropDownList runat="server" ID="drpRegion" DataTextField="Text" DataValueField="ID"
                                    OnSelectedIndexChanged="drpRegion_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <div id="divState" style="display: none;" runat="server">
                                    <table width="100%" border="0" align="left" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td valign="top">
                                                State
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top">
                                                <asp:DropDownList runat="server" ID="drpState" DataTextField="Text" DataValueField="ID"
                                                    OnSelectedIndexChanged="drpState_SelectedIndexChanged" AutoPostBack="true">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <div id="divCity" style="display: none;" runat="server">
                                    <table width="100%" border="0" align="left" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td valign="top">
                                                City
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top">
                                                <div style="overflow: auto; height: 250px;">
                                                    <asp:RadioButtonList runat="server" RepeatDirection="Vertical" ID="rbListCity" DataTextField="Text"
                                                        DataValueField="ID" AutoPostBack="true" OnSelectedIndexChanged="rbListCity_SelectedIndexChanged">
                                                    </asp:RadioButtonList>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top">
                                                <div id="divAddCity" runat="server" style="display: none;">
                                                    <table width="100%" border="0" align="left" cellpadding="0" cellspacing="0">
                                                        <tr>
                                                            <td valign="top">
                                                                CityName:
                                                            </td>
                                                            <td valign="top">
                                                                <asp:TextBox ID="txtCityName" runat="server" Text="" MaxLength="100" CssClass="mid-textbox"></asp:TextBox>
                                                                <span id="spanCityName" class="mandentoryfieldColor" style="display: none">*</span>
                                                            </td>
                                                            <td valign="top">
                                                                <asp:LinkButton ID="linkCityAdd" runat="server" Text="Add City" OnClick="linkCityAdd_Click"></asp:LinkButton>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top">
                                                <asp:Label ID="lblMsgCityName" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top">
                                                <asp:Button ID="btnInsertCity" Visible="false" Text="Continue >>" runat="server"
                                                    OnClick="btnInsertCity_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
                <div id="divInsertCity" runat="server" style="display: none;">
                    <table width="70%" border="0" align="left" cellpadding="0" cellspacing="0">
                        <tr>
                            <td valign="top">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <table cellpadding="3" cellspacing="1" border="0">
                                    <tr>
                                        <td nowrap="nowrap" valign="top">
                                            Enter Outlet Name
                                        </td>
                                        <td valign="top">
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <asp:TextBox ID="txtOutletName" runat="server" CssClass="textbox"></asp:TextBox>
                                                    <tk:TextBoxWatermarkExtender ID="txtOutletId_TextBoxWatermarkExtender" WatermarkText="Enter Counter Name"
                                                        runat="server" Enabled="True" TargetControlID="txtOutletName">
                                                    </tk:TextBoxWatermarkExtender>
                                                    &nbsp;<span id="processing" style="visibility: hidden;"></span>
                                                    <tk:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" MinimumPrefixLength="1"
                                                        CompletionInterval="1000" EnableCaching="true" CompletionSetCount="20" ServiceMethod="GetList"
                                                        CompletionListCssClass="completionList" CompletionListHighlightedItemCssClass="itemHighlighted"
                                                        CompletionListItemCssClass="listItem" OnClientPopulating="ShowIcon" OnClientPopulated="ShowIcon"
                                                        TargetControlID="txtOutletName" BehaviorID="AutoCompleteEx" OnClientItemSelected="getCode">
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
                                            <span id="spanOutletName" class="mandentoryfieldColor" style="display: none">*</span>
                                        </td>
                                        <td valign="top">
                                            <asp:ImageButton ID="btnSearch" runat="server" Text="Search" ImageUrl="~/images/main-content/search-btn.jpg"
                                                OnClick="btnSearch_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <div id="divCityGrid" runat="server" style="display: none;">
                                    <table width="100%" border="0" align="left" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td>
                                                <div style="overflow: auto; height: 150px">
                                                    <asp:GridView ID="gridOutletDetail" CellPadding="4" Width="100%" AutoGenerateColumns="true"
                                                        runat="server" CssClass="gridCssNew" GridLines="None" EmptyDataText="No City Found">
                                                    </asp:GridView>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <table width="40%" border="0" align="left" cellpadding="3" cellspacing="1">
                                                    <tr>
                                                        <td colspan="2" class="heading-blue">
                                                            Insert Outlet Details
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            <asp:Label ID="lblOutletInsertMng" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="leftCellCss">
                                                            OulterName:
                                                        </td>
                                                        <td class="rightCellCss">
                                                            <asp:TextBox ID="txtNewOutletName" runat="server" Text="" MaxLength="100" CssClass="mid-textbox"></asp:TextBox>
                                                            <span id="spanNewOutletName" class="mandentoryfieldColor" style="display: none">*</span>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="leftCellCss">
                                                            OutletType:
                                                        </td>
                                                        <td class="rightCellCss">
                                                            <asp:DropDownList ID="drpOutletType" Width="124" DataValueField="ID" DataTextField="Text"
                                                                runat="server">
                                                            </asp:DropDownList>
                                                            <span id="spanOutletType" class="mandentoryfieldColor" style="display: none">*</span>
                                                        </td>
                                                    </tr>
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
                                                    <tr>
                                                        <td class="leftCellCss cellWidth">
                                                            Retailer Name :
                                                        </td>
                                                        <td class="leftCellCss">
                                                            <asp:TextBox ID="txtRetailerName" CssClass="mid-textbox" runat="server" TextMode="MultiLine"
                                                                MaxLength="100"></asp:TextBox>
                                                            <span id="spanRetailerName" class="mandentoryfieldColor" style="display: none">*</span>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="leftCellCss cellWidth">
                                                            Retailer Phone No :
                                                        </td>
                                                        <td class="leftCellCss">
                                                            <asp:TextBox ID="txtRetailerContactNo" CssClass="mid-textbox" runat="server" MaxLength="15"></asp:TextBox>
                                                            <span id="span1" class="mandentoryfieldColor" style="display: none">*</span>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" align="left" style="padding-left: 10px;">
                                                            <asp:ImageButton ID="btnSave" runat="server" Text="Submit" OnClick="btnSave_Click"
                                                                ImageUrl="~/images/submit-btn.jpg" />
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
    </table>
</asp:Content>
