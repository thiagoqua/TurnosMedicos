<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Registro.aspx.cs" Inherits="AppWeb.Registro" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
        </div>
        <asp:Label ID="Label1" runat="server" Text="Ingrese una contraseña para su cuenta a continuacion."></asp:Label>
        <p>
            <asp:Label ID="Label2" runat="server" Text="Contraseña: "></asp:Label>
            <asp:TextBox ID="contraseña" runat="server" TextMode="Password"></asp:TextBox>
        </p>
        <asp:Label ID="Label3" runat="server" Text="Repetir contraseña: "></asp:Label>
        <asp:TextBox ID="rep_contraseña" runat="server" TextMode="Password"></asp:TextBox>
        <p>
            <asp:Button ID="btn_registrarse" runat="server" OnClick="btn_registrarse_Click" Text="Registrarse" />
        </p>
    </form>
    <p>
        &nbsp;</p>
</body>
</html>
