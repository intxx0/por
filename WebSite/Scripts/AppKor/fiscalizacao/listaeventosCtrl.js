﻿(function (app) {
    'use strict';

    app.controller('listaeventosCtrl', listaeventosCtrl);
    listaeventosCtrl.$inject = ['$scope', '$rootScope', 'apiService', 'notificationService', '$modal', '$translate', '$timeout'];

    function listaeventosCtrl($scope, $rootScope, apiService, notificationService, $modal, $translate, $timeout) {

        $scope.eventos = [];

        $scope.loadFichaDigital = loadFichaDigital;
        $scope.loadFiscalizacao = loadFiscalizacao;
        $scope.loadMap = loadMap;
        $scope.visualizaEvento = visualizaEvento;

        $scope.Filtro = JSON.parse($rootScope.FiltroOperacao);

        var promiseEventos;
        var contadorEventos = 15;


        function loadEventos() {

            apiService.get('/api/evento/listareventosfiltro?Lista=' + $rootScope.FiltroOperacao, null,
                (result) => {

                    $scope.eventos = result.data;
                },
                (result) => {

                    console.log(result.data);
                });
        }

        function visualizaEvento(evento) {

            evento.usuarioIdVisualizado = JSON.parse(localStorage.userData).UsuarioId;
            
            if (evento.NomeUsuarioVisualizado == null) {

                apiService.post('/api/evento/atualizaevento/', evento,
                    (result) => {

                        var index = $scope.eventos.indexOf(evento);

                        $scope.eventos[index].NomeUsuarioVisualizado = JSON.parse(localStorage.userData).DescNome;

                        console.log(result.data);
                    },
                    (result) => {

                        console.log(result.data);
                    });
            }
        }

        function loadFiscalizacao(idFiscalizacao) {

            $modal.open({
                animation: true,
                templateUrl: 'scripts/AppKor/fiscalizacao/modalDetalhesFiscalizacao.html',
                controller: 'modalDetalhesFiscalizacaoCtrl',
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

            $modal.open({
                animation: true,
                templateUrl: 'scripts/AppKor/fiscalizacao/modalMapa.html',
                controller: 'modalMapaCtrl',
                size: 'md',
                resolve: {
                    items: function () {
                        return localizacao;
                    }
                }
            });
        }


        function ativarRefreshEventos() {
            contadorEventos--;
            if (contadorEventos === 0) {

                loadEventos();
                contadorEventos = 15;
            }

            promiseEventos = $timeout(ativarRefreshEventos, 1000);
        }

        $scope.$on('$destroy', function () {
            $timeout.cancel(promiseEventos);
        });


        loadEventos();

        //Carregando todos os alarmes de tempos em tempos
        ativarRefreshEventos();
    }

})(angular.module('AppKor'));