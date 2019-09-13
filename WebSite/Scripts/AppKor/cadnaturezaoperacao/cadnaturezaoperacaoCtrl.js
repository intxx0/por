(function (app) {
    'use strict';

    app.controller('cadnaturezaoperacaoCtrl', cadnaturezaoperacaoCtrl);

    cadnaturezaoperacaoCtrl.$inject = ['$scope', 'apiService', 'notificationService'];

    function cadnaturezaoperacaoCtrl($scope, apiService, notificationService) {

        $scope.limparCampos = limparCampos;
        $scope.carregarNaturezaOperacao = carregarNaturezaOperacao;
        $scope.pegaDadosNaturezaOperacao = pegaDadosNaturezaOperacao;
        $scope.verificaInserirAtualizarNaturezaOperacao = verificaInserirAtualizarNaturezaOperacao;

        $scope.novoNaturezaOperacao = {};


        //1---------------Inserir Kor-------------------------------------

        function carregarNaturezaOperacao(page) {

            page = page || 0;

            var config = {
                params: {
                    page: page,
                    pageSize:10,
                    filter: $scope.filterNaturezaOperacao
                }
            };

            apiService.get('/api/naturezaoperacao/listanaturezaoperacao', config,
                loadNaturezaOperacaoSuccess,
                loadNaturezaOperacaoFailed);

        }

        function loadNaturezaOperacaoSuccess(result) {

            $scope.NaturezaOperacao = result.data.Items;

            $scope.page = result.data.Page;
            $scope.pagesCount = result.data.TotalPages;
            $scope.totalCount = result.data.TotalCount;

        }

        function loadNaturezaOperacaoFailed(result) {

            notificationService.displayError('Erro ao carregar natureza da operação. <br />' + 'Erro: ' + result.status, 'Atenção!');
        }

        //1---------------------------------------------------------------


        //2---------------Verifica Inserir ou Atualizar------------------
        function verificaInserirAtualizarNaturezaOperacao(novoNaturezaOperacao) {

            if (novoNaturezaOperacao) {

                atualizaNaturezaOperacao();

            } else {

                inserirNaturezaOperacao();
            }

        }

        //2--------------------------------------------------------------


        //3--------------Inserir Natureza da Operação---------------------

        function inserirNaturezaOperacao() {

            apiService.post('/api/naturezaoperacao/inserirnaturezaoperacao', $scope.novoNaturezaOperacao,
                loadInserirNaturezaOperacaoSuccess,
                loadInserirNaturezaOperacaoFailed);
        }

        function loadInserirNaturezaOperacaoSuccess(result) {

            $scope.novoNaturezaOperacao = result.data.Items;

            notificationService.displaySuccess('Natureza da operação inserida com sucesso', 'Atenção!');
            limparCampos();
            carregarNaturezaOperacao();

        }

        function loadInserirNaturezaOperacaoFailed(result) {

            notificationService.displayError('Erro ao inserir natureza da operacao. <br />' + 'Erro: ' + result.status, 'Atenção!');
        }

        //3--------------------------------------------------------------


        //4--------------Editar Natureza da Operação--------------------------------------

        function pegaDadosNaturezaOperacao(naturezaOperacao) {

            $scope.novoNaturezaOperacao = naturezaOperacao;
        }


        function atualizaNaturezaOperacao() {

            apiService.post('/api/naturezaoperacao/alterarnaturezaoperacao', $scope.novoNaturezaOperacao,
                loadAtualizaNaturezaOperacaoSuccess,
                loadAtualizaNaturezaOperacaoFailed);
        }

        function loadAtualizaNaturezaOperacaoSuccess(result) {

            $scope.novoNaturezaOperacao = result.data.Items;

            notificationService.displaySuccess('Natureza da operação atualizada com sucesso', 'Atenção!');
            carregarNaturezaOperacao();
        }

        function loadAtualizaNaturezaOperacaoFailed(result) {

            notificationService.displayError('Erro ao atualizar kor. <br />' + 'Erro: ' + result.status, 'Atenção!');
        }

        //4--------------------------------------------------------------


        //---------------------Limpa Campos---------------------------------
        function limparCampos() {

            $scope.novoNaturezaOperacao = {};

        }
        //------------------------------------------------------------------


        carregarNaturezaOperacao();
    }



})(angular.module('AppKor'));