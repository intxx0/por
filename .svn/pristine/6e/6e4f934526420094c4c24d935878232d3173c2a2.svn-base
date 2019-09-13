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
    [RoutePrefix("api/kor")]
    public class KorController : ApiController
    {

        ModeloBancoEntities db = new ModeloBancoEntities();

        [HttpGet]
        [Route("listakor/{page:int=0}/{pageSize=4}/{filter?}")]
        public HttpResponseMessage GetKor(HttpRequestMessage request, int? page, int? pageSize, string filter = null)
        {

            int currentPage = page.Value;
            int currentPageSize = pageSize.Value;
            int totalKors = new int();

            HttpResponseMessage response = null;


            List<kor> kors = db.kor.OrderBy(k => k.nome_kor)
                .Skip(currentPage * currentPageSize)
                .Take(currentPageSize)
                .ToList();


            totalKors = db.kor.Count();


            IEnumerable<KorViewModel> korsVM = Mapper.Map<IEnumerable<kor>, IEnumerable<KorViewModel>>(kors);

            PaginacaoConfig<KorViewModel> pagSet = new PaginacaoConfig<KorViewModel>()
            {
                Page = currentPage,
                TotalCount = totalKors,
                TotalPages = (int)Math.Ceiling((decimal)totalKors / currentPageSize),
                Items = korsVM
            };


            response = request.CreateResponse(HttpStatusCode.OK, pagSet);


            return response;
        }

        [HttpGet]
        [Route("listakorbyativadade/{IdAtividade}")]
        public HttpResponseMessage GetKorByAtividade(HttpRequestMessage request, int IdAtividade)
        {
            HttpResponseMessage response = null;

            List<int?> IdsKor = db.operacao_usuario_kor
                .Where(ouk => ouk.id_tipo_fiscalizacao == IdAtividade)
                .Select(x => x.id_kor).ToList();

            List<kor> kors = db.kor.Where(k => IdsKor.Contains(k.id_kor)).ToList();


            List<KorViewModel> KorVM = Mapper.Map<List<kor>, List<KorViewModel>>(kors);

            response = request.CreateResponse(HttpStatusCode.OK, KorVM);

            return response;
        }


        [HttpPost]
        [Route("inserirkor")]
        public HttpResponseMessage InserirKor(HttpRequestMessage request, KorViewModel korViewModel)
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


                kor novoKor = new kor()
                {
                    nome_kor = korViewModel.Nome,
                    modelo_kor = korViewModel.Modelo,
                    numero_serie_kor = korViewModel.NumeroSerie,
                    ativo = korViewModel.Ativo
                };

                db.kor.Add(novoKor);
                db.SaveChanges();

                // Update view model
                korViewModel = Mapper.Map<kor, KorViewModel>(novoKor);

                response = request.CreateResponse(HttpStatusCode.OK, korViewModel);

            }

            return response;

        }


        [HttpPost]
        [Route("alterarkor")]
        public HttpResponseMessage AlterarKor(HttpRequestMessage request, KorViewModel korViewModel)
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

                kor novoKor = new kor
                {
                    id_kor = korViewModel.IdKor,
                    nome_kor = korViewModel.Nome,
                    modelo_kor = korViewModel.Modelo,
                    numero_serie_kor = korViewModel.NumeroSerie,
                    ativo = korViewModel.Ativo
                };

                db.Entry(novoKor).State = EntityState.Modified;
                db.SaveChanges();

                korViewModel = Mapper.Map<kor, KorViewModel>(novoKor);

                response = request.CreateResponse(HttpStatusCode.OK, korViewModel);
            }

            return response;

        }

        [HttpPost]
        [Route("excluirkor")]
        public HttpResponseMessage ExcluirKor(HttpRequestMessage request, KorViewModel korViewModel)
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

                kor excluirKor = new kor
                {
                    id_kor = korViewModel.IdKor
                };

                db.Entry(excluirKor).State = EntityState.Deleted;
                db.SaveChanges();

                korViewModel = Mapper.Map<kor, KorViewModel>(excluirKor);

                response = request.CreateResponse(HttpStatusCode.OK, korViewModel);
            }

            return response;

        }

    }
}
