<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebMedicalTurnos.aspx.cs" Inherits="AppWeb.WebMedicalTurnos" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <link href="Styles/WebHomesStyle.css" rel="stylesheet" type="text/css" />
    <form id="form1" runat="server">
        <div class="buttonsSection">
            <div><asp:Image ID="Image1" runat="server" ImageUrl="~/Resources/LOGO.png"
                       Width="200px" style="border-radius: 10px 10px;margin-top:20px;"/></div>
            <asp:Button ID="backButton" runat="server" CssClass="back-button"
                        Text="Volver" PostBackUrl="~/WebMedicalHome.aspx"/>
            <p><input type="submit" value="Sign Out" runat="server" id="signOutButton" class="button"/></p>
        </div>
        <div id="addInfo" class="infoSection" runat="server">
            <div style="margin-top: 30px;margin-bottom:40px;">
                <asp:Button ID="generatePDF" runat="server" CssClass="PDFgenerator"
                            OnClick="generatePDF_Click" 
                            Text="Generar Reporte" visible="false"/>
                <div style="float:right;">
                    <asp:Label ID="label1" runat="server" Enabled="False"
                           Visible="True" Text="Seleccione fecha" 
                           style="margin-left:40px;font-weight:600; font-size:large"></asp:Label>
                    <asp:TextBox ID="fecha" runat="server" Enabled="True" Visible="True"
                                 style="margin-top:30px; margin-left: 40px;" TextMode="Date" 
                                 OnTextChanged="fecha_TextChanged" AutoPostBack="true"></asp:TextBox>
                </div>
            </div>
            <asp:TextBox ID="textBoxTurno0" runat="server" Height="108px" Width="291px"
                         Enabled="false" CssClass="text-boxes" Visible="False"
                         TextMode="MultiLine" ReadOnly="True" BackColor="#7EABED"></asp:TextBox>
            <asp:TextBox ID="textBoxTurno1" runat="server" Height="108px" Width="291px"
                         Enabled="false" CssClass="text-boxes" Visible="False"
                         TextMode="MultiLine" ReadOnly="True" BackColor="#7EABED"></asp:TextBox>
        </div>
    </form>
</body>
</html>