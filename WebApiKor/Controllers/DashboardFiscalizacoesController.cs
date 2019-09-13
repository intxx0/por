﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using WebApiKor.InfraWeb;
using WebApiKor.Models;

namespace WebApiKor.Controllers
{
    [Authorize(Roles = "Operador,Administrador")]
    [RoutePrefix("api/dashboard")]
    public class DashboardFiscalizacoesController : ApiController
    {

        ModeloBancoEntities db = new ModeloBancoEntities();


        [AllowAnonymous]
        [HttpGet]
        [Route("fiscalizacaosemdocumentacao/{idOperacao:int}")]
        public HttpResponseMessage GetFiscalizacaoSemDocumentacao(HttpRequestMessage request, int? idOperacao)
        {


            //int cont = 0;
            HttpResponseMessage response = null;
            List<DashboardFiscalizacaoViewModel> lstfiscalizacoes = new List<DashboardFiscalizacaoViewModel>();
            List<DashboardFiscalizacaoViewModel> listafiscalizacaopordata = new List<DashboardFiscalizacaoViewModel>();
            List<DashboardFiscalizacaoViewModel> lista = new List<DashboardFiscalizacaoViewModel>();

            var fiscal =
               db.fiscalizacao.Where(f => f.id_operacao == idOperacao).ToList();



            if (fiscal.Count > 0)
            {

                foreach (var fiscaliza in fiscal)
                {

                    DashboardFiscalizacaoViewModel detalhes = new DashboardFiscalizacaoViewModel
                    {
                        DataFiscalizacao = fiscaliza.data_fiscalizacao.ToShortDateString(),

                    };

                    lstfiscalizacoes.Add(detalhes);
                }


                var listapordata = lstfiscalizacoes.GroupBy(f => f.DataFiscalizacao).Select(f => new { f.Key, Count = f.Count() });



                foreach (var lst in listapordata)
                {

                    DashboardFiscalizacaoViewModel details = new DashboardFiscalizacaoViewModel
                    {
                        DataFiscalizacao = lst.Key,
                        QtdFiscalizacoes = lst.Count

                    };

                    listafiscalizacaopordata.Add(details);
                }

                var listaOrdenada = listafiscalizacaopordata.OrderBy(d => d.DataFiscalizacao);

                response = request.CreateResponse(HttpStatusCode.OK, listaOrdenada);

            }
            else
            {

                response = request.CreateResponse(HttpStatusCode.NotFound, "Operação não existe não existe");
            }

            return response;
        }


        [AllowAnonymous]
        [HttpGet]
        [Route("fiscalizacaotipoautuacoes/{idOperacao:int}")]
        public HttpResponseMessage GetFiscalizacaoTipoAutuacao(HttpRequestMessage request, int? idOperacao)
        {

            //int cont = 0;
            HttpResponseMessage response = null;
            List<DashboardFiscalizacaoViewModel> lstfiscalizacoes = new List<DashboardFiscalizacaoViewModel>();
            List<DashboardFiscalizacaoViewModel> listafiscalizacaoportipoautuacao = new List<DashboardFiscalizacaoViewModel>();


            var fiscal =
               db.fiscalizacao.Where(f => f.id_operacao == idOperacao).ToList();


            if (fiscal.Count > 0)
            {

                //foreach (var fiscaliza in fiscal)
                //{

                //    DashboardFiscalizacaoViewModel detalhes = new DashboardFiscalizacaoViewModel
                //    {
                //        IdStatusFiscalizacao = fiscaliza.id_status_fiscalizacao,

                //    };

                //    lstfiscalizacoes.Add(detalhes);
                //}


                var listapordata = fiscal.GroupBy(f => f.status_fiscalizacao.desc_status_fiscalizacao).Select(f => new { f.Key, Count = f.Count() });



                foreach (var lst in listapordata)
                {

                    DashboardFiscalizacaoViewModel details = new DashboardFiscalizacaoViewModel
                    {
                        StatusFiscalizacao = lst.Key,
                        QtdFiscalizacoes = lst.Count

                    };

                    listafiscalizacaoportipoautuacao.Add(details);
                }

                response = request.CreateResponse(HttpStatusCode.OK, listafiscalizacaoportipoautuacao);

            }
            else
            {

                response = request.CreateResponse(HttpStatusCode.NotFound, "Operação não existe não existe");
            }

            return response;
        }



