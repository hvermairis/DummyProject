<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RotateImage.aspx.cs" Inherits="RotateImage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Image Preview</title>
     
<script type="text/javascript" src="js/jquery-1.3.1.js"></script>
<script type="text/javascript" src="js/jquery.rotate.1-1.js"></script>
<script language="javascript" type="text/javascript">
function RotateImages(angle)
{

$('#image').rotate(angle)
}
</script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
      <table cellpadding="0" cellspacing="0" border="0" width="100%">
            <tr>
                <td>
                    <a onclick="RotateImages(90);" style="cursor:pointer;" ><img src="images/rotate_rl-icon.png" alt="Rotate image clockwise"></img></a>
                    
                </td>
            </tr>
            <tr>
                <td >
                <div >
                    <img src="<%= ImagePath %>"    alt="" id="image" />
                    <%--<canvas id="canvas"></canvas>--%>
                </div>
                    
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
