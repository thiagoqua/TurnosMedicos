<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebHome.aspx.cs" Inherits="AppWeb.WebHome" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <link href="Styles/WebHomeStyle.css" rel="stylesheet" type="text/css" />
    <form id="form1" runat="server">
        <div class="buttonsSection">
            <asp:Image ID="Image1" runat="server" />
            <div class="button">
                <asp:Button ID="verTurnosButton" runat="server" Height="21px" 
                            OnClick="verTurnosButton_Click" Text="Ver mis turnos" 
                            Width="104px"/>
            </div>
            <div class="button">
                <asp:Button ID="SolicitarTurnoButton" runat="server" Height="18px" 
                            Text="Solicitar/Eliminar Turnos" Width="171px"
                            PostBackUrl="~/WebTurnos.aspx"/>
            </div>
            <div class="button">
                <asp:Button ID="OtrasOpc" runat="server" Height="21px" 
                            Text="Otras opciones de contacto" Width="175px" 
                            OnClick="OtrasOpc_Click" />
            </div>
            <div class="button">
                <input type="submit" value="Sign Out" runat="server" id="signOutButton"/>
            </div>
        </div>
        <div id="addInfo" class="infoSection" runat="server">
            <div style="margin-top: 30px;">
                <asp:Button ID="generatePDF" runat="server" Height="58px" Width="163px" 
                            OnClick="generatePDF_Click" style="margin-left: 35%;" 
                            Text="Generar Reporte" BackColor="#F55C56" visible="false"/>
            </div>
            <asp:TextBox ID="textBoxTurno0" runat="server" Height="108px" Width="291px"
                         Enabled="false" style="margin-top:30px; margin-left: 40px;" Visible="False"
                         TextMode="MultiLine" ReadOnly="True"></asp:TextBox>
            <asp:TextBox ID="textBoxTurno1" runat="server" Height="108px" Width="291px"
                         Enabled="false" style="margin-top:30px; margin-left: 40px;" Visible="False"
                         TextMode="MultiLine" ReadOnly="True"></asp:TextBox>
            <asp:Label ID="label1" runat="server" Height="24px" Width="368px" Enabled="False"
                       Visible="False" Text="Teléfono: +549 11 45508120"
                       style="margin-left:40px;font-size:x-large;"></asp:Label>
            <p><asp:Label ID="label2" runat="server" Height="24px" Width="368px" Enabled="False"
                       Visible="False" Text="Mail: sanatorioparque@appweb.com"
                       style="margin-left:40px;font-size:x-large;"></asp:Label></p>
            <p><asp:Label ID="label3" runat="server" Height="24px" Width="368px" Enabled="False"
                       Visible="False" Text="Facebook: @sanatorioparque"
                       style="margin-left:40px;font-size:x-large;"></asp:Label></p>
        </div>
    </form>
</body>
</html>
