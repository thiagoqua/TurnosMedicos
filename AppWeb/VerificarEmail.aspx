<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VerificarEmail.aspx.cs" Inherits="AppWeb.VerificarEmail" %>

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
            
            <table align="center" style="text-align: left; vertical-align: middle; font-family: Calibri; font-size: large;">
                <tr>
                    <td><asp:Label ID="ingrese_lbl" runat="server" Text="Ingrese el correo electronico: " Font-Names="Calibri" Font-Size="Large"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="email" runat="server"></asp:TextBox><asp:Button ID="btn_verificar" runat="server" Text="Verificar" class="LoginButton" OnClick="btn_verificar_Click" Font-Names="Calibri" Font-Size="Large" BackColor="#0033CC" BorderStyle="None" Font-Bold="True" ForeColor="White" Height="25px" Width="100px" />
                
                    </td>
                </tr>
                  
                <tr>
                    <td><asp:Label ID="Label5" runat="server" Text="Ingrese el codigo enviado a su correo: " Font-Names="Calibri" Font-Size="Large"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="codigo" runat="server"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <br />
            <br />
            <br />

            <tr>
                <td>
                    <asp:Button ID="btn_volver" runat="server" Text="Volver" class="LoginButton" OnClick="btn_volver_Click" Font-Names="Calibri" Font-Size="Large" BackColor="#0033CC" BorderStyle="None" Font-Bold="True" ForeColor="White" Height="40px" Width="100px" />
                </td>
                <td>&nbsp;</td>
                <td>
                    <asp:Button ID="btn_confirmar" runat="server" Text="Confirmar" class="LoginButton" OnClick="btn_confirmar_Click" Font-Names="Calibri" Font-Size="Large" BackColor="#0033CC" BorderStyle="None" Font-Bold="True" ForeColor="White" Height="40px" Width="100px" />
                </td>
                    <td>&nbsp;</td>
            </tr>
        </div>
    </form>
</body>
</html>
