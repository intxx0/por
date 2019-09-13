﻿(function (app) {
    'use strict';

    app.controller('cadoperacaoCtrl', cadoperacaoCtrl);

    cadoperacaoCtrl.$inject = ['$scope', '$http', '$base64', '$location', 'apiService', 'notificationService', '$routeParams', '$filter', '$translate'];

    function cadoperacaoCtrl($scope, $http, $base64, $location, apiService, notificationService, $routeParams, $filter, $translate) {


        $scope.verificaIdFiscalizacaoOnLine = verificaIdFiscalizacaoOnLine;
        $scope.limparCamposOperacao = limparCamposOperacao;
        $scope.limparUsuarios = limparUsuarios;
        $scope.inserirOperacao = inserirOperacao;
        $scope.verificaIdUsuarioExiste = verificaIdUsuarioExiste;
        $scope.editarUsuariosArrayOperacao = editarUsuariosArrayOperacao;
        $scope.deleteUsuariosArrayOperacao = deleteUsuariosArrayOperacao;
        $scope.inserirUsuariosOperacao = inserirUsuariosOperacao;
        $scope.habilitaDesabilitaAbaCadOperacao = habilitaDesabilitaAbaCadOperacao;
        $scope.habilitaDesabilitaAbaCadFiscalizacoesOperacao = habilitaDesabilitaAbaCadFiscalizacoesOperacao;
        $scope.habilitaDesabilitaAbaCadKor = habilitaDesabilitaAbaCadKor;
        $scope.habilitaDesabilitaAbaCadUsuarios = habilitaDesabilitaAbaCadUsuarios;
        $scope.habilitaDesabilitaAbaConfirmacao = habilitaDesabilitaAbaConfirmacao;
        $scope.habilitaDesabilitaAbaOperacaoFinalizada = habilitaDesabilitaAbaOperacaoFinalizada;
        $scope.habilitaDesabilitaAbaPesquisa = habilitaDesabilitaAbaPesquisa;
        $scope.cancelarOperacao = cancelarOperacao;
        $scope.alterarFiscalizacao = alterarFiscalizacao;
        $scope.carregaNaturezaOperacao = carregaNaturezaOperacao;
        $scope.carregaTiposFiscalizacao = carregaTiposFiscalizacao;
        $scope.adicionarUsuarioOperacao = adicionarUsuarioOperacao;
        $scope.deletarUsuarioOperacao = deletarUsuarioOperacao;
        $scope.checkUsuariosValue = checkUsuariosValue;

        $scope.novaFiscalizacoesOnLine = {};
        $scope.novaOperacao = {};
        $scope.operacaofiscalizacao = {};
        $scope.smtpusuario = {};

        $scope.FiscalizacoesOnLine = [];
        $scope.NaturezaOperacao = [];
        $scope.TipoFiscalizacao = [];
        $scope.Kors = [];
        $scope.KorsSelecionados = [];
        $scope.Usuarios = [];
        $scope.Perfis = [];
        $scope.Instituicoes = [];
        
        $scope.novo = [];

        $scope.novoTipoOperacao = [];
        $scope.novoCheckKor = [];
        $scope.novoUsuarioOperacao = [];
        $scope.novoUsuario = {};
        $scope.modelUsuariosOperacao = [];
        $scope.UsuariosOperacao = [];
        $scope.ListaUsuariosOperacao = [];

        $scope.selected = [];
        $scope.selectedkor = [];

        $scope.botaoAvancarTipoFiscalizacao = false;
        $scope.botaoAvancarkor = false;
        $scope.botaoAvancarCadUsuario = false;
        $scope.qtdUsuariosOperacao = 0;


        var idOperacao = 0;
        var indiceArrayUsuarios = 0;
        $scope.smtpusuario = '';


        $scope.tabsOperacao = {
            tabPesquisar: {
                tabAtivar: "active",
                tabhabilitar: true,
                contentAtivar: "tab-pane fade in active",
                contentHabilitar: true
            },
            tabCadOperacao: {
                tabAtivar: "",
                tabhabilitar: false,
                contentAtivar: "tab-pane fade",
                contentHabilitar: false
            },
            tabCadFiscalizacao: {
                tabAtivar: "",
                tabhabilitar: false,
                contentAtivar: "tab-pane fade",
                contentHabilitar: false
            }
            ,
            tabCadKor: {
                tabAtivar: "",
                tabhabilitar: false,
                contentAtivar: "tab-pane fade",
                contentHabilitar: false
            }
            ,
            tabCadUsuario: {
                tabAtivar: "",
                tabhabilitar: false,
                contentAtivar: "tab-pane fade",
                contentHabilitar: false
            }
            ,
            tabConfirmacaoOperacao: {
                tabAtivar: "",
                tabhabilitar: false,
                contentAtivar: "tab-pane fade",
                contentHabilitar: false
            }
            ,
            tabOperacaoFinalizada: {
                tabAtivar: "",
                tabhabilitar: false,
                contentAtivar: "tab-pane fade",
                contentHabilitar: false
            }
        };

        //#region1----------------Verifica o objeto da Fiscalizacao On-Line--------------------
        function verificaIdFiscalizacaoOnLine(operacao, idNaturezaOperacao) {

            $scope.novaOperacao = operacao;
            $scope.novaOperacao.IdNatureza = idNaturezaOperacao.IdNaturezaOperacao;
            //$scope.IdFiscalizacao = operacao.IdFiscalizacao;

            if (operacao.IdFiscalizacao == undefined) {
                inserirOperacao();
            } else {
                alterarFiscalizacaoOnLine();
            }

        }
        //#endregion------------------------------------------------------------------------------

        //#region2---------------Inserir Fiscalizacao On-Line------------------------------
        function inserirOperacao() {

            var operacao = angular.copy($scope.novaOperacao);

            operacao.DataCriado = formataData(operacao.DataCriado);
            operacao.DataFinalOperacao = formataData(operacao.DataFinalOperacao);
            operacao.IdNatureza = $scope.natureza.IdNaturezaOperacao;

            operacao.IdFiscalizacao = 0;
            operacao.IdUsuario = 0;

            //envia objeto para Api
            apiService.post('/api/fiscalizacaoonline/inserirFiscalizacaoOnLine', operacao,
                  loadInserirSuccess,
                  loadInserirFailed);
        }

        function loadInserirSuccess(result) {

            idOperacao = result.data.IdFiscalizacao;

            //recupera os objetos do $scope
            var tipoFiscalizacaoOperacao = angular.copy($scope.novoTipoOperacao);
            var korsOperacao = angular.copy($scope.novoCheckKor);
            var usuariosOperacao = angular.copy($scope.novoUsuarioOperacao);

            //#region--------------Inserir Kors da Operação---------------------

            korsOperacao.IdOperacao = idOperacao;

            angular.forEach(korsOperacao, function (value) {

                var operacaoKor = {};

                operacaoKor.IdKor = value.IdKor;
                operacaoKor.IdOperacao = korsOperacao.IdOperacao;

                inserirKorOperacao(operacaoKor);
            });

            //#endregion

            //#region-------- Inserir tipo fiscalização para Operação---------

            tipoFiscalizacaoOperacao.IdOperacao = idOperacao;

            angular.forEach(tipoFiscalizacaoOperacao, function (value) {

                var operacaoFiscalizacaoOperacao = {};

                operacaoFiscalizacaoOperacao.IdTipoFiscalizacao = value.IdTipoFiscalizacao;
                operacaoFiscalizacaoOperacao.IdOperacao = tipoFiscalizacaoOperacao.IdOperacao;

                inserirTipoFiscalizacaoOperacao(operacaoFiscalizacaoOperacao);
            });

            //#endregion

            //#region------Inserir Usuarios na tabela de usuários----------

            inserirUsuariosOperacao($scope.UsuariosOperacao);

            //#endregion

            notificationService.displaySuccess('Fiscalização cadastrada com sucesso!', 'Atenção');
            habilitaDesabilitaAbaOperacaoFinalizada();

            $scope.Operacao = $scope.novaOperacao.DescOperacao;

            $scope.novaFiscalizacoesOnLine = {};
            $scope.novaOperacao = {};
            $scope.operacaofiscalizacao = {};
            $scope.novoTipoOperacao = [];
            $scope.novoCheckKor = [];
            //$scope.novoUsuarioOperacao = [];
            $scope.UsuariosOperacao = [];

        }

        function loadInserirFailed(result) {
            notificationService.displayError('Erro ao cadastradar Fiscalização!', 'Atenção');
        }

        //#endregion-----------------------------------------------------------------------




        //#region3---------------Alterar Fiscalizacao On-Line--------------------------

        function alterarFiscalizacao(operacao) {

            $scope.novaOperacao = operacao;

            $scope.novaOperacao.DataCriado = $filter('date')(operacao.DataCriado, 'dd-MM-yyyy HH:mm')
                .replace('-', '').replace(' ', '').replace(':', '');
            $scope.novaOperacao.DataFinalOperacao = $filter('date')(operacao.DataFinalOperacao, 'dd/MM/yyyy HH:mm')
                .replace('/', '').replace('', '').replace(':', '');
            $scope.novaOperacao.DataCriado = $scope.novaOperacao.DataCriado
                .replace('-', '').replace(' ', '');
            $scope.novaOperacao.DataFinalOperacao = $scope.novaOperacao.DataFinalOperacao
                .replace('/', '').replace(' ', '');

            $scope.natureza = { IdNaturezaOperacao: operacao.IdNatureza };

            carregaNaturezaOperacaoId(operacao.IdNatureza);
            carregaTiposFiscalizacao($scope.natureza);

            listaKorOperacao(operacao.IdFiscalizacao);
            listaFiscalizacaoOperacao(operacao.IdFiscalizacao);
            listaUsuarioOperacao(operacao.IdFiscalizacao);

            $('#step1').hide();

            $scope.tabsOperacao = {
                tabPesquisar: {
                    tabAtivar: "",
                    tabhabilitar: true,
                    contentAtivar: "tab-pane fade",
                    contentHabilitar: true
                },
                tabCadOperacao: {
                    tabAtivar: "active",
                    tabhabilitar: true,
                    contentAtivar: "tab-pane fade in active",
                    contentHabilitar: true
                },
                tabCadFiscalizacao: {
                    tabAtivar: "",
                    tabhabilitar: false,
                    contentAtivar: "tab-pane fade",
                    contentHabilitar: false
                },
                tabCadKor: {
                    tabAtivar: "",
                    tabhabilitar: false,
                    contentAtivar: "tab-pane fade",
                    contentHabilitar: false
                },
                tabCadUsuario: {
                    tabAtivar: "",
                    tabhabilitar: false,
                    contentAtivar: "tab-pane fade",
                    contentHabilitar: false
                },
                tabConfirmacaoOperacao: {
                    tabAtivar: "",
                    tabhabilitar: false,
                    contentAtivar: "tab-pane fade",
                    contentHabilitar: false
                },
                tabOperacaoFinalizada: {
                    tabAtivar: "",
                    tabhabilitar: false,
                    contentAtivar: "tab-pane fade",
                    contentHabilitar: false
                }
            };

        }

        function alterarFiscalizacaoOnLine() {

            $scope.novaOperacao.DataCriado = formataData($scope.novaOperacao.DataCriado);
            $scope.novaOperacao.DataFinalOperacao = formataData($scope.novaOperacao.DataFinalOperacao);
            $scope.novaOperacao.IdNatureza = $scope.natureza.IdNaturezaOperacao;

            apiService.post('/api/fiscalizacaoonline/alterarFiscalizacaoOnLine', $scope.novaOperacao,
                loadAtualizarSuccess,
                loadAtualizarFailed);

        }

        function loadAtualizarSuccess(result) {

            var fiscalizacao = result.data;

            $scope.novaOperacao = fiscalizacao;

            //recupera os objetos do $scope
            //var usuariosOperacao = angular.copy($scope.novoUsuarioOperacao);
            //var tipoFiscalizacaoOperacao = angular.copy($scope.novoTipoOperacao);
            var korsOperacao = angular.copy($scope.novoCheckKor);

            atualizarUsuariosOperacao($scope.novoUsuarioOperacao, fiscalizacao.IdFiscalizacao);
            atualizarKorOperacao($scope.novoCheckKor, fiscalizacao.IdFiscalizacao);
            atualizarTipoFiscalizacao($scope.novoTipoOperacao, fiscalizacao.IdFiscalizacao);

            notificationService.displaySuccess('Fiscalização alterada com sucesso!', 'Atenção');

            habilitaDesabilitaAbaOperacaoFinalizada();

            $scope.Operacao = $scope.novaOperacao.DescOperacao;

            $scope.novaFiscalizacoesOnLine = {};
            $scope.novaOperacao = {};
            $scope.natureza = {};
            $scope.instituicao = {};
            $scope.novoTipoOperacao = [];
            $scope.novoCheckKor = [];
            $scope.selected = [];
            $scope.selectedkor = [];
            $scope.smtpusuario = {};
            $scope.UsuariosOperacao = [];
        }

        function loadAtualizarFailed(result) {
            notificationService.displayError('Erro ao alterar Fiscalização!', 'Atenção');
        }

        // #region Altera Fiscalização,Kor e Usuários da Operação

        function atualizarKorOperacao(korsOperacao, operacaoId) {

            var kor = [];
            var arrayKorOperacao = [];
            var enviaKor = {};

            arrayKorOperacao = korsOperacao;

            for (var i = 0; i < arrayKorOperacao.length; i++) {

                enviaKor.IdKor = arrayKorOperacao[i].IdKor;
                enviaKor.IdOperacao = operacaoId;

                kor.push(enviaKor);

                enviaKor = {};
            }

            apiService.post('/api/fiscalizacaoonline/alterarkorOperacao', kor,
                loadAtualizaKorOperacaoSuccess,
                loadAtualizaKorOperacaoFailed);
        }

        function loadAtualizaKorOperacaoSuccess(result) {


        }

        function loadAtualizaKorOperacaoFailed(result) {
            notificationService.displayError('Erro ao atualizar kor(s) para operação.', 'Atenção!');
        }

        function atualizarTipoFiscalizacao(tipoFiscalizacaoOperacao, operacaoId) {

            var tipofiscalizacao = [];
            var arrayTipoFiscalizacaoOperacao = [];
            var enviaTipoFiscalizacaoOperacao = {};

            arrayTipoFiscalizacaoOperacao = tipoFiscalizacaoOperacao;

            for (var i = 0; i < arrayTipoFiscalizacaoOperacao.length; i++) {

                enviaTipoFiscalizacaoOperacao.IdTipoFiscalizacao = arrayTipoFiscalizacaoOperacao[i].IdTipoFiscalizacao;
                enviaTipoFiscalizacaoOperacao.IdOperacao = operacaoId;

                tipofiscalizacao.push(enviaTipoFiscalizacaoOperacao);

                enviaTipoFiscalizacaoOperacao = {};
            }

            apiService.post('/api/fiscalizacaoonline/alterarTipoFiscalizacaoOperacao', tipofiscalizacao,
                loadAtualizarTipoFiscalizacaoSuccess,
                loadAtualizarTipoFiscalizacaoFailed);
        }

        function loadAtualizarTipoFiscalizacaoSuccess(result) {

        }

        function loadAtualizarTipoFiscalizacaoFailed(result) {

            notificationService.displayError('Erro ao atualizar tipo fiscalização para operação.', 'Atenção!');
        }

        function atualizarUsuariosOperacao(usuariosOperacao, operacaoId) {

            apiService.post('/api/fiscalizacaoonline/atualizausuariooperacao/' + operacaoId, usuariosOperacao,
                loadatualizarUsuariosOperacaoSuccess,
                loadatualizarUsuariosOperacaoFailed);

            return;

            /*var usuOperacao = [];
            var arrayUsuarioOperacao = [];
            var enviaUsuarioOperacao = {};


            arrayUsuarioOperacao = usuariosOperacao;


            for (var i = 0; i < arrayUsuarioOperacao.length; i++) {

                enviaUsuarioOperacao.IdOperacao = operacaoId;

                if (arrayUsuarioOperacao[i].IdUsuario > 0) {

                    arrayUsuarioOperacao[i].UsuarioId = arrayUsuarioOperacao[i].IdUsuario;
                } else {
                    arrayUsuarioOperacao[i].UsuarioId = arrayUsuarioOperacao[i].UsuarioId;
                }

                enviaUsuarioOperacao.IdUsuario = arrayUsuarioOperacao[i].UsuarioId;
                enviaUsuarioOperacao.DescNomeUsuario = arrayUsuarioOperacao[i].DescNomeUsuario;
                enviaUsuarioOperacao.DescLoginUsuario = arrayUsuarioOperacao[i].DescLoginUsuario;
                enviaUsuarioOperacao.IdGrupo = arrayUsuarioOperacao[i].IdGrupo;
                enviaUsuarioOperacao.DescEmailUsuario = arrayUsuarioOperacao[i].DescEmailUsuario;
                enviaUsuarioOperacao.EmailSenha = $base64.encode(arrayUsuarioOperacao[i].EmailSenha);
                enviaUsuarioOperacao.Senha = $base64.encode(arrayUsuarioOperacao[i].Senha);
                enviaUsuarioOperacao.Ativo = arrayUsuarioOperacao[i].Ativo;
                enviaUsuarioOperacao.SmtpEmail = arrayUsuarioOperacao[i].SmtpEmail;


                usuOperacao.push(enviaUsuarioOperacao);

                enviaUsuarioOperacao = {};
            }


            apiService.post('/api/fiscalizacaoonline/atualizausuariooperacao', usuOperacao,
                loadatualizarUsuariosOperacaoSuccess,
                loadatualizarUsuariosOperacaoFailed);*/
        }

        function loadatualizarUsuariosOperacaoSuccess(result) {

        }

        function loadatualizarUsuariosOperacaoFailed(result) {

            notificationService.displayError('Erro ao atualizar usuários da operação.', 'Atenção!');
        }

        function listaKorOperacao(id) {

            apiService.get('/api/fiscalizacaoonline/listakoroperacao/' + id, null,
                listaKorOperacaoSuccess,
                listaKorOperacaoFailed);
        }

        function listaKorOperacaoSuccess(result) {

            var korOperacao = result.data;
            var korJaCarregado = $scope.Kors;

            angular.forEach(korOperacao, function (key, value) {
                angular.forEach(korJaCarregado, function (key2, value2) {
                    if (key.IdKor == key2.IdKor) {
                        $scope.novoCheckKor[value] = key2;
                        $scope.selectedkor[value] = key2;
                        $scope.Kors[value2].status = true;
                    }
                });

            });

        }

        function listaKorOperacaoFailed(result) {

            notificationService.displayError('Erro ao carregar kor para edição', 'Atenção!');
        }

        function listaFiscalizacaoOperacao(id) {

            apiService.get('/api/fiscalizacaoonline/listafiscalizacaooperacao/' + id, null,
                listaFiscalizacaoOperacaoSuccess,
                listaFiscalizacaoOperacaoFailed);
        }

        function listaFiscalizacaoOperacaoSuccess(result) {

            var tipofiscalizacao = result.data;
            var tipoFiscalizacaoJaCarregado = $scope.TipoFiscalizacao;

            angular.forEach(tipofiscalizacao, function (key, value) {
                angular.forEach(tipoFiscalizacaoJaCarregado, function (key2, value2) {
                    if (key.IdTipoFiscalizacao == key2.IdTipoFiscalizacao) {
                        $scope.novoTipoOperacao[value] = key2;
                        $scope.selected[value] = key2;
                        $scope.TipoFiscalizacao[value2].status = true;
                    }
                });
            });

        }

        function listaFiscalizacaoOperacaoFailed(result) {

            notificationService.displayError('Erro ao carregar fiscalização para edição!', 'Atenção!');
        }

        function listaUsuarioOperacao(id) {

            apiService.get('/api/fiscalizacaoonline/listausuariosoperacao/' + id, null,
                listaUsuarioOperacaoSuccess,
                listaUsuarioOperacaoFailed);
        }

        function listaUsuarioOperacaoSuccess(result) {

            var usuariosOperacao = result.data;

            $scope.UsuariosOperacao = usuariosOperacao;
            $scope.qtdUsuariosOperacao = usuariosOperacao.length;

            for (var i = 0; i < usuariosOperacao.length; i++) {
                for (var c = 0; c < $scope.Usuarios.length; c++) {
                    if (usuariosOperacao[i].UsuarioId == $scope.Usuarios[c].UsuarioId)
                        $scope.Usuarios.splice(c, 1);
                }
            }

        }

        function listaUsuarioOperacaoFailed(result) {

            notificationService.displayError('Erro ao carregar usuários para editar', 'Atenção!');
        }



        //#endregion


        //#endregion--------------------------------------------------------------------------------




        //#region4----------------Limpar Campos Fiscalizacao On-Line-----------------

        function limparCamposOperacao() {

            $scope.novaOperacao = {};
            $scope.operacaofiscalizacao = {};

        }

        function limparUsuarios() {
            $scope.novoUsuario = {};
            $scope.smtpusuario = {};
        }

        function cancelarOperacao() {


            habilitaDesabilitaAbaPesquisa();

            $scope.novaFiscalizacoesOnLine = {};
            $scope.novaOperacao = {};
            $scope.operacaofiscalizacao = {};
            $scope.smtpusuario = {};
            $scope.novoTipoOperacao = [];
            $scope.novoCheckKor = [];
            $scope.novoUsuarioOperacao = [];
            $scope.selectedkor = [];
            $scope.selected = [];

            $scope.botaoAvancarTipoFiscalizacao = false;
            $scope.botaoAvancarkor = false;
            $scope.botaoAvancarCadUsuario = false;

        }

        //#endregion-----------------------------------------------------------------




        //#region6-------------------Carregar Operacao------------------------------
        function carregarOperacao(page) {

            page = page || 0;

            var config = {
                params: {
                    page: page,
                    pageSize: 200,
                    filter: ''
                }
            };

            apiService.get('/api/fiscalizacaoonline/listafiscalizacaoonline', config,
                loadOperacaoSuccess,
                loadOperacaoFailed);
        }

        function loadOperacaoSuccess(result) {


            $scope.novaFiscalizacoesOnLine = result.data;



        }

        function loadOperacaoFailed(result) {

            notificationService.displayError('Erro ao passar parâmetro para o cadastro de operações!');
        }
        //#endregion------------------------------------------------------------------




        //#region7-----------Carrega Natureza Operação------------------------------

        function carregaNaturezaOperacao(page) {

            page = page || 0;

            var config = {
                params: {
                    page: page,
                    pageSize: 100,
                    filter: $scope.filterNaturezaOperacao
                }
            };

            apiService.get('/api/naturezaoperacao/listanaturezaoperacao', config,
                loadNaturezaSuccess,
                loadNaturezaFaliled);

        }

        function loadNaturezaSuccess(result) {
            $scope.NaturezaOperacao = result.data.Items;
        }

        function loadNaturezaFaliled(result) {
            notificationService.displayError('Erro ao carregar a natureza para operações', 'Atenção');
        }

        function carregaNaturezaOperacaoId(id) {
            apiService.get('/api/naturezaoperacao/naturezaoperacaoid/' + id, null,
                carregaNaturezaOperacaoSucccess,
                carregaNaturezaOperacaoFailed);
        }

        function carregaNaturezaOperacaoSucccess(result) {
            $scope.natureza = result.data;
        }

        function carregaNaturezaOperacaoFailed(result) {
            notificationService.displayError('Erro ao carregar a natureza da operação por ID');
        }
        //#endregion-----------------------------------------------------------------




        //#region8-------------Carrega Tipo de Fiscalização----------------------
        /*function carregaTipoFiscalizacao(page) {

            page = page || 0;

            var config = {
                params: {
                    page: page,
                    pageSize: 100,
                    filter: $scope.filterTipoFiscalizacao
                }
            };

            apiService.get('/api/tipofiscalizacao/listatipofiscalizacao', config,
                loadTipoFiscalizacaoSuccess,
                loadTipoFiscalizacaoFaliled);
        }

        function loadTipoFiscalizacaoSuccess(result) {

            $scope.TipoFiscalizacao = result.data.Items;
        }

        function loadTipoFiscalizacaoFaliled(result) {

            notificationService.displayError('Erro ao carregar tipo de fiscalização', 'Atenção!');
        }
        */
        //#endregion---------------------------------------------------------------




        //#region9-------------Selecionar CheckBox e Inserir Tipo Operação--------

        $scope.toggle = function (tipofiscalizacao, list) {

            $scope.novoTipoOperacao = list;

            var idx = $scope.novoTipoOperacao.indexOf(tipofiscalizacao);

            if (idx > -1) {
                $scope.novoTipoOperacao.splice(idx, 1);
            } else {
                $scope.novoTipoOperacao.push(tipofiscalizacao);
            }

        };

        $scope.exists = function (tipofiscalizacao, list) {

            $scope.novoTipoOperacao = list;

            //Habilta ou desabilita botão de próximo
            if ($scope.novoTipoOperacao.length > 0) {
                $scope.botaoAvancarTipoFiscalizacao = true;
            } else {
                $scope.botaoAvancarTipoFiscalizacao = false;
            }

            return $scope.novoTipoOperacao.indexOf(tipofiscalizacao) > -1;
        };

        $scope.isChecked = function () {
            return $scope.selected.length === $scope.TipoFiscalizacao.length;
        };

        $scope.toggleAll = function () {

            if ($scope.selected.length === $scope.TipoFiscalizacao.length) {
                $scope.selected = [];


            } else if ($scope.selected.length === 0 || $scope.selected.length > 0) {
                $scope.selected = $scope.TipoFiscalizacao.slice(0);
            }
        };


        //Pegar '$scope.novoTipoOperacao' e inserir Id no banco de dados

        //usando um angular.Foreach(objeto,function(){
        //      
        //    Aqui a rotina para chamar a Api e inserir no banco de dados
        //
        //});



        //#endregion--------------------------------------------------------------------




        //#region10--------------------Carrega Kors----------------------------------

        function carregaKor(page) {

            page = page || 0;

            var config = {
                params: {
                    page: page,
                    pageSize: 400,
                    filter: $scope.filterKor
                }
            };

            apiService.get('/api/kor/listakor', config,
                loadKorSuccess,
                loadKorFaliled);
        }

        function loadKorSuccess(result) {

            $scope.Kors = result.data.Items;
        }

        function loadKorFaliled(result) {

            notificationService.displayError('Erro ao carregar Kor(s)', 'Atenção!');
        }

        //#endregion--------------------------------------------------------------------





        //#region11-------------Selecionar CheckBox e Inserir Kor---------------
        $scope.togglekor = function (kor, list) {

            $scope.novoCheckKor = list;

            var idx = $scope.novoCheckKor.indexOf(kor);

            if (idx > -1) {

                $scope.novoCheckKor.splice(idx, 1);
            }
            else {
                $scope.novoCheckKor.push(kor);
            }
        };


        $scope.existskor = function (kor, list) {

            $scope.novoCheckKor = list;

            //Habilta ou desabilita botão de próximo
            if ($scope.novoCheckKor.length > 0) {

                $scope.botaoAvancarkor = true;

            }
            else {

                $scope.botaoAvancarkor = false;
            }

            return $scope.novoCheckKor.indexOf(kor) > -1;
        };


        $scope.isCheckedkor = function () {

            return $scope.selectedkor.length === $scope.Kors.length;
        };


        $scope.toggleAllkor = function () {

            if ($scope.selectedkor.length === $scope.Kors.length) {

                $scope.selectedkor = [];


            } else if ($scope.selectedkor.length === 0 || $scope.selectedkor.length > 0) {

                $scope.selectedkor = $scope.Kors.slice(0);
            }
        };


        //#endregion----------------------------------------------------------------




        //#region12------------------Carrega Smtp E-mails------------------------

        function carregaSmtpEmails() {

            apiService.get('/api/smtp/listasmtpemails', null, loadCarregaSmtpEmailSuccess, loadCarregaSmtpEmailFailed);
        }

        function loadCarregaSmtpEmailSuccess(result) {

            $scope.SmtpEmail = result.data;
        }

        function loadCarregaSmtpEmailFailed(result) {

            notificationService.displayError('Erro ao carregar Smtp dos E-mails', 'Atenção');
        }
        //#endregion--------------------------------------------------------------




        // #region 13----------------Carrega Grupo de Usuário---------------------

        function loadUsuarios(page) {

            page = page || 0;

            var config = {
                params: {
                    page: page,
                    pageSize: 100,
                    filter: ''
                }
            };

            apiService.get('/api/usuarios/listausuarios', config, loadUsuariosSuccess, loadUsuariosFailed)

        }

        function loadUsuariosSuccess(result) {

            var usuarios = result.data.Items;

            for (var i = 0; i < usuarios.length; i++) {
                var usuario = usuarios[i].DescNome;
                $scope.Usuarios.push(result.data.Items[i]);
            }
        }

        function loadUsuariosFailed(result) {
            notificationService.displayError('Erro ao carregar lista de usuários', 'Atenção!');
        }

        function loadPerfis(page) {

            page = page || 0;

            var config = {
                params: {
                    page: page,
                    pageSize: 100,
                    filter: ''
                }
            };

            apiService.get('/api/perfil/listaperfil', config, loadPerfisSuccess, loadPerfisFailed)

        }

        function loadPerfisSuccess(result) {

            var perfis = result.data.Items;

            for (var i = 0; i < perfis.length; i++) {
                var perfil = perfis[i].DescNome;
                $scope.Perfis.push(result.data.Items[i]);
            }
        }

        function loadPerfisFailed(result) {
            notificationService.displayError('Erro ao carregar lista de perfis', 'Atenção!');
        }

        // #endregion -------------------------------------------------------------




        //#region14---------------Usuarios da Operação------------------------------

        function verificaIdUsuarioExiste(usuario) {

            if (usuario.UsuarioId > 0 || usuario.IdUsuario > 0) {

                if (usuario.Senha !== usuario.ConfirmarSenha) {

                    notificationService.displayError('Senha do KOR não confere.', 'Atenção!');
                    return;
                }

                var usuarios = {};

                if (usuario.IdUsuario > 0) {

                    usuario.UsuarioId = usuario.IdUsuario;
                }

                var smtpEmail = $scope.smtpusuario.DescSmtp;

                usuarios.UsuarioId = usuario.UsuarioId;
                usuarios.DescNome = usuario.DescNomeUsuario;
                usuarios.IdGrupo = usuario.IdGrupo;
                usuarios.DescLogin = usuario.DescLoginUsuario;
                usuarios.Email = usuario.DescEmailUsuario;
                usuarios.ImapServer = smtpEmail;
                usuarios.EmailSenha = usuario.EmailSenha;
                usuarios.Senha = usuario.Senha;
                usuarios.Ativo = usuario.Ativo;

                alterarUsuarios(usuarios);

                $scope.novoUsuario = {};
                $scope.smtpusuario = {};

            } else {

                inserirUsuariosArray(usuario);
            }
        }


        function inserirUsuariosArray(usuario) {

            var usu = [];

            if (usuario.Senha !== usuario.ConfirmarSenha) {

                notificationService.displayError('Senha do KOR não confere.', 'Atenção!');

            } else {

                usuario.SmtpEmail = $scope.smtpusuario.DescSmtp;

                usu.push(usuario);

          
                if (usu.length > 0) {

                    $scope.botaoAvancarCadUsuario = true;
                }

                $scope.novoUsuario = {};
                $scope.smtpusuario = {};
                $scope.botaoAvancarCadUsuario = true;


                //#region------Inserir Usuarios na tabela de usuários----------

                angular.forEach(usu, function (value) {

                    var usuarios = {};

                    usuarios.DescNome = value.DescNomeUsuario;
                    usuarios.IdGrupo = value.IdGrupo;
                    usuarios.DescLogin = value.DescLoginUsuario;
                    usuarios.Email = value.DescEmailUsuario;
                    usuarios.ImapServer = value.SmtpEmail;
                    usuarios.EmailSenha = value.EmailSenha;
                    usuarios.Senha = value.Senha;
                    usuarios.Ativo = value.Ativo;

                    inserirUsuarios(usuarios);


                });


                //#endregion


            }

        }


        function editarUsuariosArrayOperacao(usuario) {

            var idSmtp = 0;

            $scope.novoUsuario = usuario;

            $scope.novoUsuario.IdGrupo = usuario.IdGrupo;
            $scope.novoUsuario.Senha = $base64.decode(usuario.Senha);
            $scope.novoUsuario.ConfirmarSenha = $scope.novoUsuario.Senha;
            $scope.novoUsuario.EmailSenha = $base64.decode(usuario.EmailSenha);


            var smtpCarregado = angular.copy($scope.SmtpEmail);

            var usu = usuario;

            for (var i = 0; i < smtpCarregado.length; i++) {

                if (usuario.SmtpEmail == smtpCarregado[i].DescSmtp || usuario.ImapServer == smtpCarregado[i].DescSmtp) {

                    idSmtp = smtpCarregado[i].IdSmtp;
                }

            }

            $scope.smtpusuario = { IdSmtp: idSmtp };

            var index = $scope.novoUsuarioOperacao.indexOf(usuario);
            $scope.novoUsuarioOperacao.splice(index, 1);
            $scope.botaoAvancarCadUsuario = false;


        }


        function deleteUsuariosArrayOperacao(usuario) {

            deletaUsuarios(usuario);

        }




        //#endregion---------------------------------------------------------




        //#region15--------------------Inserir Kor Operação--------------------------

        function inserirKorOperacao(korOperacao) {

            $scope.koroperacao = korOperacao;

            apiService.post('/api/fiscalizacaoonline/inserirKorOperacao', $scope.koroperacao,
                loadInserirKorOperacaoSuccess,
                loadInserirKorOperacaoFailed);


        }

        function loadInserirKorOperacaoSuccess(result) {

            //notificationService.displaySuccess('Kor inserido na operação com sucesso', 'Atenção!');
        }

        function loadInserirKorOperacaoFailed(result) {

            notificationService.displayError('Erro ao inserir Kor', 'Atenção!');

        }


        //#endregion



        //#region16----------------Inserir Tipo Fiscalização Operação---------------

        function inserirTipoFiscalizacaoOperacao(fiscalizacaooperacao) {

            $scope.tpfiscalizacao = fiscalizacaooperacao;

            apiService.post('/api/fiscalizacaoonline/inserirFiscalizacaoOperacao', $scope.tpfiscalizacao,
                loadtipofiscalizacaosuccess,
                loadtipofiscalizacaofailed);

        }

        function loadtipofiscalizacaosuccess(result) {

            // notificationService.displaySuccess('Fiscalização para operação inserida com sucesso!', 'Atenção');
        }

        function loadtipofiscalizacaofailed(result) {

            notificationService.displaySuccess(result.status);

        }



        //#endregion---------------------------------------------------------------




        //#region17----------------Inserir Usuario na tabela de Usuários---------------

        function inserirOperacaoUsuariosKor(usuarios) {

            apiService.post('/api/fiscalizacaoonline/inseriroperacaousuarioskor', usuarios,
                loadusuariosuccess,
                loadusuariofailed);

        }

        function loadusuariosuccess(result) {

            var usuarioNovo = result.data;

            usuarioNovo.UsuarioId = usuarioNovo.UsuarioId;
            usuarioNovo.DescNomeUsuario = usuarioNovo.DescNome;
            usuarioNovo.IdGrupo = usuarioNovo.IdGrupo;
            usuarioNovo.DescLoginUsuario = usuarioNovo.DescLogin;
            usuarioNovo.DescEmailUsuario = usuarioNovo.Email;
            usuarioNovo.ImapServer = usuarioNovo.ImapServer;
            usuarioNovo.EmailSenha = usuarioNovo.EmailSenha;
            usuarioNovo.Senha = usuarioNovo.Senha;
            usuarioNovo.Ativo = usuarioNovo.Ativo;

            $scope.novoUsuarioOperacao.push(usuarioNovo);
        }

        function loadusuariofailed(result) {

            notificationService.displaySuccess('Erro ao inserir usuário na tabela de usuários', 'Atenção');

        }


        function alterarUsuarios(usuarios) {

            $scope.usuario = usuarios;

            //criptografa as senhas de acesso a maleta e e-mail antes de enviar ao server
            $scope.usuario.Senha = $base64.encode($scope.usuario.Senha);
            $scope.usuario.EmailSenha = $base64.encode($scope.usuario.EmailSenha);

            apiService.post('/api/usuarios/alterarusuario', $scope.usuario,
                loadalterarusuariosuccess,
                loadalterarusuariofailed);
        }

        function loadalterarusuariosuccess(result) {

            var usuario = result.data;
            var usu = {};

            usu.UsuarioId = usuario.UsuarioId;
            usu.DescNomeUsuario = usuario.DescNome;
            usu.IdGrupo = usuario.IdGrupo;
            usu.DescLoginUsuario = usuario.DescLogin;
            usu.DescEmailUsuario = usuario.Email;
            usu.ImapServer = usuario.ImapServer;
            usu.EmailSenha = usuario.EmailSenha;
            usu.Senha = usuario.Senha;
            usu.Ativo = usuario.Ativo;


            $scope.novoUsuarioOperacao.push(usu);
            $scope.botaoAvancarCadUsuario = true;

        }

        function loadalterarusuariofailed(result) {

            notificationService.displayError('Erro ao atualizar usuário', 'Atenção!');
        }


        function deletaUsuarios(usuarios) {


            if (usuarios.IdUsuario > 0) {

                usuarios.UsuarioId = usuarios.IdUsuario;
            }

            indiceArrayUsuarios = $scope.novoUsuarioOperacao.indexOf(usuarios);


            apiService.post('/api/usuarios/deletarusuario/' + usuarios.UsuarioId, null,
                loaddeletausuariosSuccess,
                loaddeletausuariosFailed);

        }

        function loaddeletausuariosSuccess(result) {
            notificationService.displaySuccess("Usuário deletado com sucesso.", "Atenção");
            $scope.novoUsuarioOperacao.splice(indiceArrayUsuarios, 1);
            verificaTamanhoArrayUsuarios();
        }

        function loaddeletausuariosFailed(result) {
            notificationService.displayError('Erro ao deletar usuário.', 'Atenção');
        }

        //#endregion---------------------------------------------------------------




        //#region18----------------Inserir Usuario para Operação---------------

        function inserirUsuariosOperacao(usuariosOperacao) {

            var usuarioOperacao = {};

            for (var i = 0; i < usuariosOperacao.length; i++) {

                usuarioOperacao.UsuarioId = usuariosOperacao[i].Usuario.UsuarioId;
                usuarioOperacao.IdOperacao = idOperacao;
                usuarioOperacao.IdKor = usuariosOperacao[i].Kor.IdKor;
                usuarioOperacao.IdPerfil = usuariosOperacao[i].Perfil.IdPerfil;
                usuarioOperacao.Ativo = 1;

                apiService.post('/api/fiscalizacaoonline/inserirusuariosoperacao', usuarioOperacao,
                        loadusuariooperacaosuccess,
                        loadusuariooperacaofailed);

                usuarioOperacao = {};

            }

        }

        function loadusuariooperacaosuccess(result) {
            notificationService.displaySuccess('Usuários inseridos para Operação com sucesso!');
        }

        function loadusuariooperacaofailed(result) {
            notificationService.displaySuccess('Erro ao associar usuários para operação!', 'Atenção');
        }



        //#endregion---------------------------------------------------------------




        //#region19--------------Associar usuários na Operação--------------


        //#endregion




        //#region20---------------Habiltar Abas ao clicar-----------------------------

        function habilitaDesabilitaAbaCadOperacao() {

            //$scope.novaFiscalizacoesOnLine = {};
            //$scope.novaOperacao = {};
            //$scope.operacaofiscalizacao = {};
            //$scope.smtpusuario = {};
            //$scope.novoTipoOperacao = [];
            //$scope.novoCheckKor = [];
            //$scope.novoUsuarioOperacao = [];
            //$scope.selectedkor = [];
            //$scope.selected = [];

            $scope.tabsOperacao = {
                tabPesquisar: {
                    tabAtivar: "",
                    tabhabilitar: true,
                    contentAtivar: "tab-pane fade",
                    contentHabilitar: false
                },
                tabCadOperacao: {
                    tabAtivar: "active",
                    tabhabilitar: true,
                    contentAtivar: "tab-pane fade in active",
                    contentHabilitar: true
                },
                tabCadFiscalizacao: {
                    tabAtivar: "",
                    tabhabilitar: false,
                    contentAtivar: "tab-pane fade",
                    contentHabilitar: false
                }

              ,
                tabCadKor: {
                    tabAtivar: "",
                    tabhabilitar: false,
                    contentAtivar: "tab-pane fade",
                    contentHabilitar: false
                }

              ,
                tabCadUsuario: {
                    tabAtivar: "",
                    tabhabilitar: false,
                    contentAtivar: "tab-pane fade",
                    contentHabilitar: false
                }

              ,
                tabConfirmacaoOperacao: {
                    tabAtivar: "",
                    tabhabilitar: false,
                    contentAtivar: "tab-pane fade",
                    contentHabilitar: false
                }
              ,
                tabOperacaoFinalizada: {
                    tabAtivar: "",
                    tabhabilitar: false,
                    contentAtivar: "tab-pane fade",
                    contentHabilitar: false
                }
            };

        }

        function habilitaDesabilitaAbaCadFiscalizacoesOperacao() {

            loadInstituicaoById($scope.novaOperacao.IdInstituicao);

            $scope.tabsOperacao = {
                tabPesquisar: {
                    tabAtivar: "",
                    tabhabilitar: true,
                    contentAtivar: "tab-pane fade",
                    contentHabilitar: false
                },
                tabCadOperacao: {
                    tabAtivar: "",
                    tabhabilitar: true,
                    contentAtivar: "tab-pane fade",
                    contentHabilitar: false
                },
                tabCadFiscalizacao: {
                    tabAtivar: "active",
                    tabhabilitar: true,
                    contentAtivar: "tab-pane fade in active",
                    contentHabilitar: true
                }

              ,
                tabCadKor: {
                    tabAtivar: "",
                    tabhabilitar: false,
                    contentAtivar: "tab-pane fade",
                    contentHabilitar: false
                }

              ,
                tabCadUsuario: {
                    tabAtivar: "",
                    tabhabilitar: false,
                    contentAtivar: "tab-pane fade",
                    contentHabilitar: false
                }

              ,
                tabConfirmacaoOperacao: {
                    tabAtivar: "",
                    tabhabilitar: false,
                    contentAtivar: "tab-pane fade",
                    contentHabilitar: false
                }
              ,
                tabOperacaoFinalizada: {
                    tabAtivar: "",
                    tabhabilitar: false,
                    contentAtivar: "tab-pane fade",
                    contentHabilitar: false
                }
            };

        }

        function habilitaDesabilitaAbaCadKor() {

            $scope.tabsOperacao = {
                tabPesquisar: {
                    tabAtivar: "",
                    tabhabilitar: true,
                    contentAtivar: "tab-pane fade",
                    contentHabilitar: false
                },
                tabCadOperacao: {
                    tabAtivar: "",
                    tabhabilitar: true,
                    contentAtivar: "tab-pane fade",
                    contentHabilitar: false
                },
                tabCadFiscalizacao: {
                    tabAtivar: "",
                    tabhabilitar: true,
                    contentAtivar: "tab-pane fade",
                    contentHabilitar: false
                }

              ,
                tabCadKor: {
                    tabAtivar: "active",
                    tabhabilitar: true,
                    contentAtivar: "tab-pane fade  in active",
                    contentHabilitar: true
                }

              ,
                tabCadUsuario: {
                    tabAtivar: "",
                    tabhabilitar: false,
                    contentAtivar: "tab-pane fade",
                    contentHabilitar: false
                }

              ,
                tabConfirmacaoOperacao: {
                    tabAtivar: "",
                    tabhabilitar: false,
                    contentAtivar: "tab-pane fade",
                    contentHabilitar: false
                }
              ,
                tabOperacaoFinalizada: {
                    tabAtivar: "",
                    tabhabilitar: false,
                    contentAtivar: "tab-pane fade",
                    contentHabilitar: false
                }
            };

        }

        function habilitaDesabilitaAbaCadUsuarios() {

            var qtdUsuariosArray = $scope.novoUsuarioOperacao.length;

            if (qtdUsuariosArray > 0) {

                $scope.botaoAvancarCadUsuario = true;
            }

            $scope.KorsSelecionados = $scope.novoCheckKor;
            //console.log($scope.KorsSelecionados);

            $scope.tabsOperacao = {
                tabPesquisar: {
                    tabAtivar: "",
                    tabhabilitar: true,
                    contentAtivar: "tab-pane fade",
                    contentHabilitar: false
                },
                tabCadOperacao: {
                    tabAtivar: "",
                    tabhabilitar: true,
                    contentAtivar: "tab-pane fade",
                    contentHabilitar: false
                },
                tabCadFiscalizacao: {
                    tabAtivar: "",
                    tabhabilitar: true,
                    contentAtivar: "tab-pane fade",
                    contentHabilitar: false
                }

              ,
                tabCadKor: {
                    tabAtivar: "",
                    tabhabilitar: true,
                    contentAtivar: "tab-pane fade",
                    contentHabilitar: false
                }

              ,
                tabCadUsuario: {
                    tabAtivar: "active",
                    tabhabilitar: true,
                    contentAtivar: "tab-pane fade in active",
                    contentHabilitar: true
                }

              ,
                tabConfirmacaoOperacao: {
                    tabAtivar: "",
                    tabhabilitar: false,
                    contentAtivar: "tab-pane fade",
                    contentHabilitar: false
                }
              ,
                tabOperacaoFinalizada: {
                    tabAtivar: "",
                    tabhabilitar: false,
                    contentAtivar: "tab-pane fade",
                    contentHabilitar: false
                }
            };

        }

        function habilitaDesabilitaAbaConfirmacao() {

            $scope.tabsOperacao = {
                tabPesquisar: {
                    tabAtivar: "",
                    tabhabilitar: true,
                    contentAtivar: "tab-pane fade",
                    contentHabilitar: false
                },
                tabCadOperacao: {
                    tabAtivar: "",
                    tabhabilitar: true,
                    contentAtivar: "tab-pane fade",
                    contentHabilitar: false
                },
                tabCadFiscalizacao: {
                    tabAtivar: "",
                    tabhabilitar: true,
                    contentAtivar: "tab-pane fade",
                    contentHabilitar: false
                }

              ,
                tabCadKor: {
                    tabAtivar: "",
                    tabhabilitar: true,
                    contentAtivar: "tab-pane fade",
                    contentHabilitar: false
                }

              ,
                tabCadUsuario: {
                    tabAtivar: "",
                    tabhabilitar: true,
                    contentAtivar: "tab-pane fade",
                    contentHabilitar: false
                }

              ,
                tabConfirmacaoOperacao: {
                    tabAtivar: "active",
                    tabhabilitar: true,
                    contentAtivar: "tab-pane fade in active",
                    contentHabilitar: true
                }
              ,
                tabOperacaoFinalizada: {
                    tabAtivar: "",
                    tabhabilitar: false,
                    contentAtivar: "tab-pane fade",
                    contentHabilitar: false
                }
            };

        }

        function habilitaDesabilitaAbaOperacaoFinalizada() {

            $scope.tabsOperacao = {
                tabPesquisar: {
                    tabAtivar: "",
                    tabhabilitar: true,
                    contentAtivar: "tab-pane fade",
                    contentHabilitar: false
                },
                tabCadOperacao: {
                    tabAtivar: "",
                    tabhabilitar: false,
                    contentAtivar: "tab-pane fade",
                    contentHabilitar: false
                },
                tabCadFiscalizacao: {
                    tabAtivar: "",
                    tabhabilitar: false,
                    contentAtivar: "tab-pane fade",
                    contentHabilitar: false
                }

              ,
                tabCadKor: {
                    tabAtivar: "",
                    tabhabilitar: false,
                    contentAtivar: "tab-pane fade",
                    contentHabilitar: false
                }

              ,
                tabCadUsuario: {
                    tabAtivar: "",
                    tabhabilitar: false,
                    contentAtivar: "tab-pane fade",
                    contentHabilitar: false
                }

              ,
                tabConfirmacaoOperacao: {
                    tabAtivar: "",
                    tabhabilitar: false,
                    contentAtivar: "tab-pane fade",
                    contentHabilitar: false
                }
              ,
                tabOperacaoFinalizada: {
                    tabAtivar: "active",
                    tabhabilitar: true,
                    contentAtivar: "tab-pane fade in active",
                    contentHabilitar: true
                }
            };

        }

        function habilitaDesabilitaAbaPesquisa() {

            carregarOperacao();
            $scope.novoUsuarioOperacao = [];

            $('#step1').show();

            $scope.tabsOperacao = {
                tabPesquisar: {
                    tabAtivar: "active",
                    tabhabilitar: true,
                    contentAtivar: "tab-pane fade in active",
                    contentHabilitar: true
                },
                tabCadOperacao: {
                    tabAtivar: "",
                    tabhabilitar: false,
                    contentAtivar: "tab-pane fade",
                    contentHabilitar: false
                },
                tabCadFiscalizacao: {
                    tabAtivar: "",
                    tabhabilitar: false,
                    contentAtivar: "tab-pane fade",
                    contentHabilitar: false
                },
                tabCadKor: {
                    tabAtivar: "",
                    tabhabilitar: false,
                    contentAtivar: "tab-pane fade",
                    contentHabilitar: false
                },
                tabCadUsuario: {
                    tabAtivar: "",
                    tabhabilitar: false,
                    contentAtivar: "tab-pane fade",
                    contentHabilitar: false
                },
                tabConfirmacaoOperacao: {
                    tabAtivar: "",
                    tabhabilitar: false,
                    contentAtivar: "tab-pane fade",
                    contentHabilitar: false
                },
                tabOperacaoFinalizada: {
                    tabAtivar: "",
                    tabhabilitar: false,
                    contentAtivar: "tab-pane fade",
                    contentHabilitar: false
                }
            };
        }

        //#endregion




        // #region ---- Load ao carregar a página de cadastro de operações---------------


        carregarOperacao();
        carregaNaturezaOperacao();
        //carregaTipoFiscalizacao();
        carregaKor();
        carregaSmtpEmails();
        loadInstituicoes();
        loadUsuarios();
        loadPerfis();

        // #endregion 

        //#region FormataData

        function formataData(data) {

            var dia = 0;
            var mes = 0;
            var ano = 0;
            var hora = 0;
            var minuto = 0;

            dia = data.substring(0, 2);
            mes = data.substring(2, 4);
            ano = data.substring(4, 8);
            hora = data.substring(8, 10);
            minuto = data.substring(10, 12);


            var dataformatada = dia + "/" + mes + "/" + ano + " " + hora + ":" + minuto;

            return dataformatada;

        }

        //#endregion

        //verifica o tamanho do Array de Usuários para habilitar e desabilitar o botão de avançar
        function verificaTamanhoArrayUsuarios() {

            if ($scope.novoUsuarioOperacao.length > 0) {

                $scope.botaoAvancarCadUsuario = true;
            } else {
                $scope.botaoAvancarCadUsuario = false;
            }
        }

        function carregaTiposFiscalizacao(natureza) {
            if (natureza != 'undefined') {
                var config = {
                    params: {
                        IdNaturezaOperacao: natureza.IdNaturezaOperacao
                    }
                };
                apiService.get('/api/tipofiscalizacao/tiposfiscalizacao', config, loadCarregaTiposFiscalizacaoSuccess, loadCarregaTiposFiscalizacaoFailed)
            }
        }

        function loadCarregaTiposFiscalizacaoSuccess(result) {
            $scope.TipoFiscalizacao = result.data;
        }

        function loadCarregaTiposFiscalizacaoFailed(result) {
            notificationService.displayError('Erro ao carregar tipos de fiscalização', 'Atenção!');
        }

        function loadInstituicoes(page) {

            page = page || 0;

            var config = {
                params: {
                    page: page,
                    pageSize: 100,
                    filter: ''
                }
            };

            apiService.get('/api/instituicao/listainstituicao', config, loadInstituicoesSuccess, loadInstituicoesFailed)

        }

        function loadInstituicoesSuccess(result) {

            var instituicoes = result.data.Items;

            for (var i = 0; i < instituicoes.length; i++) {
                var usuario = instituicoes[i].DescNome;
                $scope.Instituicoes.push(result.data.Items[i]);
            }
        }

        function loadInstituicoesFailed(result) {
            notificationService.displayError('Erro ao carregar lista de instituições', 'Atenção!');
        }

        function loadInstituicaoById(idInstituicao) {
            apiService.get('/api/instituicao/instituicaobyid/' + idInstituicao, null, loadInstituicaoByIdSuccess, loadInstituicaoByIdFailed)
        }

        function loadInstituicaoByIdSuccess(result) {
            $scope.instituicao = result.data;
        }

        function loadInstituicaoByIdFailed(result) {
            notificationService.displayError('Erro ao carregar instituição', 'Atenção!');
        }

        function adicionarUsuarioOperacao(idKor, idUsuario, idPerfil) {

            var korsSelecionados = $scope.KorsSelecionados,
                usuarios = $scope.Usuarios,
                perfis = $scope.Perfis,
                row = {},
                i = 0;
            
            for (i = 0; i < korsSelecionados.length; i++) {
                if (korsSelecionados[i].IdKor == idKor) {
                    row.Kor = korsSelecionados[i];
                }
            }

            for (i = 0; i < usuarios.length; i++) {
                if (usuarios[i].UsuarioId == idUsuario) {
                    row.Usuario = usuarios[i];
                    $scope.Usuarios.splice(i, 1);
                    $scope.formUsuariosOperacao.usuarios.$setValidity('text', false);
                }
            }

            for (i = 0; i < perfis.length; i++)
                if (perfis[i].IdPerfil == idPerfil)
                    row.Perfil = perfis[i];

            $scope.UsuariosOperacao.push(row);
            $scope.qtdUsuariosOperacao++;

        }

        function deletarUsuarioOperacao(usuarioOperacao, index) {
            
            $scope.Usuarios.push(usuarioOperacao.Usuario);
            $scope.UsuariosOperacao.splice(index, 1);
            $scope.qtdUsuariosOperacao--;
            return;

        }

        function checkUsuariosValue() {

            if ($scope.formUsuariosOperacao.usuarios.$dirty)
                $scope.formUsuariosOperacao.usuarios.$setValidity('text', true);
            else
                $scope.formUsuariosOperacao.usuarios.$setValidity('text', false);

        }

    }

})(angular.module('AppKor'));