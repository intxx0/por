﻿(function (app) {
    'use strict';

    app.controller('mensagensCtrl', mensagensCtrl);

    mensagensCtrl.$inject = ['$scope', '$http', 'apiService', '$routeParams', 'notificationService', '$timeout', '$rootScope', '$modal', '$translate'];

    function mensagensCtrl($scope, $http, apiService, $routeParams, notificationService, $timeout, $rootScope, $modal, $translate) {


        $scope.novaMensagem = {};
        $scope.MensagensRecebidas = [];
        $scope.inserirMensagem = inserirMensagem;
        $scope.limparCampos = limparCampos;
        $scope.carregarMensagensRecebidas = carregarConversas;
        $scope.loadMensagens = carregarMensagens;

        $scope.abreModalNovaConversa = abreModalNovaConversa;
        $scope.abreModalVoz = abreModalVoz;
        $scope.abreModalImagens = abreModalImagens;
        $scope.removeImagem = removeImagem;

        $scope.usuarioId = 0;
        $scope.userId = 0;

        $rootScope.imgsrc = [];
        $scope.Mensagens = [];
        $scope.AnexoSrc = [];

        var anxMsg = {};
        var promise;
        var contador = 15;


        function ativarRefresh() {
            contador--;
            if (contador === 0) {

                carregarConversas();
                contador = 15;
            }
            promise = $timeout(ativarRefresh, 1000);
        }

        function removeImagem(id) {

            for (var i = 0; i < $rootScope.imgsrc.length; i++) {
                if (id == $rootScope.imgsrc[i].nomeArq) {

                    $rootScope.imgsrc.splice(i, 1);
                }
            }
        }

        //2------Carrega todas as mensagens da conversa com o usuario logado e o usuario selecionado-----
        function carregarMensagens(idUser) {

            $scope.userId = idUser;

            apiService.get('/api/mensagem/listamensagens/' + idUser + "/" + $scope.UsuarioId, null,
                loadMensagensSuccess,
                loadMensagensFailed);
        }

        function loadMensagensSuccess(result) {

            $scope.Mensagens = result.data;
            $scope.novaMensagem.UsuarioDestino = $scope.userId;

            atualizarStatusMensagens($scope.userId);
        }

        function loadMensagensFailed(result) {

            notificationService.displayInfo($translate.instant('ERROR_LOADING_MESSAGES') + "!\n " + result.status, $translate.instant('ATTENTION') + "!<br /><br />");
        }

        //2------------------------------------------------------------------------------------



        //3--------------------Inserir Mensagem------------------------------------------------

        function inserirMensagem() {

            if (validaCampos()) {

                var userData = JSON.parse(localStorage.getItem('userData'));
                $scope.novaMensagem.IdUsuario = userData.UsuarioId;

                apiService.post('/api/mensagem/inserirmensagem', $scope.novaMensagem,
                    inserirMensagensSuccess,
                    inserirMensagensFailed);
            }
        }

        function inserirMensagensSuccess(result) {

            $scope.novaMensagem = result.data;

            notificationService.displaySuccess($translate.instant('MESSAGE_SEND_SUCCESS') + '!', result.status, $translate.instant('ATTENTION') + '!');

            carregarConversas();
            carregarMensagens($scope.userId);

            $rootScope.imgsrc.usuario_id = $scope.novaMensagem.IdUsuario;

            var userData = JSON.parse(localStorage.getItem('userData'));

            anxMsg.IdMensagem = result.data.IdMensagem;

            forEach($rootScope.imgsrc, function (value, key) {

                value.usuario_id = userData.UsuarioId;

                apiService.post('/api/mensagem/inseriranexo', value,
                    inserirAnexosSuccess,
                    inserirAnexosFailed);
            });

            delete $rootScope.imgsrc.usuario_id;
            limparCampos();
        }

        function inserirMensagensFailed(result) {
            notificationService.displayError($translate.instant('ERROR_SEND_MESSAGE') + '!', result.status, $translate.instant('ATTENTION') + '!');
        }

        function inserirAnexosSuccess(result) {

            anxMsg.id_anexo = result.data.id_anexo;

            apiService.post('/api/mensagem/inseriranexomensagem', anxMsg,
                inserirAnexosMsgSuccess,
                inserirAnexosMsgFailed);

        }
        function inserirAnexosFailed(result) {
            console.log(result.data);
        }

        function inserirAnexosMsgSuccess(result) {
            console.log(result.data);
        }
        function inserirAnexosMsgFailed(result) {
            console.log(result.data);
        }

        //3------------------Fim---------------------------------------------------------------



        //4----Carrega todas as mensagens do banco enviadas e recebidas pelo usuário logado-----
        function carregarConversas() {

            $scope.UsuarioId = (JSON.parse(localStorage.userData).UsuarioId);

            apiService.get('/api/mensagem/listaconversas/' + $scope.UsuarioId, null,
                carregarConversasSuccess,
                carregarConversasFailed);

        }

        function carregarConversasSuccess(result) {

            $scope.MensagensRecebidas = result.data;
        }

        function carregarConversasFailed(result) {

            notificationService.displayInfo($translate.instant('ERROR_LOADING_MESSAGES') + '!\n' + result.data.status, $translate.instant('ATTENTION') + '!');
        }

        //4-------------------------------------------------------------------------------------



        //5----------------Atualiza status das mensagens-------------------------------------
        function atualizarStatusMensagens(idUser) {

            var conversaDados = $scope.MensagensRecebidas.find(m => m.usuario_destino_id == idUser || m.usuario_id == idUser);

            $scope.Mensagem = {
                Visualizado: 1,
                qtd: conversaDados.count_mensagem
            };

            apiService.post('/api/mensagem/atualizastatusmensagens/' + idUser + "/" + $scope.UsuarioId, $scope.Mensagem,
                atualizarStatusMensagensSuccess,
                atualizarStatusMensagensFailed);
        }

        function atualizarStatusMensagensSuccess(result) {

            $rootScope.MensagensNaoLidas = $rootScope.MensagensNaoLidas - $scope.Mensagem.qtd;

            var conversaAtual = $scope.MensagensRecebidas.find(u => (u.usuario_destino_id != $scope.UsuarioId) ? u.usuario_destino_id == $scope.userId : u.usuario_id == $scope.userId);

            var index = $scope.MensagensRecebidas.indexOf(conversaAtual);

            $scope.MensagensRecebidas[index].count_mensagem = 0;
        }

        function atualizarStatusMensagensFailed(result) {

            notificationService.displayError($translate.instant('ERROR_MARK_MESSAGES'));
        }

        //5------------------------------------------------------------------------------------



        //6-------------------Limpa campos das mensagens-----------------------------------------

        function limparCampos() {

            $scope.novaMensagem.TextoMensagem = "";
            $rootScope.imgsrc = [];
        }

        //6-------------------------------------------------------------------------------------



        //7--------------------Valida Campos antes de enviar a mensagem-------------------------

        function validaCampos() {

            if ($scope.novaMensagem != null) {

                if ($scope.novaMensagem.UsuarioDestino == undefined) {

                    notificationService.displayError($translate.instant('CHOOSE_RECIPIENT'));
                    return false;
                }

                if ($scope.novaMensagem.TextoMensagem == undefined || $scope.novaMensagem.TextoMensagem == "") {

                    notificationService.displayError($translate.instant('ENTER_MESSAGE'));
                    return false;
                }

            }
            return true;

        }

        //7------------------------------------------------------------------------------------



        //8--------------------------Abre modal para gravar voz--------------------------------

        function abreModalVoz() {

            $modal.open({
                animation: true,
                templateUrl: 'scripts/AppKor/mensagens/modalmensagemaudio.html',
                controller: 'modalMensagemAudioCtrl',
                size: 'lg',
                resolve: {
                    items: function () {
                        return '';
                    }
                }
            });
        }

        //8-------------------------------------------------------------------------------------


        //9------------------------Abre modal de imagens---------------------------------------

        function abreModalImagens() {

            $modal.open({
                animation: true,
                templateUrl: 'scripts/AppKor/mensagens/modalenviaimagem.html',
                controller: 'modalenviaimagemCtrl',
                size: 'lg',
                resolve: {
                    items: function () {
                        return $rootScope.imgsrc;
                    }
                }
            });
        }

        //9------------------------------------------------------------------------------------


        //9------------------------Abre modal de imagens---------------------------------------

        function abreModalNovaConversa() {

            $modal.open({
                animation: true,
                templateUrl: 'scripts/AppKor/mensagens/modalnovaconversa.html',
                controller: 'modalnovaconversaCtrl',
                size: 'lg',
                resolve: {
                    items: function () {
                        return $scope.UsuarioId;
                    }
                }
            }).result.then(function (result) {

                var usuarioTemp = {};

                usuarioTemp.texto = "";
                usuarioTemp.nome = result.NomeUsuario;
                usuarioTemp.count_mensagem = 0;
                usuarioTemp.datacriado = new Date();
                usuarioTemp.usuario_destino_id = result.UsuarioId;

                $scope.userId = result.UsuarioId;
                $scope.novaMensagem.UsuarioDestino = $scope.userId;

                $scope.MensagensRecebidas.push(usuarioTemp);
                $scope.Mensagens = [];
            });;
        }

        //9------------------------------------------------------------------------------------


        $(".mytext").on("keyup", function (e) {
            if (e.which == 13) {
                inserirMensagem();
            }
        });

        carregarConversas();
        ativarRefresh();

        //Destrói as chamadas de tempos em tempos ao sair da tela
        $scope.$on('$destroy', function () {

            $timeout.cancel(promise);

        });
    }
})(angular.module('AppKor'));