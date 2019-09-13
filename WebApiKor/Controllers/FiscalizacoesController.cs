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
    [RoutePrefix("api/fiscalizacoes")]
    public class FiscalizacoesController : ApiController
    {

        ModeloBancoEntities db = new ModeloBancoEntities();

        [AllowAnonymous]
        [HttpPost]
        [Route("listafiscalizacao")]
        public HttpResponseMessage GetFiscalizacoes(HttpRequestMessage request, BuscaFiscalizacoesViewModel buscaViewModel)
        {

            var rowSet = from _f in db.fiscalizacao
                         join _o in db.operacao on _f.id_operacao equals _o.id_operacao
                         join _g in db.guia on _f.id_guia equals _g.id_guia

                         select new
                         {
                             IdFiscalizacao = _f.id_fiscalizacao,
                             IdKor = (int?)_f.id_kor, 
                             IdOperacao = _o.id_operacao,
                             DescOperacao = _o.desc_operacao, 
                             GuiaEmissor = _g.nome_emissor, 
                             GuiaNumero = _g.numero_guia, 
                             DescFiscalizacao = _f.desc_fiscalizacao,
                             DataFiscalizacao = _f.data_fiscalizacao, 
                             NumeroAutuacao = _f.numero_autuacao, 
                             IdTipoFiscalizacao = (int?)_f.id_tipo_fiscalizacao, 
                         };

            if (buscaViewModel.IdOperacao > 0)
                rowSet = rowSet.Where(f => f.IdOperacao == buscaViewModel.IdOperacao);

            if (buscaViewModel.IdKor > 0)
                rowSet = rowSet.Where(f => f.IdKor == buscaViewModel.IdKor);

            if (buscaViewModel.DataInicio != null && buscaViewModel.DataTermino != null)
            {
                DateTime dataInicio = new DateTime(Convert.ToInt32(buscaViewModel.DataInicio.Substring(0, 4)),
                                                   Convert.ToInt32(buscaViewModel.DataInicio.Substring(2, 2)),
                                                   Convert.ToInt32(buscaViewModel.DataInicio.Substring(2, 2)));
                DateTime dataTermino = new DateTime(Convert.ToInt32(buscaViewModel.DataTermino.Substring(0, 4)),
                                                   Convert.ToInt32(buscaViewModel.DataTermino.Substring(2, 2)),
                                                   Convert.ToInt32(buscaViewModel.DataTermino.Substring(2, 2)));

                rowSet = rowSet.Where(f => DbFunctions.TruncateTime(f.DataFiscalizacao) >= dataInicio && 
                                           DbFunctions.TruncateTime(f.DataFiscalizacao) <= dataTermino);
            }

            if (buscaViewModel.IdTipoFiscalizacao > 0)
                rowSet = rowSet.Where(f => f.IdTipoFiscalizacao == buscaViewModel.IdTipoFiscalizacao);


            var fiscalizacoes = rowSet.ToList();

            return request.CreateResponse(HttpStatusCode.OK, fiscalizacoes);

        }

        [AllowAnonymous]
        [HttpGet]
        [Route("fiscalizacao/{idFiscalizacao:int=0}")]
        public HttpResponseMessage GetFiscalizacaoById(HttpRequestMessage request, int idFiscalizacao)
        {

            if (idFiscalizacao < 1)
            {
                return request.CreateResponse(HttpStatusCode.BadRequest);
            }

            var fiscalizacaoId = Convert.ToInt32(idFiscalizacao);

            var rowSet = from _f in db.fiscalizacao
                         join _o in db.operacao on _f.id_operacao equals _o.id_operacao
                         join _g in db.guia on _f.id_guia equals _g.id_guia
                         join _tg in db.tipo_guia on _g.id_tipo_guia equals _tg.id_tipo_guia
                         
                         join _k in db.kor on _f.id_kor equals _k.id_kor into _a
                         from _k in _a.DefaultIfEmpty()

                         join _t in db.tipo_fiscalizacao on _f.id_tipo_fiscalizacao equals _t.id_tipo_fiscalizacao into _c
                         from _t in _c.DefaultIfEmpty()

                         join _u in db.usuario on _f.usuario_id equals _u.usuario_id into _d
                         from _u in _d.DefaultIfEmpty()

                         select new
                         {
                             IdFiscalizacao = _f.id_fiscalizacao,
                             DescFiscalizacao = _f.desc_fiscalizacao,
                             DataFiscalizacao = _f.data_fiscalizacao, 
                             DataGravacao = _f.data_gravacao, 
                             Posto = _f.posto, 
                             StatusFiscalizacao = _t.desc_tipo_fiscalizacao, 
                             NumeroAutuacao = _f.numero_autuacao, 
                             DescOperacao = _o.desc_operacao, 
                             NomeUsuario = _u.nome,
                             NumeroGuia = _g.numero_guia,
                             DataEmissaoGuia = _g.data_emissao_guia, 
                             TipoGuia = _tg.desc_tipo_guia, 
                             DataValidadeInicio = _g.data_validade_inicio, 
                             DataValidadeFim = _g.data_validade_fim, 
                             NomeEmissor = _g.nome_emissor, 
                             CnpjEmissor = _g.cnpj_emissor, 
                             MunicipioEmissor = _g.municipio_emissor, 
                             NomeOrigem = _g.nome_origem, 
                             MunicipioOrigem = _g.municipio_origem, 
                             NomeDestino = _g.nome_destino, 
                             MunicipioDestino = _g.municipio_destino, 
                             Placa = _g.placa_registro, 
                             NumeroNotaFiscal = _g.numero_notafiscal
                         };

            rowSet = rowSet.Where(f => f.IdFiscalizacao == fiscalizacaoId);

            var row = rowSet.FirstOrDefault();

            return request.CreateResponse(HttpStatusCode.OK, row);

        }


    }
}