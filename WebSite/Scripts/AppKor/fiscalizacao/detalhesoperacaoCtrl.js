(function (app) {
    'use strict';

    app.controller('detalhesoperacaoCtrl', detalhesoperacaoCtrl);
    detalhesoperacaoCtrl.$inject = ['$scope', '$rootScope', 'apiService', 'notificationService', '$translate', '$modal'];

    function detalhesoperacaoCtrl($scope, $rootScope, apiService, notificationService, $translate, $modal) {

        $scope.operacao = {};
        $scope.fiscalizacoes = [];
        $scope.kors = [];
        $scope.UsuariosOperacao = [];
        $scope.TotalFiscalizacoes = 0;

        $scope.loadFiscalizacao = loadFiscalizacao;
        $scope.loadFichaDigital = loadFichaDigital;
        $scope.loadMap = loadMap;

        var idOperacao = 1;

        apiService.get('/api/operacoes/getdetalhesoperacao/' + idOperacao, null,
            getOperacaoSuccess,
            getOperacaoFailed);


        function getOperacaoSuccess(result) {
            $scope.operacao = result.data.operacao;
            $scope.fiscalizacoes = result.data.fiscalizacoes;
            $scope.kors = result.data.kor;
            $scope.UsuariosOperacao = result.data.usuarios;
            $scope.TotalFiscalizacoes = result.data.fiscalizacoes.length;

            console.log(result.data);
        }
        function getOperacaoFailed(result) {
            console.log(result.data);
        }

        //second chart of infractions by feedstocktype 
        apiService.get('/api/dashboard/getmateriaprimaautuadaByOp?Lista=' + $rootScope.FiltroOperacao, null,
            getInfractionDataSuccess,
            getInfractionDataFailed);

        $scope.InfractionFeedStockseries = [['Quantidade']];

        function getInfractionDataSuccess(result) {

            var Mps = new Array();
            var Qtd = new Array();

            forEach(result.data[0], function (value, key) {
                if (value != null)
                    Mps.push(value);
            });
            forEach(result.data[1], function (value, key) {
                if (value != null)
                    Qtd.push(value);
            });

            $scope.InfractionFeedStocklabels = Mps;
            $scope.InfractionFeedStockdata = [Qtd];
        }

        function getInfractionDataFailed(result) {

            console.log(result.data);
        }


        //third chart of infrations by infraction type
        apiService.get('/api/dashboard/getautuacoestipoByOp?Lista=' + $rootScope.FiltroOperacao, null,
            getTipoAutuacoesDataSuccess,
            getTipoAutuacoesDataFailed);


        function getTipoAutuacoesDataSuccess(result) {
            var infracoes = new Array();
            var Qtd = new Array();

            forEach(result.data[1], function (value, key) {
                if (value != 0) {
                    Qtd.push(value);
                    infracoes.push(result.data[0][key]);
                }
            });


            $scope.labels = infracoes;
            $scope.data = Qtd;
        }

        function getTipoAutuacoesDataFailed(result) {
            console.log(result.data);
        }

        $scope.colors = ["rgb(151,187,205)", "rgb(247,70,74)", "rgb(148,159,177)"];

        //forth chart infraction by days
        apiService.get('/api/dashboard/getautuacoesdiaByOp?Lista=' + $rootScope.FiltroOperacao, null,
            getAutuacoesDiaDataSuccess,
            getAutuacoesDiaDataFailed);

        $scope.InfractionDayseries = [['Quantidade']];

        function getAutuacoesDiaDataSuccess(result) {

            $scope.InfractionDaylabels = result.data[0];
            $scope.InfractionDaydata = [result.data[1]];
        }

        function getAutuacoesDiaDataFailed(result) {
            console.log(result.data);
        }


        $scope.onClick = function (points, evt) {
            console.log(points, evt);
        };

        console.log($rootScope.FiltroOperacao);
        //fifth chart surveillances that were noticed or not by day
        apiService.get('/api/dashboard/gettotalautuacoesByOp?Lista=' + $rootScope.FiltroOperacao, null,
            getTotalAutuacoesSuccess,
            getTotalAutuacoesFailed);

        $scope.InfractionPercentseries = ['Total', 'Autuadas'];

        function getTotalAutuacoesSuccess(result) {
            $scope.InfractionPercentlabels = result.data[0];
            result.data[2].push(0);
            $scope.InfractionPercentdata = [result.data[1], result.data[2]];
        }

        function getTotalAutuacoesFailed(result) {
            console.log(result.data);
        }


        $scope.onClick = function (points, evt) {
            console.log(points, evt);
        };

        $scope.InfractionPercentcolors = ["rgb(151,187,205)", "rgb(250,109,33)"];
        //----------------------------------------------- CHARTS AREA----------------------------------------------




        function loadFiscalizacao(idFiscalizacao) {

            $modal.open({
                animation: true,
                templateUrl: 'scripts/AppKor/fiscalizacao/detalhefiscalizacao.html',
                controller: 'detalhefiscalizacaoCtrl',
                size: 'lg',
                resolve: {
                    items: function () {
                        return idFiscalizacao;
                    }
                }
            });

        }

        function loadFichaDigital(idFiscalizacao) {

            $modal.open({
                animation: true,
                templateUrl: 'scripts/AppKor/fiscalizacao/fichadigital.html',
                controller: 'fichadigitalCtrl',
                size: 'lg',
                resolve: {
                    items: function () {
                        return idFiscalizacao;
                    }
                }
            });

        }

        function loadMap(localizacao) {

            var coordenadas = localizacao.split(",");

            $modal.open({
                animation: true,
                templateUrl: 'scripts/AppKor/fiscalizacao/localizacaoOperacao.html',
                controller: 'localizacaoOperacaoCtrl',
                size: 'md',
                resolve: {
                    items: function () {
                        return localizacao;
                    }
                }
            });

        }

    }

})(angular.module('AppKor'));