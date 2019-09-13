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
    [RoutePrefix("api/detalhesfiscalizacao")]
    public class DetalhesFiscalizacaoController : ApiController
    {

        ModeloBancoEntities db = new ModeloBancoEntities();

        [HttpGet]
        [Route("listadetalhesfiscalizacaooffline/{idOperacao:int}")]
        public HttpResponseMessage GetFiscalizacaoOffLine(HttpRequestMessage request, int? idOperacao)
        {
            
            HttpResponseMessage response = null;
            

            List<fiscalizacao> fiscalizacoes = db.fiscalizacao.Where(fisc => fisc.id_operacao == idOperacao && fisc.ativo == 1).OrderBy(fisc => fisc.data_fiscalizacao).ToList();
            

            IEnumerable<DetalhesFiscalizacaoViewModel> fiscalizacoesVM = Mapper.Map<IEnumerable<fiscalizacao>, IEnumerable<DetalhesFiscalizacaoViewModel>>(fiscalizacoes);

            PaginacaoConfig<DetalhesFiscalizacaoViewModel> pagSet = new PaginacaoConfig<DetalhesFiscalizacaoViewModel>()
            {
               
                Items = fiscalizacoesVM
            };


            response = request.CreateResponse(HttpStatusCode.OK, pagSet);


            return response;
        }




        [HttpGet]
        [Route("listadetalhesfiscalizacaoonline/{idOperacao:int}")]
        public HttpResponseMessage GetFiscalizacaoOnLine(HttpRequestMessage request, int? idOperacao)
        {

            HttpResponseMessage response = null;


            List<fiscalizacao> fiscalizacoes = db.fiscalizacao.Where(fisc => fisc.id_operacao == idOperacao && fisc.ativo == 1).OrderBy(fisc => fisc.data_fiscalizacao).ToList();


            IEnumerable<DetalhesFiscalizacaoViewModel> fiscalizacoesVM = Mapper.Map<IEnumerable<fiscalizacao>, IEnumerable<DetalhesFiscalizacaoViewModel>>(fiscalizacoes);

            PaginacaoConfig<DetalhesFiscalizacaoViewModel> pagSet = new PaginacaoConfig<DetalhesFiscalizacaoViewModel>()
            {

                Items = fiscalizacoesVM
            };


            response = request.CreateResponse(HttpStatusCode.OK, pagSet);


            return response;
        }




    }
}
