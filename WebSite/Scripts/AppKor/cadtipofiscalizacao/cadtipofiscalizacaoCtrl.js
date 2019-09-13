(function (app) {
    'use strict';

    app.controller('cadtipofiscalizacaoCtrl', cadtipofiscalizacaoCtrl);

    cadtipofiscalizacaoCtrl.$inject = ['$scope', 'apiService', 'notificationService', '$translate'];

    function cadtipofiscalizacaoCtrl($scope, apiService, notificationService, $translate) {

        $scope.carregarTipoFiscalizacao = carregarTipoFiscalizacao;
        $scope.preencheCamposAlteracao = preencheCamposAlteracao;
        $scope.verificaIdTipoFiscalizacao = verificaIdTipoFiscalizacao;
        $scope.limparCampos = limparCampos;
        $scope.excluirTipoFiscalizacao = excluirTipoFiscalizacao;

        $scope.novoTipoFiscalizacao = {};
        $scope.FiscalizacoesOnLine = [];

        $scope.NaturezasOperacao = [];

        //1----------------Verifica o da Fiscalizacao On-Line-------------------------------
        function verificaIdTipoFiscalizacao(id) {


            if (id == undefined) {


                inserirTipoFiscalizacao();

            }
            else {

                alterarTipoFiscalizacao();
            }

        }
        //1--------------------------------------------------------------------------------


        //2----------------Inserir Fiscalizacao On-Line------------------------------------

        function inserirTipoFiscalizacao() {

         
            apiService.post('/api/tipofiscalizacao/inserirtipofiscalizacao', $scope.novoTipoFiscalizacao,
                loadInserirTipoFiscalizacaoSuccess,
                loadInserirTipoFiscalizacaoFailed);
        }

        function loadInserirTipoFiscalizacaoSuccess(result) {
            
         
            notificationService.displaySuccess($translate.instant('SUCCESS_SAVE_INSPECT_TYPE'), $translate.instant('ATTENTION') + '!');
            carregarTipoFiscalizacao();
            $scope.novoTipoFiscalizacao = {};

        }

        function loadInserirTipoFiscalizacaoFailed(result) {
            notificationService.displayError($translate.instant('ERROR_INSERT_INSPECT_TYPE') + '. <br />' + $translate.instant('ERROR') + ': ' + result.status, $translate.instant('ATTENTION') + '!');
        }

        //2---------------------------------------------------------------------------------


        //3----------------Alterar Fiscalizacao On-Line------------------------------------

        function preencheCamposAlteracao(tipofiscalizacao) {

            $scope.novoTipoFiscalizacao = tipofiscalizacao;

        }

        function alterarTipoFiscalizacao() {

            apiService.post('/api/tipofiscalizacao/alterartipofiscalizacao', $scope.novoTipoFiscalizacao,
                loadAtualizarTipoFiscalizacaoSuccess,
                loadAtualizarTipoFiscalizacaoFailed);

        }

        function loadAtualizarTipoFiscalizacaoSuccess(result) {

            $scope.novoTipoFiscalizacao = result.data.Items;
            
            notificationService.displaySuccess($translate.instant('SUCCESS_UPDATE_INSPECT_TYPE'), $translate.instant('ATTENTION') + '!');
        }

        function loadAtualizarTipoFiscalizacaoFailed(result) {
            notificationService.displayError($translate.instant('ERROR_UPDATE_INSPECT_TYPE') + '. <br />' + $translate.instant('ERROR') + ': ' + result.status, $translate.instant('ATTENTION') + '!');
        }
        //3---------------------------------------------------------------------------------


        //4----------------Limpar Campos Fiscalizacao On-Line-------------------------------
        function limparCampos() {
            $scope.novoTipoFiscalizacao = {};

        }
        //4---------------------------------------------------------------------------------

        
        //5-------------------Carregar Tipo Fiscalização------------------------------
        function carregarTipoFiscalizacao(page) {

            page = page || 0;

            var config = {
                params: {
                    page: page,
                    pageSize:10,
                    filter: $scope.filtroTipoFiscalizacao
                }
            };

            apiService.get('/api/tipofiscalizacao/listatipofiscalizacao', config, loadTipoFiscalizacaoSuccess, loadTipoFiscalizacaoFailed);
        }

        function loadTipoFiscalizacaoSuccess(result) {

            $scope.TipoFiscalizacoes = result.data.Items;

            
            $scope.page = result.data.Page;
            $scope.pagesCount = result.data.TotalPages;
            $scope.totalCount = result.data.TotalCount;
        

        }

        function loadTipoFiscalizacaoFailed(result) {

            notificationService.displayError($translate.instant('ERROR_LOAD_INSPECT_TYPE') + '. <br />' + $translate.instant('ERROR') + ': ' + result.status, $translate.instant('ATTENTION') + '!');
        }
        //5-------------------------------------------------------------------

        function excluirTipoFiscalizacao(tipoFiscalizacao) {
            if (confirm($translate.instant('CONFIRM_DELETE')))
                apiService.post('/api/tipofiscalizacao/excluirtipofiscalizacao', tipoFiscalizacao, loadExcluirTipoFiscalizacaoSuccess, loadExcluirTipoFiscalizacaoFailed);
        }

        function loadExcluirTipoFiscalizacaoSuccess(result) {

            $scope.novoTipoFiscalizacao = result.data.Items;

            notificationService.displaySuccess($translate.instant('SUCCESS_DELETE_INSPECT_TYPE'), $translate.instant('ATTENTION') + '!');
            carregarTipoFiscalizacao();
        }

        function loadExcluirTipoFiscalizacaoFailed(result) {
            notificationService.displayError($translate.instant('ERROR_DELETE_INSPECT_TYPE') + '. <br />' + $translate.instant('ERROR') + ': ' + result.status, $translate.instant('ATTENTION') + '!');
        }

        function carregaNaturezasOperacao() {
            apiService.get('/api/naturezaoperacao/listanaturezaoperacao', null, loadNaturezasOperacaoSuccess, loadNaturezasOperacaoFailed);
        }

        function loadNaturezasOperacaoSuccess(result) {

            var newItem = new function () {
                this.IdNaturezaOperacao = undefined;
                this.DescnaturezaOperacao = $translate.instant('SELECT') + '...';

            };

            result.data.push(newItem);
            $scope.NaturezasOperacao = result.data;
        }

        function loadNaturezasOperacaoFailed(result) {
            $scope.NaturezasOperacao = result.data;
        }

        carregarTipoFiscalizacao();
        carregaNaturezasOperacao();

    }
})(angular.module('AppKor'));