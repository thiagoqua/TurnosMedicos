<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Verificacion.aspx.cs" Inherits="AppWeb.Verificacion" %>


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
            <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="X-Large" Text="Ingrese los siguientes datos para verificarlo en el Sistema" Font-Names="Calibri"></asp:Label>
            <br />
            <br />

            <table align="center" style="text-align: left; vertical-align: middle; font-family: Calibri; font-size: large;">
                <tr>
                    <td><asp:Label ID="dni_lbl" runat="server" Text="DNI: " Font-Names="Calibri" Font-Size="Large"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="dni" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td><asp:Label ID="Label3" runat="server" Text="Obra Social: " Font-Names="Calibri" Font-Size="Large"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="obrasocial_combo" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td><asp:Label ID="Label4" runat="server" Text="Plan: " Font-Names="Calibri" Font-Size="Large"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="plan_combo" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td><asp:Label ID="Label5" runat="server" Text="Nro Afiliado: " Font-Names="Calibri" Font-Size="Large"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="nroafiliado" runat="server"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <br />
            <asp:Label id="lblMsg" ForeColor="Red" Font-Name="Verdana" Font-Size="Large" runat="server" Font-Names="Calibri" />
            <br />
            <br />

            <tr>
                <td>
                    <asp:Button ID="volver_btn" runat="server" Text="Volver" class="LoginButton" OnClick="Volver_btn_Click" Font-Names="Calibri" Font-Size="Large" BackColor="#0033CC" BorderStyle="None" Font-Bold="True" ForeColor="White" Height="40px" Width="100px" />
                </td>
                <td>&nbsp;</td>
                <td>
                    <asp:Button ID="Button1" runat="server" Text="Confirmar" class="LoginButton" OnClick="btn_verificar" Font-Names="Calibri" Font-Size="Large" BackColor="#0033CC" BorderStyle="None" Font-Bold="True" ForeColor="White" Height="40px" Width="100px" />
                </td>
                    <td>&nbsp;</td>
            </tr>
        </div>
    </form>
</body>
</html>
