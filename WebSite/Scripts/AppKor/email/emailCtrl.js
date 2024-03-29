﻿(function (app) {
    'use strict';

    app.controller('emailCtrl', emailCtrl);

    emailCtrl.$inject = ['$scope', '$http', 'apiService', 'notificationService', '$modal', '$translate'];

    function emailCtrl($scope, $http, apiService, notificationService, $modal, $translate) {


        $scope.enviarEmail = enviarEmail;
        $scope.limparCampos = limparCampos;
        $scope.abreModal = abreModal;
        $scope.sortBy = sortBy;
        $scope.emailsRecebidos = emailsRecebidos;
        $scope.emailsEnviados = emailsEnviados;
        $scope.emailsExcluidos = emailsExcluidos;
        $scope.atualizaCampoDelete = atualizaCampoDelete;
        $scope.selecionarLinhas = selecionarLinhas;
        $scope.deletaCamposSelecionados = deletaCamposSelecionados;
        $scope.atualizaCamposDeletaEmailExcluidos = atualizaCamposDeletaEmailExcluidos;

        $scope.novoEmail = {};
        $scope.EmailsRecebidos = [];
        $scope.EmailsEnviados = [];
        $scope.EmailsExcluidos = [];

        $scope.EmailsRecebidosTotal = 0;
        $scope.EmailsEnviadosTotal = 0;

        $scope.propertyName = '';
        $scope.reverse = true;



        //1--------------------Inserir Email no banco e Enviar--------------------
        function enviarEmail() {

            apiService.post('/api/email/enviaremail', $scope.novoEmail,
                loadEnviarEmailSuccess,
                loadEnviarEmailFailed);
        }


        function loadEnviarEmailSuccess(result) {

            notificationService.displaySuccess($translate.instant('EMAIL_SEND_SUCCESS') + "!", $translate.instant('ATTENTION'));

            limparCampos();
        }


        function loadEnviarEmailFailed(result) {

            notificationService.displayError(result.status.Message, $translate.instant('ATTENTION'));
        }
        //1------------------Fim--------------------------------------------------



        //2--------------Carregar Emails Recebidos--------------------------------
        function emailsRecebidos(page) {

            page = page || 0;

            var config = {
                params: {
                    page: page,
                    pageSize: 20,
                    tipoEnvio: 2,
                    filter: $scope.filtroEmailsRecebidos
                }
            };

            apiService.get('/api/email/carregaremails', config,
                loadEmailsRecebidosSuccess,
                loadEmailsRecebidosFailed);
        }

        function loadEmailsRecebidosSuccess(result) {

            $scope.EmailsRecebidos = result.data.Items;
            $scope.EmailsRecebidosTotal = result.data.Items.length;

        }

        function loadEmailsRecebidosFailed(result) {

            notificationService.displayError($translate.instant('ERROR_LOAD_EMAILS') + '! ' + result.status);

        }
        //2----------------------------Fim---------------------------------------




        //3--------------Carregar Emails Enviados-----------------------------

        function emailsEnviados(page) {

            page = page || 0;

            var config = {
                params: {
                    page: page,
                    pageSize: 20,
                    tipoEnvio: 1,
                    filter: $scope.filtroEmailsEnviados
                }
            };

            apiService.get('/api/email/carregaremails', config, loadEmailsenviadosSuccess, loadEmailsEnviadosFailed);
        }

        function loadEmailsenviadosSuccess(result) {

            $scope.EmailsEnviados = result.data.Items;
            $scope.EmailsEnviadosTotal = result.data.Items.length;

        }

        function loadEmailsEnviadosFailed(result) {

            notificationService.displayError($translate.instant('ERROR_LOAD_SEND_EMAILS') + '! ' + result.status);

        }
        //3---------------------Fim------------------------------------------


        //4--------------------Limpar Campos do Email-----------------------------
        function limparCampos() {
            $scope.novoEmail = {};
        }
        //4--------------------Fim Limpar Campos do Email-------------------------


        //5------------------Ordenar por cabecario-----------------------------

        function sortBy(propertyName) {
            $scope.reverse = ($scope.propertyName === propertyName) ? !$scope.reverse : false;
            $scope.propertyName = propertyName;
        };

        //5-------------------------------------------------------------------



        //6---------------Abre Modal com detalhes do email------------------



        function abreModal(email) {

            $modal.open({
                animation: true,
                templateUrl: 'scripts/AppKor/email/modaldetalhesemail.html',
                controller: 'detalhesEmailModalCtrl',
                size: 'lg',
                resolve: {
                    items: function () {
                        return email;
                    }
                }

            });


            atualizaEmailParaVisualizado(email);

        }

        //6----------------------------------------------------------------



        // #region Atualiza Emails para visualizados
        function atualizaEmailParaVisualizado(email) {

            apiService.post('/api/email/atualizaemailparavisualizado', email,
                ladAtualizaEmailVisualizadoSuccess,
                ladAtualizaEmailVisualizadoFailed);
        }

        function ladAtualizaEmailVisualizadoSuccess(result) {

            emailsRecebidos();
        }

        function ladAtualizaEmailVisualizadoFailed(result) {

            notificationService.displayError($translate.instant('ERROR_UPDATE_EMAIL') + '!');
        }

        // #endregion



        //7--------------Carregar Emails Excluidos------------------------

        function emailsExcluidos(page) {

            page = page || 0;

            var config = {
                params: {
                    page: page,
                    pageSize: 20,
                    tipoEnvio: 0,
                    filter: $scope.filtroEmailsExcluidos
                }
            };

            apiService.get('/api/email/carregaremails', config, loadEmailsExcluidosSuccess, loadEmailsExcluidosFailed);
        }

        function loadEmailsExcluidosSuccess(result) {

            $scope.EmailsExcluidos = result.data.Items;

        }

        function loadEmailsExcluidosFailed(result) {

            notificationService.displayError($translate.instant('ERROR_LOAD_DELETED_EMAILS') + '! ' + result.status);

        }

        //7---------------------------------------------------------------


        //8-------------------Deletar Email-----------------------------------

        function atualizaCampoDelete(email) {

            apiService.post('/api/email/atualizaCampoDeleteEmail', email,
                loadAtualizaCampoDeleteSucccess,
                loadAtualizaCampoDeleteFailed);
        }

        function loadAtualizaCampoDeleteSucccess(result) {

            notificationService.displaySuccess($translate.instant('SUCCESS_DELETE_EMAIL') + '!');

            emailsRecebidos();
            emailsEnviados();
            emailsExcluidos();
        }

        function loadAtualizaCampoDeleteFailed(result) {

            notificationService.displaySuccess($translate.instant('ERROR_DELETE_EMAIL') + '!', $translate.instant('ERROR') + ': ' + result.status);

        }
        //8------------------------------------------------------------------


        //9-------------------Selecionar todas as linhas da tabela-----------

        function selecionarLinhas(tipo) {

            //recebido = 0
            //enviado = 1
            //excluido = 2

            if ($scope.selectedAll) {

                $scope.selectedAll = true;
                $scope.botaodelete = true;

            } else {

                $scope.selectedAll = false;
                $scope.botaodelete = false;
            }

            if (tipo === 2) {

                angular.forEach($scope.EmailsExcluidos, function (item) {
                    item.Selected = $scope.selectedAll;

                });
            }

            if (tipo === 1) {


                angular.forEach($scope.EmailsEnviados, function (item) {
                    item.Selected = $scope.selectedAll;
                });
            }

            if (tipo === 0) {

                angular.forEach($scope.EmailsRecebidos, function (item) {
                    item.Selected = $scope.selectedAll;
                });
            }

        }

        function deletaCamposSelecionados(tipo) {

            //recebido = 0
            //enviado = 1
            //excluido = 2

            if (tipo === 0) {

                var teste = 0;

                angular.forEach($scope.EmailsRecebidos, function (item) {

                    if (item.Selected) {

                        funcaoEnviaParaExcluidos(item);
                    }

                });

            }


            if (tipo === 1) {

                angular.forEach($scope.EmailsEnviados, function (item) {

                    if (item.Selected) {

                        funcaoEnviaParaExcluidos(item);
                    }
                });
            }


            if (tipo === 2) {


                angular.forEach($scope.EmailsExcluidos, function (item) {

                    if (item.Selected) {

                        funcaoEnviaParaExcluidos(item);

                    }

                });
            }


        }

        function funcaoEnviaParaExcluidos(objeto) {

            atualizaCampoDelete(objeto);

        }
        //9------------------------------------------------------------------


        //10-------------Atualiza Campo Deleta Email Excluidos-----------------------

        function atualizaCamposDeletaEmailExcluidos(excluidos) {

            apiService.post('/api/email/atualizaCampoDeleteEmailExcluidos', excluidos,
               loadAtualizaCampoDeleteEmailExcluidosSucccess,
               loadAtualizaCampoDeleteEmailExcluidosFailed);

        }

        function loadAtualizaCampoDeleteEmailExcluidosSucccess(result) {

            notificationService.displaySuccess($translate.instant('SUCCESS_RETURN_EMAILS'), $translate.instant('ATTENTION'));

            emailsRecebidos();
            emailsEnviados();
            emailsExcluidos();

        }

        function loadAtualizaCampoDeleteEmailExcluidosFailed(result) {

            notificationService.displayError($translate.instant('ERROR_UPDATE_DELETE') + result.status, 'Atenção');

        }
        //10---------------------------------------------------------------


        emailsRecebidos();
        emailsEnviados();
        emailsExcluidos();
    }

})(angular.module('AppKor'));