        [AllowAnonymous]
        [HttpGet]
        [Route("fiscalizacaotipomateriaprima/{idOperacao:int}")]
        public HttpResponseMessage GetFiscalizacaoMateriaPrima(HttpRequestMessage request, int? idOperacao)
        {

            HttpResponseMessage response = null;
            List<DashboardTipoMateriaPrimaViewModel> listafiscalizacaoportipomateriaprima = new List<DashboardTipoMateriaPrimaViewModel>();
            List<item_guia> itemsGuias = new List<item_guia>();
            List<DashboardTipoMateriaPrimaViewModel> listafiscalizacaoportipomateriaprima2 = new List<DashboardTipoMateriaPrimaViewModel>();



            /*var numeroGuia = db.fiscalizacao
                .Where(f => f.id_operacao == idOperacao && f.id_status_fiscalizacao != 1)
                .Select(f => f.numero_guia).ToList();


            if (numeroGuia.Count > 0)
            {

                foreach (var guia in numeroGuia)
                {
                    var IdGuia = db.guia.Where(g => g.numero_guia == guia).Select(i => i.id_guia).FirstOrDefault();

                    itemsGuias = db.item_guia.Where(i => i.id_guia == IdGuia).ToList();


                    var qtdTipoMateriaPrima =
                        itemsGuias.GroupBy(i => i.descricao_item).Select(i => new { i.Key, Count = i.Count() });


                    foreach (var lst in qtdTipoMateriaPrima)
                    {

                        DashboardTipoMateriaPrimaViewModel materiaPrima = new DashboardTipoMateriaPrimaViewModel
                        {
                            DescMateriaPrima = lst.Key,
                            QtdAutuacoesMateriaPrima = lst.Count

                        };

                        listafiscalizacaoportipomateriaprima.Add(materiaPrima);
                    }


                }

                var materiaPrimaAgrupada =
                      listafiscalizacaoportipomateriaprima.GroupBy(i => i.DescMateriaPrima).Select(i => new { i.Key, Count = i.Count() });


                foreach (var mp in materiaPrimaAgrupada)
                {
                    DashboardTipoMateriaPrimaViewModel materiaPrima = new DashboardTipoMateriaPrimaViewModel
                    {
                        DescMateriaPrima = mp.Key,
                        QtdAutuacoesMateriaPrima = mp.Count

                    };

                    listafiscalizacaoportipomateriaprima2.Add(materiaPrima);

                }

                var listaMateriaPrimaOrdenada = listafiscalizacaoportipomateriaprima2.OrderBy(o => o.DescMateriaPrima);

                response = request.CreateResponse(HttpStatusCode.OK, listaMateriaPrimaOrdenada);
            }
            else
            {

                response = request.CreateResponse(HttpStatusCode.NotFound, "Operação não existe não existe");
            }*/

            return response;
        }



