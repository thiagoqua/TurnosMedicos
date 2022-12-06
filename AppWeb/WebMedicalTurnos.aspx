<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebMedicalTurnos.aspx.cs" Inherits="AppWeb.WebMedicalTurnos" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <link href="Styles/WebMedicalTurnosStyle.css" rel="stylesheet" type="text/css" />
    <form id="form1" runat="server">
        <div class="buttonsSection">
            <asp:Image ID="Image1" runat="server" />
            <asp:Button ID="backButton" runat="server" Height="109px" Width="180px" 
                        style="margin-left: 35%;font-size:xx-large" CssClass="button"
                        Text="Volver" BackColor="#3366ff" PostBackUrl="~/WebMedicalHome.aspx"/>
            <p><input type="submit" value="Sign Out" runat="server" id="signOutButton" class="button"/></p>
        </div>
        <div id="addInfo" class="infoSection" runat="server">
            <div style="margin-top: 30px;">
                <asp:Button ID="generatePDF" runat="server" Height="58px" Width="163px" 
                            OnClick="generatePDF_Click" style="margin-left: 35%;" 
                            Text="Generar Reporte" BackColor="#F55C56" visible="false"/>
                <asp:Label ID="label1" runat="server" Height="24px" Enabled="False"
                       Visible="True" Text="Seleccione fecha"
                       style="margin-left:40px;"></asp:Label>
                <asp:TextBox ID="fecha" runat="server" Enabled="True" Visible="True"
                             style="margin-top:30px; margin-left: 40px;" TextMode="Date" 
                             OnTextChanged="fecha_TextChanged" AutoPostBack="true"></asp:TextBox>
            </div>
            <asp:TextBox ID="textBoxTurno0" runat="server" Height="108px" Width="291px"
                         Enabled="false" style="margin-top:30px; margin-left: 40px;" Visible="False"
                         TextMode="MultiLine" ReadOnly="True"></asp:TextBox>
            <asp:TextBox ID="textBoxTurno1" runat="server" Height="108px" Width="291px"
                         Enabled="false" style="margin-top:30px; margin-left: 40px;" Visible="False"
                         TextMode="MultiLine" ReadOnly="True"></asp:TextBox>
        </div>
    </form>
</body>
</html>