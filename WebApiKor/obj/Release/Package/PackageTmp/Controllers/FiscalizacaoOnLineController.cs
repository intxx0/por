﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using AutoMapper;
using WebApiKor.InfraWeb;
using WebApiKor.Models;

namespace WebApiKor.Controllers
{


    [RoutePrefix("api/fiscalizacaoonline")]
    public class FiscalizacaoOnLineController : ApiController
    {
        ModeloBancoEntities db = new ModeloBancoEntities();
        


        #region Operacação



        [HttpGet]
        [Route("listafiscalizacaoonline/{page:int=0}/{pageSize=4}/{filter?}")]
        public HttpResponseMessage GetFiscalizacao(HttpRequestMessage request, int? page, int? pageSize, string filter = null)
        {

            int currentPage = page.Value;
            int currentPageSize = pageSize.Value;
            int totalFiscalizacoes = new int();

            List<operacao> fiscalizacoes = new List<operacao>();
            List<FiscalizacaoOnLineViewModel> opera = new List<FiscalizacaoOnLineViewModel>();



            HttpResponseMessage response = null;

            if (filter != null)
            {

                int idNaturezaOperacao = Convert.ToInt32(filter);

                fiscalizacoes = db.operacao.Where(fisc => fisc.id_natureza_operacao == idNaturezaOperacao)
                        .OrderBy(f => f.ativo)
                        .ToList();


                for (int i = 0; i < fiscalizacoes.Count; i++)
                {
                    var idOperacao = 0;
                    FiscalizacaoOnLineViewModel fiscOnLine = new FiscalizacaoOnLineViewModel();
                    fiscOnLine.tipoFiscalizacao = new List<tipo_fiscalizacao>();


                    idOperacao = fiscalizacoes[i].id_operacao;

                    var opTipoFiscalizacao = db.fiscalizacao_operacao.Where(t => t.id_operacao == idOperacao).ToList();


                    if (opTipoFiscalizacao.Count > 0)
                    {

                        foreach (var optipoFisc in opTipoFiscalizacao)
                        {


                            if (fiscalizacoes[i].id_operacao == optipoFisc.id_operacao)
                            {

                                var tipofiscalizacao = db.tipo_fiscalizacao.Where(f => f.id_tipo_fiscalizacao == optipoFisc.id_fiscalizacao).ToList();

                                if (tipofiscalizacao.Count > 0)
                                {
                                    tipo_fiscalizacao tipoFisc = new tipo_fiscalizacao();


                                    foreach (var tipo in tipofiscalizacao)
                                    {
                                        tipoFisc.id_tipo_fiscalizacao = tipo.id_tipo_fiscalizacao;
                                        tipoFisc.desc_tipo_fiscalizacao = tipo.desc_tipo_fiscalizacao;
                                        tipoFisc.ativo = tipo.ativo;

                                        fiscOnLine.tipoFiscalizacao.Add(tipoFisc);
                                    }

                                }
                            }
                        }
                    }

                    fiscOnLine.IdFiscalizacao = fiscalizacoes[i].id_operacao;
                    fiscOnLine.DescOperacao = fiscalizacoes[i].desc_operacao;
                    fiscOnLine.DataCriado = fiscalizacoes[i].data_criado_operacao;
                    fiscOnLine.DataFinalOperacao = fiscalizacoes[i].data_final_operacao;
                    fiscOnLine.Ativo = Convert.ToInt16(fiscalizacoes[i].ativo);

                    opera.Add(fiscOnLine);
                }

                response = request.CreateResponse(HttpStatusCode.OK, opera);
            }
            else
            {


                List<operacao> operacao = db.operacao.ToList();


                IEnumerable<FiscalizacaoOnLineViewModel> operacaoOnLineVM = Mapper.Map<IEnumerable<operacao>, IEnumerable<FiscalizacaoOnLineViewModel>>(operacao);


                response = request.CreateResponse(HttpStatusCode.OK, operacaoOnLineVM);

            }

            return response;
        }


