(function (app) {
    'use strict';

    app.controller('detalhesOperacaoCtrl', detalhesOperacaoCtrl);

    detalhesOperacaoCtrl.$inject = ['$scope', '$http', '$routeParams', '$modal', 'apiService', 'notificationService'];

    function detalhesOperacaoCtrl($scope, $http, $routeParams, $modal, apiService, notificationService) {
     
        $scope.openEditDialog = openEditDialog;


        $scope.FiscalizacoesOnLine = [];


        //1-----------------Carrega lista de Fiscalizações On-Line-----------------------------
       
        function carregarFiscalizacoesOnLine() {
            
            var config = $routeParams.id;

            apiService.get('/api/detalhesfiscalizacao/listadetalhesfiscalizacaoonline/' + config, null,
                loadFiscalizacaoOnLineSuccess,
                loadFiscalizacaoOnLineFailed);

        }

        function loadFiscalizacaoOnLineSuccess(result) {
            
            $scope.FiscalizacoesOnLine = result.data.Items;

            notificationService.displayInfo("(" + $scope.FiscalizacoesOnLine.length + ")" + " operações em andamento!", "Atenção<br /><br />");


        }

        function loadFiscalizacaoOnLineFailed(result) {

            notificationService.displayError("Erro ao carregar operações!", "Atenção !\n");
            
        }
        //1-------------------------Fim -------------------------------------------------------



        function openEditDialog(fiscalizacao) {

            $modal.open({
                animation: true,
                templateUrl: 'scripts/AppKor/operacoes/modaloperacao.html',
                controller: 'detalhesModalCtrl',
                size: 'lg',
                resolve: {
                    items: function () {
                        return fiscalizacao.NumeroGuia;
                    }
                }

            });

        }



        carregarFiscalizacoesOnLine();

    }

})(angular.module('AppKor'));