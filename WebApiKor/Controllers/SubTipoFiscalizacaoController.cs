﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.SessionState;
using System.Web.Http.Description;
using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using WebApiKor;
using WebApiKor.InfraWeb;
using WebApiKor.Models;

namespace WebApiKor.Controllers
{
    [RoutePrefix("api/subtipofiscalizacao")]

    [Authorize(Roles = "Administrador, Operador")]
    public class SubTipoFiscalizacaoController : ApiController
    {
        ModeloBancoEntities db = new ModeloBancoEntities();

        [AllowAnonymous]
        [HttpGet]
        [Route("listasubtipofiscalizacao/{page:int=0}/{pageSize=4}/{filter?}")]
        public HttpResponseMessage GetSubTipoFiscalizacao(HttpRequestMessage request, int? page, int? pageSize, string filter = null)
        {

            int currentPage = page.Value;
            int currentPageSize = pageSize.Value;
            int totalSubTiposFiscalizacao = new int();


            HttpResponseMessage response = null;


            List<subtipo_fiscalizacao> subtiposfiscalizacao = db.subtipo_fiscalizacao.OrderBy(stf => stf.desc_subtipo)
                .Skip(currentPage * currentPageSize)
                .Take(currentPageSize)
                .ToList();

            totalSubTiposFiscalizacao = db.subtipo_fiscalizacao.Count();

            IEnumerable<SubTipoFiscalizacaoViewModel> subtiposfiscalizacaoVM = Mapper.Map<IEnumerable<subtipo_fiscalizacao>, IEnumerable<SubTipoFiscalizacaoViewModel>>(subtiposfiscalizacao);


            PaginacaoConfig<SubTipoFiscalizacaoViewModel> pagSet = new PaginacaoConfig<SubTipoFiscalizacaoViewModel>()
            {
                Page = currentPage,
                TotalCount = totalSubTiposFiscalizacao,
                TotalPages = (int)Math.Ceiling((decimal)totalSubTiposFiscalizacao / currentPageSize),
                Items = subtiposfiscalizacaoVM
            };


            response = request.CreateResponse(HttpStatusCode.OK, pagSet);


            return response;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("alterarsubtipofiscalizacao")]
        public HttpResponseMessage AlterarSubTiposFiscalizacao(HttpRequestMessage request, SubTipoFiscalizacaoViewModel subtipofiscalizacaoViewModel)
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

                subtipo_fiscalizacao subtipofiscalizacao = new subtipo_fiscalizacao
                {
                    id_subtipo_fiscalizacao = subtipofiscalizacaoViewModel.IdSubTipoFiscalizacao,
                    id_tipo_fiscalizacao = subtipofiscalizacaoViewModel.IdTipoFiscalizacao,
                    desc_subtipo = subtipofiscalizacaoViewModel.DescSubTipoFiscalizacao
                };

                db.Entry(subtipofiscalizacao).State = EntityState.Modified;
                db.SaveChanges();

                subtipofiscalizacaoViewModel = Mapper.Map<subtipo_fiscalizacao, SubTipoFiscalizacaoViewModel>(subtipofiscalizacao);

                response = request.CreateResponse(HttpStatusCode.OK, subtipofiscalizacaoViewModel);

            }

            return response;

        }

        [AllowAnonymous]
        [HttpPost]
        [Route("inserirsubtipofiscalizacao")]
        public HttpResponseMessage InserirSubTipoFiscalizacao(HttpRequestMessage request, SubTipoFiscalizacaoViewModel subtipofiscalizacaoViewModel)
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

                subtipo_fiscalizacao novoSubTipoFiscalizacao = new subtipo_fiscalizacao()
                {
                    id_tipo_fiscalizacao = subtipofiscalizacaoViewModel.IdTipoFiscalizacao,
                    desc_subtipo = subtipofiscalizacaoViewModel.DescSubTipoFiscalizacao
                };

                db.subtipo_fiscalizacao.Add(novoSubTipoFiscalizacao);
                db.SaveChanges();

                // Update view model
                subtipofiscalizacaoViewModel = Mapper.Map<subtipo_fiscalizacao, SubTipoFiscalizacaoViewModel>(novoSubTipoFiscalizacao);

                response = request.CreateResponse(HttpStatusCode.Created, subtipofiscalizacaoViewModel);

            }

            return response;

        }

        [HttpPost]
        [Route("excluirsubtipofiscalizacao")]
        public HttpResponseMessage ExcluirSubTipoFiscalizacao(HttpRequestMessage request, SubTipoFiscalizacaoViewModel subTipoFiscalizacaoViewModel)
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

                subtipo_fiscalizacao excluirSubTipoFiscalizacao = new subtipo_fiscalizacao
                {
                    id_subtipo_fiscalizacao = subTipoFiscalizacaoViewModel.IdSubTipoFiscalizacao
                };

                db.Entry(excluirSubTipoFiscalizacao).State = EntityState.Deleted;
                db.SaveChanges();

                subTipoFiscalizacaoViewModel = Mapper.Map<subtipo_fiscalizacao, SubTipoFiscalizacaoViewModel>(excluirSubTipoFiscalizacao);

                response = request.CreateResponse(HttpStatusCode.OK, subTipoFiscalizacaoViewModel);
            }

            return response;

        }


    }
}