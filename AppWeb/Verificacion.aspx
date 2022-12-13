<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Verificacion.aspx.cs" Inherits="AppWeb.Verificacion" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form2" runat="server">
        <div>
            <asp:Label ID="Label1" runat="server" Text="Ingrese los siguientes datos para verificarlo en el sistema"></asp:Label>
            <div>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;

            </div>
        </div>
        <asp:Label ID="dni_lbl" runat="server" BorderStyle="None" Text="DNI: "></asp:Label>
        <asp:TextBox ID="Dni" runat="server"></asp:TextBox>
        <p>
            <asp:Label ID="obrasocial_lbl" runat="server" BorderStyle="None" Text="Obra social: "></asp:Label>
            <asp:DropDownList ID="obrasocial_combo" runat="server" OnSelectedIndexChanged="obrasocial_combo_SelectedIndexChanged" AutoPostBack="True">
            </asp:DropDownList>
        </p>
        <p>
            <asp:Label ID="plan_lbl" runat="server" BorderStyle="None" Text="Plan: "></asp:Label>
            <asp:DropDownList ID="plan_combo" runat="server" OnSelectedIndexChanged="plan_combo_SelectedIndexChanged">
            </asp:DropDownList>
        </p>
        <asp:Label ID="lbl_nroafiliado" runat="server" BorderStyle="None" Text="Nro Afiliado: "></asp:Label>
        <asp:TextBox ID="nroafiliado" runat="server"></asp:TextBox>
        <p>
            &nbsp;</p>
        <div style="width: 312px">
            &nbsp;&nbsp;&nbsp;<asp:HyperLink ID="HyperLink_volver" runat="server" NavigateUrl="~/Ingreso.aspx">Volver</asp:HyperLink>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            
            <asp:Button ID="btn_Verificar" runat="server" OnClick="Button1_Click" Text="Verificar" />
            
        </div>
        <p>
        <asp:Label id="lblMsg" ForeColor="red" Font-Name="Verdana" Font-Size="10" runat="server" />
        </p>
    </form>
</body>
</html>
