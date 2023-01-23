<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Registro.aspx.cs" Inherits="AppWeb.Registro" %>

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
        <div style="vertical-align: middle; text-align: center; height: 259px;">
            <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="X-Large" Text="Ingrese una contraseña para su cuenta" Font-Names="Calibri"></asp:Label>
            <br />
            <br />

            <table align="center" style="text-align: left; vertical-align: middle; font-family: Calibri; font-size: large;">
                <tr>
                    <td><asp:Label ID="contraseña_lbl" runat="server" Text="Contraseña: " Font-Names="Calibri" Font-Size="Large"></asp:Label>
                    </td>
                    <td>
                        <input id="contraseña" type="password" runat="server" style="font-family: Calibri; font-size: large; width: 260px;"/>
                    </td>
                </tr>
                  
                <tr>
                    <td><asp:Label ID="Label5" runat="server" Text="Repetir contraseña: " Font-Names="Calibri" Font-Size="Large"></asp:Label>
                    </td>
                    <td>
                        <input id="rep_contraseña" type="password" runat="server" style="font-family: Calibri; font-size: large; width: 260px;"/>
                    </td>
                </tr>
            </table>
            <br />
            <br />
            <br />

            <tr>
                <td>
                </td>
                <td>&nbsp;</td>
                <td>
                    <asp:Button ID="btn_registrarse" runat="server" Text="Registrarse" class="LoginButton" OnClick="btn_registrarse_Click" Font-Names="Calibri" Font-Size="Large" BackColor="#0033CC" BorderStyle="None" Font-Bold="True" ForeColor="White" Height="40px" Width="100px" />
                </td>
                    <td>&nbsp;
                </td>
            </tr>
        </div>
    </form>
</body>
</html>
