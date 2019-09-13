using AutoMapper;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using WebApiKor.Models;

namespace WebApiKor.Controllers
{
    [Authorize(Roles = "Administrador, Operador")]
    [RoutePrefix("api/imagens")]
    public class ImagensController : ApiController
    {
        ModeloBancoEntities db = new ModeloBancoEntities();

        [AllowAnonymous]
        [HttpPost]
        [Route("inseririmagens")]
        public HttpResponseMessage InserirImagem(HttpRequestMessage request, ImagensViewModel imagensViewModel)
        {
            HttpResponseMessage response = null;

            imagem novaImagem = new imagem()
            {
                nome = imagensViewModel.NomeImagem,
                imagem_id = imagensViewModel.ImagemId,
                img_base64 = imagensViewModel.ImagemBase64
            };

            response = request.CreateResponse(HttpStatusCode.Created, novaImagem);

            return response;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("comprimirimagens")]
        public HttpResponseMessage comprimirImagem(HttpRequestMessage request, ImagensViewModel imagensViewModel)
        {
            HttpResponseMessage response = null;

            imagem novaImagem = new imagem()
            {
                nome = imagensViewModel.NomeImagem,
                img_base64 = imagensViewModel.ImagemBase64,
            };


            novaImagem.img_base64 = comprimirImagem(novaImagem.img_base64, novaImagem.nome, 10L);

            response = request.CreateResponse(HttpStatusCode.Created, novaImagem);

            return response;
        }
        public String comprimirImagem(string base64String, string nomeArq, long qualidade)
        {
            byte[] imageBytes = Convert.FromBase64String(base64String);

            using (var streamBitmap = new MemoryStream(imageBytes))
            {
                using (Image img = Image.FromStream(streamBitmap))
                {
                    //Parametros para compressão
                    ImageCodecInfo codec = ImageCodecInfo.GetImageEncoders().First(enc => enc.FormatID == ImageFormat.Jpeg.Guid);
                    EncoderParameters imgParams = new EncoderParameters(1);
                    if (nomeArq.Contains(".png"))
                        qualidade += 30;
                    imgParams.Param = new[] { new EncoderParameter(Encoder.Quality, qualidade) };

                    var novaLargura = 0;
                    var novaAltura = 0;

                    //Redimensionamento
                    if (img.Width >= img.Height)
                    {
                        novaAltura = 720;
                        novaLargura = (img.Width * 720) / img.Height;
                    }
                    else
                    {
                        novaLargura = 720;
                        novaAltura = (img.Height * 720) / img.Width;
                    }

                    //Cria a imagem
                    var novaImagem = new Bitmap(novaLargura, novaAltura);

                    using (var graphics = Graphics.FromImage(novaImagem))
                        graphics.DrawImage(img, 0, 0, novaLargura, novaAltura);

                    //Cria um stream na memoria para armazenar a imagem
                    MemoryStream ms = new MemoryStream();

                    novaImagem.Save(ms, ImageFormat.Jpeg);
                    byte[] buffer = ms.ToArray();
                    string serialized = Convert.ToBase64String(buffer);

                    return serialized;
                }
            }
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("listarimagens")]
        public HttpResponseMessage ListarImagens(HttpRequestMessage request)
        {
            //Cada pasta dentro de 'operacoes' representa uma operação, sendo nomeada com o ID correspondente
            HttpResponseMessage response = null;

            //Abre o diretorio de operações e atribui ao array Files
            DirectoryInfo Dir = new DirectoryInfo(HttpContext.Current.Server.MapPath("~\\Gallery\\operacoes"));
            FileInfo[] Files = Dir.GetFiles("*", SearchOption.AllDirectories);
            string[,] ops = new string[Files.Length, 5];
            int i = 0;

            List<operacao> operacoes = db.operacao.OrderByDescending(ope => ope.id_operacao).ToList();

            //Insere no array 'ops' as informações de cada operação e imagens
            //Cada linha contendo um arquivo com os atributos operacao_id, nomeImagem, Localização, descricao e Titulo da operação
            //No frontend é feito o tratamento de separação de cada operação.
            foreach (FileInfo File in Files)
            {
                string opId = File.DirectoryName, desc = "", titulo = "";
                string[] chunks = opId.Split('\\');
                opId = chunks[chunks.Length - 1];

                foreach (var op in operacoes)
                {
                    if (op.id_operacao == int.Parse(opId))
                    {
                        desc = op.desc_observacao;
                        titulo = op.desc_operacao;
                    }
                }

                ops[i, 0] = opId;
                ops[i, 1] = File.Name;
                ops[i, 2] = "http:///por.icconsulting.com.br/Gallery/operacoes/" + opId + "/" + File.Name;
                ops[i, 3] = desc;
                ops[i, 4] = titulo;

                i++;
            }


            response = request.CreateResponse(HttpStatusCode.Created, ops);

            return response;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("listarimagensfiltro")]
        public HttpResponseMessage GetImagens(HttpRequestMessage request, [FromUri]String filtro)
        {
            HttpResponseMessage response = null;

            BuscaFiscalizacoesViewModel filtroVM = Newtonsoft.Json.JsonConvert.DeserializeObject<BuscaFiscalizacoesViewModel>(filtro);

            int? idAtividade = filtroVM.IdTipoFiscalizacao,
                idUsuario = filtroVM.IdUsuario;

            DateTime dt_inicio = DateTime.Parse(filtroVM.DataInicio),
                     dt_final = DateTime.Parse(filtroVM.DataTermino);


            var anexosFisc = db.anexo_fiscalizacao.Where(af => af.id_tipo_fiscalizacao == idAtividade)
                            .Select(af => af.id_anexo).ToList();


            var anexos = db.anexo.Where(a => anexosFisc.Contains(a.id_anexo)).ToList();

            anexos = anexos.Where(a => a.datacriado >= dt_inicio && a.datacriado <= dt_final).ToList();

            if (idUsuario != null)
                anexos = anexos.Where(a => a.usuario_id == idUsuario).ToList();


            AnexoViewModel anexosVM = new AnexoViewModel();

            if (anexos.Count != 0)
            {
                anexosVM.nome = "http:///localhost:62272/Gallery/operacoes/" + anexos[0].nome;
                anexosVM.nomeArq = anexos[0].desc_anexo;
                anexosVM.data_criado = anexos[0].datacriado;
                anexosVM.NomeUsuario = anexos[0].usuario.nome;

                anexosVM.album = Mapper.Map<List<anexo>, List<AnexoViewModel>>(anexos);

                anexosVM.album.ForEach(a => a.nome = "http:///localhost:62272/Gallery/operacoes/" + a.nome);

                anexosVM.album.RemoveAt(0);

                response = request.CreateResponse(HttpStatusCode.Created, anexosVM);
            }
            else
                response = request.CreateResponse(HttpStatusCode.NoContent);

            return response;
        }

    }
}
