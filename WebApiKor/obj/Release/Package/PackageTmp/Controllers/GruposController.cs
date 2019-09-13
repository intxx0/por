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
    [RoutePrefix("api/grupos")]
    
    public class GruposController : ApiController
    {

        ModeloBancoEntities db = new ModeloBancoEntities();

       
        [HttpGet]
        [Route("listagrupos/{page:int=0}/{pageSize=4}/{filter?}")]
        public HttpResponseMessage Grupos(HttpRequestMessage request, int? page, int? pageSize, string filter = null)
        {

            int currentPage = page.Value;
            int currentPageSize = pageSize.Value;
            int totalGrupos = new int();

            HttpResponseMessage response = null;



            List<grupo> grupos = db.grupo.OrderBy(gru => gru.nome)
                .Skip(currentPage * currentPageSize)
                .Take(currentPageSize)
                .ToList();

            totalGrupos = db.grupo.Count();


            IEnumerable<GruposViewModel> gruposVM = Mapper.Map<IEnumerable<grupo>, IEnumerable<GruposViewModel>>(grupos);

            PaginacaoConfig<GruposViewModel> pagSet = new PaginacaoConfig<GruposViewModel>()
            {
                Page = currentPage,
                TotalCount = totalGrupos,
                TotalPages = (int)Math.Ceiling((decimal)totalGrupos / currentPageSize),
                Items = gruposVM
            };
            
         
            response = request.CreateResponse(HttpStatusCode.OK, pagSet);


            return response;
        }



        [HttpPost]
        [Route("inserirgrupo")]
        public HttpResponseMessage InserirGrupo(HttpRequestMessage request, GruposViewModel grupoViewModel)
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


                grupo novoGrupo = new grupo()
                {
                    nome = grupoViewModel.DescGrupo,
                    datacriado = DateTime.Now,
                    ativo = grupoViewModel.Ativo,
                    datamodificado = DateTime.Now

                };

                db.grupo.Add(novoGrupo);
                db.SaveChanges();

                // Atualizar view model
                grupoViewModel = Mapper.Map<grupo, GruposViewModel>(novoGrupo);

                response = request.CreateResponse(HttpStatusCode.Created, grupoViewModel);

            }

            return response;

        }


       
        [HttpPost]
        [Route("alterargrupo")]
        public HttpResponseMessage AlterarGrupos(HttpRequestMessage request, GruposViewModel grupoViewModel)
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
                
                grupo grupo = new grupo
                {
                    grupo_id = grupoViewModel.IdGrupo,
                    nome = grupoViewModel.DescGrupo,
                    datamodificado = DateTime.Now,
                    ativo = grupoViewModel.Ativo,
                    datacriado = grupoViewModel.DataCriado

                };

                db.Entry(grupo).State = EntityState.Modified;
                db.SaveChanges();

                grupoViewModel = Mapper.Map<grupo, GruposViewModel>(grupo);

                response = request.CreateResponse(HttpStatusCode.OK, grupoViewModel);
                
            }

            return response;

        }
        


    }
}
