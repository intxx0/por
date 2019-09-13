(function (app) {
    'use strict';

    app.controller('modalMapaCtrl', modalMapaCtrl);
    modalMapaCtrl.$inject = ['$scope', '$modalInstance', 'items', 'apiService', 'notificationService', '$translate', 'NgMap', '$timeout'];

    function modalMapaCtrl($scope, $uibModalInstance, items, apiService, notificationService, $translate, NgMap, $timeout) {

        $scope.cancel = cancel;
        $scope.coordenadas = [];
        $scope.esconderMostrarMapa = false;

        function cancel() {
            $uibModalInstance.dismiss();
        };

        var coordenadas = items.split(" ");

        $scope.coordenadas.Latitude = coordenadas[0];
        $scope.coordenadas.Longitude = coordenadas[1];

        var vm = this;
        NgMap.getMap().then(function (map) {
            vm.map = map;
        });

        vm.googleMapsUrl = "key=AIzaSyAJrJYOH27JqSwP3Uf8Ppmi_2mGzlzC3MQ&language=en&region=US";

        vm.mostrarMapa = function () {
            $timeout(function () {
                $scope.esconderMostrarMapa = true;
            }, 500);
        }

        vm.mostrarMapa();
    }

})(angular.module('AppKor'));