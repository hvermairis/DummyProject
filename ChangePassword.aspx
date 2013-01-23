<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ChangePassword.aspx.cs" Inherits="ChangePassword" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <title>Sony Mobiles</title>
    <link href="css/style.css" rel="stylesheet" type="text/css" />
    <link href="css/devcss.css" rel="stylesheet" type="text/css" />
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
                                <img src="images/sony-logo.png" alt="Sony" width="129" height="44" border="0" style="margin-left: 35px;
                                    margin-top: 35px; display: block;" />
                            </td>
                            <td align="right">
                                <img src="images/iris-logo.png" alt="Sony" border="0" style="margin-right: 35px;
                                    margin-top: 35px; display: block;" />
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
                <td>
                    &nbsp;
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
            <tr>
                <td>
                    <table width="466" border="0" align="center" cellpadding="0" cellspacing="0">
                        <tr>
                            <td class="login-bg">
                                <table width="466" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td colspan="3" align="center">
                                            <img src="images/spacer.gif" width="2" height="10" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td class="heading-blue">
                                            <asp:Label ID="lbmessage" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" align="center">
                                            <img src="images/spacer.gif" width="2" height="10" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="23">
                                            &nbsp;
                                        </td>
                                        <td width="123" valign="middle">
                                            Old Password:
                                           
                                        </td>
                                        <td width="320">
                                            <asp:TextBox ID="txtoldpassword" TextMode="Password" CssClass="textbox" MaxLength="50"
                                                runat="server"></asp:TextBox>
                                                 <asp:RequiredFieldValidator ID="rfvoldpassword" runat="server" ErrorMessage="Please enter the valid Old Password"
                                                ControlToValidate="txtoldpassword" Display="Dynamic">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" align="center">
                                            <img src="images/spacer.gif" width="2" height="10" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td valign="middle">
                                            New Password:
                                           
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtnewpassword" CssClass="textbox" TextMode="Password" MaxLength="50"
                                                runat="server"></asp:TextBox>
                                                 <asp:RequiredFieldValidator ID="rfvnewpassword" runat="server" ErrorMessage="Please enter the valid New Password"
                                                ControlToValidate="txtnewpassword">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" align="center">
                                            <img src="images/spacer.gif" width="2" height="10" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            Retype Password:
                                           
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtretypepassword" TextMode="Password" CssClass="textbox" MaxLength="50"
                                                runat="server"></asp:TextBox>
                                                 <asp:RequiredFieldValidator ID="rfvretypepassword" runat="server" ErrorMessage="Please enter the valid retype password"
                                                ControlToValidate="txtretypepassword">*</asp:RequiredFieldValidator>
                                            <asp:CompareValidator ID="cmpvcompare" runat="server" ErrorMessage="Retype password doesn't match with the new password"
                                                ControlToValidate="txtretypepassword" ControlToCompare="txtnewpassword">*</asp:CompareValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" align="center">
                                            <img src="images/spacer.gif" width="2" height="5" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="btnSubmit" runat="server" Text="Change Password" OnClick="btnsubmit_Click"
                                                ImageUrl="~/images/change-password-btn.jpg" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
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
    </div>
    </form>
</body>
</html>
