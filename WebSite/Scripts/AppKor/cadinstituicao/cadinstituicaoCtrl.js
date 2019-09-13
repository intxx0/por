(function (app) {
    'use strict';

    app.controller('cadinstituicaoCtrl', cadinstituicaoCtrl);

    cadinstituicaoCtrl.$inject = ['$scope', 'apiService', 'notificationService', '$translate'];

    function cadinstituicaoCtrl($scope, apiService, notificationService, $translate) {

        $scope.limparCampos = limparCampos;
        $scope.pegaDadosInstituicao = pegaDadosInstituicao;
        $scope.verificaInserirAtualizarInstituicao = verificaInserirAtualizarInstituicao;
        $scope.excluirInstituicao = excluirInstituicao;

        $scope.modelinstituicao = {};

        function carregarInstituicao(page) {

            page = page || 0;

            var config = {
                params: {
                    page: page,
                    pageSize: 10,
                    filter: $scope.filterinstituicao
                }
            };

            apiService.get('/api/instituicao/listainstituicao', config,
                loadInstituicaoSucess,
                loadInstituicaoFailed);

        }

        function loadInstituicaoSucess(result) {

            $scope.Missoes = result.data.Items;

            $scope.page = result.data.Page;
            $scope.pagesCount = result.data.TotalPages;
            $scope.totalCount = result.data.TotalCount;

        }

        function loadInstituicaoFailed(result) {

            notificationService.displayError($translate.instant('ERROR_LOAD_ROLES') + '. <br />' + $translate.instant('ERROR') + ': ' + result.status, $translate.instant('ATTENTION') + '!');
        }

        function verificaInserirAtualizarInstituicao(novoInstituicao) {

            if (novoInstituicao) {
                atualizaInstituicao();
            } else {
                inserirInstituicao();
            }

        }

        function inserirInstituicao() {
            apiService.post('/api/instituicao/inseririnstituicao', $scope.modelinstituicao,
                loadInserirInstituicaoSuccess,
                loadInserirInstituicaoFailed);
        }

        function loadInserirInstituicaoSuccess(result) {

            $scope.modelinstituicao = result.data.Items;

            notificationService.displaySuccess($translate.instant('SUCCESS_SAVE_ROLE'), $translate.instant('ATTENTION') + '!');
            limparCampos();
            carregarInstituicao();

        }

        function loadInserirInstituicaoFailed(result) {
            notificationService.displayError($translate.instant('ERROR_INSERT_ROLE') + '. <br />' + $translate.instant('ERROR') + ': ' + result.status, $translate.instant('ATTENTION') + '!');
        }

        function pegaDadosInstituicao(instituicao) {
            $scope.modelinstituicao = instituicao;
        }

        function atualizaInstituicao() {
            apiService.post('/api/instituicao/alterarinstituicao', $scope.modelinstituicao, loadAtualizaInstituicaoSuccess, loadAtualizaInstituicaoFailed);
        }

        function excluirInstituicao(instituicao) {
            if (confirm($translate.instant('CONFIRM_DELETE')))
                apiService.post('/api/instituicao/excluirinstituicao', instituicao, loadExcluirInstituicaoSuccess, loadExcluirInstituicaoFailed);
        }

        function loadAtualizaInstituicaoSuccess(result) {

            $scope.modelinstituicao = result.data.Items;

            notificationService.displaySuccess($translate.instant('SUCCESS_UPDATE_ROLE'), $translate.instant('ATTENTION') + '!');
            carregarInstituicao();
        }

        function loadAtualizaInstituicaoFailed(result) {
            notificationService.displayError($translate.instant('ERROR_UPDATE_ROLE') + '. <br />' + $translate.instant('ERROR') + ': ' + result.status, $translate.instant('ATTENTION') + '!');
        }

        function loadExcluirInstituicaoSuccess(result) {

            $scope.modelinstituicao = result.data.Items;

            notificationService.displaySuccess($translate.instant('SUCCESS_DELETE_ROLE'), $translate.instant('ATTENTION') + '!');
            carregarInstituicao();
        }

        function loadExcluirInstituicaoFailed(result) {

            notificationService.displayError($translate.instant('ERROR_DELETE_ROLE') + '. <br />' + $translate.instant('ERROR') + ': ' + result.status, $translate.instant('ATTENTION') + '!');
        }

        function limparCampos() {

            $scope.modelinstituicao = {};

        }

        carregarInstituicao();
    }

})(angular.module('AppKor'));