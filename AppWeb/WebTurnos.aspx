<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebTurnos.aspx.cs" Inherits="AppWeb.WebTurnos" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <link href="Styles/WebABMStyle.css" rel="stylesheet" type="text/css" />
    <form id="form1" runat="server">
        <div class="buttonsSection">
            <div><asp:Image ID="Image1" runat="server" ImageUrl="~/Resources/LOGO.png"
                       Width="200px" style="border-radius: 10px 10px;margin-top:20px;"/></div>
            <asp:Label ID="Label6" runat="server" Text="Seleccione fecha" 
                       style="color:aliceblue;margin-left:10px;font-weight:600;margin-top: 20px;" 
                       Visible="false"></asp:Label>
                    <asp:Calendar ID="fechaTurnoPicker" runat="server" BackColor="White" MinimunDate="2022-11-24"
                        BorderColor="#3366CC" BorderWidth="1px" CellPadding="1" DayNameFormat="Shortest" 
                        Font-Names="Verdana" Font-Size="8pt" ForeColor="#003399" Height="166px" 
                        Width="200px" style="margin-left:24%;" Visible="false"
                        OnSelectionChanged="fechaTurnoPicker_SelectionChanged">
                        <DayHeaderStyle BackColor="#99CCCC" ForeColor="#336666" Height="1px" />
                        <NextPrevStyle Font-Size="8pt" ForeColor="#CCCCFF" />
                        <OtherMonthDayStyle ForeColor="#999999" />
                        <SelectedDayStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                        <SelectorStyle BackColor="#99CCCC" ForeColor="#336666" />
                        <TitleStyle BackColor="#003399" BorderColor="#3366CC" BorderWidth="1px" Font-Bold="True" Font-Size="10pt" ForeColor="#CCCCFF" Height="25px" />
                        <TodayDayStyle BackColor="#99CCCC" ForeColor="White" />
                        <WeekendDayStyle BackColor="#CCCCFF" />
                    </asp:Calendar>
            <asp:Button ID="backButton" runat="server" CssClass="back-button" Text="Volver"
                        PostBackUrl="~/WebHome.aspx"/>
        </div>
        <div class="inputSection">
            <div>
                <asp:Button ID="addTurnoButton" runat="server" OnClick="addTurnoButton_Click" 
                            Text="Sacar nuevo turno" CssClass="add-delete"/>
                <asp:Button ID="cancelAddButton" runat="server" OnClick="cancelAddButton_Click" 
                            Text="Cancelar" Visible="false" CssClass="cancels"/>
                <%--PRIMER FILA--%>
                <p>
                    <div style="float:left;margin-top:10px;margin-left:20px;">
                        <asp:Label ID="Label2" runat="server" Text="Seleccione provincia" 
                                   Visible="false" CssClass="labeles"></asp:Label>
                        <asp:DropDownList ID="comboProvincia" runat="server" CssClass="dropDowns" 
                                          OnSelectedIndexChanged="comboProvincia_SelectedIndexChanged1"
                                          AutoPostBack="true" Visible="false">
                        </asp:DropDownList>
                    </div>
                    <div style="float:left;margin-top:10px;margin-left:20px;">
                        <asp:Label ID="Label3" runat="server" Text="Seleccione localidad" 
                                   Visible="false" CssClass="labeles"></asp:Label>
                        <asp:DropDownList ID="comboLocalidad" runat="server" CssClass="dropDowns" 
                                          OnSelectedIndexChanged="comboLocalidad_SelectedIndexChanged"
                                          AutoPostBack="true" Visible="false">
                        </asp:DropDownList>
                    </div>
                    <div style="float:left;margin-top:10px;margin-left:20px;">
                        <asp:Label ID="Label4" runat="server" Text="Seleccione sucursal" 
                                   Visible="false" CssClass="labeles"></asp:Label>
                        <asp:DropDownList ID="comboSucursales" runat="server" CssClass="dropDowns" 
                                          OnSelectedIndexChanged="comboSucursales_SelectedIndexChanged"
                                          AutoPostBack="true" Visible="false">
                        </asp:DropDownList>
                    </div>
                </p>
                <%--SEGUNDA FILA--%>
                <p>
                    <div style="float:left;margin-top:10px;margin-left:20px;">
                        <asp:Label ID="Label1" runat="server" Text="Seleccione especialidad" 
                                   Visible="false" CssClass="labeles"></asp:Label>
                        <asp:DropDownList ID="comboEspecialidades" runat="server" CssClass="dropDowns" 
                                          OnSelectedIndexChanged="comboEspecialidades_SelectedIndexChanged1"
                                          AutoPostBack="true" Visible="false">
                        </asp:DropDownList>
                    </div>
                    <div style="float:left;margin-top:10px;margin-left:20px;">
                        <asp:Label ID="Label5" runat="server" Text="Seleccione médico" 
                                   Visible="false" CssClass="labeles"></asp:Label>
                        <asp:DropDownList ID="comboMedicos" runat="server" CssClass="dropDowns" 
                                          OnSelectedIndexChanged="comboMedicos_SelectedIndexChanged"
                                          AutoPostBack="true" Visible="false">
                        </asp:DropDownList>
                    </div>
                    <div style="float:left;margin-top:10px;margin-left:20px;">
                        <asp:Label ID="Label7" runat="server" Text="Seleccione hora" 
                                   Visible="false" CssClass="labeles"></asp:Label>
                        <asp:DropDownList ID="comboHorarios" runat="server" CssClass="dropDowns"
                                          OnSelectedIndexChanged="comboHorarios_SelectedIndexChanged"
                                          AutoPostBack="true" Visible="false">
                        </asp:DropDownList>
                    </div>
                </p>
                <%--TERCER FILA--%>
                <p>
                    <div style="margin-top:10px;margin-left:20px;">
                        <asp:Label ID="Label18" runat="server" BackColor="#FFC0FF" Width="317px"
                                   Text="" style="margin-left:5px; font-weight:900; 
                                                  border-radius: 7px 7px; margin-top:10px;" 
                                   Visible="false"></asp:Label>
                    </div>
                    <asp:Button ID="sacarTurnoButton" runat="server" CssClass="nuevo-turno"
                                OnClick="sacarTurnoButton_Click" Text="Sacar turno" Visible="false"/>
                </p>
            </div>
            <div>
                <asp:Button ID="rmTurnoButton" runat="server" OnClick="rmTurnoButton_Click" 
                            Text="Eliminar turnos" CssClass="add-delete"/>
                <asp:Button ID="cancelRmButton" runat="server" OnClick="cancelRmButton_Click" 
                            Text="Cancelar" Visible="false" CssClass="cancels"/>
                <p>
                    <asp:Label ID="Label8" runat="server" Text="Datos de sus turnos" 
                               Visible="false" CssClass="titles"></asp:Label>
                </p>
                <%--PRIMER FILA--%>
                <p>
                    <div style="float:left;margin-top:10px;margin-left:20px;">
                        <asp:Label ID="Label9" runat="server" Text="Medico" 
                                   Visible="false" CssClass="labeles"></asp:Label>
                        <asp:TextBox ID="TextBox1" runat="server" CssClass="dropDowns" Visible="false"
                                     Enabled="false"></asp:TextBox>
                    </div>
                    <div style="float:left;margin-top:10px;margin-left:20px;">
                        <asp:Label ID="Label10" runat="server" Text="Especialidad" 
                                   Visible="false" CssClass="labeles"></asp:Label>
                        <asp:TextBox ID="TextBox2" runat="server" CssClass="dropDowns" Visible="false"
                                     Enabled="false"></asp:TextBox>
                    </div>
                    <div style="float:left;margin-top:10px;margin-left:20px;">
                        <asp:Label ID="Label11" runat="server" Text="Provincia" 
                                   Visible="false" CssClass="labeles"></asp:Label>
                        <asp:TextBox ID="TextBox3" runat="server" CssClass="dropDowns" Visible="false"
                                     Enabled="false"></asp:TextBox>
                    </div>
                </p>
                <%--SEGUNDA FILA--%>
                <p>
                    <div style="float:left;margin-top:10px;margin-left:20px;">
                        <asp:Label ID="Label12" runat="server" Text="Localidad" 
                                   Visible="false" CssClass="labeles"></asp:Label>
                        <asp:TextBox ID="TextBox4" runat="server" CssClass="dropDowns" Visible="false"
                                     Enabled="false"></asp:TextBox>
                    </div>
                    <div style="float:left;margin-top:10px;margin-left:20px;">
                        <asp:Label ID="Label13" runat="server" Text="Sucursal" 
                                   Visible="false" CssClass="labeles"></asp:Label>
                        <asp:TextBox ID="TextBox5" runat="server" CssClass="dropDowns" Visible="false"
                                     Enabled="false"></asp:TextBox>
                    </div>
                    <div style="float:left;margin-top:10px;margin-left:20px;">
                        <asp:Label ID="Label14" runat="server" Text="Fecha" 
                                   Visible="false" CssClass="labeles"></asp:Label>
                        <asp:TextBox ID="TextBox6" runat="server" CssClass="dropDowns" Visible="false"
                                     Enabled="false"></asp:TextBox>
                    </div>
                    <div style="float:left;margin-top:10px;margin-left:20px;">
                        <asp:Label ID="Label15" runat="server" Text="Hora" 
                                   Visible="false" CssClass="labeles"></asp:Label>
                        <asp:TextBox ID="TextBox7" runat="server" CssClass="dropDowns" Visible="false"
                                     Enabled="false"></asp:TextBox>
                     </div>
                </p>
                <%--TERCER FILA--%>
                <p style="margin-top:20%">
                    <div style="float:left;margin-top:10px;margin-left:20px;">
                        <asp:ImageButton ID="backTurnoButton" runat="server" Visible="false"
                                         ImageUrl="~/Resources/BACK_ARROW.png" OnClick="backTurnoButton_Click"/>
                        <asp:Label ID="Label16" runat="server" Text="Ver anterior turno" CssClass="labeles"
                                   Visible="false" style="float:right;margin-top: 6px;margin-left: 5px;"></asp:Label>
                    </div>
                    <div style="float:left;margin-top:10px;margin-left:20px;">
                        <asp:Label ID="Label17" runat="server" Text="Ver siguiente turno" 
                                   Visible="false" CssClass="labeles"
                                   style="margin-left:100px;float:left;margin-top:6px;margin-right: 5px;"></asp:Label>
                        <asp:ImageButton ID="nextTurnoButton" runat="server" Visible="false"
                                         ImageUrl="~/Resources/NEXT_ARROW.png" OnClick="nextTurnoButton_Click"/>
                    </div>
                    <div>
                        <asp:Button ID="eliminarTurnoButton" runat="server" CssClass="borrar-turno"
                                    OnClick="eliminarTurnoButton_Click" Text="Eliminar éste turno" 
                                    Visible="false"/>
                    </div>
                </p>
            </div>
        </div>
    </form>
</body>
</html>
