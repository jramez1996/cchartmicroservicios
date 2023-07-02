using Newtonsoft.Json;
using System.Net.Http;
using System.Net;
using System.Web.Http;
using WebApplicationPortafolio.BusinessLogicLayer;
using System.Collections.Generic;
using WebApplicationPortafolio.Models;
using OfficeOpenXml;
using System.Data;
using System.IO;
using System.Net.Http.Headers;

using PersonaReport=WebApplicationPortafolio.Report.Persona.PersonaReport;

namespace WebApplicationPortafolio.Controllers
{
    public class PersonaController : ApiController
    {
        private PersonBusinessLogic personBusinessLogic;

        private readonly PersonaReport prsonaReport;
        public PersonaController()
        {
            this.personBusinessLogic = new PersonBusinessLogic();
            this.prsonaReport = new PersonaReport();

        }
        [Route("api/listado")]
        public HttpResponseMessage Get()
        {
            List<Person> datos2 = personBusinessLogic.GetAllPersons();
            List<Person> datos = new List<Person>();

            var response = Request.CreateResponse(HttpStatusCode.OK, datos2);
            response.Content = new StringContent(JsonConvert.SerializeObject(datos2), System.Text.Encoding.UTF8, "application/json");
            return response;
       
        }

        
        [System.Web.Http.HttpPost]
        [Route("api/persona/registrar")]
        public IHttpActionResult RegistrarPersona(Person persona)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            personBusinessLogic.RegistrarPersona(persona);

            return Ok();
        }/**/
      
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

        /*  */
        private DataTable GetData()
        {
            // Obtener los datos de tu fuente de datos (base de datos, servicios, etc.)
            // Aquí se utiliza un ejemplo con datos estáticos para ilustrar el proceso de exportación
            DataTable data = new DataTable();
            data.Columns.Add("Nombre");
            data.Columns.Add("Apellido");
            data.Rows.Add("John", "Doe");
            data.Rows.Add("Jane", "Smith");

            return data;
        }

      
        [System.Web.Http.HttpPut]
        [Route("api/persona/actualizar")]
        public IHttpActionResult ActualizarPersona(Person persona)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            personBusinessLogic.ActualizarPersona(persona);

            return Ok();
        }  

        
        [System.Web.Http.HttpGet]
        [Route("api/personaExportarExcel")]
        public HttpResponseMessage ExportToExcelPersona()
        {
           
            MemoryStream excelStream = personBusinessLogic.exportarExcelPersona();
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Content = new ByteArrayContent(excelStream.ToArray());
            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
            response.Content.Headers.ContentDisposition.FileName = "datos.xlsx";
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");

            return response;
        }
        /**/
        [System.Web.Http.HttpPost]
        [Route("api/personaExportarPdf")]
        public HttpResponseMessage generatePdfPersona()
        {
  
            byte[] pdfBytes = personBusinessLogic.exportarPersona();
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Content = new ByteArrayContent(pdfBytes);
            response.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment");
            response.Content.Headers.ContentDisposition.FileName = "archivo.pdf";
            response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/pdf");

            return response;
        
      }
        /**/



        /**/
    }
}