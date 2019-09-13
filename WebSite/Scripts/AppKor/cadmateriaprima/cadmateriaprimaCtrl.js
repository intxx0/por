(function (app) {
    'use strict';

    app.controller('cadmateriaprimaCtrl', cadmateriaprimaCtrl);

    cadmateriaprimaCtrl.$inject = ['$scope', 'apiService', 'notificationService', '$translate'];

    function cadmateriaprimaCtrl($scope, apiService, notificationService, $translate) {

        $scope.limparCampos = limparCampos;
        $scope.pegaDadosMateriaPrima = pegaDadosMateriaPrima;
        $scope.verificaInserirAtualizarMateriaPrima = verificaInserirAtualizarMateriaPrima;
        $scope.excluirMateriaPrima = excluirMateriaPrima;

        $scope.modelmateriaprima = {};

        $scope.Ativo = [{ tipo: $translate.instant('INACTIVE'), value: "0" }, { tipo: $translate.instant('ACTIVE'), value: "1" }];

        function carregarMateriaPrima(page) {

            page = page || 0;

            var config = {
                params: {
                    page: page,
                    pageSize: 10,
                    filter: $scope.filtermateriaprima
                }
            };

            apiService.get('/api/materiaprima/listamateriaprima', config,
                loadMateriaPrimaSucess,
                loadMateriaPrimaFailed);

        }

        function loadMateriaPrimaSucess(result) {

            $scope.Missoes = result.data.Items;

            $scope.page = result.data.Page;
            $scope.pagesCount = result.data.TotalPages;
            $scope.totalCount = result.data.TotalCount;

        }

        function loadMateriaPrimaFailed(result) {

            notificationService.displayError($translate.instant('ERROR_LOAD_FEEDSTOCKS') + '. <br />' + $translate.instant('ERROR') + ': ' + result.status, $translate.instant('ATTENTION') + '!');
        }

        function verificaInserirAtualizarMateriaPrima(novoMateriaPrima) {

            if (novoMateriaPrima) {
                atualizaMateriaPrima();
            } else {
                inserirMateriaPrima();
            }

        }

        function inserirMateriaPrima() {
            apiService.post('/api/materiaprima/inserirmateriaprima', $scope.modelmateriaprima,
                loadInserirMateriaPrimaSuccess,
                loadInserirMateriaPrimaFailed);
        }

        function loadInserirMateriaPrimaSuccess(result) {

            $scope.modelmateriaprima = result.data.Items;

            notificationService.displaySuccess($translate.instant('SUCCESS_SAVE_FEEDSTOCK'), $translate.instant('ATTENTION') + '!');
            limparCampos();
            carregarMateriaPrima();

        }

        function loadInserirMateriaPrimaFailed(result) {
            notificationService.displayError($translate.instant('ERROR_INSERT_FEEDSTOCK') + '. <br />' + $translate.instant('ERROR') + ': ' + result.status, $translate.instant('ATTENTION') + '!');
        }

        function pegaDadosMateriaPrima(materiaprima) {
            $scope.modelmateriaprima = materiaprima;
        }

        function atualizaMateriaPrima() {
            apiService.post('/api/materiaprima/alterarmateriaprima', $scope.modelmateriaprima, loadAtualizaMateriaPrimaSuccess, loadAtualizaMateriaPrimaFailed);
        }

        function excluirMateriaPrima(materiaprima) {
            if (confirm($translate.instant('CONFIRM_DELETE')))
                apiService.post('/api/materiaprima/excluirmateriaprima', materiaprima, loadExcluirMateriaPrimaSuccess, loadExcluirMateriaPrimaFailed);
        }

        function loadAtualizaMateriaPrimaSuccess(result) {

            $scope.modelmateriaprima = result.data.Items;

            notificationService.displaySuccess($translate.instant('SUCCESS_UPDATE_FEEDSTOCK'), $translate.instant('ATTENTION') + '!');
            carregarMateriaPrima();
        }

        function loadAtualizaMateriaPrimaFailed(result) {
            notificationService.displayError($translate.instant('ERROR_UPDATE_FEEDSTOCK') + '. <br />' + $translate.instant('ERROR') + ': ' + result.status, $translate.instant('ATTENTION') + '!');
        }

        function loadExcluirMateriaPrimaSuccess(result) {

            $scope.modelmateriaprima = result.data.Items;

            notificationService.displaySuccess($translate.instant('SUCCESS_DELETE_FEEDSTOCK'), $translate.instant('ATTENTION') + '!');
            carregarMateriaPrima();
        }

        function loadExcluirMateriaPrimaFailed(result) {

            notificationService.displayError($translate.instant('ERROR_DELETE_FEEDSTOCK') + '. <br />' + $translate.instant('ERROR') + ': ' + result.status, $translate.instant('ATTENTION') + '!');
        }

        function limparCampos() {

            $scope.modelmateriaprima = {};

        }

        carregarMateriaPrima();
    }

})(angular.module('AppKor'));