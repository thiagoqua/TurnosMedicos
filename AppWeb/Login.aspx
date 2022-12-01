<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="AppWeb.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <h3>
    <font face="Verdana">Inicio de sesión</font>
        </h3>
        <table>
            <tr>
                <td>Email:</td>
                <td><input id="txtUserName" type="text" runat="server"/></td>
                <td><ASP:RequiredFieldValidator ControlToValidate="txtUserName"
                    Display="Static" ErrorMessage="*" runat="server" 
                    ID="vUserName" /></td>
            </tr>
            <tr>
                <td>Contraseña:</td>
                <td><input id="txtUserPass" type="password" runat="server"/></td>
                <td><ASP:RequiredFieldValidator ControlToValidate="txtUserPass"
                Display="Static" ErrorMessage="*" runat="server"
                ID="vUserPass" />
                </td>
            </tr>
            <tr>
                <td>Guardar sesión:</td>
                <td><ASP:CheckBox id="chkPersistCookie" runat="server" autopostback="false" /></td>
                <td></td>
            </tr>
        </table>
        <input type="submit" value="Login" runat="server" id="cmdLogin"/>&nbsp;&nbsp;&nbsp;
        
        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/ForgotPass.aspx">Olvidé mi contraseña</asp:HyperLink>
        <br />
        <br />
        <asp:Label id="lblMsg" ForeColor="red" Font-Name="Verdana" Font-Size="10" runat="server" />
    </form>
</body>
</html>
