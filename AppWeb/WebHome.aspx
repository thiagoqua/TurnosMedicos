<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebHome.aspx.cs" Inherits="AppWeb.WebHome" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <link href="Styles/WebHomesStyle.css" rel="stylesheet" type="text/css" />
    <form id="form1" runat="server">
        <div class="buttonsSection">
            <asp:Image ID="Image1" runat="server" />
            <div>
                <asp:Button ID="verTurnosButton" runat="server" CssClass="button"
                            OnClick="verTurnosButton_Click" Text="Ver mis turnos"/>
            </div>
            <div>
                <asp:Button ID="SolicitarTurnoButton" runat="server" CssClass="button"
                            Text="Solicitar/Eliminar Turnos" PostBackUrl="~/WebTurnos.aspx"/>
            </div>
            <div>
                <asp:Button ID="OtrasOpc" runat="server" CssClass="button"
                            Text="Otras opciones de contacto" OnClick="OtrasOpc_Click" />
            </div>
            <div>
                <asp:Button ID="signOut" runat="server" CssClass="button"
                            Text="Sign out" OnClick="signOutButton_Click" />
            </div>
        </div>
        <div id="addInfo" class="infoSection" runat="server">
            <div style="margin-top: 30px;">
                <asp:Button ID="generatePDF" runat="server" 
                            OnClick="generatePDF_Click" CssClass="PDFgenerator"
                            Text="Generar Reporte" Visible="false"/>
            </div>
            <asp:TextBox ID="textBoxTurno0" runat="server" Height="108px" Width="291px"
                         Enabled="false" Visible="False" ReadOnly="True" 
                         CssClass="main-text-boxes"
                         TextMode="MultiLine" BackColor="#7EABED"></asp:TextBox>
            <asp:TextBox ID="textBoxTurno1" runat="server" Height="108px" Width="291px"
                         Enabled="false" Visible="False" ReadOnly="True"
                         CssClass="main-text-boxes"
                         TextMode="MultiLine" BackColor="#7EABED"></asp:TextBox>
            <div style="text-align:center">
                <div style="margin-bottom:50px;">
                    <asp:Image ID="PHONEicon" runat="server" ImageUrl="~/Resources/PHONE_ICON.png"
                               Width="90px" Height="90px" Visible="False"/>
                    <asp:Label ID="label1" runat="server" Height="24px" Width="368px" Enabled="False"
                               Visible="False" Text="+549 11 45508120" CssClass="labeles"></asp:Label>
                </div>
                <div style="margin-bottom:50px;">
                    <asp:Image ID="MAILicon" runat="server" ImageUrl="~/Resources/MAIL_ICON.png"
                               Width="100px" Height="100px" Visible="False"/>
                    <asp:Label ID="label2" runat="server" Height="24px" Width="368px" Enabled="False"
                               Visible="False" Text="sanatorioparque@appweb.com" CssClass="labeles"></asp:Label>
                </div>
                <div>
                    <asp:Image ID="FCBicon" runat="server" ImageUrl="~/Resources/FCB_ICON.png"
                               Width="100px" Height="100px" Visible="False"/>
                    <asp:Label ID="label3" runat="server" Height="24px" Width="368px" Enabled="False"
                               Visible="False" Text="@sanatorioparque" CssClass="labeles"></asp:Label>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
