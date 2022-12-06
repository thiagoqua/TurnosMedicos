<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebMedicalDisponibility.aspx.cs" Inherits="AppWeb.WebMedicalDisponibility" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <link href="Styles/WebMedicalDisponibilityStyle.css" rel="stylesheet" type="text/css" />
    <form id="form1" runat="server">
        <div class="buttonsSection" runat="server">
            <asp:Image ID="Image1" runat="server" />
            <asp:Button ID="backButton" runat="server" Height="109px" Width="180px" 
                        style="margin-left: 35%;font-size:xx-large" CssClass="button"
                        Text="Volver" BackColor="#ccffff" PostBackUrl="~/WebMedicalHome.aspx"/>
        </div>
        <div class="inputSection" runat="server">
            <div class="topSide" runat="server">
                <p><asp:Label ID="Label1" runat="server" Text="Modificar disponibilidad" 
                            Font-Underline="true" ForeColor="OrangeRed" Font-Size="X-Large"
                            style="font-family:Calibri;text-align:center;display:block;"></asp:Label></p>
                <asp:Label ID="Label2" runat="server" Text="Seleccione sucursal" Visible="true"></asp:Label>
                <asp:DropDownList ID="comboSucursales" runat="server" CssClass="dropDowns" 
                                    OnSelectedIndexChanged="comboSucursales_SelectedIndexChanged"
                                    AutoPostBack="true" Visible="true"></asp:DropDownList>
                <asp:Label ID="Label3" runat="server" Text="Seleccione día" Visible="true"></asp:Label>
                <asp:DropDownList ID="comboDias" runat="server" CssClass="dropDowns" 
                                    OnSelectedIndexChanged="comboDias_SelectedIndexChanged"
                                    AutoPostBack="true" Visible="true"></asp:DropDownList>
                <asp:Button ID="abmDyS" runat="server" Text="Modificar sucursales y días" 
                            Height="87px" OnClick="abmDyS_Click" Width="177px"
                            style="margin-left:10%;"/>
                <p>
                    <asp:Label ID="ubicacion" runat="server" Visible="true" 
                               BackColor="#ff9966"></asp:Label>
                </p>
                <p style="margin-left:10px">
                    <asp:Label ID="Label4" runat="server" Text="Horario inicio" Visible="false"></asp:Label>
                    <asp:TextBox ID="textboxHoraInicio" runat="server" CssClass="textBoxes"
                                    Visible="false"></asp:TextBox>
                    <asp:Label ID="Label6" runat="server" Text="Horario finalización" 
                                Visible="false"></asp:Label>
                    <asp:TextBox ID="textboxHoraFin" runat="server" CssClass="textBoxes"
                                    Visible="false"></asp:TextBox>
                    <asp:Label ID="Label5" runat="server" Text="Consultorio" Visible="false"></asp:Label>
                    <asp:TextBox ID="textboxConsultorio" runat="server" CssClass="textBoxes"
                                    Visible="false"></asp:TextBox>
                </p>
                <p>
                    <asp:Button ID="makeABM1" runat="server" Text="Guardar cambios" 
                                Visible="false" Height="33px" Width="187px" 
                                OnClick="makeABM1_Click"/>
                </p>
            </div>
            <div class="bottomSide">
                <p><asp:Label ID="Label7" runat="server" Visible="false"
                              Text="Agregar/eliminar sucursales y días" 
                              Font-Underline="true" ForeColor="OrangeRed" Font-Size="X-Large"
                              style="font-family:Calibri;text-align:center;display:block;"></asp:Label></p>
                <%--ELIMINAR SUCURSALES--%>
                <div style="float:left;width:23%">
                    <asp:Button ID="rmSuc" runat="server" Text="Eliminar sucursal" 
                                Visible="false" Height="33px" Width="100%" BackColor="#99ccff"
                                OnClick="rmSuc_Click"/>
                    <asp:Label ID="Label8" runat="server" Text="Seleccione sucursal a eliminar" 
                                Visible="false"></asp:Label>
                    <asp:DropDownList ID="comboSucursalesRemove" runat="server" CssClass="dropDowns" 
                                      OnSelectedIndexChanged="comboSucursalesRemove_SelectedIndexChanged"
                                      AutoPostBack="true" Visible="false"></asp:DropDownList>
                    <asp:Button ID="abmSUC" runat="server" Text="Eliminar" 
                                Visible="false" Height="33px" Width="100%" BackColor="#FF0000"
                                OnClick="abmSUC_Click" style="margin-top:10px;margin-bottom:40px;"/>
                    <asp:Button ID="cancelRmSuc" runat="server" Text="Cancelar" 
                                Visible="false" Height="33px" Width="100%" BackColor="#999999"
                                OnClick="cancelRmSuc_Click"/>
                </div>
                <%--AÑADIR SUCURSALES--%>
                <div style="float:left;width:23%">
                    <asp:Button ID="addSuc" runat="server" Text="Agregar sucursal" 
                                Visible="false" Height="33px" Width="100%" BackColor="#cc99ff"
                                OnClick="addSuc_Click"/>
                    <asp:Label ID="Label9" runat="server" Text="Seleccione provincia" 
                                Visible="false"></asp:Label>
                    <asp:DropDownList ID="comboProvinciaAdd" runat="server" CssClass="dropDowns" 
                                      OnSelectedIndexChanged="comboProvinciaAdd_SelectedIndexChanged"
                                      AutoPostBack="true" Visible="false" style="margin-bottom:20px;"></asp:DropDownList>
                    <asp:Label ID="Label10" runat="server" Text="Seleccione localidad" 
                                Visible="false"></asp:Label>
                    <asp:DropDownList ID="comboLocalidadAdd" runat="server" CssClass="dropDowns" 
                                      OnSelectedIndexChanged="comboLocalidadAdd_SelectedIndexChanged"
                                      AutoPostBack="true" Visible="false" style="margin-bottom:20px;"></asp:DropDownList>
                    <asp:Label ID="Label11" runat="server" Text="Seleccione sucursal" 
                                Visible="false"></asp:Label>
                    <asp:DropDownList ID="comboSucursalAdd" runat="server" CssClass="dropDowns" 
                                      OnSelectedIndexChanged="comboSucursalAdd_SelectedIndexChanged"
                                      AutoPostBack="true" Visible="false" style="margin-bottom:20px;"></asp:DropDownList>
                    <asp:Button ID="abmSUC1" runat="server" Text="Agregar" 
                                Visible="false" Height="33px" Width="100%" BackColor="#00FF00"
                                OnClick="abmSUC1_Click" style="margin-top:10px;margin-bottom:40px;"/>
                    <asp:Button ID="cancelAddSuc" runat="server" Text="Cancelar" 
                                Visible="false" Height="33px" Width="100%" BackColor="#999999"
                                OnClick="cancelAddSuc_Click"/>
                </div>
                <%--AÑADIR Y BORRAR DÍAS--%>
                <div style="float:left;width:23%">
                    <asp:Button ID="addRmDays" runat="server" Text="Agregar/eliminar dias" 
                                Visible="false" Height="33px" Width="100%" BackColor="#ff99ff"
                                OnClick="addRmDays_Click"/>
                    <asp:CheckBox ID="agregarDias" runat="server" Text="Agregar días" Visible="false" 
                                  OnCheckedChanged="agregarDias_CheckedChanged" AutoPostBack="true"/>
                    <asp:CheckBox ID="eliminarDias" runat="server" Text="Eliminar días" Visible="false" 
                                  OnCheckedChanged="eliminarDias_CheckedChanged" AutoPostBack="true"/>
                    <p><asp:Label ID="Label12" runat="server" Text="Seleccione sucursal" 
                                  Visible="false"></asp:Label>
                    <asp:DropDownList ID="comboSucursalesDias" runat="server"
                                      OnSelectedIndexChanged="comboSucursalesDias_SelectedIndexChanged"
                                      AutoPostBack="true" Visible="false" style="margin-bottom:10px;width:120px"></asp:DropDownList></p>
                    <asp:Label ID="Label13" runat="server" Text="Seleccione dia" 
                               Visible="false"></asp:Label>
                    <asp:DropDownList ID="comboDias1" runat="server" CssClass="dropDowns" 
                                      OnSelectedIndexChanged="comboDias1_SelectedIndexChanged"
                                      AutoPostBack="true" Visible="false" style="margin-bottom:10px;"></asp:DropDownList>
                    <asp:Button ID="abmDAY" runat="server"
                                Visible="false" Height="33px" Width="100%"
                                OnClick="abmDAY_Click" style="margin-top:10px;margin-bottom:40px;"/>
                    <asp:Button ID="cancelAddRmDays" runat="server" Text="Cancelar" 
                                Visible="false" Height="33px" Width="100%" BackColor="#999999"
                                OnClick="cancelAddRmSuc_Click"/>
                </div>
            </div>
        </div>
    </form>
</body>
</html>