﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Classes;

namespace AppEscritorio {
    public partial class MedicalTurnos : Form {
        private Medico whoAmI;
        private Afiliado whoAmIAsAfiliado;
        private TablesDataContext db;
        //almacena los TextBoxes que hay en la interfaz
        private List<TextBox> boxes;
        //almacena los turnos que los pacientes tienen con el médico
        private string[,] descripcionTurno;

        //utilizado para volver al componente anterior
        private MedicalHome previousState;

        public MedicalTurnos(Medico medico,MedicalHome home) {
            InitializeComponent();
            whoAmI = medico;
            previousState = home;
            db = new TablesDataContext();
            whoAmIAsAfiliado = (from af in db.Afiliado
                                join user in db.Usuario
                                    on af.AfiliadoID equals user.IDAfiliado
                                where user.UsuarioID == whoAmI.IDUsuario
                                select af).First();
            boxes = new List<TextBox>();
        }

        /// <summary>
        ///     Crea una cierta cantidad de TextBoxes con los parámetros necesarios, invisibles 
        ///     y los agrega a la lista boxes.
        /// </summary>
        /// <param name="cant">cantidad de TextBoxes que hay que crear</param>
        private void makeBoxes(int cant) {
            /*
              tanto textBoxTurno0 (lado izquierdo) como textBoxTurno1 (lado derecho) 
              son los dos TextBoxes que ya se encuentran puestos en la interfaz, 
              pero de manera invisible. Existen ya que se usan como "molde" para crear 
              los próximos TextBoxes en caso de que sean necesarios.
            */
            boxes.Clear();
            const int offsetY = 140,
                      lineLength = 2;
            int deltaY,Xposition,Yposition;
            TextBox toAdd;
            deltaY = Xposition = Yposition = 0;
            switch(cant) {
                case 1:
                    boxes.Add(textBoxTurno1);
                    return;
                case 2:
                    boxes.Add(textBoxTurno1); 
                    boxes.Add(textBoxTurno2);
                    return;
                default:
                    boxes.Add(textBoxTurno1);
                    boxes.Add(textBoxTurno2);
                    for(int i = 2; i < cant; ++i) {
                        toAdd = new TextBox();
                        switch(i % lineLength) {
                            case 0:
                                //pertenece a un box del lado izquierdo
                                toAdd.Clear();
                                deltaY += offsetY;
                                Xposition = boxes[0].Location.X;
                                Yposition = boxes[0].Location.Y;
                                toAdd.Size = boxes[0].Size;
                                break;
                            case 1:
                                //pertenece a un box del lado derecho
                                toAdd.Clear();
                                Xposition = boxes[1].Location.X;
                                Yposition = boxes[1].Location.Y;
                                toAdd.Size = boxes[1].Size;
                                break;
                        }
                        toAdd.Multiline = true;
                        toAdd.Location = new Point(Xposition, Yposition + deltaY);
                        toAdd.BorderStyle = boxes[0].BorderStyle;
                        toAdd.BackColor = boxes[0].BackColor;
                        toAdd.Font = boxes[0].Font;
                        boxes.Add(toAdd);
                    }
                    break;
            }
        }

        private void MedicalTurnos_Load(object sender, EventArgs e) {

        }

        private void fecha_ValueChanged(object sender, EventArgs e) {
            string[] lines = new string[5];
            DateTime selected = Convert.ToDateTime(fecha.Value.Date);
            int szQuery;
            Turno tempTurno;
            List<Turno> queryTurnos;

            //para que haya un espacio entre la información
            lines[1] = "";
            queryTurnos = (from turno in db.Turno
                           join ft in db.FechaTurno
                             on turno.IDFechaTurno equals ft.FechaTurnoID
                           where turno.IDMedico == whoAmI.MedicoID && ft.Fecha == selected
                           select turno).ToList();
            //elimino de la interfaz los TextBoxes si es que hay visibles
            for(int i = 0;i < boxes.Count();++i) {
                if(i < 2) {
                    boxes[i].Visible = false;
                    continue;
                }
                Controls.Remove(boxes[i]);
            }

            szQuery = queryTurnos.Count();
            if(szQuery == 0) {
                generatePDF.Visible = false;
                return;
            }
            makeBoxes(szQuery);
            descripcionTurno = new string[szQuery,3];
            //se arma el texto donde se detalla cada turno
            for(int i = 0;i < szQuery;++i){
                tempTurno = queryTurnos[i];
                descripcionTurno[i,0] = "Turno sacado por: ";
                Afiliado afiliado = (from af in db.Afiliado
                                     join user in db.Usuario
                                        on af.AfiliadoID equals user.IDAfiliado
                                     where user.UsuarioID == tempTurno.IDUsuario
                                     select af).First();
                descripcionTurno[i,0] += afiliado.Nombre.Trim() + " " + afiliado.Apellido.Trim();

                var querySucursal = from suc in db.Sucursal
                                    where suc.SucursalId == tempTurno.IDSucursal
                                    select suc;
                descripcionTurno[i,1] = "En sucursal: " + 
                        querySucursal.First().SucursalDescripcion;

                var queryProvincia = from prov in db.Provincia
                                     where prov.ProvinciaId == tempTurno.IDProvincia
                                     select prov;
                descripcionTurno[i,1] += ", " + 
                        queryProvincia.First().ProvinciaDescripcion;

                var queryLocalidad = from loc in db.Localidad
                                     where loc.LocalidadId == tempTurno.IDLocalidad
                                     select loc;
                descripcionTurno[i,1] += ", " + 
                        queryLocalidad.First().LocalidadDescripcion.Trim();

                descripcionTurno[i, 2] = "Horario: ";
                TimeSpan horario = (from h in db.Horario
                                    join ft in db.FechaTurno
                                        on h.HorarioID equals ft.IDHorario
                                    where tempTurno.IDFechaTurno == ft.FechaTurnoID
                                    select h.Hora).First();
                descripcionTurno[i, 2] += horario.ToString();
            }
            //salteo posiciones para crear un espacio entre cada línea del TextBox
            for(int i = 0;i < szQuery; ++i) {
                lines[0] = descripcionTurno[i, 0];
                lines[2] = descripcionTurno[i, 1];
                lines[4] = descripcionTurno[i, 2];
                boxes[i].Visible = true;
                boxes[i].Enabled = false;
                boxes[i].Lines = lines;
                if(i < 2)
                    continue;
                Controls.Add(boxes[i]);
            }
            generatePDF.Visible = true;
        }

        private void textbox_Paint(object sender, PaintEventArgs e) {
            
        }

        private void BarraTitulo_Paint(object sender, PaintEventArgs e) {

        }

        private void btnCerrar_Click(object sender, EventArgs e) {
            Application.Exit();
        }

        private void scroller_Scroll(object sender, ScrollEventArgs e) {
            
        }

        private void btnCerrar_Click_1(object sender, EventArgs e) {
            Application.Exit();
        }

        private void back_Click(object sender, EventArgs e) {
            previousState.Show();
            this.Close();
        }

        private void generatePDF_Click(object sender, EventArgs e) {
            PDFDrawer drawer;
            string filepath, filename, caption, msg;

            filepath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\";
            filename = "reporte.pdf";
            drawer = new PDFDrawer(filepath, filename);

            drawer.drawFromMedico(whoAmIAsAfiliado, descripcionTurno, fecha.Value);

            caption = "Reporte generado de manera exitosa";
            msg = "El reporte ha sido creado y almacenado en " + filepath +
                  " con el nombre de " + filename;

            MessageBox.Show(msg, caption,MessageBoxButtons.OK);
        }
    }
}
