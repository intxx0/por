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
    [RoutePrefix("api/perfil")]
    public class PerfilController : ApiController
    {

        ModeloBancoEntities db = new ModeloBancoEntities();

        [HttpGet]
        [Route("listaperfil/{page:int=0}/{pageSize=4}/{filter?}")]
        public HttpResponseMessage GetPerfil(HttpRequestMessage request, int? page, int? pageSize, string filter = null)
        {

            int currentPage = page.Value;
            int currentPageSize = pageSize.Value;
            int totalPerfils = new int();

            HttpResponseMessage response = null;



            List<perfil> perfils = db.perfil.OrderBy(k => k.desc_perfil)
                .Skip(currentPage * currentPageSize)
                .Take(currentPageSize)
                .ToList();


            totalPerfils = db.perfil.Count();


            IEnumerable<PerfilViewModel> perfilsVM = Mapper.Map<IEnumerable<perfil>, IEnumerable<PerfilViewModel>>(perfils);

            PaginacaoConfig<PerfilViewModel> pagSet = new PaginacaoConfig<PerfilViewModel>()
            {
                Page = currentPage,
                TotalCount = totalPerfils,
                TotalPages = (int)Math.Ceiling((decimal)totalPerfils / currentPageSize),
                Items = perfilsVM
            };


            response = request.CreateResponse(HttpStatusCode.OK, pagSet);


            return response;
        }


        [HttpPost]
        [Route("inserirperfil")]
        public HttpResponseMessage InserirPerfil(HttpRequestMessage request, PerfilViewModel perfilViewModel)
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


                perfil novoPerfil = new perfil()
                {
                    desc_perfil = perfilViewModel.DescPerfil
                };

                db.perfil.Add(novoPerfil);
                db.SaveChanges();

                // Update view model
                perfilViewModel = Mapper.Map<perfil, PerfilViewModel>(novoPerfil);

                response = request.CreateResponse(HttpStatusCode.OK, perfilViewModel);

            }

            return response;

        }


        [HttpPost]
        [Route("alterarperfil")]
        public HttpResponseMessage AlterarPerfil(HttpRequestMessage request, PerfilViewModel perfilViewModel)
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

                perfil novoPerfil = new perfil
                {
                    id_perfil = perfilViewModel.IdPerfil,
                    desc_perfil = perfilViewModel.DescPerfil
                };

                db.Entry(novoPerfil).State = EntityState.Modified;
                db.SaveChanges();

                perfilViewModel = Mapper.Map<perfil, PerfilViewModel>(novoPerfil);

                response = request.CreateResponse(HttpStatusCode.OK, perfilViewModel);
            }

            return response;

        }

        [HttpPost]
        [Route("excluirperfil")]
        public HttpResponseMessage ExcluirPerfil(HttpRequestMessage request, PerfilViewModel perfilViewModel)
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

                perfil excluirPerfil = new perfil
                {
                    id_perfil = perfilViewModel.IdPerfil
                };

                db.Entry(excluirPerfil).State = EntityState.Deleted;
                db.SaveChanges();

                perfilViewModel = Mapper.Map<perfil, PerfilViewModel>(excluirPerfil);

                response = request.CreateResponse(HttpStatusCode.OK, perfilViewModel);
            }

            return response;

        }

    }
}
