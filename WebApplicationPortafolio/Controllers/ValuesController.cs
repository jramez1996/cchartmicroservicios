
using System;

using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using WebApplicationPortafolio.restConsumer;
namespace WebApplicationPortafolio.Controllers
{
    public class ValuesController : ApiController
    {

        private ApiClient _apiClient;
        public ValuesController()
        {
            _apiClient = new ApiClient();
        }

        public async Task<IHttpActionResult> GetDataValues()
        {
            try
            {
                bool data = await _apiClient.CheckPropertyExistsAsync();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, ex.Message);
            }
    }
    
    }
}