        [AllowAnonymous]
        [HttpPost]
        [Route("inserirFiscalizacaoOnLine")]
        public HttpResponseMessage InserirFiscalizacaoOnLine(HttpRequestMessage request, FiscalizacaoOnLineViewModel fiscalizacaoOnLineViewModel)
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

                var usuarios = db.usuario.Where(usu => usu.email == User.Identity.Name).Select(usu => usu.usuario_id);

                foreach (var usuario in usuarios)
                {
                    fiscalizacaoOnLineViewModel.IdUsuario = usuario;
                }


                operacao novaOperacao = new operacao()
                {

                    usuario_id = fiscalizacaoOnLineViewModel.IdUsuario,
                    id_natureza_operacao = fiscalizacaoOnLineViewModel.IdNatureza,
                    nome_responsavel = fiscalizacaoOnLineViewModel.NomeResponsavel,
                    cargo_responsavel = fiscalizacaoOnLineViewModel.CargoResponsavel,
                    instituicao_responsavel = fiscalizacaoOnLineViewModel.InstituicaoResponsavel,
                    email_responsavel = fiscalizacaoOnLineViewModel.EmailResposnsavel,
                    tel_responsavel = fiscalizacaoOnLineViewModel.TelResponsavel,
                    desc_operacao = fiscalizacaoOnLineViewModel.DescOperacao,
                    desc_observacao = fiscalizacaoOnLineViewModel.DescObservacao,
                    data_final_operacao = fiscalizacaoOnLineViewModel.DataFinalOperacao,
                    data_criado_operacao = fiscalizacaoOnLineViewModel.DataCriado,
                    ativo = Convert.ToInt32(fiscalizacaoOnLineViewModel.Ativo),

                };

                db.operacao.Add(novaOperacao);
                db.SaveChanges();

                // Update view model
                fiscalizacaoOnLineViewModel = Mapper.Map<operacao, FiscalizacaoOnLineViewModel>(novaOperacao);

                response = request.CreateResponse(HttpStatusCode.Created, fiscalizacaoOnLineViewModel);

            }

