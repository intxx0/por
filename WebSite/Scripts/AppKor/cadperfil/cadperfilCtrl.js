(function (app) {
    'use strict';

    app.controller('cadperfilCtrl', cadperfilCtrl);

    cadperfilCtrl.$inject = ['$scope', 'apiService', 'notificationService', '$translate'];

    function cadperfilCtrl($scope, apiService, notificationService, $translate) {

        $scope.limparCampos = limparCampos;
        $scope.pegaDadosPerfil = pegaDadosPerfil;
        $scope.verificaInserirAtualizarPerfil = verificaInserirAtualizarPerfil;
        $scope.excluirPerfil = excluirPerfil;

        $scope.modelperfil = {};

        function carregarPerfil(page) {

            page = page || 0;

            var config = {
                params: {
                    page: page,
                    pageSize: 10,
                    filter: $scope.filterperfil
                }
            };

            apiService.get('/api/perfil/listaperfil', config,
                loadPerfilSucess,
                loadPerfilFailed);

        }

        function loadPerfilSucess(result) {

            $scope.Missoes = result.data.Items;

            $scope.page = result.data.Page;
            $scope.pagesCount = result.data.TotalPages;
            $scope.totalCount = result.data.TotalCount;

        }

        function loadPerfilFailed(result) {

            notificationService.displayError($translate.instant('ERROR_LOAD_ROLES') + '. <br />' + $translate.instant('ERROR') + ': ' + result.status, $translate.instant('ATTENTION') + '!');
        }

        function verificaInserirAtualizarPerfil(novoPerfil) {

            if (novoPerfil) {
                atualizaPerfil();
            } else {
                inserirPerfil();
            }

        }

        function inserirPerfil() {
            apiService.post('/api/perfil/inserirperfil', $scope.modelperfil,
                loadInserirPerfilSuccess,
                loadInserirPerfilFailed);
        }

        function loadInserirPerfilSuccess(result) {

            $scope.modelperfil = result.data.Items;

            notificationService.displaySuccess($translate.instant('SUCCESS_SAVE_ROLE'), $translate.instant('ATTENTION') + '!');
            limparCampos();
            carregarPerfil();

        }

        function loadInserirPerfilFailed(result) {
            notificationService.displayError($translate.instant('ERROR_INSERT_ROLE') + '. <br />' + $translate.instant('ERROR') + ': ' + result.status, $translate.instant('ATTENTION') + '!');
        }

        function pegaDadosPerfil(perfil) {
            $scope.modelperfil = perfil;
        }

        function atualizaPerfil() {
            apiService.post('/api/perfil/alterarperfil', $scope.modelperfil, loadAtualizaPerfilSuccess, loadAtualizaPerfilFailed);
        }

        function excluirPerfil(perfil) {
            if (confirm($translate.instant('CONFIRM_DELETE')))
                apiService.post('/api/perfil/excluirperfil', perfil, loadExcluirPerfilSuccess, loadExcluirPerfilFailed);
        }

        function loadAtualizaPerfilSuccess(result) {

            $scope.modelperfil = result.data.Items;

            notificationService.displaySuccess($translate.instant('SUCCESS_UPDATE_ROLE'), $translate.instant('ATTENTION') + '!');
            carregarPerfil();
        }

        function loadAtualizaPerfilFailed(result) {
            notificationService.displayError($translate.instant('ERROR_UPDATE_ROLE') + '. <br />' + $translate.instant('ERROR') + ': ' + result.status, $translate.instant('ATTENTION') + '!');
        }

        function loadExcluirPerfilSuccess(result) {

            $scope.modelperfil = result.data.Items;

            notificationService.displaySuccess($translate.instant('SUCCESS_DELETE_ROLE'), $translate.instant('ATTENTION') + '!');
            carregarPerfil();
        }

        function loadExcluirPerfilFailed(result) {

            notificationService.displayError($translate.instant('ERROR_DELETE_ROLE') + '. <br />' + $translate.instant('ERROR') + ': ' + result.status, $translate.instant('ATTENTION') + '!');
        }

        function limparCampos() {

            $scope.modelperfil = {};

        }

        carregarPerfil();
    }

})(angular.module('AppKor'));