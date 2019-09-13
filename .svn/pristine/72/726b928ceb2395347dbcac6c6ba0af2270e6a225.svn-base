using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Web.Http;
using AutoMapper;
using WebApiKor.InfraWeb;
using WebApiKor.Models;

namespace WebApiKor.Controllers
{

    [RoutePrefix("api/email")]
    public class EmailController : ApiController
    {

        ModeloBancoEntities db = new ModeloBancoEntities();

        
        [HttpPost]
        [Route("enviaremail")]
        public HttpResponseMessage EnviarEmail(HttpRequestMessage request, EmailViewModel emailViewModel)
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
                try
                {

                    //Variáveis para envio de mensagem
                    string subject = emailViewModel.Assunto;
                    string body = emailViewModel.Texto;
                    string FromMail = "vsouza@icconsulting.com.br";
                    List<email> emailTo = emailViewModel.Para;


                    //Criando mensagem para envio
                    MailMessage mail = new MailMessage();
                    SmtpClient SmtpServer = new SmtpClient("imap.exchangecorp.com.br");
                    mail.From = new MailAddress(FromMail);

                    

                    for (int i = 0; i < emailTo.Count; i++)
                    {

                        if (emailTo[i].text != "")
                        {
                            mail.To.Add(emailTo[i].text);
                        }
                        else
                        {
                            response = request.CreateResponse(HttpStatusCode.NotAcceptable);
                        }


                    }

                    mail.Subject = subject;
                    //mail.CC.Add("vsouza@icconsulting.com.br");
                    mail.Body = body;
                    SmtpServer.Port = 587;
                    SmtpServer.Credentials = new System.Net.NetworkCredential("vsouza@icconsulting.com.br", "valdemir1212");
                    SmtpServer.EnableSsl = false;
                    SmtpServer.Send(mail);
                    emailViewModel.Remetente = FromMail;


                    caixa_email novoEmail = new caixa_email()
                    {
                        usuario_id = 2, //Pegar id do usuário logado para inserir no banco
                        remetente = emailViewModel.Remetente,
                        destinatario = emailViewModel.Destinatario,
                        assunto = emailViewModel.Assunto,
                        texto = emailViewModel.Texto,
                        datacriado = DateTime.Now,
                        tipo = "enviado",
                        enviado = 1,
                        baixado = 0,
                        ativo = 1,
                        deleted = null

                    };

                    db.caixa_email.Add(novoEmail);
                    db.SaveChanges();

                    // Update view model
                    //emailViewModel = Mapper.Map<caixa_email, EmailViewModel>(novoEmail);

                    response = request.CreateResponse(HttpStatusCode.OK);

                }
                catch (Exception ex)
                {

                    response = request.CreateResponse(HttpStatusCode.NotFound, ex);
                }

            }

