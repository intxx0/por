using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.UI.WebControls;
using AutoMapper;
using WebApiKor.Models;

namespace WebApiKor.Mappers
{
    public class DomainToViewModelMappingProfile : Profile
    {


        protected override void Configure()

        {

            #region  Usuario
            Mapper.CreateMap<usuario, UsuarioViewModel>()
                .ForMember(vm => vm.UsuarioId, map => map.MapFrom(m => m.usuario_id))
                .ForMember(vm => vm.IdGrupo, map => map.MapFrom(m => m.grupo_id))
                .ForMember(vm => vm.DescGrupo, map => map.MapFrom(m => m.grupo.nome))
                .ForMember(vm => vm.DescLogin, map => map.MapFrom(m => m.login))
                .ForMember(vm => vm.DescNome, map => map.MapFrom(m => m.nome))
                .ForMember(vm => vm.DescLogin, map => map.MapFrom(m => m.login))
                .ForMember(vm => vm.Senha, map => map.MapFrom(m => m.senha))
                .ForMember(vm => vm.DataCriado, map => map.MapFrom(m => m.data_criado))
                .ForMember(vm => vm.Email, map => map.MapFrom(m => m.email))
                .ForMember(vm => vm.UltimoAcesso, map => map.MapFrom(m => m.ultimo_acesso))
                .ForMember(vm => vm.Senha, map => map.MapFrom(m => m.senha))
                .ForMember(vm => vm.Coordenada, map => map.MapFrom(m => m.coordenada))
                .ForMember(vm => vm.QtdAcessos, map => map.MapFrom(m => m.qtd_acessos))
                .ForMember(vm => vm.ImapServer, map => map.MapFrom(m => m.imap_server))
                .ForMember(vm => vm.Deletado, map => map.MapFrom(m => m.deleted))
                .ForMember(vm => vm.EmailSenha, map => map.MapFrom(m => m.email_senha))
                .ForMember(vm => vm.Ativo, map => map.MapFrom(m => m.ativo));
            #endregion



            #region Grupos

            Mapper.CreateMap<grupo, GruposViewModel>()
                .ForMember(vm => vm.IdGrupo, map => map.MapFrom(m => m.grupo_id))
                .ForMember(vm => vm.DescGrupo, map => map.MapFrom(m => m.nome))
                .ForMember(vm => vm.DataModificado, map => map.MapFrom(m => m.datamodificado))
                .ForMember(vm => vm.DataCriado, map => map.MapFrom(m => m.datacriado))
                .ForMember(vm => vm.Ativo, map => map.MapFrom(m => m.ativo));

            #endregion



            #region Emergencia

            Mapper.CreateMap<emergencia, EmergenciaViewModel>()
              .ForMember(vm => vm.IdEmergencia, map => map.MapFrom(m => m.emergencia_id))

               .ForMember(vm => vm.UsuarioId, map => map.MapFrom(m => m.usuario_id))

              .ForMember(vm => vm.DescUsuarioAtendeu, map => map.MapFrom(m => m.usuario1.nome))

              .ForMember(vm => vm.UsuarioAtendeuId, map => map.MapFrom(e => e.usuario_atendeu_id))

              .ForMember(vm => vm.DescUsuario, map => map.MapFrom(e => e.usuario.nome))

             .ForMember(vm => vm.Coordenada, map => map.MapFrom(m => m.coordenada))

             .ForMember(vm => vm.Latitude, map => map.MapFrom(m => m.latitude))

             .ForMember(vm => vm.Longitude, map => map.MapFrom(m => m.longitude))

             .ForMember(vm => vm.DataCriado, map => map.MapFrom(m => m.datacriado))

              .ForMember(vm => vm.Deletado, map => map.MapFrom(m => m.deleted))

              .ForMember(vm => vm.Ativo, map => map.MapFrom(m => m.ativo));

            #endregion



            #region Mensagem

            Mapper.CreateMap<mensagem, MensagemViewModel>()
                .ForMember(vm => vm.IdMensagem, map => map.MapFrom(m => m.mensagem_id))

                .ForMember(vm => vm.IdUsuario, map => map.MapFrom(m => m.usuario.usuario_id))

                 .ForMember(vm => vm.NomeRemetente, map => map.MapFrom(m => m.usuario.nome))


                .ForMember(vm => vm.UsuarioDestino, map => map.MapFrom(m => m.usuario1.usuario_id))

                .ForMember(vm => vm.NomeDestinatario, map => map.MapFrom(m => m.usuario1.nome))


                .ForMember(vm => vm.TextoMensagem, map => map.MapFrom(m => m.texto))

                .ForMember(vm => vm.Visualizado, map => map.MapFrom(m => m.visualizado))

                .ForMember(vm => vm.Baixado, map => map.MapFrom(m => m.baixado))

                .ForMember(vm => vm.DataCriado, map => map.MapFrom(m => m.datacriado))

                .ForMember(vm => vm.DataModificado, map => map.MapFrom(m => m.datamodificado))

                .ForMember(vm => vm.Deletado, map => map.MapFrom(m => m.deleted));

            #endregion




            #region FiscalizacoesOnLine

            Mapper.CreateMap<operacao, FiscalizacaoOnLineViewModel>()
                .ForMember(vm => vm.IdFiscalizacao, map => map.MapFrom(m => m.id_operacao))

                .ForMember(vm => vm.IdUsuario, map => map.MapFrom(m => m.usuario_id))

                .ForMember(vm => vm.IdNatureza, map => map.MapFrom(m => m.id_natureza_operacao))

                .ForMember(vm => vm.DescNaturezaOperacao, map => map.MapFrom(m => m.natureza_operacao.desc_natureza_operacao))

                .ForMember(vm => vm.NomeUsuario, map => map.MapFrom(m => m.usuario.nome))

                .ForMember(vm => vm.NomeResponsavel, map => map.MapFrom(m => m.nome_responsavel))

                .ForMember(vm => vm.CargoResponsavel, map => map.MapFrom(m => m.cargo_responsavel))

                .ForMember(vm => vm.InstituicaoResponsavel, map => map.MapFrom(m => m.instituicao_responsavel))

                .ForMember(vm => vm.EmailResposnsavel, map => map.MapFrom(m => m.email_responsavel))

                .ForMember(vm => vm.TelResponsavel, map => map.MapFrom(m => m.tel_responsavel))

                .ForMember(vm => vm.DescOperacao, map => map.MapFrom(m => m.desc_operacao))
               
                .ForMember(vm => vm.DescObservacao, map => map.MapFrom(m => m.desc_observacao))

                .ForMember(vm => vm.DataCriado, map => map.MapFrom(m => m.data_criado_operacao))

                .ForMember(vm => vm.DataFinalOperacao, map => map.MapFrom(m => m.data_final_operacao))

                .ForMember(vm => vm.Ativo, map => map.MapFrom(m => m.ativo));

            #endregion




            #region Detalhes da Fiscalizacao Off-Line e On-Line

            Mapper.CreateMap<fiscalizacao, DetalhesFiscalizacaoViewModel>()

                .ForMember(vm => vm.IdFiscalizacao, map => map.MapFrom(m => m.id_fiscalizacao))

               .ForMember(vm => vm.DataFiscalizacao, map => map.MapFrom(m => m.data_fiscalizacao))

               .ForMember(vm => vm.NumeroGuia, map => map.MapFrom(m => m.numero_guia))

               .ForMember(vm => vm.QuantidadeVerificada, map => map.MapFrom(m => m.qtd_verificada))

               .ForMember(vm => vm.PlacaRegistro, map => map.MapFrom(m => m.placa_registro))

               .ForMember(vm => vm.NotaFiscal, map => map.MapFrom(m => m.nota_fiscal))

               .ForMember(vm => vm.ValidadeDocumento, map => map.MapFrom(m => m.validade_documento))

               .ForMember(vm => vm.IdStatusFiscalizacao, map => map.MapFrom(m => m.id_status_fiscalizacao))

               .ForMember(vm => vm.DescStatusFiscalizacao, map => map.MapFrom(m => m.status_fiscalizacao.desc_status_fiscalizacao))

               .ForMember(vm => vm.IdOperacao, map => map.MapFrom(m => m.id_operacao))

               .ForMember(vm => vm.Ativo, map => map.MapFrom(m => m.ativo));

            #endregion




            #region Guias

            Mapper.CreateMap<guia, GuiaViewModel>()

                .ForMember(vm => vm.IdGuia, map => map.MapFrom(m => m.id_guia))

                .ForMember(vm => vm.NumeroGuia, map => map.MapFrom(m => m.numero_guia))

                 .ForMember(vm => vm.NomeEmisssor, map => map.MapFrom(m => m.nome_emissor))


                .ForMember(vm => vm.CnpjEmissor, map => map.MapFrom(m => m.cnpj_emissor))

                .ForMember(vm => vm.MunicipioEmissor, map => map.MapFrom(m => m.municipio_emissor))


                .ForMember(vm => vm.NomeOrigem, map => map.MapFrom(m => m.nome_origem))

                .ForMember(vm => vm.MunicipioOrigem, map => map.MapFrom(m => m.municipio_origem))

                .ForMember(vm => vm.NomeDestino, map => map.MapFrom(m => m.nome_destino))

                .ForMember(vm => vm.MunicipioDestino, map => map.MapFrom(m => m.municipio_destino))

                .ForMember(vm => vm.PlacaRegistro, map => map.MapFrom(m => m.placa_registro))

                .ForMember(vm => vm.NumeroNotaFiscal, map => map.MapFrom(m => m.numero_notafiscal))

                .ForMember(vm => vm.DataEmissaoGuia, map => map.MapFrom(m => m.data_emissao_guia))

                .ForMember(vm => vm.DataValidadeInicio, map => map.MapFrom(m => m.data_validade_inicio))

                 .ForMember(vm => vm.DataValidadeFim, map => map.MapFrom(m => m.data_validade_fim))

                 .ForMember(vm => vm.Quantidade, map => map.MapFrom(m => m.quantidade))

                 .ForMember(vm => vm.UnidadeMedida, map => map.MapFrom(m => m.unidade_medida))

                 .ForMember(vm => vm.Valor, map => map.MapFrom(m => m.valor))

                .ForMember(vm => vm.Ativo, map => map.MapFrom(m => m.ativo));

            #endregion




            #region Email

            Mapper.CreateMap<caixa_email, EmailViewModel>()

               .ForMember(vm => vm.CaixaEmailId, map => map.MapFrom(m => m.caixa_email_id))

               .ForMember(vm => vm.UsuarioId, map => map.MapFrom(m => m.usuario_id))

               .ForMember(vm => vm.Destinatario, map => map.MapFrom(m => m.destinatario))

               .ForMember(vm => vm.Remetente, map => map.MapFrom(m => m.remetente))

               .ForMember(vm => vm.Assunto, map => map.MapFrom(m => m.assunto))

               .ForMember(vm => vm.Texto, map => map.MapFrom(m => m.texto))

               .ForMember(vm => vm.Tipo, map => map.MapFrom(m => m.tipo))

               .ForMember(vm => vm.Enviado, map => map.MapFrom(m => m.enviado))

               .ForMember(vm => vm.Baixado, map => map.MapFrom(m => m.baixado))

               .ForMember(vm => vm.DataCriado, map => map.MapFrom(m => m.datacriado))

               .ForMember(vm => vm.Deletado, map => map.MapFrom(m => m.deleted))
              
               .ForMember(vm => vm.Visualizado, map => map.MapFrom(m => m.visualizado))

               .ForMember(vm => vm.Ativo, map => map.MapFrom(m => m.ativo));

            #endregion


            #region Smtp Email

            Mapper.CreateMap<smtp_email, SmtpEmailViewModel>()

          .ForMember(vm => vm.IdSmtp, map => map.MapFrom(m => m.id_smtp))
          .ForMember(vm => vm.DescSmtp, map => map.MapFrom(m => m.desc_smtp))
          .ForMember(vm => vm.Ativo, map => map.MapFrom(m => m.ativo));

            #endregion



            #region Operação - Usuario

            Mapper.CreateMap<usuario_operacao, UsuarioOperacaoViewModel>()

              .ForMember(vm => vm.IdUsuarioOperacao, map => map.MapFrom(m => m.usuario_operacao_id))

              .ForMember(vm => vm.IdUsuario, map => map.MapFrom(m => m.usuario_id))

              .ForMember(vm => vm.IdOperacao, map => map.MapFrom(m => m.id_operacao))

              .ForMember(vm => vm.Ativo, map => map.MapFrom(m => m.ativo));

            #endregion


            #region Kor

            Mapper.CreateMap<kor, KorViewModel>()

            .ForMember(vm => vm.IdKor, map => map.MapFrom(m => m.id_kor))
            .ForMember(vm => vm.Nome, map => map.MapFrom(m => m.nome_kor))
            .ForMember(vm => vm.Modelo, map => map.MapFrom(m => m.modelo_kor))
            .ForMember(vm => vm.NumeroSerie, map => map.MapFrom(m => m.numero_serie_kor))
            .ForMember(vm => vm.Ativo, map => map.MapFrom(m => m.ativo));

            #endregion


            #region Natureza Operacao

            Mapper.CreateMap<natureza_operacao, NaturezaOperacaoViewModel>()

            .ForMember(vm => vm.IdNaturezaOperacao, map => map.MapFrom(m => m.id_natureza_operacao))
            .ForMember(vm => vm.DescnaturezaOperacao, map => map.MapFrom(m => m.desc_natureza_operacao))
            .ForMember(vm => vm.Ativo, map => map.MapFrom(m => m.ativo));

            #endregion



            #region Tipo Fiscalização

            Mapper.CreateMap<tipo_fiscalizacao, TipoFiscalizacaoViewModel>()

           .ForMember(vm => vm.IdTipoFiscalizacao, map => map.MapFrom(m => m.id_tipo_fiscalizacao))
           .ForMember(vm => vm.DescTipoFiscalizacao, map => map.MapFrom(m => m.desc_tipo_fiscalizacao))
           .ForMember(vm => vm.Ativo, map => map.MapFrom(m => m.ativo));

            #endregion



            #region Tipo Fiscalização - Operação

            Mapper.CreateMap<fiscalizacao_operacao, TipoFiscalizacaoOperacaoViewModel>()

                .ForMember(vm => vm.IdTipoFiscalizacaoOperacao, map => map.MapFrom(m => m.id_fiscalizacao_operacao))
                .ForMember(vm => vm.IdOperacao, map => map.MapFrom(m => m.id_operacao))
                .ForMember(vm => vm.IdTipoFiscalizacao, map => map.MapFrom(m => m.id_fiscalizacao));

            #endregion




            #region Kor - Operação

            Mapper.CreateMap<kor_operacao, KorOperacaoViewModel>()

             .ForMember(vm => vm.IdKorOperacao, map => map.MapFrom(m => m.id_kor_operacao))
             .ForMember(vm => vm.IdKor, map => map.MapFrom(m => m.id_kor))
             .ForMember(vm => vm.IdOperacao, map => map.MapFrom(m => m.id_operacao));

            #endregion




            #region Usuario - Operacao

            Mapper.CreateMap<usuario_operacao, UsuarioOperacaoViewModel>()

                .ForMember(vm => vm.IdUsuarioOperacao, map => map.MapFrom(m => m.usuario_operacao_id))
                .ForMember(vm => vm.IdOperacao, map => map.MapFrom(m => m.operacao.id_operacao))
                .ForMember(vm => vm.IdUsuario, map => map.MapFrom(m => m.usuario.usuario_id))
                .ForMember(vm => vm.DescNomeUsuario, map => map.MapFrom(m => m.usuario.nome))
                .ForMember(vm => vm.DescLoginUsuario, map => map.MapFrom(m => m.usuario.login))
                .ForMember(vm => vm.DescEmailUsuario, map => map.MapFrom(m => m.usuario.email))
                .ForMember(vm => vm.EmailSenha, map => map.MapFrom(m => m.usuario.email_senha))
                .ForMember(vm => vm.Senha, map => map.MapFrom(m => m.usuario.senha))
                .ForMember(vm => vm.SmtpEmail, map => map.MapFrom(m => m.usuario.imap_server))
                .ForMember(vm => vm.IdGrupo, map => map.MapFrom(m => m.usuario.grupo_id))
                .ForMember(vm => vm.Ativo, map => map.MapFrom(m => m.ativo));

            #endregion



            #region DashBoard Fiscalização

            Mapper.CreateMap<fiscalizacao, DashboardFiscalizacaoViewModel>()

            .ForMember(vm => vm.IdOperacao, map => map.MapFrom(m => m.id_operacao))
            .ForMember(vm => vm.IdFiscalizacao, map => map.MapFrom(m => m.id_fiscalizacao))
            .ForMember(vm => vm.IdStatusFiscalizacao, map => map.MapFrom(m => m.id_status_fiscalizacao))
            .ForMember(vm => vm.StatusFiscalizacao, map => map.MapFrom(m => m.status_fiscalizacao.desc_status_fiscalizacao))
            .ForMember(vm => vm.DataFiscalizacao, map => map.MapFrom(m => m.data_fiscalizacao));

            #endregion

        }

    }
}