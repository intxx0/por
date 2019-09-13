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
    [RoutePrefix("api/missao")]
    public class MissaoController : ApiController
    {

        ModeloBancoEntities db = new ModeloBancoEntities();

        [HttpGet]
        [Route("listamissao/{page:int=0}/{pageSize=4}/{filter?}")]
        public HttpResponseMessage GetMissao(HttpRequestMessage request, int? page, int? pageSize, string filter = null)
        {

            int currentPage = page.Value;
            int currentPageSize = pageSize.Value;
            int totalMissaos = new int();

            HttpResponseMessage response = null;



            List<missao> missaos = db.missao.OrderBy(k => k.desc_missao)
                .Skip(currentPage * currentPageSize)
                .Take(currentPageSize)
                .ToList();


            totalMissaos = db.missao.Count();


            IEnumerable<MissaoViewModel> missaosVM = Mapper.Map<IEnumerable<missao>, IEnumerable<MissaoViewModel>>(missaos);

            PaginacaoConfig<MissaoViewModel> pagSet = new PaginacaoConfig<MissaoViewModel>()
            {
                Page = currentPage,
                TotalCount = totalMissaos,
                TotalPages = (int)Math.Ceiling((decimal)totalMissaos / currentPageSize),
                Items = missaosVM
            };


            response = request.CreateResponse(HttpStatusCode.OK, pagSet);


            return response;
        }


        [HttpPost]
        [Route("inserirmissao")]
        public HttpResponseMessage InserirMissao(HttpRequestMessage request, MissaoViewModel missaoViewModel)
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


                missao novoMissao = new missao()
                {
                    desc_missao = missaoViewModel.DescMissao
                };

                db.missao.Add(novoMissao);
                db.SaveChanges();

                // Update view model
                missaoViewModel = Mapper.Map<missao, MissaoViewModel>(novoMissao);

                response = request.CreateResponse(HttpStatusCode.OK, missaoViewModel);

            }

            return response;

        }


        [HttpPost]
        [Route("alterarmissao")]
        public HttpResponseMessage AlterarMissao(HttpRequestMessage request, MissaoViewModel missaoViewModel)
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

                missao novoMissao = new missao
                {
                    id_missao = missaoViewModel.IdMissao,
                    desc_missao = missaoViewModel.DescMissao
                };

                db.Entry(novoMissao).State = EntityState.Modified;
                db.SaveChanges();

                missaoViewModel = Mapper.Map<missao, MissaoViewModel>(novoMissao);

                response = request.CreateResponse(HttpStatusCode.OK, missaoViewModel);
            }

            return response;

        }

        [HttpPost]
        [Route("excluirmissao")]
        public HttpResponseMessage ExcluirMissao(HttpRequestMessage request, MissaoViewModel missaoViewModel)
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

                missao excluirMissao = new missao
                {
                    id_missao = missaoViewModel.IdMissao
                };

                db.Entry(excluirMissao).State = EntityState.Deleted;
                db.SaveChanges();

                missaoViewModel = Mapper.Map<missao, MissaoViewModel>(excluirMissao);

                response = request.CreateResponse(HttpStatusCode.OK, missaoViewModel);
            }

            return response;

        }

    }
}
