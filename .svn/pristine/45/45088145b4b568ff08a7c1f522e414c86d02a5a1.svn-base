using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace WebApiKor.Controllers
{
    [Authorize(Roles ="Administrador, Operador")]
    [RoutePrefix("api/imagens")]
    public class ImagensController : ApiController
    {

        ModeloBancoEntities db = new ModeloBancoEntities();

        [AllowAnonymous]
        [HttpPost]
        [Route("inseririmagens")]
        public HttpResponseMessage InserirImagem(HttpRequestMessage request)
        {
         
            HttpResponseMessage response = null;

            

            string path = System.Web.Hosting.HostingEnvironment.MapPath("/Gallery/");

            HttpFileCollection file = HttpContext.Current.Request.Files;



         
            return response;
        }
    }
}
