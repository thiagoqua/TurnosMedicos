using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PdfSharp.Pdf;
using PdfSharp.Drawing;
using PdfSharp.Drawing.Layout;

namespace Classes {
    public class PDFDrawer {
        private string filepath;
        private string filename;

        public PDFDrawer(string rutaDestino,string nombreArchivo) {
            filepath = rutaDestino;
            filename = nombreArchivo;
        }

        public void drawFromUser(Afiliado pacienteAsAfiliado,string[,] descripcionTurno) {
            PdfDocument document = new PdfDocument();
            PdfPage actualPage = document.AddPage();
            XFont titleFont, textFont;
            XGraphics gfx = XGraphics.FromPdfPage(actualPage);
            XTextFormatter formatter = new XTextFormatter(gfx);
            XRect rect;
            int deltaY, cantTurnos;
            const int MAX_TURNOS_PER_PAGE = 7;
            
            deltaY = 80; cantTurnos = descripcionTurno.Length / 4;
            rect = new XRect(0, 0, actualPage.Width, actualPage.Height);

            titleFont = new XFont("Calibri", 40, XFontStyle.Bold);
            textFont = new XFont("Calibri", 12);

            //Escribo el título
            gfx.DrawString("Turnos para " + pacienteAsAfiliado.Nombre.Trim() + " "
                                          + pacienteAsAfiliado.Apellido.Trim(),
                                 titleFont,
                                 XBrushes.DarkBlue,
                                 rect,
                                 XStringFormats.TopCenter
            );

            rect.X = 20;
            rect.Y = deltaY;
            for(int i = 0; i < cantTurnos; ++i) {
                deltaY += 80;
                rect.Y = deltaY;
                gfx.DrawString(descripcionTurno[i, 0],
                               textFont,
                               XBrushes.Black,
                               rect,
                               XStringFormats.TopLeft
                );

                rect.Y = deltaY + 15;
                gfx.DrawString(descripcionTurno[i, 1],
                               textFont,
                               XBrushes.Black,
                               rect,
                               XStringFormats.TopLeft
                );

                rect.Y = deltaY + 30;
                gfx.DrawString(descripcionTurno[i, 2],
                               textFont,
                               XBrushes.Black,
                               rect,
                               XStringFormats.TopLeft
                );

                rect.Y = deltaY + 45;
                gfx.DrawString(descripcionTurno[i, 3],
                               textFont,
                               XBrushes.Black,
                               rect,
                               XStringFormats.TopLeft
                );

                if(i % MAX_TURNOS_PER_PAGE == MAX_TURNOS_PER_PAGE - 1 &&
                   i != cantTurnos - 1) {
                    deltaY = 0;
                    actualPage = document.AddPage();
                    gfx = XGraphics.FromPdfPage(actualPage);
                }
            }

            document.Save(filepath + filename);
        }

        public void drawFromMedico(Afiliado medicoAsAfiliado,string [,] descripcionTurno,
                                   DateTime fechaTurnos) {
            PdfDocument document = new PdfDocument();
            PdfPage actualPage = document.AddPage();
            XFont titleFont, headerFont, textFont;
            XGraphics gfx = XGraphics.FromPdfPage(actualPage);
            XTextFormatter formatter = new XTextFormatter(gfx);
            XRect rect;
            int deltaY, cantTurnos;
            const int MAX_TURNOS_PER_PAGE = 8;
            string header = "Turnos para el día " + fechaTurnos.ToString("dddd") + " " +
                            fechaTurnos.Day.ToString() + " de " +
                            fechaTurnos.ToString("MMMM") + " de " +
                            fechaTurnos.Year;

            deltaY = 80; cantTurnos = descripcionTurno.Length / 3;
            rect = new XRect(0, 0, actualPage.Width, actualPage.Height);

            titleFont = new XFont("Calibri", 40, XFontStyle.Bold);
            headerFont = new XFont("Calibri", 16, XFontStyle.Underline);
            textFont = new XFont("Calibri", 12);

            //Escribo el título
            gfx.DrawString("Doctor " + medicoAsAfiliado.Nombre.Trim() + " "
                                     + medicoAsAfiliado.Apellido.Trim(),
                                 titleFont,
                                 XBrushes.DarkBlue,
                                 rect,
                                 XStringFormats.TopCenter
            );

            //Escribo la fecha
            rect.X = 20;
            rect.Y = deltaY;
            formatter.DrawString(header,
                                 headerFont,
                                 XBrushes.DarkRed,
                                 rect,
                                 XStringFormats.TopLeft
            );

            for(int i = 0; i < cantTurnos; ++i) {
                deltaY += 80;
                rect.Y = deltaY;
                gfx.DrawString(descripcionTurno[i, 0],
                               textFont,
                               XBrushes.Black,
                               rect,
                               XStringFormats.TopLeft
                );

                rect.Y = deltaY + 15;
                gfx.DrawString(descripcionTurno[i, 1],
                               textFont,
                               XBrushes.Black,
                               rect,
                               XStringFormats.TopLeft
                );

                rect.Y = deltaY + 30;
                gfx.DrawString(descripcionTurno[i, 2],
                               textFont,
                               XBrushes.Black,
                               rect,
                               XStringFormats.TopLeft
                );

                if(i % MAX_TURNOS_PER_PAGE == MAX_TURNOS_PER_PAGE - 1 &&
                   i != cantTurnos - 1) {
                    deltaY = 0;
                    actualPage = document.AddPage();
                    gfx = XGraphics.FromPdfPage(actualPage);
                }
            }

            document.Save(filepath + filename);
        }
    }
}