        [AllowAnonymous]
        [HttpGet]
        [Route("fiscalizacaoautuadasorigem/{idOperacao:int}")]
        public HttpResponseMessage GetFiscalizacaoAutuadasOrigem(HttpRequestMessage request, int? idOperacao)
        {
            HttpResponseMessage response = null;
            List<guia> guias = new List<guia>();
            List<DashboardAutuadasOrigemViewModel> listaOrigem = new List<DashboardAutuadasOrigemViewModel>();


            /*var numeroGuia = db.fiscalizacao
                .Where(f => f.id_operacao == idOperacao && f.id_status_fiscalizacao != 1)
                .Select(f => f.numero_guia).Distinct().ToList();

            foreach (var guia in numeroGuia)
            {
                var fiscalizacoes = db.guia.Where(f => f.numero_guia == guia).FirstOrDefault();

                guias.Add(fiscalizacoes);
            }

            if (guias.Count > 0)
            {

                for (int i = 0; i < guias.Count; i++)
                {
                    if (guias[i] == null)
                    {
                        guias.Remove(guias[i]);
                    }
                }

                var origemAgrupada = guias.GroupBy(g => g.nome_origem).Select(i => new { i.Key, Count = i.Count() });



                foreach (var or in origemAgrupada)
                {
                    DashboardAutuadasOrigemViewModel origemVM = new DashboardAutuadasOrigemViewModel
                    {
                        DescAutudasOrigem = or.Key,
                        QtdAutuacoesOrigem = or.Count
                    };

                    listaOrigem.Add(origemVM);
                }

                response = request.CreateResponse(HttpStatusCode.OK, listaOrigem);
            }
            else
            {
                response = request.CreateResponse(HttpStatusCode.NotFound, "Operação não existe não existe");

            }*/

            return response;
        }


