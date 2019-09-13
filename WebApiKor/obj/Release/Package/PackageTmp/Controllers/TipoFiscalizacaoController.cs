using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using WebApiKor.InfraWeb;
using WebApiKor.Models;

namespace WebApiKor.Controllers
{
    [Authorize(Roles = "Administrador")]
    [RoutePrefix("api/tipofiscalizacao")]
    public class TipoFiscalizacaoController : ApiController
    {
        ModeloBancoEntities db = new ModeloBancoEntities();



        [HttpGet]
        [Route("listatipofiscalizacao/{page:int=0}/{pageSize=4}/{filter?}")]
        public HttpResponseMessage GetTipoFiscalizacao(HttpRequestMessage request, int? page, int? pageSize, string filter = null)
        {

            int currentPage = page.Value;
            int currentPageSize = pageSize.Value;
            int totaltipofiscalizacao = new int();

            HttpResponseMessage response = null;



            List<tipo_fiscalizacao> tipofiscalizacoes = db.tipo_fiscalizacao.OrderBy(k => k.desc_tipo_fiscalizacao)
                .Skip(currentPage * currentPageSize)
                .Take(currentPageSize)
                .ToList();


            totaltipofiscalizacao = db.tipo_fiscalizacao.Count();


            IEnumerable<TipoFiscalizacaoViewModel> tipofiscalizacoesVM = Mapper.Map<IEnumerable<tipo_fiscalizacao>, IEnumerable<TipoFiscalizacaoViewModel>>(tipofiscalizacoes);

            PaginacaoConfig<TipoFiscalizacaoViewModel> pagSet = new PaginacaoConfig<TipoFiscalizacaoViewModel>()
            {
                Page = currentPage,
                TotalCount = totaltipofiscalizacao,
                TotalPages = (int)Math.Ceiling((decimal)totaltipofiscalizacao / currentPageSize),
                Items = tipofiscalizacoesVM
            };


            response = request.CreateResponse(HttpStatusCode.OK, pagSet);


            return response;
        }





        [HttpPost]
        [Route("inserirtipofiscalizacao")]
        public HttpResponseMessage InserirTipoFiscalizacao(HttpRequestMessage request, TipoFiscalizacaoViewModel tipoFiscalizacaoViewModel)
        {

            HttpResponseMessage response = null;

            if (!ModelState.IsValid)
            {
                response = request.CreateResponse(HttpStatusCode.BadRequest,
                    ModelState.Keys.SelectMany(k => ModelState[k].Errors)
                          .Select(m => m.ErrorMessage).ToArray());
            }
            else
            {


                tipo_fiscalizacao novoTipoFiscalizacao = new tipo_fiscalizacao()
                {
                  desc_tipo_fiscalizacao = tipoFiscalizacaoViewModel.DescTipoFiscalizacao,
                  ativo = tipoFiscalizacaoViewModel.Ativo
                };

                db.tipo_fiscalizacao.Add(novoTipoFiscalizacao);
                db.SaveChanges();

                // Update view model
                tipoFiscalizacaoViewModel = Mapper.Map<tipo_fiscalizacao, TipoFiscalizacaoViewModel>(novoTipoFiscalizacao);

                response = request.CreateResponse(HttpStatusCode.OK, tipoFiscalizacaoViewModel);

            }

            return response;

        }


        [HttpPost]
        [Route("alterartipofiscalizacao")]
        public HttpResponseMessage AlterarKor(HttpRequestMessage request, TipoFiscalizacaoViewModel tipoFiscalizacaoViewModel)
        {
            HttpResponseMessage response = null;

            if (!ModelState.IsValid)
            {
                response = request.CreateResponse(HttpStatusCode.BadRequest,
                   ModelState.Keys.SelectMany(k => ModelState[k].Errors)
                         .Select(m => m.ErrorMessage).ToArray());
            }

            else

            {

                tipo_fiscalizacao novoTipoFiscalizacao = new tipo_fiscalizacao()
                {
                   id_tipo_fiscalizacao = tipoFiscalizacaoViewModel.IdTipoFiscalizacao,
                   desc_tipo_fiscalizacao = tipoFiscalizacaoViewModel.DescTipoFiscalizacao,
                   ativo = tipoFiscalizacaoViewModel.Ativo
                };

                db.Entry(novoTipoFiscalizacao).State = EntityState.Modified;
                db.SaveChanges();

                tipoFiscalizacaoViewModel = Mapper.Map<tipo_fiscalizacao, TipoFiscalizacaoViewModel>(novoTipoFiscalizacao);

                response = request.CreateResponse(HttpStatusCode.OK, tipoFiscalizacaoViewModel);
            }

            return response;

        }



    }
}
