﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebApiKor;
using AutoMapper;
using WebApiKor.InfraWeb;
using WebApiKor.Models;

namespace WebApiKor.Controllers
{
    [RoutePrefix("api/integracao")]
    public class IntegracaoController : ApiController
    {
        private ModeloBancoEntities db = new ModeloBancoEntities();

        [AllowAnonymous]
        [HttpGet]
        [Route("operacao/{idKor:int=0}")]
        public HttpResponseMessage OperacaoByIdKor(HttpRequestMessage request, int? idKor)
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

                if (idKor > 0)
                {
                    var query = db.operacao_usuario_kor.Include("operacao");

                    int korId = Convert.ToInt16(idKor);

                    var ouk = query.Where(koper => koper.id_kor == korId).Select(x=> x.id_operacao);

                    if (ouk == null)
                        return request.CreateResponse(HttpStatusCode.NotFound, "NOT_FOUND");
                    

                    var opr = db.operacao.Where(oper => ouk.Contains(oper.id_operacao)).ToList();

                    if (opr == null)
                        return request.CreateResponse(HttpStatusCode.NotFound, "NOT_FOUND");


                    IEnumerable<OperacaoViewModel> operacaoViewModel = Mapper.Map<IEnumerable<operacao>, IEnumerable<OperacaoViewModel>>(opr);

                    response = request.CreateResponse(HttpStatusCode.OK, operacaoViewModel);

                }

            }

