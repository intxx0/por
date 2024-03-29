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
using AutoMapper;
using WebApiKor;
using WebApiKor.InfraWeb;
using WebApiKor.Models;

namespace WebApiKor.Controllers
{
    [RoutePrefix("api/usuarios")]

    [Authorize(Roles ="Administrador, Operador")]
    public class UsuariosController : ApiController
    {
        ModeloBancoEntities db = new ModeloBancoEntities();


        [AllowAnonymous]
        [HttpGet]
        [Route("listausuarios/{page:int=0}/{pageSize=4}/{filter?}")]
        public HttpResponseMessage Usuarios(HttpRequestMessage request,int? page , int? pageSize, string filter = null)
        {

            int currentPage = page.Value;
            int currentPageSize = pageSize.Value;
            int totalUsuarios = new int();


            HttpResponseMessage response = null;


            List<usuario> usuarios = db.usuario.OrderBy(usu=> usu.nome)
                .Skip(currentPage * currentPageSize)
                .Take(currentPageSize)
                .ToList();

            totalUsuarios = db.usuario.Count();

            IEnumerable<UsuarioViewModel> usuariosVM = Mapper.Map<IEnumerable<usuario>, IEnumerable<UsuarioViewModel>>(usuarios);


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
        [HttpPost]
        [Route("alterarusuario")]
        public HttpResponseMessage AlterarUsuarios(HttpRequestMessage request, UsuarioViewModel usuarioViewModel)
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

                usuario usuario = new usuario
                {
                    usuario_id = usuarioViewModel.UsuarioId,
                    grupo_id = usuarioViewModel.IdGrupo,
                    email = usuarioViewModel.Email,
                    login = usuarioViewModel.DescLogin,
                    senha = usuarioViewModel.Senha,
                    nome = usuarioViewModel.DescNome,
                    ultimo_acesso = DateTime.Now,
                    ativo = Convert.ToInt32(usuarioViewModel.Ativo),
                    imap_server = usuarioViewModel.ImapServer,
                    email_senha = usuarioViewModel.EmailSenha
                    

                };

                db.Entry(usuario).State = EntityState.Modified;
                db.SaveChanges();

                usuarioViewModel = Mapper.Map<usuario, UsuarioViewModel>(usuario);

                response = request.CreateResponse(HttpStatusCode.OK, usuarioViewModel);
                
            }

            return response;

        }



        [AllowAnonymous]
        [HttpPost]
        [Route("inserirusuario")]
        public HttpResponseMessage InserirUsuario(HttpRequestMessage request, UsuarioViewModel usuarioViewModel)
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

                
                usuario novoUsuario = new usuario()
                {
                    
                    grupo_id = usuarioViewModel.IdGrupo,
                    nome = usuarioViewModel.DescNome,
                    login = usuarioViewModel.DescLogin,
                    email = usuarioViewModel.Email,
                    imap_server = usuarioViewModel.ImapServer,
                    email_senha = usuarioViewModel.EmailSenha,
                    senha = usuarioViewModel.Senha,
                    data_criado = DateTime.Now,
                    ativo = Convert.ToInt32(usuarioViewModel.Ativo),
                    
                };
                
                db.usuario.Add(novoUsuario);
                db.SaveChanges();

                // Update view model
                usuarioViewModel = Mapper.Map<usuario, UsuarioViewModel>(novoUsuario);

                response = request.CreateResponse(HttpStatusCode.Created, usuarioViewModel);

            }

            return response;

        }




        [AllowAnonymous]
        [HttpPost]
        [Route("deletarusuario/{idusuario:int}")]
        public HttpResponseMessage DeletarUsuario(HttpRequestMessage request, int? idusuario)
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

                var usuarioOperacao = db.usuario_operacao.Where(uo => uo.usuario_id == idusuario).ToList();

                foreach (var usuarioOp in usuarioOperacao)
                {
                    db.Entry(usuarioOp).State = EntityState.Deleted;
                    db.SaveChanges();
                }


                if (idusuario > 0)
                {
                    var usuario = db.usuario.SingleOrDefault(u => u.usuario_id == idusuario);

                    db.usuario.Remove(usuario);
                    db.SaveChanges();


                    response = request.CreateResponse(HttpStatusCode.OK, usuario);
                }

             }

            return response;

        }

    }
}