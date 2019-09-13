(function (app) {
    'use strict';

    app.controller('cadmissaoCtrl', cadmissaoCtrl);

    cadmissaoCtrl.$inject = ['$scope', 'apiService', 'notificationService', '$translate'];

    function cadmissaoCtrl($scope, apiService, notificationService, $translate) {

        $scope.limparCampos = limparCampos;
        $scope.pegaDadosMissao = pegaDadosMissao;
        $scope.verificaInserirAtualizarMissao = verificaInserirAtualizarMissao;
        $scope.excluirMissao = excluirMissao;

        $scope.modelmissao = {};

        function carregarMissao(page) {

            page = page || 0;

            var config = {
                params: {
                    page: page,
                    pageSize: 10,
                    filter: $scope.filtermissao
                }
            };

            apiService.get('/api/missao/listamissao', config,
                loadMissaoSucess,
                loadMissaoFailed);

        }

        function loadMissaoSucess(result) {

            $scope.Missoes = result.data.Items;

            $scope.page = result.data.Page;
            $scope.pagesCount = result.data.TotalPages;
            $scope.totalCount = result.data.TotalCount;

        }

        function loadMissaoFailed(result) {

            notificationService.displayError($translate.instant('ERROR_LOAD_MISSIONS') + '. <br />' + $translate.instant('ERROR') + ': ' + result.status, $translate.instant('ATTENTION') + '!');
        }

        function verificaInserirAtualizarMissao(novoMissao) {

            if (novoMissao) {
                atualizaMissao();
            } else {
                inserirMissao();
            }

        }

        function inserirMissao() {
            apiService.post('/api/missao/inserirmissao', $scope.modelmissao,
                loadInserirMissaoSuccess,
                loadInserirMissaoFailed);
        }

        function loadInserirMissaoSuccess(result) {

            $scope.modelmissao = result.data.Items;

            notificationService.displaySuccess($translate.instant('SUCCESS_SAVE_MISSION'), $translate.instant('ATTENTION') + '!');
            limparCampos();
            carregarMissao();

        }

        function loadInserirMissaoFailed(result) {
            notificationService.displayError($translate.instant('ERROR_INSERT_MISSION') + '. <br />' + $translate.instant('ERROR') + ': ' + result.status, $translate.instant('ATTENTION') + '!');
        }

        function pegaDadosMissao(missao) {
            $scope.modelmissao = missao;
        }

        function atualizaMissao() {
            apiService.post('/api/missao/alterarmissao', $scope.modelmissao, loadAtualizaMissaoSuccess, loadAtualizaMissaoFailed);
        }

        function excluirMissao(missao) {
            if (confirm($translate.instant('CONFIRM_DELETE')))
                apiService.post('/api/missao/excluirmissao', missao, loadExcluirMissaoSuccess, loadExcluirMissaoFailed);
        }

        function loadAtualizaMissaoSuccess(result) {

            $scope.modelmissao = result.data.Items;

            notificationService.displaySuccess($translate.instant('SUCCESS_UPDATE_MISSION'), $translate.instant('ATTENTION') + '!');
            carregarMissao();
        }

        function loadAtualizaMissaoFailed(result) {
            notificationService.displayError($translate.instant('ERROR_UPDATE_MISSION') + '. <br />' + $translate.instant('ERROR') + ': ' + result.status, $translate.instant('ATTENTION') + '!');
        }

        function loadExcluirMissaoSuccess(result) {

            $scope.modelmissao = result.data.Items;

            notificationService.displaySuccess($translate.instant('SUCCESS_DELETE_MISSION'), $translate.instant('ATTENTION') + '!');
            carregarMissao();
        }

        function loadExcluirMissaoFailed(result) {

            notificationService.displayError($translate.instant('ERROR_DELETE_MISSION') + '. <br />' + $translate.instant('ERROR') + ': ' + result.status, $translate.instant('ATTENTION') + '!');
        }

        function limparCampos() {

            $scope.modelmissao = {};

        }

        carregarMissao();
    }

})(angular.module('AppKor'));