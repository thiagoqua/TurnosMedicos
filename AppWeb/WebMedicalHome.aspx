<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebMedicalHome.aspx.cs" Inherits="AppWeb.WebMedicalHome" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <link href="Styles/WebHomesStyle.css" rel="stylesheet" type="text/css" />
    <form id="form1" runat="server">
        <div class="buttonsSection">
            <asp:Image ID="Image1" runat="server" ImageUrl="~/Resources/LOGO.png"
                       Width="200px" style="border-radius: 10px 10px;margin-top:20px;"/>
            <div>
                <asp:Button ID="verTurnosButton" runat="server" CssClass="button"
                            Text="Ver turnos" PostBackUrl="~/WebMedicalTurnos.aspx"/>
            </div>
            <div>
                <asp:Button ID="verDisponibilidadButton" runat="server" CssClass="button"
                            Text="Ver disponibilidad" PostBackUrl="~/WebMedicalDisponibility.aspx"/>
            </div>
            <div>
                <asp:Button ID="signOut" runat="server" CssClass="button"
                            Text="Sign out" OnClick="signOutButton_Click" />
            </div>
        </div>
    </form>
</body>
</html>
