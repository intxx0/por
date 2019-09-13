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

    [RoutePrefix("api/emergencia")]
    public class EmergenciaController : ApiController
    {
        ModeloBancoEntities db = new ModeloBancoEntities();

        [HttpGet]
        [Route("listaemergencia/{page:int=0}/{pageSize=4}/{filter?}")]
        public HttpResponseMessage GetEmergencias(HttpRequestMessage request, int? page, int? pageSize, string filter = null)
        {

            int currentPage = page.Value;
            int currentPageSize = pageSize.Value;
            int totalEmergencias = new int();

            HttpResponseMessage response = null;



            List<emergencia> emergencias = db.emergencia.OrderBy(emer => emer.datacriado)
                .Skip(currentPage * currentPageSize)
                .Take(currentPageSize)
                .ToList();

            
            totalEmergencias = db.emergencia.Count();


            IEnumerable<EmergenciaViewModel> emergenciasVM = Mapper.Map<IEnumerable<emergencia>, IEnumerable<EmergenciaViewModel>>(emergencias);

            PaginacaoConfig<EmergenciaViewModel> pagSet = new PaginacaoConfig<EmergenciaViewModel>()
            {
                Page = currentPage,
                TotalCount = totalEmergencias,
                TotalPages = (int)Math.Ceiling((decimal)totalEmergencias / currentPageSize),
                Items = emergenciasVM
            };


            response = request.CreateResponse(HttpStatusCode.OK, pagSet);


            return response;
        }


        [HttpGet]
        [Route("listaemergenciahistorico/{filter:int}")]
        public HttpResponseMessage GetEmergenciasHistorico(HttpRequestMessage request, int filter = 0)
        {
            

            HttpResponseMessage response = null;

            if (filter != 0)
            {
                List<emergencia> emergencias = db.emergencia
               .Where(e => e.emergencia_id == filter)
               .OrderBy(emer => emer.datacriado).ToList();


                IEnumerable<EmergenciaViewModel> emergenciasVM = Mapper.Map<IEnumerable<emergencia>, IEnumerable<EmergenciaViewModel>>(emergencias);

                PaginacaoConfig<EmergenciaViewModel> pagSet = new PaginacaoConfig<EmergenciaViewModel>()
                {
                 
                    Items = emergenciasVM
                };

                response = request.CreateResponse(HttpStatusCode.OK, pagSet);
             
            }
            else
            {
                response = request.CreateResponse(HttpStatusCode.NoContent);

            }

            
            return response;
        }



        [HttpGet]
        [Route("lista_qtd_emergencia_sem_atendimento")]
        public HttpResponseMessage GetEmergenciasSemAtendimento(HttpRequestMessage request)
        {


            HttpResponseMessage response = null;

            int emergencias = db.emergencia.Where(e=>e.ativo == 1).Count();

            if (emergencias > 0)
            {
                

                response = request.CreateResponse(HttpStatusCode.OK, emergencias);
            }

            else
            {
                response = request.CreateResponse(HttpStatusCode.NoContent);

            }

            
            return response;
        }
        


        [HttpPost]
        [Route("atualizaHistoricoAlarme")]
        public HttpResponseMessage AtualizaHistoricoAlarme(HttpRequestMessage request,  EmergenciaViewModel emergenciaViewModel)
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

                    emergencia emergencia = new emergencia()
                    {
                        emergencia_id = emergenciaViewModel.IdEmergencia,
                        ativo = 0,
                        usuario_atendeu_id = idUser
                    };


                    using (ModeloBancoEntities banco = new ModeloBancoEntities())
                    {

                        banco.emergencia.Attach(emergencia);

                        banco.Entry(emergencia).State = EntityState.Modified;


                        banco.Entry(emergencia).Property(u => u.emergencia_id).IsModified = true;
                        banco.Entry(emergencia).Property(u => u.usuario_id).IsModified = false;
                        banco.Entry(emergencia).Property(u => u.usuario_atendeu_id).IsModified = true;
                        banco.Entry(emergencia).Property(u => u.latitude).IsModified = false;
                        banco.Entry(emergencia).Property(u => u.longitude).IsModified = false;
                        banco.Entry(emergencia).Property(u => u.coordenada).IsModified = false;
                        banco.Entry(emergencia).Property(u => u.datacriado).IsModified = false;
                        banco.Entry(emergencia).Property(u => u.deleted).IsModified = false;
                        banco.Entry(emergencia).Property(u => u.ativo).IsModified = true;
                       
                        banco.SaveChanges();
                        
                        response = request.CreateResponse(HttpStatusCode.OK, emergencia);
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
