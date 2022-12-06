<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebMedicalHome.aspx.cs" Inherits="AppWeb.WebMedicalHome" %>

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
                            Text="Ver turnos" PostBackUrl="~/WebMedicalTurnos.aspx"
                            Width="104px"/>
            </div>
            <div class="button">
                <asp:Button ID="verDisponibilidadButton" runat="server" Height="18px" 
                            Text="Ver disponibilidad" Width="171px"
                            PostBackUrl="~/WebMedicalDisponibility.aspx"/>
            </div>
            <div class="button">
                <input type="submit" value="Sign Out" runat="server" id="signOutButton"/>
            </div>
        </div>
    </form>
</body>
</html>
