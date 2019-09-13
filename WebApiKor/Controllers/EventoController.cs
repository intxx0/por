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

    [RoutePrefix("api/evento")]
    public class EmergenciaController : ApiController
    {
        ModeloBancoEntities db = new ModeloBancoEntities();

        [HttpGet]
        [Route("listareventos")]
        public HttpResponseMessage GetEventos(HttpRequestMessage request)
        {
            HttpResponseMessage response = null;

            var eventos = db.evento.ToList();

            List<EventoViewModel> eventosVM = Mapper.Map<List<evento>, List<EventoViewModel>>(eventos);

            eventosVM = eventosVM.OrderByDescending(e => e.DataCriado).ToList();

            response = request.CreateResponse(HttpStatusCode.OK, eventosVM);

            return response;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("listareventosfiltro")]
        public HttpResponseMessage GetEventosByFiltro(HttpRequestMessage request, [FromUri]String Lista)
        {
            HttpResponseMessage response = null;

            var filtroVM = Newtonsoft.Json.JsonConvert.DeserializeObject<BuscaFiscalizacoesViewModel>(Lista);

            int? idAtividade = filtroVM.IdTipoFiscalizacao,
                idKor = filtroVM.IdKor,
                idUsuario = filtroVM.IdUsuario;

            DateTime dt_inicio = DateTime.Parse(filtroVM.DataInicio),
                     dt_final = DateTime.Parse(filtroVM.DataTermino);

            List<evento> Eventos = db.evento.Where(e => ((e.id_tipo_fiscalizacao == idAtividade) || e.id_tipo_fiscalizacao == null)
                                               && (e.datacriado >= dt_inicio && e.datacriado <= dt_final))
                                               .ToList();

            if (idKor != null)
                Eventos = Eventos.Where(e => e.id_kor == idKor).ToList();
            if (idUsuario != null)
                Eventos = Eventos.Where(e => e.usuario_id == idUsuario).ToList();

            List<EventoViewModel> eventosVM = Mapper.Map<List<evento>, List<EventoViewModel>>(Eventos);

            eventosVM = eventosVM.OrderByDescending(e => e.DataCriado).ToList();

            response = request.CreateResponse(HttpStatusCode.OK, eventosVM);

            return response;
        }

        [HttpGet]
        [Route("listaemergenciasativas")]
        public HttpResponseMessage GetEmergenciasHistorico(HttpRequestMessage request)
        {
            HttpResponseMessage response = null;

            var eventos = db.evento.Where(e => e.tipo_evento_id == 1 && e.usuario_id_visualizado == null).ToList();

            List<EventoViewModel> eventosVM = Mapper.Map<List<evento>, List<EventoViewModel>>(eventos);

            eventosVM = eventosVM.OrderByDescending(e => e.DataCriado).ToList();

            response = request.CreateResponse(HttpStatusCode.OK, eventosVM);

            return response;
        }



        [HttpGet]
        [Route("lista_qtd_eventos_nao_visualizados")]
        public HttpResponseMessage GetEventosNaoVisualizados(HttpRequestMessage request)
        {
            HttpResponseMessage response = null;

            int eventosAtivos = db.evento.Where(e => e.usuario_id_visualizado == null).Count();

            if (eventosAtivos > 0)
                response = request.CreateResponse(HttpStatusCode.OK, eventosAtivos);
            else
                response = request.CreateResponse(HttpStatusCode.NoContent);
            
            return response;
        }

        [HttpPost]
        [Route("atualizaevento")]
        public HttpResponseMessage AtualizaEvento(HttpRequestMessage request, EventoViewModel eventoVM)
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
                var evento = db.evento.Where(e => e.evento_id == eventoVM.EventoId).FirstOrDefault();

                evento.usuario_id_visualizado = eventoVM.usuarioIdVisualizado;
                db.SaveChanges();

                response = request.CreateResponse(HttpStatusCode.OK);
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
