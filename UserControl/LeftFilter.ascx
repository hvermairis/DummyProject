<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LeftFilter.ascx.cs" Inherits="UserControl_LeftFilter" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="TK" %>
<style type="text/css">
    .LPanelHeader
    {
        height: 24px;
    }
    .LPanelContent
    {
        /*padding-top:5px;
    	padding-bottom:5px;
    	padding-left:10px;
    	padding-right:3px;*/
        height: 200px;
        overflow: auto;
    }
</style>
<table width="150" border="0" align="center" cellpadding="0" cellspacing="0" class="leftCellCss">
    <tr>
        <td class="leftboxbg">
            <TK:Accordion ID="ACDN" runat="server" Width="100%" HeaderCssClass="AccordionPanelTab LPanelHeader"
                FadeTransitions="true" ContentCssClass="AccordionPanelContent LPanelContent"
                AutoSize="None" SelectedIndex="0" RequireOpenedPane="true" SuppressHeaderPostbacks="true">
                <Panes>
                    <TK:AccordionPane ID="trRegion" runat="server">
                        <Header>
                            &nbsp;&nbsp;&nbsp;&nbsp; Select Region</Header>
                        <Content>
                            <asp:CheckBoxList ID="chkRegion" CssClass="paddingLeft" Width="172" AutoPostBack="false"
                                DataValueField="RegionID" DataTextField="Region" RepeatColumns="1" OnSelectedIndexChanged="chkRegion_SelectedIndexChanged"
                                RepeatDirection="Vertical" runat="server">
                            </asp:CheckBoxList>
                        </Content>
                    </TK:AccordionPane>
                    <TK:AccordionPane ID="trState" runat="server">
                        <Header>
                            &nbsp;&nbsp;&nbsp;&nbsp; Select State</Header>
                        <Content>
                            <asp:CheckBoxList ID="chkState" CssClass="paddingLeft" Width="172" DataValueField="StateID"
                                AutoPostBack="false" DataTextField="State" RepeatColumns="1" RepeatDirection="Vertical"
                                runat="server" OnSelectedIndexChanged="chkState_SelectedIndexChanged">
                            </asp:CheckBoxList>
                        </Content>
                    </TK:AccordionPane>
                    <TK:AccordionPane ID="trCity" runat="server">
                        <Header>
                            &nbsp;&nbsp;&nbsp;&nbsp;Select City</Header>
                        <Content>
                            <asp:CheckBoxList ID="chkCity" CssClass="paddingLeft" Width="172" DataValueField="CityID"
                                DataTextField="City" RepeatColumns="1" RepeatDirection="Vertical" runat="server" />
                        </Content>
                    </TK:AccordionPane>
                    <TK:AccordionPane ID="trOuttypeType" runat="server">
                        <Header>
                            &nbsp;&nbsp;&nbsp;&nbsp;Select Outlet Type</Header>
                        <Content>
                            <asp:RadioButtonList ID="rBlOutletType" CssClass="paddingLeft" Width="172" DataValueField="OutletTypeID"
                                DataTextField="OutletType" RepeatColumns="1" RepeatDirection="Vertical" runat="server" ></asp:RadioButtonList>
                        </Content>
                    </TK:AccordionPane>
                </Panes>
            </TK:Accordion>
        </td>
    </tr>
    <tr>
        <td>
            <asp:HiddenField ID="hdnIsUsercontrol" runat="server" Value="" />
              <asp:HiddenField ID="hdnRegionIds" runat="server" Value="" />
                <asp:HiddenField ID="hdnStateIds" runat="server" Value="" />
            &nbsp;
        </td>
    </tr>
    <tr>
        <td align="right" style="padding-right: 15px;" class="leftboxbg">
            <asp:ImageButton ID="imgBtn" runat="server" ImageUrl="~/images/view-report.jpg" OnClick="imgBtn_Click" />
        </td>
    </tr>
    <tr>
        <td>
            &nbsp;
        </td>
    </tr>
    <tr>
        <td>
            &nbsp;
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


    function checkUncheckCheckBox(CheckBoxId, countLimit) {
        var firstCheckBoxId = CheckBoxId + '_0';

        //alert('firstCheckBoxId='+firstCheckBoxId);
        document.getElementById(firstCheckBoxId).checked = IsCheckCheckBox(CheckBoxId, countLimit);
    }


    function checkUncheckAllCheckBox(reportType, CheckBoxId, countLimit) {
        var firstCheckBoxId = CheckBoxId + '_0';
        var IsSelected = true;
        // alert('firstCheckBoxId='+firstCheckBoxId);
        if (!IsNothingCheckCheckBox(CheckBoxId, countLimit))//reportType , 1=India Level,2 = State Level , 3= City Level 
        {

            if (reportType == 1)
                alert('Please select any region');
            else if (reportType == 2)
                alert('Please select any state');
            else if (reportType == 3)
                alert('Please select any city');

            IsSelected = false;

        }

        return IsSelected;
    }

    function IsNothingCheckCheckBox(CheckBoxId, countLimit) {

        var strcheckBoxId = "";
        strcheckBoxId = CheckBoxId + '_1';

        var countCheck = 0;
        var IsSelected = false;
        for (count = 1; count < countLimit; count++) {
            strcheckBoxId = CheckBoxId + '_' + count;
            // alert(strcheckBoxId);
            if (document.getElementById(strcheckBoxId).checked) {
                countCheck = countCheck + 1;
            }

        }

        if (countCheck > 0) {
            IsSelected = true;
        }

        //alert('IsSelected='+IsSelected);
        return IsSelected;

    }


    function IsCheckCheckBox(CheckBoxId, countLimit) {

        var strcheckBoxId = "";
        strcheckBoxId = CheckBoxId + '_1';

        // alert(strcheckBoxId);
        //alert(document.getElementById(strcheckBoxId));
        //alert(document.getElementById(strcheckBoxId).checked);

        var IsSelected = true;
        for (count = 1; count < countLimit; count++) {
            strcheckBoxId = CheckBoxId + '_' + count;
            // alert(strcheckBoxId);
            if (!document.getElementById(strcheckBoxId).checked) {
                IsSelected = false;
                break;

            }
        }

        return IsSelected;

    }
</script>
