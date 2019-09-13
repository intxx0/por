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
    [RoutePrefix("api/materiaprima")]
    public class MateriaPrimaController : ApiController
    {

        ModeloBancoEntities db = new ModeloBancoEntities();

        [HttpGet]
        [Route("listamateriaprima/{page:int=0}/{pageSize=4}/{filter?}")]
        public HttpResponseMessage GetMateriaPrima(HttpRequestMessage request, int? page, int? pageSize, string filter = null)
        {

            int currentPage = page.Value;
            int currentPageSize = pageSize.Value;
            int totalMateriaPrimas = new int();

            HttpResponseMessage response = null;



            List<tipo_materia_prima> materiaprimas = db.tipo_materia_prima.OrderBy(k => k.desc_materia_prima)
                .Skip(currentPage * currentPageSize)
                .Take(currentPageSize)
                .ToList();


            totalMateriaPrimas = db.tipo_materia_prima.Count();


            IEnumerable<MateriaPrimaViewModel> materiaprimasVM = Mapper.Map<IEnumerable<tipo_materia_prima>, IEnumerable<MateriaPrimaViewModel>>(materiaprimas);

            PaginacaoConfig<MateriaPrimaViewModel> pagSet = new PaginacaoConfig<MateriaPrimaViewModel>()
            {
                Page = currentPage,
                TotalCount = totalMateriaPrimas,
                TotalPages = (int)Math.Ceiling((decimal)totalMateriaPrimas / currentPageSize),
                Items = materiaprimasVM
            };


            response = request.CreateResponse(HttpStatusCode.OK, pagSet);


            return response;
        }


        [HttpPost]
        [Route("inserirmateriaprima")]
        public HttpResponseMessage InserirMateriaPrima(HttpRequestMessage request, MateriaPrimaViewModel materiaprimaViewModel)
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


                tipo_materia_prima novoMateriaPrima = new tipo_materia_prima()
                {
                    desc_materia_prima = materiaprimaViewModel.DescMateriaPrima
                };

                db.tipo_materia_prima.Add(novoMateriaPrima);
                db.SaveChanges();

                // Update view model
                materiaprimaViewModel = Mapper.Map<tipo_materia_prima, MateriaPrimaViewModel>(novoMateriaPrima);

                response = request.CreateResponse(HttpStatusCode.OK, materiaprimaViewModel);

            }

            return response;

        }


        [HttpPost]
        [Route("alterarmateriaprima")]
        public HttpResponseMessage AlterarMateriaPrima(HttpRequestMessage request, MateriaPrimaViewModel materiaprimaViewModel)
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

                tipo_materia_prima novoMateriaPrima = new tipo_materia_prima
                {
                    id_tipo_materia_prima = materiaprimaViewModel.IdMateriaPrima,
                    desc_materia_prima = materiaprimaViewModel.DescMateriaPrima
                };

                db.Entry(novoMateriaPrima).State = EntityState.Modified;
                db.SaveChanges();

                materiaprimaViewModel = Mapper.Map<tipo_materia_prima, MateriaPrimaViewModel>(novoMateriaPrima);

                response = request.CreateResponse(HttpStatusCode.OK, materiaprimaViewModel);
            }

            return response;

        }

        [HttpPost]
        [Route("excluirmateriaprima")]
        public HttpResponseMessage ExcluirMateriaPrima(HttpRequestMessage request, MateriaPrimaViewModel materiaprimaViewModel)
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

                tipo_materia_prima excluirMateriaPrima = new tipo_materia_prima
                {
                    id_tipo_materia_prima = materiaprimaViewModel.IdMateriaPrima
                };

                db.Entry(excluirMateriaPrima).State = EntityState.Deleted;
                db.SaveChanges();

                materiaprimaViewModel = Mapper.Map<tipo_materia_prima, MateriaPrimaViewModel>(excluirMateriaPrima);

                response = request.CreateResponse(HttpStatusCode.OK, materiaprimaViewModel);
            }

            return response;

        }

    }
}