            return response;

        }


        /*[AllowAnonymous]
        [HttpGet]
        [Route("kor/{idKor:int=0}")]
        public HttpResponseMessage KorById(HttpRequestMessage request, int? idKor)
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

                if (idKor > 0)
                {

                    int korId = Convert.ToInt16(idKor);

                    var Kor = db.kor.SingleOrDefault(k => k.id_kor == korId);

                    if (Kor == null)
                    {
                        return request.CreateResponse(HttpStatusCode.NotFound, "NOT_FOUND");
                    }

                    KorViewModel korViewModel;
                    korViewModel = Mapper.Map<kor, KorViewModel>(Kor);

                    response = request.CreateResponse(HttpStatusCode.OK, korViewModel);

                }

            }

            return response;

        }*/

        [AllowAnonymous]
        [HttpGet]
        [Route("kor/{idKorOrModel:int=0}")]
        public HttpResponseMessage KorByIdOrModel(HttpRequestMessage request, string idKorOrModel)
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
                int idKor = 0, i = 0;

                if (int.TryParse(idKorOrModel, out i))
                    idKor = Convert.ToInt16(idKorOrModel);

                var Kor = db.kor.FirstOrDefault(k => k.id_kor == idKor || k.modelo_kor == idKorOrModel);

                if (Kor == null)
                {
                    return request.CreateResponse(HttpStatusCode.NotFound, "NOT_FOUND");
                }

                KorViewModel korViewModel;
                korViewModel = Mapper.Map<kor, KorViewModel>(Kor);

                response = request.CreateResponse(HttpStatusCode.OK, korViewModel);

            }

            return response;

        }


        [AllowAnonymous]
        [HttpGet]
        [Route("tipofiscalizacao/{idKor:int=0}")]
        public HttpResponseMessage TipoFiscalizacaoByIdKor(HttpRequestMessage request, int? idKor)
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

                if (idKor > 0)
                {

                    int korId = Convert.ToInt16(idKor);
                    var row = db.operacao_usuario_kor.Where(k => k.id_kor == korId).Select(x=>x.id_operacao);
                    

                    if (row == null)
                    {
                        return request.CreateResponse(HttpStatusCode.NotFound, "NOT_FOUND");
                    }
                    var tfo = db.tipofiscalizacao_operacao.Where(z => row.Contains(z.id_operacao)).Select(c => c.id_tipo_fiscalizacao);

                    if (tfo == null)
                    {
                        return request.CreateResponse(HttpStatusCode.NotFound, "NOT_FOUND");
                    }
                    var tf = db.tipo_fiscalizacao.Where(x=> tfo.Contains(x.id_tipo_fiscalizacao)).ToList();

                    if (tf == null)
                    {
                        return request.CreateResponse(HttpStatusCode.NotFound, "NOT_FOUND");
                    }

                    IEnumerable<TipoFiscalizacaoViewModel> tipoFiscalizacaoViewModel = Mapper.Map<IEnumerable<tipo_fiscalizacao>, IEnumerable<TipoFiscalizacaoViewModel>>(tf);

                    response = request.CreateResponse(HttpStatusCode.OK, tipoFiscalizacaoViewModel);

                }

            }

            return response;

        }

        [AllowAnonymous]
        [HttpGet]
        [Route("operacaousuarioskor/{idKor:int=0}")]
        public HttpResponseMessage OperacaoUsuariosKorByIdKor(HttpRequestMessage request, int? idKor)
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

                if (idKor > 0)
                {

                    int korId = Convert.ToInt16(idKor);

                    var row = db.operacao_usuario_kor.SingleOrDefault(o => o.id_kor == korId);

                    if (row == null)
                    {
                        return request.CreateResponse(HttpStatusCode.NotFound, "NOT_FOUND");
                    }

                    OperacaoUsuariosKorViewModel operacaoUsuariosKorViewModel;
                    operacaoUsuariosKorViewModel = Mapper.Map<operacao_usuario_kor, OperacaoUsuariosKorViewModel>(row);

                    response = request.CreateResponse(HttpStatusCode.OK, operacaoUsuariosKorViewModel);

                }

            }

            return response;

        }


        [AllowAnonymous]
        [HttpGet]
        [Route("statusfiscalizacao")]
        public HttpResponseMessage StatusFiscalizacao(HttpRequestMessage request)
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

                List<status_fiscalizacao> rows = db.status_fiscalizacao.OrderBy(s => s.id_status_fiscalizacao).ToList();

                if (rows == null)
                {
                    return request.CreateResponse(HttpStatusCode.NotFound, "NOT_FOUND");
                }

                IEnumerable<StatusFiscalizacaoViewModel> statusFiscalizacaoViewModel = Mapper.Map<IEnumerable<status_fiscalizacao>, IEnumerable<StatusFiscalizacaoViewModel>>(rows);

                response = request.CreateResponse(HttpStatusCode.OK, statusFiscalizacaoViewModel);

            }

            return response;

        }

        [AllowAnonymous]
        [HttpGet]
        [Route("missao/{idMissao:int=0}")]
        public HttpResponseMessage MissaoById(HttpRequestMessage request, int? idMissao)
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

                if (idMissao > 0)
                {

                    int missaoId = Convert.ToInt16(idMissao);
                    var row = db.missao.SingleOrDefault(m => m.id_missao == missaoId);

                    if (row == null)
                    {
                        return request.CreateResponse(HttpStatusCode.NotFound, "NOT_FOUND");
                    }

                    MissaoViewModel missaoViewModel;
                    missaoViewModel = Mapper.Map<missao, MissaoViewModel>(row);

                    response = request.CreateResponse(HttpStatusCode.OK, missaoViewModel);

                }

            }

            return response;

        }

        

        [AllowAnonymous]
        [HttpGet]
        [Route("operacaousuariokor/{idKor:int=0}")]
        public HttpResponseMessage UsuarioOperacaoById(HttpRequestMessage request, int? idKor)
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

                if (idKor > 0)
                {

                    idKor = Convert.ToInt16(idKor);
                    var row = db.operacao_usuario_kor.Where(m => m.id_kor == idKor).ToList();

                    if (row == null)
                    {
                        return request.CreateResponse(HttpStatusCode.NotFound, "NOT_FOUND");
                    }

                    IEnumerable<OperacaoUsuariosKorViewModel> operacaoUsuariosKorViewModel = Mapper.Map<IEnumerable<operacao_usuario_kor>, IEnumerable<OperacaoUsuariosKorViewModel>>(row);

                    response = request.CreateResponse(HttpStatusCode.OK, operacaoUsuariosKorViewModel);

                }

            }

            return response;

        }

        [AllowAnonymous]
        [HttpGet]
        [Route("missaooperacao/{idOperacao:int=0}")]
        public HttpResponseMessage MissaoOperacaoByIdOperacao(HttpRequestMessage request, int? idOperacao)
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

                if (idOperacao > 0)
                {

                    int operacaoId = Convert.ToInt16(idOperacao);
                    var row = db.missao_operacao.SingleOrDefault(m => m.id_operacao == operacaoId);

                    if (row == null)
                    {
                        return request.CreateResponse(HttpStatusCode.NotFound, "NOT_FOUND");
                    }

                    MissaoOperacaoViewModel missaoOperacaoViewModel;
                    missaoOperacaoViewModel = Mapper.Map<missao_operacao, MissaoOperacaoViewModel>(row);

                    response = request.CreateResponse(HttpStatusCode.OK, missaoOperacaoViewModel);

                }

            }

            return response;

        }

        [AllowAnonymous]
        [HttpGet]
        [Route("usuarios")]
        public HttpResponseMessage UsuariosByIdOperacao(HttpRequestMessage request)
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

                List<usuario> rows = db.usuario.OrderBy(usu => usu.usuario_id).ToList();

                if (rows == null)
                {
                    return request.CreateResponse(HttpStatusCode.NotFound, "NOT_FOUND");
                }

                IEnumerable<UsuarioViewModel> usuariosViewModel = Mapper.Map<IEnumerable<usuario>, IEnumerable<UsuarioViewModel>>(rows);

                response = request.CreateResponse(HttpStatusCode.OK, usuariosViewModel);

            }

            return response;

        }

        [AllowAnonymous]
        [HttpGet]
        [Route("grupos")]
        public HttpResponseMessage GruposByIdKor(HttpRequestMessage request)
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

                    var grupos = db.grupo.OrderBy(x=>x.grupo_id).ToList();

                    IEnumerable<GruposViewModel> gruposViewModel = Mapper.Map<IEnumerable<grupo>, IEnumerable<GruposViewModel>>(grupos);

                    response = request.CreateResponse(HttpStatusCode.OK, gruposViewModel);

                

            }

            return response;

        }

        [AllowAnonymous]
        [HttpGet]
        [Route("cargos")]
        public HttpResponseMessage Cargos(HttpRequestMessage request)
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

                List<cargo> rows = db.cargo.OrderBy(c => c.id_cargo).ToList();

                if (rows == null)
                {
                    return request.CreateResponse(HttpStatusCode.NotFound, "NOT_FOUND");
                }

                IEnumerable<CargoViewModel> cargoViewModel = Mapper.Map<IEnumerable<cargo>, IEnumerable<CargoViewModel>>(rows);

                response = request.CreateResponse(HttpStatusCode.OK, cargoViewModel);

            }

            return response;

        }

        [AllowAnonymous]
        [HttpGet]
        [Route("instituicoes")]
        public HttpResponseMessage Instituicoes(HttpRequestMessage request)
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

                List<instituicao> rows = db.instituicao.OrderBy(i => i.id_instituicao).ToList();

                if (rows == null)
                {
                    return request.CreateResponse(HttpStatusCode.NotFound, "NOT_FOUND");
                }

                IEnumerable<InstituicaoViewModel> instituicaoViewModel = Mapper.Map<IEnumerable<instituicao>, IEnumerable<InstituicaoViewModel>>(rows);

                response = request.CreateResponse(HttpStatusCode.OK, instituicaoViewModel);

            }

            return response;

        }

        [AllowAnonymous]
        [HttpGet]
        [Route("perfis")]
        public HttpResponseMessage Perfis(HttpRequestMessage request)
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

                List<perfil> rows = db.perfil.OrderBy(i => i.id_perfil).ToList();

                if (rows == null)
                {
                    return request.CreateResponse(HttpStatusCode.NotFound, "NOT_FOUND");
                }

                IEnumerable<PerfilViewModel> perfilViewModel = Mapper.Map<IEnumerable<perfil>, IEnumerable<PerfilViewModel>>(rows);

                response = request.CreateResponse(HttpStatusCode.OK, perfilViewModel);

            }

            return response;

        }

        [AllowAnonymous]
        [HttpGet]
        [Route("tipomateriasprimas")]
        public HttpResponseMessage TipoMateriasPrimas(HttpRequestMessage request)
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

                List<tipo_materia_prima> rows = db.tipo_materia_prima.OrderBy(m => m.id_tipo_materia_prima).ToList();

                if (rows == null)
                {
                    return request.CreateResponse(HttpStatusCode.NotFound, "NOT_FOUND");
                }

                IEnumerable<MateriaPrimaViewModel> tipoMateriaPrimaViewModel = Mapper.Map<IEnumerable<tipo_materia_prima>, IEnumerable<MateriaPrimaViewModel>>(rows);

                response = request.CreateResponse(HttpStatusCode.OK, tipoMateriaPrimaViewModel);

            }

            return response;

        }

        [AllowAnonymous]
        [HttpGet]
        [Route("guias")]
        public HttpResponseMessage Guias(HttpRequestMessage request)
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

                List<guia> rows = db.guia.OrderBy(g => g.id_guia).ToList();

                if (rows == null)
                {
                    return request.CreateResponse(HttpStatusCode.NotFound, "NOT_FOUND");
                }

                IEnumerable<GuiaViewModel> guiaViewModel = Mapper.Map<IEnumerable<guia>, IEnumerable<GuiaViewModel>>(rows);

                response = request.CreateResponse(HttpStatusCode.OK, guiaViewModel);

            }

            return response;

        }

        [AllowAnonymous]
        [HttpGet]
        [Route("tiposguias")]
        public HttpResponseMessage TiposGuias(HttpRequestMessage request)
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

                List<tipo_guia> rows = db.tipo_guia.OrderBy(g => g.id_tipo_guia).ToList();

                if (rows == null)
                {
                    return request.CreateResponse(HttpStatusCode.NotFound, "NOT_FOUND");
                }

                IEnumerable<TipoGuiaViewModel> tipoGuiaViewModel = Mapper.Map<IEnumerable<tipo_guia>, IEnumerable<TipoGuiaViewModel>>(rows);

                response = request.CreateResponse(HttpStatusCode.OK, tipoGuiaViewModel);

            }

            return response;

        }

        [AllowAnonymous]
        [HttpPost]
        [Route("fiscalizacao")]
        public HttpResponseMessage Fiscalizacao(HttpRequestMessage request, FiscalizacaoViewModel fiscalizacaoViewModel)
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

                fiscalizacao row = new fiscalizacao()
                {
                    desc_fiscalizacao = fiscalizacaoViewModel.DescFiscalizacao,
                    data_fiscalizacao = fiscalizacaoViewModel.DataFiscalizacao,
                    posto = fiscalizacaoViewModel.Posto,
                    id_status_fiscalizacao = fiscalizacaoViewModel.IdStatusFiscalizacao,
                    id_operacao = fiscalizacaoViewModel.IdOperacao,
                    id_guia = fiscalizacaoViewModel.IdGuia, 
                    ativo = fiscalizacaoViewModel.Ativo,
                    id_subtipo_fiscalizacao = fiscalizacaoViewModel.IdSubTipoFiscalizacao,
                    finalizada = Convert.ToBoolean(fiscalizacaoViewModel.Finalizada),
                    usuario_id = fiscalizacaoViewModel.UsuarioId,
                    data_gravacao = fiscalizacaoViewModel.DataGravacao,
                    numero_autuacao = fiscalizacaoViewModel.NumeroAutuacao,
                    id_kor = fiscalizacaoViewModel.IdKor, 
                    sincronizado = fiscalizacaoViewModel.Sincronizado
                };

                db.fiscalizacao.Add(row);
                db.SaveChanges();

                FiscalizacaoViewModel novoFiscalizacaoViewModel = Mapper.Map<fiscalizacao, FiscalizacaoViewModel>(row);

                response = request.CreateResponse(HttpStatusCode.Created, novoFiscalizacaoViewModel);

            }

            return response;

        }

        [AllowAnonymous]
        [HttpPost]
        [Route("email")]
        public HttpResponseMessage Email(HttpRequestMessage request, EmailViewModel emailViewModel)
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

                caixa_email row = new caixa_email()
                {
                    caixa_email_id = emailViewModel.CaixaEmailId,
                    usuario_id = emailViewModel.UsuarioId,
                    destinatario = emailViewModel.Destinatario,
                    remetente = emailViewModel.Remetente,
                    assunto = emailViewModel.Assunto,
                    texto = emailViewModel.Texto,
                    tipo = emailViewModel.Tipo,
                    ativo = emailViewModel.Ativo,
                    enviado = emailViewModel.Enviado,
                    baixado = emailViewModel.Baixado,
                    datacriado = emailViewModel.DataCriado,
                    deleted = emailViewModel.Deletado,
                    visualizado = Convert.ToBoolean(emailViewModel.Visualizado)
                };

                db.caixa_email.Add(row);
                db.SaveChanges();

                EmailViewModel novoEmailViewModel = Mapper.Map<caixa_email, EmailViewModel>(row);

                response = request.CreateResponse(HttpStatusCode.Created, novoEmailViewModel);

            }

            return response;

        }

        [AllowAnonymous]
        [HttpPost]
        [Route("mensagem")]
        public HttpResponseMessage Mensagem(HttpRequestMessage request, MensagemViewModel mensagemViewModel)
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

                mensagem row = new mensagem()
                {
                    usuario_id = mensagemViewModel.IdUsuario,
                    usuario_destino_id = mensagemViewModel.UsuarioDestino,
                    texto = mensagemViewModel.TextoMensagem,
                    visualizado = mensagemViewModel.Visualizado,
                    baixado = mensagemViewModel.Baixado,
                    datacriado = mensagemViewModel.DataCriado,
                    datamodificado = mensagemViewModel.DataModificado,
                    deleted = mensagemViewModel.Deletado,
                    sincronizado = mensagemViewModel.Sincronizado
                };

                db.mensagem.Add(row);
                db.SaveChanges();

                MensagemViewModel novoEmailViewModel = Mapper.Map<mensagem, MensagemViewModel>(row);

                response = request.CreateResponse(HttpStatusCode.Created, novoEmailViewModel);

            }

            return response;

        }

        [AllowAnonymous]
        [HttpGet]
        [Route("emails/{idKor:int=0}")]
        public HttpResponseMessage GetEmails(HttpRequestMessage request, int? idKor)
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

                int korId = Convert.ToInt16(idKor);
                List<caixa_email> emails = new List<caixa_email>();

                List<int?> rowset = db.operacao_usuario_kor.Where(o => o.id_kor == korId).Select(x=> x.usuario_id).Distinct().ToList();

                foreach (int i in rowset)
                {
                    List<string> rowsetUsuarios = db.usuario.Where(u => u.usuario_id == i).Select(x=> x.email).Distinct().ToList();
                    foreach (var j in rowsetUsuarios)
                    {
                        List<caixa_email> rowsetEmails = db.caixa_email.Where(e => e.destinatario.Equals(j) && e.sincronizado < 1).ToList();
                        for (int z = 0; z < rowsetEmails.Count(); z++)
                        {
                            emails.Add(rowsetEmails[z]);
                            rowsetEmails[z].sincronizado = 1;
                            db.Entry(rowsetEmails[z]).State = EntityState.Modified;
                            db.SaveChanges();
                        }
                    }
                }

                IEnumerable<EmailViewModel> emailsViewModel = Mapper.Map<IEnumerable<caixa_email>, IEnumerable<EmailViewModel>>(emails);
                response = request.CreateResponse(HttpStatusCode.Created, emailsViewModel);

            }

            return response;

        }

        [AllowAnonymous]
        [HttpGet]
        [Route("mensagens/{idKor:int=0}")]
        public HttpResponseMessage GetMensagens(HttpRequestMessage request, int? idKor)
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

                int korId = Convert.ToInt16(idKor);
                List<mensagem> mensagens = new List<mensagem>();

                List<int?> rowset = db.operacao_usuario_kor.Where(o => o.id_kor == korId).Select(x=> x.usuario_id).Distinct().ToList();

                foreach (var i in rowset)
                {
                    List<mensagem> rowsetMensagens = db.mensagem.Where(m => m.usuario_destino_id == i).ToList();
                    for (int j = 0; j < rowsetMensagens.Count(); j++)
                    {
                        mensagens.Add(rowsetMensagens[j]);
                        rowsetMensagens[j].sincronizado = 1;
                        db.Entry(rowsetMensagens[j]).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }

                IEnumerable<MensagemViewModel> mensagensViewModel = Mapper.Map<IEnumerable<mensagem>, IEnumerable<MensagemViewModel>>(mensagens);
                response = request.CreateResponse(HttpStatusCode.Created, mensagensViewModel);

            }

            return response;

        }

        [AllowAnonymous]
        [HttpPost]
        [Route("emergencia")]
        public HttpResponseMessage Emergencia(HttpRequestMessage request, EmergenciaViewModel emergenciaViewModel)
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

                emergencia row = new emergencia()
                {
                    emergencia_id = emergenciaViewModel.IdEmergencia,
                    usuario_id = emergenciaViewModel.UsuarioId,
                    usuario_atendeu_id = emergenciaViewModel.UsuarioAtendeuId,
                    latitude = emergenciaViewModel.Latitude,
                    longitude = emergenciaViewModel.Longitude,
                    coordenada = emergenciaViewModel.Coordenada,
                    datacriado = emergenciaViewModel.DataCriado,
                    deleted = emergenciaViewModel.Deletado,
                    ativo = emergenciaViewModel.Ativo
                };

                db.emergencia.Add(row);
                db.SaveChanges();

                EmergenciaViewModel novaEmergenciaViewModel = Mapper.Map<emergencia, EmergenciaViewModel>(row);

                response = request.CreateResponse(HttpStatusCode.Created, novaEmergenciaViewModel);

            }

            return response;

        }



    }
}