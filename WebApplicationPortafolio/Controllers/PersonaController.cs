using Newtonsoft.Json;
using System.Net.Http;
using System.Net;
using System.Web.Http;
using WebApplicationPortafolio.BusinessLogicLayer;
using System.Collections.Generic;
using WebApplicationPortafolio.Models;
using System.IO;
using System.Net.Http.Headers;
using PersonaReport=WebApplicationPortafolio.Report.Persona.PersonaReport;
using System;
using WebApplicationPortafolio.restConsumer;
using System.Threading.Tasks;

namespace WebApplicationPortafolio.Controllers
{
    public class PersonaController : ApiController
    {
        private ApiClient _apiClient;
  

        private PersonBusinessLogic personBusinessLogic;

        private readonly PersonaReport prsonaReport;
        public PersonaController()
        {
            this.personBusinessLogic = new PersonBusinessLogic();
            this.prsonaReport = new PersonaReport();
            _apiClient = new ApiClient();

        }

        [System.Web.Http.HttpGet]
        [Route("api/listado")]
        public HttpResponseMessage Get()
        {
            List<Person> datos2 = personBusinessLogic.GetAllPersons();
            List<Person> datos = new List<Person>();

            var response = Request.CreateResponse(HttpStatusCode.OK, datos2);
            response.Content = new StringContent(JsonConvert.SerializeObject(datos2), System.Text.Encoding.UTF8, "application/json");
            return response;
       
        }
        //PersonaController
        public async Task<IHttpActionResult> GetDataPersona()
        {
            try
            {
                List<Person> data = await _apiClient.GetApiDataAsync();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, ex.Message);
            }
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
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/personaExportarPdf")]
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