            return response;
        }
        



        [HttpGet]
        [Route("carregaremails/{page:int=0}/{pageSize=4}/{filter?}")]
        public HttpResponseMessage GetEmails(HttpRequestMessage request, int? page, int? pageSize, int? tipoEnvio, string filter = null)
        {
            HttpResponseMessage response = null;


            int currentPage = page.Value;
            int currentPageSize = pageSize.Value;
            int totalEmails = new int();
            int idUsuario = 0;

            try
            {
                var usuariosLogado = User.Identity.Name;

                var usuario = db.usuario.Where(u => u.email == usuariosLogado).Select(u => u.usuario_id);

                foreach (var user in usuario)
                {
                    idUsuario = user;
                }


                //Retorna os emails Enviados
                //1 = Enviados
                if (tipoEnvio == 1)
                {

                    List<caixa_email> emailsrecebidos = db.caixa_email.Where(e => e.remetente == usuariosLogado && e.deleted != idUsuario).OrderByDescending(e => e.datacriado)
                   .Skip(currentPage * currentPageSize)
                   .Take(currentPageSize)
                   .ToList();

                    totalEmails = db.caixa_email.Count();

                    IEnumerable<EmailViewModel> emailsRecebidosVM = Mapper.Map<IEnumerable<caixa_email>, IEnumerable<EmailViewModel>>(emailsrecebidos);

                    PaginacaoConfig<EmailViewModel> paginaSet = new PaginacaoConfig<EmailViewModel>()
                    {
                        Page = currentPage,
                        TotalCount = totalEmails,
                        TotalPages = (int)Math.Ceiling((decimal)totalEmails / currentPageSize),
                        Items = emailsRecebidosVM
                    };

                    response = request.CreateResponse(HttpStatusCode.OK, paginaSet);

                    return response;

                }

                //Retorna os emails Excluidos
                //2 = Excluidos
                if (tipoEnvio == 2)
                {

                    List<caixa_email> emailsrecebidos = db.caixa_email.Where(e => e.deleted == idUsuario).OrderByDescending(e => e.datacriado)
                   .Skip(currentPage * currentPageSize)
                   .Take(currentPageSize)
                   .ToList();

                    totalEmails = db.caixa_email.Count();

                    IEnumerable<EmailViewModel> emailsRecebidosVM = Mapper.Map<IEnumerable<caixa_email>, IEnumerable<EmailViewModel>>(emailsrecebidos);

                    PaginacaoConfig<EmailViewModel> paginaSet = new PaginacaoConfig<EmailViewModel>()
                    {
                        Page = currentPage,
                        TotalCount = totalEmails,
                        TotalPages = (int)Math.Ceiling((decimal)totalEmails / currentPageSize),
                        Items = emailsRecebidosVM
                    };

                    response = request.CreateResponse(HttpStatusCode.OK, paginaSet);

                    return response;

                }


                //Retorna emails Recebidos
                List<caixa_email> emails = db.caixa_email.Where(e => e.destinatario == usuariosLogado && e.deleted != idUsuario).OrderByDescending(e => e.datacriado)
              .Skip(currentPage * currentPageSize)
              .Take(currentPageSize)
              .ToList();

                totalEmails = db.caixa_email.Count();

                IEnumerable<EmailViewModel> emailsVM = Mapper.Map<IEnumerable<caixa_email>, IEnumerable<EmailViewModel>>(emails);

                PaginacaoConfig<EmailViewModel> pagSet = new PaginacaoConfig<EmailViewModel>()
                {
                    Page = currentPage,
                    TotalCount = totalEmails,
                    TotalPages = (int)Math.Ceiling((decimal)totalEmails / currentPageSize),
                    Items = emailsVM
                };

                response = request.CreateResponse(HttpStatusCode.OK, pagSet);

            }
            catch (Exception)
            {

                throw new Exception("Erro ao carregar emails na Api!");
            }


            return response;
        }




        [HttpGet]
        [Route("lista_qtd_emails_nao_lidos")]
        public HttpResponseMessage GetEmailsNaoLidos(HttpRequestMessage request)
        {

            HttpResponseMessage response = null;
            
            
            int emails = db.caixa_email.Where(e => e.destinatario == User.Identity.Name && e.visualizado == false).Count();


            if (emails > 0)
            {

                response = request.CreateResponse(HttpStatusCode.OK, emails);
            }

            else
            {
                response = request.CreateResponse(HttpStatusCode.NoContent);

            }


            return response;
        }





        [HttpPost]
        [Route("atualizaCampoDeleteEmail")]
        public HttpResponseMessage AtualizaCampoDeleteEmail(HttpRequestMessage request, EmailViewModel emailViewModel)
        {

            HttpResponseMessage response = null;
            int idUser = 0;


            if (!ModelState.IsValid)
            {
                response = request.CreateResponse(HttpStatusCode.BadRequest,
                    ModelState.Keys.SelectMany(k => ModelState[k].Errors)
                          .Select(m => m.ErrorMessage).ToArray());
            }

            else
            {
                try
                {
                    //pega usuario logado
                    var usuarioLogado = User.Identity.Name;

                    var usuarios = db.usuario.Where(u => u.email == usuarioLogado).Select(u => u.usuario_id);

                    foreach (var usuario in usuarios)
                    {
                        idUser = usuario;
                    }

                    caixa_email emails = new caixa_email()
                    {
                        caixa_email_id = emailViewModel.CaixaEmailId,
                        deleted = idUser
                    };


                    using (ModeloBancoEntities banco = new ModeloBancoEntities())
                    {

                        banco.caixa_email.Attach(emails);
                        banco.Entry(emails).State = EntityState.Modified;

                        // Marcar a propriedade E-Mail como modificada
                        banco.Entry(emails).Property(u => u.deleted).IsModified = true;
                        banco.Entry(emails).Property(u => u.caixa_email_id).IsModified = true;
                        banco.Entry(emails).Property(u => u.usuario_id).IsModified = false;
                        banco.Entry(emails).Property(u => u.destinatario).IsModified = false;
                        banco.Entry(emails).Property(u => u.texto).IsModified = false;
                        banco.Entry(emails).Property(u => u.ativo).IsModified = false;
                        banco.Entry(emails).Property(u => u.remetente).IsModified = false;
                        banco.Entry(emails).Property(u => u.assunto).IsModified = false;
                        banco.Entry(emails).Property(u => u.tipo).IsModified = false;
                        banco.Entry(emails).Property(u => u.enviado).IsModified = false;
                        banco.Entry(emails).Property(u => u.baixado).IsModified = false;
                        banco.Entry(emails).Property(u => u.datacriado).IsModified = false;

                        banco.SaveChanges();

                        response = request.CreateResponse(HttpStatusCode.OK);
                    }
                }
                catch (Exception)
                {

                    response = request.CreateResponse(HttpStatusCode.NotModified);
                }
            }
            return response;
        }



        [HttpPost]
        [Route("atualizaCampoDeleteEmailExcluidos")]
        public HttpResponseMessage AtualizaCampoDeleteEmailExcluidos(HttpRequestMessage request, EmailViewModel emailViewModel)
        {

            HttpResponseMessage response = null;
            int idUser = 0;


            if (!ModelState.IsValid)
            {
                response = request.CreateResponse(HttpStatusCode.BadRequest,
                    ModelState.Keys.SelectMany(k => ModelState[k].Errors)
                          .Select(m => m.ErrorMessage).ToArray());
            }

            else
            {
                try
                {
                    //pega usuario logado
                    var usuarioLogado = User.Identity.Name;

                    var usuarios = db.usuario.Where(u => u.email == usuarioLogado).Select(u => u.usuario_id);

                    foreach (var usuario in usuarios)
                    {
                        idUser = usuario;
                    }

                    caixa_email emails = new caixa_email()
                    {
                        caixa_email_id = emailViewModel.CaixaEmailId,
                        deleted = 0
                    };


                    using (ModeloBancoEntities banco = new ModeloBancoEntities())
                    {

                        banco.caixa_email.Attach(emails);
                        banco.Entry(emails).State = EntityState.Modified;

                        // Marcar a propriedade E-Mail como modificada
                        banco.Entry(emails).Property(u => u.deleted).IsModified = true;
                        banco.Entry(emails).Property(u => u.caixa_email_id).IsModified = true;
                        banco.Entry(emails).Property(u => u.usuario_id).IsModified = false;
                        banco.Entry(emails).Property(u => u.destinatario).IsModified = false;
                        banco.Entry(emails).Property(u => u.texto).IsModified = false;
                        banco.Entry(emails).Property(u => u.ativo).IsModified = false;
                        banco.Entry(emails).Property(u => u.remetente).IsModified = false;
                        banco.Entry(emails).Property(u => u.assunto).IsModified = false;
                        banco.Entry(emails).Property(u => u.tipo).IsModified = false;
                        banco.Entry(emails).Property(u => u.enviado).IsModified = false;
                        banco.Entry(emails).Property(u => u.baixado).IsModified = false;
                        banco.Entry(emails).Property(u => u.datacriado).IsModified = false;

                        banco.SaveChanges();

                        response = request.CreateResponse(HttpStatusCode.OK);
                    }
                }
                catch (Exception)
                {

                    response = request.CreateResponse(HttpStatusCode.NotModified);
                }
            }
            return response;
        }




        [HttpPost]
        [Route("atualizaemailparavisualizado")]
        public HttpResponseMessage AtualizaEmailParaVisualizado(HttpRequestMessage request, EmailViewModel emailViewModel)
        {

            HttpResponseMessage response = null;
            int idUser = 0;


            if (!ModelState.IsValid)
            {
                response = request.CreateResponse(HttpStatusCode.BadRequest,
                    ModelState.Keys.SelectMany(k => ModelState[k].Errors)
                          .Select(m => m.ErrorMessage).ToArray());
            }

            else
            {
                try
                {
                    //pega usuario logado
                    var usuarioLogado = User.Identity.Name;

                    var usuarios = db.usuario.Where(u => u.email == usuarioLogado).Select(u => u.usuario_id);

                    foreach (var usuario in usuarios)
                    {
                        idUser = usuario;
                    }

                    caixa_email emails = new caixa_email()
                    {
                        caixa_email_id = emailViewModel.CaixaEmailId,
                        visualizado = true
                    };


                    using (ModeloBancoEntities banco = new ModeloBancoEntities())
                    {

                        banco.caixa_email.Attach(emails);
                        banco.Entry(emails).State = EntityState.Modified;

                        // Marcar a propriedade E-Mail como modificada
                        banco.Entry(emails).Property(u => u.deleted).IsModified = false;
                        banco.Entry(emails).Property(u => u.caixa_email_id).IsModified = true;
                        banco.Entry(emails).Property(u => u.visualizado).IsModified = true;
                        banco.Entry(emails).Property(u => u.usuario_id).IsModified = false;
                        banco.Entry(emails).Property(u => u.destinatario).IsModified = false;
                        banco.Entry(emails).Property(u => u.texto).IsModified = false;
                        banco.Entry(emails).Property(u => u.ativo).IsModified = false;
                        banco.Entry(emails).Property(u => u.remetente).IsModified = false;
                        banco.Entry(emails).Property(u => u.assunto).IsModified = false;
                        banco.Entry(emails).Property(u => u.tipo).IsModified = false;
                        banco.Entry(emails).Property(u => u.enviado).IsModified = false;
                        banco.Entry(emails).Property(u => u.baixado).IsModified = false;
                        banco.Entry(emails).Property(u => u.datacriado).IsModified = false;

                        banco.SaveChanges();

                        response = request.CreateResponse(HttpStatusCode.OK);
                    }
                }
                catch (Exception)
                {

                    response = request.CreateResponse(HttpStatusCode.NotModified);
                }
            }
            return response;
        }



    }
}
