using ApiSaSTokenStorageTables.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiSaSTokenStorageTables.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TableTokenController : ControllerBase
    {
        private ServiceSaSToken service;

        public TableTokenController(ServiceSaSToken service)
        {
            this.service = service;
        }

        [HttpGet]
        [Route("[action]/{curso}")]
        public ActionResult<string> GenerateToken(string curso)
        {
            string token = this.service.GenerateSaSToken(curso);
            //podemos personalizar el json que devolvemos
            //{numeroregistros: 5, Datos: List<T>}
            //return Ok(
            //    new
            //    {
            //        numeroregistros = 5,
            //        datos = new List<int>()
            //    });
            //{token: TOKNEVALUE}
            return Ok(new
            {
                token = token
            });
        }
    }
}
