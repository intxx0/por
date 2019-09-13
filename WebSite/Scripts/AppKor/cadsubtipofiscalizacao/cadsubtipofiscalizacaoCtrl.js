(function (app) {
    'use strict';

    app.controller('cadsubtipofiscalizacaoCtrl', cadsubtipofiscalizacaoCtrl);

    cadsubtipofiscalizacaoCtrl.$inject = ['$scope', 'apiService', 'notificationService', '$translate'];

    function cadsubtipofiscalizacaoCtrl($scope, apiService, notificationService, $translate) {

        $scope.limparCampos = limparCampos;
        $scope.pegaDadosSubTipoFiscalizacao = pegaDadosSubTipoFiscalizacao;
        $scope.verificaInserirAtualizarSubTipoFiscalizacao = verificaInserirAtualizarSubTipoFiscalizacao;
        $scope.excluirSubTipoFiscalizacao = excluirSubTipoFiscalizacao;

        $scope.TiposFiscalizacao = [];

        $scope.modelsubtipofiscalizacao = {};

        function carregarSubTipoFiscalizacao(page) {

            page = page || 0;

            var config = {
                params: {
                    page: page,
                    pageSize: 10,
                    filter: $scope.filtersubtipofiscalizacao
                }
            };

            apiService.get('/api/subtipofiscalizacao/listasubtipofiscalizacao', config,
                loadSubTipoFiscalizacaoSucess,
                loadSubTipoFiscalizacaoFailed);

        }

        function carregaTiposFiscalizacao() {
            apiService.get('/api/tipofiscalizacao/listatipofiscalizacao', null, loadTiposFiscalizacaoSuccess, loadTiposFiscalizacaoFailed);
        }

        function loadTiposFiscalizacaoSuccess(result) {

            var newItem = new function () {
                this.IdTipoFiscalizacao = undefined;
                this.DescTipoFiscalizacao = $translate.instant('SELECT') + '...';
            };
            result.data.Items.push(newItem);
            $scope.TiposFiscalizacao = result.data.Items;

        }

        function loadTiposFiscalizacaoFailed(result) {
            $scope.TiposFiscalizacao = result.data;
        }

        function loadSubTipoFiscalizacaoSucess(result) {

            $scope.Missoes = result.data.Items;

            $scope.page = result.data.Page;
            $scope.pagesCount = result.data.TotalPages;
            $scope.totalCount = result.data.TotalCount;

        }

        function loadSubTipoFiscalizacaoFailed(result) {

            notificationService.displayError($translate.instant('ERROR_LOAD_INSPECTION_SUBTYPES') + '. <br />' + $translate.instant('ERROR') + ': ' + result.status, $translate.instant('ATTENTION') + '!');
        }

        function verificaInserirAtualizarSubTipoFiscalizacao(novoSubTipoFiscalizacao) {

            if (novoSubTipoFiscalizacao) {
                atualizaSubTipoFiscalizacao();
            } else {
                inserirSubTipoFiscalizacao();
            }

        }

        function inserirSubTipoFiscalizacao() {
            apiService.post('/api/subtipofiscalizacao/inserirsubtipofiscalizacao', $scope.modelsubtipofiscalizacao,
                loadInserirSubTipoFiscalizacaoSuccess,
                loadInserirSubTipoFiscalizacaoFailed);
        }

        function loadInserirSubTipoFiscalizacaoSuccess(result) {

            $scope.modelsubtipofiscalizacao = result.data.Items;

            notificationService.displaySuccess($translate.instant('SUCCESS_SAVE_INSPECTION_SUBTYPE'), $translate.instant('ATTENTION') + '!');
            limparCampos();
            carregarSubTipoFiscalizacao();

        }

        function loadInserirSubTipoFiscalizacaoFailed(result) {
            notificationService.displayError($translate.instant('ERROR_INSERT_INSPECTION_SUBTYPE') + '. <br />' + $translate.instant('ERROR') + ': ' + result.status, $translate.instant('ATTENTION') + '!');
        }

        function pegaDadosSubTipoFiscalizacao(subtipofiscalizacao) {
            $scope.modelsubtipofiscalizacao = subtipofiscalizacao;
        }

        function atualizaSubTipoFiscalizacao() {
            apiService.post('/api/subtipofiscalizacao/alterarsubtipofiscalizacao', $scope.modelsubtipofiscalizacao, loadAtualizaSubTipoFiscalizacaoSuccess, loadAtualizaSubTipoFiscalizacaoFailed);
        }

        function excluirSubTipoFiscalizacao(subtipofiscalizacao) {
            if (confirm($translate.instant('CONFIRM_DELETE')))
                apiService.post('/api/subtipofiscalizacao/excluirsubtipofiscalizacao', subtipofiscalizacao, loadExcluirSubTipoFiscalizacaoSuccess, loadExcluirSubTipoFiscalizacaoFailed);
        }

        function loadAtualizaSubTipoFiscalizacaoSuccess(result) {

            $scope.modelsubtipofiscalizacao = result.data.Items;

            notificationService.displaySuccess($translate.instant('SUCCESS_UPDATE_INSPECTION_SUBTYPE'), $translate.instant('ATTENTION') + '!');
            carregarSubTipoFiscalizacao();
        }

        function loadAtualizaSubTipoFiscalizacaoFailed(result) {
            notificationService.displayError($translate.instant('ERROR_UPDATE_INSPECTION_SUBTYPE') + '. <br />' + $translate.instant('ERROR') + ': ' + result.status, $translate.instant('ATTENTION') + '!');
        }

        function loadExcluirSubTipoFiscalizacaoSuccess(result) {

            $scope.modelsubtipofiscalizacao = result.data.Items;

            notificationService.displaySuccess($translate.instant('SUCCESS_DELETE_INSPECTION_SUBTYPE'), $translate.instant('ATTENTION') + '!');
            carregarSubTipoFiscalizacao();
        }

        function loadExcluirSubTipoFiscalizacaoFailed(result) {

            notificationService.displayError($translate.instant('ERROR_DELETE_INSPECTION_SUBTYPE') + '. <br />' + $translate.instant('ERROR') + ': ' + result.status, $translate.instant('ATTENTION') + '!');
        }

        function limparCampos() {

            $scope.modelsubtipofiscalizacao = {};

        }

        carregaTiposFiscalizacao();
        carregarSubTipoFiscalizacao();
    }

})(angular.module('AppKor'));