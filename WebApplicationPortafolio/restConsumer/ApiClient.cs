using System;
using System.Collections.Generic;

using System.Net.Http;

using System.Threading.Tasks;
/*
using System.Web;
using static System.Web.Razor.Parser.SyntaxConstants;
using System.Web.Http;
using System.Web.UI.WebControls.WebParts;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Drawing;
using System.Linq;
using System.Net;*/

using Newtonsoft.Json;
using WebApplicationPortafolio.Models;
using Newtonsoft.Json.Linq;

namespace WebApplicationPortafolio.restConsumer
{
  
    public class ApiClient
    {
        private HttpClient _httpClient;
        public ApiClient()
        {
            _httpClient = new HttpClient();
        }

        public async Task<List<Person>> GetApiDataAsync()
        {
            string apiUrl = "http://localhost:3006/dataGeneral/dataPrueba"; // Reemplaza esto con la URL de la API que deseas consumir

            HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                string jsonData = await response.Content.ReadAsStringAsync();
                List<Person> personas = JsonConvert.DeserializeObject<List<Person>>(jsonData);
                return personas;
            }
            else
            {
                throw new Exception("Error al llamar a la API: " + response.StatusCode);
            }
        }

        public async Task<bool> CheckPropertyExistsAsync()
        {
            string apiUrl = "http://localhost:3006/dataGeneral/dataPrueba";

            HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                string jsonData = await response.Content.ReadAsStringAsync();
                JObject jsonObject = JObject.Parse(jsonData);
                return jsonObject.ContainsKey("estado"); // Reemplaza "propiedad" con el nombre de la propiedad que deseas verificar
            }
            else
            {
                throw new Exception("Error al llamar a la API: " + response.StatusCode);
            }
        }
    }

}