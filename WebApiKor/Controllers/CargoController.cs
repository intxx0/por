﻿using System;
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
    [RoutePrefix("api/cargo")]
    public class CargoController : ApiController
    {

        ModeloBancoEntities db = new ModeloBancoEntities();

        [HttpGet]
        [Route("listacargo/{page:int=0}/{pageSize=4}/{filter?}")]
        public HttpResponseMessage GetCargo(HttpRequestMessage request, int? page, int? pageSize, string filter = null)
        {

            int currentPage = page.Value;
            int currentPageSize = pageSize.Value;
            int totalCargos = new int();

            HttpResponseMessage response = null;



            List<cargo> cargos = db.cargo.OrderBy(c => c.nome_cargo)
                .Skip(currentPage * currentPageSize)
                .Take(currentPageSize)
                .ToList();


            totalCargos = db.cargo.Count();


            IEnumerable<CargoViewModel> cargosVM = Mapper.Map<IEnumerable<cargo>, IEnumerable<CargoViewModel>>(cargos);

            PaginacaoConfig<CargoViewModel> pagSet = new PaginacaoConfig<CargoViewModel>()
            {
                Page = currentPage,
                TotalCount = totalCargos,
                TotalPages = (int)Math.Ceiling((decimal)totalCargos / currentPageSize),
                Items = cargosVM
            };


            response = request.CreateResponse(HttpStatusCode.OK, pagSet);


            return response;
        }


        [HttpPost]
        [Route("inserircargo")]
        public HttpResponseMessage InserirCargo(HttpRequestMessage request, CargoViewModel cargoViewModel)
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


                cargo novoCargo = new cargo()
                {
                    nome_cargo = cargoViewModel.NomeCargo
                };

                db.cargo.Add(novoCargo);
                db.SaveChanges();

                // Update view model
                cargoViewModel = Mapper.Map<cargo, CargoViewModel>(novoCargo);

                response = request.CreateResponse(HttpStatusCode.OK, cargoViewModel);

            }

            return response;

        }


        [HttpPost]
        [Route("alterarcargo")]
        public HttpResponseMessage AlterarCargo(HttpRequestMessage request, CargoViewModel cargoViewModel)
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

                cargo novoCargo = new cargo
                {
                    id_cargo = cargoViewModel.IdCargo,
                    nome_cargo = cargoViewModel.NomeCargo
                };

                db.Entry(novoCargo).State = EntityState.Modified;
                db.SaveChanges();

                cargoViewModel = Mapper.Map<cargo, CargoViewModel>(novoCargo);

                response = request.CreateResponse(HttpStatusCode.OK, cargoViewModel);
            }

            return response;

        }

        [HttpPost]
        [Route("excluircargo")]
        public HttpResponseMessage ExcluirCargo(HttpRequestMessage request, CargoViewModel cargoViewModel)
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

                cargo excluirCargo = new cargo
                {
                    id_cargo = cargoViewModel.IdCargo
                };

                db.Entry(excluirCargo).State = EntityState.Deleted;
                db.SaveChanges();

                cargoViewModel = Mapper.Map<cargo, CargoViewModel>(excluirCargo);

                response = request.CreateResponse(HttpStatusCode.OK, cargoViewModel);
            }

            return response;

        }

    }
}
