<%@ Control Language="C#" AutoEventWireup="true" CodeFile="RecceElementControl.ascx.cs"
    Inherits="UserControl_RecceElementControl" %>

<table cellpadding="0" cellspacing="1" border="0" width="100%">
    <tr>
        <td class="leftCellCss">
            &nbsp;
        </td>
        <td class="leftCellCss" style="width: 70px;">
            Availability
        </td>
        <td class="leftCellCss">
            <asp:Label ID="lblQuantity" runat="server" Text="Quantity"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="leftCellCss cellWidth">
            <asp:Label ID="lblElementName" runat="server"></asp:Label>
        </td>
        <td class="leftCellCss" width="98px;">
            <asp:DropDownList ID="drpAvaibility" runat="server" AutoPostBack="true" Width="88px"
                OnSelectedIndexChanged="drpAvaibility_SelectedIndexChanged">
                <asp:ListItem Text="Select" Value="-1" Selected="True"></asp:ListItem>
                <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                <asp:ListItem Text="No" Value="0"></asp:ListItem>
            </asp:DropDownList>
            <span id="spandrpAvaibility" runat="server" class="mandentoryfieldColor" style="display:none">*</span>
        </td> 
        <td class="leftCellCss">
            <asp:TextBox ID="txtQuantity" runat="server" Text="" MaxLength="2" CssClass="small-textbox" AutoPostBack="true"
                OnTextChanged="txtQuantity_TextChanged"></asp:TextBox>
                  <span id="spanTxtQuantity" runat="server" class="mandentoryfieldColor" style="display:none">*</span>
        </td>
    </tr>
    <tr>
        <td colspan="3">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td colspan="3">
            <table cellpadding="0" cellspacing="1" border="0" id="tbControl"  runat="server">
            </table>
        </td>
    </tr>
    <tr>
        <td colspan="3">
            <div id="divRemarks" runat="server" style="display: none;">
                <table cellpadding="0" cellspacing="1" border="0" width="100%">
                    <tr>
                        <td class="leftCellCss cellWidth" >
                            Remarks
                        </td>
                        <td align="left" colspan="2">
                            <asp:TextBox ID="txtElementRemarks" runat="server" TextMode="MultiLine" MaxLength="500"
                                CssClass="textarea"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
</table>
<asp:HiddenField ID="hdntxtHeigths"  runat="server" Value=""/>
<asp:HiddenField ID="hdntxtWidths"  runat="server" Value=""/>
<asp:HiddenField ID="hdnstrdrpSurface"  runat="server" Value=""/>
<asp:HiddenField ID="hdnstrtxtSurFaceText"  runat="server" Value=""/>
<asp:HiddenField ID="hdnstrSpanHeight"  runat="server" Value=""/>
<asp:HiddenField ID="hdnstrSpanWidth"  runat="server" Value=""/>
<asp:HiddenField ID="hdnstrSpanDrpSurface"  runat="server" Value=""/>
<asp:HiddenField ID="hdnstrSpanTxtSurFaceText"  runat="server" Value=""/>
<asp:HiddenField ID="hdnstrTxtDepth"  runat="server" Value=""/>
<asp:HiddenField ID="hdnstrSpanDepth"  runat="server" Value=""/>
<asp:Literal ID="lblScript" runat="server"></asp:Literal>