<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Ingreso.aspx.cs" Inherits="AppWeb.Ingreso" %>

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
            <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="X-Large" Text="¡Bienvenido al sistema!" Font-Names="Calibri"></asp:Label>
            <br />
            <br />

            <table align="center" style="text-align: left; vertical-align: middle; font-family: Calibri; font-size: large;">
                <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>
            <asp:Button ID="Registrarse_btn" runat="server" Text="Registrarse" class="LoginButton" OnClick="registrarse_btn_Click" Font-Names="Calibri" Font-Size="Large" BackColor="#0033CC" BorderStyle="None" Font-Bold="True" ForeColor="White" Height="40px" Width="100px" />
                    </td>
                    <td>&nbsp;</td>
                             <td>
            <asp:Button ID="Ingresar_btn" runat="server" Text="Ingresar" class="LoginButton" OnClick="ingresar_btn_Click" Font-Names="Calibri" Font-Size="Large" BackColor="#0033CC" BorderStyle="None" Font-Bold="True" ForeColor="White" Height="40px" Width="100px" />
                    </td>
                    <td>&nbsp;</td>
                </tr>
            </table>
            
            <br />
            <br />
            <br />
        </div>
    </form>
</body>
</html>
