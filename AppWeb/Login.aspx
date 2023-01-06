<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="AppWeb.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    
</head>
<body>
    <link href="Styles/LoginStyle.css" rel="stylesheet" type="text/css" />
    <p>
        <br />
    </p>
    <p>
        &nbsp;</p>
    <form id="form1" runat="server" style="vertical-align: middle; text-align: center;">
        <h3 style="font-size: x-large; font-family: Calibri;">
    <font face="Verdana">Inicio de sesión</font>
        </h3>
        <table align="center" style="text-align: left; vertical-align: middle; font-family: Calibri; font-size: large;">
            <tr>
                <td class="auto-style1">Email:</td>
                <td class="auto-style1"><input id="txtUserName" type="text" runat="server" style="font-family: Calibri; font-size: large; width: 260px;"/></td>
                <td class="auto-style1"><ASP:RequiredFieldValidator ControlToValidate="txtUserName"
                    Display="Static" ErrorMessage="*" runat="server" 
                    ID="vUserName" Font-Bold="True" ForeColor="Red" /></td>
            </tr>
            <tr>
                <td>Contraseña:</td>
                <td><input id="txtUserPass" type="password" runat="server" style="font-family: Calibri; font-size: large; width: 260px;"/></td>
                <td><ASP:RequiredFieldValidator ControlToValidate="txtUserPass"
                Display="Static" ErrorMessage="*" runat="server"
                ID="vUserPass" Font-Bold="True" ForeColor="Red" />
                </td>
            </tr>
            <tr>
                <td>Guardar sesión:</td>
                <td><ASP:CheckBox id="chkPersistCookie" runat="server" autopostback="false" /></td>
                <td></td>
            </tr>
        </table>
        <br />
       
        <input type="submit" value="Login" runat="server" id="cmdLogin" class="LoginButton" style="border-style: none; font-family: Calibri; font-size: large; font-weight: bold; font-style: normal; color: #FFFFFF; background-color: #0033CC; width: 100px; height: 40px;"/>&nbsp;&nbsp;&nbsp;
        
        <br />
        <br />
        
        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/ForgotPass.aspx" Font-Bold="True" Font-Names="Calibri" Font-Size="Large">Olvidé mi contraseña</asp:HyperLink>
        <br />
        <br />
        <asp:Label id="lblMsg" ForeColor="Red" Font-Name="Verdana" Font-Size="Large" runat="server" Font-Names="Calibri" Font-Bold="True" />
    </form>
</body>
</html>
