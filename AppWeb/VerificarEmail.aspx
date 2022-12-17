<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VerificarEmail.aspx.cs" Inherits="AppWeb.VerificarEmail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="Label1" runat="server" Text="Ingrese el correo electrónico   "></asp:Label>
            <asp:TextBox ID="Email" runat="server"></asp:TextBox>
            <asp:Button ID="btn_Verificar" runat="server" OnClick="btn_Verificar_Click" Text="Verificar" />
        </div>
        <p>
            <asp:Label ID="Label2" runat="server" Text="Ingrese el codigo enviado a su correo   "></asp:Label>
            <asp:TextBox ID="Codigo" runat="server"></asp:TextBox>
        </p>
        <p>
            <asp:Button ID="btn_volver" runat="server" OnClick="btn_volver_Click" Text="Volver" />
            <asp:Button ID="btn_confirmar" runat="server" Enabled="False" OnClick="btn_confirmar_Click" Text="Confirmar" />
        </p>
        <p>
        <asp:Label id="lblMsg" ForeColor="red" Font-Name="Verdana" Font-Size="10" runat="server" />
        </p>
        <p>
            &nbsp;</p>
    </form>
</body>
</html>