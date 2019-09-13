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
    [RoutePrefix("api/instituicao")]
    public class InstituicaoController : ApiController
    {

        ModeloBancoEntities db = new ModeloBancoEntities();

        [HttpGet]
        [Route("listainstituicao/{page:int=0}/{pageSize=4}/{filter?}")]
        public HttpResponseMessage GetInstituicao(HttpRequestMessage request, int? page, int? pageSize, string filter = null)
        {

            int currentPage = page.Value;
            int currentPageSize = pageSize.Value;
            int totalInstituicaos = new int();

            HttpResponseMessage response = null;



            List<instituicao> instituicaos = db.instituicao.OrderBy(k => k.nome_instituicao)
                .Skip(currentPage * currentPageSize)
                .Take(currentPageSize)
                .ToList();


            totalInstituicaos = db.instituicao.Count();


            IEnumerable<InstituicaoViewModel> instituicaosVM = Mapper.Map<IEnumerable<instituicao>, IEnumerable<InstituicaoViewModel>>(instituicaos);

            PaginacaoConfig<InstituicaoViewModel> pagSet = new PaginacaoConfig<InstituicaoViewModel>()
            {
                Page = currentPage,
                TotalCount = totalInstituicaos,
                TotalPages = (int)Math.Ceiling((decimal)totalInstituicaos / currentPageSize),
                Items = instituicaosVM
            };


            response = request.CreateResponse(HttpStatusCode.OK, pagSet);


            return response;
        }

        [HttpGet]
        [Route("instituicaobyid/{id:int=0}")]
        public HttpResponseMessage GetInstituicaoById(HttpRequestMessage request, int? id)
        {

            HttpResponseMessage response = null;

            instituicao instituicao = db.instituicao.FirstOrDefault(i => i.id_instituicao == id);

            InstituicaoViewModel instituicaoViewModel = Mapper.Map<instituicao, InstituicaoViewModel>(instituicao);

            return request.CreateResponse(HttpStatusCode.OK, instituicaoViewModel);

        }



        [HttpPost]
        [Route("inseririnstituicao")]
        public HttpResponseMessage InserirInstituicao(HttpRequestMessage request, InstituicaoViewModel instituicaoViewModel)
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


                instituicao novoInstituicao = new instituicao()
                {
                    nome_instituicao = instituicaoViewModel.NomeInstituicao
                };

                db.instituicao.Add(novoInstituicao);
                db.SaveChanges();

                // Update view model
                instituicaoViewModel = Mapper.Map<instituicao, InstituicaoViewModel>(novoInstituicao);

                response = request.CreateResponse(HttpStatusCode.OK, instituicaoViewModel);

            }

            return response;

        }


        [HttpPost]
        [Route("alterarinstituicao")]
        public HttpResponseMessage AlterarInstituicao(HttpRequestMessage request, InstituicaoViewModel instituicaoViewModel)
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

                instituicao novoInstituicao = new instituicao
                {
                    id_instituicao = instituicaoViewModel.IdInstituicao,
                    nome_instituicao = instituicaoViewModel.NomeInstituicao
                };

                db.Entry(novoInstituicao).State = EntityState.Modified;
                db.SaveChanges();

                instituicaoViewModel = Mapper.Map<instituicao, InstituicaoViewModel>(novoInstituicao);

                response = request.CreateResponse(HttpStatusCode.OK, instituicaoViewModel);
            }

            return response;

        }

        [HttpPost]
        [Route("excluirinstituicao")]
        public HttpResponseMessage ExcluirInstituicao(HttpRequestMessage request, InstituicaoViewModel instituicaoViewModel)
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

                instituicao excluirInstituicao = new instituicao
                {
                    id_instituicao = instituicaoViewModel.IdInstituicao
                };

                db.Entry(excluirInstituicao).State = EntityState.Deleted;
                db.SaveChanges();

                instituicaoViewModel = Mapper.Map<instituicao, InstituicaoViewModel>(excluirInstituicao);

                response = request.CreateResponse(HttpStatusCode.OK, instituicaoViewModel);
            }

            return response;

        }

    }
}
