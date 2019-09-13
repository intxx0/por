using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using WebApiKor.InfraWeb;
using WebApiKor.Models;

namespace WebApiKor.Controllers
{

    [RoutePrefix("api/guia")]
    public class GuiasController : ApiController
    {
        ModeloBancoEntities db = new ModeloBancoEntities();



        [HttpGet]
        [Route("listadetalhesguia/{numeroGuia}")]
        public HttpResponseMessage GetGuia(HttpRequestMessage request, string numeroGuia)
        {

            HttpResponseMessage response = null;


            List<guia> guias =
                db.guia.Where(gui => gui.numero_guia == numeroGuia).OrderBy(gui => gui.data_emissao_guia).ToList();

            if (guias.Count > 0)
            {

                IEnumerable<GuiaViewModel> guiasVM = Mapper.Map<IEnumerable<guia>, IEnumerable<GuiaViewModel>>(guias);
                
                PaginacaoConfig<GuiaViewModel> pagSet = new PaginacaoConfig<GuiaViewModel>()
                {

                    Items = guiasVM
                };
                
                response = request.CreateResponse(HttpStatusCode.OK, pagSet);


                return response;
            }
            else
            {

                response = request.CreateResponse(HttpStatusCode.NotFound,"Guia não existe");
            }

            return response;
        }


        
    }
}
