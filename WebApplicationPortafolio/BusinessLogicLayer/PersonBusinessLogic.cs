using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplicationPortafolio.DataAccessLayer;
using WebApplicationPortafolio.Models;
using WebApplicationPortafolio.Report.Persona;
using WebApplicationPortafolio.Report;
using System.Data;
using Org.BouncyCastle.Asn1.X509.SigI;
using System.IO;
//using System.Data.DataTable;
namespace WebApplicationPortafolio.BusinessLogicLayer
{
    public class PersonBusinessLogic
    {
        private PersonDataAccess personDataAccess;
        private PersonaReport personaReport;

        public PersonBusinessLogic()
        {
            personDataAccess = new PersonDataAccess();
            personaReport = new PersonaReport();
        }

        public List<Person> GetAllPersons()
        {
          return personDataAccess.GetAllPersons();
        }
        public void RegistrarPersona(Person persona)
        {
            // Aquí puedes realizar validaciones adicionales si es necesario antes de registrar la persona

            personDataAccess.InsertarPersona(persona);
        }

        public void ActualizarPersona(Person persona)
        {
            // Aquí puedes realizar validaciones adicionales si es necesario antes de actualizar la persona

            personDataAccess.ActualizarPersona(persona);
        }

        public byte[] exportarPersona()
        {
            var personasList= personDataAccess.GetAllPersons(); 
            byte[] pdfBytes = personaReport.GenerarPDF(personasList);
            return pdfBytes;
            //personaReport.ActualizarPersona(persona);
        }
        public MemoryStream exportarExcelPersona()
        {
            DataTable data = personaReport.GetData(personDataAccess.GetAllPersons());

            MemoryStream pdfMemoryStream = personaReport.ExportToExcel(data);
            return pdfMemoryStream;
        }
    }
}