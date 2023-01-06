<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChangePass.aspx.cs" Inherits="AppWeb.ChangePass" %>

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

            <table align="center" style="text-align: left; vertical-align: middle; font-family: Calibri; font-size: large;">
                <tr>
                    <!--<td>Nueva contraseña:</td>-->
                    <td><asp:Label ID="Label2" runat="server" Text="Nueva contraseña: " Font-Names="Calibri" Font-Size="Large"></asp:Label>
                    </td>
                    <td><asp:TextBox ID="TextBox1" type="password" runat="server" Font-Names="Calibri" Font-Size="Large" Width="200px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <!--<td>Confirmar contraseña:</td>-->
                    <td><asp:Label ID="Label3" runat="server" Text="Confirmar contraseña: " Font-Names="Calibri" Font-Size="Large"></asp:Label>
                    </td>
                    <td><asp:TextBox ID="TextBox2" type="password" runat="server" Font-Names="Calibri" Font-Size="Large" Width="200px"></asp:TextBox>
                    </td>
                </tr>
            </table>
            
            <br />
            <asp:Label id="lblMsg" ForeColor="Red" Font-Name="Verdana" Font-Size="Large" runat="server" Font-Names="Calibri" />
            <br />
            <br />
            <asp:Button ID="Button1" runat="server" Text="Confirmar" class="LoginButton" OnClick="Button1_Click" Font-Names="Calibri" Font-Size="Large" BackColor="#009900" BorderStyle="None" Font-Bold="True" ForeColor="White" Height="40px" Width="100px" />
        </div>
    </form>
</body>
</html>
