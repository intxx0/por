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
    [RoutePrefix("api/operacoes")]
    public class OperacoesController : ApiController
    {

        ModeloBancoEntities db = new ModeloBancoEntities();

        [HttpGet]
        [Route("listaoperacao/{page:int=0}/{pageSize=4}/{filter?}")]
        public HttpResponseMessage GetPerfil(HttpRequestMessage request, int? page, int? pageSize, string filter = null)
        {

            int currentPage = page.Value;
            int currentPageSize = pageSize.Value;
            int totalOperacoes = new int();

            List<operacao> operacoes = db.operacao.OrderBy(k => k.desc_operacao)
                .Skip(currentPage * currentPageSize)
                .Take(currentPageSize)
                .ToList();

            totalOperacoes = db.operacao.Count();

            IEnumerable<OperacaoViewModel> operacoesVM = Mapper.Map<IEnumerable<operacao>, IEnumerable<OperacaoViewModel>>(operacoes);
            
            PaginacaoConfig<OperacaoViewModel> pagSet = new PaginacaoConfig<OperacaoViewModel>()
            {
                Page = currentPage,
                TotalCount = totalOperacoes,
                TotalPages = (int)Math.Ceiling((decimal)totalOperacoes / currentPageSize),
                Items = operacoesVM
            };

            return request.CreateResponse(HttpStatusCode.OK, pagSet);

        }

        [HttpGet]
        [Route("getdetalhesoperacao/{idOperacao}")]
        public HttpResponseMessage GetDetalhesOperacao(HttpRequestMessage request, int idOperacao)
        {
            if (idOperacao < 1)
                return request.CreateResponse(HttpStatusCode.BadRequest);

            var operacaoId = Convert.ToInt32(idOperacao);

            /////////////////////Seleciona Lista de operações///////////////////////////
            var rowSet = from _op in db.operacao

                         select new
                         {
                             IdOperacao = _op.id_operacao,
                             NomeOperacao = _op.desc_operacao,
                             DescOperacao = _op.desc_observacao,
                             DataInicioOperacao = _op.data_criado_operacao,
                             DataFimOperacao = _op.data_final_operacao,
                             Localizacao = _op.localizacao
                         };
            ////////////////Insere Apenas a operação com o ID enviado na variavel ROW////////////
            rowSet = rowSet.Where(op => op.IdOperacao == operacaoId);
            var row = rowSet.FirstOrDefault();

            /////////////////////Seleciona Lista de Fiscalizações/////////////////////////
            var rowSetFisc = from _f in db.fiscalizacao
                             join _o in db.operacao on _f.id_operacao equals _o.id_operacao
                             join _k in db.kor on _f.id_kor equals _k.id_kor
                             join _g in db.guia on _f.id_guia equals _g.id_guia
                             join _st in db.status_fiscalizacao on _f.id_status_fiscalizacao equals _st.id_status_fiscalizacao

                             where _f.id_operacao == _o.id_operacao

                             select new
                             {
                                 IdFiscalizacao = _f.id_fiscalizacao,
                                 IdOperacao = _f.id_operacao,
                                 DescFisc = _f.desc_fiscalizacao,
                                 NumeroGuia = _g.numero_guia,
                                 Status = _st.desc_status_fiscalizacao,
                                 NumeroAutuacao = _f.numero_autuacao,
                                 DataFiscalizacao = _f.data_fiscalizacao,
                                 Kor = _k.nome_kor,
                                 Posto = _f.posto
                             };

          

            DetalhesOperacaoViewModel detalhesOperacaoVM = new DetalhesOperacaoViewModel();

            detalhesOperacaoVM.operacao = row;

            detalhesOperacaoVM.fiscalizacoes = rowSetFisc.ToList();


            return request.CreateResponse(HttpStatusCode.OK, detalhesOperacaoVM);
        }

        /*
        [HttpGet]
        [Route("listaoperacaobynatureza/{idNatureza}/{page:int=0}/{pageSize=4}/{filter?}")]
        public HttpResponseMessage GetOperacoes(HttpRequestMessage request, int idNatureza, int? page, int? pageSize, string filter = null)
        {
            int currentPage = page.Value;
            int currentPageSize = pageSize.Value;
            int totalOperacoes = new int();

            List<operacao> operacoes = db.operacao.OrderBy(k => k.desc_operacao)
                .Where(op => op.id_natureza_operacao == idNatureza)
                .Skip(currentPage * currentPageSize)
                .Take(currentPageSize)
                .ToList();

            totalOperacoes = db.operacao.Count();

            IEnumerable<OperacaoViewModel> operacoesVM = Mapper.Map<IEnumerable<operacao>, IEnumerable<OperacaoViewModel>>(operacoes);

            PaginacaoConfig<OperacaoViewModel> pagSet = new PaginacaoConfig<OperacaoViewModel>()
            {
                Page = currentPage,
                TotalCount = totalOperacoes,
                TotalPages = (int)Math.Ceiling((decimal)totalOperacoes / currentPageSize),
                Items = operacoesVM
            };

            return request.CreateResponse(HttpStatusCode.OK, pagSet);

        }
        */
    }
}
