﻿//Documentação para acessar api do mapa

// https://ngmap.github.io/#/!control-custom-state.html

(function (app) {
    'use strict';

    app.controller('homeCtrl', homeCtrl)

    homeCtrl.$inject = ['$rootScope', '$scope', '$interval', 'apiService', 'notificationService', '$translate', 'NgMap', '$timeout', '$route', '$document'];

    function homeCtrl($rootScope, $scope, $interval, apiService, notificationService, $translate, NgMap, $timeout, $route, $document) {

        $scope.pegaEmergencias = pegaEmergencias;
        $scope.pegaEmergenciaHistorico = pegaEmergenciaHistorico;

        var vm = this;
        NgMap.getMap().then(function (map) {
            vm.map = map;
        });

        //vm.googleMapsUrl = "https://maps.googleapis.com/maps/api/js?key=AIzaSyAJrJYOH27JqSwP3Uf8Ppmi_2mGzlzC3MQ&language=en&region=US";
        vm.googleMapsUrl = "key=AIzaSyAJrJYOH27JqSwP3Uf8Ppmi_2mGzlzC3MQ&language=en&region=US";
        vm.pauseLoading = true;

        $timeout(function () {
            vm.pauseLoading = false;
        }, 500);

        $scope.locale = { 'language': $translate.use().substring(0, 2), 'region': $translate.use().substring(3, 2) };
        
        $scope.infoKor = [];
        var position = [];

        $scope.positions = [];

        var generateMarkers = function () {

            $scope.positions = [];

            var numMarkers = Math.floor(Math.random() * 4) + 4; //between 4 to 8 markers

            for (var i = 0; i < numMarkers; i++) {

                var lat = -4.703559 + (Math.random() / 100);
                var lng = -60.7906863 + (Math.random() / 100);
                $scope.positions.push({ lat: lat, lng: lng });
            }

            console.log("positions", $scope.positions);
        };

        function pegaEmergencias() {

            apiService.get('/api/evento/listareventos', null,
            (result) => {

                position = data.Items;
            },
            (result) => {

            })
        }


        function pegaEmergenciaHistorico() {

            apiService.get('/api/evento/listaemergenciasativas/', null,
            (result) => {

                var resultado = result.data.Items;

                $scope.infoKor = resultado;
            },
            (result) => {

                notificationService.displayError('Erro ao buscar kor no banco de dados!');
            });
        }


        //----------------------------------------------- CHARTS AREA----------------------------------------------
        //General 
        $scope.options = {
            legend: {
                display: true,
                position: 'bottom'
            }
        }
        $scope.barOptions = {
            legend: {
                display: true,
                position: 'top'
            }
        }

        //First chart of all operations
        apiService.get('/api/dashboard/getfiscsoperacoes/', null,
            getPieChartDataSuccess,
            getPieChartDataFailed);

        function getPieChartDataSuccess(result) {

            $scope.PieOperacaolabels = result.data[0];
            $scope.PieOperacaodata = result.data[1];
            
            $scope.PieOperacaoseries = ["Quantidade"];
            $scope.PieOperacaocolors = ["rgb(151,187,205)", "rgb(247,70,74)", "rgb(148,159,177)"];
            $scope.options = {
                legend: {
                    display: true,
                    position: 'bottom'
                }
            }

        }
        function getPieChartDataFailed(result) {
            console.log(result.data);
        }
       
        $scope.onClick = function (points, evt) {
            console.log(points, evt);
        };



        //second chart of infractions by feedstocktype 
        apiService.get('/api/dashboard/getmateriaprimaautuada/', null,
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
        apiService.get('/api/dashboard/getautuacoestipo/', null,
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
        apiService.get('/api/dashboard/getautuacoesdia/', null,
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

        //fifth chart surveillances that were noticed or not by day
        apiService.get('/api/dashboard/gettotalautuacoes/', null,
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
    }

})(angular.module('AppKor'));