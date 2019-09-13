(function (app) {
    'use strict';

    app.controller('cadcontaemailCtrl', cadcontaemailCtrl);

    cadcontaemailCtrl.$inject = ['$scope', 'apiService', 'notificationService', '$translate'];

    function cadcontaemailCtrl($scope, apiService, notificationService, $translate) {

        $scope.limparCampos = limparCampos;
        $scope.pegaDadosContaEmail = pegaDadosContaEmail;
        $scope.verificaInserirAtualizarContaEmail = verificaInserirAtualizarContaEmail;
        $scope.excluirContaEmail = excluirContaEmail;
        $scope.ContasEmails = [];
        $scope.Usuarios = [];

        $scope.modelcontaemail = {};

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


        function carregarContaEmail(page) {

            page = page || 0;

            var config = {
                params: {
                    page: page,
                    pageSize: 10,
                    filter: $scope.filtercontaemail
                }
            };

            apiService.get('/api/contaemail/listacontaemail', config,
                loadContaEmailSucess,
                loadContaEmailFailed);

        }

        function loadContaEmailSucess(result) {

            $scope.ContasEmails = result.data.Items;

            $scope.page = result.data.Page;
            $scope.pagesCount = result.data.TotalPages;
            $scope.totalCount = result.data.TotalCount;

        }

        function loadContaEmailFailed(result) {

            notificationService.displayError($translate.instant('ERROR_LOAD_EMAIL_ACCOUNTS') + '. <br />' + $translate.instant('ERROR') + ': ' + result.status, $translate.instant('ATTENTION') + '!');
        }

        function verificaInserirAtualizarContaEmail(novoContaEmail) {

            $scope.modelcontaemail.NomeUsuario = '';
            $scope.modelcontaemail.SmtpAuth = $scope.modelcontaemail.SmtpAuth ? 1 : 0;
            $scope.modelcontaemail.Pop3Delete = $scope.modelcontaemail.Pop3Delete ? 1 : 0;

            if (novoContaEmail) {
                atualizaContaEmail();
            } else {
                inserirContaEmail();
            }

        }

        function inserirContaEmail() {
            apiService.post('/api/contaemail/inserircontaemail', $scope.modelcontaemail,
                loadInserirContaEmailSuccess,
                loadInserirContaEmailFailed);
        }

        function loadInserirContaEmailSuccess(result) {

            $scope.modelcontaemail = result.data.Items;

            notificationService.displaySuccess($translate.instant('SUCCESS_SAVE_EMAIL_ACCOUNT'), $translate.instant('ATTENTION') + '!');
            limparCampos();
            carregarContaEmail();

        }

        function loadInserirContaEmailFailed(result) {
            notificationService.displayError($translate.instant('ERROR_INSERT_EMAIL_ACCOUNT') + '. <br />' + $translate.instant('ERROR') + ': ' + result.status, $translate.instant('ATTENTION') + '!');
        }

        function pegaDadosContaEmail(contaemail) {
            $scope.modelcontaemail = contaemail;
        }

        function atualizaContaEmail() {
            apiService.post('/api/contaemail/alterarcontaemail', $scope.modelcontaemail, loadAtualizaContaEmailSuccess, loadAtualizaContaEmailFailed);
        }

        function excluirContaEmail(contaemail) {
            if (confirm($translate.instant('CONFIRM_DELETE')))
                apiService.post('/api/contaemail/excluircontaemail', contaemail, loadExcluirContaEmailSuccess, loadExcluirContaEmailFailed);
        }

        function loadAtualizaContaEmailSuccess(result) {

            $scope.modelcontaemail = result.data.Items;

            notificationService.displaySuccess($translate.instant('SUCCESS_UPDATE_EMAIL_ACCOUNT'), $translate.instant('ATTENTION') + '!');
            carregarContaEmail();
        }

        function loadAtualizaContaEmailFailed(result) {
            notificationService.displayError($translate.instant('ERROR_UPDATE_EMAIL_ACCOUNT') + '. <br />' + $translate.instant('ERROR') + ': ' + result.status, $translate.instant('ATTENTION') + '!');
        }

        function loadExcluirContaEmailSuccess(result) {

            $scope.modelcontaemail = result.data.Items;

            notificationService.displaySuccess($translate.instant('SUCCESS_DELETE_EMAIL_ACCOUNT'), $translate.instant('ATTENTION') + '!');
            carregarContaEmail();
        }

        function loadExcluirContaEmailFailed(result) {

            notificationService.displayError($translate.instant('ERROR_DELETE_EMAIL_ACCOUNT') + '. <br />' + $translate.instant('ERROR') + ': ' + result.status, $translate.instant('ATTENTION') + '!');
        }

        function limparCampos() {

            $scope.modelcontaemail = {};

        }

        carregarContaEmail();
        loadUsuarios();
    }

})(angular.module('AppKor'));