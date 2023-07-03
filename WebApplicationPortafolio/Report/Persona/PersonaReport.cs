using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System.Collections.Generic;
using System.IO;
using WebApplicationPortafolio.Models;


using RazorEngine;
using RazorEngine.Templating;
using System;
using System.IO;
using OfficeOpenXml;
using System.Data;

namespace WebApplicationPortafolio.Report.Persona
{
    public class PersonaReport
    {
        public  byte[]  GenerarPDF(List<Person> personas)
        {


            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            string rutaArchivo = Path.Combine(basePath, "Report/Plantillahtml/reportePdf.html");

            string template = File.ReadAllText(rutaArchivo);
            string html = Engine.Razor.RunCompile(template, "templateKey", typeof(List<Person>), personas);

            using (var stream = new MemoryStream())
            {
                var document = new Document();
                PdfWriter writer = PdfWriter.GetInstance(document, stream);
                document.Open();
                HTMLWorker worker = new HTMLWorker(document);
                worker.Parse(new StringReader(html));
                document.Close();

                byte[] bytes = stream.ToArray();
                return bytes;
                // Guarda el archivo PDF en disco o haz lo que necesites con los bytes.
            }
        }
        
        public MemoryStream ExportToExcel(DataTable data)
        {
            using (ExcelPackage package = new ExcelPackage())
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Datos");

                // Escribir encabezados
                for (int i = 0; i < data.Columns.Count; i++)
                {
                    worksheet.Cells[1, i + 1].Value = data.Columns[i].ColumnName;
                }

                // Escribir datos
                for (int i = 0; i < data.Rows.Count; i++)
                {
                    for (int j = 0; j < data.Columns.Count; j++)
                    {
                        worksheet.Cells[i + 2, j + 1].Value = data.Rows[i][j];
                    }
                }

                // Convertir el paquete Excel a un MemoryStream
                MemoryStream stream = new MemoryStream(package.GetAsByteArray());

                return stream;
            }
        }


        public DataTable GetData(List<Person> dataList)
        {
            // Obtener los datos de tu fuente de datos (base de datos, servicios, etc.)
            // Aquí se utiliza un ejemplo con datos estáticos para ilustrar el proceso de exportación
            DataTable data = new DataTable();
            data.Columns.Add("Id");
            data.Columns.Add("Nombre");
           
            foreach (var person in dataList)
            {
                data.Rows.Add(person.id.ToString(), person.nombre);
                data.Rows.Add("Jane", "Smith");
            }
            //data.Rows.Add("John", "Doe");
            //data.Rows.Add("Jane", "Smith");

            return data;
        }


    }
}