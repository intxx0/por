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
    [RoutePrefix("api/tipoguia")]
    public class TipoGuiaController : ApiController
    {

        ModeloBancoEntities db = new ModeloBancoEntities();

        [HttpGet]
        [Route("listatipoguia/{page:int=0}/{pageSize=4}/{filter?}")]
        public HttpResponseMessage GetTipoGuia(HttpRequestMessage request, int? page, int? pageSize, string filter = null)
        {

            int currentPage = page.Value;
            int currentPageSize = pageSize.Value;
            int totalTipoGuias = new int();

            HttpResponseMessage response = null;



            List<tipo_guia> tipoguias = db.tipo_guia.OrderBy(k => k.desc_tipo_guia)
                .Skip(currentPage * currentPageSize)
                .Take(currentPageSize)
                .ToList();


            totalTipoGuias = db.tipo_guia.Count();


            IEnumerable<TipoGuiaViewModel> tipoguiasVM = Mapper.Map<IEnumerable<tipo_guia>, IEnumerable<TipoGuiaViewModel>>(tipoguias);

            PaginacaoConfig<TipoGuiaViewModel> pagSet = new PaginacaoConfig<TipoGuiaViewModel>()
            {
                Page = currentPage,
                TotalCount = totalTipoGuias,
                TotalPages = (int)Math.Ceiling((decimal)totalTipoGuias / currentPageSize),
                Items = tipoguiasVM
            };


            response = request.CreateResponse(HttpStatusCode.OK, pagSet);


            return response;
        }


        [HttpPost]
        [Route("inserirtipoguia")]
        public HttpResponseMessage InserirTipoGuia(HttpRequestMessage request, TipoGuiaViewModel tipoguiaViewModel)
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


                tipo_guia novoTipoGuia = new tipo_guia()
                {
                    desc_tipo_guia = tipoguiaViewModel.DescTipoGuia
                };

                db.tipo_guia.Add(novoTipoGuia);
                db.SaveChanges();

                // Update view model
                tipoguiaViewModel = Mapper.Map<tipo_guia, TipoGuiaViewModel>(novoTipoGuia);

                response = request.CreateResponse(HttpStatusCode.OK, tipoguiaViewModel);

            }

            return response;

        }


        [HttpPost]
        [Route("alterartipoguia")]
        public HttpResponseMessage AlterarTipoGuia(HttpRequestMessage request, TipoGuiaViewModel tipoguiaViewModel)
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

                tipo_guia novoTipoGuia = new tipo_guia
                {
                    id_tipo_guia = tipoguiaViewModel.IdTipoGuia,
                    desc_tipo_guia = tipoguiaViewModel.DescTipoGuia
                };

                db.Entry(novoTipoGuia).State = EntityState.Modified;
                db.SaveChanges();

                tipoguiaViewModel = Mapper.Map<tipo_guia, TipoGuiaViewModel>(novoTipoGuia);

                response = request.CreateResponse(HttpStatusCode.OK, tipoguiaViewModel);
            }

            return response;

        }

        [HttpPost]
        [Route("excluirtipoguia")]
        public HttpResponseMessage ExcluirTipoGuia(HttpRequestMessage request, TipoGuiaViewModel tipoguiaViewModel)
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

                tipo_guia excluirTipoGuia = new tipo_guia
                {
                    id_tipo_guia = tipoguiaViewModel.IdTipoGuia
                };

                db.Entry(excluirTipoGuia).State = EntityState.Deleted;
                db.SaveChanges();

                tipoguiaViewModel = Mapper.Map<tipo_guia, TipoGuiaViewModel>(excluirTipoGuia);

                response = request.CreateResponse(HttpStatusCode.OK, tipoguiaViewModel);
            }

            return response;

        }

    }
}
