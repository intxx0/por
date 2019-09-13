(function (app) {
    'use strict';

    app.controller('naturezaoperacaoCtrl', naturezaoperacaoCtrl);

    naturezaoperacaoCtrl.$inject = ['$location', '$scope', 'apiService', 'notificationService'];

    function naturezaoperacaoCtrl($location, $scope, apiService, notificationService) {

        $scope.listaNaturezaOperacao = [];
        $scope.carregaListaNaturezaOperacao = carregaListaNaturezaOperacao;
        $scope.exibirOperacoes = exibirOperacoes;

        //1------------Carrega Natureza Operações------------------------
        function carregaListaNaturezaOperacao() {

            apiService.get('/api/naturezaoperacao/listanaturezaoperacao', null,
                loadListaNaturezaOperacaoSuccess,
                loadListaNaturezaOperacaoFailed);

        }

        function loadListaNaturezaOperacaoSuccess(result) {

            $scope.listaNaturezaOperacao = result.data.Items;
          
        }

        function loadListaNaturezaOperacaoFailed(result) {

            notificationService.displayError('Erro carregar natureza das operações');

        }

        function exibirOperacoes(idNaturezaOperacao) {

            $location.path('/operacoes/' + idNaturezaOperacao);

        }
        //1----------------------------------------------------------------
       
        carregaListaNaturezaOperacao();

    }
})(angular.module('AppKor'));
