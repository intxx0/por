(function (app) {
    'use strict';

    app.controller('cadtipoguiaCtrl', cadtipoguiaCtrl);

    cadtipoguiaCtrl.$inject = ['$scope', 'apiService', 'notificationService', '$translate'];

    function cadtipoguiaCtrl($scope, apiService, notificationService, $translate) {

        $scope.limparCampos = limparCampos;
        $scope.pegaDadosTipoGuia = pegaDadosTipoGuia;
        $scope.verificaInserirAtualizarTipoGuia = verificaInserirAtualizarTipoGuia;
        $scope.excluirTipoGuia = excluirTipoGuia;

        $scope.modeltipoguia = {};

        function carregarTipoGuia(page) {

            page = page || 0;

            var config = {
                params: {
                    page: page,
                    pageSize: 10,
                    filter: $scope.filtertipoguia
                }
            };

            apiService.get('/api/tipoguia/listatipoguia', config,
                loadTipoGuiaSucess,
                loadTipoGuiaFailed);

        }

        function loadTipoGuiaSucess(result) {

            $scope.Missoes = result.data.Items;

            $scope.page = result.data.Page;
            $scope.pagesCount = result.data.TotalPages;
            $scope.totalCount = result.data.TotalCount;

        }

        function loadTipoGuiaFailed(result) {

            notificationService.displayError($translate.instant('ERROR_LOAD_ROLES') + '. <br />' + $translate.instant('ERROR') + ': ' + result.status, $translate.instant('ATTENTION') + '!');
        }

        function verificaInserirAtualizarTipoGuia(novoTipoGuia) {

            if (novoTipoGuia) {
                atualizaTipoGuia();
            } else {
                inserirTipoGuia();
            }

        }

        function inserirTipoGuia() {
            apiService.post('/api/tipoguia/inserirtipoguia', $scope.modeltipoguia,
                loadInserirTipoGuiaSuccess,
                loadInserirTipoGuiaFailed);
        }

        function loadInserirTipoGuiaSuccess(result) {

            $scope.modeltipoguia = result.data.Items;

            notificationService.displaySuccess($translate.instant('SUCCESS_SAVE_ROLE'), $translate.instant('ATTENTION') + '!');
            limparCampos();
            carregarTipoGuia();

        }

        function loadInserirTipoGuiaFailed(result) {
            notificationService.displayError($translate.instant('ERROR_INSERT_ROLE') + '. <br />' + $translate.instant('ERROR') + ': ' + result.status, $translate.instant('ATTENTION') + '!');
        }

        function pegaDadosTipoGuia(tipoguia) {
            $scope.modeltipoguia = tipoguia;
        }

        function atualizaTipoGuia() {
            apiService.post('/api/tipoguia/alterartipoguia', $scope.modeltipoguia, loadAtualizaTipoGuiaSuccess, loadAtualizaTipoGuiaFailed);
        }

        function excluirTipoGuia(tipoguia) {
            if (confirm($translate.instant('CONFIRM_DELETE')))
                apiService.post('/api/tipoguia/excluirtipoguia', tipoguia, loadExcluirTipoGuiaSuccess, loadExcluirTipoGuiaFailed);
        }

        function loadAtualizaTipoGuiaSuccess(result) {

            $scope.modeltipoguia = result.data.Items;

            notificationService.displaySuccess($translate.instant('SUCCESS_UPDATE_ROLE'), $translate.instant('ATTENTION') + '!');
            carregarTipoGuia();
        }

        function loadAtualizaTipoGuiaFailed(result) {
            notificationService.displayError($translate.instant('ERROR_UPDATE_ROLE') + '. <br />' + $translate.instant('ERROR') + ': ' + result.status, $translate.instant('ATTENTION') + '!');
        }

        function loadExcluirTipoGuiaSuccess(result) {

            $scope.modeltipoguia = result.data.Items;

            notificationService.displaySuccess($translate.instant('SUCCESS_DELETE_ROLE'), $translate.instant('ATTENTION') + '!');
            carregarTipoGuia();
        }

        function loadExcluirTipoGuiaFailed(result) {

            notificationService.displayError($translate.instant('ERROR_DELETE_ROLE') + '. <br />' + $translate.instant('ERROR') + ': ' + result.status, $translate.instant('ATTENTION') + '!');
        }

        function limparCampos() {

            $scope.modeltipoguia = {};

        }

        carregarTipoGuia();
    }

})(angular.module('AppKor'));