<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebTurnos.aspx.cs" Inherits="AppWeb.WebTurnos" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        #Select1 {
            width: 135px;
        }
    </style>
</head>
<body>
    <link href="WebTurnosStyle.css" rel="stylesheet" type="text/css" />
    <form id="form1" runat="server">
        <div class="buttonsSection">
            <asp:Image ID="Image1" runat="server" />
            <asp:Label ID="Label6" runat="server" Text="Seleccione fecha" 
                       style="color:white;margin-left:31%;" Visible="false"></asp:Label>
                    <asp:Calendar ID="fechaTurnoPicker" runat="server" BackColor="White" MinimunDate="2022-11-24"
                        BorderColor="#3366CC" BorderWidth="1px" CellPadding="1" DayNameFormat="Shortest" 
                        Font-Names="Verdana" Font-Size="8pt" ForeColor="#003399" Height="166px" 
                        Width="200px" style="margin-left:20%;" Visible="false"
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
        </div>
        <div class="inputSection">
            <div class="newTurn">
                <asp:Button ID="addTurnoButton" runat="server" OnClick="addTurnoButton_Click" 
                            Text="Sacar nuevo turno" style="margin-left:50%"/>
                <asp:Button ID="cancelAddButton" runat="server" OnClick="cancelAddButton_Click" 
                            Text="Cancelar" style="margin-left:15px;" Visible="false"/>
                <%--PRIMER FILA--%>
                <p>
                    <asp:Label ID="Label2" runat="server" Text="Seleccione provincia" Visible="false"></asp:Label>
                    <asp:DropDownList ID="comboProvincia" runat="server" CssClass="dropDowns" 
                                      OnSelectedIndexChanged="comboProvincia_SelectedIndexChanged1"
                                      AutoPostBack="true" Visible="false">
                    </asp:DropDownList>
                   
                    <asp:Label ID="Label3" runat="server" Text="Seleccione localidad" Visible="false"></asp:Label>
                    <asp:DropDownList ID="comboLocalidad" runat="server" CssClass="dropDowns" 
                                      OnSelectedIndexChanged="comboLocalidad_SelectedIndexChanged"
                                      AutoPostBack="true" Visible="false">
                    </asp:DropDownList>
                   
                    <asp:Label ID="Label4" runat="server" Text="Seleccione sucursal" Visible="false"></asp:Label>
                    <asp:DropDownList ID="comboSucursales" runat="server" CssClass="dropDowns" 
                                      OnSelectedIndexChanged="comboSucursales_SelectedIndexChanged"
                                      AutoPostBack="true" Visible="false">
                    </asp:DropDownList>
                </p>
                <%--SEGUNDA FILA--%>
                <p>
                    <asp:Label ID="Label1" runat="server" Text="Seleccione especialidad" Visible="false"></asp:Label>
                    <asp:DropDownList ID="comboEspecialidades" runat="server" CssClass="dropDowns" 
                                      OnSelectedIndexChanged="comboEspecialidades_SelectedIndexChanged1"
                                      AutoPostBack="true" Visible="false">
                    </asp:DropDownList>
                   
                    <asp:Label ID="Label5" runat="server" Text="Seleccione médico" Visible="false"></asp:Label>
                    <asp:DropDownList ID="comboMedicos" runat="server" CssClass="dropDowns" 
                                      OnSelectedIndexChanged="comboMedicos_SelectedIndexChanged"
                                      AutoPostBack="true" Visible="false">
                    </asp:DropDownList>
                    <asp:Label ID="Label7" runat="server" Text="Seleccione hora" Visible="false"></asp:Label>
                    <asp:DropDownList ID="comboHorarios" runat="server" CssClass="dropDowns"
                                      OnSelectedIndexChanged="comboHorarios_SelectedIndexChanged"
                                      AutoPostBack="true" Visible="false"></asp:DropDownList>
                </p>
                <%--TERCER FILA--%>
                <p>
                    <asp:Label ID="Label18" runat="server" BackColor="#FFC0FF" Width="317px"
                               Text="" style="margin-left:5px; font-family: Calibri;" 
                               Visible="false"></asp:Label>
                    <asp:Button ID="sacarTurnoButton" runat="server" style="margin-left:50%"
                                OnClick="sacarTurnoButton_Click" BackColor="#66ff99"
                                Width="50%" Height="46px" Font-Bold="true" Font-Size="12"
                                Text="Sacar turno" Visible="false"/>
                </p>
            </div>
            <div class="deleteTurn">
                <asp:Button ID="rmTurnoButton" runat="server" OnClick="rmTurnoButton_Click" 
                            Text="Eliminar turnos" style="margin-left:50%"/>
                <asp:Button ID="cancelRmButton" runat="server" OnClick="cancelRmButton_Click" 
                            Text="Cancelar" style="margin-left:15px;" Visible="false"/>
                <p>
                    <asp:Label ID="Label8" runat="server" Text="Datos de sus turnos" Visible="false"
                               Font-Underline="true" ForeColor="OrangeRed" Font-Size="X-Large"
                               style="font-family:Calibri;margin-left:40%;"></asp:Label>
                </p>
                <%--PRIMER FILA--%>
                <p>
                    <asp:Label ID="Label9" runat="server" Text="Medico" Visible="false"></asp:Label>
                    <asp:TextBox ID="TextBox1" runat="server" CssClass="dropDowns" Visible="false"
                                 Enabled="false"></asp:TextBox>
                    <asp:Label ID="Label10" runat="server" Text="Especialidad" Visible="false"></asp:Label>
                    <asp:TextBox ID="TextBox2" runat="server" CssClass="dropDowns" Visible="false"
                                 Enabled="false"></asp:TextBox>
                    <asp:Label ID="Label11" runat="server" Text="Provincia" Visible="false"></asp:Label>
                    <asp:TextBox ID="TextBox3" runat="server" CssClass="dropDowns" Visible="false"
                                 Enabled="false"></asp:TextBox>
                </p>
                <%--SEGUNDA FILA--%>
                <p>
                    <asp:Label ID="Label12" runat="server" Text="Localidad" Visible="false"></asp:Label>
                    <asp:TextBox ID="TextBox4" runat="server" CssClass="dropDowns" Visible="false"
                                 Enabled="false"></asp:TextBox>
                    <asp:Label ID="Label13" runat="server" Text="Sucursal" Visible="false"></asp:Label>
                    <asp:TextBox ID="TextBox5" runat="server" CssClass="dropDowns" Visible="false"
                                 Enabled="false"></asp:TextBox>
                    <asp:Label ID="Label14" runat="server" Text="Fecha" Visible="false"></asp:Label>
                    <asp:TextBox ID="TextBox6" runat="server" CssClass="dropDowns" Visible="false"
                                 Enabled="false"></asp:TextBox>
                    <asp:Label ID="Label15" runat="server" Text="Hora" Visible="false"></asp:Label>
                    <asp:TextBox ID="TextBox7" runat="server" CssClass="dropDowns" Visible="false"
                                 Enabled="false"></asp:TextBox>
                </p>
                <%--TERCER FILA--%>
                <p>
                    <asp:ImageButton ID="backTurnoButton" runat="server" Visible="false"
                                     ImageUrl="~/Resources/BACK_ARROW.png" OnClick="backTurnoButton_Click"/>
                    <asp:Label ID="Label16" runat="server" Text="Ver anterior turno" 
                               Visible="false" style="margin-left:5px;"></asp:Label>
                    <asp:Label ID="Label17" runat="server" Text="Ver siguiente turno" 
                               Visible="false" style="margin-left:100px;"></asp:Label>
                    <asp:ImageButton ID="nextTurnoButton" runat="server" Visible="false"
                                     ImageUrl="~/Resources/NEXT_ARROW.png" OnClick="nextTurnoButton_Click"/>
                    <asp:Button ID="eliminarTurnoButton" runat="server" 
                                style="float:right;" BackColor="#ff6666"
                                OnClick="eliminarTurnoButton_Click" Text="Eliminar éste turno" 
                                Width="30%" Height="46px" Font-Bold="true" Font-Size="12"
                                Visible="false"/>
                </p>
            </div>
        </div>
    </form>
</body>
</html>
