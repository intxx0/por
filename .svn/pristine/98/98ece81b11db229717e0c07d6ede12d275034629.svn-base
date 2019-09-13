(function (app) {
    'use strict';

    app.controller('historicoAlarmesCtrl', historicoAlarmesCtrl);

    historicoAlarmesCtrl.$inject = ['$scope', '$rootScope', 'apiService', 'notificationService', '$timeout', '$modal'];

    function historicoAlarmesCtrl($scope, $rootScope, apiService, notificationService, $timeout, $modal) {

        $scope.eventos = [];

        $scope.loadFichaDigital = loadFichaDigital;
        $scope.loadFiscalizacao = loadFiscalizacao;
        $scope.loadMap = loadMap;
        $scope.visualizaEvento = visualizaEvento;

        var promiseEventos;
        var contadorEventos = 15;

        function loadEventos() {

            apiService.get('/api/evento/listareventos', null,
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

                        console.log($rootScope.EmergenciasAtivas);

                        $rootScope.EmergenciasAtivas = $rootScope.EmergenciasAtivas - 1;

                        console.log($rootScope.EmergenciasAtivas);

                        $scope.eventos[index].NomeUsuarioVisualizado = JSON.parse(localStorage.userData).DescNome;
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