            return response;

        }



        [AllowAnonymous]
        [HttpPost]
        [Route("alterarFiscalizacaoOnLine")]
        public HttpResponseMessage AlterarFiscalizacaoOnLine(HttpRequestMessage request, FiscalizacaoOnLineViewModel fiscalizacaoOnLineViewModel)
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

                var usuarios = db.usuario.Where(usu => usu.email == User.Identity.Name).Select(usu => usu.usuario_id);

                foreach (var usuario in usuarios)
                {
                    fiscalizacaoOnLineViewModel.IdUsuario = usuario;
                }


                operacao novaOperacao = new operacao()
                {
                    id_operacao = fiscalizacaoOnLineViewModel.IdFiscalizacao,
                    desc_operacao = fiscalizacaoOnLineViewModel.DescOperacao,
                    id_natureza_operacao = fiscalizacaoOnLineViewModel.IdNatureza,
                    nome_responsavel = fiscalizacaoOnLineViewModel.NomeResponsavel,
                    cargo_responsavel = fiscalizacaoOnLineViewModel.CargoResponsavel,
                    instituicao_responsavel = fiscalizacaoOnLineViewModel.InstituicaoResponsavel,
                    email_responsavel = fiscalizacaoOnLineViewModel.EmailResposnsavel,
                    tel_responsavel = fiscalizacaoOnLineViewModel.TelResponsavel,
                    desc_observacao = fiscalizacaoOnLineViewModel.DescObservacao,
                    data_criado_operacao = fiscalizacaoOnLineViewModel.DataCriado,
                    data_final_operacao = fiscalizacaoOnLineViewModel.DataFinalOperacao,
                    ativo = Convert.ToInt32(fiscalizacaoOnLineViewModel.Ativo),
                    usuario_id = fiscalizacaoOnLineViewModel.IdUsuario
                };

                db.Entry(novaOperacao).State = EntityState.Modified;

                db.SaveChanges();

                // atualiza view model
                fiscalizacaoOnLineViewModel = Mapper.Map<operacao, FiscalizacaoOnLineViewModel>(novaOperacao);

                response = request.CreateResponse(HttpStatusCode.OK, fiscalizacaoOnLineViewModel);

            }

            return response;

        }



        #endregion




        #region Kor - Operação



        [AllowAnonymous]
        [HttpGet]
        [Route("listakoroperacao/{id:int}")]
        public HttpResponseMessage GetKorOperacao(HttpRequestMessage request, int? id)
        {

            HttpResponseMessage response = null;


            List<kor_operacao> korOperacao = db.kor_operacao.Where(k => k.id_operacao == id).ToList();


            IEnumerable<KorOperacaoViewModel> operacaoeskorVM = Mapper.Map<IEnumerable<kor_operacao>, IEnumerable<KorOperacaoViewModel>>(korOperacao);



            response = request.CreateResponse(HttpStatusCode.OK, operacaoeskorVM);


            return response;
        }



        [AllowAnonymous]
        [HttpPost]
        [Route("inserirKorOperacao")]
        public HttpResponseMessage InserirKorOperacao(HttpRequestMessage request, KorOperacaoViewModel korOperacaoViewModel)
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

                kor_operacao novoKor = new kor_operacao()
                {

                    id_kor = korOperacaoViewModel.IdKor,
                    id_operacao = korOperacaoViewModel.IdOperacao

                };

                db.kor_operacao.Add(novoKor);
                db.SaveChanges();


                response = request.CreateResponse(HttpStatusCode.OK);

            }

            return response;

        }



        [AllowAnonymous]
        [HttpPost]
        [Route("alterarkorOperacao")]
        public HttpResponseMessage AlterarKorOperacao(HttpRequestMessage request, List<KorOperacaoViewModel> korOperacao)
        {

            HttpResponseMessage response = null;
            KorOperacaoViewModel korOperacaoViewModel = new KorOperacaoViewModel();


            if (!ModelState.IsValid)
            {
                response = request.CreateResponse(HttpStatusCode.BadRequest,
                    ModelState.Keys.SelectMany(k => ModelState[k].Errors)
                        .Select(m => m.ErrorMessage).ToArray());
            }
            else
            {

                korOperacaoViewModel.IdOperacao = korOperacao[0].IdOperacao;


                var kors = db.kor_operacao.Where(o => o.id_operacao == korOperacaoViewModel.IdOperacao).ToList();


                //deleta todas as operações com o idOperação
                if (kors.Count > 0)
                {
                    foreach (var koroperacao in kors)
                    {

                        db.Entry(koroperacao).State = EntityState.Deleted;
                    }

                    db.SaveChanges();
                }



                //salva o kor da operação no banco
                if (korOperacao.Count > 0)
                {
                    for (int i = 0; i < korOperacao.Count; i++)
                    {
                        kor_operacao novoKor = new kor_operacao()
                        {
                            id_kor = korOperacao[i].IdKor,
                            id_operacao = korOperacao[i].IdOperacao
                        };

                        db.kor_operacao.Add(novoKor);

                    }

                    db.SaveChanges();
                }

                response = request.CreateResponse(HttpStatusCode.OK);
            }

            return response;

        }


        #endregion




        #region Fiscalização Operação


        [AllowAnonymous]
        [HttpGet]
        [Route("listafiscalizacaooperacao/{id:int}")]
        public HttpResponseMessage GetFiscalizacaoOperacao(HttpRequestMessage request, int? id)
        {

            HttpResponseMessage response = null;


            List<fiscalizacao_operacao> fiscalizacaoOperacao = db.fiscalizacao_operacao.Where(k => k.id_operacao == id).ToList();


            IEnumerable<TipoFiscalizacaoOperacaoViewModel> fiscalizacaooperacaoVM = Mapper.Map<IEnumerable<fiscalizacao_operacao>, IEnumerable<TipoFiscalizacaoOperacaoViewModel>>(fiscalizacaoOperacao);



            response = request.CreateResponse(HttpStatusCode.OK, fiscalizacaooperacaoVM);


            return response;
        }



        [AllowAnonymous]
        [HttpPost]
        [Route("inserirFiscalizacaoOperacao")]
        public HttpResponseMessage InserirFiscalizacaoOperacao(HttpRequestMessage request, TipoFiscalizacaoOperacaoViewModel tipoFiscalizacaoOperacaoViewModel)
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

                fiscalizacao_operacao novaFiscalizacaoOperacao = new fiscalizacao_operacao
                {

                    id_fiscalizacao = tipoFiscalizacaoOperacaoViewModel.IdTipoFiscalizacao,
                    id_operacao = tipoFiscalizacaoOperacaoViewModel.IdOperacao

                };

                db.fiscalizacao_operacao.Add(novaFiscalizacaoOperacao);
                db.SaveChanges();


                response = request.CreateResponse(HttpStatusCode.OK);

            }

            return response;

        }



        [AllowAnonymous]
        [HttpPost]
        [Route("alterarTipoFiscalizacaoOperacao")]
        public HttpResponseMessage AlterarTipoFiscalizacaoOperacao(HttpRequestMessage request, List<TipoFiscalizacaoOperacaoViewModel> tipofiscalizaoperacao)
        {

            HttpResponseMessage response = null;
            TipoFiscalizacaoOperacaoViewModel tipoFiscalizacaoOperacaoViewModel = new TipoFiscalizacaoOperacaoViewModel();


            if (!ModelState.IsValid)
            {
                response = request.CreateResponse(HttpStatusCode.BadRequest,
                    ModelState.Keys.SelectMany(k => ModelState[k].Errors)
                        .Select(m => m.ErrorMessage).ToArray());
            }
            else
            {

                tipoFiscalizacaoOperacaoViewModel.IdOperacao = tipofiscalizaoperacao[0].IdOperacao;


                var operacoes = db.fiscalizacao_operacao.Where(o => o.id_operacao == tipoFiscalizacaoOperacaoViewModel.IdOperacao).ToList();


                //deleta todas as operações com o idOperação
                if (operacoes.Count > 0)
                {
                    foreach (var tipoFiscalizaOperacao in operacoes)
                    {
                        db.Entry(tipoFiscalizaOperacao).State = EntityState.Deleted;
                    }

                    db.SaveChanges();
                }



                //salva o tipo fiscalização da operação no banco
                if (tipofiscalizaoperacao.Count > 0)
                {
                    for (int i = 0; i < tipofiscalizaoperacao.Count; i++)
                    {
                        fiscalizacao_operacao novoTipoFiscalizacaoOperacao = new fiscalizacao_operacao()
                        {
                            id_fiscalizacao = tipofiscalizaoperacao[i].IdTipoFiscalizacao,
                            id_operacao = tipofiscalizaoperacao[i].IdOperacao
                        };

                        db.fiscalizacao_operacao.Add(novoTipoFiscalizacaoOperacao);
                    }

                    db.SaveChanges();
                }

                response = request.CreateResponse(HttpStatusCode.OK);
            }

            return response;
        }


     

        #endregion




        #region Usuário - Operação


        //lista de usuários que tenham o perfil de maleta
        [AllowAnonymous]
        [HttpGet]
        [Route("listausuarios/{page:int=0}/{pageSize=4}/{filter?}")]
        public HttpResponseMessage GetUsuarios(HttpRequestMessage request, int? page, int? pageSize, string filter = null)
        {

            int currentPage = page.Value;
            int currentPageSize = pageSize.Value;
            int totalUsuarios = new int();


            HttpResponseMessage response = null;


            List<usuario> usuarios = db.usuario.Where(usu => usu.grupo.nome == "Kor").OrderBy(usu => usu.nome)
                .Skip(currentPage * currentPageSize)
                .Take(currentPageSize)
                .ToList();

            totalUsuarios = db.usuario.Count();

            IEnumerable<UsuarioViewModel> usuariosVM =
                Mapper.Map<IEnumerable<usuario>, IEnumerable<UsuarioViewModel>>(usuarios);


            PaginacaoConfig<UsuarioViewModel> pagSet = new PaginacaoConfig<UsuarioViewModel>()
            {
                Page = currentPage,
                TotalCount = totalUsuarios,
                TotalPages = (int)Math.Ceiling((decimal)totalUsuarios / currentPageSize),
                Items = usuariosVM
            };


            response = request.CreateResponse(HttpStatusCode.OK, pagSet);


            return response;
        }



        [AllowAnonymous]
        [HttpGet]
        [Route("listausuariosoperacao/{id:int}")]
        public HttpResponseMessage GetUsuariosOperacao(HttpRequestMessage request, int? id)
        {

            HttpResponseMessage response = null;


            List<usuario_operacao> usuarioOperacao = db.usuario_operacao.Where(k => k.id_operacao == id).ToList();


            IEnumerable<UsuarioOperacaoViewModel> usuarioOperacaoVM = Mapper.Map<IEnumerable<usuario_operacao>, IEnumerable<UsuarioOperacaoViewModel>>(usuarioOperacao);


            response = request.CreateResponse(HttpStatusCode.OK, usuarioOperacaoVM);


            return response;
        }



        //Inserir Usuários para Operação que o mesmo deverá atuar
        [AllowAnonymous]
        [HttpPost]
        [Route("inserirusuariooperacao")]
        public HttpResponseMessage InserirUsuarioOperacao(HttpRequestMessage request, UsuarioOperacaoViewModel usuarioFiscalizacaoViewModel)
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

                usuario_operacao novoUsuarioOperacao = new usuario_operacao
                {

                    id_operacao = usuarioFiscalizacaoViewModel.IdOperacao,
                    usuario_id = usuarioFiscalizacaoViewModel.IdUsuario,
                    ativo = usuarioFiscalizacaoViewModel.Ativo

                };

                db.usuario_operacao.Add(novoUsuarioOperacao);
                db.SaveChanges();

                response = request.CreateResponse(HttpStatusCode.Created);

            }

            return response;

        }



        [AllowAnonymous]
        [HttpPost]
        [Route("atualizausuariooperacao")]
        public HttpResponseMessage AlterarUsuarioOperacao(HttpRequestMessage request, List<UsuarioOperacaoViewModel> usuariosOperacao)
        {

            HttpResponseMessage response = null;
            UsuarioOperacaoViewModel usuarioOperacaoViewModel = new UsuarioOperacaoViewModel();

            if (!ModelState.IsValid)
            {
                response = request.CreateResponse(HttpStatusCode.BadRequest,
                    ModelState.Keys.SelectMany(k => ModelState[k].Errors)
                        .Select(m => m.ErrorMessage).ToArray());
            }
            else
            {
                usuarioOperacaoViewModel.IdOperacao = usuariosOperacao[0].IdOperacao;

                //Pega usuários da tabela usuario_operacao
                var usuarios = db.usuario_operacao.Where(o => o.id_operacao == usuarioOperacaoViewModel.IdOperacao).ToList();



                //deleta todos os usuários da operação selecionados acima
                if (usuarios.Count > 0)
                {
                    foreach (var usuario in usuarios)
                    {
                        db.Entry(usuario).State = EntityState.Deleted;

                    }
                    db.SaveChanges();
                }



                //inseri usuário para operação no banco
                if (usuariosOperacao.Count > 0)
                {
                    for (int i = 0; i < usuariosOperacao.Count; i++)
                    {
                        usuario_operacao novoUsuarioOperacao = new usuario_operacao()
                        {
                            usuario_id = usuariosOperacao[i].IdUsuario,
                            id_operacao = usuariosOperacao[i].IdOperacao,
                            ativo = 1
                        };

                        db.usuario_operacao.Add(novoUsuarioOperacao);
                        db.SaveChanges();
                    }
                }
                response = request.CreateResponse(HttpStatusCode.OK);
            }
            return response;
        }



        #endregion


    }
}