        [AllowAnonymous]
        [HttpGet]
        [Route("pegaIdOperacao/{idFiscalizacao:int}")]
        public HttpResponseMessage GetIdOperacao(HttpRequestMessage request, int? idFiscalizacao)
        {
            HttpResponseMessage response = null;


            if (idFiscalizacao > 0)
            {
                var natureza = db.operacao.Where(o => o.id_operacao == idFiscalizacao).FirstOrDefault();


                NaturezaOperacaoViewModel naturezaOperacao = new NaturezaOperacaoViewModel
                {
                    //IdNaturezaOperacao = Convert.ToInt16(natureza.id_natureza_operacao)

                };

                response = request.CreateResponse(HttpStatusCode.OK, naturezaOperacao);

            }
            else
            {
                response = request.CreateResponse(HttpStatusCode.NotFound, "Id da operação não existe");
            }


            return response;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("getDashboardData")]
        public HttpResponseMessage GetDashboard(HttpRequestMessage request)
        {

            HttpResponseMessage response = null;
            
            //var db = db.operacao
            //var query =
            //       from fisc in db.fiscalizacao
            //       join guia in db.guia on fisc.id_guia equals guia.id_guia
            //       join ig in db.item_guia on guia.id_item_guia equals ig.id_item_guia
            //       join tmp in db.tipo_materia_prima on ig.id_tipo_materia_prima equals tmp.id_tipo_materia_prima
            //       where fisc.id_status_fiscalizacao != 1 group fisc by tmp.desc_materia_prima into total
            //       select new { total = total., Meta = meta };





            response = request.CreateResponse(HttpStatusCode.OK, "");



            response = request.CreateResponse(HttpStatusCode.NotFound, "Id da operação não existe");


            return response;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("getfiscsoperacoes")]
        public HttpResponseMessage GetFiscsOperacoes(HttpRequestMessage request)
        {
            HttpResponseMessage response = null;

            var ops = db.operacao.Select(op => op).ToList();

            if(ops.Count > 0)
            {
                String[,] arr = new string[2, ops.Count];

                int i = 0, j = 0;
                foreach (var op in ops)
                {
                    arr[0, i] = op.desc_operacao;

                    var fiscs = db.fiscalizacao.Where(f => f.id_operacao == op.id_operacao).ToList();
                    arr[1, i] = fiscs.Count.ToString();

                    i++;
                }

                response = request.CreateResponse(HttpStatusCode.OK, arr);

            }
            else
            {
                response = request.CreateResponse(HttpStatusCode.NotFound, "Erro");
            }

            return response;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("getmateriaprimaautuada")]
        public HttpResponseMessage GetMateriaPrimaAutuada(HttpRequestMessage request)
        {
            HttpResponseMessage response = null;

            var idsGuiasAutadas = db.fiscalizacao.Where(f => f.numero_autuacao != null).Select(f => f.id_guia).ToList();
            var descMPS = new List<string>();
            var size = db.tipo_materia_prima.Count();
            String[,] arr = new string[2, size];
            int i = 0;

            foreach (var idGuia in idsGuiasAutadas)
            {
                var idsMP = db.item_guia.Where(ig => ig.id_guia == idGuia).Select(ig => ig.id_tipo_materia_prima).ToList();

                foreach (var id in idsMP)
                {
                    var materiaPrima = db.tipo_materia_prima.Where(mp => mp.id_tipo_materia_prima == id).Select(x => x.desc_materia_prima).FirstOrDefault();
                    var soma = db.item_guia.Where(ig => ig.id_tipo_materia_prima == id).Sum(s => s.qtd_item);

                    if (!descMPS.Contains(materiaPrima))
                    {
                        descMPS.Add(materiaPrima);

                        arr[0, i] = materiaPrima;
                        arr[1, i] = soma.ToString();
                        i++;
                    }
                    else
                    {
                        var index = descMPS.IndexOf(materiaPrima);
                        arr[1, index] = (int.Parse(arr[1, index]) + soma).ToString();
                    }
                }
            }


            response = request.CreateResponse(HttpStatusCode.OK, arr);


            return response;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("getautuacoestipo")]
        public HttpResponseMessage GetAutuacoesTipo(HttpRequestMessage request)
        {
            HttpResponseMessage response = null;

            var fiscs = db.fiscalizacao.Where(f => f.id_status_fiscalizacao != 1);
            var statusF = db.status_fiscalizacao.Where(sf => sf.id_status_fiscalizacao != 1).ToList();
            String[,] arr = new string[2, statusF.Count];
            int i = 0;

            foreach (var item in statusF)
            {
                var qtd = db.fiscalizacao.Where(f => f.id_status_fiscalizacao == item.id_status_fiscalizacao).ToList();

                arr[0, i] = item.desc_status_fiscalizacao;
                arr[1, i] = qtd.Count.ToString();
                i++;
            }

            response = request.CreateResponse(HttpStatusCode.OK, arr);

            return response;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("getautuacoesdia")]
        public HttpResponseMessage GetAutuacoesPorDia(HttpRequestMessage request)
        {
            HttpResponseMessage response = null;

            var fiscs = db.fiscalizacao.Where(f => f.numero_autuacao != null).ToList();
            var datas = fiscs.Select(f => f.data_fiscalizacao.ToString("dd/MM/yyyy")).Distinct().ToList();
            String[,] arr = new string[2, datas.Count];
            int i = 0;

            foreach (var dt in datas)
            {
                var qtd = fiscs.Where(f => f.data_fiscalizacao.ToString("dd/MM/yyyy") == dt).Count();

                arr[0, i] = DateTime.Parse(dt).ToString("dd/MM");
                arr[1, i] = qtd.ToString();
                i++;
            }

            response = request.CreateResponse(HttpStatusCode.OK, arr);

            return response;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("gettotalautuacoes")]
        public HttpResponseMessage GetTotalAutuacoes(HttpRequestMessage request)
        {
            HttpResponseMessage response = null;

            var fiscs = db.fiscalizacao.ToList();
            var datas = fiscs.Select(f => f.data_fiscalizacao.ToString("dd/MM/yyyy")).Distinct().ToList();
            String[,] arr = new string[3, datas.Count];
            int i = 0;

            foreach (var dt in datas)
            {
                var qtd = fiscs.Where(f => f.data_fiscalizacao.ToString("dd/MM/yyyy") == dt).Count();
                var qtdAutuadas = fiscs.Where(f => f.data_fiscalizacao.ToString("dd/MM/yyyy") == dt && f.numero_autuacao != null).Count();

                arr[0, i] = DateTime.Parse(dt).ToString("dd/MM");
                arr[1, i] = qtd.ToString();
                arr[2, i] = qtdAutuadas.ToString();
                i++;
            }

            response = request.CreateResponse(HttpStatusCode.OK, arr);

            return response;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("getmateriaprimaautuadaByOp")]
        public HttpResponseMessage GetMateriaPrimaAutuadaByOp(HttpRequestMessage request, [FromUri]String Lista)
        {
            HttpResponseMessage response = null;

            BuscaFiscalizacoesViewModel filtroVM = Newtonsoft.Json.JsonConvert.DeserializeObject<BuscaFiscalizacoesViewModel>(Lista);

            int? idAtividade = filtroVM.IdTipoFiscalizacao,
                idKor = filtroVM.IdKor,
                idUsuario = filtroVM.IdUsuario;

            DateTime dt_inicio = DateTime.Parse(filtroVM.DataInicio),
                     dt_final = DateTime.Parse(filtroVM.DataTermino);

            //id_tipo_fiscalizacao = id_atividade
            var idsGuiasAutadas = db.fiscalizacao.Where(f => f.id_status_fiscalizacao != 1 && f.id_tipo_fiscalizacao == idAtividade
                                                    && (f.data_fiscalizacao >= dt_inicio && f.data_fiscalizacao <= dt_final))
                                                    .ToList();

            if (idKor != null)
                idsGuiasAutadas = idsGuiasAutadas.Where(f => f.id_kor == idKor).ToList();
            if (idUsuario != null)
                idsGuiasAutadas = idsGuiasAutadas.Where(f => f.usuario_id == idUsuario).ToList();

            var idsGA = idsGuiasAutadas.Select(f => f.id_guia).ToList();

            var descMPS = new List<string>();
            var size = db.tipo_materia_prima.Count();
            String[,] arr = new string[2, size];
            int i = 0;

            foreach (var idGuia in idsGA)
            {
                var idsMP = db.item_guia.Where(ig => ig.id_guia == idGuia).Select(ig => ig.id_tipo_materia_prima).ToList();

                foreach (var id in idsMP)
                {
                    var materiaPrima = db.tipo_materia_prima.Where(mp => mp.id_tipo_materia_prima == id).Select(x => x.desc_materia_prima).FirstOrDefault();
                    var soma = db.item_guia.Where(ig => ig.id_tipo_materia_prima == id).Sum(s => s.qtd_item);

                    if (!descMPS.Contains(materiaPrima))
                    {
                        descMPS.Add(materiaPrima);

                        arr[0, i] = materiaPrima;
                        arr[1, i] = soma.ToString();
                        i++;
                    }
                    else
                    {
                        var index = descMPS.IndexOf(materiaPrima);
                        arr[1, index] = (int.Parse(arr[1, index]) + soma).ToString();
                    }
                }
            }

            response = request.CreateResponse(HttpStatusCode.OK, arr);

            return response;
        }


