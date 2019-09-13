using System;
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

    [RoutePrefix("api/smtp")]
    public class SmtpEmailController : ApiController
    {

        ModeloBancoEntities db = new ModeloBancoEntities();

        [HttpGet]
        [Route("listasmtpemails")]
        public HttpResponseMessage GetSmtpEmail(HttpRequestMessage request)
        {
            HttpResponseMessage response = null;


            try
            {

                List<smtp_email> smtpemails = db.smtp_email.ToList();


                IEnumerable<SmtpEmailViewModel> smtpEmailsVM = Mapper.Map<IEnumerable<smtp_email>, IEnumerable<SmtpEmailViewModel>>(smtpemails);

             
                response = request.CreateResponse(HttpStatusCode.OK, smtpEmailsVM);

            }

            catch (Exception)
            {

                throw new Exception("Erro ao carregar Smtp E-mails na Api!");
            }


            return response;
        }

    }
}
