(function (app) {
    'use strict';

    app.controller('operacaoCtrl', operacaoCtrl);

    operacaoCtrl.$inject = ['$scope', '$http', 'apiService', 'notificationService', '$routeParams', '$modal', '$location'];

    function operacaoCtrl($scope, $http, apiService, notificationService, $routeParams, $modal, $location) {


        $scope.Operacoes = [];


        $scope.carregarOperacoesOnLine = carregarOperacoesOnLine;
        $scope.openEditDialog = openEditDialog;
        $scope.openReports = openReports;

        //1-----------------Carrega lista de Fiscalizações On-Line-----------------------------

        function carregarOperacoesOnLine(page) {


            page = page || 0;

            var config = {
                params: {
                    page: page,
                    pageSize: 200,
                    filter: $routeParams.id
                }
            };

            apiService.get('/api/fiscalizacaoonline/listafiscalizacaoonline', config,
                loadFiscalizacaoOnLineSuccess,
                loadFiscalizacaoOnLineError);

        }

        function loadFiscalizacaoOnLineSuccess(result) {


            $scope.Operacoes = result.data;


            for (var i = 0; i < $scope.Operacoes.length - 1; i++) {

                $scope.DescNatureza = $scope.Operacoes[i].DescNaturezaOperacao;

                i = $scope.Operacoes.length;
            }


            for (var j = 0; j < $scope.Operacoes.length; j++) {

                carregarFiscalizacoes($scope.Operacoes[j].IdFiscalizacao);
               
            }


            notificationService.displayInfo("(" + $scope.Operacoes.length + ")" + " operações em andamento!", "Atenção<br /><br />");

            $scope.page = result.data.Page;
            $scope.pagesCount = result.data.TotalPages;
            $scope.totalCount = result.data.TotalCount;
        }

        function loadFiscalizacaoOnLineError(result) {

            notificationService.displayError("Erro ao carregar operações!", "Atenção !\n");

        }

        //1-------------------------Fim -------------------------------------------------------


        //2-----------------Carrega Tipo Fiscalização-----------------------------------------

        function carregarFiscalizacoes(id) {

            var config = id;

            apiService.get('/api/detalhesfiscalizacao/listadetalhesfiscalizacaoonline/' + config, null,
                loadFiscalizacaoSuccess,
                loadFiscalizacaoFailed);

        }

        function loadFiscalizacaoSuccess(result) {

            $scope.Fiscalizacoes = result.data.Items;

            //notificationService.displayInfo("(" + $scope.Fiscalizacoes.length + ")" + " operações em andamento!", "Atenção<br /><br />");


        }

        function loadFiscalizacaoFailed(result) {

            notificationService.displayError("Erro ao carregar fiscalizações!", "Atenção !\n");

        }
        //2------------------------------Fim--------------------------------------------------


        //3--------------------Abre os detalhes da guia----------------------------------------

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

        function openReports(idFiscalizacao) {
            $location.path('/dashboard/' + idFiscalizacao);
        }

        //3-----------------------------------------------------------------------------------



        //Carregando todas as fiscalizacoes de tempos em tempos
        carregarOperacoesOnLine();

      

    }

})(angular.module('AppKor'));