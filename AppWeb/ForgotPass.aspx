<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ForgotPass.aspx.cs" Inherits="AppWeb.ForgotPass" %>

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
    <form id="form1" runat="server">
        <div style="vertical-align: middle; text-align: center">
            <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="X-Large" Text="Cambiar contraseña" Font-Names="Calibri"></asp:Label>
            <br />
            <br />
            &nbsp;<asp:Label ID="Label2" runat="server" Font-Names="Calibri" Font-Size="Large" Text="Ingrese su correo electrónico: "></asp:Label>
            <asp:TextBox ID="TextBox1" runat="server" Width="260px" Font-Names="Calibri" Font-Size="Large"></asp:TextBox>
            &nbsp;<br />
            <asp:Label id="lblMsg" ForeColor="red" Font-Name="Verdana" Font-Size="10" runat="server" />
            <br />
            <br />
            <asp:Button ID="Button1" class="LoginButton" runat="server" OnClick="Button1_Click" Text="Enviar" Font-Bold="True" Font-Names="Calibri" Font-Size="Large" Height="40px" Width="100px" BackColor="#0033CC" BorderStyle="None" ForeColor="White" />
            <br />
            <br />
&nbsp;<asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Login.aspx" Font-Bold="True" Font-Names="Calibri" Font-Size="Large">Volver al inicio</asp:HyperLink>
        </div>
    </form>
</body>
</html>