        [AllowAnonymous]
        [HttpGet]
        [Route("getautuacoestipoByOp")]
        public HttpResponseMessage GetAutuacoesTipoByOp(HttpRequestMessage request, [FromUri]String Lista)
        {
            HttpResponseMessage response = null;

            BuscaFiscalizacoesViewModel filtroVM = Newtonsoft.Json.JsonConvert.DeserializeObject<BuscaFiscalizacoesViewModel>(Lista);

            int? idAtividade = filtroVM.IdTipoFiscalizacao,
                idKor = filtroVM.IdKor,
                idUsuario = filtroVM.IdUsuario;

            DateTime dt_inicio = DateTime.Parse(filtroVM.DataInicio),
                    dt_final = DateTime.Parse(filtroVM.DataTermino);

            var statusFisc = db.status_fiscalizacao.Where(sf => sf.id_status_fiscalizacao != 1).ToList();

            String[,] arr = new string[2, statusFisc.Count];
            int i = 0;

            foreach (var sf in statusFisc)
            {
                var fiscs = db.fiscalizacao.Where(f => f.id_status_fiscalizacao == sf.id_status_fiscalizacao && f.id_operacao == idAtividade
                                            && (f.data_fiscalizacao >= dt_inicio && f.data_fiscalizacao <= dt_final))
                                            .ToList();
                if (idKor != null)
                    fiscs = fiscs.Where(f => f.id_kor == idKor).ToList();
                if (idUsuario != null)
                    fiscs = fiscs.Where(f => f.usuario_id == idUsuario).ToList();

                arr[0, i] = sf.desc_status_fiscalizacao;
                arr[1, i] = fiscs.Count.ToString();
                i++;
            }

            response = request.CreateResponse(HttpStatusCode.OK, arr);

            return response;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("getautuacoesdiaByOp")]
        public HttpResponseMessage GetAutuacoesPorDiaByOp(HttpRequestMessage request, [FromUri]String Lista)
        {
            HttpResponseMessage response = null;

            var filtroVM = Newtonsoft.Json.JsonConvert.DeserializeObject<BuscaFiscalizacoesViewModel>(Lista);

            int? idAtividade = filtroVM.IdTipoFiscalizacao,
                idKor = filtroVM.IdKor,
                idUsuario = filtroVM.IdUsuario;

            DateTime dt_inicio = DateTime.Parse(filtroVM.DataInicio),
                     dt_final = DateTime.Parse(filtroVM.DataTermino);

            var fiscs = db.fiscalizacao.Where(f => f.id_status_fiscalizacao != 1 && f.id_tipo_fiscalizacao == idAtividade
                                          && (f.data_fiscalizacao >= dt_inicio && f.data_fiscalizacao <= dt_final))
                                          .ToList();

            if (idKor != null)
                fiscs = fiscs.Where(f => f.id_kor == idKor).ToList();
            if (idUsuario != null)
                fiscs = fiscs.Where(f => f.usuario_id == idUsuario).ToList();

            var datas = fiscs.Select(f => f.data_fiscalizacao.ToString("dd/MM/yyyy")).Distinct().ToList();

            String[,] arr = new string[2, datas.Count];

            int i = 0;

            foreach (var dt in datas)
            {
                var qtd = fiscs.Where(f => f.data_fiscalizacao.ToString("dd/MM/yyyy") == dt).Count();

                arr[0, i] = DateTime.Parse(dt).ToString("dd/MM");
                arr[1, i] = qtd.ToString();
                i++;
            }

            response = request.CreateResponse(HttpStatusCode.OK, arr);

            return response;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("gettotalautuacoesByOp")]
        public HttpResponseMessage GetTotalAutuacoesByOp(HttpRequestMessage request, [FromUri]String Lista)
        {
            HttpResponseMessage response = null;

            var filtroVM = Newtonsoft.Json.JsonConvert.DeserializeObject<BuscaFiscalizacoesViewModel>(Lista);

            int? idAtividade = filtroVM.IdTipoFiscalizacao,
                idKor = filtroVM.IdKor,
                idUsuario = filtroVM.IdUsuario;

            DateTime dt_inicio = DateTime.Parse(filtroVM.DataInicio),
                   dt_final = DateTime.Parse(filtroVM.DataTermino);

            var fiscs = db.fiscalizacao.Where(f => f.id_tipo_fiscalizacao == idAtividade
                                            && (f.data_fiscalizacao >= dt_inicio && f.data_fiscalizacao <= dt_final)).ToList();

            if (idKor != null)
                fiscs = fiscs.Where(f => f.id_kor == idKor).ToList();
            if (idUsuario != null)
                fiscs = fiscs.Where(f => f.usuario_id == idUsuario).ToList();

            var datas = fiscs.Select(f => f.data_fiscalizacao.ToString("dd/MM/yyyy")).Distinct().ToList();

            String[,] arr = new string[3, datas.Count];
            int i = 0;

            foreach (var dt in datas)
            {
                var qtd = fiscs.Where(f => f.data_fiscalizacao.ToString("dd/MM/yyyy") == dt).Count();
                var qtdAutuadas = fiscs.Where(f => f.data_fiscalizacao.ToString("dd/MM/yyyy") == dt && f.id_status_fiscalizacao != 1).Count();

                arr[0, i] = DateTime.Parse(dt).ToString("dd/MM");
                arr[1, i] = qtd.ToString();
                arr[2, i] = qtdAutuadas.ToString();
                i++;
            }

            response = request.CreateResponse(HttpStatusCode.OK, arr);

            return response;
        }
    }
}
