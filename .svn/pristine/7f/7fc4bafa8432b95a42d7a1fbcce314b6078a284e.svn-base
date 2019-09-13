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
    [Authorize(Roles = "Administrador,Kor")]
    [RoutePrefix("api/mensagem")]
    public class MensagemController : ApiController
    {

        ModeloBancoEntities db = new ModeloBancoEntities();

        [HttpGet]
        [Route("listamensagem")]
        public HttpResponseMessage GetQtdMensagens(HttpRequestMessage request, string filter = null)
        {
            var idUsuario = 0;

            HttpResponseMessage response = null;

            //Pegar o usuário logado para trazer somente as mensagens enviadas por ele ou recebidas pelo mesmo.
            var usuarios = db.usuario.Where(user => user.email == User.Identity.Name).Select(user => user.usuario_id);

            foreach (var usuario in usuarios)
            {
                idUsuario = usuario;
            }

            List<mensagem> mensagens = db.mensagem.Where(usu => usu.usuario_destino_id == idUsuario && usu.visualizado == 0).ToList();

            var quantidadeMensagens = mensagens.Count;

            response = request.CreateResponse(HttpStatusCode.OK, quantidadeMensagens);


            return response;
        }



        [HttpGet]
        [Route("listausuarios")]
        public HttpResponseMessage GetUsuarioDestinatario(HttpRequestMessage request)
        {


            HttpResponseMessage response = null;


            //Pegar o usuário logado e trazer somente os usuários que sejam diferentes dele mesmo.
            var email = User.Identity.Name;

            List<usuario> usuarios = db.usuario.Where(usu => usu.email != email.Trim() && usu.ativo == 1).OrderBy(usu => usu.nome).ToList();


            int totalUsuarios = db.usuario.Count();


            IEnumerable<UsuarioViewModel> usuariosVM = Mapper.Map<IEnumerable<usuario>, IEnumerable<UsuarioViewModel>>(usuarios);

            PaginacaoConfig<UsuarioViewModel> pagSet = new PaginacaoConfig<UsuarioViewModel>()
            {
                Items = usuariosVM
            };


            response = request.CreateResponse(HttpStatusCode.OK, pagSet);


            return response;
        }


        [AllowAnonymous]
        [HttpPost]
        [Route("inserirmensagem")]
        public HttpResponseMessage InserirMensagem(HttpRequestMessage request, MensagemViewModel mensagemViewModel)
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

                //Pegar usuário logado para adicionar no campo de remetente.
                var emailUsuario = User.Identity.Name;

                var usuarios = db.usuario.Where(usu => usu.email == emailUsuario);


                foreach (var usuario in usuarios)
                {
                    mensagemViewModel.IdUsuario = usuario.usuario_id;
                }


                //Atualiza o model para pode inserir no banco de dados 
                mensagem novaMensagem = new mensagem()
                {
                    usuario_id = mensagemViewModel.IdUsuario,
                    usuario_destino_id = mensagemViewModel.UsuarioDestino,
                    texto = mensagemViewModel.TextoMensagem,
                    visualizado = 0,
                    baixado = 0,
                    datacriado = DateTime.Now

                };

                db.mensagem.Add(novaMensagem);
                db.SaveChanges();

                // Update view model
                mensagemViewModel = Mapper.Map<mensagem, MensagemViewModel>(novaMensagem);


                response = request.CreateResponse(HttpStatusCode.Created, mensagemViewModel);
            }

            return response;

        }


        [HttpGet]
        [Route("listamensagemrecebidas")]
        public HttpResponseMessage GetMensagemRecebidas(HttpRequestMessage request, string filter = null)
        {
            var idUsuario = 0;

            HttpResponseMessage response = null;


            //Pegar o usuário logado para trazer somente as mensagens enviadas por ele ou recebidas pelo mesmo.
            var usuarios = db.usuario.Where(user => user.email == User.Identity.Name).Select(user => user.usuario_id);

            foreach (var usuario in usuarios)
            {
                idUsuario = usuario;
            }

            List<mensagem> mensagens = db.mensagem.Where(usu => usu.usuario_destino_id == idUsuario || usu.usuario_id == idUsuario).OrderByDescending(mens => mens.datacriado).ToList();


            IEnumerable<MensagemViewModel> mensagensVM = Mapper.Map<IEnumerable<mensagem>, IEnumerable<MensagemViewModel>>(mensagens);

            PaginacaoConfig<MensagemViewModel> pagSet = new PaginacaoConfig<MensagemViewModel>()
            {
                IdUsuarioLogado = idUsuario,
                Items = mensagensVM
            };


            response = request.CreateResponse(HttpStatusCode.OK, pagSet);


            return response;
        }



        [AllowAnonymous]
        [HttpPost]
        [Route("atualizamensagensbaixadas")]
        public HttpResponseMessage AtualizaMensagensLidas(HttpRequestMessage request, MensagemViewModel mensagemViewModel)
        {

            var idUsuario = 0;

            HttpResponseMessage response = null;

            if (!ModelState.IsValid)
            {
                response = request.CreateResponse(HttpStatusCode.BadRequest,
                    ModelState.Keys.SelectMany(k => ModelState[k].Errors)
                          .Select(m => m.ErrorMessage).ToArray());
            }
            else
            {

                //Pegar usuário logado para adicionar no campo de remetente.
                var emailUsuario = User.Identity.Name;

                var usuarios = db.usuario.Where(usu => usu.email == emailUsuario);


                foreach (var usuario in usuarios)
                {
                    idUsuario = usuario.usuario_id;
                }

                var mensagens = db.mensagem.Where(m => m.visualizado == 0 && m.usuario_destino_id == idUsuario).Select(m => m.mensagem_id).ToList();

                foreach (var mensagem in mensagens)
                {
                    
                    //Atualiza o model para pode inserir no banco de dados 
                    mensagem novaMensagem = new mensagem()
                    {
                        mensagem_id = mensagem,
                        visualizado = mensagemViewModel.Visualizado
                    };

                    using (ModeloBancoEntities banco = new ModeloBancoEntities())
                    {
                        banco.mensagem.Attach(novaMensagem);
                        banco.Entry(novaMensagem).State = EntityState.Modified;
                        
                        // Marcar a propriedade E-Mail como modificada
                        banco.Entry(novaMensagem).Property(u => u.visualizado).IsModified = true;
                        banco.Entry(novaMensagem).Property(u => u.mensagem_id).IsModified = true;
                        banco.Entry(novaMensagem).Property(u => u.usuario_id).IsModified = false;
                        banco.Entry(novaMensagem).Property(u => u.usuario_destino_id).IsModified = false;
                        banco.Entry(novaMensagem).Property(u => u.texto).IsModified = false;
                        banco.Entry(novaMensagem).Property(u => u.baixado).IsModified = false;
                        banco.Entry(novaMensagem).Property(u => u.datacriado).IsModified = false;
                        banco.Entry(novaMensagem).Property(u => u.datamodificado).IsModified = false;
                        banco.Entry(novaMensagem).Property(u => u.deleted).IsModified = false;

                        banco.SaveChanges();

                    }
                }

                response = request.CreateResponse(HttpStatusCode.OK);
            }

            return response;

        }

    }
}
