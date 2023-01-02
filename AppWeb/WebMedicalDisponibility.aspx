<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebMedicalDisponibility.aspx.cs" Inherits="AppWeb.WebMedicalDisponibility" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <link href="Styles/WebABMStyle.css" rel="stylesheet" type="text/css" />
    <form id="form1" runat="server">
        <div class="buttonsSection" runat="server">
            <asp:Image ID="Image1" runat="server" />
            <asp:Button ID="backButton" runat="server" CssClass="back-button"
                        Text="Volver" PostBackUrl="~/WebMedicalHome.aspx"/>
            <p style="margin-top:100px;"><asp:Label ID="Label14" runat="server" Text="Cambiar vista a:" Visible="true"
                        style="margin-bottom:10px;text-decoration:underline;font-weight:600;
                               font-size:x-large;"
                        ForeColor="OrangeRed"></asp:Label></p>
            <asp:Button ID="abmDyS" runat="server" Text="Modificar sucursales y días" 
                        OnClick="abmDyS_Click" CssClass="actionchanger-button"/>
        </div>
        <div class="inputSection" runat="server">
            <p><asp:Label ID="Label1" runat="server" Text="Modificar disponibilidad" 
                          CssClass="titles"></asp:Label></p>
            <p><asp:Label ID="Label7" runat="server" Visible="false"
                          Text="Agregar/eliminar sucursales y días" CssClass="titles"
                          style="margin-left:25%;"></asp:Label></p>
            <div class="topSide" runat="server">
                <div style="margin-top:50px;margin-left:20px;">
                    <asp:Label ID="Label2" runat="server" Text="Seleccione sucursal" Visible="true"
                               CssClass="labeles"></asp:Label>
                    <asp:DropDownList ID="comboSucursales" runat="server" CssClass="dropDowns" 
                                        OnSelectedIndexChanged="comboSucursales_SelectedIndexChanged"
                                        AutoPostBack="true" Visible="true"></asp:DropDownList>
                </div>
                <div style="margin-top:30px;margin-left:20px;">
                    <asp:Label ID="Label3" runat="server" Text="Seleccione día" Visible="true"
                               CssClass="labeles"></asp:Label>
                    <asp:DropDownList ID="comboDias" runat="server" CssClass="dropDowns" 
                                        OnSelectedIndexChanged="comboDias_SelectedIndexChanged"
                                        AutoPostBack="true" Visible="true"></asp:DropDownList>
                </div>
                <div style="margin-left: 20px;margin-top:30px;">
                    <asp:Label ID="ubicacion" runat="server" Visible="true" BackColor="#ff9966" 
                               style="margin-left:5px; font-weight:900; 
                                      border-radius: 7px 7px; margin-top:10px;"></asp:Label>
                </div>
            </div>
            <div class="topSide" runat="server">
                <div style="margin-top:10%;margin-left:20px;">
                    <asp:Label ID="Label4" runat="server" Text="Horario inicio" Visible="false"
                                CssClass="labeles"></asp:Label>
                    <asp:TextBox ID="textboxHoraInicio" runat="server" CssClass="text-boxes"
                                    Visible="false"></asp:TextBox>
                </div>
                <div style="margin-top:20px;margin-left:20px;">
                    <asp:Label ID="Label6" runat="server" Text="Horario finalización" 
                                Visible="false" CssClass="labeles"></asp:Label>
                    <asp:TextBox ID="textboxHoraFin" runat="server" CssClass="text-boxes"
                                    Visible="false"></asp:TextBox>
                </div>
                <div style="margin-top:20px;margin-left:20px;">
                    <asp:Label ID="Label5" runat="server" Text="Consultorio" Visible="false"
                                CssClass="labeles"></asp:Label>
                    <asp:TextBox ID="textboxConsultorio" runat="server" CssClass="text-boxes"
                                    Visible="false"></asp:TextBox>
                </div>
                <div style="margin-top:50px;margin-left:20px;">
                    <asp:Button ID="makeABM1" runat="server" Text="Guardar cambios" 
                                Visible="false" CssClass="nuevo-turno" style="margin-left:0;"
                                OnClick="makeABM1_Click"/>
                </div>
            </div>
            <div class="bottomSide">
                <asp:Button ID="rmSuc" runat="server" Text="Eliminar sucursal" 
                            Visible="false" CssClass="eliminar-sucursal"
                            OnClick="rmSuc_Click"/>
                <asp:Button ID="addSuc" runat="server" Text="Agregar sucursal" 
                                Visible="false" CssClass="agregar-sucursal"
                                OnClick="addSuc_Click"/>
                <asp:Button ID="addRmDays" runat="server" Text="Agregar/eliminar dias" 
                                Visible="false" CssClass="agregar-eliminar-dias"
                                OnClick="addRmDays_Click"/>
                <%--ELIMINAR SUCURSALES--%>
                <div>
                    <div style="margin-top:20px;margin-left:20px;">
                        <asp:Label ID="Label8" runat="server" Text="Seleccione sucursal a eliminar" 
                                   Visible="false" CssClass="labeles"></asp:Label>
                        <asp:DropDownList ID="comboSucursalesRemove" runat="server" CssClass="dropDowns" 
                                          OnSelectedIndexChanged="comboSucursalesRemove_SelectedIndexChanged"
                                          AutoPostBack="true" Visible="false"></asp:DropDownList>
                    </div>
                    <asp:Button ID="abmSUC" runat="server" Text="Eliminar" 
                                Visible="false" CssClass="borrar-turno"
                                OnClick="abmSUC_Click" style="margin-top:40px;"/>
                    <asp:Button ID="cancelRmSuc" runat="server" Text="Cancelar" 
                                Visible="false" CssClass="cancels" style="margin-top:40px;"
                                OnClick="cancelRmSuc_Click"/>
                </div>
                <%--AÑADIR SUCURSALES--%>
                <div>
                    <div style="margin-top:20px;margin-left:20px;">
                        <asp:Label ID="Label9" runat="server" Text="Seleccione provincia" 
                                    Visible="false" CssClass="labeles"></asp:Label>
                        <asp:DropDownList ID="comboProvinciaAdd" runat="server" CssClass="dropDowns" 
                                          OnSelectedIndexChanged="comboProvinciaAdd_SelectedIndexChanged"
                                          AutoPostBack="true" Visible="false" style="margin-bottom:20px;"></asp:DropDownList>
                    </div>
                    <div style="margin-top:20px;margin-left:20px;">
                        <asp:Label ID="Label10" runat="server" Text="Seleccione localidad" 
                                    Visible="false" CssClass="labeles"></asp:Label>
                        <asp:DropDownList ID="comboLocalidadAdd" runat="server" CssClass="dropDowns" 
                                          OnSelectedIndexChanged="comboLocalidadAdd_SelectedIndexChanged"
                                          AutoPostBack="true" Visible="false" style="margin-bottom:20px;"></asp:DropDownList>
                    </div>
                    <div style="margin-top:20px;margin-left:20px;">
                        <asp:Label ID="Label11" runat="server" Text="Seleccione sucursal" 
                                    Visible="false" CssClass="labeles"></asp:Label>
                        <asp:DropDownList ID="comboSucursalAdd" runat="server" CssClass="dropDowns" 
                                          OnSelectedIndexChanged="comboSucursalAdd_SelectedIndexChanged"
                                          AutoPostBack="true" Visible="false" style="margin-bottom:20px;"></asp:DropDownList>
                    </div>
                    <asp:Button ID="abmSUC1" runat="server" Text="Agregar" 
                                Visible="false" CssClass="nuevo-turno"
                                OnClick="abmSUC1_Click" style="margin-top:40px;float:right;margin-left:0;"/>
                    <asp:Button ID="cancelAddSuc" runat="server" Text="Cancelar" 
                                Visible="false" CssClass="cancels"
                                OnClick="cancelAddSuc_Click" style="margin-top:40px;float:left;"/>
                </div>
                <%--AÑADIR Y BORRAR DÍAS--%>
                <div>
                    <asp:CheckBox ID="agregarDias" runat="server" Text="Agregar días" Visible="false" 
                                  OnCheckedChanged="agregarDias_CheckedChanged" AutoPostBack="true"
                                  CssClass="labeles"/>
                    <asp:CheckBox ID="eliminarDias" runat="server" Text="Eliminar días" Visible="false" 
                                  OnCheckedChanged="eliminarDias_CheckedChanged" AutoPostBack="true"
                                  CssClass="labeles"/>
                    <div style="margin-top:20px;margin-left:20px;">
                        <p><asp:Label ID="Label12" runat="server" Text="Seleccione sucursal" 
                                      Visible="false" CssClass="labeles"></asp:Label>
                        <asp:DropDownList ID="comboSucursalesDias" runat="server"
                                          OnSelectedIndexChanged="comboSucursalesDias_SelectedIndexChanged"
                                          AutoPostBack="true" Visible="false" CssClass="dropDowns"
                                          style="margin-bottom:10px;width:120px"></asp:DropDownList></p>
                    </div>
                    <div style="margin-top:20px;margin-left:20px;">
                        <asp:Label ID="Label13" runat="server" Text="Seleccione dia" 
                                   Visible="false" CssClass="labeles"></asp:Label>
                        <asp:DropDownList ID="comboDias1" runat="server" CssClass="dropDowns" 
                                          OnSelectedIndexChanged="comboDias1_SelectedIndexChanged"
                                          AutoPostBack="true" Visible="false"
                                          style="margin-bottom:10px;"></asp:DropDownList>
                    </div>
                    <asp:Button ID="abmDAY" runat="server" Visible="false" 
                                style="margin-top:40px;margin-left:0;float:right"
                                OnClick="abmDAY_Click"/>
                    <asp:Button ID="cancelAddRmDays" runat="server" Text="Cancelar" 
                                Visible="false" CssClass="cancels" style="margin-top:40px;float:left;"
                                OnClick="cancelAddRmSuc_Click"/>
                </div>
            </div>
        </div>
    </form>
</body>
</html>