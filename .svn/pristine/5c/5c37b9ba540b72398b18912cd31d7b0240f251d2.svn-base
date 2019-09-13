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
    [RoutePrefix("api/contaemail")]
    public class ContaEmailController : ApiController
    {

        ModeloBancoEntities db = new ModeloBancoEntities();

        [HttpGet]
        [Route("listacontaemail/{page:int=0}/{pageSize=4}/{filter?}")]
        public HttpResponseMessage GetContaEmail(HttpRequestMessage request, int? page, int? pageSize, string filter = null)
        {

            int currentPage = page.Value;
            int currentPageSize = pageSize.Value;
            int totalContaEmails = new int();

            HttpResponseMessage response = null;



            List<conta_email> contaemails = db.conta_email.OrderBy(c => c.id_conta_email)
                .Skip(currentPage * currentPageSize)
                .Take(currentPageSize)
                .ToList();


            totalContaEmails = db.conta_email.Count();


            IEnumerable<ContaEmailViewModel> contaemailsVM = Mapper.Map<IEnumerable<conta_email>, IEnumerable<ContaEmailViewModel>>(contaemails);

            PaginacaoConfig<ContaEmailViewModel> pagSet = new PaginacaoConfig<ContaEmailViewModel>()
            {
                Page = currentPage,
                TotalCount = totalContaEmails,
                TotalPages = (int)Math.Ceiling((decimal)totalContaEmails / currentPageSize),
                Items = contaemailsVM
            };


            response = request.CreateResponse(HttpStatusCode.OK, pagSet);


            return response;
        }


        [HttpPost]
        [Route("inserircontaemail")]
        public HttpResponseMessage InserirContaEmail(HttpRequestMessage request, ContaEmailViewModel contaemailViewModel)
        {

            HttpResponseMessage response = null;

            /*if (!ModelState.IsValid)
            {
                response = request.CreateResponse(HttpStatusCode.BadRequest,
                    ModelState.Keys.SelectMany(k => ModelState[k].Errors)
                          .Select(m => m.ErrorMessage).ToArray());
            }
            else
            {*/


                conta_email novoContaEmail = new conta_email()
                {
                    usuario_id = contaemailViewModel.UsuarioId,
                    smtp_host = contaemailViewModel.SmtpHost,
                    smtp_port = contaemailViewModel.SmtpPort,
                    smtp_auth = contaemailViewModel.SmtpAuth,
                    smtp_username = contaemailViewModel.SmtpUsername,
                    smtp_password = contaemailViewModel.SmtpPassword,
                    pop3_host = contaemailViewModel.Pop3Host,
                    pop3_port = contaemailViewModel.Pop3Port,
                    pop3_username = contaemailViewModel.Pop3Username,
                    pop3_password = contaemailViewModel.Pop3Password,
                    pop3_delete = contaemailViewModel.Pop3Delete
                };

                db.conta_email.Add(novoContaEmail);
                db.SaveChanges();

                // Update view model
                contaemailViewModel = Mapper.Map<conta_email, ContaEmailViewModel>(novoContaEmail);

                response = request.CreateResponse(HttpStatusCode.OK, contaemailViewModel);

            //}

            return response;

        }


        [HttpPost]
        [Route("alterarcontaemail")]
        public HttpResponseMessage AlterarContaEmail(HttpRequestMessage request, ContaEmailViewModel contaemailViewModel)
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

                conta_email novoContaEmail = new conta_email
                {
                    id_conta_email = contaemailViewModel.IdContaEmail,
                    usuario_id = contaemailViewModel.UsuarioId,
                    smtp_host = contaemailViewModel.SmtpHost,
                    smtp_port = contaemailViewModel.SmtpPort,
                    smtp_auth = contaemailViewModel.SmtpAuth,
                    smtp_username = contaemailViewModel.SmtpUsername,
                    smtp_password = contaemailViewModel.SmtpPassword,
                    pop3_host = contaemailViewModel.Pop3Host,
                    pop3_port = contaemailViewModel.Pop3Port,
                    pop3_username = contaemailViewModel.Pop3Username,
                    pop3_password = contaemailViewModel.Pop3Password,
                    pop3_delete = contaemailViewModel.Pop3Delete
                };

                db.Entry(novoContaEmail).State = EntityState.Modified;
                db.SaveChanges();

                contaemailViewModel = Mapper.Map<conta_email, ContaEmailViewModel>(novoContaEmail);

                response = request.CreateResponse(HttpStatusCode.OK, contaemailViewModel);
            }

            return response;

        }

        [HttpPost]
        [Route("excluircontaemail")]
        public HttpResponseMessage ExcluirContaEmail(HttpRequestMessage request, ContaEmailViewModel contaemailViewModel)
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

                conta_email excluirContaEmail = new conta_email
                {
                    id_conta_email = contaemailViewModel.IdContaEmail
                };

                db.Entry(excluirContaEmail).State = EntityState.Deleted;
                db.SaveChanges();

                contaemailViewModel = Mapper.Map<conta_email, ContaEmailViewModel>(excluirContaEmail);

                response = request.CreateResponse(HttpStatusCode.OK, contaemailViewModel);
            }

            return response;

        }

    }
}
